using System.Diagnostics;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Drawing2D;


/// -----------------------------------------------------------------------------
/// Project	 : XPCommonControls
/// Class	 : XPCommonControls.XPTextBox
///
/// -----------------------------------------------------------------------------
/// <summary>
/// a specially designed textbox
/// </summary>
/// <remarks>
/// this textbox should mimic the XP Design. It has rounded edges and when receiving
/// the focus, a thin orange line is drawn in the lower part of the control (similar
/// to that in tab pages.
/// everything else is like a regular textbox.
/// </remarks>
/// <history>
/// 	[Mike]	14.03.2004	Created
///     [Mike]  25.04.2004  bug: if the parents back color was transparent and the
///                         control was disabled an error was generated, because
///                         the textboxes backcolor cannot be transparent
///                         Now the first non-transparent backcolor of the controls
///                         parent or of the parents parent is used!
/// </history>
/// -----------------------------------------------------------------------------
namespace System.Windows.Forms
{
    [DefaultEvent("TextChanged")]
    public class XPTextBox : System.Windows.Forms.UserControl
    {

        private bool mHasFocus;
        private Color mBackColor;
        private Color mBorderColor;
        private Color mForeColor;
 private System.Windows.Forms.TextBox MyTextBox;

        public XPTextBox()
        {
            mHasFocus = false;
            mBackColor = SystemColors.Window;
            mBorderColor = SystemColors.ActiveBorder;
            mForeColor = SystemColors.WindowText;


            // Dieser Aufruf ist fr den Windows Form-Designer erforderlich.
            InitializeComponent();

            // Initialisierungen nach dem Aufruf InitializeComponent() hinzufgen
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ContainerControl, true);

