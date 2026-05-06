using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class TrollyModel
    {
        public int Id { get; set; }
        public string NoTrans { get; set; }
        public string NoTransDepo { get; set; }
        public string InOut { get; set; }
        public DateTime TglTrans { get; set; }
        public string CustId { get; set; }
        public string NoMobil { get; set; }
        public string NamaSupir { get; set; }
        public string Username { get; set; }
        public List<TrollyDetailModel> Details { get; set; }
    }

    public class TrollyDetailModel
    {
        public int Id { get; set; }
        public string NoTrans { get; set; }
        public string KodeQR { get; set; }
        public string Project { get; set; }
        public string TypeTrolly { get; set; }
        public string InvtId { get; set; }
        public string PartNo { get; set; }
        public string PartName { get; set; }
        public string Type { get; set; }
        public int Qty { get; set; }
        public string Satuan { get; set; }
    }

    public class TrollyAllModel
    {
        public int Id { get; set; }
        public string NoTrans { get; set; }
        public string InOut { get; set; }
        public DateTime TglTrans { get; set; }
        public string CustId { get; set; }
        public string NoMobil { get; set; }
        public string NamaSupir { get; set; }
        public string KodeQR { get; set; }
        public string Project { get; set; }
        public string TypeTrolly { get; set; }
    }

    public class TrollyInOutModel
    {
        public string NoTrans { get; set; }
        public string NoMobil { get; set; }
        public string KodeQR { get; set; }
        public DateTime TglTransTSC { get; set; }
        public string StatusTSC { get; set; }
        public int QtyTrollyTSC { get; set; }
        public int QtyPartTSC { get; set; }
        public int Qty { get; set; }
        public DateTime TglTransDepo { get; set; }
        public string StatusDepo { get; set; }
        public int QtyTrollyDepo { get; set; }
        public int QtyPartDepo { get; set; }
    }

    public class TrollyInOutDetailModel
    {
        public string NoTrans { get; set; }
        public string NoMobil { get; set; }
        public string NamaSupir { get; set; }
        public string KodeQR { get; set; }
        public string TypeTrolly { get; set; }
        public string Project { get; set; }
        public DateTime TglTransTSC { get; set; }
        public string InvtIdTSC { get; set; }
        public string PartNameTSC { get; set; }
        public string ColorTSC { get; set; }
        public int QtyTrollyTSC { get; set; }
        public int QtyPartTSC { get; set; }
        public int Qty { get; set; }
        public string StatusTSC { get; set; }
        public DateTime TglTransDepo { get; set; }
        public string InvtIdDepo { get; set; }
        public string PartNameDepo { get; set; }
        public int QtyTrollyDepo { get; set; }
        public int QtyPartDepo { get; set; }
        public string StatusDepo { get; set; }
    }
}
