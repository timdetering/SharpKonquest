using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace System
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
                Error.ActivarAnalisis("SharpKonquest", System.InformacionPrograma.VersionActual, System.InformacionPrograma.MailSoporte,
    System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath).ToBitmap());
            }

            Application.Run(new SharpKonquest.Principal());
        }

        public static System.Drawing.Image ObtenerImagenIncrustada(string nombre)
        {
            try
            {
                string ruta = System.IO.Path.Combine(Application.StartupPath, "Recursos");
                return System.Drawing.Image.FromFile(System.IO.Path.Combine(ruta, nombre + ".png"));
            }
            catch
            {
                try
                {
                    System.Resources.ResourceManager recursos =
                         new System.Resources.ResourceManager("SharpKonquest.Properties.Recursos", System.Reflection.Assembly.GetExecutingAssembly());

                    return (System.Drawing.Image)recursos.GetObject(nombre);
                }
                catch
                {

                    try
                    {
                        System.Resources.ResourceManager recursos =
                    new System.Resources.ResourceManager("SharpKonquest.Recursos", System.Reflection.Assembly.GetExecutingAssembly());

                        return (System.Drawing.Image)recursos.GetObject(nombre);
                    }
                    catch { return null; }
                }
            }
        }
    }
}
