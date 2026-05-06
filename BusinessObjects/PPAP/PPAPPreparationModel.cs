using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPPreparationModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public string CodeParent { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string Material { get; set; }
        public string ProsesPlanInjection { get; set; }
        public string ProsesPlanPainting { get; set; }
        public string ProsesPlanAssembling { get; set; }
        public string Machine { get; set; }
        public string ChuckRobot { get; set; }
        public string Tools { get; set; }
        public string JigPainting { get; set; }
        public string JigAssy { get; set; }
        public string MejaKerja { get; set; }
        public string MappingSkillInjection { get; set; }
        public string MappingSkillPainting { get; set; }
        public string MappingSkillAssembling { get; set; }
        public string Remark { get; set; }
        public DateTime? TargetComplete { get; set; }
    }
}
