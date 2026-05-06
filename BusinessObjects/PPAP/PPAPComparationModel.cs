using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPComparationModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public string CodeParent { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string TonaseMCMoldSpec { get; set; }
        public string TonaseMCActual { get; set; }
        public string TonaseMCNo { get; set; }
        public string CycleTimeSpec { get; set; }
        public string CycleTimePlus { get; set; }
        public string CycleTimeActual { get; set; }
        public string PartWeightMinus { get; set; }
        public string PartWeight2DData { get; set; }
        public string PartWeightPlus { get; set; }
        public string PartWeightActual { get; set; }
    }
}
