using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnBici.Entidades
{
    public class AireParametros
    {
        private string _codParametro;
        private string _descripcion;
        private string _abreviatura;
        private DateTime _fechaMedida;
        private decimal? _medida;
        private string _unidadMedida;
        private string _idTecnicaMedida;
        private string _descripcionMedida;
        private decimal? _nivelBueno;
        private decimal? _nivelModerado;
        private decimal? _nivelDeficiente;
        private decimal? _nivelMalo;

        /// <summary>
        /// Atributo para almacenar el identificador del parámetro
        /// </summary>
        public string CodParametro
        {
            get { return _codParametro; }
            set { _codParametro = value; }
        }

        /// <summary>
        /// Atributo para almacenar la descripción del parámetro
        /// </summary>
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        /// <summary>
        /// Atributo para almacenar la abreviatura
        /// </summary>
        public string Abreviatura
        {
            get { return _abreviatura; }
            set { _abreviatura = value; }
        }

        public DateTime FechaMedida
        {
            get { return _fechaMedida; }
            set { _fechaMedida = value; }
        }

        /// <summary>
        /// Atributo para almacenar la medida
        /// </summary>
        public decimal? Medida
        {
            get { return _medida; }
            set { _medida = value; }
        }

        /// <summary>
        /// Atributo para almacenar la unidad de medida
        /// </summary>
        public string UnidadMedida
        {
            get { return _unidadMedida; }
            set { _unidadMedida = value; }
        }

        /// <summary>
        /// Atributo para almacenar la técnica de medida
        /// </summary>
        public string IdTecnicaMedida
        {
            get { return _idTecnicaMedida; }
            set { _idTecnicaMedida = value; }
        }

        /// <summary>
        /// Atributo para almacenar la técnica de medida
        /// </summary>
        public string DescripcionMedida
        {
            get { return _descripcionMedida; }
            set { _descripcionMedida = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? NivelBueno
        {
            get { return _nivelBueno; }
            set { _nivelBueno = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? NivelModerado
        {
            get { return _nivelModerado ; }
            set { _nivelModerado = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? NivelDeficiente
        {
            get { return _nivelDeficiente; }
            set { _nivelDeficiente = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? NivelMalo
        {
            get { return _nivelMalo; }
            set { _nivelMalo = value; }
        }

        public AireParametros()
        {
            CodParametro = string.Empty;
            Descripcion = string.Empty;
            Abreviatura = string.Empty;
            FechaMedida = DateTime.MinValue;
            Medida = 0;
            UnidadMedida = string.Empty;
            IdTecnicaMedida = string.Empty;
            DescripcionMedida = string.Empty;
            NivelBueno = 0;
            NivelModerado = 0;
            NivelDeficiente = 0;
            NivelMalo = 0;
        }
    }
}