            base.BackColor = Color.Transparent;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!(components == null))
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        
        private System.ComponentModel.Container components = null;

       
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.MyTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MyTextBox
            // 
            this.MyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MyTextBox.Location = new System.Drawing.Point(24, 8);
            this.MyTextBox.Name = "MyTextBox";
            this.MyTextBox.Size = new System.Drawing.Size(100, 13);
            this.MyTextBox.TabIndex = 0;
            this.MyTextBox.LostFocus += new System.EventHandler(this.MyTextBox_LostFocus);
            this.MyTextBox.Click += new System.EventHandler(this.MyTextBox_Click);
            this.MyTextBox.GotFocus += new System.EventHandler(this.MyTextBox_GotFocus);
            this.MyTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MyTextBox_KeyUp);
            this.MyTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            this.MyTextBox.TextChanged += new System.EventHandler(this.MyTextBox_TextChanged);
            this.MyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyTextBox_KeyDown);
            // 
            // XPTextBox
            // 
            this.Controls.Add(this.MyTextBox);
            this.Name = "XPTextBox";
            this.ParentChanged += new System.EventHandler(this.XPTextBox_ParentChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.BackColor = this.Parent.BackColor;

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            //groesse festlegen
            PositionTextBox();

            //rand zeichnen
            Pen lpenBorder = new Pen(mBorderColor, 1);
            GraphicsPath gp = new GraphicsPath();

            lpenBorder.Alignment = PenAlignment.Inset;
            gp = Paths.RoundedRect(
                new Rectangle(0, 0, this.Width <= 0 ? 1: this.Width,this.Height <= 0 ? 1: this.Height), 3, 1, Corner.All);

            if (this.Enabled)
            {
                e.Graphics.FillPath(new SolidBrush(mBackColor), gp);
                MyTextBox.BackColor = mBackColor;
                //orange linie zeichnen wenn focus erhalten...
                if (mHasFocus)
                {
                    e.Graphics.DrawLine(new Pen(Color.Orange, 2), 1, this.Height - 2, this.Width - 2, this.Height - 2);
                }
                e.Graphics.DrawPath(lpenBorder, gp);
            }
            else
            {
                //textbox does not support transparent back color, so
                //check for the fist nontransparent backcolor
                MyTextBox.BackColor = CtrlHelper.GetNonTransparentBackColor(this);
                e.Graphics.FillPath(new SolidBrush(this.Parent.BackColor), gp);
                e.Graphics.DrawPath(lpenBorder, gp);
            }

            gp.Dispose();
            lpenBorder.Dispose();

            base.OnPaint(e);
        }


        #region "Public Properties"

        [Browsable(true), Description("The textBox use to draw the control"), Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
       
        public TextBox TextBoxBase
        {
            get { return MyTextBox; }
            set { MyTextBox = value; this.Invalidate(); }
        }

        public bool AcceptsReturn
        {
            get { return MyTextBox.AcceptsReturn; }
            set { MyTextBox.AcceptsReturn = value; }
        }

        public bool AcceptsTab
        {
            get { return MyTextBox.AcceptsTab; }
            set { MyTextBox.AcceptsTab = value; }
        }

        public ScrollBars ScrollBars
        {
            get { return MyTextBox.ScrollBars; }
            set { MyTextBox.ScrollBars = value; }
        }

        public int TextLength
        {
            get { return MyTextBox.TextLength; }
        }

        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get { return MyTextBox.AutoCompleteCustomSource; }
            set { MyTextBox.AutoCompleteCustomSource = value; }
        }

        public bool ReadOnly
        {
            get { return MyTextBox.ReadOnly; }
            set { MyTextBox.ReadOnly = value; }
        }


        public bool UseSystemPasswordChar
        {
            get { return MyTextBox.UseSystemPasswordChar; }
            set { MyTextBox.UseSystemPasswordChar = value; }
        }

        public int MaxLength
        {
            get { return MyTextBox.MaxLength; }
            set { MyTextBox.MaxLength = value; }
        }

        public AutoCompleteSource AutoCompleteSource
        {
            get { return MyTextBox.AutoCompleteSource; }
            set { MyTextBox.AutoCompleteSource = value; }
        }



        public AutoCompleteMode AutoCompleteMode
        {
            get { return MyTextBox.AutoCompleteMode; }
            set { MyTextBox.AutoCompleteMode = value; }
        }

        public void Paste()
        {
            MyTextBox.Paste();
        }

        [Browsable(true), Description("The Backgroundcolor"), Category("Appearance"), DefaultValue(typeof(Color), "Window"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Color BackColor
        {
            get
            {
                return mBackColor;
            }
            set
            {
                mBackColor = value;
                MyTextBox.BackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), Description("the border color for the textbox"), Category("Appearance"), DefaultValue(typeof(Color), "ActiveBorder"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get
            {
                return mBorderColor;
            }
            set
            {
                mBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), Description("the font of the textbox"), Category("Appearance"), DefaultValue(typeof(Font), "Microsoft Sans Serif, 8.25pt"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                MyTextBox.Font = value;
                MyTextBox.Invalidate();
                this.Invalidate();
            }
        }

        [Browsable(true), Description("show multiple lines of text in the control"), Category("Behavior"), DefaultValue(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Multiline
        {
            get
            {
                return MyTextBox.Multiline;
            }
            set
            {
                MyTextBox.Multiline = value;
                this.Invalidate();
            }
        }

        [Browsable(true), Description("the forecolor (font color) of the control"), Category("Appearance"), DefaultValue(typeof(Color), "WindowText"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color ForeColor
        {
            get
            {
                return mForeColor;
            }
            set
            {
                mForeColor = value;
                MyTextBox.ForeColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), Localizable(true), Description("the text to display in the control"), Category("Appearance"), DefaultValue(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return MyTextBox.Text;
            }
            set
            {
                MyTextBox.Text = value;
                this.Invalidate();
            }
        }

        [Browsable(true), Localizable(true), Description("checks if the control is enabled or disabled"), Category("Appearance"), DefaultValue(true)]
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                this.Invalidate();
            }
        }

        public void Select(int start, int lenght)
        {
            MyTextBox.Select(start, lenght);
        }



        #endregion

        #region "private methods"
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            PositionTextBox();
        }

        private void PositionTextBox()
        {
            //positionieren der textbox
            System.Windows.Forms.TextBox with_1 = MyTextBox;
            with_1.Left = 5;
            with_1.Top = 5;
            with_1.Width = this.Width - 10;
            if (with_1.Multiline)
            {
                with_1.Height = this.Height - 10;
            }
            else
            {
                this.Height = with_1.Height + 10;
            }
        }

        #endregion

        #region "tb events to bubble up"
        private EventHandler TextChangedEvent;

        [Browsable(true)]
        public new event EventHandler TextChanged
        {
            add
            {
                TextChangedEvent = (EventHandler)System.Delegate.Combine(TextChangedEvent, value);
            }
            remove
            {
                TextChangedEvent = (EventHandler)System.Delegate.Remove(TextChangedEvent, value);
            }
        }

        private EventHandler ValidatedEvent;

        public new event EventHandler Validated
        {
            add
            {
                ValidatedEvent = (EventHandler)System.Delegate.Combine(ValidatedEvent, value);
            }
            remove
            {
                ValidatedEvent = (EventHandler)System.Delegate.Remove(ValidatedEvent, value);
            }
        }


        private EventHandler ValidatingEvent;

        public new event EventHandler Validating
        {
            add
            {
                ValidatingEvent = (EventHandler)System.Delegate.Combine(ValidatingEvent, value);
            }
            remove
            {
                ValidatingEvent = (EventHandler)System.Delegate.Remove(ValidatingEvent, value);
            }
        }

        private System.Windows.Forms.KeyEventHandler KeyDownEvent;

        public new event KeyEventHandler KeyDown
        {
            add
            {
                KeyDownEvent = (KeyEventHandler)System.Delegate.Combine(KeyDownEvent, value);
            }
            remove
            {
                KeyDownEvent = (KeyEventHandler)System.Delegate.Remove(KeyDownEvent, value);
            }
        }

        private System.Windows.Forms.KeyPressEventHandler KeyPressEvent;

        public new event KeyPressEventHandler KeyPress
        {
            add
            {
                KeyPressEvent = (KeyPressEventHandler)System.Delegate.Combine(KeyPressEvent, value);
            }
            remove
            {
                KeyPressEvent = (KeyPressEventHandler)System.Delegate.Remove(KeyPressEvent, value);
            }
        }

       private System.Windows.Forms.KeyEventHandler KeyUpEvent;

        public new event System.Windows.Forms.KeyEventHandler KeyUp
        {
            add
            {
                KeyUpEvent = (KeyEventHandler)System.Delegate.Combine(KeyUpEvent, value);
            }
            remove
            {
                KeyUpEvent = (KeyEventHandler)System.Delegate.Remove(KeyUpEvent, value);
            }
        }

        private EventHandler GotFocusEvent;

        public new event EventHandler GotFocus
        {
            add
            {
                GotFocusEvent = (EventHandler)System.Delegate.Combine(GotFocusEvent, value);
            }
            remove
            {
                GotFocusEvent = (EventHandler)System.Delegate.Remove(GotFocusEvent, value);
            }
        }

        private EventHandler LostFocusEvent;

        public new event EventHandler LostFocus
        {
            add
            {
                LostFocusEvent = (EventHandler)System.Delegate.Combine(LostFocusEvent, value);
            }
            remove
            {
                LostFocusEvent = (EventHandler)System.Delegate.Remove(LostFocusEvent, value);
            }
        }

        private EventHandler ParentChangedEvent;

        public new event EventHandler Click;

        public new event EventHandler ParentChanged
        {
            add
            {
                ParentChangedEvent = (EventHandler)System.Delegate.Combine(ParentChangedEvent, value);
            }
            remove
            {
                ParentChangedEvent = (EventHandler)System.Delegate.Remove(ParentChangedEvent, value);
            }
        }


        private void MyTextBox_TextChanged(System.Object sender, System.EventArgs e)
        {
            if (TextChangedEvent != null)
                TextChangedEvent(this, e);
        }

        private void MyTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (KeyDownEvent != null)
                KeyDownEvent(this, e);
        }

        private void MyTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (KeyPressEvent != null)
                KeyPressEvent(this, e);
        }

        private void MyTextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (KeyUpEvent != null)
                KeyUpEvent(this, e);
        }

        private void MyTextBox_GotFocus(object sender, System.EventArgs e)
        {
            mHasFocus = true;
            this.Invalidate();

        }

        private void MyTextBox_LostFocus(object sender, System.EventArgs e)
        {
            mHasFocus = false;
            this.Invalidate();
        }

        private void XPTextBox_ParentChanged(object sender, System.EventArgs e)
        {
            this.Invalidate();
        }
        #endregion

        private void MyTextBox_Click(object sender, EventArgs e)
        {
            if (Click != null) Click(this, EventArgs.Empty);
        }
    }

    [Flags()]
    internal enum Corner
    {
        None = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 4,
        BottomRight = 8,
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }

    /// -----------------------------------------------------------------------------
    /// Project	 : XPCommonControls
    /// Class	 : XPCommonControls.Paths
    ///
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// predefined paths for drawing routines
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// 	[Mike]	14.03.2004	Created
    /// </history>
    /// -----------------------------------------------------------------------------
    internal class Paths
    {
        public static System.Drawing.Drawing2D.GraphicsPath RoundedRect(RectangleF rect, int cornerradius, int margin, Corner roundedcorners)
        {
            System.Drawing.Drawing2D.GraphicsPath p = new System.Drawing.Drawing2D.GraphicsPath();
            float x = rect.X;
            float y = rect.Y;
            float w = rect.Width;
            float h = rect.Height;
            int m = margin;
            int r = cornerradius;

            p.StartFigure();
            //top left arc
            if (System.Convert.ToBoolean(roundedcorners & Corner.TopLeft))
            {
                p.AddArc(CtrlHelper.CheckedRectangleF(x + m, y + m, 2 * r, 2 * r), 180, 90);
            }
            else
            {
                p.AddLine(new PointF(x + m, y + m + r), new PointF(x + m, y + m));
                p.AddLine(new PointF(x + m, y + m), new PointF(x + m + r, y + m));
            }

            //top line
            p.AddLine(new PointF(x + m + r, y + m), new PointF(x + w - m - r, y + m));

            //top right arc
            if (System.Convert.ToBoolean(roundedcorners & Corner.TopRight))
            {
                p.AddArc(CtrlHelper.CheckedRectangleF(x + w - m - 2 * r, y + m, 2 * r, 2 * r), 270, 90);
            }
            else
            {
                p.AddLine(new PointF(x + w - m - r, y + m), new PointF(x + w - m, y + m));
                p.AddLine(new PointF(x + w - m, y + m), new PointF(x + w - m, y + m + r));
            }

            //right line
            p.AddLine(new PointF(x + w - m, y + m + r), new PointF(x + w - m, y + h - m - r));

            //bottom right arc
            if (System.Convert.ToBoolean(roundedcorners & Corner.BottomRight))
            {
                p.AddArc(CtrlHelper.CheckedRectangleF(x + w - m - 2 * r, y + h - m - 2 * r, 2 * r, 2 * r), 0, 90);
            }
            else
            {
                p.AddLine(new PointF(x + w - m, y + h - m - r), new PointF(x + w - m, y + h - m));
                p.AddLine(new PointF(x + w - m, y + h - m), new PointF(x + w - m - r, y + h - m));
            }

            //bottom line
            p.AddLine(new PointF(x + w - m - r, y + h - m), new PointF(x + m + r, y + h - m));

            //bottom left arc
            if (System.Convert.ToBoolean(roundedcorners & Corner.BottomLeft))
            {
                p.AddArc(CtrlHelper.CheckedRectangleF(x + m, y + h - m - 2 * r, 2 * r, 2 * r), 90, 90);
            }
            else
            {
                p.AddLine(new PointF(x + m + r, y + h - m), new PointF(x + m, y + h - m));
                p.AddLine(new PointF(x + m, y + h - m), new PointF(x + m, y + h - m - r));
            }

            //left line
            p.AddLine(new PointF(x + m, y + h - m - r), new PointF(x + m, y + m + r));

            //close figure...
            p.CloseFigure();

            return p;
        }

        

        public static System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle rect, int cornerradius, int margin, Corner roundedcorners)
        {
            return RoundedRect(CtrlHelper.CheckedRectangleF(rect.Left, rect.Top, rect.Width, rect.Height), cornerradius, margin, roundedcorners);
        }
        
    }

    internal class CtrlHelper
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// this function returns the first non-transparent color of a control's parent(s)
        /// loops through all parents to find the first non transparent color
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns>the parents background color</returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[Mike]	25.04.2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static Color GetNonTransparentBackColor(Control ctrl)
        {
            Color bc;
            Control p;
            try
            {
                p = ctrl.Parent;
                bc = p.BackColor;
                while (bc.Equals(Color.Transparent) && p.Parent != null)
                {
                    p = p.Parent;
                    bc = p.BackColor;
                }

                if (bc.Equals(Color.Transparent))
                {
                    bc = SystemColors.Control;
                }

                return bc;
            }
            catch (Exception)
            {
                return SystemColors.Control;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// this function returns a checked rectangle (width and height always > 1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[Mike]	11.03.2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static RectangleF CheckedRectangleF(float x, float y, float width, float height)
        {
            return new RectangleF(x, y, width <= 0? 1: width, height <= 0? 1: height);

        }
    }
}
