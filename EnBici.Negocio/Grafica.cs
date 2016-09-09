using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Negocio
{
    public class Grafica
    {
        public static DataSet ObtenerInformacionCalidadAire(Entidades.Grafica oGrafica)
        {
            if (oGrafica.GAire.EsDiario)
            {
                return Datos.Grafica.ObtenerInformacionCalidadAireDiario(oGrafica);

            }
            else
            {
                return Datos.Grafica.ObtenerInformacionCalidadAireMensual(oGrafica);
            }
        }

        public static DataSet ObtenerInformacionContaminacionAcustica(Entidades.Grafica oGrafica)
        {
            DataSet ds = new DataSet();
            DataSet dsAux = new DataSet();
            ds.Tables.Add(new DataTable());

            if (oGrafica.GRuido.EsDiario)
            {
                ds.Tables[0].Columns.Add("Elemento", typeof(string));
                ds.Tables[0].Columns.Add("Datos", typeof(decimal));
                ds.Tables[0].Columns.Add("Fecha", typeof(DateTime));

                dsAux =Datos.Grafica.ObtenerInformacionNivelRuidoDiario(oGrafica);
                foreach (DataRow row in dsAux.Tables[0].Rows)
                {
                    ds.Tables[0].Rows.Add("LAEQ", Decimal.Parse(row.ItemArray[0].ToString()),DateTime.Parse(row.ItemArray[1].ToString()));
                }
                foreach (DataRow row in dsAux.Tables[1].Rows)
                {
                    ds.Tables[0].Rows.Add("LAS01", Decimal.Parse(row.ItemArray[0].ToString()), DateTime.Parse(row.ItemArray[1].ToString()));
                }
                foreach (DataRow row in dsAux.Tables[2].Rows)
                {
                    ds.Tables[0].Rows.Add("LAS10", Decimal.Parse(row.ItemArray[0].ToString()), DateTime.Parse(row.ItemArray[1].ToString()));
                }
                foreach (DataRow row in dsAux.Tables[3].Rows)
                {
                    ds.Tables[0].Rows.Add("LAS50", Decimal.Parse(row.ItemArray[0].ToString()), DateTime.Parse(row.ItemArray[1].ToString()));
                }
                foreach (DataRow row in dsAux.Tables[4].Rows)
                {
                    ds.Tables[0].Rows.Add("LAS90", Decimal.Parse(row.ItemArray[0].ToString()), DateTime.Parse(row.ItemArray[1].ToString()));
                }
                foreach (DataRow row in dsAux.Tables[5].Rows)
                {
                    ds.Tables[0].Rows.Add("LAS99", Decimal.Parse(row.ItemArray[0].ToString()), DateTime.Parse(row.ItemArray[1].ToString()));
                }
            }
            else
            {
                ds.Tables[0].Columns.Add("Elemento", typeof(string));
                ds.Tables[0].Columns.Add("Datos", typeof(decimal));
                ds.Tables[0].Columns.Add("Fecha", typeof(Int32));

                dsAux = Datos.Grafica.ObtenerInformacionNivelRuidoMensual(oGrafica);
                foreach (DataRow row in dsAux.Tables[0].Rows)
                {
                    ds.Tables[0].Rows.Add("LAEQ", Decimal.Parse(row.ItemArray[0].ToString()), Int32.Parse(row.ItemArray[1].ToString()));
                }
                foreach (DataRow row in dsAux.Tables[1].Rows)
                {
                    ds.Tables[0].Rows.Add("LAS01", Decimal.Parse(row.ItemArray[0].ToString()), Int32.Parse(row.ItemArray[1].ToString()));
                }
                foreach (DataRow row in dsAux.Tables[2].Rows)
                {
                    ds.Tables[0].Rows.Add("LAS10", Decimal.Parse(row.ItemArray[0].ToString()), Int32.Parse(row.ItemArray[1].ToString()));
                }
                foreach (DataRow row in dsAux.Tables[3].Rows)
                {
                    ds.Tables[0].Rows.Add("LAS50", Decimal.Parse(row.ItemArray[0].ToString()), Int32.Parse(row.ItemArray[1].ToString()));
                }
                foreach (DataRow row in dsAux.Tables[4].Rows)
                {
                    ds.Tables[0].Rows.Add("LAS90", Decimal.Parse(row.ItemArray[0].ToString()), Int32.Parse(row.ItemArray[1].ToString()));
                }
                foreach (DataRow row in dsAux.Tables[5].Rows)
                {
                    ds.Tables[0].Rows.Add("LAS99", Decimal.Parse(row.ItemArray[0].ToString()), Int32.Parse(row.ItemArray[1].ToString()));
                }
            }

            return ds;
        }
    }
}
