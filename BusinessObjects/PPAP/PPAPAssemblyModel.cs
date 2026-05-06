using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPAssemblyModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public int Seq { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string ComponentPart { get; set; }
        public double? Qty { get; set; }
        public string ProcessName { get; set; }
        public int? ManPower { get; set; }
        public double? Actual { get; set; }
        public double? Spec { get; set; }
    }
}
