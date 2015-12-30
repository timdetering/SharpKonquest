using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.TCP;

namespace SharpKonquest.Clases
{
    public class Cliente
    {
        public ClienteTCP ClienteTcp;
        public string Nombre;
        public Color Color;
        public bool Derrotado = false;
        public bool PoseePlaneta = true;
        public int IdCliente;
        /// <summary>
        /// Solo válida en el servidor
        /// </summary>
        public bool AdministradorServidor = false;
        /// <summary>
        /// Flotas en movimiento del jugador actual
        /// </summary>
        public List<Flota> Flotas = new List<Flota>();

        public override string ToString()
        {
            return Nombre;
        }

        public Cliente(string nombre, string hostServidor,int IdCliente)
        {
            if (hostServidor != null)//Conectar
            {
                ClienteTcp = new ClienteTCP(hostServidor, 4444,"Cliente");
            }
            Nombre = nombre;
            this.IdCliente = IdCliente;
        }

        /// <summary>
        /// Obtiene todos los planetas de un jugador dentro de un mapa
        /// </summary>
        public List<Planeta> ObtenerPlanetas(Mapa mapa)
        {
            List<Planeta> res = new List<Planeta>();
            foreach (Planeta p in mapa.Planetas)
            {
                if (p.Dueño == this)
                    res.Add(p);
            }
            return res;
        }

        /// <summary>
        /// Conecta con el servidor
        /// </summary>
        public void Conectar(int IdCliente,string versionCliente)
        {
            ClienteTcp.EnviarComando(10,string.Format("Hola, mi ID de cliente es '{0}' y la versión es '{1}'. Mi nombres es '{2}', mi color es '{3}'",
                IdCliente, versionCliente, Nombre, Color.ToArgb()));
        }

        /// <summary>
        /// Acaba el turno de este jugador
        /// </summary>
        public void FinTurno()
        {
            ClienteTcp.EnviarComando(203, "He acabado el turno");
        }


        /// <summary>
        /// Envia una flota entre dos planetas
        /// </summary>
        public void EnviarFlota(Planeta origen, Planeta destino, int naves, int rondaActual)
        {
            if (naves != 0)
            {
                if (origen.Naves < naves)
                    naves = origen.Naves;

                origen.Naves -= naves;
                Flota flota = new Flota();
                flota.Destino = destino;
                flota.TecnologiaMilitar = origen.TecnologiaMilitar;
                flota.Origen = origen;
                flota.Naves = naves;

                flota.Distancia = Cliente.CalcularDistancia(origen, destino);
                flota.RondaSalida = rondaActual;
                flota.RondaLlegada = (rondaActual + (int)Math.Round(flota.Distancia));

                origen.Dueño.Flotas.Add(flota);

                origen.Dueño.ClienteTcp.EnviarComando(205,
                    string.Format("He enviado '{0}' naves desde '{1}' hasta '{2}'. La distancia es de '{3}' y la tecnologia militar es '{4}'",
                    naves, origen.Name, destino.Name, flota.Distancia, flota.TecnologiaMilitar));
            }
        }

        /// <summary>
        /// Distancia entre dos planetas
        /// </summary>
        public static double CalcularDistancia(Planeta p1, Planeta p2)
        {
            double distancia = Math.Sqrt(
       Math.Pow(p1.Columna - p2.Columna, 2) +
        Math.Pow(p1.Fila - p2.Fila, 2));
            distancia = distancia / 2;

            if (distancia < 1)
                distancia = 1;
            return distancia;
        }
    }
}

