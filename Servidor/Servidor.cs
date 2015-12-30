using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Drawing;
using SharpKonquest.Clases;
using System.Threading;

namespace SharpKonquest
{
    class Servidor
    {
        public delegate void DelegadoClienteConectado(Cliente nuevoCliente);

        public Dictionary<ClienteTCP, Cliente> Clientes;
        public ServidorTCP servidorTCP;
        public bool AceptarNuevosJugadores = true;

        /// <summary>
        /// Mapa que representa el estado del juego
        /// </summary>
        public Mapa Mapa;

        public Servidor()
        {
            Clientes = new Dictionary<ClienteTCP, Cliente>();
            try
            {
                servidorTCP = new ServidorTCP(4444);
            }
            catch
            {
                Console.WriteLine("El puerto usado por el servidor ya está en uso. Cierre todas las instancias para poder continuar");
                Console.ReadLine();
                Environment.Exit(1);
            }
            servidorTCP.DatosRecibidos += new DelegadoComandoRecibido(ComandoRecibido);
            Console.WriteLine("------>Esperando conexiones...");
           
            Mapa = new Mapa();
            Mapa.Semilla = new Random().Next();
            Mapa.Neutrales = 10;
        }

        /// <summary>
        /// Envia datos a todos los clientes
        /// </summary>
        public void DifundirMensaje(int comando, string texto)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Servidor (difusión): " + comando + " " + texto);
            Console.ResetColor();


            List<int> IdsClienteEnviadas = new List<int>();
            foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
            {
                if (IdsClienteEnviadas.IndexOf(elemento.Value.IdCliente) == -1)
                {
                    IdsClienteEnviadas.Add(elemento.Value.IdCliente);
                    elemento.Key.EnviarDatos(comando, texto);
                }
            }
        }

