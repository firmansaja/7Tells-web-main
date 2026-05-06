using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPHeaderModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Status { get; set; }
        public string Site { get; set; }
        public string CustID { get; set; }
        public string ProjectName { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public DateTime MassproductionDate { get; set; }
        public string OverallJudgment { get; set; }
        public string OverallComment { get; set; }
        public string Item { get; set; }
        public string NoNPP { get; set; }
        public string NoNPWO { get; set; }
        public bool IsEdit { get; set; }
    }
}
