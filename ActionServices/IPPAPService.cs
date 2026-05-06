using BusinessObjects.PPAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface IPPAPService
    {
        List<PPAPHeaderModel> GetPPAP();

        PPAPHeaderModel GetPPAPById(string noPPAP, string idStatus);

        List<PPAPDetailModel> GetPPAPDetailById(string noPPAP, string idStatus);

        List<PPAPCheckItemModel> GetPPAPCheckItemById(string noPPAP, string idStatus);

        List<PPAPAksesDeptModel> GetPPAPAksesDept(string username);

        List<PPAPAttendanceModel> GetPPAPAttendanceById(string noPPAP, string idStatus);

        List<PPAPApprovalDeptModel> GetPPAPApprovalDeptById(string noPPAP, string idStatus);

        List<PPAPToolingModel> GetPPAPToolingById(string noPPAP, string idStatus);

        List<PPAPQualityModel> GetPPAPQualityById(string noPPAP, string idStatus);

        List<PPAPPreparationModel> GetPPAPPreparationById(string noPPAP, string idStatus);

        List<PPAPDocumentModel> GetPPAPDocumentById(string noPPAP, string idStatus);

        List<PPAPDocumentDetailModel> GetPPAPDocumentDetailByItem(string noPPAP, string idStatus, string code);

        List<PPAPRptDocumentModel> GetPPAPRptDocumentById(string noPPAP, string idStatus);

        List<PPAPSupplierModel> GetPPAPSupplierById(string noPPAP, string idStatus);

        List<PPAPPackingModel> GetPPAPPackingById(string noPPAP, string idStatus);

        List<PPAPComparationModel> GetPPAPComparationById(string noPPAP, string idStatus);

        List<PPAPPaintingModel> GetPPAPPaintingById(string noPPAP, string idStatus);

        List<PPAPPaintingDetailModel> GetPPAPPaintingDetailById(string noPPAP, string idStatus);

        List<PPAPPaintingDetailModel> GetPPAPPaintingDetailByItem(string noPPAP, string idStatus, string code);

        List<PPAPAssemblyModel> GetPPAPAssemblyById(string noPPAP, string idStatus);

        int UpdateApprovalDept(PPAPApprovalDeptModel model);
    }
}
