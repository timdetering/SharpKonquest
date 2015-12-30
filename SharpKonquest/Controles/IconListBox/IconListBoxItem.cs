using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace System.Windows.Forms
{
    public class IconListBoxItem : Label
    {
        public IconListBoxItem()
        {
            this.Height = 48;
            this.Width = 200;
            valorBarraProgreso = -1;
            this.BackColor = Color.Transparent;
            base.Text = string.Empty;
            this.texto = "IconListBoxItem";
            this.ResizeRedraw = true;           

            this.Click += new EventHandler(Seleccionar);
            this.Paint += new PaintEventHandler(Dibujar);
            this.KeyDown += new KeyEventHandler(TeclaPulsada);
        }

        void TeclaPulsada(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (Parent.SelectedItem == null)
                    Parent.SelectedItem = Parent.Items[0];
                else if (Parent.Items.IndexOf(Parent.SelectedItem) == Parent.Items.Count - 1)
                {
                    return;
                }
                else
                {
                    int indiceSiguiente=Parent.Items.IndexOf(Parent.SelectedItem) + 1;
                    Parent.SelectedItem = Parent.Items[indiceSiguiente];
                }
            }
            else if(e.KeyCode == Keys.Up)
            {
                if (Parent.SelectedItem == null)
                    Parent.SelectedItem = Parent.Items[0];
                else if (Parent.Items.IndexOf(Parent.SelectedItem) == 0)
                {
                    return;
                }
                else
                {
                    int indiceSiguiente = Parent.Items.IndexOf(Parent.SelectedItem) - 1;
                    Parent.SelectedItem = Parent.Items[indiceSiguiente];
                }
            }
        }

        private void refrescarControl(object sender, EventArgs e)
        {
            this.Refresh();
        }
        public new string Text
        {
            get { return texto; }
            set { texto = value; 
                if(this.Handle!=null)this.Invoke(new EventHandler(refrescarControl)); 
            }
        }
        private string texto;

        public new Image Image
        {
            get { return image; }
            set { image = value;  if(this.Handle!=null)this.Invoke(new EventHandler(refrescarControl)); }
        }
        private Image image;
        
        void Seleccionar(object sender, EventArgs e)
        {
            this.Selected = true;
            Parent.SelectedItem = this;
            this.Focus();
        }

        private ProgressBar barraProgreso;

        public new IconListBox Parent;
        /// <summary>
        /// Progreso realizado, si es -1, no se muestra la barra de progreso
        /// </summary>
        public int Progress
        {
            get
            {
                return valorBarraProgreso;
            }
            set
            {
                valorBarraProgreso = value;
                if (barraProgreso != null && value>=0)
                    barraProgreso.Value = value;
                else
                    if (this.Handle != null) this.Invoke(new EventHandler(refrescarControl));
            }
        }
        private int valorBarraProgreso;

        public int MinProgress
        {
            get { return minProgress; }
            set
            {
                minProgress = value;
                if (barraProgreso != null)
                    barraProgreso.Minimum = minProgress; 
                 if(this.Handle!=null)this.Invoke(new EventHandler(refrescarControl));
            }
        }
        private int minProgress = 0;

        public int MaxProgress
        {
            get { return maxProgress; }
            set {
                maxProgress = value;
                if (barraProgreso != null)
                    barraProgreso.Maximum=maxProgress ;
                 if(this.Handle!=null)this.Invoke(new EventHandler(refrescarControl)); 
            }
        }
        private int maxProgress = 100;

        public void AddLinkLabel(LinkLabel link)
        {
            if (LinkLabels == null)
                LinkLabels = new List<LinkLabel>(2);
            LinkLabels.Add(link);
             if(this.Handle!=null)this.Invoke(new EventHandler(refrescarControl));
        }

        public void RemoveLinkLabel(LinkLabel link)
        {
            LinkLabels.Remove(link);
             if(this.Handle!=null)this.Invoke(new EventHandler(refrescarControl));
        }

        public List<LinkLabel> LinkLabels;

        private Rectangle AreaDibujo()
        {
            Rectangle rectangulo = new Rectangle();

            if (Parent == null)
            {
                Parent = new IconListBox();
            }
            rectangulo.X = Parent.BorderLeft;
            rectangulo.Y = Parent.BorderTop;
            rectangulo.Width = Parent.Width - Parent.BorderLeft - Parent.BorderRight;
            rectangulo.Height = Parent.ItemHeight - Parent.BorderTop - Parent.BorderBottom;
            return rectangulo;
        }


        void Dibujar(object sender, PaintEventArgs e)
        {
            //Fondo
            if (selected)
            {
                e.Graphics.FillRectangle(new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(217, 226, 234), Color.FromArgb(184, 200, 216), LinearGradientMode.Vertical)
                , 0, 0, this.Width, this.Height);
            }
            this.Height = Parent.ItemHeight;
            Rectangle rectangulo = AreaDibujo();

            //Imagen
            if (Image != null)
            {
                e.Graphics.DrawImage(this.Image, rectangulo.X, rectangulo.Y + (rectangulo.Height / 2 - Parent.ImageSize.Height / 2),
                     Parent.ImageSize.Width, Parent.ImageSize.Height);
                rectangulo.X += Parent.ImageSize.Width + 3;
            }

            //Texto
            SizeF tamañoTexto = e.Graphics.MeasureString(this.texto, this.Font);
            if (valorBarraProgreso >= 0)
                e.Graphics.DrawString(this.texto, this.Font, new SolidBrush(this.ForeColor), rectangulo.X,
                    rectangulo.Y + (rectangulo.Height / 2) - (tamañoTexto.Height + Parent.ProgressBarHeight + 3) / 2);
            else
                e.Graphics.DrawString(this.texto, this.Font, new SolidBrush(this.ForeColor), rectangulo.X,
                rectangulo.Y + (rectangulo.Height / 2) - (tamañoTexto.Height / 2));

            //Linea divisoria
            Pen lapiz = new Pen(Color.LightGray, 0.5f);
            for (int contador = 0; contador < this.Width; contador += 2)
            {
                e.Graphics.DrawRectangle(lapiz, contador, this.Height - 1, 0.5f, 0.5f);
            }

            //LinkLabels
            int maximoAncho = 0;
            if (LinkLabels != null)
            {
                foreach (LinkLabel label in LinkLabels)
                {
                    SizeF tamañoLabel = e.Graphics.MeasureString(label.Text, this.Font);
                    if (tamañoLabel.Width > maximoAncho)
                        maximoAncho = (int)tamañoLabel.Width;
                }
                int y = Parent.BorderTop + rectangulo.Height / 2 - (LinkLabels.Count * (int)tamañoTexto.Height) / 2;
                foreach (LinkLabel label in LinkLabels)
                {
                    label.Location = new Point(this.Width - Parent.BorderRight - maximoAncho, y);
                    y += (int)tamañoTexto.Height + 3;
                    label.BringToFront();
                    this.Controls.Add(label);
                }
            }

            //Barra de progreso
            if (valorBarraProgreso == -1)
            {
                if (barraProgreso != null)
                {
                    barraProgreso.Dispose();
                    barraProgreso = null;
                }
            }
            else if (barraProgreso != null)
            {
                barraProgreso.Size = new Size(this.Width - rectangulo.X - Parent.BorderRight - maximoAncho - 15, Parent.ProgressBarHeight);
                barraProgreso.Location = new Point(rectangulo.X + 5, rectangulo.Y + (rectangulo.Height / 2) + (((int)tamañoTexto.Height + Parent.ProgressBarHeight + 3) / 2) - Parent.ProgressBarHeight);
                barraProgreso.Value = valorBarraProgreso;
            }
            else if (barraProgreso == null && valorBarraProgreso >= 0)
            {
                barraProgreso = new ProgressBar();
                barraProgreso.Minimum = minProgress;
                barraProgreso.Maximum = maxProgress;
                barraProgreso.Value = valorBarraProgreso;

                barraProgreso.Size = new Size(this.Width - rectangulo.X - Parent.BorderRight - maximoAncho - 15, Parent.ProgressBarHeight);
                barraProgreso.Location = new Point(rectangulo.X + 5, rectangulo.Y + (rectangulo.Height / 2) + (((int)tamañoTexto.Height + Parent.ProgressBarHeight + 3) / 2) - Parent.ProgressBarHeight);
                barraProgreso.Click += new EventHandler(Seleccionar);
                barraProgreso.Click += new EventHandler(ProgresoClick);
                this.Controls.Add(barraProgreso);
            }
        }

        void ProgresoClick(object sender, EventArgs e)
        {
            this.OnClick(new EventArgs());
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set { selected = value;  if(this.Handle!=null)this.Invoke(new EventHandler(refrescarControl)); }
        }
    }
}
