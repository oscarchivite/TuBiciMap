using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Entidades
{
    public class RuidoPeriodo
    {
        private string _idPeriodo;
        private string _descripcionPeriodo;
        
        /// <summary>
        /// Atributo para almacenar el período de la toma de medida
        /// </summary>
        public string IdPeriodo
        {
            get { return _idPeriodo; }
            set { _idPeriodo = value; }
        }

        /// <summary>
        /// Atributo para almacenar la descripción del período de la toma de medida
        /// </summary>
        public string DescripcionPeriodo
        {
            get { return _descripcionPeriodo; }
            set { _descripcionPeriodo = value; }
        }

        public RuidoPeriodo()
        {
            IdPeriodo = string.Empty;
            DescripcionPeriodo = string.Empty;            
        }
    }
}
