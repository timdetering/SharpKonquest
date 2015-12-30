using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SharpKonquest.Clases
{
   public class ServidorTCP
    {
        public event DelegadoComandoRecibido DatosRecibidos;
        public event DelegadoComandoRecibido ClienteConectado;

        private System.Net.Sockets.TcpListener listener;
        /// <summary>
        /// Clientes conectados
        /// </summary>
        public List<ClienteTCP> Clientes
        {
            get { return clientes; }
        }
        private List<ClienteTCP> clientes;

        /// <summary>
        /// Indica si los clientes conectados son aceptados o rechazados
        /// </summary>
        public bool AceptarNuevasConexiones = true;

        /// <summary>
        /// Control usado para invocar los delegados de eventos
        /// </summary>
        public Control ControlInvoke;

        public ServidorTCP(int puerto)
        {
            listener = new System.Net.Sockets.TcpListener(IPAddress.Any,puerto);

            //Esperar peticiones de conexión
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(ComprobarClienteConectado), null);
        }

        /// <summary>
        /// Comprueba si hay nuevos clientes para conectar
        /// </summary>
        private void ComprobarClienteConectado(IAsyncResult ar)
        {
            if (clientes == null)
            {
                clientes = new List<ClienteTCP>();
            }

            if (AceptarNuevasConexiones)
            {
                try
                {
                    ClienteTCP nuevoCliente = new ClienteTCP(listener.EndAcceptTcpClient(ar),"Servidor");
                    clientes.Add(nuevoCliente);

                    nuevoCliente.DatosRecibidos +=new DelegadoComandoRecibido(DatosClienteRecibidos);
                    //Esperar más clientes
                    listener.BeginAcceptTcpClient(new AsyncCallback(ComprobarClienteConectado), null);

                    //Invocar el evento
                    InvocarEvento(ClienteConectado, ControlInvoke, nuevoCliente, null);
                }
                catch { }
            }
        }

        internal static void InvocarEvento(Delegate evento,Control ControlInvoke,params object[] args)
        {
            if (evento != null)
            {
                if (ControlInvoke != null)
                {
                    foreach (Delegate delegado in evento.GetInvocationList())
                    {
                        ControlInvoke.Invoke(delegado, args);
                    }
                }
                else
                    evento.DynamicInvoke(args);
            }
        }

        /// <summary>
        /// Envia datos a todos los clientes
        /// </summary>
        /// <param name="buffer"></param>
        public void EnviarDatos(int comando,string texto)
        {
            if (clientes != null)
            {
                //Enviar solo una vez por IP
                foreach (ClienteTCP cliente in clientes)
                {
                    try
                    {
                            cliente.EnviarDatos(comando, texto);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Datos de cualquier cliente recibidos
        /// </summary>
        void DatosClienteRecibidos(int comando, string[] parametros, string cadena, ClienteTCP cliente)
        {
            if (DatosRecibidos != null)
                InvocarEvento(DatosRecibidos, this.ControlInvoke, comando,parametros,cadena,cliente);
        }

        /// <summary>
        /// Desconecta el servidor
        /// </summary>
        public void Desactivar()
        {
            this.listener.Stop();
        }
    }
}
