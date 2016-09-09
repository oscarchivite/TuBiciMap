using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnBici.Entidades
{
    public class GraficaAire
    {
        private int _diaDesde;
        private int _mesDesde;
        private int _anioDesde;
        private int _diaHasta;
        private int _mesHasta;
        private int _anioHasta;
        private string _elementos;
        private string _hora;
        private bool _esDiario;
        private string _codEstacion03;

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

        public string Hora
        {
            get { return _hora; }
            set { _hora = value; }
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

        public string CodEstacion03
        {
            get { return _codEstacion03; }
            set { _codEstacion03 = value; }
        }

        public GraficaAire()
        {
            DiaDesde = 0;
            MesDesde = 0;
            AnioDesde = 0;
            DiaHasta = 0;
            MesHasta = 0;
            AnioHasta = 0;
            Elementos = string.Empty;
            Hora = string.Empty;
            EsDiario = false;
            CodEstacion03 = string.Empty;
        }
    }
}
