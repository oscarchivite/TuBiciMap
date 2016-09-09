using System;
using System.IO;
using System.Configuration;

namespace EnBici.Negocio
{
    public class ProcesoExterno
    {
        #region logs
        /// <summary>
        /// Metodo que crea un log para escribir en el todos los errores que van sucediendo   mientras el servicio esta procesando
        /// </summary>
        /// <param name="error"></param>
        public static void EscribirLogErrores(string error)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AppSettingsSection appSettings = configuration.AppSettings;
                string rutaLog = appSettings.Settings["RutaLog"].Value;
                if (!Directory.Exists(rutaLog))
                {
                    Directory.CreateDirectory(rutaLog);
                }
                string nombreLogErrores = appSettings.Settings["NombreLogErrores"].Value;
                string formato = (string)ConfigurationManager.AppSettings["Formato"];
                rutaLog = rutaLog + nombreLogErrores + "_" + DateTime.Now.ToLocalTime().ToString(formato) + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(rutaLog, true);

                string fecdia = (string)ConfigurationManager.AppSettings["FechaHoy"];

                sw.WriteLine(DateTime.Now.ToString(fecdia) + " - " + error + "\r\n");
                sw.Close();
            }
            //Si da un error registrando el error no hacemos nada.
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Método que crea un Log para escribir todo lo que sucede cuando el servicio esta procesando 
        /// </summary>
        /// <param name="texto"></param>
        public static void EscribirLogProceso(string texto)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AppSettingsSection appSettings = configuration.AppSettings;

                string rutaLog = appSettings.Settings["RutaLog"].Value;
                if (!Directory.Exists(rutaLog))
                {
                    Directory.CreateDirectory(rutaLog);
                }
                string nombreLogProceso = appSettings.Settings["NombreLogProceso"].Value;
                string formato = appSettings.Settings["Formato"].Value;
                rutaLog = rutaLog + nombreLogProceso + "_" + DateTime.Now.ToLocalTime().ToString(formato) + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(rutaLog, true);

                string fecdia = appSettings.Settings["FechaHoy"].Value;
                sw.WriteLine(DateTime.Now.ToString(fecdia) + " - " + texto + "\r\n");
                sw.Close();

            }
            catch (Exception ex)
            {
                EscribirLogErrores(ex.ToString());
            }

        }
        #endregion
    }
}
