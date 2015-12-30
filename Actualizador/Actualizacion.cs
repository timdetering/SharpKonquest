using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;

namespace Actualizador
{
    public class Actualizacion
    {
        public static event DelegadoActualizacionDisponible ActualizacionDisponible;
        public delegate void DelegadoActualizacionDisponible(string nuevaVersion, string urlArchivoActualizador);

        public static void ComprobarActualizacion(string versionActual, bool avisar, params string[] url)
        {
            System.Threading.Thread subProceso = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(comprobarActualizacion));
            subProceso.Start(new object[] { url, versionActual,avisar });
        }

        private static void comprobarActualizacion(object arg)
        {
            string[] urls = (string[])((object[])arg)[0];
            string versionActual = ((object[])arg)[1].ToString();
            bool  aviso= (bool)((object[])arg)[2];

            foreach (string url in urls)
            {
                try
                {
                    System.Net.WebClient cliente = new System.Net.WebClient();

                    System.IO.Stream myStream = cliente.OpenRead(url);
                    System.IO.BinaryReader lector = new BinaryReader(myStream);

                    string versionDisponible = lector.ReadString();

                    if (CompararVersiones(versionDisponible, versionActual) > 0)
                    {
                        ActualizacionDisponible(versionDisponible, url);
                        lector.Close();
                        return;
                    }

                    lector.Close();
                }
                catch { }
            }
            if (aviso)
            {
                MessageBox.Show("No hay ninguna nueva versión del programa disponible.", "Ya tienes la última versión",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static int CompararVersiones(string a, string b)
        {
            try
            {
                if (a == b)
                    return 0;
                string[] partesA = a.Split('.');
                string[] partesB = b.Split('.');

                int contador = 0;
                foreach (string parte in partesA)
                {
                    string valorParteA = parte;
                    string valorParteB;
                    if (contador >= partesB.Length)
                        valorParteB = string.Empty;
                    else
                        valorParteB = partesB[contador];

                    if (string.Compare(valorParteA, valorParteB) > 0)
                        return 1;
                    else if (string.Compare(valorParteA, valorParteB) < 0)
                        return -1;
                    contador++;
                }
                return 0;
            }
            catch { return 1; }
        }

        public void Actualizar(string url, string carpetaInstalacion, string nombrePrograma)
        {
            try
            {
                /* Formato archivos de actualizacion
              * Cadena de version
              * Numero de archivos
              *      Cadena de nombre de archivo
              *      Cadena de carpeta de destino de archivo
              *      Longitud del archivo (long)
              *      Array de bytes con el contenido
              */
                ventanaProgreso = new Progreso();
                ventanaProgreso.Show();

                System.Net.WebClient cliente = new System.Net.WebClient();

                System.IO.Stream myStream = cliente.OpenRead(url);
                System.IO.BinaryReader lector = new BinaryReader(myStream);

                string versionDisponible = lector.ReadString().Trim();
                ventanaProgreso.label1.Text = string.Format("Actualizando {0} a la versión {1}", nombrePrograma, versionDisponible);
                lector.Close();

                Application.DoEvents();

                string archivoActualizacion = Path.GetTempFileName();
                DescargarArchivo(url, archivoActualizacion);

                try
                {
                    FileStream archivo = new FileStream(archivoActualizacion, FileMode.Open);
                    lector = new BinaryReader(archivo);
                    lector.ReadString();//Omitir la versión
                    int numeroArchivos = lector.ReadInt32();

                    ventanaProgreso.progressBar1.Value = 0;
                    ventanaProgreso.progressBar1.Maximum = numeroArchivos;
                    for (int contador = 0; contador < numeroArchivos; contador++)
                    {
                        try
                        {
                            string nombreArchivo = lector.ReadString();
                            string carpetaDestino = lector.ReadString();
                            int tamaño = lector.ReadInt32();

                            string rutaDestino;
                            if (carpetaDestino == "\\")
                                rutaDestino = Path.Combine(carpetaInstalacion, nombreArchivo);
                            else if (Path.IsPathRooted(carpetaDestino))
                                rutaDestino = Path.Combine(carpetaDestino, nombreArchivo);
                            else
                                rutaDestino = Path.Combine(Path.Combine(carpetaInstalacion, carpetaDestino), nombreArchivo);

                            if (Directory.Exists(Path.GetDirectoryName(rutaDestino)) == false)
                                Directory.CreateDirectory(Path.GetDirectoryName(rutaDestino));

                            try
                            {
                                ventanaProgreso.label2.Text = string.Format("Actualizando archivo \"{0}\"", nombreArchivo);
                            }
                            catch { }

                           
                            MemoryStream res = new MemoryStream(lector.ReadBytes(tamaño));
                            FileStream destino = new FileStream(rutaDestino, FileMode.Create,FileAccess.Write);

                            LZMA lzma = new LZMA();
                            lzma.Decompress(res, destino);

                            destino.Close();
                            res.Close();

                            Application.DoEvents();
                            ventanaProgreso.progressBar1.Value++;
                        }
                        catch (Exception error) { MessageBox.Show("Error: " + error.Message); }
                    }
                }
                catch { }
                finally
                {
                    lector.Close();
                }
            }
            catch { }
        }

        private void DescargarArchivo(string url, string destino)
        {
            ventanaProgreso.progressBar1.Maximum = 100;
            System.Net.WebClient cliente = new System.Net.WebClient();

            descargado = false;

            cliente.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(ArchivoDescargado);
            cliente.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(DescargandoArchivo);

            if (Directory.Exists(Path.GetDirectoryName(destino)) == false)
                Directory.CreateDirectory(Path.GetDirectoryName(destino));

            Uri urlUri=new Uri(url);
            descargarActual = urlUri.Segments[urlUri.Segments.Length-1];
            cliente.DownloadFileAsync(new Uri(url), destino);

            while (descargado == false)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(20);
            }
        }

        void DescargandoArchivo(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                if (ventanaProgreso != null)
                {
                    ventanaProgreso.label2.Text = string.Format("Descargando archivo \"{0}\": {1}% realizado, descargados {2} bytes de {3}.", descargarActual, e.ProgressPercentage, e.BytesReceived, e.TotalBytesToReceive);
                    ventanaProgreso.progressBar1.Value = e.ProgressPercentage;
                }
            }
            catch { }
        }

        void ArchivoDescargado(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            descargado = true;
        }
        private Progreso ventanaProgreso;
        private bool descargado;
        private string descargarActual = String.Empty;
    }
}