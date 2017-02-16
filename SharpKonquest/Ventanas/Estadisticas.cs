namespace SharpKonquest.Ventanas
{
    using SharpKonquest;
    using SharpKonquest.Clases;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class Estadisticas : GlassForm
    {
        private ColumnHeader columna;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private Label label1;
        private ListView listView1;
        private Mapa mapa;

        public Estadisticas(Mapa mapa)
        {
            this.InitializeComponent();
            base.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.mapa = mapa;
            this.ActualizarDatos(mapa);
        }

        private void ActualizarDatos(Mapa mapa)
        {
            this.listView1.Items.Clear();
            foreach (Cliente jugador in mapa.Jugadores)
            {
                List<Planeta> planetas = jugador.ObtenerPlanetas(mapa);
                ListViewItem item = new ListViewItem();
                item.Text = jugador.Nombre;
                item.ForeColor = jugador.Color;
                item.SubItems.Add(planetas.Count.ToString());
                int numeroNaves = 0;
                int tecnosTotal = 0;
                int produccionTotal = 0;
                foreach (Planeta planeta in planetas)
                {
                    numeroNaves += planeta.Naves;
                    tecnosTotal += planeta.TecnologiaMilitar;
                    produccionTotal += planeta.Produccion;
                }
                int navesVuelo = 0;
                foreach (Flota flota in jugador.Flotas)
                {
                    navesVuelo += flota.Naves;
                }

                item.SubItems.Add((numeroNaves + navesVuelo).ToString());
                item.SubItems.Add(jugador.Flotas.Count.ToString());
                item.SubItems.Add(navesVuelo.ToString());
                int puntuacion = ((produccionTotal * tecnosTotal) * (numeroNaves + navesVuelo)) / 0x3e8;
                item.SubItems.Add(puntuacion.ToString());
                this.listView1.Items.Add(item);
            }
            new Thread(delegate(object param)
            {
                int rondaActual = mapa.RondaActual;
                while (true)
                {
                    try
                    {
                        if (this.Visible == false)
                            return;
                        if (rondaActual != mapa.RondaActual)
                        {
                            this.Invoke(new delegadoActualizarDatos(this.ActualizarDatos), new object[] {this.mapa});
                            return;
                        }
                        Thread.Sleep(150);
                    }
                    catch
                    {
                    }
                }
            }).Start();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.label1 = new Label();
            this.columnHeader8 = new ColumnHeader();
            this.columnHeader9 = new ColumnHeader();
            this.columnHeader10 = new ColumnHeader();
            this.columnHeader11 = new ColumnHeader();
            this.columnHeader12 = new ColumnHeader();
            this.columna = new ColumnHeader();
            base.SuspendLayout();
            this.listView1.AllowColumnReorder = true;
            this.listView1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.listView1.BackColor = SystemColors.Window;
            this.listView1.Columns.AddRange(new ColumnHeader[]
            {
                this.columnHeader8, this.columnHeader9, this.columnHeader10, this.columna, this.columnHeader11,
                this.columnHeader12
            });
            this.listView1.Location = new Point(12, 30);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x284, 0xe9);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x6f, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Estad\x00edsticas del juego";
            this.columnHeader8.Text = "Jugador";
            this.columnHeader9.Text = "N\x00famero de planetas";
            this.columnHeader9.Width = 0x7d;
            this.columnHeader10.Text = "N\x00famero de naves";
            this.columnHeader10.Width = 0x7d;
            this.columnHeader11.DisplayIndex = 4;
            this.columnHeader11.Text = "Naves en vuelo";
            this.columnHeader11.Width = 0x7d;
            this.columnHeader12.DisplayIndex = 5;
            this.columnHeader12.Text = "Puntuaci\x00f3n";
            this.columnHeader12.Width = 0x7d;
            this.columna.DisplayIndex = 3;
            this.columna.Text = "Flotas";
            this.columna.Width = 80;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.ClientSize = new Size(0x29c, 0x113);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.Name = "Estadisticas";
            this.Text = "Estad\x00edsticas";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private delegate void delegadoActualizarDatos(Mapa mapa);
    }
}
