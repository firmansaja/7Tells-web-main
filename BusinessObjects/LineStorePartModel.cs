using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class LineStorePartModel
    {
        public string InvtId { get; set; }
        public string PartNo { get; set; }
        public string JobNo { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
        public int QtyMin { get; set; }
        public int QtyMax { get; set; }
        public int Delivery { get; set; }
        public double Ratio { get; set; }
        public string Status { get; set; }
    }

    public class LineStoreScanPartModel
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

    public class LineStorePunchModel
    {
        public string NoTrans { get; set; }
        public DateTime TglTrans { get; set; }
        public DateTime TglShift { get; set; }
        public int Shift { get; set; }
        public string InvtId { get; set; }
        public string InvtIdPunch { get; set; }
        public string PartNo { get; set; }
        public string JobNo { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
    }
}
