using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPDetailModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public string CodeParent { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public string GroupParent { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public double Forecast { get; set; }
        public string Remark { get; set; }
        public string GroupID { get; set; }
    }
}
