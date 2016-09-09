using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Entidades
{
    public class Marcadores
    {
        private string _nombreEstacion;
        private string _codEstacion01;
        private string _codEstacion02;
        private string _codEstacion03;
        private string _latitud; 
        private string _longitud;
        private string _icono;
        private string _contenido;
        
        /// <summary>
        ///
        /// </summary>
        public string NombreEstacion
        {
            get { return _nombreEstacion; }
            set { _nombreEstacion = value; }
        }
               

        /// <summary>
        ///
        /// </summary>
        public string CodEstacion01
        {
            get { return _codEstacion01; }
            set { _codEstacion01 = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public string CodEstacion02
        {
            get { return _codEstacion02; }
            set { _codEstacion02 = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public string CodEstacion03
        {
            get { return _codEstacion03; }
            set { _codEstacion03 = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Latitud
        {
            get { return _latitud; }
            set { _latitud = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Icono
        {
            get { return _icono; }
            set { _icono = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Contenido
        {
            get { return _contenido; }
            set { _contenido = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Marcadores()
        {
            NombreEstacion = string.Empty; 
            Latitud = string.Empty; 
            Longitud = string.Empty; 
            Icono = string.Empty;
            Contenido = string.Empty;           
        }
    }
}
