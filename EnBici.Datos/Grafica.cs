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
    public class Grafica
    {
        /// <summary>
        /// Método para obtener los datos buscados sobre calidad del aire de forma diaria
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerInformacionCalidadAireDiario(Entidades.Grafica oGrafica)
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerAireGraficaDiario]");
            db.AddInParameter(dbCommand, "AnioDesde", DbType.Int32, oGrafica.GAire.AnioDesde);
            db.AddInParameter(dbCommand, "MesDesde", DbType.Int32, oGrafica.GAire.MesDesde);
            db.AddInParameter(dbCommand, "DiaDesde", DbType.Int32, oGrafica.GAire.DiaDesde);
            db.AddInParameter(dbCommand, "AnioHasta", DbType.Int32, oGrafica.GAire.AnioHasta);
            db.AddInParameter(dbCommand, "MesHasta", DbType.Int32, oGrafica.GAire.MesHasta);
            db.AddInParameter(dbCommand, "DiaHasta", DbType.Int32, oGrafica.GAire.DiaHasta);
            db.AddInParameter(dbCommand, "Elementos", DbType.String, oGrafica.GAire.Elementos);
            db.AddInParameter(dbCommand, "CodEstacion03", DbType.String, oGrafica.GAire.CodEstacion03);
            db.AddInParameter(dbCommand, "Hora", DbType.String, oGrafica.GAire.Hora);
            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        /// <summary>
        /// Método para obtener los datos buscados sobre calidad del aire de forma mensual
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerInformacionCalidadAireMensual(Entidades.Grafica oGrafica)
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerAireGraficaMensual]");
            db.AddInParameter(dbCommand, "AnioDesde", DbType.Int32, oGrafica.GAire.AnioDesde);
            db.AddInParameter(dbCommand, "MesDesde", DbType.Int32, oGrafica.GAire.MesDesde);
            db.AddInParameter(dbCommand, "AnioHasta", DbType.Int32, oGrafica.GAire.AnioHasta);
            db.AddInParameter(dbCommand, "MesHasta", DbType.Int32, oGrafica.GAire.MesHasta);
            db.AddInParameter(dbCommand, "Elementos", DbType.String, oGrafica.GAire.Elementos);
            db.AddInParameter(dbCommand, "CodEstacion03", DbType.String, oGrafica.GAire.CodEstacion03);
            db.AddInParameter(dbCommand, "Hora", DbType.String, oGrafica.GAire.Hora);
            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        /// <summary>
        /// Método para obtener los datos buscados sobre nivel de ruido de forma mensual
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerInformacionNivelRuidoMensual(Entidades.Grafica oGrafica)
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerRuidoGraficaMensual]");
            db.AddInParameter(dbCommand, "IdEstacion", DbType.Int32, oGrafica.GRuido.CodEstacion);
            db.AddInParameter(dbCommand, "AnioDesde", DbType.Int32, oGrafica.GRuido.AnioDesde);
            db.AddInParameter(dbCommand, "MesDesde", DbType.Int32, oGrafica.GRuido.MesDesde);
            db.AddInParameter(dbCommand, "AnioHasta", DbType.Int32, oGrafica.GRuido.AnioHasta);
            db.AddInParameter(dbCommand, "MesHasta", DbType.Int32, oGrafica.GRuido.MesHasta);
            //db.AddInParameter(dbCommand, "Elementos", DbType.String, oGrafica.GRuido.Elementos);
            //db.AddInParameter(dbCommand, "IdPeriodo", DbType.String, oGrafica.GRuido.Periodo);

            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }


        /// <summary>
        /// Método para obtener los datos buscados sobre el nivel de ruido de forma diaria
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerInformacionNivelRuidoDiario(Entidades.Grafica oGrafica)
        {
            DataSet ds;
            //*******************************************
            //Creamos la conexion a la BBDD y realizamos la llamada al
            //procedimiento almacenado
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database db = factory.Create("BICI");
            DbCommand dbCommand = db.GetStoredProcCommand("[dbo].[RObtenerRuidoGraficaDiario]");
            db.AddInParameter(dbCommand, "IdEstacion", DbType.Int32, oGrafica.GRuido.CodEstacion);
            db.AddInParameter(dbCommand, "AnioDesde", DbType.Int32, oGrafica.GRuido.AnioDesde);
            db.AddInParameter(dbCommand, "MesDesde", DbType.Int32, oGrafica.GRuido.MesDesde);
            db.AddInParameter(dbCommand, "DiaDesde", DbType.String, oGrafica.GRuido.DiaDesde);
            db.AddInParameter(dbCommand, "AnioHasta", DbType.Int32, oGrafica.GRuido.AnioHasta);
            db.AddInParameter(dbCommand, "MesHasta", DbType.Int32, oGrafica.GRuido.MesHasta);
            db.AddInParameter(dbCommand, "DiaHasta", DbType.String, oGrafica.GRuido.DiaHasta);
            //db.AddInParameter(dbCommand, "Elementos", DbType.String, oGrafica.GRuido.Elementos);
            db.AddInParameter(dbCommand, "IdPeriodo", DbType.String, oGrafica.GRuido.Periodo);
            //Ejecución del procedimiento almacenado
            ds = db.ExecuteDataSet(dbCommand);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
    }
}
