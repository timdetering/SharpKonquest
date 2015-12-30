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
    partial class Flotas : GlassForm
    {
        public Cliente jugador;
        public Flotas(Cliente jugador)
        {
            InitializeComponent();

            this.jugador = jugador;

            Inicializar(jugador);

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        public void Inicializar(Cliente jugador)
        {
            label1.Text = "Movimientos de flotas del jugador " + jugador.Nombre;

            foreach (Flota flota in jugador.Flotas)
            {
                ListViewItem item = new ListViewItem();

                item.Text=flota.Origen.Name;
                item.SubItems.Add(flota.Destino.Name);
                item.SubItems.Add(flota.Naves.ToString());
                item.SubItems.Add(flota.TecnologiaMilitar.ToString());
                item.SubItems.Add(flota.Distancia.ToString());
                item.SubItems.Add(flota.RondaSalida.ToString());
                item.SubItems.Add(flota.RondaLlegada.ToString());

                listView1.Items.Add(item);
            }
        }
    }
}