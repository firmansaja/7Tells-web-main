using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPToolingModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public string CodeParent { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string Data2D { get; set; }
        public string DataRNECI { get; set; }
        public string DataGrain { get; set; }
        public string MoldFabricationLocation { get; set; }
        public string MoldProblem { get; set; }
        public string MoldStatus { get; set; }
        public DateTime? MoldTargetComplete { get; set; }
        public string ETFabricationLocation { get; set; }
        public string ETProblem { get; set; }
        public string ETStatus { get; set; }
        public DateTime? ETTargetComplete { get; set; }
        public string CFFabricationLocation { get; set; }
        public string CFProblem { get; set; }
        public string CFStatus { get; set; }
        public DateTime? CFTargetComplete { get; set; }
    }
}
