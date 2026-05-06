using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPPaintingDetailModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public string CodeItem { get; set; }
        public int NoUrut { get; set; }
        public string Item { get; set; }
        public string SubItem { get; set; }
        public string ProductName { get; set; }
        public double? Spec { get; set; }
        public double? Actual { get; set; }
    }
}
