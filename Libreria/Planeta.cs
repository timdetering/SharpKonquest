using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SharpKonquest.Clases;

namespace SharpKonquest
{
   public class Planeta:Control
    {
        public Cliente Dueño;

        public int Naves;
        public int Produccion;
        public int TecnologiaMilitar;
        public Image Imagen;
        public string Localizacion;

        public override string ToString()
        {
            return string.Format("Planeta '{0}', {1} naves, {3} de produccion y {2} de tecnología militar", this.Name, this.Naves, this.TecnologiaMilitar,this.Produccion);
        }

        public Planeta(string nombre,Cliente jugador,int tecno,int produccion,int imagen)
        {

            string[] imagenes = System.IO.Directory.GetFiles(Application.StartupPath, "*.jpg", System.IO.SearchOption.AllDirectories);

            if (imagenes.Length >0)
            this.Imagen = Image.FromFile(imagenes[imagen % imagenes.Length]);


            this.Paint += new PaintEventHandler(Dibujar);
            this.Name = nombre;           

            this.Dock = DockStyle.Fill;

            Dueño = jugador;

            if (jugador != null)
            {
                Naves = 50;
                TecnologiaMilitar = 50;
                Produccion = 50;

            }
            else
            {
                TecnologiaMilitar = tecno;
                Produccion = produccion;
                Naves = Produccion;
            }
        }

        void Dibujar(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Imagen, this.DisplayRectangle);

            StringFormat formatoTexto = new StringFormat();    
            formatoTexto.LineAlignment = StringAlignment.Far;
            e.Graphics.DrawString(this.Name, this.Font, new SolidBrush(Color.Red), this.DisplayRectangle, formatoTexto);
        }

        public int Columna
        {
            get
            {
                return int.Parse(Localizacion.Split(':')[0]);
            }
        }

        public int Fila
        {
            get
            {
                return int.Parse(Localizacion.Split(':')[1]);
            }
        }
    }
}
