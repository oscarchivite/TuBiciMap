using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnBici.Entidades
{
    public class Ruido
    {
        private int _codEstacion;
        private string _nombre;
        private int _anio;
        private int _mes;
        private int _dia;
        private DateTime _fecha;    
        private decimal _latitud;
        private decimal _longitud;
        private string _periodo;
        private decimal _LAEQ;
        private decimal _LAS01;
        private decimal _LAS10;
        private decimal _LAS50;
        private decimal _LAS90;
        private decimal _LAS99;
        private List<RuidoMedida> _medida;

        /// <summary>
        /// Atributo para almacenar el código de la estación de medición
        /// </summary>
        public int CodEstacion
        {
            get { return _codEstacion; }
            set { _codEstacion = value; }
        }

        /// <summary>
        /// Atributo para almacenar el año de la toma de medida
        /// </summary>
        public int Anio
        {
            get { return _anio; }
            set { _anio = value; }
        }

        /// <summary>
        /// Atributo para almacenar el mes de la toma de medida
        /// </summary>
        public int Mes
        {
            get { return _mes; }
            set { _mes = value; }
        }

        /// <summary>
        /// Atributo para almacenar el día de la toma de medida
        /// </summary>
        public int Dia
        {
            get { return _dia; }
            set { _dia = value; }
        }   
          
        /// <summary>
        /// 
        /// </summary>
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
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
        /// Atributo para almacenar la latitud en formato decimal de la estación de medida.
        /// </summary>
        public decimal Latitud
        {
            get { return _latitud; }
            set { _latitud = value; }
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
        /// Atributo para almacenar el periodo de medición
        /// </summary>
        public string Periodo
        {
            get { return _periodo; }
            set { _periodo = value; }
        }

        /// <summary>
        /// Nivel continuo equivalente con ponderación frecuencial A, que es el nivel de ruido supuesto 
        /// constante y continuo a lo largo de un periodo de tiempo, que se corresponde con la misma 
        /// cantidad de energía que aquel nivel real variable medido en el mismo periodo.
        /// </summary>
        public decimal LAEQ
        {
            get { return _LAEQ; }
            set { _LAEQ = value; }
        }

        /// <summary>
        /// Es el nivel de presión sonora con ponderación frecuencial A y ponderación temporal Slow, que se  sobrepasa durante el 1% del tiempo de observación.
        /// </summary>
        public decimal LAS01
        {
            get { return _LAS01; }
            set { _LAS01 = value; }
        }

        /// <summary>
        /// Es el nivel de presión sonora con ponderación frecuencial A y ponderación temporal Slow, que se sobrepasa durante el 10% del tiempo de observación.
        /// </summary>
        public decimal LAS10
        {
            get { return _LAS10; }
            set { _LAS10 = value; }
        }

        /// <summary>
        /// Es el nivel presión sonora con ponderación frecuencial A y ponderación temporal Slow, que se sobrepasa durante el 50% del tiempo de observación.
        /// </summary>
        public decimal LAS50
        {
            get { return _LAS50; }
            set { _LAS50 = value; }
        }

        /// <summary>
        /// Es el nivel de presión sonora con ponderación frecuencial A y ponderación temporal Slow, que se sobrepasa durante el 90% del tiempo de observación.
        /// </summary>
        public decimal LAS90
        {
            get { return _LAS90; }
            set { _LAS90 = value; }
        }

        /// <summary>
        /// Es nivel de presión sonora con ponderación frecuencial A y ponderación temporal Slow, que se sobrepasa durante el 99% del tiempo de observación.
        /// </summary>
        public decimal LAS99
        {
            get { return _LAS99; }
            set { _LAS99 = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public List<RuidoMedida> Medida
        {
            get { return _medida; }
            set { _medida = value; }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public Ruido()
        {
            CodEstacion = 0;
            Anio = 0;
            Mes = 0;
            Dia = 0;
            Fecha = DateTime.MinValue;
            Nombre = string.Empty;
            Latitud = 0;
            Longitud = 0;
            Periodo = string.Empty;
            LAEQ = 0;
            LAS01 = 0;
            LAS10 = 0;
            LAS50 = 0;
            LAS90 = 0;
            LAS99 = 0;
            Medida = new List<RuidoMedida>();
        }
    }
}