using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace System
{
    class Programa
    {
        public static AutoResetEvent EstadoEspera = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Console.WriteLine("Iniciando servidor...");
           new SharpKonquest.Servidor();
            EstadoEspera.WaitOne();
            Environment.Exit(0);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.Write(e.ExceptionObject.ToString());
            Console.ReadLine();
            Environment.Exit(1);
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Console.Write(e.Exception.ToString());
            Console.ReadLine();
            Environment.Exit(1);
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
    }
}
