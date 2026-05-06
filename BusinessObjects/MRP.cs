using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class MRP
    {

    }

    public class MRPForecastCust
    {
        public string CustID { get; set; }
        public string Customer { get; set; }
        public int FCJan { get; set; }
        public int FCFeb { get; set; }
        public int FCMar { get; set; }
        public int FCApr { get; set; }
        public int FCMei { get; set; }
        public int FCJun { get; set; }
        public int FCJul { get; set; }
        public int FCAgt { get; set; }
        public int FCSep { get; set; }
        public int FCOkt { get; set; }
        public int FCNov { get; set; }
        public int FCDes { get; set; }
    }

    public class MRPForecastModel
    {
        public string CustID { get; set; }
        public string Customer { get; set; }
        public string Model { get; set; }
        public int FCJan { get; set; }
        public int FCFeb { get; set; }
        public int FCMar { get; set; }
        public int FCApr { get; set; }
        public int FCMei { get; set; }
        public int FCJun { get; set; }
        public int FCJul { get; set; }
        public int FCAgt { get; set; }
        public int FCSep { get; set; }
        public int FCOkt { get; set; }
        public int FCNov { get; set; }
        public int FCDes { get; set; }
    }

    public class MRPForecastDetail
    {
        public string CustID { get; set; }
        public string Customer { get; set; }
        public string Model { get; set; }
        public string InvtID { get; set; }
        public string Nama { get; set; }
        public int FCJan { get; set; }
        public int FCFeb { get; set; }
        public int FCMar { get; set; }
        public int FCApr { get; set; }
        public int FCMei { get; set; }
        public int FCJun { get; set; }
        public int FCJul { get; set; }
        public int FCAgt { get; set; }
        public int FCSep { get; set; }
        public int FCOkt { get; set; }
        public int FCNov { get; set; }
        public int FCDes { get; set; }
    }

    public class MRPBoMCalculate
    {
        public string Account { get; set; }
        public string Site { get; set; }
        public string InvtID { get; set; }
        public string Nama { get; set; }
        public string Unit { get; set; }
        public double FCJan { get; set; }
        public double FCFeb { get; set; }
        public double FCMar { get; set; }
        public double FCApr { get; set; }
        public double FCMei { get; set; }
        public double FCJun { get; set; }
        public double FCJul { get; set; }
        public double FCAgt { get; set; }
        public double FCSep { get; set; }
        public double FCOkt { get; set; }
        public double FCNov { get; set; }
        public double FCDes { get; set; }
    }

    public class MRPBoMCalculateDetail
    {
        public string ParentID { get; set; }
        public string ParentIDLvl0 { get; set; }
        public string Desc { get; set; }
        public string Unit { get; set; }
        public double FCJan { get; set; }
        public double FCFeb { get; set; }
        public double FCMar { get; set; }
        public double FCApr { get; set; }
        public double FCMei { get; set; }
        public double FCJun { get; set; }
        public double FCJul { get; set; }
        public double FCAgt { get; set; }
        public double FCSep { get; set; }
        public double FCOkt { get; set; }
        public double FCNov { get; set; }
        public double FCDes { get; set; }
    }
}
