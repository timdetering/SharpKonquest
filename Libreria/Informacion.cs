using System;
using System.Collections.Generic;
using System.Text;

namespace SharpKonquest
{
    public class InformacionPrograma
    {
        public static string MailSoporte = "administradorerrrores@gmail.com";
        public static string VersionActual = "2.1.0.0";
        public static string[] UrlActualizacion
        {
            get
            {
                return new string[] {
                "http://sharpkonquest.googlepages.com/actualizacion.xml",
                "http://thauglor.no-ip.info/SharpKonquest/actualizacion.xml"
            };
            }
        }

        /// <summary>
        /// Indica si se esta ejecutando el programa desde Mono
        /// </summary>
        public static bool Mono;

        static InformacionPrograma()
        {
#if MONO
        	Mono = true;
#else
            Mono = false;
#endif
        }
    }
}