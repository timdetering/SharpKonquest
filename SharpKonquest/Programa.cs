using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace SharpKonquest
{
    static class Programa
    {
        public static int IdCliente = new Random().Next(0, int.MaxValue);


        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if MONO
            InformacionPrograma.Mono = true;
#endif

            if (InformacionPrograma.Mono == false)
            {
                Error.ActivarAnalisis("SharpKonquest", InformacionPrograma.VersionActual,
                    InformacionPrograma.MailSoporte,
                    Icon.ExtractAssociatedIcon(Application.ExecutablePath).ToBitmap());
            }

            Application.Run(new Principal());
        }

        public static Image ObtenerImagenIncrustada(string fileName)
        {
            try
            {
                string ruta = Path.Combine(Application.StartupPath, "Recursos");
                return Image.FromFile(Path.Combine(ruta, fileName + ".png"));
            }
            catch
            {
                try
                {
                    ResourceManager recursos =
                        new ResourceManager("SharpKonquest.Properties.Recursos",
                            Assembly.GetExecutingAssembly());

                    return (Image)recursos.GetObject(fileName);
                }
                catch
                {

                    try
                    {
                        ResourceManager recursos =
                            new ResourceManager("SharpKonquest.Recursos",
                                Assembly.GetExecutingAssembly());

                        return (Image)recursos.GetObject(fileName);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }
    }
}
