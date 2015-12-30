using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SharpKonquest.Ventanas
{
    public partial class AcercaDe : GlassForm
    {
        public AcercaDe()
        {
            InitializeComponent();
            label2.Text = "Versión " + System.InformacionPrograma.VersionActual;
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            
            this.pictureBox1.Image = Programa.ObtenerImagenIncrustada("logo");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto: fcojaviermarinros89@gmail.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(((Control)sender).Tag.ToString());
            }
            catch { }
        }
    }
}
