using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class ApproveHistory
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string MenuCode { get; set; }
        public string DeptID { get; set; }
        public string NoTransaksi { get; set; }
        public int LevelApproved { get; set; }
        public string StatusApproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int IsActive { get; set; }
    }
}
