using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using SharpKonquest.Clases;

namespace SharpKonquest.Clases
{
    public partial class Mapa : UserControl
    {
        public Mapa()
        {
            InitializeComponent();

            Inicializar(new Random().Next(), new List<Cliente>(), 10);

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.Load += new EventHandler(Mapa_Load);

            this.Font = new Font("Tahoma", 10, FontStyle.Bold);
            this.BackColor = Color.Transparent;
        }

        void Mapa_Load(object sender, EventArgs e)
        {
            Redimensionar();
        }


        public void Redimensionar()
        {
            this.SuspendLayout();
            int max = Math.Max(this.Height, this.Width);
            int tamaño = max - (max % 15);
            if (tamaño != tabla.Height)
            tabla.Size = new Size(tamaño, tamaño);
            this.ResumeLayout();
        }

        public List<Cliente> Jugadores;
        public Dictionary<string, Planeta> Planetas;
        public int Semilla;
        public int Neutrales;
        public int RondaActual;
        public Cliente JugadorActual;
        public bool DibujarFlotasPropias = true;
        public bool DibujarFlotasEnemigas = true;

        private static string[] nombresBasicos = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "#", "@", "€", "$", "&", "!", "?" };
        private TableLayoutPanel tabla;
        public void Inicializar(int semilla, List<Cliente> jugadores, int colonias)
        {
            RondaActual = 0;
            List<string> listaNombres = new List<string>(); ;

            foreach (string nombre in nombresBasicos)
            {
                listaNombres.Add(nombre);
            }
            foreach (string nombre in nombresBasicos)
            {
                listaNombres.Add(nombre + "2");
            }
            foreach (string nombre in nombresBasicos)
            {
                listaNombres.Add(nombre + "2");
            }
            foreach (string nombre in nombresBasicos)
            {
                listaNombres.Add(nombre + "3");
            }
         
            this.Jugadores = jugadores;
            Semilla = semilla;
            Neutrales = colonias;
            this.Planetas = new Dictionary<string, Planeta>();

            tabla = new TableLayoutPanel();
            this.Controls.Clear();
            tabla.BackColor = Color.Black;
            tabla.CellPaint += new TableLayoutCellPaintEventHandler(DibujarCelda);
            tabla.Paint += new PaintEventHandler(DibujarTabla);
            tabla.Location = new Point(0, 0);

            float ancho = 100f / 15f;
            for (int contador = 0; contador < 16; contador++)
            {
                tabla.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, ancho));
                tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, ancho));
            }

            int jugadoresPuestos = 0;
            int coloniasPuestos = 0;
            System.Random rand = new Random(semilla);
            for (int contador = 0; contador < jugadores.Count + colonias; contador++)
            {
                int tecno=  rand.Next(35, 95);
                int prod = 100 - tecno + rand.Next(0, 10);
                Planeta planeta = null;
                if (jugadoresPuestos < jugadores.Count)
                {
                    planeta = new Planeta(listaNombres[contador % listaNombres.Count],
                        jugadores[jugadoresPuestos], tecno,prod,rand.Next());
                    jugadoresPuestos++;
                }
                else if (coloniasPuestos < colonias)
                {
                    planeta = new Planeta(listaNombres[contador % listaNombres.Count], null, tecno,prod,rand.Next());
                    coloniasPuestos++;
                }
                if (planeta != null)
                {
                    planeta.MouseHover += new EventHandler(planeta_MouseHover);
                    planeta.MouseLeave += new EventHandler(planeta_MouseLeave);
                    planeta.Click += new EventHandler(planeta_Click);

                DeNuevo:
                    int columna = rand.Next(0, 15);
                    int fila = rand.Next(0, 15);
                    if (Planetas.ContainsKey(columna + ":" + fila))
                        goto DeNuevo;
                    planeta.Localizacion = columna + ":" + fila;
                    Planetas.Add(columna + ":" + fila, planeta);
                    tabla.Controls.Add(planeta, columna, fila);
                }
            }

            tabla.Controls.Add(new Control(), 14, 14);
            this.Controls.Add(tabla);
            Redimensionar();
        }

        void DibujarTabla(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            foreach (Cliente jugador in Jugadores)
            {
                if(DibujarFlotasPropias==false && jugador==JugadorActual)
                    continue;

                if(DibujarFlotasEnemigas==false && jugador!=JugadorActual)
                    continue;

                foreach (Flota flota in jugador.Flotas)
                {
                    try
                    {
                        Color inicioDegradado = jugador.Color;
                        Color finalDegradado = ControlPaint.LightLight(jugador.Color);

                        PointF caminoMasCorto = CalcularCaminoMasCorto(flota.Origen, flota.Destino);

                        float y0 = flota.Origen.Location.Y + flota.Origen.Height / 2;
                        float x0 = flota.Origen.Location.X + flota.Origen.Width / 2;
                        float y = caminoMasCorto.Y;
                        float x = caminoMasCorto.X;

                        Pen lapiz = new Pen(jugador.Color, 4f);
                        lapiz.EndCap = LineCap.ArrowAnchor;
                        lapiz.StartCap = LineCap.Round;
                        lapiz.DashStyle = DashStyle.Dot;
                        lapiz.Brush = new LinearGradientBrush(new PointF(x0, y0), new PointF(x, y), inicioDegradado, finalDegradado);

                        e.Graphics.DrawLine(lapiz, new PointF(x0, y0), new PointF(x, y));

                        //Dibujar la parte recorrida
                        float distancia = Pitagoras(x - x0, y - y0);
                        float recorridoCompletado = (RondaActual - flota.RondaSalida) / (float)(flota.RondaLlegada - flota.RondaSalida);

                        lapiz = new Pen(jugador.Color, 6f);
                        lapiz.EndCap = LineCap.ArrowAnchor;
                        lapiz.StartCap = LineCap.Round;

                        float distanciaCompletada = distancia * recorridoCompletado;

                        if (distanciaCompletada > 0)
                        {

                            float seno = (float)Math.Abs((y - y0) / distancia);

                            float xActual = x0;
                            if (x0 > x)
                                xActual -= distanciaCompletada * (float)Math.Cos(Math.Asin(seno));
                            else
                                xActual += distanciaCompletada * (float)Math.Cos(Math.Asin(seno));

                            float yActual = y0;
                            if (y0 > y)
                                yActual -= distanciaCompletada * seno;
                            else
                                yActual += distanciaCompletada * seno;

                            lapiz.Brush = new LinearGradientBrush(new PointF(x0, y0), new PointF(xActual, yActual), inicioDegradado, finalDegradado);

                            e.Graphics.DrawLine(lapiz, new PointF(x0, y0), new PointF(xActual, yActual));
                        }
                    }
                    catch { }
                }
            }
        }

        private float Pitagoras(float x, float y)
        {
            return (float)Math.Sqrt(x*x + y*y);
        }

        private PointF CalcularCaminoMasCorto(Planeta origen, Planeta destino)
        {
            float y0 = origen.Location.Y + origen.Height / 2;
            float x0 = origen.Location.X + origen.Width / 2;

            Dictionary<float, PointF> distancias = new Dictionary<float, PointF>();

            PointF puntoDestino = new PointF(destino.Location.X, destino.Location.Y + destino.Height / 2);
            float distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0)-10;
            distancias.Add(distancia, puntoDestino);

            puntoDestino = new PointF(destino.Location.X + destino.Width / 2, destino.Location.Y);
            distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0)-10;
            if (distancias.ContainsKey(distancia) == false)
                distancias.Add(distancia, puntoDestino);
            
            puntoDestino = new PointF(destino.Location.X + destino.Width, destino.Location.Y + destino.Height / 2);
            distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0) - 10;
            if (distancias.ContainsKey(distancia) == false)
                distancias.Add(distancia, puntoDestino);

            puntoDestino = new PointF(destino.Location.X + destino.Width / 2, destino.Location.Y + destino.Height);
            distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0)-10;
            if (distancias.ContainsKey(distancia) == false)
                distancias.Add(distancia, puntoDestino);

            #region Esquinas
            puntoDestino = new PointF(destino.Location.X, destino.Location.Y);//Esquina
            distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0);
            if (distancias.ContainsKey(distancia) == false)
                distancias.Add(distancia, puntoDestino);

            puntoDestino = new PointF(destino.Location.X + destino.Width, destino.Location.Y);//Esquina
            distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0);
            if (distancias.ContainsKey(distancia) == false)
                distancias.Add(distancia, puntoDestino);

            puntoDestino = new PointF(destino.Location.X + destino.Width, destino.Location.Y + destino.Height);//Esquina
            distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0);
            if (distancias.ContainsKey(distancia) == false)
                distancias.Add(distancia, puntoDestino);

            puntoDestino = new PointF(destino.Location.X, destino.Location.Y + destino.Height);//Esquina
            distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0);
            if (distancias.ContainsKey(distancia) == false)
                distancias.Add(distancia, puntoDestino);
            #endregion

            System.Collections.Generic.KeyValuePair<float, PointF>? puntoMinimo = null;
            foreach (System.Collections.Generic.KeyValuePair<float, PointF> elemento in distancias)
            {
                if (puntoMinimo.HasValue == false)
                    puntoMinimo = elemento;
                else
                    puntoMinimo = elemento.Key < puntoMinimo.Value.Key ? elemento : puntoMinimo;
            }

            return puntoMinimo.Value.Value;
        }

        void DibujarCelda(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (Planetas.ContainsKey(e.Column + ":" + e.Row))
            {
                Planeta planeta = Planetas[e.Column + ":" + e.Row];
                Pen lapiz;
                if (planeta.Dueño == null)
                    lapiz = new Pen(Color.White, 1f);
                else
                    lapiz = new Pen( planeta.Dueño.Color, 2f);

                e.Graphics.DrawRectangle(lapiz, e.CellBounds.X,
                     e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1);
            }
            else
                e.Graphics.DrawRectangle(new Pen(Color.DarkGreen, 1f), e.CellBounds);
        }

        public event DelegadoPlaneta MouseSobrePlaneta;
        public event DelegadoPlaneta MouseFueraPlaneta;
        public event DelegadoPlaneta MouseClicPlaneta;

        public delegate void DelegadoPlaneta(Mapa mapa, Planeta planeta);

        void planeta_MouseLeave(object sender, EventArgs e)
        {
            if (MouseFueraPlaneta != null)
                MouseFueraPlaneta(this, (Planeta)sender);
        }
        void planeta_Click(object sender, EventArgs e)
        {
            if (MouseClicPlaneta != null)
                MouseClicPlaneta(this, (Planeta)sender);
        }
        void planeta_MouseHover(object sender, EventArgs e)
        {
            if (MouseSobrePlaneta != null)
                MouseSobrePlaneta(this, (Planeta)sender);
        }

        public Planeta ObtenerPlaneta(string nombre)
        {
            foreach(System.Collections.Generic.KeyValuePair<string,Planeta> planeta in Planetas)
            {
                if (planeta.Value.Name == nombre)
                    return planeta.Value;
            }
            return null;
        }

        public Cliente BuscarJugador(string nombre)
        {
            foreach (Cliente jugador in Jugadores)
            {
                if (jugador.Nombre == nombre)
                    return jugador;
            }
            return null;
        }
    }
}
