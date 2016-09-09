using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Entidades
{
    public class RuidoEstaciones
    {
        private int _codEstacion;
        private string _nombre;
        private string _direccion;
        private decimal _longitud;
        private decimal _latitud;
        private decimal _altitud;
        private int _idRuidoObjetivo;
        private decimal? _nivelDiurno;
        private decimal? _nivelNocturno;
        private string _descripcionNivel;

        /// <summary>
        /// Atributo para almacenar el código de la estación de medición
        /// </summary>
        public int CodEstacion
        {
            get { return _codEstacion; }
            set { _codEstacion = value; }
        }       

        /// <summary>
        /// Atributo para almacenar el nombre de la estación de medida
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Atributo para almacenar la dirección de la estación de medida.
        /// </summary>
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        /// <summary>
        /// Atributo para almacenar la longitud en formato decimal de la estación de medida
        /// </summary>
        public decimal Longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }

        /// <summary>
        /// Atributo para almacenar la latitud en formato decimal de la estación de medida.
        /// </summary>
        public decimal Latitud
        {
            get { return _latitud; }
            set { _latitud = value; }
        }

        /// <summary>
        /// Atributo para almacenar la altitud en formato decimal de la estación de medida.
        /// </summary>
        public decimal Altitud
        {
            get { return _altitud; }
            set { _altitud = value; }
        }

        /// <summary>
        /// Atributo para almacenar el identificador del ruido objetivo
        /// </summary>
        public int IdRuidoObjetivo
        {
            get { return _idRuidoObjetivo; }
            set { _idRuidoObjetivo = value; }
        }

        /// <summary>
        /// Atributo para almacenar el valor máximo de ruido diurno
        /// </summary>
        public decimal? NivelDiurno
        {
            get { return _nivelDiurno; }
            set { _nivelDiurno= value; }
        }

        /// <summary>
        /// Atributo para almacenar el valor máximo de ruido nocturno
        /// </summary>
        public decimal? NivelNocturno
        {
            get { return _nivelNocturno; }
            set { _nivelNocturno = value; }
        }

        /// <summary>
        /// Atributo para almacenar el texto descriptivo del valor objetivo
        /// </summary>
        public string DescripcionNivel
        {
            get { return _descripcionNivel; }
            set { _descripcionNivel = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RuidoEstaciones()
        {
            CodEstacion = 0;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Longitud = 0;
            Latitud = 0;
            Altitud = 0;
            IdRuidoObjetivo = 0;
            NivelDiurno = 0;
            NivelNocturno = 0;
            DescripcionNivel = string.Empty;
        }
    }
}
