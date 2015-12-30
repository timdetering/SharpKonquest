using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SharpKonquest.Ventanas;
using SharpKonquest.Clases;
using System.Threading;
using System.Drawing.Drawing2D;
using System.TCP;

namespace SharpKonquest
{
    partial class Principal : GlassForm
    {
        public Principal()
        {
            InitializeComponent();

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            Actualizador.Actualizacion.ActualizacionDisponible += new Actualizador.Actualizacion.DelegadoActualizacionDisponible(Actualizacion_ActualizacionDisponible);
            Actualizador.Actualizacion.ComprobarActualizacion(System.InformacionPrograma.VersionActual, false, System.InformacionPrograma.UrlActualizacion);


            if (System.InformacionPrograma.Mono == false)
            {
                interfazToolStripMenuItem.Visible = true;
                office2007ToolStripMenuItem.PerformClick();
            }
            else
            {
                interfazToolStripMenuItem.Visible = false;
                splitContainer1.BackColor = Color.FromArgb(159, 190, 230);
            }


            //Imagenes
            this.iniciarNuevaPartidaToolStripMenuItem.Image = Programa.ObtenerImagenIncrustada("package_games_board");
            this.toolStripButton2.Image = Programa.ObtenerImagenIncrustada("package_games_board");
            this.toolStripButton6.Image = Programa.ObtenerImagenIncrustada("Clanbomber");
            this.toolStripButton5.Image = Programa.ObtenerImagenIncrustada("Search");
            this.toolStripButton4.Image = Programa.ObtenerImagenIncrustada("Administrative_Tool");
            this.toolStripButton7.Image = Programa.ObtenerImagenIncrustada("softwareD");
            this.toolStripButton3.Image = Programa.ObtenerImagenIncrustada("calculadora");
            this.toolStripButton1.Image = Programa.ObtenerImagenIncrustada("rendimiento");

            if(System.InformacionPrograma.Mono)
            	this.Text+=" Mono Version";
        }

        private void TamañoCambiado(object sender, EventArgs e)
        {
            try
            {
                if (splitContainer1.SplitterDistance != splitContainer1.Height)
                    splitContainer1.SplitterDistance = splitContainer1.Height;
                mapa.Redimensionar();
            }
            catch { }
        }

        /// <summary>
        /// Clientes que juegan en la maquina local
        /// </summary>
        List<Cliente> Clientes;
        private System.Diagnostics.Process procesoServidor;
        private void NuevaPartida(object sender, EventArgs e)
        {
            NuevaPartida partida = new NuevaPartida(this);

            if (partida.ShowDialog() == DialogResult.OK)
            {
                listaJugadores.Items.Clear();
                procesoServidor = partida.procesoServidor;

                //Nueva partida
                Clientes = partida.clientes;
                foreach (Cliente cliente in Clientes)
                {
                    cliente.ClienteTcp.DatosRecibidos += ComandoRecibido;

                    if (cliente.AdministradorServidor)
                    {
                        limitarTiempoDeTurnoToolStripMenuItem.Visible = true;
                        listaJugadores.Items.Add("Todos");

                        toolStripMenuItem5.Visible = true;
                        guardarpartida.Visible = true;
                        cargarPartidaToolStripMenuItem.Visible = true;
                    }
                }

                //Inicializar mapa
                mapa.Jugadores = new List<Cliente>();
                foreach (NuevaPartida.ItemLista item in partida.listaJugadores.Items)
                {
                    bool añadido = false;
                    foreach (Cliente cliente in Clientes)
                    {
                        if (string.Compare(item.Tag.Nombre, cliente.Nombre, true) == 0)
                        {
                            mapa.Jugadores.Add(cliente);
                            listaJugadores.Items.Add(cliente);
                            añadido = true;
                            break;
                        }
                    }

                    if (añadido == false)
                    {
                        mapa.Jugadores.Add((Cliente)item.Tag);
                        listaJugadores.Items.Add((Cliente)item.Tag);
                    }
                }
                mapa.Inicializar((int)partida.semillaMapa.Value, mapa.Jugadores, (int)partida.neutrales.Value);


                groupBox1.Visible = true;
                listaJugadores.SelectedIndex = 0;
            }
        }

