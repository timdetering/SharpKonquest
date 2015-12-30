using System;
using System.Collections.Generic;
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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Principal());
        }
    }
}