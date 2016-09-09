using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Entidades
{
    public class GraficaRuido
    {
        private int _diaDesde;
        private int _mesDesde;
        private int _anioDesde;
        private int _diaHasta;
        private int _mesHasta;
        private int _anioHasta;
        private string _elementos;
        private string _periodo;
        private string _codEstacion;
        private bool _esDiario;

        public int DiaDesde
        {
            get { return _diaDesde; }
            set { _diaDesde = value; }        
        }

        public int MesDesde
        {
            get { return _mesDesde; }
            set { _mesDesde = value; }
        }

        public int AnioDesde
        {
            get { return _anioDesde; }
            set { _anioDesde = value; }
        }

        public int DiaHasta
        {
            get { return _diaHasta; }
            set { _diaHasta = value; }
        }

        public int MesHasta
        {
            get { return _mesHasta; }
            set { _mesHasta = value; }
        }

        public string Elementos
        {
            get { return _elementos; }
            set { _elementos = value; }
        }

        public string Periodo
        {
            get { return _periodo; }
            set { _periodo = value; }
        }

        public string CodEstacion
        {
            get { return _codEstacion; }
            set { _codEstacion = value; }
        }

        public int AnioHasta
        {
            get { return _anioHasta; }
            set { _anioHasta = value; }
        }

        public bool EsDiario
        {
            get { return _esDiario; }
            set { _esDiario = value; }
        }

        public GraficaRuido()
        {
            DiaDesde = 0;
            MesDesde = 0;
            AnioDesde = 0;
            DiaHasta = 0;
            MesHasta = 0;
            AnioHasta = 0;
            Elementos = string.Empty;
            Periodo = string.Empty;
            CodEstacion = string.Empty;
            EsDiario = false;
        }
    }
}
