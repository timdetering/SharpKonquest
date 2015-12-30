using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class GlassForm : Form
    {
        public GlassForm()
        {
            this.ResizeRedraw = true;

            GlassPaddding = new Padding(0);
   
            this.Shown += new EventHandler(GlassForm_Shown);
            this.glowSize = 10;
        }

        public Padding GlassPaddding
        {
            get
            {
                return glassPadding;
            }
            set
            {
                glassPadding = value;

                if (GlassEffectsEnabled)
                {
                    MARGINS mg = new MARGINS();
                    if (glassPadding.Bottom > 0)
                        mg.m_Buttom = glassPadding.Bottom - SystemInformation.MinWindowTrackSize.Height;
                    if (glassPadding.Left > 0)
                        mg.m_Left = glassPadding.Left - SystemInformation.MinWindowTrackSize.Height;
                    if (glassPadding.Right > 0)
                        mg.m_Right = glassPadding.Right - SystemInformation.MinWindowTrackSize.Height;
                    if (glassPadding.Top > 0)
                        mg.m_Top = glassPadding.Top - SystemInformation.MinWindowTrackSize.Height;

                    DwmExtendFrameIntoClientArea(this.Handle, ref mg);
                }
                this.Refresh();
            }
        }
        private Padding glassPadding;

        private int glowSize;
        public int GlowSize
        {
            get { return glowSize; }
            set { glowSize = value; this.Refresh(); }
        }

        public bool GlassEffectsEnabled
        {
            get { return System.Environment.OSVersion.Version.Major >= 6 && DwmIsCompositionEnabled(); }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (DesignMode || GlassEffectsEnabled)
            {
                foreach (Rectangle rectangulo in MarginsRectangle(glassPadding))
                {
                    if (DesignMode)
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightCyan), rectangulo);
                    else
                        e.Graphics.FillRectangle(new SolidBrush(Color.Black), rectangulo);
                }
                /*
                //Pintar el resto
                Rectangle areaNormal = new Rectangle(glassPadding.Left, glassPadding.Top,
                    this.Width - (glassPadding.Left + glassPadding.Right),
                    this.Height - (glassPadding.Top + glassPadding.Bottom));

                if (areaNormal.Width > 0 && areaNormal.Height > 0)
                {
                    if (DesignMode)
                        e.Graphics.DrawRectangle(new Pen(Color.Red), areaNormal);

                    e.Graphics.FillRectangle(new SolidBrush(this.BackColor), areaNormal);
                }*/
            }
        }

        private Rectangle[] MarginsRectangle(Padding margins)
        {
            List<Rectangle> rectangulos = new List<Rectangle>();
            if (margins.Left > 0)
                rectangulos.Add(new Rectangle(0, 0, margins.Left, this.Height));
            if (margins.Top > 0)
                rectangulos.Add(new Rectangle(0, 0, this.Width, margins.Top));
            if (margins.Right > 0)
                rectangulos.Add(new Rectangle(this.Width - margins.Right, 0, margins.Right, this.Height));
            if (margins.Bottom > 0)
                rectangulos.Add(new Rectangle(0, this.Height - margins.Bottom, this.Width, margins.Bottom));

            return rectangulos.ToArray();
        }

        public void DrawGlassText(string text, Rectangle rect)
        {
            if (System.Environment.OSVersion.Version.Major >= 6 && DwmIsCompositionEnabled())
            {
                this.DrawGlassText(this.Handle, text, this.Font, rect, glowSize);
            }
            else
            {
                StringFormat formato = StringFormat.GenericDefault;
                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;

                this.CreateGraphics().DrawString(text, this.Font, new SolidBrush(this.ForeColor), rect, formato);
            }
        }

        void GlassForm_Shown(object sender, EventArgs e)
        {
            this.ControlAdded += new ControlEventHandler(GlassForm_ControlAdded);
            CheckControls(this.Controls);
        }

        private void CheckControls(Control.ControlCollection coleccion)
        {
            foreach (Control control in coleccion)
            {
                Control_LocationChanged(control, null);

                if (control.Controls != null && control.Controls.Count > 0)
                    CheckControls(control.Controls);
            }
        }

        void GlassForm_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.LocationChanged += new EventHandler(Control_LocationChanged);
            Control_LocationChanged(e.Control, null);
        }

        void Control_LocationChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            bool glassEffects = GlassEffectsEnabled && !DesignMode;

            if (controles == null)
                controles = new List<Control>();

            if (control is Label || control is PictureBox)
            {
                bool intersect = false;

                foreach (Rectangle rect in MarginsRectangle(glassPadding))
                {
                    intersect = control.Bounds.IntersectsWith(rect);
                    if (intersect) break;
                }

                if (intersect && glassEffects)//Is into the glass zone
                {
                    control.Visible = false;
                    controles.Add(control);
                    this.Paint += new PaintEventHandler(GlassForm_Paint);
                }
                else
                {
                    control.Visible = true;
                    if (controles.Contains(control))
                        controles.Remove(control);
                }
            }
            else if (control is Button)
            {
                if (glassEffects)
                {
                    Button boton = (Button)control;
                    boton.FlatStyle = FlatStyle.System;
                  /*  int BM_SETIMAGE = 0x00F7;
                    SendMessage(boton.Handle, BM_SETIMAGE, 1, (int)SystemIcons.Asterisk.Handle);*/
                }
                else
                {
                    Button boton = (Button)control;
                    boton.FlatStyle = FlatStyle.Standard;
                }
            }
            else if (control is ListView)
            {
                if (glassEffects)
                {
                    SetWindowTheme(control.Handle, "explorer", null); //Explorer style
                    SendMessage(control.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_DOUBLEBUFFER, LVS_EX_DOUBLEBUFFER);
                }
            }
            else if (control is TreeView)
            {
                if (glassEffects)
                {
                    SetWindowTheme(control.Handle, "explorer", null); //Explorer style
                    SendMessage(control.Handle, TVM_SETEXTENDEDSTYLE, TVS_EX_FADEINOUTEXPANDOS, TVS_EX_FADEINOUTEXPANDOS);
                    SendMessage(control.Handle, TVM_SETEXTENDEDSTYLE, TVS_EX_AUTOHSCROLL, TVS_EX_AUTOHSCROLL);
                }
            }
            else if (control is CheckBox)
            {
                if (glassEffects)
                    ((CheckBox)control).FlatStyle = FlatStyle.System;
                else
                    ((CheckBox)control).FlatStyle = FlatStyle.Standard;
            }
            else if (control is RadioButton)
            {
                if (glassEffects)
                    ((RadioButton)control).FlatStyle = FlatStyle.System;
                else
                    ((RadioButton)control).FlatStyle = FlatStyle.Standard;
            }
        }

        private List<Control> controles;

        void GlassForm_Paint(object sender, PaintEventArgs e)
        {
            bool glassEffects = GlassEffectsEnabled && !DesignMode;
            foreach (Control control in controles)
            {
                if (control is Label)
                {
                    if (glassEffects)
                        this.DrawGlassText(this.Handle, control.Text, control.Font, control.Bounds, glowSize);
                    else if (control.Visible == false)
                        control.Visible = true;
                }
                else if (control is PictureBox)
                {
              //      e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    if (glassEffects)
                        e.Graphics.DrawImage(((PictureBox)control).Image, control.Bounds);
                    else if (control.Visible == false)
                        control.Visible = true;
                }
            }
        }

        #region API

        //Treeview universal constants
        public const int TV_FIRST = 0x1100;
        //Treeview messages
        public const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
        public const int TVM_GETEXTENDEDSTYLE = TV_FIRST + 45;
        public const int TVM_SETAUTOSCROLLINFO = TV_FIRST + 59;
        //Treeview styles
        public const int TVS_NOHSCROLL = 0x8000;
        //Treeview extended styles
        public const int TVS_EX_AUTOHSCROLL = 0x0020;
        public const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;
        public const int GWL_STYLE = -16;

        //Listview universal constants
        public const int LVM_FIRST = 0x1000;
        //Listview messages
        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
        public const int LVS_EX_FULLROWSELECT = 0x00000020;
        //Listview extended styles
        public const int LVS_EX_DOUBLEBUFFER = 0x00010000;

        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);

        private const int DTT_COMPOSITED = (int)(1UL << 13);
        private const int DTT_GLOWSIZE = (int)(1UL << 11);

        //Text format consts
        private const int DT_SINGLELINE = 0x00000020;
        private const int DT_CENTER = 0x00000001;
        private const int DT_VCENTER = 0x00000004;
        private const int DT_NOPREFIX = 0x00000800;

        //Const for BitBlt
        private const int SRCCOPY = 0x00CC0020;


        //Consts for CreateDIBSection
        private const int BI_RGB = 0;
        private const int DIB_RGB_COLORS = 0;//color table in RGBs

        public struct MARGINS
        {
            public int m_Left;
            public int m_Right;
            public int m_Top;
            public int m_Buttom;
        };

        private struct POINTAPI
        {
            public int x;
            public int y;
        };

        private struct DTTOPTS
        {
            public uint dwSize;
            public uint dwFlags;
            public uint crText;
            public uint crBorder;
            public uint crShadow;
            public int iTextShadowType;
            public POINTAPI ptShadowOffset;
            public int iBorderSize;
            public int iFontPropId;
            public int iColorPropId;
            public int iStateId;
            public int fApplyOverlay;
            public int iGlowSize;
            public IntPtr pfnDrawTextCallback;
            public int lParam;
        };

        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;


        };

        private struct BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        };

        private struct RGBQUAD
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;
        };

        private struct BITMAPINFO
        {
            public BITMAPINFOHEADER bmiHeader;
            public RGBQUAD bmiColors;
        };


        //API declares
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern bool DwmIsCompositionEnabled();
        [DllImport("dwmapi.dll")]
        internal static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margin);


        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetDC(IntPtr hdc);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern int SaveDC(IntPtr hdc);
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern int ReleaseDC(IntPtr hdc, int state);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

        [DllImport("UxTheme.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DTTOPTS pOptions);
        [DllImport("UxTheme.dll", ExactSpelling = true, SetLastError = true)]
        private static extern int DrawThemeText(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags1, int dwFlags2, ref RECT pRect);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);

        public void DrawGlassText(IntPtr hwnd, String text, Font font, Rectangle ctlrct, int iglowSize)
        {
            RECT rc = new RECT();
            RECT rc2 = new RECT();

            rc.left = ctlrct.Left;
            rc.right = ctlrct.Right + 2 * iglowSize;  //make it larger to contain the glow effect
            rc.top = ctlrct.Top;
            rc.bottom = ctlrct.Bottom + 2 * iglowSize;

            //Just the same rect with rc,but (0,0) at the lefttop
            rc2.left = 0;
            rc2.top = 0;
            rc2.right = rc.right - rc.left;
            rc2.bottom = rc.bottom - rc.top;

            IntPtr destdc = GetDC(hwnd);    //hwnd must be the handle of form,not control
            IntPtr Memdc = CreateCompatibleDC(destdc);   // Set up a memory DC where we'll draw the text.
            IntPtr bitmap;
            IntPtr bitmapOld = IntPtr.Zero;
            IntPtr logfnotOld;

            int uFormat = DT_SINGLELINE | DT_CENTER | DT_VCENTER | DT_NOPREFIX;   //text format

            BITMAPINFO dib = new BITMAPINFO();
            dib.bmiHeader.biHeight = -(rc.bottom - rc.top);         // negative because DrawThemeTextEx() uses a top-down DIB
            dib.bmiHeader.biWidth = rc.right - rc.left;
            dib.bmiHeader.biPlanes = 1;
            dib.bmiHeader.biSize = Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            dib.bmiHeader.biBitCount = 32;
            dib.bmiHeader.biCompression = BI_RGB;
            if (!(SaveDC(Memdc) == 0))
            {
                bitmap = CreateDIBSection(Memdc, ref dib, DIB_RGB_COLORS, 0, IntPtr.Zero, 0);   // Create a 32-bit bmp for use in offscreen drawing when glass is on
                if (!(bitmap == IntPtr.Zero))
                {
                    bitmapOld = SelectObject(Memdc, bitmap);
                    IntPtr hFont = font.ToHfont();
                    logfnotOld = SelectObject(Memdc, hFont);
                    try
                    {

                        System.Windows.Forms.VisualStyles.VisualStyleRenderer renderer = new System.Windows.Forms.VisualStyles.VisualStyleRenderer(System.Windows.Forms.VisualStyles.VisualStyleElement.Window.Caption.Active);

                        DTTOPTS dttOpts = new DTTOPTS();

                        dttOpts.dwSize = (uint)Marshal.SizeOf(typeof(DTTOPTS));

                        dttOpts.dwFlags = DTT_COMPOSITED | DTT_GLOWSIZE;


                        dttOpts.iGlowSize = iglowSize;

                        DrawThemeTextEx(renderer.Handle, Memdc, 0, 0, text, -1, uFormat, ref rc2, ref dttOpts);




                        BitBlt(destdc, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, Memdc, 0, 0, SRCCOPY);



                    }
                    catch
                    {
                    }


                    //Remember to clean up
                    SelectObject(Memdc, bitmapOld);
                    SelectObject(Memdc, logfnotOld);
                    DeleteObject(bitmap);
                    DeleteObject(hFont);

                    ReleaseDC(Memdc, -1);
                    DeleteDC(Memdc);



                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        #endregion
    }
}