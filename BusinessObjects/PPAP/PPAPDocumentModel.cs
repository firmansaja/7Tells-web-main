using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPDocumentModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public string CodeParent { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string PIS { get; set; }
        public string CPFP { get; set; }
        public string PFMEA { get; set; }
        public string IRDCheckSheet { get; set; }
        public string SCPInjection { get; set; }
        public string SCPPainting { get; set; }
        public string SCPAssembling { get; set; }
        public string InvtID { get; set; }
        public string Remark { get; set; }
        public DateTime? TargetComplete { get; set; }
    }
}
