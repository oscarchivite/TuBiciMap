using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Entidades
{
    public class Grafica
    {
        private GraficaAire _gAire;
        private GraficaRuido _gRuido;

        public GraficaAire GAire
        {
            get { return _gAire; }
            set { _gAire = value; }   
        }

        public GraficaRuido GRuido
        {
            get { return _gRuido; }
            set { _gRuido = value; }
        }

        public Grafica()
        { 
            GAire = new GraficaAire();
            GRuido = new GraficaRuido();
        }
    }
}
