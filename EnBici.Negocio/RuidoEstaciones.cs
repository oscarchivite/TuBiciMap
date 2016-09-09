using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Negocio
{
    public class RuidoEstaciones
    {
        /// <summary>
        /// Método que obtiene de la web el último fichero con los datos de aire colgado por el ayuntamiento de Madrid
        /// </summary>
        /// <returns></returns>
        public static List<Entidades.RuidoEstaciones> ObtenerRuidoEstaciones()
        {
            Entidades.RuidoEstaciones oRuidoEstaciones = new Entidades.RuidoEstaciones();
            List<Entidades.RuidoEstaciones> listRuidoEstaciones = new List<Entidades.RuidoEstaciones>();
            DataSet ds = Datos.RuidoEstaciones.ObtenerRuidoEstaciones();
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    oRuidoEstaciones = new Entidades.RuidoEstaciones();
                    oRuidoEstaciones.CodEstacion =  Int32.Parse(dr[1].ToString());
                    oRuidoEstaciones.Nombre = dr[2].ToString();
                    oRuidoEstaciones.Direccion = dr[3].ToString();
                    oRuidoEstaciones.Longitud = Decimal.Parse(dr[4].ToString());
                    oRuidoEstaciones.Latitud = Decimal.Parse(dr[5].ToString());
                    oRuidoEstaciones.Altitud = Decimal.Parse(dr[6].ToString());
                    oRuidoEstaciones.IdRuidoObjetivo = Int32.Parse(dr[7].ToString());
                    oRuidoEstaciones.NivelDiurno = Decimal.Parse(dr[8].ToString());
                    oRuidoEstaciones.NivelNocturno = Decimal.Parse(dr[9].ToString());
                    oRuidoEstaciones.DescripcionNivel = dr[10].ToString();
                    listRuidoEstaciones.Add(oRuidoEstaciones);
                }
            }
            return listRuidoEstaciones;
        }
    }
}
