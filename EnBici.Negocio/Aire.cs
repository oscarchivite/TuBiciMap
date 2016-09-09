using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Linq;
using System.Text;
using EnBici.Entidades;

namespace EnBici.Negocio
{
    public class Aire
    {
        public static List<Entidades.AireParametros> BuscarUltimasMedicionesEstacion(List<Entidades.Aire> oAire, List<Entidades.AireParametros> listAireParametros)
        {
            List<Entidades.AireParametros> listAireP = new List<Entidades.AireParametros>();
            foreach (Entidades.AireParametros oAireP in listAireParametros)
            {
                Dictionary<string, string> diccionario = BuscarUltimaMedicion(oAire, oAireP.CodParametro);
                if (diccionario != null)
                {
                    Entidades.AireParametros oAireParametro = new Entidades.AireParametros();
                    oAireParametro.Abreviatura = oAireP.Abreviatura;
                    oAireParametro.CodParametro = oAireP.CodParametro;
                    oAireParametro.Descripcion = oAireP.Descripcion;
                    oAireParametro.DescripcionMedida = oAireP.DescripcionMedida;
                    oAireParametro.UnidadMedida = oAireP.UnidadMedida;
                    oAireParametro.IdTecnicaMedida = oAireP.IdTecnicaMedida;
                    oAireParametro.Medida = decimal.Parse(diccionario["Medida"].ToString());
                    oAireParametro.FechaMedida = DateTime.Parse(diccionario["FechaMedida"].ToString());
                    oAireParametro.NivelBueno = oAireP.NivelBueno;
                    oAireParametro.NivelModerado = oAireP.NivelModerado;
                    oAireParametro.NivelDeficiente = oAireP.NivelDeficiente;
                    oAireParametro.NivelMalo = oAireP.NivelMalo;
                    listAireP.Add(oAireParametro);
                }
            }
            return listAireP;
        }

