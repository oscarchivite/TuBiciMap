using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnBici.Entidades;
using EnBici.Negocio;
using System.Data;
using System.Web.Services;

namespace EnBici
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session["listAireEstaciones"] = null;
            HttpContext.Current.Session["listAireTiempoReal"] = null;
            HttpContext.Current.Session["listAireParametros"] = null;
            CargarMarcadoresAire();
            HttpContext.Current.Session["listRuidoEstaciones"] = null;
            HttpContext.Current.Session["listRuidoTiempoReal"] = null;
            CargarMarcadoresRuido();
        }

        /// <summary>
        /// Método para obtener en tiempo real los valores para los marcadores de las estaciones de medida de calidad de aire
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

        /// <summary>
        /// Método para obtener en tiempo real los valores para los marcadores de las estaciones de medida de ruido
        /// </summary>
        public void CargarMarcadoresRuido()
        {
            List<Entidades.RuidoEstaciones> listRuidoEstaciones = Negocio.RuidoEstaciones.ObtenerRuidoEstaciones();
            HttpContext.Current.Session["listRuidoEstaciones"] = listRuidoEstaciones;
            List<Entidades.Ruido> listRuidoTiempoReal = Negocio.Ruido.LecturaEnTiempoReal(listRuidoEstaciones);
            HttpContext.Current.Session["listRuidoTiempoReal"] = listRuidoTiempoReal;            
        }
    }
}