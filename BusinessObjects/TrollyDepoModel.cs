using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class TrollyDepoModel
    {
        public int Id { get; set; }
        public string NoTrans { get; set; }
        public string NoTransTSC { get; set; }
        public string InOut { get; set; }
        public DateTime TglTrans { get; set; }
        public string CustId { get; set; }
        public string NoMobil { get; set; }
        public string NamaSupir { get; set; }
        public string Username { get; set; }
        public List<TrollyDepoDetailModel> Details { get; set; }
    }

    public class TrollyDepoDetailModel
    {
        public int Id { get; set; }
        public string NoTrans { get; set; }
        public string KodeQR { get; set; }
        public string Project { get; set; }
        public string TypeTrolly { get; set; }
        public string InvtId { get; set; }
        public string PartNo { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
        public string Satuan { get; set; }
    }

    public class TrollyDepoAllModel
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
        public string InvtId { get; set; }
        public string PartNo { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
        public string Satuan { get; set; }
    }

    public class FilterTrollyOutDepoModel
    {
        public DateTime? DTDari { get; set; }
        public DateTime? DTSampai { get; set; }
    }

    public class TrollyInOutDepoModel
    {
        public string NoTrans { get; set; }
        public string NoMobil { get; set; }
        public string KodeQR { get; set; }
        public DateTime TglTransDepo { get; set; }
        public string StatusDepo { get; set; }
        public DateTime TglTransTSC { get; set; }
        public string StatusTSC { get; set; }
    }

    public class TrollyInOutDepoToADMModel
    {
        public string NoTrans { get; set; }
        public DateTime TglTrans { get; set; }
        public DateTime TglShift { get; set; }
        public int Shift { get; set; }
        public int Line { get; set; }
        public string InvtId { get; set; }
        public string PartNo { get; set; }
        public string JobNo { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
    }

    public class TrollyStokDepoModel
    {
        public string InvtId { get; set; }
        public string PartName { get; set; }
        public string Type { get; set; }
        public string JobNo { get; set; }
        public int StdStok { get; set; }
        public int StokAwal { get; set; }
        public int MinusWOS { get; set; }
        public int PlanWOS { get; set; }
        public int OutTsc1 { get; set; }
        public int OutTsc2 { get; set; }
        public int InDepo1 { get; set; }
        public int InDepo2 { get; set; }
        public int OutAdm1 { get; set; }
        public int OutAdm2 { get; set; }
        public int OutDepo { get; set; }
        public int StokAkhir { get; set; }
    }
}
