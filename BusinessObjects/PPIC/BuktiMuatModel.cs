using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPIC
{
    public class BuktiMuatModel
    {
        public string NoUpload { get; set; }
        public int NoMobil { get; set; }
        public string NoMobilAktual { get; set; }
        public DateTime TglKirim { get; set; }
        public string Tujuan { get; set; }
        public string PartNo { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string Lokasi { get; set; }
        public string UserCode { get; set; }
        public string PF { get; set; }
        public string OrderNo { get; set; }
        public int OrderQty { get; set; }


    }
}