        Cliente clienteActual;
        void ComandoRecibido(ushort comando, string[] parametros, string cadena, ClienteTCP clienteTcp)
        {
            if (cerrando)
                return;
            switch (comando)
            {
                case 2://Ping
                    clienteTcp.EnviarComando(3, "PING OK");
                    break;
                case 51://Inicio de ronda
                    numeroRondaActual = int.Parse(parametros[0]);
                    mapa.RondaActual = numeroRondaActual;
                    rondaActual.Text = "Ronda " + numeroRondaActual;
                    Informacion("Inicio de la ronda " + numeroRondaActual);
                    mapa.Refresh();
                    break;
                case 52://Inicio de turno de jugador
                    Informacion("Turno del jugador " + parametros[0]);
                    rondaActual.Text = "Ronda " + numeroRondaActual;

                    lblTurno.ForeColor = Color.FromArgb(int.Parse(parametros[1]));
                    peticion.Left = lblTurno.Right + 5;
                    clienteActual = null;

                    lblTurno.Text = "Turno de " + parametros[0];
                    //Comprobar si hay tiempo limite
                    if (parametros.Length > 2)
                    {
                        TiempoRestante = int.Parse(parametros[2]);
                        timer1.Enabled = true;
                    }

                    foreach (Cliente jugador in Clientes)
                    {
                        if (string.Compare(jugador.Nombre, parametros[0], true) == 0)
                        {
                            clienteActual = jugador;
                            mapa.JugadorActual = clienteActual;

                            if (alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.Checked)
                                ReproducirAlarma();

                            toolStrip2.Enabled = true;
                            toolStripButton6.Enabled = true;
                            toolStripButton4.Enabled = true;

                            listaJugadores.Items.Remove(clienteActual);
                            if (listaJugadores.SelectedIndex == -1)
                                listaJugadores.SelectedIndex = 0;

                            bFinTurno.Visible = true;
                            jugador.ClienteTcp.EnviarComando(53, "Comando de inicio de turno recibido");
                            this.Invoke(new EventHandler(EnviarFlota), new object[] { null, null });//Enviar ataque
                            break;
                        }
                    }
                    if (clienteActual == null)
                    {
                        HerramientaCancelada();
                        peticion.Text = "Esperando a que acabe su turno....";
                        clienteActual = null;

                        toolStrip2.Enabled = true;
                        if (Clientes.Count == 1)
                        {
                            toolStripButton6.Enabled = false;
                        }
                        else
                        {
                            toolStripButton6.Enabled = false;
                            toolStripButton4.Enabled = false;
                        }
                        bFinTurno.Visible = false;
                        mapa.JugadorActual = null;
                    }
                    break;
                case 204://Fin de tiempo de turno
                    bFinTurno.PerformClick();
                    break;
                case 60://Fin de partida
                    Informacion("El jugador " + parametros[0] + " ha ganadao la partida", true);

                    try
                    {                        
                        lblTurno.Text = "¡El jugador " + parametros[0] + " ha ganadao la partida!";
                        lblTurno.ForeColor = mapa.BuscarJugador(parametros[0]).Color;
                        peticion.Text = string.Empty;
                        CancelarHerramienta(null, null);
                    }
                    catch { }
                    if (Clientes != null && Clientes.Count > 0)
                        Clientes[0].ClienteTcp.EnviarComando(302, "Salir del servidor");

                    break;
                case 61://Jugador eliminado

                    Informacion("El jugador " + parametros[0] + " ha sido eliminado", true);
                    Cliente eliminado = mapa.BuscarJugador(parametros[0]);
                    if (eliminado != null)
                        eliminado.Derrotado = true;

                    break;
                case 62://Jugador sin planetas
                    Informacion("El jugador " + parametros[0] + " no tiene ningún planeta conquistado");
                    Cliente sinplanetas = mapa.BuscarJugador(parametros[0]);
                    if (sinplanetas != null)
                        sinplanetas.PoseePlaneta = false;

                    break;
                case 70://Llegada de flota
                    Informacion(string.Format("Han llegado {0} naves de refuerzo al planeta {1}", parametros[1], parametros[0]));
                    break;
                case 72://Jugador retirado
                    Informacion(string.Format("El jugador {0} se ha retirado de la partida. Al final del turno sus planetas serán neutrales.", parametros[0]), true);
                    break;
                case 71://Batalla
                    Batalla.ResultadoBatalla resultado = (Batalla.ResultadoBatalla)int.Parse(parametros[3]);

                    string dueño = parametros[2] == string.Empty ? "Neutral" : parametros[2];
                    if (resultado == Batalla.ResultadoBatalla.GanaAtacante)
                    {
                        Informacion(string.Format("El planeta {0} ({1}) ha caído ante una flota de {2} naves del jugador {3}. Han sobrevivido {4} naves."
                                                  , parametros[0], dueño, parametros[4], parametros[1], parametros[6]), true);
                    }
                    else if (resultado == Batalla.ResultadoBatalla.GanaDefensor)
                    {
                        string texto = string.Format("El planeta {0} ({1}) se ha defendido con {4} naves de un ataque de {3} naves del jugador {2}."
                                                     , parametros[0], dueño, parametros[1], parametros[4], parametros[5]);
                        if (parametros[2] != string.Empty)//No es neutral
                            texto += " Han sobrevivido " + parametros[7] + " naves.";
                        Informacion(texto, true);
                    }
                    else//Empate
                    {
                        Informacion(string.Format("La batalla en el planeta {0} entre {1} naves del jugador {2} y {3} naves del jugador {4} ha terminado en empate. Han sobrevivido {5} naves del atacante y {6} del defensor."
                                                  , parametros[0], parametros[4], parametros[1], parametros[5], dueño, parametros[6], parametros[7]), true);
                    }
                    break;
                case 206://Actualizacion de datos
                    mapa.CargarDatos(parametros);
                    break;
                case 210://Cargar partida
                    mapa.Inicializar(int.Parse(parametros[0]), mapa.Jugadores, int.Parse(parametros[1]));
                    Informacion("El administrador ha cargado una nueva partida", true);
                    break;
                case 401://Chat de administrador
                    ListViewItem item = new ListViewItem();
                    item.Text = "Administrador";
                    item.SubItems.Add(parametros[0]);
                    item.Font = new Font(item.Font, FontStyle.Bold);
                    item.ForeColor = Color.Black;
                    listaChat.Items.Add(item);

                    item.EnsureVisible();

                    if (alarmaAlRecibirMensajeDeChatToolStripMenuItem.Checked)
                        MensajeNuevo();
                    break;
                case 402://Chat de jugador

                    ListViewItem itemChat = new ListViewItem();
                    itemChat.Text = parametros[0];
                    itemChat.SubItems.Add(parametros[1]);
                    foreach (Cliente jugador in mapa.Jugadores)
                    {
                        if (jugador.Nombre == parametros[0])
                        {
                            itemChat.ForeColor = jugador.Color;
                            break;
                        }
                    }
                    listaChat.Items.Add(itemChat);

                    itemChat.EnsureVisible();

                    if (alarmaAlRecibirMensajeDeChatToolStripMenuItem.Checked)
                        MensajeNuevo();

                    break;
            }
        }

