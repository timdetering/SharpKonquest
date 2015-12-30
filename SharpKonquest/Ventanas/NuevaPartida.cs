using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SharpKonquest.Clases;
using System.TCP;

namespace SharpKonquest
{
    partial class NuevaPartida : GlassForm
    {
        public List<Cliente> clientes=new List<Cliente>();
        Principal vPrincipal;

        public NuevaPartida(Principal ventanaPrincipal)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            semillaAleatoria.PerformClick();

            semillaMapa.Maximum = int.MaxValue;

            vPrincipal = ventanaPrincipal;
        }

        private void MapaAleatorio(object sender, EventArgs e)
        {
            Aleatorios r = new Aleatorios();
            semillaMapa.Value = r.Next(0, int.MaxValue);
        }

        bool actualizarMapa=true;
        private void SemillaMapa(object sender, EventArgs e)
        {
            if (actualizarMapa == false)
                return;
            
            mapa.Jugadores.Clear();
            foreach (ItemLista item in listaJugadores.Items)
            {
                    mapa.Jugadores.Add(item.Tag);
            }

            mapa.Inicializar((int)semillaMapa.Value, mapa.Jugadores, (int)neutrales.Value);
            mapa.Invalidate();
            
            ActualizarDatosMapaServidor();
        }

        private void ActualizarDatosMapaServidor()
        {
            if (clientes.Count > 0)
                clientes[0].ClienteTcp.EnviarComando(300, string.Format("La semilla del mapa es '{0}' y los planetas neutrales son '{1}'", semillaMapa.Value, neutrales.Value));
        }



