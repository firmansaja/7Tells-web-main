using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPPackingModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public string CodeParent { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string SPPS { get; set; }
        public string PackingSpec { get; set; }
        public string TypePacking { get; set; }
        public int Qty { get; set; }
        public string PackingStandard { get; set; }
        public int OrderPackingPlan { get; set; }
        public int OrderPackingActual { get; set; }
        public string Remark { get; set; }
        public DateTime? TargetComplete { get; set; }
    }
}