        private void Informacion(string texto)
        {
            Informacion(texto, false);
        }

        private void Informacion(string texto, bool mensaje)
        {
            ListViewItem item = new ListViewItem();
            item.Text = texto;
            item.SubItems.Add(numeroRondaActual.ToString());
            listaMensajes.Items.Add(item);

            item.EnsureVisible();

            //   iconListBox1.scr = iconListBox1.Items.Count - 1;

            if (mensaje && mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.Checked)
                MessageBox.Show(texto, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int TiempoRestante;
        private void SegundoTranscurrido(object sender, EventArgs e)
        {
            TiempoRestante--;
            if (TiempoRestante < 0)
                TiempoRestante = 0;

            rondaActual.Text = string.Format("Ronda {0} ({1} segundos restantes)", numeroRondaActual, TiempoRestante);

            if (TiempoRestante <= 0 && bFinTurno.Visible)
            {
                bFinTurno.PerformClick();
                timer1.Enabled = false;
            }
        }

        private void ReproducirAlarma()
        {
            try
            {
                string ruta = System.IO.Path.Combine(Application.StartupPath, "alarma.wav");
                if (System.IO.File.Exists(ruta))
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(ruta);
                    player.Play();
                }
            }
            catch { }
        }

        private void MensajeNuevo()
        {
            try
            {
                string ruta = System.IO.Path.Combine(Application.StartupPath, "chat.wav");
                if (System.IO.File.Exists(ruta))
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(ruta);
                    player.Play();
                }
            }
            catch { }
        }

