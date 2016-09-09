using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnBici.Entidades;
using System.Configuration;
using System.Net;
using System.IO;

namespace EnBici.Negocio
{
    public class Ruido
    {
        /// <summary>
        /// Método que obtiene de la web el último fichero con los datos de ruido colgado por el ayuntamiento de Madrid
        /// </summary>
        /// <returns></returns>
        public static LogEntidad DescargaFichero()
        {
            LogEntidad oDescarga = new LogEntidad();
            oDescarga.fechaInicioProceso = DateTime.Now;
            string remoteUri = string.Empty;
            string localUri = string.Empty;
            string fileName = string.Empty;
            string localWebResource = string.Empty;

            try
            {
                remoteUri = ConfigurationManager.AppSettings["UrlServiceRuido"].ToString();
                localUri = ConfigurationManager.AppSettings["UrlLocalRuido"].ToString();
                fileName = ConfigurationManager.AppSettings["FileNameRuido"].ToString();
                localWebResource = string.Empty;
                // Creamos a una instancia de WebClient.
                WebClient myWebClient = new WebClient();
                //Formamos el nombre del fichero descargado utilizando la fecha y la hora, para formar el nombre del archivo.
                fileName = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + "_" + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss") + "_" + fileName;
                // Formamos la ruta desde donde descargar el fichero.
                localWebResource = string.Concat(localUri, fileName);

                // Si el directorio no existe, lo creamos.
                if (!Directory.Exists(localUri))
                {
                    Directory.CreateDirectory(localUri);
                }
                // Descargamos el fichero a la ruta local del repositorio.
                myWebClient.DownloadFile(remoteUri, localWebResource);
                oDescarga.fechaFinProceso = DateTime.Now;
                oDescarga.procesoCorrecto = true;
                oDescarga.descargaCorrecta = true;
                oDescarga.fichero = fileName;
                return oDescarga;
            }
            catch (Exception ex)
            {
                oDescarga.descargaCorrecta = false;
                oDescarga.fichero = fileName;
                oDescarga.log = ex.ToString();
                oDescarga.fechaFinProceso = DateTime.Now;
                return oDescarga;
            }
        }

