using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPQualityModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public string CodeParent { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string CapabilityInjection { get; set; }
        public string CapabilityPainting { get; set; }
        public string CapabilityAssembling { get; set; }
        public double DataAccuracy { get; set; }
        public string DataN1 { get; set; }
        public string LimitSampleStatus { get; set; }
        public string LimitSampleKeterangan { get; set; }
        public string ProblemPart { get; set; }
        public string Remark { get; set; }
        public DateTime? TargetComplete { get; set; }
    }
}
