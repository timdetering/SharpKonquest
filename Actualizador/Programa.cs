using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Actualizador
{
    static class Programa
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] arg)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (arg.Length > 0)
            {
                //Arg 0 - url del archivo 
                //Arg 1 - carpeta de instalacion
                //Arg 2 - nombre del programa
                //Arg 3 - ejecutable iniciado al acabar
                //Arg 4 - parametros el ejecutable iniciado
                Actualizacion act = new Actualizacion();
                act.Actualizar(arg[0], arg[1], arg[2]);

                MessageBox.Show("El programa se ha actualizado con éxito", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Iniciar el programa al acabar
                try
                {
                    if (string.IsNullOrEmpty(arg[3]) == false)
                    {
                        string archivo;
                        if (Path.IsPathRooted(arg[3]))//Ruta absoluta
                            archivo = arg[3];
                        else
                            archivo = Path.Combine(arg[1], arg[3]);

                        if (File.Exists(archivo))
                        {
                            if (arg.Length > 3)
                                System.Diagnostics.Process.Start(archivo, arg[4]);
                            else
                                System.Diagnostics.Process.Start(archivo);
                        }
                    }
                }
                catch { }
            }
        }
    }
}