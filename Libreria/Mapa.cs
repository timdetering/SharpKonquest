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
		private static string[] nombresBasicos = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "#", "@", "€", "$", "&", "!", "?" };

		static Mapa()
		{
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

			nombresBasicos = listaNombres.ToArray();
		}

		public Mapa()
		{
			ColorFondoCeldas = Color.Black;

			InitializeComponent();

			Inicializar(new Aleatorios().Next(), new List<Cliente>(), 10);

			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.Paint += new PaintEventHandler(DibujarTabla);

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
			int tamañoCelda = TamañoCelda();

			foreach (Planeta planeta in this.Planetas)
			{
				try
				{
					string[] partes = planeta.Localizacion.Split(':');

					planeta.Size = new Size(tamañoCelda, tamañoCelda);
					planeta.Location = new Point(int.Parse(partes[0]) * tamañoCelda, int.Parse(partes[1]) * tamañoCelda);
				}
				catch { }
			}

			this.Size = new Size(tamañoCelda * 15, tamañoCelda * 15);
		}

		private int TamañoCelda()
		{
			int max = Math.Min(this.Height, this.Width);
			int tamañoCelda = (int)Math.Round((max - (max % 15)) / 15f);
			return tamañoCelda;
		}

		public List<Cliente> Jugadores;
		public List<Planeta> Planetas;
		public int Semilla;
		public int Neutrales;
		public int RondaActual;
		public Cliente JugadorActual;
		public bool DibujarFlotasPropias = true;
		public bool DibujarFlotasEnemigas = true;
		public bool ModoGrafico = true;

		public void Inicializar(int semilla, List<Cliente> jugadores, int colonias)
		{
			RondaActual = 0;

			this.Jugadores = jugadores;
			Semilla = semilla;
			Neutrales = colonias;

			this.Planetas = new List<Planeta>();
			this.Controls.Clear();

			int jugadoresPuestos = 0;
			int coloniasPuestos = 0;
			Aleatorios rand = new Aleatorios(semilla);
			List<string> Coordenadas = new List<string>();
			for (int contador = 0; contador < jugadores.Count + colonias; contador++)
			{
				int tecno = rand.Next(35, 95);
				int prod = 100 - tecno + rand.Next(0, 10);
				Planeta planeta = null;
				if (jugadoresPuestos < jugadores.Count)
				{
					planeta = new Planeta(nombresBasicos[contador % nombresBasicos.Length],
					                      jugadores[jugadoresPuestos], tecno, prod, rand.Next());
					jugadoresPuestos++;
				}
				else if (coloniasPuestos < colonias)
				{
					planeta = new Planeta(nombresBasicos[contador % nombresBasicos.Length], null, tecno, prod, rand.Next());
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
					string coordenadas = columna + ":" + fila;
					if (Coordenadas.IndexOf(coordenadas) != -1)
						goto DeNuevo;
					planeta.Localizacion = columna + ":" + fila;
					Planetas.Add(planeta);
					Coordenadas.Add(coordenadas);
				}
			}

			if (ModoGrafico)
			{
                Redimensionar();

                foreach (Planeta pl in Planetas)
                {
                    this.Controls.Add(pl);
                }

				Redimensionar();
			}
		}

		public Color ColorFondoCeldas
		{
			get { return Color.Black; }
			set { brochaCeldas = new SolidBrush(value); this.Refresh(); }
		}
		public Brush brochaCeldas;

		public Pen PincelBordesCeldas = new Pen(Color.DarkGreen, 1f);

		void DibujarTabla(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            
			//Flotas
			#region DibujarFlotas
			foreach (Cliente jugador in Jugadores)
			{
				if (DibujarFlotasPropias == false && jugador == JugadorActual)
					continue;

				if (DibujarFlotasEnemigas == false && jugador != JugadorActual)
					continue;

				foreach (Flota flota in jugador.Flotas)
				{
					DibujarFlota(e, jugador, flota);
				}
			}
			#endregion

            //Opcional, dibuja los planetas encima o debajo de las lineas de flota
			if(System.InformacionPrograma.Mono==false)
			{
				foreach (Planeta planeta in Planetas)
				{
					if (planeta.Dueño != null)
					{
						bool llegaFlotaPlaneta=false;
						bool salenFlotasPlaneta=false;
						foreach (Cliente jugador in Jugadores)
						{
							foreach (Flota flota in jugador.Flotas)
							{
								if (flota.Destino == planeta)
									llegaFlotaPlaneta = true;
								if (flota.Origen == planeta)
									salenFlotasPlaneta = true;
							}
						}

						if (llegaFlotaPlaneta == false)
							DibujarPlaneta(e, planeta);						
						else if (llegaFlotaPlaneta && salenFlotasPlaneta)//Dibujar solo flotas que llegan
						{
							DibujarPlaneta(e, planeta);
							
							foreach (Cliente jugador in Jugadores)
							{
								foreach (Flota flota in jugador.Flotas)
								{
									if (flota.Destino == planeta)
										DibujarFlota(e, jugador, flota);
								}
							}
						}
					}
				}
			}
		}

		private Image imagenFondo;

		public Image ImagenFondo
		{
			get { return imagenFondo; }
			set { imagenFondo = value; this.Refresh(); }
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			// base.OnPaintBackground(e);
			int tamañoCelda = TamañoCelda();

		//	e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		//	e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

			//Fondo
			if (imagenFondo != null)
				e.Graphics.DrawImage(ImagenFondo, 0, 0, tamañoCelda * 15, tamañoCelda * 15);
			else
				e.Graphics.FillRectangle(brochaCeldas, 0, 0, tamañoCelda * 15, tamañoCelda * 15);

			//Celdas
			for (int fila = 0; fila < 15; fila++)
			{
				int y = fila * tamañoCelda;
				e.Graphics.DrawLine(PincelBordesCeldas, 0, y, (tamañoCelda * 15) - 1, y);
			}
			for (int columna = 0; columna < 15; columna++)
			{
				int x = columna * tamañoCelda;
				e.Graphics.DrawLine(PincelBordesCeldas, x, 0, x, (tamañoCelda * 15) - 1);
			}

			//Dibujar planetas
			foreach (Planeta planeta in Planetas)
			{
				DibujarPlaneta(e, planeta);
			}
		}

		private void DibujarFlota(PaintEventArgs e, Cliente jugador, Flota flota)
		{
			try
			{
				Color inicioDegradado = jugador.Color;
				Color finalDegradado = jugador.Color;

				PointF caminoMasCorto = CalcularCaminoMasCorto(flota.Origen, flota.Destino);

				float y0 = flota.Origen.Location.Y + flota.Origen.Height / 2;
				float x0 = flota.Origen.Location.X + flota.Origen.Width / 2;
				float y = caminoMasCorto.Y;
				float x = caminoMasCorto.X;
				
				if (Cliente.CalcularDistancia(flota.Origen, flota.Destino) <= 1)
				{
					y = flota.Destino.Location.Y + flota.Destino.Height / 2;
					x = flota.Destino.Location.X + flota.Destino.Width / 2;
				}

				Pen lapiz = new Pen(jugador.Color, 4f);
				lapiz.EndCap = LineCap.ArrowAnchor;
				lapiz.StartCap = LineCap.Round;
				lapiz.DashStyle = DashStyle.Dot;

				if(System.InformacionPrograma.Mono==false)
				{
					inicioDegradado = jugador.Color;
					finalDegradado = ControlPaint.LightLight(jugador.Color);

					lapiz.Brush = new LinearGradientBrush(new PointF(x0, y0), new PointF(x, y), inicioDegradado, finalDegradado);
				}

				e.Graphics.DrawLine(lapiz, new PointF(x0, y0), new PointF(x, y));

				//Dibujar la parte recorrida
				float distancia = Pitagoras(x - x0, y - y0);
				float recorridoCompletado = (RondaActual - flota.RondaSalida) / (float)(flota.RondaLlegada - flota.RondaSalida);

				lapiz = new Pen(jugador.Color, 6f);
				lapiz.EndCap = LineCap.ArrowAnchor;
				lapiz.StartCap = LineCap.Round;

				float distanciaCompletada = distancia * recorridoCompletado;

				if (recorridoCompletado > 0 && recorridoCompletado <= 1)
				{
					float seno = (float)Math.Abs((y - y0) / distancia);
                    float coseno=(float)Math.Cos(Math.Asin(seno));

					float xActual = x0;
					if (x0 > x)
                        xActual -= distanciaCompletada * coseno;
					else
                        xActual += distanciaCompletada * coseno;

					float yActual = y0;
					if (y0 > y)
						yActual -= distanciaCompletada * seno;
					else
						yActual += distanciaCompletada * seno;

                    if (System.InformacionPrograma.Mono)
					{
						lapiz.Brush = new SolidBrush(ControlPaint.LightLight(jugador.Color));
					}
					else
					{
						lapiz.Brush = new LinearGradientBrush(new PointF(x0, y0), new PointF(xActual, yActual), inicioDegradado, finalDegradado);
					}

					e.Graphics.DrawLine(lapiz, new PointF(x0, y0), new PointF(xActual, yActual));

					if (DibujarDestinoFlotas)
					{
						e.Graphics.DrawString(flota.Destino.Name, this.Font, new SolidBrush(Color.Red),
						  new RectangleF(xActual - 5, yActual - 5, 10, 10), StringFormat.GenericDefault);
					}
				}
			}
			catch { }
		}

		public bool DibujarDestinoFlotas = false;

		private static void DibujarPlaneta(PaintEventArgs e, Planeta planeta)
		{
			Rectangle area=new Rectangle(planeta.Location.X, planeta.Location.Y,planeta.Width,planeta.Height);
			
            if (planeta.Imagen != null)
                e.Graphics.DrawImage(planeta.Imagen, area);

			StringFormat formatoTexto = new StringFormat();
			formatoTexto.LineAlignment = StringAlignment.Far;
            e.Graphics.DrawString(planeta.Name, planeta.Font, new SolidBrush(Color.Red), area, formatoTexto);


			Pen lapiz;
			if (planeta.Dueño == null)
				lapiz = new Pen(Color.White, 1f);
			else
				lapiz = new Pen(planeta.Dueño.Color, 2f);

            e.Graphics.DrawRectangle(lapiz, area.X, area.Y,area.Width - 1, area.Height - 1);
		}

		private float Pitagoras(float x, float y)
		{
			return (float)Math.Sqrt(x * x + y * y);
		}

		private PointF CalcularCaminoMasCorto(Planeta origen, Planeta destino)
		{
			float y0 = origen.Location.Y + origen.Height / 2;
			float x0 = origen.Location.X + origen.Width / 2;

			Dictionary<float, PointF> distancias = new Dictionary<float, PointF>();

			PointF puntoDestino = new PointF(destino.Location.X, destino.Location.Y + destino.Height / 2);
			float distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0) - 10;
			distancias.Add(distancia, puntoDestino);

			puntoDestino = new PointF(destino.Location.X + destino.Width / 2, destino.Location.Y);
			distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0) - 10;
			if (distancias.ContainsKey(distancia) == false)
				distancias.Add(distancia, puntoDestino);

			puntoDestino = new PointF(destino.Location.X + destino.Width, destino.Location.Y + destino.Height / 2);
			distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0) - 10;
			if (distancias.ContainsKey(distancia) == false)
				distancias.Add(distancia, puntoDestino);

			puntoDestino = new PointF(destino.Location.X + destino.Width / 2, destino.Location.Y + destino.Height);
			distancia = Pitagoras(puntoDestino.X - x0, puntoDestino.Y - y0) - 10;
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
			foreach (Planeta planeta in Planetas)
			{
				if (planeta.Name == nombre)
					return planeta;
			}
			return null;
		}

		public Cliente BuscarJugador(string nombre)
		{
			foreach (Cliente jugador in Jugadores)
			{
				if (string.Compare(jugador.Nombre, nombre, true) == 0)
					return jugador;
			}
			return null;
		}

		public void CargarDatos(string[] parametros)
		{
			foreach (Cliente jugador in Jugadores)
			{
				jugador.Flotas.Clear();
			}

			for (int contador = 0; contador < parametros.Length; contador++)
			{
				if (parametros[contador] == "P")//Datos de planeta
				{
					try
					{
						Planeta p = this.ObtenerPlaneta(parametros[contador + 1]);
						p.Naves = int.Parse(parametros[contador + 2]);
						if (parametros[contador + 3] == string.Empty)
							p.Dueño = null;
						else
							p.Dueño = this.BuscarJugador(parametros[contador + 3]);
					}
					catch { }
					contador += 3;
				}
				else if (parametros[contador] == "J")//Datos de jugador
				{
					try
					{
						Cliente jugador = this.BuscarJugador(parametros[contador + 1]);
						Flota flota = new Flota();
						flota.Naves = int.Parse(parametros[contador + 2]);
						flota.TecnologiaMilitar = int.Parse(parametros[contador + 3]);
						flota.RondaSalida = int.Parse(parametros[contador + 4]);
						flota.RondaLlegada = int.Parse(parametros[contador + 5]);
						flota.Origen = this.ObtenerPlaneta(parametros[contador + 6]);
						flota.Destino = this.ObtenerPlaneta(parametros[contador + 7]);
						flota.Distancia = Cliente.CalcularDistancia(flota.Origen, flota.Destino);

						jugador.Flotas.Add(flota);
					}
					catch { }
					contador += 7;
				}
			}
		}

		public string GuardarDatos()
		{
			string mensaje = string.Empty;
			foreach (Planeta planeta in this.Planetas)
			{
				mensaje += string.Format("(Tipo 'P', Planeta: '{0}', naves: '{1}', dueño: '{2}'); ", planeta.Name, planeta.Naves, planeta.Dueño == null ? string.Empty : planeta.Dueño.Nombre);
			}
			foreach (Cliente jugador in this.Jugadores)
			{
				foreach (Flota flota in jugador.Flotas)
				{
					mensaje += string.Format("(Tipo 'J', Jugador: '{0}'; Naves: '{1}'; Tecnologia: '{2}'; Salida: '{3}'; Llegada: '{4}'; Origen: '{5}'; Destino: '{6}'), ",
					                         jugador.Nombre, flota.Naves, flota.TecnologiaMilitar, flota.RondaSalida, flota.RondaLlegada, flota.Origen.Name, flota.Destino.Name);
				}
			}
			if (mensaje.EndsWith(", "))
				mensaje = mensaje.Substring(0, mensaje.Length - 2);
			return mensaje;
		}
	}
}
