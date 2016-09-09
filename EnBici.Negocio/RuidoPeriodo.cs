using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Negocio
{
    public class RuidoPeriodo
    {
        /// <summary>
        /// Método que obtiene de la web el último fichero con los datos de aire colgado por el ayuntamiento de Madrid
        /// </summary>
        /// <returns></returns>
        public static List<Entidades.RuidoPeriodo> ObtenerRuidoPeriodo()
        {
            Entidades.RuidoPeriodo oRuidoPeriodo = new Entidades.RuidoPeriodo();
            List<Entidades.RuidoPeriodo> listRuidoPeriodo = new List<Entidades.RuidoPeriodo>();
            DataSet ds = Datos.RuidoPeriodo.ObtenerRuidoPeriodo();
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    oRuidoPeriodo = new Entidades.RuidoPeriodo();
                    oRuidoPeriodo.IdPeriodo = dr[0].ToString();
                    oRuidoPeriodo.DescripcionPeriodo = dr[1].ToString();
                    listRuidoPeriodo.Add(oRuidoPeriodo);
                }
            }
            return listRuidoPeriodo;
        }
    }
}