        /// <summary>
        /// Envia datos a un cliente
        /// </summary>
        public void EnviarComando(int comando, string texto, ClienteTCP cliente)
        {
            if (Clientes.ContainsKey(cliente))
            {
                Cliente jugador = Clientes[cliente];

                string[] nombres = Enum.GetNames(typeof(ConsoleColor));
                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), nombres[jugador.GetHashCode() % (nombres.Length - 1)]);
                Console.WriteLine("Servidor->" + jugador.Nombre + ": " + comando + " " + texto);
                Console.ResetColor();
            }
            else
                Console.WriteLine("Servidor->Cliente: " + comando + " " + texto);

            cliente.EnviarDatos(comando, texto);
        }

        /// <summary>
        /// Datos recibidos por cualquier cliente
        /// </summary>
        void ComandoRecibido(int comando, string[] parametros, string cadena, ClienteTCP clienteTcp)
        {
            if (Clientes.ContainsKey(clienteTcp))
            {
                try
                {
                    string[] nombres = Enum.GetNames(typeof(ConsoleColor));
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), nombres[Clientes[clienteTcp].GetHashCode() % (nombres.Length - 1)]);
                    if (Console.ForegroundColor == ConsoleColor.Black)
                        Console.ForegroundColor = ConsoleColor.White;

                    if (EsAdministrador(clienteTcp))
                        Console.WriteLine(Clientes[clienteTcp].Nombre + "(*): " + cadena);
                    else
                        Console.WriteLine(Clientes[clienteTcp].Nombre + ": " + cadena);

                    Console.ResetColor();
                }
                catch { }
            }
            else
                Console.WriteLine("Cliente: " + cadena);

            try
            {
                switch (comando)
                {
                    case 3://PING
                        break;

                    case 10://Cliente conectado
                        if (AceptarNuevosJugadores == false)
                        {
                            EnviarComando(102, "Partida ya iniciada", clienteTcp);
                            return;
                        }

                        if (Programa.CompararVersiones(System.Windows.Forms.Application.ProductVersion, parametros[1]) > 0)
                        {
                            //Version cliente antigua
                            EnviarComando(103, "Versión antigua", clienteTcp);
                            return;
                        }

                        Cliente jugador = new Cliente(parametros[2], null, int.Parse(parametros[0]));
                        jugador.Color = Color.FromArgb(int.Parse(parametros[3]));
                        jugador.ClienteTcp = clienteTcp;
                        foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
                        {
                            if (elemento.Value.Nombre == jugador.Nombre)//Nombre repetido
                            {
                                EnviarComando(100, "Nombre repetido", clienteTcp);
                                return;
                            }
                            if (elemento.Value.Color.ToArgb() == jugador.Color.ToArgb())//Color repetido
                            {
                                EnviarComando(101, "Color repetido", clienteTcp);
                                return;
                            }
                        }
                        if (Clientes.Count == 0)//Primer conectado, es el admin
                        {
                            jugador.AdministradorServidor = true;
                            EnviarComando(13, "Eres administrador", clienteTcp);
                        }
                        Clientes.Add(clienteTcp, jugador);

                        //Todo correcto
                        EnviarComando(12, "Login correcto", clienteTcp);

                        ActualizarParametrosPartida();

                        break;
                    case 11://Desconectado
                        if (Clientes.ContainsKey(clienteTcp))
                            Clientes.Remove(clienteTcp);
                        break;
                    case 300://Informacion del mapa
                        if (EsAdministrador(clienteTcp))
                        {
                            Mapa.Semilla = int.Parse(parametros[0]);
                            Mapa.Neutrales = int.Parse(parametros[1]);
                            Mapa.Inicializar(Mapa.Semilla, new List<Cliente>(ObtenerJugadores()), Mapa.Neutrales);
                            ActualizarParametrosPartida();
                        }
                        break;
                    case 301://Iniciar partida
                        if (EsAdministrador(clienteTcp) && Clientes.Count > 1)
                        {
                            Console.WriteLine("------>Partida iniciada");
                            IniciarPartida();
                        }
                        break;
                    case 302://Salir
                        if (EsAdministrador(clienteTcp))
                        {
                            Console.WriteLine("------>Cerrar el servidor");
                            Programa.EstadoEspera.Set();
                        }
                        break;
                    case 303://Segundos limite
                        if (EsAdministrador(clienteTcp))
                            SegundosLimiteTurno = int.Parse(parametros[0]);
                        break;
                    case 304://Expulsar jugador
                        if (EsAdministrador(clienteTcp))
                        {
                            foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
                            {
                                if (elemento.Value.Nombre == parametros[0])
                                {
                                    Clientes.Remove(elemento.Key);
                                    ActualizarParametrosPartida();
                                    EnviarComando(11, "Adios", elemento.Key);
                                    elemento.Key.Desconectar();
                                    break;
                                }
                            }
                        }
                        break;
                    case 400://Chat de administrador
                        if (EsAdministrador(clienteTcp))
                            DifundirMensaje(401, string.Format("El administrador envia el siguiente mensaje: '{0}'", parametros[0]));
                        break;
                    case 402://Chat entre jugadores
                        foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
                        {
                            if (elemento.Value.Nombre == parametros[0])
                            {
                                elemento.Key.EnviarDatos(402, string.Format("El jugador '{0}' envia el mensaje \"'{1}'\" a '{2}'",Clientes[clienteTcp],parametros[1],parametros[0]));
                                break;
                            }
                        }
                        break;
                }
            }
            catch (Exception e) { Console.WriteLine("Error: " + e.ToString()); }
        }

        public bool EsAdministrador(ClienteTCP cliente)
        {
            if (Clientes.ContainsKey(cliente) && Clientes[cliente].AdministradorServidor)
                return true;
            return false;
        }

        /// <summary>
        /// Envia a los clientes parametros de la partida como los jugadores, semilla y neutrales del mapa
        /// </summary>
        private void ActualizarParametrosPartida()
        {
            try
            {
                string listaJugadores = string.Empty;

                foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
                {
                    listaJugadores += "Nombre: '" + elemento.Value.Nombre + "', Color: '" + elemento.Value.Color.ToArgb() + "', Admin: '" + (elemento.Value.AdministradorServidor == true ? "1" : "0") + "'; ";
                }
                if (listaJugadores.EndsWith("; "))
                    listaJugadores = listaJugadores.Substring(0, listaJugadores.Length - 2);

                if (Mapa != null)
                DifundirMensaje(200, string.Format("La semilla del mapa actual es '{0}' con '{1}' neutrales. Los jugadores son: " + listaJugadores, Mapa.Semilla, Mapa.Neutrales));
            }
            catch (Exception e) { Console.WriteLine("Error: " + e.ToString()); }
        }

        /// <summary>
        /// Obtiene la lista de jugadores conectados
        /// </summary>
        public Cliente[] ObtenerJugadores()
        {
            Cliente[] lista = new Cliente[Clientes.Count];
            Clientes.Values.CopyTo(lista, 0);
            return lista;
        }

        #region Partida
        public int SegundosLimiteTurno = -1;
        int ronda = 1;
        /// <summary>
        /// Inicia la partida e indica al primer cliente que es su turno.
        /// </summary>
        public void IniciarPartida()
        {
            DifundirMensaje(201, "Iniciar partida");

            AceptarNuevosJugadores = false;

            Mapa.Inicializar(Mapa.Semilla, new List<Cliente>(ObtenerJugadores()), Mapa.Neutrales);

            System.Threading.Thread.Sleep(250);
            /*
            System.Threading.Thread sub = new System.Threading.Thread(new System.Threading.ThreadStart(ComprobacionConexion));
            sub.Name = "Ping a clientes";
            sub.Start();
             */

            ronda = 1;
            while (true)
            {
                Console.WriteLine("------>Inicio de la ronda " + ronda);
                DifundirMensaje(51, string.Format("Inicio de la ronda '{0}'", ronda));

                foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
                {
                    if (elemento.Value.Derrotado || elemento.Value.PoseePlaneta == false)
                        continue;

                    RondaDeJugador(elemento.Value);
                }

                //Procesar fin de turno
                FinalRonda();

                //Actualizar todos los clientes
                ActualizarEstadoPartida();

                if (ComprobarCondicionesVictoria() == true)//Partida acabada
                    return;

                ronda++;
            }
        }

        /// <summary>
        /// Comprueba que los clientes siguen conectados
        /// </summary>
        private void ComprobacionConexion()
        {
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(20 * 1000);
            System.Windows.Forms.Application.DoEvents();
            foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
            {
                if (elemento.Key.TcpClient.Connected)
                    EnviarComando(2, "PING", elemento.Key);
            }
            ComprobacionConexion();
        }

        private void RondaDeJugador(Cliente jugador)
        {
            jugador.ClienteTcp.DatosRecibidos += DatosJugadorActualRecibidos;

          timer = new Timer(new TimerCallback(Temporizador), jugador,0, 10000);

            EsperandoFinTurno = new AutoResetEvent(false);
            EsperandoFinTurno.WaitOne();

            jugador.ClienteTcp.DatosRecibidos -= DatosJugadorActualRecibidos;
        }
        private AutoResetEvent EsperandoFinTurno = new AutoResetEvent(false);
        private System.Threading.Timer timer;


        /// <summary>
        /// Envia periodicamente el comando de incio de turno
        /// </summary>
        void Temporizador(object objeto)
        {
            Cliente jugador = (Cliente)objeto;

            if (SegundosLimiteTurno > 0)
                DifundirMensaje(52, string.Format("Inicio del turno del jugador '{0}' de color '{1}'. Tiene '{2}' segundos para acabar.", jugador.Nombre, jugador.Color.ToArgb(), SegundosLimiteTurno));
            else
                DifundirMensaje(52, string.Format("Inicio del turno del jugador '{0}' de color '{1}'", jugador.Nombre, jugador.Color.ToArgb()));
        }

        /// <summary>
        /// Datos recibidos del jugador que juega el turno actual
        /// </summary>
        void DatosJugadorActualRecibidos(int comando, string[] subCadenas, string cadena, ClienteTCP cliente)
        {
            switch (comando)
            {
                case 53://Inicio de ronda recibido correctamente
                    if(timer!=null)
                    timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                    break;
                case 203: //Fin de turno
                    EnviarComando(1, "OK", cliente);
                    EsperandoFinTurno.Set();
                    break;

                case 205://Envio de ataque (naves,origen,destino)
                    Flota flota = new Flota();
                    flota.Origen = Mapa.ObtenerPlaneta(subCadenas[1]);
                    flota.Destino = Mapa.ObtenerPlaneta(subCadenas[2]);
                    flota.TecnologiaMilitar = flota.Origen.TecnologiaMilitar;
                    flota.Naves = int.Parse(subCadenas[0]);
                    if (flota.Naves > flota.Origen.Naves)
                        flota.Naves = flota.Origen.Naves;
                    flota.Origen.Naves -= flota.Naves;

                    flota.Distancia = Cliente.CalcularDistancia(flota.Origen, flota.Destino);
                    flota.RondaSalida = ronda;
                    flota.RondaLlegada = (ronda + (int)Math.Truncate(flota.Distancia));
                    Clientes[cliente].Flotas.Add(flota);

                    EnviarComando(1, "OK", cliente);
                    break;
            }
        }

        #region Final de ronda

        /// <summary>
        /// Procesa la produccion, batallas, movimientos de naves, etc.
        /// </summary>
        private void FinalRonda()
        {
            foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
            {
                Cliente jugador = elemento.Value;

                //Llegadas de flota
                ProcesarLlegadasFlota(jugador);

                foreach (Planeta p in jugador.ObtenerPlanetas(Mapa))
                {
                    p.Naves += p.Produccion;
                }
            }
        }

        private void ProcesarLlegadasFlota(Cliente jugador)
        {
            foreach (Flota flota in jugador.Flotas)
            {
                if (flota.RondaLlegada == ronda)
                {
                    if (flota.Destino.Dueño != null && flota.Destino.Dueño.Nombre == jugador.Nombre)
                    {
                        flota.Destino.Naves += flota.Naves;
                        DifundirMensaje(70, string.Format("Han llegado refuerzos para el planeta '{0}'. Cantidad: '{1}' naves",
                            flota.Destino.Name, flota.Naves));
                    }
                    else//Batalla
                    {
                        ResultadoBatalla(jugador, flota);
                    }
                    jugador.Flotas.Remove(flota);
                    ProcesarLlegadasFlota(jugador);
                    return;
                }
            }
        }

        private void ResultadoBatalla(Cliente jugador, Flota flota)
        {
            Batalla res = Batalla.SimularBatalla(flota.Naves, flota.TecnologiaMilitar, flota.Destino.Naves, flota.Destino.TecnologiaMilitar);

            DifundirMensaje(71, string.Format("La batalla en el planeta '{0}' entre '{1}' y '{2}' ha terminado en '{3}'. Naves iniciales del atacante: '{4}'. Naves iniciales del defensor: '{5}'. Naves restantes del atacante: '{6}'. Naves restantes del defensor: '{7}'.",
     flota.Destino.Name, jugador.Nombre,
     flota.Destino.Dueño == null ? string.Empty : flota.Destino.Dueño.Nombre,
     (int)res.Resultado, flota.Naves, flota.Destino.Naves, res.RestanteAtacante, res.RestanteDefensor));

            if (res.Resultado == Batalla.ResultadoBatalla.Empate)
            {
                flota.Destino.Naves = res.RestanteDefensor;

                //Enviar las naves restantes a casa
                if (res.RestanteAtacante > 0)
                {
                    Flota retorno = new Flota();
                    retorno.Origen = flota.Destino;
                    retorno.Destino = flota.Origen;
                    retorno.TecnologiaMilitar = flota.TecnologiaMilitar;
                    retorno.Naves = res.RestanteAtacante;

                    retorno.Distancia = Cliente.CalcularDistancia(retorno.Origen, retorno.Destino);
                    retorno.RondaSalida = ronda;
                    retorno.RondaLlegada = (ronda + (int)Math.Truncate(retorno.Distancia));
                    jugador.Flotas.Add(retorno);
                }
            }
            else if (res.Resultado == Batalla.ResultadoBatalla.GanaDefensor)//Defendido
            {
                flota.Destino.Naves = res.RestanteDefensor;
            }
            else//Gana el atacante
            {
                flota.Destino.Naves = res.RestanteAtacante;
                flota.Destino.Dueño = jugador;
            }

            if (flota.Destino.Naves == 0)
                flota.Destino.Naves = 1;
        }

        private bool ComprobarCondicionesVictoria()
        {
            int jugadoresActivos = 0;
            foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
            {
                Cliente jugador = elemento.Value;
                if (jugador.Derrotado == false)
                {
                    jugadoresActivos++;
                    if (jugador.Flotas.Count == 0 && jugador.ObtenerPlanetas(Mapa).Count == 0)
                    {
                        DifundirMensaje(61, string.Format("El jugador '{0}' ha sido eliminado", jugador.Nombre));

                        jugador.Derrotado = true;
                      
                        jugadoresActivos--;
                    }
                    else if (jugador.ObtenerPlanetas(Mapa).Count == 0)
                    {
                        DifundirMensaje(62, string.Format("El jugador '{0}' no tiene ningún planeta", jugador.Nombre));
                        jugador.PoseePlaneta = false;
                    }
                }
            }
            if (jugadoresActivos == 1)
            {
                foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
                {
                    Cliente jugador = elemento.Value;
                    DifundirMensaje(60, string.Format("El jugador '{0}' ha ganado", elemento.Value.Nombre));

                    return true;
                }
            }
            return false;
        }

        #endregion

        /// <summary>
        /// Actualiza el estado de todos los clientes, planetas y flotas
        /// </summary>
        private void ActualizarEstadoPartida()
        {
            string mensaje = "Datos de los planetas: ";

            foreach (System.Collections.Generic.KeyValuePair<string, Planeta> elemento in Mapa.Planetas)
            {
                Planeta planeta = elemento.Value;
                mensaje += string.Format("(Planeta: '{0}', naves: '{1}', dueño: '{2}'); ", planeta.Name, planeta.Naves, planeta.Dueño == null ? string.Empty : planeta.Dueño.Nombre);
            }

            if (mensaje.EndsWith(", "))
                mensaje = mensaje.Substring(0, mensaje.Length - 2);

            DifundirMensaje(206, mensaje);

            mensaje = "Datos de las flotas: ";

            foreach (System.Collections.Generic.KeyValuePair<ClienteTCP, Cliente> elemento in Clientes)
            {
                Cliente jugador = elemento.Value;
                foreach (Flota flota in jugador.Flotas)
                {
                    mensaje += string.Format("(Jugador: '{0}'; Naves: '{1}'; Tecnologia: '{2}'; Salida: '{3}'; Llegada: '{4}'; Origen: '{5}'; Destino: '{6}'), ",
                        jugador.Nombre, flota.Naves, flota.TecnologiaMilitar, flota.RondaSalida, flota.RondaLlegada, flota.Origen.Name, flota.Destino.Name);
                }
            }

            if (mensaje.EndsWith(", "))
                mensaje = mensaje.Substring(0, mensaje.Length - 2);

            DifundirMensaje(207, mensaje);
        }

        #endregion
    }
}