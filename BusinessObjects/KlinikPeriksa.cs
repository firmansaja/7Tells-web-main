using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class KlinikPeriksa
    {
        public string NoTrans { get; set; }
        public DateTime Tanggal { get; set; }
        public string NIK { get; set; }
        public string Nama { get; set; }
        public string Dept { get; set; }
        public string NamaDokter { get; set; }
        public string Diagnosa { get; set; }
        public int TotalObat { get; set; }
        public double TotalHargaObat { get; set; }
    }

    public class KlinikPakaiObat
    {
        public string NoTrans { get; set; }
        public string KodeObat { get; set; }
        public string NamaObat { get; set; }
        public string Jumlah { get; set; }
        public string HargaSatuan { get; set; }
        public string HargaTotal { get; set; }
    }
}
