using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace System.Windows.FutureStyle
{
    public partial class ReflectionPicture : UserControl
    {
        public ReflectionPicture()
        {
            indiceOpacidad = 165;
            distanciaReflejo = 5;
            restringirProporciones = true;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.Resize += new EventHandler(ReflectionPicture_Resize);
            areaImagen = ObtenerAreaImagen();
        }

        void ReflectionPicture_Resize(object sender, EventArgs e)
        {
            areaImagen = ObtenerAreaImagen();
            ActualizarReflejo();
        }

        private Image reflejo;
        private Rectangle areaImagen;
        private Image imagen;
        public Image Image
        {
            get { return imagen; }
            set
            {
                imagen = value;

                if (value == null)
                {
                    this.Refresh();
                    return;
                }

                if (cache.ContainsKey(imagen.GetHashCode()) && cache[imagen.GetHashCode()]!=null)
                {
                    reflejo = cache[imagen.GetHashCode()];
                    this.Refresh();
                }
                else
                {
                    ActualizarReflejo();
                    if(reflejo!=null)
                    cache.Add(imagen.GetHashCode(), (Image)reflejo.Clone());
                }

            }
        }
        private Dictionary<int, Image> cache=new Dictionary<int,Image>();

        private bool restringirProporciones;
        public bool AjustSize
        {
            get { return restringirProporciones; }
            set { restringirProporciones = value; ActualizarReflejo();  }
        }

        private int distanciaReflejo;
        public int ReflectDistance
        {
            get { return distanciaReflejo; }
            set { distanciaReflejo = value; ActualizarReflejo();  }
        }


        private byte indiceOpacidad;
        public byte OpacityIndex
        {
            get { return indiceOpacidad; }
            set { indiceOpacidad = value; ActualizarReflejo();  }
        }

        private bool actualizando;
        private void SubActualizarReflejo()
        {
            try
            {
                actualizando = true;
                this.reflejo = null;

                Bitmap volteada = new Bitmap(imagen, areaImagen.Size);
                volteada.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Bitmap reflejo = new Bitmap(volteada.Width, volteada.Height, PixelFormat.Format32bppArgb);

                for (int y = 0; y < volteada.Height; y++)
                {
                    //Opacidad resultante para un pixel totalmente opaco
                    int opacidad = (int)Math.Round(((double)255 / volteada.Height) * (volteada.Height - y)) - indiceOpacidad;

                    if (opacidad < 0) opacidad = 0;
                    if (opacidad > 255) opacidad = 255;

                    for (int x = 0; x < volteada.Width; x++)
                    {
                        Color color = volteada.GetPixel(x, y);
                        if (color.A == 0)
                            continue;

                        int opacidadPixel = (int)Math.Round((opacidad / (double)255) * color.A);
                        reflejo.SetPixel(x, y, Color.FromArgb(opacidadPixel, color.R, color.G, color.B));
                    }
                }

                this.reflejo = reflejo;
                actualizando = false;
              
                this.Invoke(new EventHandler(refrescar));
            }
            catch
            {
                //MessageBox.Show(error.ToString()+"\r\n"+this.Handle);
                this.reflejo = null;
            }
        }

        private void refrescar(object sender, EventArgs e)
        {
           // OnPaint(new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle));
            this.Refresh();
        }


        private void ActualizarReflejo()
        {
            try
            {
                if (imagen == null || actualizando == true)
                    return;

                System.Threading.Thread sub = new System.Threading.Thread(new System.Threading.ThreadStart(SubActualizarReflejo));
            //    sub.Priority = System.Threading.ThreadPriority.Normal;
                sub.Start();
            }
            catch
            {
                this.reflejo = null;
            }
        }

        private Rectangle ObtenerAreaImagen()
        {
           Rectangle rectangulo= new Rectangle(0, 0, this.Width, (int)(this.Height * 0.6));
           if (restringirProporciones && imagen!=null)
           {
               int ancho = imagen.Width;
               int alto = imagen.Height;

               if (ancho > rectangulo.Width)
               {
                   ancho = rectangulo.Width;
                   alto = (int)Math.Round(imagen.Height * ancho / (decimal)imagen.Width);
               }
               if (alto > rectangulo.Height)
               {
                   alto = rectangulo.Height;
                   ancho = (int)Math.Round(imagen.Width * alto / (decimal)imagen.Height);          
               }

               rectangulo.Width = ancho;
               rectangulo.Height = alto;
           }
            return rectangulo;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if (this.DesignMode)
                {
                    e.Graphics.DrawString(areaImagen.Width + "x" + areaImagen.Height, this.Font, new SolidBrush(this.ForeColor), 10, 10);
                    e.Graphics.DrawRectangle(new Pen(this.ForeColor, 2f), areaImagen);
                }
                if (imagen == null)
                    return;
              
                //Dibujar el reflejo                   
                if (reflejo != null)
                {
                    Rectangle rectanguloReflejo = new Rectangle(0, areaImagen.Height + distanciaReflejo, areaImagen.Width, areaImagen.Height);
                    e.Graphics.DrawImage(reflejo, rectanguloReflejo);
                }
                else
                    ActualizarReflejo();

                //Dibujar la imagen   
                e.Graphics.DrawImage(imagen, areaImagen);
            }
            catch 
            {
                e.Graphics.Clear(Color.Transparent);
            }
        }
    }
}
