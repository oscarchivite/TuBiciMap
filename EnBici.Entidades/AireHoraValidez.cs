using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Entidades
{
    public class AireHoraValidez
    {            
        private decimal? _hora;
        private string _validez;
        
        /// <summary>
        /// Atributo para almacenar el valor de la medición en la hora 11
        /// </summary>
        public decimal? Hora
        {
            get { return _hora; }
            set { _hora = value; }
        }

        /// <summary>
        /// Atributo para almacenar si la medición realizada en la hora 11 es válida
        /// </summary>
        public string Validez
        {
            get { return _validez; }
            set { _validez = value; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public AireHoraValidez()
        {
            Hora = null;
            Validez = "N";
        }        
    }
}
