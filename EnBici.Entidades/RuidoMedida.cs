using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Entidades
{
    public class RuidoMedida
    {
        private string _periodo;
        private string _descripcionPeriodo;
        private decimal _LAEQ;
        private decimal _LAS01;
        private decimal _LAS10;
        private decimal _LAS50;
        private decimal _LAS90;
        private decimal _LAS99;

        /// <summary>
        /// Atributo para almacenar el periodo de medición
        /// </summary>
        public string Periodo
        {
            get { return _periodo; }
            set { _periodo = value; }
        }

        /// <summary>
        /// Atributo para almacenar el periodo de medición
        /// </summary>
        public string DescripcionPeriodo
        {
            get { return _descripcionPeriodo; }
            set { _descripcionPeriodo = value; }
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
        /// Constructor
        /// </summary>
        public RuidoMedida()
        {
            Periodo = string.Empty;
            DescripcionPeriodo = string.Empty;
            LAEQ = 0;
            LAS01 = 0;
            LAS10 = 0;
            LAS50 = 0;
            LAS90 = 0;
            LAS99 = 0;
        }
    }
}
