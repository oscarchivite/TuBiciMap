using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Datos
{
    public class AireCalendario
    {
        /// <summary>
        /// Método para obtener las horas para cargarlas en el combo de horas
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerAireHoras()
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerAireHoras]");

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// Método para obtener los peridos de medición del ruido
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerRuidoPeriodo()
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerRuidoPeriodo]");

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// Procedimiento para obtener los años existentes en base de datos para el combo de los años
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerAireAnios()
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerAireAnios]");

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// Método para obtener las estaciones de medición de la calidad del aire
        /// </summary>
        /// <returns></returns>
        public static object ObtenerAireEstaciones()
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerAireEstacionesGrafica]");

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// Procedimiento para obtener los meses existentes en base de datos para el combo de los meses
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerAireMeses()
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerAireMeses]");

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// Procedimiento para obtener los años existentes en base de datos para el combo de los años
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerRuidoAnios()
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerRuidoAnios]");

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// Procedimiento para obtener los meses existentes en base de datos para el combo de los meses
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerRuidoMeses()
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerRuidoMeses]");

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// Método para obtener las estaciones de medición del nivel de ruido
        /// </summary>
        /// <returns></returns>
        public static object ObtenerRuidoEstaciones()
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerRuidoEstacionesGrafica]");

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
    }
}