        void ComandoRecibido(ushort comando, string[] parametros, string cadena, ClienteTCP clienteTcp)
        {
            switch (comando)
            {
                case 100://Nombre repetido
                    MessageBox.Show("Error, nombre repetido");
                    foreach (Cliente jugador in this.clientes)//Borrar el cliente actual    
                    {
                        if (jugador.ClienteTcp == clienteTcp)
                        {
                            clientes.Remove(jugador);
                            jugador.ClienteTcp.Desconectar();
                            break;
                        }
                    }
                    break;
                case 101://Color repetido
                    MessageBox.Show("Error, color repetido");
                    foreach (Cliente jugador in this.clientes)//Borrar el cliente actual    
                    {
                        if (jugador.ClienteTcp == clienteTcp)
                        {
                            clientes.Remove(jugador);
                            jugador.ClienteTcp.Desconectar();
                            break;
                        }
                    }
                    break;
                case 103://Versión antigua
                    MessageBox.Show("El servidor necesita de una versión más reciente del programa para poder jugar en él.\r\nActualiza el programa para poder continuar.",
                          "Nueva versión necesaria", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    foreach (Cliente jugador in this.clientes)//Borrar el cliente actual    
                    {
                        if (jugador.ClienteTcp == clienteTcp)
                        {
                            clientes.Remove(jugador);
                            jugador.ClienteTcp.Desconectar();
                            break;
                        }
                    }
                    break;
                case 12://Login correcto
                    ActualizarDatosMapaServidor();
                    break;
                case 13://Jugador es administrador
                    foreach (Cliente jugadorLocal in clientes)
                    {
                        if (jugadorLocal.ClienteTcp == clienteTcp)
                        {
                            jugadorLocal.AdministradorServidor = true;
                            break;
                        }
                    }
                    break;
                case 11://Jugador desconectado
                    try
                    {
                        clienteTcp.Desconectar();
                    }
                    catch { }
                    foreach (Cliente jugadorLocal in clientes)
                    {
                        if (jugadorLocal.ClienteTcp == clienteTcp)
                        {
                            MessageBox.Show("El jugador " + jugadorLocal.Nombre + " ha sido expulsado de la partida");
                            clientes.Remove(jugadorLocal);
                            try
                            {
                                foreach (ItemLista item in listaJugadores.Items)
                                {
                                    if (string.Compare(item.Tag.Nombre ,jugadorLocal.Nombre,true)==0)
                                    {
                                        listaJugadores.Items.Remove(item);
                                        break;
                                    }
                                }
                            }
                            catch { }
                            break;
                        }
                    }

                    break;
                case 200://Informacion sobre el mapa
                    actualizarMapa = false;

                    bool cambiado = false;

                    if (semillaMapa.Value != int.Parse(parametros[0]))
                    {
                        semillaMapa.Value = int.Parse(parametros[0]);
                        cambiado = true;
                    }
                    if (neutrales.Value != int.Parse(parametros[1]))
                    {
                        neutrales.Value = int.Parse(parametros[1]);
                        cambiado = true;
                    }

                    List<Cliente> listaClientes = mapa.Jugadores;
                    if ((parametros.Length - 2) / 2 != listaJugadores.Items.Count)//Jugadores añadidos
                    {
                        cambiado = true;
                        listaJugadores.Items.Clear();

                        listaClientes = new List<Cliente>();
                        for (int contador = 2; contador < parametros.Length; contador += 3)
                        {
                            Cliente jugador = new Cliente(parametros[contador], null, 0);
                            jugador.Color = Color.FromArgb(int.Parse(parametros[contador + 1]));
                            jugador.AdministradorServidor = parametros[contador + 2] == "1" ? true : false;
                            listaClientes.Add(jugador);
                            JugadorConectado(jugador);
                        }
                    }
                    if (cambiado)
                        mapa.Inicializar((int)semillaMapa.Value, listaClientes, (int)neutrales.Value);

                    actualizarMapa = true;
                    break;
                case 201://Iniciar partida
                    foreach (Cliente cliente in clientes)
                    {
                        cliente.ClienteTcp.DatosRecibidos -= ComandoRecibido;
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if(colorDialog1.Color!=Color.White)
                color.BackColor = colorDialog1.Color;
            }
        }

        public bool ServidorActivo = false;
        public System.Diagnostics.Process procesoServidor;
        private void bConectar_Click(object sender, EventArgs e)
        {
            if (host.Text == "localhost" || host.Text == "127.0.0.1")
            {
                try
                {
                    //Comprobar puerto escucha
                        ServidorTCP servidorTCP = new ServidorTCP(4444);
                        servidorTCP.Desactivar();

                    //Iniciar servidor
                    System.Diagnostics.ProcessStartInfo servidor = new System.Diagnostics.ProcessStartInfo(System.IO.Path.Combine(Application.StartupPath, "Servidor.exe"));
#if DEBUG
                    servidor.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
#else
                    servidor.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
#endif
                    procesoServidor=System.Diagnostics.Process.Start(servidor);
                    ServidorActivo = true;
                    System.Windows.Forms.Application.DoEvents();
                }
                catch { ServidorActivo = true; }
            }
            try
            {
                Cliente cliente = new Cliente(nombreJugador.Text.Replace("'", "\""), host.Text, Programa.IdCliente);
                cliente.ClienteTcp.ControlInvoke = vPrincipal;

                cliente.Color = color.BackColor;

                clientes.Add(cliente);
                cliente.ClienteTcp.DatosRecibidos += ComandoRecibido;
                cliente.Conectar(Programa.IdCliente, System.InformacionPrograma.VersionActual);
            }
            catch (System.Net.Sockets.SocketException error)
            {
                MessageBox.Show("Error al conectar al servidor: " + error.Message + "\r\nCodigo de error: " + error.ErrorCode, "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class ItemLista
        {
            public string Text;
            public Color Color;
            public Cliente Tag;
            public string SubText;
        }

        void JugadorConectado(Cliente nuevoCliente)
        {
            ItemLista item = new ItemLista();
            item.Text = nuevoCliente.Nombre;
            item.Color = nuevoCliente.Color;
            item.Tag = nuevoCliente;
            listaJugadores.Items.Add(item);
            if (nuevoCliente.AdministradorServidor)
            {
                item.SubText="  (Administrador)";
               //  item.ToolTipText = "Este jugador es el administrador de la partida";
            }
            SemillaMapa(null, null);//Actualizar el mapa
        }

        private void ExpulsarJugador(object sender, EventArgs e)
        {
            if (listaJugadores.SelectedItems.Count == 0)
                return;

            ItemLista item = (ItemLista)listaJugadores.SelectedItems[0];

            if (clientes.Count > 0)
                clientes[0].ClienteTcp.EnviarComando(304, string.Format("Expulsar al jugador '{0}'", ((Cliente)item.Tag).Nombre));
        }

        private void ComenzarPartida(object sender, EventArgs e)
        {
                if (clientes.Count > 0)
                    clientes[0].ClienteTcp.EnviarComando(301, "Iniciar partida");
        }

        private void NuevaPartida_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                try
                {
                    foreach (Cliente cliente in clientes)
                    {
                        if(cliente.AdministradorServidor)
                            cliente.ClienteTcp.EnviarComando(302, "Salir del servidor");

                        cliente.ClienteTcp.Desconectar();
                    }
                    clientes.Clear();

                    System.GC.Collect();
                }
                catch { }
            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            ItemLista item = (ItemLista)listaJugadores.Items[e.Index];

            e.DrawBackground();

            string texto;
            if (string.IsNullOrEmpty(item.SubText))
                texto = item.Text;
            else
                texto = item.Text + " " + item.SubText;

            StringFormat format = StringFormat.GenericDefault;
            format.LineAlignment = StringAlignment.Center;

            e.Graphics.DrawString(texto, this.Font, new SolidBrush(item.Color), e.Bounds, format);

            e.DrawFocusRectangle();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (mapa.Jugadores.Count < 2)
            {
                semillaMapa.Value = new Random().Next();
                return;
            }
            Mapa mapaTemporal = new Mapa();
            mapaTemporal.ModoGrafico = false;
            System.Random rand = new Random();

            List<double> distancias = new List<double>(this.mapa.Jugadores.Count);
            List<int> cercanos = new List<int>(this.mapa.Jugadores.Count);
            int contador = 0;
            while (true)
            {
                distancias.Clear();
                cercanos.Clear();
                mapaTemporal.Inicializar(rand.Next(), this.mapa.Jugadores, (int)neutrales.Value);

                foreach (Cliente jugador in this.mapa.Jugadores)
                {
                    int cercano = 0;
                    double distanciaMedia = 0;

                    foreach (Planeta planetaJugador in jugador.ObtenerPlanetas(mapaTemporal))
                    {
                        double distanciasMediaPlaneta = 0;
                        foreach (Planeta planeta in mapaTemporal.Planetas)
                        {
                            double distancia = Cliente.CalcularDistancia(planetaJugador, planeta);

                            if (distancia <=1.5)
                                cercano++;
                            distanciasMediaPlaneta += distancia;
                        }

                        distanciaMedia += distanciasMediaPlaneta / mapaTemporal.Planetas.Count;                      
                    }
                    distancias.Add(distanciaMedia);
                    cercanos.Add(cercano);
                }

                double anterior=-1;
                bool mapaValido=true;
                foreach (double distancia in distancias)
                {
                    if (anterior != -1 && Math.Abs(anterior - distancia) > 0.3)
                    {
                        mapaValido = false;
                        break;
                    }
                    anterior = distancia;
                }
                anterior = -1;
                foreach (int cercano in cercanos)
                {
                    if (anterior != -1 && Math.Abs(anterior - cercano)>1)
                    {
                        mapaValido = false;
                        break;
                    }
                    anterior = cercano;
                }

                if (mapaValido || contador>1000)
                {
                    semillaMapa.Value = mapaTemporal.Semilla;
                    return;
                }

                contador++;
            }
        }
    }
}
