using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Negocio
{
    public class AireCalendario
    {
        /// <summary>
        /// Método para obtener los valores de las horas para el combo
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerAireHoras()
        {
            return Datos.AireCalendario.ObtenerAireHoras();              
        }

        /// <summary>
        /// Método para obtener los valores de los periodos de medición de ruido
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerRuidoPeriodo()
        {
            return Datos.AireCalendario.ObtenerRuidoPeriodo();
        }

        /// <summary>
        /// Método para obtener los valores de los meses para el combo en función de los meses existentes de mediciones de la calidad del aire
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerAireMeses()
        {
            return Datos.AireCalendario.ObtenerAireMeses();
        }

        /// <summary>
        /// Método para obtener los valores de los años para el combo en función de los año existentes de mediciones de la calidad del aire
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerAireAnios()
        {
            return Datos.AireCalendario.ObtenerAireAnios();
        }

        /// <summary>
        /// Método para obtener los valores de los meses para el combo en función de los meses existentes de mediciones de ruido
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerRuidoMeses()
        {
            return Datos.AireCalendario.ObtenerRuidoMeses();
        }

        /// <summary>
        /// Método para obtener los valores de los años para el combo en función de los años existentes de mediciones de ruido
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerRuidoAnios()
        {
            return Datos.AireCalendario.ObtenerRuidoAnios();
        }

        public static object ObtenerAireEstaciones()
        {
            return Datos.AireCalendario.ObtenerAireEstaciones();
        }

        public static object ObtenerRuidoEstaciones()
        {
            return Datos.AireCalendario.ObtenerRuidoEstaciones();
        }
    }
}
