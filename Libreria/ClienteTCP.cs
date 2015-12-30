using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;

namespace System.TCP
{
    public delegate void DelegadoComandoRecibido(ushort comando, string[] parametros, string cadena, ClienteTCP clienteTcp);

    /// <summary>
    /// Administra un objecto TcpClient para enviar y recibir comandos de una sola línea a traves de la conexión
    /// </summary>
    public class ClienteTCP:IDisposable
    {
        public event DelegadoComandoRecibido DatosRecibidos;
        public TcpClient TcpClient;

        /// <summary>
        /// Control usado para invocar los delegados de eventos
        /// </summary>
        public Control ControlInvoke;
        /// <summary>
        /// Nombre que identifica este cliente TCP
        /// </summary>
        public string Nombre;

        public ClienteTCP(string host, int puerto, string nombre)
        {
            this.TcpClient = new TcpClient();
            this.TcpClient.Connect(host, puerto);
            Nombre = nombre;
            Comprimir = true;

            Instancias.Add(this);
            if (sub == null || sub.IsAlive == false)
            {
                sub = new System.Threading.Thread(new System.Threading.ThreadStart(ComprobarDatosRecibidos));
                sub.Name = "ComprobarDatosTCP";
                sub.Start();
            }
        }

        public ClienteTCP(TcpClient cliente, string nombre)
        {
            this.TcpClient = cliente;
            Nombre = nombre;
            Comprimir = true;

            Instancias.Add(this);
            if (sub == null || sub.IsAlive == false)
            {
                sub = new System.Threading.Thread(new System.Threading.ThreadStart(ComprobarDatosRecibidos));
                sub.Name = "ComprobarDatosTCP";
                sub.Start();
            }
        }

        /// <summary>
        /// Instancias abiertas de clientes tcp
        /// </summary>
        private static List<ClienteTCP> Instancias = new List<ClienteTCP>();
        private static System.Threading.Thread sub;

        /// <summary>
        /// Comprueba si se han recibido datos del servidor
        /// </summary>
        private static void ComprobarDatosRecibidos()
        {
            while (true)
            {
                try
                {
                    System.Threading.Thread.Sleep(10);
                    System.Windows.Forms.Application.DoEvents();

                    foreach (ClienteTCP cliente in Instancias)
                    {
                        if (cliente.TcpClient.Connected == false)
                        {
                            Instancias.Remove(cliente);
                            break;
                        }
                        if (cliente.TcpClient.Available > 0 && cliente.DatosRecibidos != null)
                        {
                            System.Threading.Thread subproceso = new System.Threading.Thread(
                                new System.Threading.ParameterizedThreadStart(DatosDisponibles));
                            subproceso.Name = "EventoDatosDisponibles";
                            subproceso.Start(cliente);
                            Application.DoEvents();
                        }
                    }
                }
                catch { }
            }
        }

        private static Regex SubCadenas = new Regex(
    @"'((?:.|\s)*?)'",
    RegexOptions.IgnoreCase
    | RegexOptions.CultureInvariant
    | RegexOptions.IgnorePatternWhitespace
    | RegexOptions.Compiled
    );

        public static string[] ObtenerSubCadenas(string cadena)
        {

            List<string> res = new List<string>();
            foreach (Match match in SubCadenas.Matches(cadena))
            {
                res.Add(match.Groups[1].Value);
            }
            return res.ToArray();
        }

        /// <summary>
        /// Envia datos al servidor
        /// </summary>
        public void EnviarComando(ushort comando, string datos)
        {
            if (TcpClient.Connected == false)
                return;

            byte[] array = Encoding.UTF8.GetBytes(datos);

            bool comprimido = false;
            if (Comprimir)
            {
               byte[] arrayComprimido = LZMA.Compress(array);
               if (arrayComprimido.Length < array.Length)
               {
                   comprimido = true;
                   array = arrayComprimido;
               }
            }

            TcpClient.SendBufferSize = array.Length + 15;

            if (TcpClient != null)
            {
                NetworkStream stream = TcpClient.GetStream();
                BinaryWriter escritor = new BinaryWriter(stream);

                escritor.Write(comprimido);
                escritor.Write((UInt16)comando);
                escritor.Write((Int32)array.Length);
                escritor.Write(array);

                escritor.Flush();

                System.Windows.Forms.Application.DoEvents();
            }
        }

        public bool Comprimir;

        /// <summary>
        /// Se llama cuando existen datos disponibles para un cliente
        /// </summary>
        private static void DatosDisponibles(object arg)
        {
            ClienteTCP cliente = (ClienteTCP)arg;
            if (cliente.DatosRecibidos != null)
            {
                NetworkStream stream = cliente.TcpClient.GetStream();
                BinaryReader lector = new BinaryReader(stream);

                while (true)
                {
                    try
                    {
                        if (cliente.TcpClient.Available > 0)
                        {
                            bool comprimido = lector.ReadBoolean();
                            ushort comando = lector.ReadUInt16();
                            int longitudArray = lector.ReadInt32();
                            byte[] datos = lector.ReadBytes((int)longitudArray);

                            if (comprimido)
                                datos = LZMA.Decompress(datos);

                            string cadena = Encoding.UTF8.GetString(datos);

                            string[] parametros = ObtenerSubCadenas(cadena);

                            //Llamar al evento
                            if (cliente.ControlInvoke != null)
                            {
                                foreach (Delegate delegado in cliente.DatosRecibidos.GetInvocationList())
                                {
                                    cliente.ControlInvoke.Invoke(delegado, new object[] { comando, parametros, cadena, cliente });
                                    Application.DoEvents();
                                    if (cliente.DatosRecibidos == null)
                                        break;
                                }
                            }
                            else
                            {
                                cliente.DatosRecibidos(comando, parametros, cadena, cliente);
                                Application.DoEvents();
                                if (cliente.DatosRecibidos == null)
                                    break;
                            }
                        }
                        else
                            break;
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Desactiva este cliente
        /// </summary>
        public void Desconectar()
        {
            this.TcpClient.Client.Close();
            Instancias.Remove(this);
            if (Instancias.Count == 0)
                sub.Abort();
        }



        #region Miembros de IDisposable

        public void Dispose()
        {
            this.Desconectar();
        }

        #endregion
    }
}
