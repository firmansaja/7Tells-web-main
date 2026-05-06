using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPDocumentDetailModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public string CodeItem { get; set; }
        public string Item { get; set; }
        public string PIC { get; set; }
        public string Status { get; set; }
        public string Pending { get; set; }
        public DateTime? DueDate { get; set; }
        public string FileName { get; set; }
        public Byte FileData { get; set; }
        public int? No { get; set; }
        public bool IsEdit { get; set; }
    }
}
