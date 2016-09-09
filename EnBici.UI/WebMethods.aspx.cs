using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.ServiceModel.Web;
using EnBici.Entidades;

namespace EnBici
{
    [ScriptService]
    public partial class WebMethods : System.Web.UI.Page
    {
        #region Marcadores para las estaciones de medición de la calidad del aire

        /// <summary>
        /// Método web para crear el JSON que contendrá la información de los marcadores de las estaciones de medida de la calidad de aire
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Entidades.Marcadores[] CrearMarcadorAire()
        {
            List<Entidades.Marcadores> lstMarkers = new List<Entidades.Marcadores>();
            DateTime fecha = new DateTime();
            int nivel = 0;
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            try
            {
                if ((HttpContext.Current.Session["listAireEstaciones"] != null) && (HttpContext.Current.Session["listAireTiempoReal"] != null))
                {        
                    List<Entidades.Aire> listAireTiempoReal = (List<Entidades.Aire>)HttpContext.Current.Session["listAireTiempoReal"];
                    List<Entidades.AireParametros> listAireParametros = (List<Entidades.AireParametros>)HttpContext.Current.Session["listAireParametros"];
                     
                    foreach (Entidades.AireEstaciones oAireEstaciones in (List<Entidades.AireEstaciones>)(HttpContext.Current.Session["listAireEstaciones"]))
                    { 
                        Entidades.Marcadores objMAPS = new Entidades.Marcadores();
                        List<Entidades.AireParametros> oAireParametros = Negocio.Aire.BuscarUltimasMedicionesEstacion(listAireTiempoReal.FindAll(x=>x.CodEstacion03 == oAireEstaciones.CodEstacion03), listAireParametros);
                        objMAPS.NombreEstacion = oAireEstaciones.Nombre;
                        objMAPS.CodEstacion01 = oAireEstaciones.CodEstacion01;
                        objMAPS.CodEstacion02 = oAireEstaciones.CodEstacion02;
                        objMAPS.CodEstacion03 = oAireEstaciones.CodEstacion03;
                        objMAPS.Latitud = oAireEstaciones.Latitud.ToString().Replace(",", ".");
                        objMAPS.Longitud = oAireEstaciones.Longitud.ToString().Replace(",", ".");               

                        objMAPS.Contenido = "<div id='iw-container'>"
                                + "<div class='iw-title'>" + oAireEstaciones.Nombre + "</div>"
                                + "<div class='iw-content'>"
                                    + "<div class='iw-subTitle'>Ubicación: </div>"
                                    + "<p>Latitud: " + oAireEstaciones.Latitud + "<br />" 
                                    + "Longitud: " + oAireEstaciones.Longitud + "</p>"
                                    + "<div class='iw-subTitle'>Datos: </div><ul>";
                        nivel = 0;
                        foreach (Entidades.AireParametros oAux in oAireParametros)
                        {
                            diccionario = new Dictionary<string,string>();
                            diccionario = CargarIconoMedidaAire(oAux);
                            fecha = oAux.FechaMedida;
                            objMAPS.Contenido = objMAPS.Contenido + "<li>" + oAux.Descripcion + " (" + oAux.Abreviatura + "): " + oAux.Medida + " " + oAux.UnidadMedida + "<img src='" + diccionario["Icono"] + "'></img></li>";
                            if (nivel < int.Parse(diccionario["Nivel"].ToString()))
                            {
                                nivel = int.Parse(diccionario["Nivel"].ToString());
                            }
                        }
                        
                        if (nivel == 0 || nivel == 1)
                        {
                            objMAPS.Icono = "/img/aire/windturbine_blue.png";
                        }
                        else if (nivel == 2)
                        {
                            objMAPS.Icono = "/img/aire/windturbine_green.png";
                        }
                        else if (nivel == 3)
                        {
                            objMAPS.Icono = "/img/aire/windturbine_yellow.png";
                        }
                        else if (nivel == 4)
                        {
                            objMAPS.Icono = "/img/aire/windturbine_red.png";
                        }
                        else
                        {
                            objMAPS.Icono = "/img/aire/parking.png";
                        }
                        objMAPS.Contenido = objMAPS.Contenido + "</ul><p>Fecha de los datos: " + fecha + "</p>";
                        objMAPS.Contenido = objMAPS.Contenido + "</div><div class='iw-bottom-gradient'></div></div>";
                        lstMarkers.Add(objMAPS);
                    }                   
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstMarkers.ToArray();
        }

        /// <summary>
        /// Método que cargará el icono a mostrar para cada uno de los parámetros de calidad de aire en función de los niveles recomendados
        /// </summary>
        /// <param name="oAux"></param>
        /// <returns></returns>
        private static Dictionary<string, string> CargarIconoMedidaAire(AireParametros oAux)
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            if (oAux.NivelBueno != 0)
            {
                if (oAux.NivelBueno >= oAux.Medida)
                {
                    diccionario.Add("Icono", "/img/bolas/bola-azul-16x16.png");
                    diccionario.Add("Nivel", "1");
                }
                else if (oAux.NivelModerado >= oAux.Medida)
                {
                    diccionario.Add("Icono", "/img/bolas/bola-verde-16x16.png");
                    diccionario.Add("Nivel", "2");
                }
                else if (oAux.NivelDeficiente >= oAux.Medida)
                {
                    diccionario.Add("Icono", "/img/bolas/bola-amarillo-16x16.png");
                    diccionario.Add("Nivel", "3");
                }
                else if (oAux.NivelMalo >= oAux.Medida)
                {
                    diccionario.Add("Icono", "/img/bolas/bola-roja-16x16.png");
                    diccionario.Add("Nivel", "4");
                }
                else
                {
                    diccionario.Add("Icono", "/img/bolas/quedate-en-casa-16x16.png");
                    diccionario.Add("Nivel", "5");
                }
            }
            else
            {
                diccionario.Add("Icono", "/img/bolas/sin-referencia-16x16.png");
                diccionario.Add("Nivel", "0");
            }
            return diccionario;
        }

        /// <summary>
        /// Método que apoyado en las clases de negocio, obtiene los indicadores en tiempo real de la calidad de aire y los niveles recomendados desde la base de datos. 
        /// </summary>
        public void CargarMarcadoresAire()
        {
            List<Entidades.AireEstaciones> listAireEstaciones = Negocio.AireEstaciones.ObtenerAireEstaciones();
            HttpContext.Current.Session["listAireEstaciones"] = listAireEstaciones;
            List<Entidades.Aire> listAireTiempoReal = Negocio.Aire.LecturaEnTiempoReal(listAireEstaciones);
            HttpContext.Current.Session["listAireTiempoReal"] = listAireTiempoReal;
            List<Entidades.AireParametros> listAireParametros = Negocio.AireParametros.ObtenerAireParametros();
            HttpContext.Current.Session["listAireParametros"] = listAireParametros;
        }

        #endregion Marcadores para las estaciones de medición de la calidad de aire

        #region Marcadores para las estaciones de medición de la contaminación acústica
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Entidades.Marcadores[] CrearMarcadorRuido()
        {
            List<Entidades.Marcadores> lstMarkers = new List<Entidades.Marcadores>();
            Entidades.Ruido oRuido = new Entidades.Ruido();
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            try
            {
                if ((HttpContext.Current.Session["listRuidoEstaciones"] != null) && (HttpContext.Current.Session["listRuidoTiempoReal"] != null))
                {
                    List<Entidades.Ruido> listRuidoTiempoReal = (List<Entidades.Ruido>)HttpContext.Current.Session["listRuidoTiempoReal"];
                    Entidades.RuidoMedida medida = new RuidoMedida();
                    foreach (Entidades.RuidoEstaciones oRuidoEstaciones in (List<Entidades.RuidoEstaciones>)(HttpContext.Current.Session["listRuidoEstaciones"]))
                    {
                        medida = new RuidoMedida();
                        diccionario = new Dictionary<string, string>();
                        oRuido = listRuidoTiempoReal.Find(x => x.CodEstacion == oRuidoEstaciones.CodEstacion);
                        
                        if (oRuido != null)
                        {
                            Entidades.Marcadores objMAPS = new Entidades.Marcadores();
                            objMAPS.NombreEstacion = oRuidoEstaciones.Nombre;
                            objMAPS.CodEstacion01 = oRuidoEstaciones.CodEstacion.ToString();
                            objMAPS.NombreEstacion = oRuidoEstaciones.Nombre.ToString();
                            objMAPS.Latitud = oRuidoEstaciones.Latitud.ToString().Replace(",", ".");
                            objMAPS.Longitud = oRuidoEstaciones.Longitud.ToString().Replace(",", ".");
                            objMAPS.Contenido = "<div id='iw-container'>"
                                    + "<div class='iw-title'>" + oRuidoEstaciones.Nombre + "</div>"
                                    + "<div class='iw-content'>"
                                        + "<div class='iw-subTitle'>Dirección: </div>"
                                        + "<p>" + oRuidoEstaciones.Direccion + "</p>"
                                        + "<div class='iw-subTitle'>Ubicación: </div>"
                                        + "<p>Latitud: " + oRuidoEstaciones.Latitud + "<br />"
                                        + "Longitud: " + oRuidoEstaciones.Longitud + "</p>"
                                        + "<div class='iw-subTitle'>Datos: </div><ul>";
                            
                            medida = oRuido.Medida.Find(x => x.Periodo == "N");
                            if (medida == null)
                            {
                                medida = new Entidades.RuidoMedida();
                                medida = oRuido.Medida.Find(x => x.Periodo == "V");

                                if (medida == null)
                                {
                                    medida = new Entidades.RuidoMedida();
                                    medida = oRuido.Medida.Find(x => x.Periodo == "D");

                                    if (medida == null)
                                    {
                                        medida = new Entidades.RuidoMedida();
                                        medida = oRuido.Medida.Find(x => x.Periodo == "T");
                                    }
                                }
                            }
                            diccionario = CalcularIconoRuido(medida, oRuidoEstaciones);
                            objMAPS.Contenido = objMAPS.Contenido
                                    + "<li>LAEQ: " + medida.LAEQ.ToString() + " dB" + diccionario["iconoBola"] +"</li>"
                                    + "<li>LAS01: " + medida.LAS01.ToString() + " dB" + "</li >"
                                    + "<li>LAS10: " + medida.LAS10.ToString() + " dB" + "</li >"
                                    + "<li>LAS50: " + medida.LAS50.ToString() + " dB" + "</li >"
                                    + "<li>LAS90: " + medida.LAS90.ToString() + " dB" + "</li >"
                                    + "<li>LAS99: " + medida.LAS99.ToString() + " dB" + "</li >";
                            objMAPS.Contenido = objMAPS.Contenido + "</ul><p>Fecha de los datos: " + oRuido.Fecha.ToString("dd/MM/yyyy") + "</p>";
                            objMAPS.Contenido = objMAPS.Contenido + "</div><div class='iw-bottom-gradient'></div></div>";
                            objMAPS.Icono = diccionario["iconoMarcador"];                               
                            lstMarkers.Add(objMAPS);
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstMarkers.ToArray();
        }

        private static Dictionary<string,string> CalcularIconoRuido(RuidoMedida medida, RuidoEstaciones oRuidoEstaciones)
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            string icono = string.Empty;
            switch (medida.Periodo)
            {
                case "D":
                case "V":
                    if (medida.LAEQ > oRuidoEstaciones.NivelDiurno)
                    {
                        diccionario.Add("iconoBola","<img src='/img/bolas/bola-roja-16x16.png'></img>");
                        diccionario.Add("iconoMarcador", "/img/ruido/audio_red.png");
                    }
                    else if (medida.LAEQ < oRuidoEstaciones.NivelDiurno)
                    {
                        diccionario.Add("iconoBola", "<img src='/img/bolas/bola-azul-16x16.png'></img>");
                        diccionario.Add("iconoMarcador", "/img/ruido/audio_blue.png");
                    }
                    break;
                case "N":
                    if (medida.LAEQ > oRuidoEstaciones.NivelDiurno)
                    {
                        diccionario.Add("iconoBola", "<img src='/img/bolas/bola-roja-16x16.png'></img>");
                        diccionario.Add("iconoMarcador", "/img/ruido/audio_red.png");
                    }
                    else if (medida.LAEQ < oRuidoEstaciones.NivelDiurno)
                    {
                        diccionario.Add("iconoBola", "<img src='/img/bolas/bola-azul-16x16.png'></img>");
                        diccionario.Add("iconoMarcador", "/img/ruido/audio_blue.png");
                    }
                    break;
                case "T":
                    decimal? mediaAritmetica = (oRuidoEstaciones.NivelDiurno + oRuidoEstaciones.NivelNocturno) / 2;
                    if (medida.LAEQ > mediaAritmetica)
                    {
                        diccionario.Add("iconoBola", "<img src='/img/bolas/bola-roja-16x16.png'></img>");
                        diccionario.Add("iconoMarcador", "/img/ruido/audio_red.png");
                    }
                    else if (medida.LAEQ < mediaAritmetica)
                    {
                        diccionario.Add("iconoBola", "<img src='/img/bolas/bola-azul-16x16.png'></img>");
                        diccionario.Add("iconoMarcador", "/img/ruido/audio_blue.png");
                    }
                    break;
            }            
            
            return diccionario;
        }

        private static Dictionary<string, string> CargarIconoMedidaRuido(AireParametros oAux)
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            if (oAux.NivelBueno != 0)
            {
                if (oAux.NivelBueno >= oAux.Medida)
                {
                    diccionario.Add("Icono", "/img/bolas/bola-azul-16x16.png");
                    diccionario.Add("Nivel", "1");
                }
                else if (oAux.NivelModerado >= oAux.Medida)
                {
                    diccionario.Add("Icono", "/img/bolas/bola-verde-16x16.png");
                    diccionario.Add("Nivel", "2");
                }
                else if (oAux.NivelDeficiente >= oAux.Medida)
                {
                    diccionario.Add("Icono", "/img/bolas/bola-amarillo-16x16.png");
                    diccionario.Add("Nivel", "3");
                }
                else if (oAux.NivelMalo >= oAux.Medida)
                {
                    diccionario.Add("Icono", "/img/bolas/bola-roja-16x16.png");
                    diccionario.Add("Nivel", "4");
                }
                else
                {
                    diccionario.Add("Icono", "/img/bolas/quedate-en-casa-16x16.png");
                    diccionario.Add("Nivel", "5");
                }
            }
            else
            {
                diccionario.Add("Icono", "/img/bolas/sin-referencia-16x16.png");
                diccionario.Add("Nivel", "0");
            }
            return diccionario;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CargarMarcadoresRuido()
        {
            List<Entidades.AireEstaciones> listAireEstaciones = Negocio.AireEstaciones.ObtenerAireEstaciones();
            HttpContext.Current.Session["listAireEstaciones"] = listAireEstaciones;
            List<Entidades.Aire> listAireTiempoReal = Negocio.Aire.LecturaEnTiempoReal(listAireEstaciones);
            HttpContext.Current.Session["listAireTiempoReal"] = listAireTiempoReal;
            List<Entidades.AireParametros> listAireParametros = Negocio.AireParametros.ObtenerAireParametros();
            HttpContext.Current.Session["listAireParametros"] = listAireParametros;
        }
    }
    #endregion Marcadores para las estaciones de medición de la contaminación acústica
}