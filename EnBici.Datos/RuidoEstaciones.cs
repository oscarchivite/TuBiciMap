using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EnBici.Datos
{
    public class RuidoEstaciones
    {
        public static DataSet ObtenerRuidoEstaciones()
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("Bici");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerRuidoEstaciones]");

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
    }
}