        private static Dictionary<string, string> BuscarUltimaMedicion(List<Entidades.Aire> oAire, string codParametro)
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            Entidades.Aire oAux = oAire.Find(x => x.Magnitud == codParametro);
            if (oAux != null)
            {
                int i = oAux.HoraValidez.Count() - 1;
                int hora = 0;
                bool encontrado = false;
                DateTime fecha = new DateTime();
                do
                {
                    if (oAux.HoraValidez[i].Validez == "V")
                    {
                        encontrado = true;
                        hora = i;
                    }
                    else
                    {
                        i--;
                    }
                } while ((i >= 0) && (!encontrado));

                //if (hora == 23)
                //{
                //    hora = 0;
                //}
                //else
                //{
                //    hora = (hora + 1);
                //}

                fecha = Convert.ToDateTime(oAux.Dia + "/" + oAux.Mes + "/" + oAux.Anio + " " + hora  + ":00");
                diccionario.Add("FechaMedida", fecha.ToString());
                diccionario.Add("Medida", oAux.HoraValidez[hora].Hora.ToString());
                return diccionario;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la última lectura existente de los valores de aire de la base de datos para el último periodo leído.
        /// </summary>
        /// <returns></returns>
        public static List<Entidades.Aire> LecturaEnTiempoReal(List<Entidades.AireEstaciones> listAireEstaciones)
        {
            List<Entidades.Aire> listTiempoReal = new List<Entidades.Aire>();
            //Tratamos de descargar el último fichero colgado en la web del 1ayuntamiento de Madrid.
            LogEntidad oDownloadFile = Negocio.Aire.DescargaFichero();

            // Si el fichero se ha podido descargar de forma correcta, comprobamos si debemos insertar en base de datos cada uno de los registros.
            if (oDownloadFile.descargaCorrecta)
            {
                try
                {
                    listTiempoReal = Negocio.Aire.LeerFichero(oDownloadFile, listAireEstaciones);

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

        /// <summary>
        /// Método que obtiene de la web el último fichero con los datos de aire colgado por el ayuntamiento de Madrid
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
                remoteUri = ConfigurationManager.AppSettings["UrlServiceAire"].ToString();
                localUri = ConfigurationManager.AppSettings["UrlLocalAire"].ToString();
                fileName = ConfigurationManager.AppSettings["FileNameAire"].ToString();
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
        public static List<Entidades.Aire> LeerFichero(LogEntidad oDownloadFile, List<Entidades.AireEstaciones> listAireEstaciones)
        {
            try
            {
                List<Entidades.Aire> listAire = new List<Entidades.Aire>();
                Entidades.Aire oAire = new Entidades.Aire();
                Entidades.AireEstaciones oAireEstaciones = new Entidades.AireEstaciones();
                string linea = string.Empty;
                int numLinea = 0;
                char separador = char.Parse(",");
                string[] lectura = new string[57];

                System.IO.StreamReader file = new System.IO.StreamReader(ConfigurationManager.AppSettings["UrlLocalAire"].ToString() + (oDownloadFile.fichero));

                linea = file.ReadLine();
                numLinea++;
                do
                {
                    lectura = new string[57];
                    lectura = linea.ToString().Split(separador);
                    oAire = new Entidades.Aire();
                    if (lectura[2].Trim() != "099")
                    {
                        oAire.CodEstacion01 = lectura[0].Trim();
                        oAire.CodEstacion02 = lectura[1].Trim();
                        oAire.CodEstacion03 = lectura[2].Trim();
                        oAire.Magnitud = lectura[3].Trim();
                        oAire.Tecnica = lectura[4].Trim();
                        oAire.Periodo = lectura[5].Trim();  //Un 02 indica que son mediciones diarias
                        oAire.Anio = int.Parse(lectura[6].Trim());
                        oAire.Mes = int.Parse(lectura[7].Trim());
                        oAire.Dia = int.Parse(lectura[8].Trim());
                        oAire.HoraValidez[0].Hora = decimal.Parse(lectura[9].Trim());
                        oAire.HoraValidez[0].Validez = lectura[10].Trim();
                        oAire.HoraValidez[1].Hora = decimal.Parse(lectura[11].Trim());
                        oAire.HoraValidez[1].Validez = lectura[12].Trim();
                        oAire.HoraValidez[2].Hora = decimal.Parse(lectura[13].Trim());
                        oAire.HoraValidez[2].Validez = lectura[14].Trim();
                        oAire.HoraValidez[3].Hora = decimal.Parse(lectura[15].Trim());
                        oAire.HoraValidez[3].Validez = lectura[16].Trim();
                        oAire.HoraValidez[4].Hora = decimal.Parse(lectura[17].Trim());
                        oAire.HoraValidez[4].Validez = lectura[18].Trim();
                        oAire.HoraValidez[5].Hora = decimal.Parse(lectura[19].Trim());
                        oAire.HoraValidez[5].Validez = lectura[20].Trim();
                        oAire.HoraValidez[6].Hora = decimal.Parse(lectura[21].Trim());
                        oAire.HoraValidez[6].Validez = lectura[22].Trim();
                        oAire.HoraValidez[7].Hora = decimal.Parse(lectura[23].Trim());
                        oAire.HoraValidez[7].Validez = lectura[24].Trim();
                        oAire.HoraValidez[8].Hora = decimal.Parse(lectura[25].Trim());
                        oAire.HoraValidez[8].Validez = lectura[26].Trim();
                        oAire.HoraValidez[9].Hora = decimal.Parse(lectura[27].Trim());
                        oAire.HoraValidez[9].Validez = lectura[28].Trim();
                        oAire.HoraValidez[10].Hora = decimal.Parse(lectura[29].Trim());
                        oAire.HoraValidez[10].Validez = lectura[30].Trim();
                        oAire.HoraValidez[11].Hora = decimal.Parse(lectura[31].Trim());
                        oAire.HoraValidez[11].Validez = lectura[32].Trim();
                        oAire.HoraValidez[12].Hora = decimal.Parse(lectura[33].Trim());
                        oAire.HoraValidez[12].Validez = lectura[33].Trim();
                        oAire.HoraValidez[13].Hora = decimal.Parse(lectura[35].Trim());
                        oAire.HoraValidez[13].Validez = lectura[36].Trim();
                        oAire.HoraValidez[14].Hora = decimal.Parse(lectura[37].Trim());
                        oAire.HoraValidez[14].Validez = lectura[38].Trim();
                        oAire.HoraValidez[15].Hora = decimal.Parse(lectura[39].Trim());
                        oAire.HoraValidez[15].Validez = lectura[40].Trim();
                        oAire.HoraValidez[16].Hora = decimal.Parse(lectura[41].Trim());
                        oAire.HoraValidez[16].Validez = lectura[41].Trim();
                        oAire.HoraValidez[17].Hora = decimal.Parse(lectura[43].Trim());
                        oAire.HoraValidez[17].Validez = lectura[44].Trim();
                        oAire.HoraValidez[18].Hora = decimal.Parse(lectura[45].Trim());
                        oAire.HoraValidez[18].Validez = lectura[46].Trim();
                        oAire.HoraValidez[19].Hora = decimal.Parse(lectura[47].Trim());
                        oAire.HoraValidez[19].Validez = lectura[48].Trim();
                        oAire.HoraValidez[20].Hora = decimal.Parse(lectura[49].Trim());
                        oAire.HoraValidez[20].Validez = lectura[50].Trim();
                        oAire.HoraValidez[21].Hora = decimal.Parse(lectura[51].Trim());
                        oAire.HoraValidez[21].Validez = lectura[52].Trim();
                        oAire.HoraValidez[22].Hora = decimal.Parse(lectura[53].Trim());
                        oAire.HoraValidez[22].Validez = lectura[54].Trim();
                        oAire.HoraValidez[23].Hora = decimal.Parse(lectura[55].Trim());
                        oAire.HoraValidez[23].Validez = lectura[56].Trim();
                        oAireEstaciones = listAireEstaciones.Find(x => int.Parse(x.CodEstacion01) == int.Parse(lectura[0].Trim()) && int.Parse(x.CodEstacion02) == int.Parse(lectura[1].Trim()) && int.Parse(x.CodEstacion03) == int.Parse(lectura[2].Trim()));
                        if (oAireEstaciones != null)
                        {
                            oAire.NombreEstacion = oAireEstaciones.Nombre;
                            oAire.Longitud = oAireEstaciones.Longitud;
                            oAire.Latitud = oAireEstaciones.Latitud;
                        }                        
                        listAire.Add(oAire);
                    }
                } while ((linea = file.ReadLine()) != null);

                file.Close();

                //Retornamos la lista de los elementos leídos del fichero.
                return listAire;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}
