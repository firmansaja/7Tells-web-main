using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPSupplierModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public int Seq { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string PIS { get; set; }
        public string CPFP { get; set; }
        public string IRDCheckSheet { get; set; }
        public string N1Data { get; set; }
        public string LimitSample { get; set; }
        public string PackingSpec { get; set; }
        public int PackingQty { get; set; }
        public string Remark { get; set; }
        public DateTime? TargetComplete { get; set; }
    }
}
