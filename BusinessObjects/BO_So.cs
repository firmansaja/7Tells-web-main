using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class BO_So
    {
        public string CustOrdNbr { get; set; }
        public string OrdNbr { get; set; }
        public string ShipName { get; set; }
        public string InvtID { get; set; }
        public string Descr { get; set; }
        public string AlternateID { get; set; }
        public DateTime ReqDate { get; set; }
        public double QtyOrd { get; set; }
        public double QtyOpenShip { get; set; }
        public double QtyCloseShip { get; set; }
        public double QtyShip { get; set; }
        public double QtyBal { get; set; }
        public string SiteID { get; set; }
        public string NoSPAB { get; set; }
        public string Tujuan { get; set; }
        public int No { get; set; }
    }
}
