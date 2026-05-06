using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;


namespace ActionServices
{
    public class POServices : IPOServices
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IPODao moduleDao = factory.PODao;

        public int ApprovePO(string UserName, int Kol, string PONo)
        {
            return moduleDao.ApprovePO(UserName, Kol, PONo);
        }

        public POAksesUser GetAkses(string UserName)
        {
            return moduleDao.GetAkses(UserName);
        }

        public List<BO_POExport> GetModelPOHistory(POHistory filter)
        {
            return moduleDao.GetModelPOHistory(filter);
        }

        public List<BO_POExport> GetModelPOHistoryExport(POHistory filter)
        {
            return moduleDao.GetModelPOHistoryExport(filter);
        }

        public List<BO_PO> GetPO(POAksesUser poakses) 
        {
            return moduleDao.GetModelPO(poakses);
        }

        public List<BO_POApproval> GetPOApprove(string PONo)
        {
            return moduleDao.GetPOApprove(PONo);
        }

        public List<BO_PO> GetPOApproveSM(POAksesUser pOakses)
        {
            return moduleDao.GetModelPOApproveSM(pOakses);
        }

        public List<BO_PO> GetPOByUser(POAksesUser pOakses,DateTime dtdari,DateTime dtsampai)
        {
            return moduleDao.GetModelPOByUser(pOakses,dtdari,dtsampai);
        }

        public List<BO_PODetail> GetPODetail(string PONo)
        {
            return moduleDao.GetPODetail(PONo);
        }

        public BO_PO GetPOHeaderById(string PONo)
        {
            return moduleDao.GetPOHeaderById(PONo);
        }

        public List<BO_PO> GetPONonConfirm(POAksesUser pOakses)
        {
            return moduleDao.GetModelPONonConfirm(pOakses);
        }

        public List<BO_PO> GetPOSupplierApprove(POAksesUser pOakses)
        {
            return moduleDao.GetModelPOSupplierApprove(pOakses);
        }

        public BO_PO GetPoTotalAmount(string PONo)
        {
            return moduleDao.GetPOTotalAmount(PONo);
        }

        public int PrintPO(string PONo)
        {
            return moduleDao.PrintPO(PONo);
        }

        public int RejectPO(string UserName, int Kol, string PONo)
        {
            return moduleDao.RejectPO(UserName, Kol, PONo);
        }

        public int SendEmail(string PONo)
        {
            return moduleDao.SendEmail(PONo);
        }

        public int UpdateApproval(List<BO_PODetail> PODetail, string UserName, int Kol)
        {
            return moduleDao.UpdateApproval(PODetail,UserName,Kol);
        }

        public int UpdateUrgent(string PONo)
        {
            return moduleDao.UpdateUrgent(PONo);
        }

        public int RejectApproval(List<BO_PODetail> PODetail, string UserName, int Kol)
        {
            return moduleDao.RejectApproval(PODetail, UserName, Kol);
        }

    }
}