        #region Eventos de mouse sobre planetas
        private void SobrePlaneta(Mapa mapa, Planeta planeta)
        {
            imagenPlaneta.Image = planeta.Imagen;

            Info.Text = string.Format("Nombre del planeta: {0}\r\nLocalizazión: {1}", planeta.Name, planeta.Localizacion);
            if (planeta.Dueño != null)
            {
                Info.Text += string.Format("\r\nDueño: {1}\r\nNaves: {2}\r\nProducción: {3}\r\nTecnología militar: {4}"
                    , planeta.Name, planeta.Dueño.Nombre, planeta.Naves, planeta.Produccion, planeta.TecnologiaMilitar);
            }
        }

        private void FueraPlaneta(Mapa mapa, Planeta planeta)
        {
            imagenPlaneta.Image = null;
            Info.Text = string.Empty;
        }

        private void ClicPlaneta(Mapa mapa, Planeta planeta)
        {
            PlanetaPulsado = planeta;
            if (Esperar != null)
                Esperar.Set();
        }
        #endregion

        #region Funciones herramientas
        Planeta PlanetaPulsado;
        int numeroNaves;
        int numeroRondaActual;
        AutoResetEvent Esperar;

        private Planeta ObtenerPlanetaPulsado()
        {
            PlanetaPulsado = null;
            Esperar = new AutoResetEvent(false);
            Esperar.WaitOne();
            return PlanetaPulsado;
        }

        void AñadirTextBox(object sender, EventArgs e)
        {
            this.peticionTexto = new TextBox();
            this.peticionTexto.Location = new System.Drawing.Point(427, 8);
            peticionTexto.Left = peticion.Right + 5;
            this.peticionTexto.Size = new System.Drawing.Size(100, 23);
            this.peticionTexto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TeclaPulsadaPeticionTexto);
            this.panel1.Controls.Add(this.peticionTexto);
            peticionTexto.Focus();
        }
        private System.Windows.Forms.TextBox peticionTexto;

        void OcultarTextBox(object sender, EventArgs e)
        {
            panel1.Controls.Remove(peticionTexto);
        }

        private int ObtenerNumeroIntroducido()
        {
            this.Invoke(new EventHandler(AñadirTextBox));

            numeroNaves = -1;
            while (numeroNaves == -1)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(15);
            }

            this.Invoke(new EventHandler(OcultarTextBox));

