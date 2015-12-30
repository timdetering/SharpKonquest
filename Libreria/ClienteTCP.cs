using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SharpKonquest.Clases
{
    public delegate void DelegadoComandoRecibido(int comando, string[] parametros, string cadena, ClienteTCP clienteTcp);
     
    /// <summary>
    /// Administra un objecto TcpClient para enviar y recibir comandos de una sola línea a traves de la conexión
    /// </summary>
   public class ClienteTCP
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

        public ClienteTCP(string host, int puerto,string nombre)
        {
            this.TcpClient = new TcpClient();
            this.TcpClient.Connect(host, puerto);
            Nombre = nombre;

            Instancias.Add(this);
            if (sub == null || sub.IsAlive==false)
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

            Instancias.Add(this);
            if (sub == null || sub.IsAlive == false)
            {
                sub = new System.Threading.Thread(new System.Threading.ThreadStart(ComprobarDatosRecibidos));
                sub.Name = "ComprobarDatosTCP";
                sub.Start();
            }
        }

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
                       if (cliente.TcpClient.Available > 0 && cliente.DatosRecibidos!=null)
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

       private static void DatosDisponibles(object arg)
       {
           ClienteTCP cliente = (ClienteTCP)arg;
           if (cliente.DatosRecibidos != null)
           {
               byte[] buffer = new byte[cliente.TcpClient.Available];
               NetworkStream stream = cliente.TcpClient.GetStream();

               stream.Read(buffer, 0, buffer.Length);

               string cadenaDatos = Encoding.UTF8.GetString(buffer).Trim();

               foreach (string cadena in cadenaDatos.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
               {
                   try
                   {
                       int comando = int.Parse(cadena.Trim().Substring(0, cadena.IndexOf(' ')));
                       string[] parametros = ObtenerSubCadenas(cadena);

                       //Llamar al evento

                       if (cliente.ControlInvoke != null)
                       {
                           foreach (Delegate delegado in cliente.DatosRecibidos.GetInvocationList())
                           {
                               cliente.ControlInvoke.Invoke(delegado, comando, parametros, cadena, cliente);
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
                   catch (Exception e) { Console.WriteLine("Error: " + e.ToString()); }
               }
           }
       }

        private static Regex SubCadenas = new Regex(
    @"'((?:.|\s)*?)'",
    RegexOptions.IgnoreCase
    | RegexOptions.CultureInvariant
    | RegexOptions.IgnorePatternWhitespace
    | RegexOptions.Compiled
    );

        internal static string[] ObtenerSubCadenas(string cadena)
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
        public void EnviarDatos(int comando, string datos)
        {
            if (TcpClient.Connected == false)
                return;

            string texto = comando + " " + datos + "\r\n";
            if (TcpClient != null)
            {
                NetworkStream stream = TcpClient.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(texto);

                stream.Write(data, 0, data.Length);

                System.Windows.Forms.Application.DoEvents();
            }
        }

        /// <summary>
        /// Desactiva este cliente
        /// </summary>
        public void Desconectar()
        {
            EnviarDatos(11, "Desconectar");
            this.TcpClient.Client.Close();
            Instancias.Remove(this);
            if (Instancias.Count == 0)
                sub.Abort();
        }
    }
}
