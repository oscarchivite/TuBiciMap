using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnBici.Entidades
{
    public class LogEntidad
    {
        private string _log;
        private string _fichero;
        private bool _descargaCorrecta;
        private bool _procesoCorrecto;
        private DateTime _fechaInicioProceso;
        private DateTime _fechaFinProceso;

        /// <summary>
        /// Atributo para almacenar los posibles errores producidos en la ejecución del servicio
        /// </summary>
        public string log
        {
            get { return _log; }
            set { _log = value; }
        }

        /// <summary>
        /// Atributo para almacenar el fichero en base 64
        /// </summary>
        public string fichero
        {
            get { return _fichero; }
            set { _fichero = value; }
        }

        /// <summary>
        /// Atributo para almacenar si la descarga del fichero desde la web de ayuntamiento ha sido correcta
        /// </summary>
        public bool descargaCorrecta
        {
            get { return _descargaCorrecta; }
            set { _descargaCorrecta = value; }
        }

        /// <summary>
        /// Atributo para almacenar si la ejecución del servicio ha sido correcto
        /// </summary>
        public bool procesoCorrecto
        {
            get { return _procesoCorrecto; }
            set { _procesoCorrecto = value; }
        }

        /// <summary>
        /// Atributo para almacenar la fecha de la ejecución del servicio
        /// </summary>
        public DateTime fechaInicioProceso
        {
            get { return _fechaInicioProceso; }
            set { _fechaInicioProceso = value; }
        }

        /// <summary>
        /// Atributo para almacenar la fecha de la ejecución del servicio
        /// </summary>
        public DateTime fechaFinProceso
        {
            get { return _fechaFinProceso; }
            set { _fechaFinProceso = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogEntidad()
        {
            log = string.Empty;

        }
    }
}

