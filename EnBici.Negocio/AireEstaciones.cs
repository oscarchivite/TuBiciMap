using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Negocio
{
    public class AireEstaciones
    {
        /// <summary>
        /// Método que obtiene de la web el último fichero con los datos de aire colgado por el ayuntamiento de Madrid
        /// </summary>
        /// <returns></returns>
        public static List<Entidades.AireEstaciones> ObtenerAireEstaciones()
        {
            Entidades.AireEstaciones oAireEstaciones = new Entidades.AireEstaciones();
            List<Entidades.AireEstaciones> listAireEstaciones = new List<Entidades.AireEstaciones>();
            DataSet ds = Datos.AireEstaciones.ObtenerAireEstaciones();
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    oAireEstaciones = new Entidades.AireEstaciones();
                    oAireEstaciones.CodEstacion01 = dr[1].ToString();
                    oAireEstaciones.CodEstacion02 = dr[2].ToString();
                    oAireEstaciones.CodEstacion03 = dr[3].ToString();
                    oAireEstaciones.CodTipoEstacion = dr[4].ToString();
                    oAireEstaciones.Nombre = dr[5].ToString();
                    oAireEstaciones.Direccion = dr[6].ToString();
                    oAireEstaciones.Longitud = Decimal.Parse(dr[7].ToString());
                    oAireEstaciones.Latitud = Decimal.Parse(dr[8].ToString());
                    listAireEstaciones.Add(oAireEstaciones);
                }
            }

            return listAireEstaciones;
        }
    }
}
