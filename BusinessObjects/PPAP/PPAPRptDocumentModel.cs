using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPRptDocumentModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Numbering { get; set; }
        public string Code { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string InvtID { get; set; }
        public int? No { get; set; }
        public string CodeItem { get; set; }
        public string Item { get; set; }
        public string PIC { get; set; }
        public bool IsEdit { get; set; }
        public string Status { get; set; }
        public string Pending { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
