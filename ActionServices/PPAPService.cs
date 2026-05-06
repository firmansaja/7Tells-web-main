using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.PPAP;
using DataObjects;

namespace ActionServices
{
    public class PPAPService : IPPAPService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IPPAPDao ppapDao = factory.PPAPDao;

        public List<PPAPHeaderModel> GetPPAP()
        {
            return ppapDao.GetPPAP();
        }

        public PPAPHeaderModel GetPPAPById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPById(noPPAP, idStatus);
        }

        public List<PPAPDetailModel> GetPPAPDetailById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPDetailById(noPPAP, idStatus);
        }

        public List<PPAPCheckItemModel> GetPPAPCheckItemById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPCheckItemById(noPPAP, idStatus);
        }

        public List<PPAPAksesDeptModel> GetPPAPAksesDept(string username)
        {
            return ppapDao.GetPPAPAksesDept(username);
        }

        public List<PPAPAttendanceModel> GetPPAPAttendanceById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPAttendanceById(noPPAP, idStatus);
        }

        public List<PPAPApprovalDeptModel> GetPPAPApprovalDeptById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPApprovalDeptById(noPPAP, idStatus);
        }

        public List<PPAPToolingModel> GetPPAPToolingById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPToolingById(noPPAP, idStatus);
        }

        public List<PPAPQualityModel> GetPPAPQualityById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPQualityById(noPPAP, idStatus);
        }

        public List<PPAPPreparationModel> GetPPAPPreparationById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPPreparationById(noPPAP, idStatus);
        }

        public List<PPAPDocumentModel> GetPPAPDocumentById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPDocumentById(noPPAP, idStatus);
        }

        public List<PPAPDocumentDetailModel> GetPPAPDocumentDetailByItem(string noPPAP, string idStatus, string code)
        {
            return ppapDao.GetPPAPDocumentDetailByItem(noPPAP, idStatus, code);
        }

        public List<PPAPRptDocumentModel> GetPPAPRptDocumentById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPRptDocumentById(noPPAP, idStatus);
        }

        public List<PPAPSupplierModel> GetPPAPSupplierById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPSupplierById(noPPAP, idStatus);
        }

        public List<PPAPPackingModel> GetPPAPPackingById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPPackingById(noPPAP, idStatus);
        }

        public List<PPAPComparationModel> GetPPAPComparationById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPComparationById(noPPAP, idStatus);
        }

        public List<PPAPPaintingModel> GetPPAPPaintingById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPPaintingById(noPPAP, idStatus);
        }

        public List<PPAPPaintingDetailModel> GetPPAPPaintingDetailById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPPaintingDetailById(noPPAP, idStatus);
        }

        public List<PPAPPaintingDetailModel> GetPPAPPaintingDetailByItem(string noPPAP, string idStatus, string code)
        {
            return ppapDao.GetPPAPPaintingDetailByItem(noPPAP, idStatus, code);
        }

        public List<PPAPAssemblyModel> GetPPAPAssemblyById(string noPPAP, string idStatus)
        {
            return ppapDao.GetPPAPAssemblyById(noPPAP, idStatus);
        }

        public int UpdateApprovalDept(PPAPApprovalDeptModel model)
        {
            return ppapDao.UpdateApprovalDept(model);
        }
    }
}
