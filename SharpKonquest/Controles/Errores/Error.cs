using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.aTxLiB.Mail;

namespace System
{
    public partial class Error : Form
    {
        public Error()
        {
            InitializeComponent();
        }

        #region Propiedades

        public static string NombrePrograma;
        public static string Version;
        public static string Destino;
        public static Image LogoPrograma;
        public static bool IntentarOmitirExcepciones;
        public static bool EnvioAutomatico;

        public static string txtTextoVentana = "%NombrePrograma%";
        public static string txtCabecera = "Error de ejecución en %NombrePrograma% %Version%";
        public static string txtAviso = @"Pulsa continuar para intentar omitir el error, o salir para cerrar el programa.
Aviso: perderá todos sus datos no guardados.";
        public static string txtInformacionError = "Información sobre el error:";
        public static string txtContinuar = "Continuar";
        public static string txtSalir = "Salir";
        public static string txtEnviarInforme = "Enviar informe del error";
        public static string txtErrorDesconocido = "Se ha producido un error desconocido en %NombrePrograma%";
        public static string txtTipo = "Tipo:";
        public static string txtMensaje = "Mensaje:";


        public static string txtToolTipEnviarInfome = "Enviar informe del error al autor del programa para poder corregirlo en futuras versiones.";
        public static string txtToolTipContinuar = "Continuar con la ejecucion del programa intentando omitir el error.";
        public static string txtToolTipSalir = "Salir del programa inmediatamente.";
        public static string txtTituloToolTip = "Informacion";


        #endregion

