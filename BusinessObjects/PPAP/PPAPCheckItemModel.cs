using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPCheckItemModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public int IdItem { get; set; }
        public string Item { get; set; }
        public bool IsCheck { get; set; }
        public string Countermeasure { get; set; }
        public string DueDate { get; set; }
        public string PIC { get; set; }
        public string Judge { get; set; }
        public string Remark { get; set; }
        public string Problem { get; set; }
    }
}