        /// <summary>
        /// Método para leer el fichero descargado de la web del ayuntamiento de Madrid. El método devuelve una lista de objetos de tipo AireEntidad.
        /// </summary>
        /// <param name="oDownloadFile"></param>
        /// <returns></returns>
        public static List<Entidades.Ruido> LeerFichero(LogEntidad oDownloadFile, List<Entidades.RuidoEstaciones> listRuidoEstaciones)
        {
            try
            {
                Entidades.Ruido oRuido = new Entidades.Ruido();
                List<Entidades.Ruido> listRuido = new List<Entidades.Ruido>();
                List<Entidades.Ruido> listRuidoAuxiliar = new List<Entidades.Ruido>();
                Entidades.RuidoEstaciones oRuidoEstaciones = new Entidades.RuidoEstaciones();
                string linea = string.Empty;
                int numLinea = 0;
                char separador = char.Parse(",");
                string[] lectura = new string[57];

                System.IO.StreamReader file = new System.IO.StreamReader(ConfigurationManager.AppSettings["UrlLocalRuido"].ToString() + (oDownloadFile.fichero));

                linea = file.ReadLine();
                numLinea++;
                do
                {
                    lectura = new string[11];
                    lectura = linea.ToString().Split(separador);
                    oRuido = new Entidades.Ruido();
                    oRuido.CodEstacion = int.Parse(lectura[0].Trim());
                    oRuido.Anio = int.Parse(lectura[1].Trim());
                    oRuido.Mes = int.Parse(lectura[2].Trim());
                    oRuido.Dia = int.Parse(lectura[3].Trim());
                    oRuido.Periodo = lectura[4].Trim();
                    oRuido.LAEQ = decimal.Parse(lectura[5].Trim().Replace(".",","));
                    oRuido.LAS01 = decimal.Parse(lectura[6].Trim().Replace(".", ","));
                    oRuido.LAS10 = decimal.Parse(lectura[7].Trim().Replace(".", ","));
                    oRuido.LAS50 = decimal.Parse(lectura[8].Trim().Replace(".", ","));
                    oRuido.LAS90 = decimal.Parse(lectura[9].Trim().Replace(".", ","));
                    oRuido.LAS99 = decimal.Parse(lectura[10].Trim().Replace(".", ","));
                    oRuidoEstaciones = listRuidoEstaciones.Find(x => int.Parse(x.CodEstacion.ToString()) == int.Parse(lectura[0].Trim()));
                    if (oRuidoEstaciones != null)
                    {
                        oRuido.Nombre = oRuidoEstaciones.Nombre;
                        oRuido.Longitud = oRuidoEstaciones.Longitud;
                        oRuido.Latitud = oRuidoEstaciones.Latitud;
                    }
                    listRuido.Add(oRuido);
                    
                } while ((linea = file.ReadLine()) != null);

                file.Close();
                listRuidoAuxiliar = UnificarLecturasEstaciones(listRuido);
                //Retornamos la lista de los elementos leídos del fichero.
                return listRuidoAuxiliar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<Entidades.Ruido> UnificarLecturasEstaciones(List<Entidades.Ruido> listRuido)
        {
            Entidades.Ruido oAuxRuido = new Entidades.Ruido();
            List<Entidades.Ruido> listAuxRuido = new List<Entidades.Ruido>();
            List<Entidades.RuidoPeriodo> oPeriodo = Negocio.RuidoPeriodo.ObtenerRuidoPeriodo();
            foreach (Entidades.Ruido oRuido in listRuido)
            {
                oAuxRuido = listAuxRuido.Find(x => x.CodEstacion == oRuido.CodEstacion);
                if (oAuxRuido == null)
                {
                    oAuxRuido = new Entidades.Ruido();
                    Entidades.RuidoMedida medida = new Entidades.RuidoMedida();
                    medida.LAEQ = oRuido.LAEQ;
                    medida.LAS01 = oRuido.LAS01;
                    medida.LAS10 = oRuido.LAS10;
                    medida.LAS50 = oRuido.LAS50;
                    medida.LAS90 = oRuido.LAS90;
                    medida.LAS99 = oRuido.LAS99;
                    medida.Periodo = oRuido.Periodo;
                    medida.DescripcionPeriodo = oPeriodo.Find(x => x.IdPeriodo == oRuido.Periodo).DescripcionPeriodo;
                    oAuxRuido.Medida.Add(medida);
                    oAuxRuido.LAEQ = 0;
                    oAuxRuido.LAS01 = 0;
                    oAuxRuido.LAS10 = 0;
                    oAuxRuido.LAS50 = 0;
                    oAuxRuido.LAS90 = 0;
                    oAuxRuido.LAS99 = 0;
                    oAuxRuido.CodEstacion = oRuido.CodEstacion;
                    oAuxRuido.Nombre = oRuido.Nombre;
                    oAuxRuido.Anio = oRuido.Anio;
                    oAuxRuido.Mes = oRuido.Mes;
                    oAuxRuido.Dia = oRuido.Dia;
                    oAuxRuido.Fecha =Convert.ToDateTime(oRuido.Dia + "/" + oRuido.Mes + "/" + oRuido.Anio);
                    oAuxRuido.Longitud = oRuido.Longitud;
                    oAuxRuido.Latitud = oRuido.Latitud;                    
                    listAuxRuido.Add(oAuxRuido);
                }
                else {
                    Entidades.RuidoMedida medida = new Entidades.RuidoMedida();
                    medida.LAEQ = oRuido.LAEQ;
                    medida.LAS01 = oRuido.LAS01;
                    medida.LAS10 = oRuido.LAS10;
                    medida.LAS50 = oRuido.LAS50;
                    medida.LAS90 = oRuido.LAS90;
                    medida.LAS99 = oRuido.LAS99;
                    medida.Periodo = oRuido.Periodo;
                    medida.DescripcionPeriodo = oPeriodo.Find(x => x.IdPeriodo == oRuido.Periodo).DescripcionPeriodo;
                    oAuxRuido.Medida.Add(medida);                                       
                }              
            }

            return listAuxRuido;
        }


        /// <summary>
        /// Método que obtiene la última lectura existente de los valores de aire de la base de datos para el último periodo leído.
        /// </summary>
        /// <returns></returns>
        public static List<Entidades.Ruido> LecturaEnTiempoReal(List<Entidades.RuidoEstaciones> listRuidoEstaciones)
        {
            List<Entidades.Ruido> listTiempoReal = new List<Entidades.Ruido>();
            //Tratamos de descargar el último fichero colgado en la web del 1ayuntamiento de Madrid.
            LogEntidad oDownloadFile = Negocio.Ruido.DescargaFichero();

            // Si el fichero se ha podido descargar de forma correcta, comprobamos si debemos insertar en base de datos cada uno de los registros.
            if (oDownloadFile.descargaCorrecta)
            {
                try
                {
                    listTiempoReal = Negocio.Ruido.LeerFichero(oDownloadFile, listRuidoEstaciones);

                }
                catch (Exception ex)
                {
                    oDownloadFile.log = ex.ToString();
                    oDownloadFile.procesoCorrecto = false;
                    return null;
                }
            }
            else
            {
                oDownloadFile.log = "Descarga incorrecta";
                oDownloadFile.procesoCorrecto = false;
                return null;
            }
            return listTiempoReal;
        }       
    }
}