        #region Captura de errores
        public static void ActivarAnalisis(string nombrePrograma, string version, string destino, Image logoPrograma)
        {
            NombrePrograma = nombrePrograma;
            Version = version;
            Destino = destino;
            LogoPrograma = logoPrograma;

            EnvioAutomatico = true;
            IntentarOmitirExcepciones = false;

            /*	Establecer los delegados	*/
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ErrorSubproceso);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ErrorNoCapturado);
        }

        public static void ActivarAnalisis(string destino)
        {
            ActivarAnalisis(Application.ProductName, Application.ProductVersion, destino, null);
        }

        static void ErrorNoCapturado(object sender, UnhandledExceptionEventArgs e)
        {
            MostrarError((Exception)e.ExceptionObject, sender);
        }

        static void ErrorSubproceso(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MostrarError(e.Exception, sender);
        }
        #endregion

        #region Mostrar error

        private static void MostrarError(Exception error, object sender)
        {
            if (IntentarOmitirExcepciones)
            {
                if (EnvioAutomatico)
                    EnviarInforme(error, sender, false);
                return;
            }

            Error ventanaError = new Error();

            //Textos
            ventanaError.Text = ReemplazarVariables(txtTextoVentana);
            ventanaError.cabecera.Text = ReemplazarVariables(txtCabecera);
            ventanaError.aviso.Text = ReemplazarVariables(txtAviso);
            ventanaError.info.Text = ReemplazarVariables(txtInformacionError);
            ventanaError.bContinuar.Text = ReemplazarVariables(txtContinuar);
            ventanaError.bSalir.Text = ReemplazarVariables(txtSalir);
            ventanaError.enviarInforme.Text = ReemplazarVariables(txtEnviarInforme);
            if (error == null)
                ventanaError.infoError.Text = txtErrorDesconocido;
            else
                ventanaError.infoError.Text = string.Format("{0} {1}\r\n{2} {3}", txtTipo, error.GetType().ToString(), txtMensaje, error.Message);

            //Tooltips
            ventanaError.ToolTip.SetToolTip(ventanaError.enviarInforme, ReemplazarVariables(txtToolTipEnviarInfome));
            ventanaError.ToolTip.SetToolTip(ventanaError.bContinuar, ReemplazarVariables(txtToolTipContinuar));
            ventanaError.ToolTip.SetToolTip(ventanaError.bSalir, ReemplazarVariables(txtToolTipSalir));

            //Otros
            if (Destino == null)
                ventanaError.enviarInforme.Visible = false;
            else
                ventanaError.enviarInforme.Checked = EnvioAutomatico;
            ventanaError.Tag = new object[] { error, sender };
            if (LogoPrograma != null)
                ventanaError.imagen.Image = LogoPrograma;

            ventanaError.ShowDialog();
        }

        private static string ReemplazarVariables(string texto)
        {
            string res = texto;
            res = res.Replace("%NombrePrograma%", NombrePrograma);
            res = res.Replace("%Version%", Version);
            res = res.Replace("%Destino%", Destino);
            res = res.Replace("%IntentarOmitirExcepciones%", IntentarOmitirExcepciones.ToString());
            res = res.Replace("%EnvioAutomatico%", EnvioAutomatico.ToString());
            return res;
        }

        #endregion

        #region Funciones de la ventana

        private void Continuar(object sender, EventArgs e)
        {
            if (this.enviarInforme.Checked)
            {
                try
                {
                    object[] array = (object[])this.Tag;
                    EnviarInforme((Exception)array[0], array[1], false);
                }
                catch { }            
            }
            this.Close();
            return;
        }

        private void Salir(object sender, EventArgs e)
        {
            if (this.enviarInforme.Checked)
            {
                try
                {
                    object[] array = (object[])this.Tag;
                    foreach (Form ventana in Application.OpenForms)
                    {
                        ventana.Hide();
                    }
                    EnviarInforme((Exception)array[0], array[1], true);
                }
                catch { }
            }
            else
                Environment.Exit(1);
        }



        #endregion

        /// <summary>
        /// Enviar el informe del error en segundo plano
        /// </summary>
        public static void EnviarInforme(Exception error, object provocadorExcepcion, bool salirAlAcabar)
        {
            Thread subProcesoInforme = new System.Threading.Thread(new ParameterizedThreadStart(GenerarEnviarInforme));

            subProcesoInforme.Start(new object[] { error, provocadorExcepcion, salirAlAcabar });
        }

        private static void GenerarEnviarInforme(object parametros)
        {
            bool salirAlAcabar = false;
            try
            {
                object[] arrayParametros = (object[])parametros;

                Exception error = (Exception)arrayParametros[0];
                object provocadorExcepcion = arrayParametros[1];
                salirAlAcabar = (bool)arrayParametros[2];
                MailMessage informe = GenerarInforme(error, NombrePrograma, Version, Destino, provocadorExcepcion);
                SmtpMail.Send(informe);
            }
            catch { }
            if (salirAlAcabar == true)
                Environment.Exit(1);
        }

        /// <summary>
        /// Genera un informe sobre una excepcion
        /// </summary>
        /// <param name="ObjetoProvocadorExcepcion">Objeto que provoco la excepcion. Puede ser null</param>
        public static MailMessage GenerarInforme(Exception Error, string NombrePrograma, string Version, string EmailDestino, object ObjetoProvocadorExcepcion)
        {
            string cadenaError = HTML.Replace("%nombreprograma%", NombrePrograma).Replace("%version%", Version) ;
         
            //Información sobre el error
             try
            {
                string infoError=string.Empty;
                infoError = AñadirTextoCadena(infoError, "Error", "<b>" + Error.ToString().Replace("\n", "<br />") + "</b>");
                infoError = AñadirTextoCadena(infoError, "HelpLink", Error.HelpLink);
                infoError = AñadirTextoCadena(infoError, "Source", Error.Source);
                infoError = AñadirTextoCadena(infoError, "TargetSite", Error.TargetSite.ToString());
              
             
                if (ObjetoProvocadorExcepcion != null)
                {
                    infoError = AñadirTextoCadena(infoError, "Objeto provocador del error", ObjetoProvocadorExcepcion.ToString());
                    infoError = AñadirTextoCadena(infoError, "Tipo del objeto provocador del error", ObjetoProvocadorExcepcion.GetType().Name);
                }

                 cadenaError=cadenaError.Replace("%informacionerror%",infoError);
            }
            catch
            {
                cadenaError=cadenaError.Replace("%informacionerror%","<b>La información del error no está disponible.</b");
            }


            //Información sobre el sistema					
            try
            {
                string infoSistema = string.Empty;

                infoSistema = AñadirTextoCadena(infoSistema, "Sistema operativo", "<b>" + System.Environment.OSVersion.Platform.ToString() + " - " + System.Environment.OSVersion.Version.ToString() + "</b>");
                infoSistema = AñadirTextoCadena(infoSistema, "Version de .NET Framework", "<b>" + System.Environment.Version.ToString() + "</b>");
                infoSistema = AñadirTextoCadena(infoSistema, "Pila", System.Environment.StackTrace.Replace("\n", "<br />"));
                infoSistema = AñadirTextoCadena(infoSistema, "Directorio del ejecutable", Application.ExecutablePath);
                infoSistema = AñadirTextoCadena(infoSistema, "Dominio de aplicacion", System.AppDomain.CurrentDomain.ToString());
                infoSistema = AñadirTextoCadena(infoSistema, "Memoria asignada al proceso", System.Environment.WorkingSet/1024/1024+" MB");
                infoSistema = AñadirTextoCadena(infoSistema, "Usuario", System.Environment.UserName + " en " + System.Environment.MachineName);
                infoSistema = AñadirTextoCadena(infoSistema, "Cultura actual", "<b>" + System.Windows.Forms.Application.CurrentCulture.Name + " - " + System.Windows.Forms.Application.CurrentCulture.EnglishName + " - " + System.Windows.Forms.Application.CurrentCulture.ToString() + "</b>");
                infoSistema = AñadirTextoCadena(infoSistema, "Idioma de entrada", System.Windows.Forms.Application.CurrentInputLanguage.LayoutName.ToString() + " - " + System.Windows.Forms.Application.CurrentInputLanguage.Culture.ToString());
                infoSistema = AñadirTextoCadena(infoSistema, "Fecha", System.DateTime.Now.ToLongDateString() + " " + System.DateTime.Now.ToLongTimeString());

                cadenaError = cadenaError.Replace("%informacionsistema%", infoSistema);   
                }
            catch
            {
                cadenaError = cadenaError.Replace("%informacionsistema%", "<b>La información del sistema no está disponible.</b");
           
            }           

            try
            {
                #region Informacion del proceso

                string infoProceso = string.Empty;

                System.Diagnostics.Process actual = System.Diagnostics.Process.GetCurrentProcess();

              infoProceso=  AñadirTextoCadena(infoProceso, "Nombre del proceso", actual.ProcessName);
              infoProceso = AñadirTextoCadena(infoProceso, "Tiempo en ejecucion", ((TimeSpan)(DateTime.Now - actual.StartTime)).ToString());
              infoProceso = AñadirTextoCadena(infoProceso, "Prioridad base", actual.BasePriority);
              infoProceso = AñadirTextoCadena(infoProceso, "Prioridad general", actual.PriorityClass);
              infoProceso = AñadirTextoCadena(infoProceso, "ID", actual.Id);
              infoProceso = AñadirTextoCadena(infoProceso, "Modulo principal", actual.MainModule.ToString());
              infoProceso = AñadirTextoCadena(infoProceso, "Responde la interfaz", "<b>" + actual.Responding.ToString() + "</b>");
              infoProceso = AñadirTextoCadena(infoProceso, "Argumentos pasados a la aplicacion", actual.StartInfo.Arguments);

              infoProceso = AñadirTextoCadena(infoProceso, "Subprocesos", actual.Threads.Count);
                foreach (ProcessThread subProceso in actual.Threads)
                {
                    infoProceso = AñadirTextoCadena(infoProceso, string.Empty, string.Format("ID: {0} , Prioridad: {1} , Estado: {2}",
                        subProceso.Id, subProceso.PriorityLevel.ToString(), subProceso.ThreadState.ToString()));

                }

                cadenaError = cadenaError.Replace("%informacionproceso%", infoProceso);
         
/*
                cadenaError.Append("\n\nModulos cargados:");
                foreach (ProcessModule modulo in actual.Modules)
                {
                    cadenaError.Append(string.Format("\nNombre: {0} , Archivo: {1} , Version: {2}, Descripcion: {3}, Idioma: {4}, Producto: {5}, Version del producto: {6}\n",
                        modulo.ModuleName, modulo.FileName, modulo.FileVersionInfo.FileVersion,
                        modulo.FileVersionInfo.FileDescription, modulo.FileVersionInfo.Language.ToString(), modulo.FileVersionInfo.ProductName, modulo.FileVersionInfo.ProductVersion));
                }
 */
                #endregion
            }
            catch
            {
                cadenaError = cadenaError.Replace("%informacionproceso%", "<b>La información del proceso no está disponible.</b");
            }

            try
            {
                // Variables del sistema
                string variables = string.Empty;

                System.Collections.IDictionary variablesSistema = Environment.GetEnvironmentVariables();
                foreach (System.Collections.DictionaryEntry variable in variablesSistema)
                {
                variables=    AñadirTextoCadena(variables,  variable.Key.ToString(),variable.Value.ToString());
                }
                cadenaError = cadenaError.Replace("%variablesSistema%", variables);
            }
            catch
            {
                cadenaError = cadenaError.Replace("%variablesSistema%", "<b>Las variables del sistema no están disponibles.</b");
          
            }

            System.aTxLiB.Mail.MailMessage mensaje = new System.aTxLiB.Mail.MailMessage();

            mensaje.To.Add(new MailRecipient(EmailDestino, EmailDestino));
            //     mensaje.Sender = new MailRecipient("Error de " + NombrePrograma, NombrePrograma + "@error.com");
            mensaje.Sender = new MailRecipient(Error.GetType().ToString() + " en " + NombrePrograma + " " + Version, NombrePrograma + Version+ "@error.com");
            mensaje.Subject = "Error en " + NombrePrograma + " " + Version;
            mensaje.BodyFormat = MailFormat.Html;
            mensaje.Body = cadenaError.ToString();
           
            return mensaje;
        }

        private static string AñadirTextoCadena(string cadena, string key, object value)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    cadena += string.Format("<li><span style=\"color: black\">{0}</span></li>", value.ToString());
                else
                    cadena += string.Format("<li><span style=\"color: black\">{0}: {1}</span></li>", key, value.ToString());
            }
            catch { }
            return cadena;
        }


        private static string HTML=@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<html>
	<head>
		<title></title>
	</head>
	<body>
        <span style=""color: red""><strong>
        Error de %nombreprograma%, versión %version%
            <br />
            <br />
            <br />
            <span style=""color: black"">Información del error:<br />
                <br />
            </span></strong></span>

          %informacionerror%
        <br />
        <br />

        <span style=""color: red""><span style=""color: black""><strong>Información del sistema:<br />
        </strong>
            <br />
                %informacionsistema% <br />
            <br />
            <strong>Información del proceso:<br />
            </strong>
                <br />
            %informacionproceso%
            <br />
            <br />
            <strong>Variables del sistema:<br />
                <br />
            </strong>
                %variablesSistema%

        </span></span>
	
	</body>
</html>";
    }
}
