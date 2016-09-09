using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EnBici.Datos
{
    public class AireParametros
    {
        public static DataSet ObtenerAireParametros()
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("Bici");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerAireParametros]");

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
    }
}
