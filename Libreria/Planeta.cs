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

        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        this.BackColor = Color.Transparent;

            this.Name = nombre;          

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
