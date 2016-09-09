using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Entidades
{    public class AireEstaciones
    {
        private string _codEstacion01; //2 dígitos
        private string _codEstacion02; //3 dígitos
        private string _codEstacion03; //3 dígitos
        private string _codTipoEstacion;
        private string _nombre;
        private string _direccion;
        private decimal _longitud;
        private decimal _latitud;
        
        /// <summary>
        /// Atributo para almacenar los dos primeros dígitos de la estación
        /// </summary>
        public string CodEstacion01
        {
            get { return _codEstacion01; }
            set { _codEstacion01 = value; }
        }

        /// <summary>
        /// Atributo para almacenar los tres siguientes dígitos de la estación
        /// </summary>
        public string CodEstacion02
        {
            get { return _codEstacion02; }
            set { _codEstacion02 = value; }
        }

        /// <summary>
        /// Atributo para almacenar los tres últimos dígitos de la estación
        /// </summary>
        public string CodEstacion03
        {
            get { return _codEstacion03; }
            set { _codEstacion03 = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CodTipoEstacion
        {
            get { return _codTipoEstacion; }
            set { _codTipoEstacion = value; }
        }

        /// <summary>
        /// Atributo para almacenar la técnica de medida utilizada
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Atributo para almacenar la dirección de la estación de medición de aire
        /// </summary>
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        /// <summary>
        /// Atributo para almacenar la longitud de la posición de la estación de aire
        /// </summary>
        public decimal Longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }

        /// <summary>
        /// Atributo para almacenar la latitud de la posición de la estación de aire 
        /// </summary>
        public decimal Latitud
        {
            get { return _latitud; }
            set { _latitud = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AireEstaciones()
        {
            CodEstacion01 = string.Empty; //2 dígitos
            CodEstacion02 = string.Empty; //3 dígitos
            CodEstacion03 = string.Empty; //3 dígitos
            CodTipoEstacion = string.Empty;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Longitud = 0;
            Latitud = 0;
        }
    }
}
