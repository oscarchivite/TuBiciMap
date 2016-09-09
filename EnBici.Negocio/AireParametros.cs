using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnBici.Negocio
{
    public class AireParametros
    {
        public static List<Entidades.AireParametros> ObtenerAireParametros()
        {
            Entidades.AireParametros oAireParametros = new Entidades.AireParametros();
            List<Entidades.AireParametros> listAireParametros = new List<Entidades.AireParametros>();
            DataSet ds = Datos.AireParametros.ObtenerAireParametros();
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    oAireParametros = new Entidades.AireParametros();
                    oAireParametros.CodParametro = dr[1].ToString();
                    oAireParametros.Descripcion = dr[2].ToString();
                    oAireParametros.Abreviatura = dr[3].ToString();
                    oAireParametros.UnidadMedida = dr[4].ToString();
                    oAireParametros.IdTecnicaMedida = dr[5].ToString();
                    oAireParametros.DescripcionMedida = dr[6].ToString();
                    oAireParametros.NivelBueno = decimal.Parse(dr[7].ToString());
                    oAireParametros.NivelModerado = decimal.Parse(dr[8].ToString());
                    oAireParametros.NivelDeficiente = decimal.Parse(dr[9].ToString());
                    oAireParametros.NivelMalo = decimal.Parse(dr[10].ToString());
                    listAireParametros.Add(oAireParametros);
                }
            }
            return listAireParametros;
        }
    }
}
