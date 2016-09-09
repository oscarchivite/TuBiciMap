using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnBici.Entidades
{
    public class Aire
    {
        private string _codEstacion01; //2 dígitos
        private string _codEstacion02; //3 dígitos
        private string _codEstacion03; //3 dígitos
        private string _magnitud;
        private string _tecnica;
        private string _periodo; //Un 02 indica que son mediciones diarias
        private int _anio;
        private int _mes;
        private int _dia;
        private Entidades.AireHoraValidez[] _horaValidez = new Entidades.AireHoraValidez[24];       
        private string _nombreEstacion;
        private decimal _latitud;
        private decimal _longitud;

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
        /// Atributo para almacenar el tipo de elemento a medir
        /// </summary>
        public string Magnitud
        {
            get { return _magnitud; }
            set { _magnitud = value; }
        }

        /// <summary>
        /// Atributo para almacenar la técnica de medida utilizada
        /// </summary>
        public string Tecnica
        {
            get { return _tecnica; }
            set { _tecnica = value; }
        }

        /// <summary>
        /// Atributo para almacenar el tipo de peridodo de medición, en nuestro caso siempre será el 02 - Diaria
        /// </summary>
        public string Periodo
        {
            get { return _periodo; }
            set { _periodo = value; }
        }

        /// <summary>
        /// Atributo para almacenar el año de la medición
        /// </summary>
        public int Anio
        {
            get { return _anio; }
            set { _anio = value; }
        }

        /// <summary>
        /// Atributo para almacenar el mes de la medición
        /// </summary>
        public int Mes
        {
            get { return _mes; }
            set { _mes = value; }
        }

        /// <summary>
        /// Atributo para almacenar el día de la medición
        /// </summary>
        public int Dia
        {
            get { return _dia; }
            set { _dia = value; }
        }

        public Entidades.AireHoraValidez[] HoraValidez
        {
            get { return _horaValidez; }
            set { _horaValidez = value; }
        }
        
        /// <summary>
        /// Atributo para almacenar el nombre de la estación de medida
        /// </summary>
        public string NombreEstacion
        {
            get { return _nombreEstacion; }
            set { _nombreEstacion = value; }
        }

        /// <summary>
        /// Atributo para almacenar la latitud en decimal 
        /// </summary>
        public decimal Latitud
        {
            get { return _latitud; }
            set { _latitud = value; }
        }

        /// <summary>
        /// Atributo para almacenar la longitud en decimal 
        /// </summary>
        public decimal Longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Aire()
        {
            CodEstacion01 = string.Empty; //2 dígitos
            CodEstacion02 = string.Empty; //3 dígitos
            CodEstacion03 = string.Empty; //3 dígitos
            Magnitud = string.Empty;
            Tecnica = string.Empty;
            Periodo = string.Empty; //Un 02 indica que son mediciones diarias
            Anio = 0;
            Mes = 0;
            Dia = 0;
            for (int i=0;i < HoraValidez.Count(); i++)
            {
                HoraValidez[i] = new Entidades.AireHoraValidez();               
            }
            NombreEstacion = string.Empty;
            Latitud = 0;
            Longitud = 0;
        }
    }
}