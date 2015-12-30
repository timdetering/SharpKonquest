using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SharpKonquest.Clases;

namespace SharpKonquest.Ventanas
{
    partial class SimularBatalla : Form
    {
        public SimularBatalla(Planeta planeta, Planeta planeta2, Cliente jugador)
        {
            InitializeComponent();

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);


            label1.Text = "Simulacion de batalla para el jugador " + jugador.Nombre + " desde el planeta " + planeta.Name;

            numericUpDownEx1.Value = planeta.Naves;
            numericUpDownEx4.Value = planeta.TecnologiaMilitar;
            if (planeta2.Dueño != null)
            {
                numericUpDownEx2.Value = planeta2.Naves;
                numericUpDownEx3.Value = planeta2.TecnologiaMilitar;
            }

            numericUpDownEx1.ValueChanged += ResimularBatalla;
            numericUpDownEx2.ValueChanged += ResimularBatalla;
            numericUpDownEx3.ValueChanged += ResimularBatalla;
            numericUpDownEx4.ValueChanged += ResimularBatalla;
            ResimularBatalla(null, null);
        }

        private void ResimularBatalla(object sender, EventArgs e)
        {
            int victorias = 0;
            int empates = 0;
            int derrotas = 0;
            int restantesAtacante = 0;
            int restantesDefensor = 0;
            for (int contador = 0; contador < 100; contador++)
            {
                Batalla resultadoBatalla = Batalla.SimularBatalla(numericUpDownEx1.Value,
           numericUpDownEx4.Value, numericUpDownEx2.Value, numericUpDownEx3.Value);

                if (resultadoBatalla.Resultado == Batalla.ResultadoBatalla.GanaAtacante)
                    victorias++;
                else if (resultadoBatalla.Resultado == Batalla.ResultadoBatalla.GanaDefensor)
                    derrotas++;
                else//Empate
                    empates++;

                restantesAtacante += resultadoBatalla.RestanteAtacante;
                restantesDefensor += resultadoBatalla.RestanteDefensor;
            }

            if (victorias > derrotas && victorias > empates)
            {
                label6.Text = "Gana el atacante\r\nNaves restantes: " + Math.Round(restantesAtacante / 100d);
            }
            else if (derrotas > victorias && derrotas > empates)
            {
                label6.Text = "Gana el defensor\r\nNaves restantes: " + Math.Round(restantesDefensor / 100d);
            }
            else//Empate
            {
                label6.Text = "Empate\r\nNaves restantes del atacante: " + Math.Round(restantesAtacante / 100d) +
                    "\r\nNaves restantes del defensor: " + Math.Round(restantesDefensor / 100d);
            }

            label6.Text += "\r\n\r\n" + victorias + "% de victorias." +
   "\r\n" + derrotas + "% de derrotas." +
   "\r\n" + empates + "% de empates.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}