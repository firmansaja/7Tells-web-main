using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPApprovalDeptModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Dept { get; set; }
        public string Descr { get; set; }
        public string Site { get; set; }
        public string Username { get; set; }
        public string Nama { get; set; }
        public string NIK { get; set; }
        public string Jabatan { get; set; }
        public string Judgment { get; set; }
        public string Comment { get; set; }
        public bool IsCheck { get; set; }
        public DateTime? Tanggal { get; set; }
    }
}