            return numeroNaves;
        }

        void TeclaPulsadaPeticionTexto(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                try
                {
                    numeroNaves = int.Parse(peticionTexto.Text);
                }
                catch { numeroNaves = 0; }
            }
            else if (e.KeyValue < 48 || e.KeyValue > 57)
                e.Handled = true;
        }

        private void HerramientaCancelada()
        {
            try
            {
                peticion.Left = lblTurno.Right + 5;
                peticion.Text = "Selecciona una herramienta";

                peticionTexto.Visible = false;
                bCancelarHerramienta.Visible = false;
                if (herramienta != null && herramienta.IsAlive)
                {
                    herramienta.Abort();
                }
            }
            catch { }
        }


        private void FinalizarTurno(object sender, EventArgs e)
        {
            bFinTurno.Visible = false;
            timer1.Enabled = false;
            bCancelarHerramienta.Visible = false;

            //Esperar un poco para procesar eventos
            Application.DoEvents();
            System.Threading.Thread.Sleep(50);
            Application.DoEvents();
            System.Threading.Thread.Sleep(50);
            Application.DoEvents();

            HerramientaCancelada();

            if (Clientes.Count == 1)
            {
                toolStripButton6.Enabled = false;
            }
            else
            {
                toolStripButton6.Enabled = false;
                toolStripButton4.Enabled = false;
            }
         
            lblTurno.Text = string.Empty;
            peticion.Text = string.Empty;
            bCancelarHerramienta.Visible = false;

            if (clienteActual != null)
            {
                listaJugadores.Items.Add(clienteActual);
                clienteActual.FinTurno();
            }
            clienteActual = null;
        }

        private void CancelarHerramienta(object sender, EventArgs e)
        {
            HerramientaCancelada();
        }
        #endregion

        #region Herramientas

        private System.Threading.Thread herramienta;
        private void IniciarHerramienta(System.Threading.ThreadStart evento)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                HerramientaCancelada();
                bCancelarHerramienta.Visible = true;

                herramienta = new System.Threading.Thread(evento);
                herramienta.Name = "SubHerramienta";
                herramienta.Start();
            }
            catch { }
        }

        private void EnviarFlota(object sender, EventArgs e)
        {
            HerramientaCancelada();
            IniciarHerramienta(new ThreadStart(EnviarFlota));
        }

        private void EnviarFlota()
        {
            bCancelarHerramienta.Visible = true;

        otroPlaneta:
            peticion.Text = "Selecciona planeta de origen de la flota...";
            Planeta origen = ObtenerPlanetaPulsado();

            if (origen.Dueño != clienteActual)
            {
                goto otroPlaneta;
            }

        DeNuevo:
            peticion.Text = "Selecciona planeta de destino...";
            Planeta destino = ObtenerPlanetaPulsado();

            if (origen == destino)
                goto DeNuevo;

            peticion.Text = "Número de naves";
            int naves = ObtenerNumeroIntroducido();

            if (origen.Naves >= naves)
            {
                clienteActual.EnviarFlota(origen, destino, naves, numeroRondaActual);
            }
            else
            {
                if (MessageBox.Show("No hay suficientes naves en el planeta.\r\n\r\n¿Enviar todas las naves que quedan? (" + origen.Naves + " naves)", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
                    == DialogResult.Yes)
                {
                    clienteActual.EnviarFlota(origen, destino, origen.Naves, numeroRondaActual);
                }
            }

            mapa.Refresh();

            this.Invoke(new EventHandler(EnviarFlota), new object[] { null, null });//Enviar ataque
        }

        private void MedirDistancia(object sender, EventArgs e)
        {
            HerramientaCancelada();
            IniciarHerramienta(new ThreadStart(MedirDistancia));
        }

        void MedirDistancia()
        {
            peticion.Text = "Selecciona planeta de origen...";
            Planeta p1 = ObtenerPlanetaPulsado();

        DeNuevo:
            peticion.Text = "Selecciona planeta de destino...";
            Planeta p2 = ObtenerPlanetaPulsado();

            if (p2 == p1)
                goto DeNuevo;

            if (p1 == null || p2 == null)
                return;

            double distancia = Cliente.CalcularDistancia(p1, p2);
            MessageBox.Show("La distancia de ambos planetas es de " +
                            distancia + " años luz.\r\n\r\nUna flota enviada en este turno, llegaría en el turno " +
                            (numeroRondaActual + (int)Math.Round(distancia)) + ".", "Distancia", MessageBoxButtons.OK);

            this.Invoke(new EventHandler(EnviarFlota), new object[] { null, null });//Enviar ataque
        }

        private void MostrarFlotasEnMovimiento(object sender, EventArgs e)
        {
            if (Clientes.Count == 1)
            {
                Flotas ventana = new Flotas(Clientes[0]);
                ventana.ShowDialog();
            }
            else if (clienteActual != null)
            {
                Flotas ventana = new Flotas(clienteActual);
                ventana.ShowDialog();
            }
        }

        private void SimularBatalla(object sender, EventArgs e)
        {
            HerramientaCancelada();
            IniciarHerramienta(new ThreadStart(SimularBatalla));
        }

        void SimularBatalla()
        {
            peticion.Text = "Selecciona planeta de origen...";
            Planeta p1 = ObtenerPlanetaPulsado();

        DeNuevo:
            peticion.Text = "Selecciona planeta de destino...";
            Planeta p2 = ObtenerPlanetaPulsado();

            if (p2 == p1)
                goto DeNuevo;

            if (Clientes.Count == 1)
                this.Invoke(new DelegadoSimularBatalla(simularBatalla), new object[] { p1, p2, Clientes[0] });
            else if (clienteActual != null)
                this.Invoke(new DelegadoSimularBatalla(simularBatalla), new object[] { p1, p2, clienteActual });

            this.Invoke(new EventHandler(EnviarFlota), new object[] { null, null });//Enviar ataque
        }

        private delegate void DelegadoSimularBatalla(Planeta p1, Planeta p2, Cliente jugador);

        private void simularBatalla(Planeta p1, Planeta p2, Cliente jugador)
        {
            SimularBatalla bat = new SimularBatalla(p1, p2, clienteActual);
            bat.Show();
        }

        private void IniciarCalculadora(object sender, EventArgs e)
        {
            try
            {
                string ruta = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\system32\calc.exe";
                System.Diagnostics.Process.Start(ruta);
            }
            catch { }
        }

        #endregion

        #region Actualizaciones
        private void buscarActualizacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actualizador.Actualizacion.ComprobarActualizacion(System.InformacionPrograma.VersionActual, true, System.InformacionPrograma.UrlActualizacion);
        }

        void Actualizacion_ActualizacionDisponible(string nuevaVersion, string urlArchivoActualizador)
        {
            if (MessageBox.Show("Se ha encontrado una nueva versión (" + nuevaVersion + ") de SharpKonquest. ¿Quieres actualizar automáticamente los archivos?\r\n\r\nAviso: Si instala la actualización, el programa se cerrará para poder llevarla a cabo.",
                                "Actualización disponible", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                Application.ApplicationExit += new EventHandler(IniciarActualizador);
                urlActualizacion = urlArchivoActualizador;
                Application.Exit();
            }
        }
        private string urlActualizacion;

        void IniciarActualizador(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(urlActualizacion))
                return;
            string destino = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Actualizador" + new Random().Next() + ".exe");
            System.IO.File.Copy(System.IO.Path.Combine(Application.StartupPath, "Actualizador.exe"), destino);
            string arg = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\"", urlActualizacion, Application.StartupPath, "SharpKonquest", Application.ExecutablePath, "aviso");
            System.Diagnostics.Process.Start(destino, arg);

            Environment.Exit(0);
        }

        #endregion


        private bool cerrando = false;
        private void Cerrar(object sender, FormClosedEventArgs e)
        {
            try
            {
                cerrando = true;
                foreach (Cliente jugador in Clientes)
                {
                    try
                    {
                        if (jugador.AdministradorServidor)
                            jugador.ClienteTcp.EnviarComando(302, "Salir del servidor");
                        jugador.ClienteTcp.EnviarComando(11, "Adios");
                    }
                    catch { }
                }
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
            }
            catch { }
            try
            {
                if (procesoServidor != null)
                    procesoServidor.Kill();
            }
            catch { }
            if (System.InformacionPrograma.Mono)
            {
                Application.Exit();
            }
            else
            {
                if (string.IsNullOrEmpty(urlActualizacion))
                    Environment.Exit(0);
                else
                    Application.Exit();
            }
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDe acerca = new AcercaDe();
            acerca.ShowDialog();
        }

        private void limpiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listaMensajes.Items.Clear();
        }

        private void dibujarFlotasPropiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapa.DibujarFlotasPropias = dibujarFlotasPropiasToolStripMenuItem.Checked;
            mapa.Refresh();
        }

        private void dibujarFlotasEnemigasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapa.DibujarFlotasEnemigas = dibujarFlotasEnemigasToolStripMenuItem.Checked;
            mapa.Refresh();
        }

        private void limitarTiempoDeTurnoToolStripMenuItem_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            if (Clientes.Count > 0)
            {
                Clientes[0].ClienteTcp.EnviarComando(303, "Limitar turnos a '" + e.ClickedItem.Tag.ToString() + "' segundos");
            }
        }

        private void bChat_Click(object sender, EventArgs e)
        {
            try
            {
                if (textoChat.Text.Length == 0)
                    return;
                textoChat.Text = textoChat.Text.Replace("'", "\"");
                if (listaJugadores.SelectedItem is string)//Enviar a todos
                {
                    foreach (Cliente jugador in Clientes)
                    {
                        if (jugador.AdministradorServidor)
                            jugador.ClienteTcp.EnviarComando(400, string.Format("Difundir mensaje: '{0}'", textoChat.Text));
                        textoChat.Text = string.Empty;
                    }
                }
                else
                {
                    if (Clientes.Count == 1)
                    {
                        Clientes[0].ClienteTcp.EnviarComando(402, string.Format("Enviar mensaje a '{0}': '{1}'", listaJugadores.SelectedItem.ToString(), textoChat.Text));

                        ListViewItem itemchat = new ListViewItem();
                        itemchat.Text = Clientes[0].Nombre + "->" + listaJugadores.SelectedItem.ToString();
                        itemchat.SubItems.Add(textoChat.Text);
                        itemchat.ForeColor = Clientes[0].Color;

                        listaChat.Items.Add(itemchat);
                        itemchat.EnsureVisible();
                    }
                    else if (clienteActual != null)
                    {
                        clienteActual.ClienteTcp.EnviarComando(402, string.Format("Enviar mensaje a '{0}': '{1}'", listaJugadores.SelectedItem.ToString(), textoChat.Text));

                        ListViewItem itemchat = new ListViewItem();
                        itemchat.Text = clienteActual.Nombre + "->" + listaJugadores.SelectedItem.ToString();
                        itemchat.SubItems.Add(textoChat.Text);
                        itemchat.ForeColor = clienteActual.Color;

                        listaChat.Items.Add(itemchat);
                        itemchat.EnsureVisible();
                    }
                    textoChat.Text = string.Empty;
                }

                textoChat.Focus();
            }
            catch { }
        }

        private void bChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                bChat.PerformClick();
            }
        }

        private void office2007ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.FutureStyle.Office2007.OfficeRenderer renderer = new System.Windows.FutureStyle.Office2007.OfficeRenderer();
            toolStrip1.Renderer = renderer;
            toolStrip2.Renderer = renderer;
            menuStrip1.Renderer = renderer;
            futurePanel1.Style = System.Windows.Office2007.BackPanelStyles.Luna;
        }

        private void porDefectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip1.Renderer = null;
            toolStrip2.Renderer = null;
            menuStrip1.Renderer = null;
            futurePanel1.Style = System.Windows.Office2007.BackPanelStyles.Transparent;
        }

        private void oscuraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.FutureStyle.Office2007.OfficeRenderer renderer = new System.Windows.FutureStyle.Office2007.OfficeRenderer();
            toolStrip1.Renderer = renderer;
            toolStrip2.Renderer = renderer;
            menuStrip1.Renderer = renderer;
            futurePanel1.Style = System.Windows.Office2007.BackPanelStyles.Vista;
        }

        private void splitContainer2_Panel1_Resize(object sender, EventArgs e)
        {
            imagenPlaneta.Left = panel2.Width / 2 - imagenPlaneta.Width / 2;
            listaChat.Height = groupBox1.Height - 54;
        }

        private void listaMensajes_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            e.DrawBackground();

            StringFormat format = StringFormat.GenericDefault;
            format.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(listaMensajes.Items[e.Index].ToString(), this.Font, new SolidBrush(this.ForeColor), e.Bounds, format);

            e.DrawFocusRectangle();
        }

        private void limpiarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listaChat.Items.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Estadisticas ventana = new Estadisticas(mapa);
            ventana.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XmlConfig conf = new XmlConfig();
                conf.SetProperty("semilla", mapa.Semilla);
                conf.SetProperty("neutrales", mapa.Neutrales);
                conf.SetProperty("ronda", numeroRondaActual);
                conf.SetProperty("datos", mapa.GuardarDatos());
                conf.Save(saveFileDialog1.FileName);
            }
        }

        private void cargarPartidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (Cliente jugador in Clientes)
                {
                    if (jugador.AdministradorServidor)
                    {
                        XmlConfig conf = new XmlConfig(openFileDialog1.FileName);
                        conf.Load();

                        jugador.ClienteTcp.EnviarComando(210, string.Format("Cargar la partida de semilla '{0}', con '{1}' neutrales y en la ronda '{2}'. Datos: '{3}'",
                            conf.GetInt("semilla", 0), conf.GetInt("neutrales", 0), conf.GetInt("ronda", 0), conf.GetString("datos", string.Empty).Replace("'", "&apos;")));

                        break;
                    }
                }
            }
        }

        private void ningunaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapa.ImagenFondo = null;
            mapa.ColorFondoCeldas = Color.Black;
        }

        private void espacioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapa.ImagenFondo = Programa.ObtenerImagenIncrustada("fondoespacio");
        }

        private void elegirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    mapa.ImagenFondo = Image.FromFile(openFileDialog2.FileName);
                }
                catch { MessageBox.Show("Imagen no válida"); }
            }
        }

        private void degradadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapa.ImagenFondo = null;
            mapa.brochaCeldas = new LinearGradientBrush(new Rectangle(0, 0, mapa.Width, mapa.Height),
                Color.Black, Color.DimGray, LinearGradientMode.ForwardDiagonal);
            mapa.Refresh();
        }

        private void mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            mapa.DibujarDestinoFlotas = mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem.Checked;
            mapa.Refresh();
        }

        private void listaMensajes_SizeChanged(object sender, EventArgs e)
        {
            columnHeader2.Width = listaMensajes.Width - 65;
        }

        private void listaChat_SizeChanged(object sender, EventArgs e)
        {
            columnHeader4.Width = listaChat.Width - 165;
        }
    }
}
