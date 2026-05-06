using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.PPAP
{
    public class PPAPPaintingModel
    {
        public string NoPPAP { get; set; }
        public int IdStatus { get; set; }
        public string Code { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string ColorCode { get; set; }
        public string PaintMaker { get; set; }
        public string ToolsEquipments { get; set; }
        public string FollowDocumentStandard { get; set; }
        public double? CTMaskingSpec { get; set; }
        public double? CTMaskingActual { get; set; }
        public string Judgement { get; set; }
    }
}
