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

namespace SharpKonquest
{
    partial class Principal : Form
    {

        public Principal()
        {
            InitializeComponent();

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            Actualizador.Actualizacion.ActualizacionDisponible += new Actualizador.Actualizacion.DelegadoActualizacionDisponible(Actualizacion_ActualizacionDisponible);
            Actualizador.Actualizacion.ComprobarActualizacion(Application.ProductVersion, false, Juego.DatosActualizacion.UrlActualizacion);

            System.Windows.FutureStyle.Office2007.OfficeRenderer renderer = new System.Windows.FutureStyle.Office2007.OfficeRenderer();
            toolStrip1.Renderer = renderer;
            toolStrip2.Renderer = renderer;
            menuStrip1.Renderer = renderer;
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
                    }
                }

                //Inicializar mapa
                mapa.Jugadores = new List<Cliente>();
                foreach (IconListBoxItem item in partida.listaJugadores.Items)
                {
                    bool añadido = false;
                    foreach (Cliente cliente in Clientes)
                    {
                        if (((Cliente)item.Tag).Nombre == cliente.Nombre)
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

                panel2.Visible = true;
                listaJugadores.SelectedIndex = 0;
            }
        }

        Cliente clienteActual;
        void ComandoRecibido(int comando, string[] parametros, string cadena, ClienteTCP clienteTcp)
        {
            switch (comando)
            {
                case 2://Ping
                    clienteTcp.EnviarDatos(3, "PING OK");
                    break;
                case 51://Inicio de ronda
                    numeroRondaActual = int.Parse(parametros[0]);
                    mapa.RondaActual = numeroRondaActual;
                    mapa.Refresh();
                    rondaActual.Text = "Ronda " + numeroRondaActual;
                    Informacion("Inicio de la ronda " + numeroRondaActual);
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
                        if (jugador.Nombre == parametros[0])
                        {
                            clienteActual = jugador;
                            mapa.JugadorActual = clienteActual;

                            if (alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.Checked)
                                ReproducirAlarma();

                            toolStrip2.Enabled = true;
                            bFinTurno.Visible = true;
                            jugador.ClienteTcp.EnviarDatos(53, "Comando de inicio de turno recibido");
                            this.Invoke(new EventHandler(EnviarFlota), null, null);//Enviar ataque
                            break;
                        }
                    }
                    if (clienteActual == null)
                    {
                        HerramientaCancelada();
                        peticion.Text = "Esperando a que acabe su turno....";
                        clienteActual = null;
                        toolStrip2.Enabled = false;
                        bFinTurno.Visible = false;
                        mapa.JugadorActual = null;
                    }
                    break;
                case 204://Fin de tiempo de turno
                    bFinTurno.PerformClick();
                    break;
                case 60://Fin de partida
                    Informacion("El jugador " + parametros[0] + " ha ganadao la partida", true);

                    if (Clientes != null && Clientes.Count > 0)
                        Clientes[0].ClienteTcp.EnviarDatos(302, "Salir del servidor");

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
                case 206://Actualizacion de datos de planetas
                    for (int contador = 0; contador < parametros.Length; contador += 3)
                    {
                        Planeta p = mapa.ObtenerPlaneta(parametros[contador]);
                        p.Naves = int.Parse(parametros[contador + 1]);
                        if (parametros[contador + 2] == string.Empty)
                            p.Dueño = null;
                        else
                            p.Dueño = mapa.BuscarJugador(parametros[contador + 2]);
                    }
                    break;

                case 207://Actualizacion de datos de jugadores
                    foreach (Cliente jugador in mapa.Jugadores)
                    {
                        jugador.Flotas.Clear();
                    }

                    for (int contador = 0; contador < parametros.Length; contador += 7)
                    {
                        Cliente jugador = mapa.BuscarJugador(parametros[contador]);
                        Flota flota = new Flota();
                        flota.Naves = int.Parse(parametros[contador + 1]);
                        flota.TecnologiaMilitar = int.Parse(parametros[contador + 2]);
                        flota.RondaSalida = int.Parse(parametros[contador + 3]);
                        flota.RondaLlegada = int.Parse(parametros[contador + 4]);
                        flota.Origen = mapa.ObtenerPlaneta(parametros[contador + 5]);
                        flota.Destino = mapa.ObtenerPlaneta(parametros[contador + 6]);
                        flota.Distancia = Cliente.CalcularDistancia(flota.Origen, flota.Destino);

                        jugador.Flotas.Add(flota);
                    }
                    mapa.Refresh();
                    break;
                case 401://Chat de administrador
                    MessageBox.Show("El administrador envía el siguiente mensaje:\r\n" + parametros[0], "Chat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 402://Chat de jugador
                    MessageBox.Show("El jugador " + parametros[0] + " te envía el siguiente mensaje:\r\n" + parametros[1], "Chat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }
        private void Informacion(string texto)
        {
            Informacion(texto, false);
        }
        private void Informacion(string texto, bool mensaje)
        {
            IconListBoxItem item = new IconListBoxItem();
            item.Text = texto;
            iconListBox1.AddItem(item);
            iconListBox1.ScrollControlIntoView(item);

            if (mensaje && mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.Checked)
                MessageBox.Show(texto,"Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        #region Eventos de mouse sobre planetas
        private void SobrePlaneta(Mapa mapa, Planeta planeta)
        {
            imagenPlaneta.Image = planeta.Imagen;

            if (planeta.Dueño != null)
            {
                Info.Text = string.Format(
                    "Nombre del planeta: {0}\r\nDueño: {1}\r\nNaves: {2}\r\nProducción: {3}\r\nTecnología militar: {4}"
                    , planeta.Name, planeta.Dueño.Nombre, planeta.Naves, planeta.Produccion, planeta.TecnologiaMilitar);
            }
            else
            {
                Info.Text = "Nombre del planeta: " + planeta.Name;
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
            this.peticionTexto = new XPTextBox();
            this.peticionTexto.Location = new System.Drawing.Point(427, 8);
            peticionTexto.Left = peticion.Right + 5;
            this.peticionTexto.Size = new System.Drawing.Size(100, 23);
            this.peticionTexto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TeclaPulsadaPeticionTexto);
            this.panel1.Controls.Add(this.peticionTexto);
            peticionTexto.Focus();
        }
        private System.Windows.Forms.XPTextBox peticionTexto;

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
            toolStrip2.Enabled = false;
            lblTurno.Text = string.Empty;
            peticion.Text = string.Empty;
            bCancelarHerramienta.Visible = false;

            if (clienteActual != null)
                clienteActual.FinTurno();
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

            this.Invoke(new EventHandler(EnviarFlota), null, null);//Enviar ataque
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
                (numeroRondaActual + (int)Math.Truncate(distancia)) + ".", "Distancia", MessageBoxButtons.OK);

            this.Invoke(new EventHandler(EnviarFlota), null, null);//Enviar ataque
        }

        private void MostrarFlotasEnMovimiento(object sender, EventArgs e)
        {
            Flotas ventana = new Flotas(clienteActual);
            ventana.ShowDialog();
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

            this.Invoke(new DelegadoSimularBatalla(simularBatalla), p1, p2, clienteActual);

            this.Invoke(new EventHandler(EnviarFlota), null, null);//Enviar ataque
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
            Actualizador.Actualizacion.ComprobarActualizacion(Application.ProductVersion, true, Juego.DatosActualizacion.UrlActualizacion);
        }

        void Actualizacion_ActualizacionDisponible(string nuevaVersion, string urlArchivoActualizador)
        {
            if (MessageBox.Show("Se ha encontrado una nueva versión de SharpKonquest. ¿Quieres actualizar automáticamente los archivos?\r\n\r\nAviso: Si instala la actualización, el programa se cerrará para poder llevarla a cabo.",
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

        private void Cerrar(object sender, FormClosedEventArgs e)
        {
            try
            {
                foreach (Cliente jugador in Clientes)
                {
                    try
                    {
                        if (jugador.AdministradorServidor)
                            jugador.ClienteTcp.EnviarDatos(302, "Salir del servidor");
                        jugador.ClienteTcp.EnviarDatos(11, "Adios");
                    }
                    catch { }
                }

                Application.DoEvents();
            }
            catch { }
            try
            {
                if (procesoServidor != null)
                    procesoServidor.Kill();
            }
            catch { }
            if (string.IsNullOrEmpty(urlActualizacion))
                Environment.Exit(0);
            else
                Application.Exit();
        }

        private void PanelInformacionCambiado(object sender, EventArgs e)
        {
            if (splitContainer2.SplitterDistance != 265)
                splitContainer2.SplitterDistance = 265;
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDe acerca = new AcercaDe();
            acerca.ShowDialog();
        }

        private void limpiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iconListBox1.Clear();
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
                Clientes[0].ClienteTcp.EnviarDatos(303, "Limitar turnos a '" + e.ClickedItem.Tag.ToString() + "' segundos");
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
                            jugador.ClienteTcp.EnviarDatos(400, string.Format("Difundir mensaje: '{0}'", textoChat.Text));
                        textoChat.Text = string.Empty;
                    }
                }
                else
                {
                    if (Clientes.Count == 1)
                    {
                        Clientes[0].ClienteTcp.EnviarDatos(402, string.Format("Enviar mensaje a '{0}': '{1}'", listaJugadores.SelectedItem.ToString(), textoChat.Text));
                    }
                    else if (clienteActual != null)
                    {
                        clienteActual.ClienteTcp.EnviarDatos(402, string.Format("Enviar mensaje a '{0}': '{1}'", listaJugadores.SelectedItem.ToString(), textoChat.Text));
                    }
                    textoChat.Text = string.Empty;
                }
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
    }
}