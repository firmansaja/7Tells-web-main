using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace ActionServices
{
    public class PRService : IPRService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IPRDao moduleDao = factory.PRDao;
        public List<PR> GetPR()
        {
            return moduleDao.GetPR();
        }

        public List<PR> GetModelPR(PRAksesUser prakses)
        {
            return moduleDao.GetModelPR(prakses);
        }

        public List<PR> GetPRById(string PRNo)
        {
            return moduleDao.GetPRById(PRNo);
        }

        public PR GetPRModelById(string PRNo)
        {
            return moduleDao.GetPRModelById(PRNo);
        }

        public PR GetPRHeaderById(string PRNo)
        {
            return moduleDao.GetPRHeaderById(PRNo);
        }

        public int Update(PR pr)
        {
            throw new NotImplementedException();
        }

        public List<PR> GetModelPRByUser(PRAksesUser prakses)
        {
            return moduleDao.GetModelPRByUser(prakses);
        }

        public List<PR> GetModelPRByAll(PRAksesUser prakses)
        {
            return moduleDao.GetModelPRByAll(prakses);
        }

        public List<PR> GetModelPRPurchase()
        {
            return moduleDao.GetModelPRPurchase();
        }

        public List<PR> GetModelPRHistory(PRHistory filter)
        {
            return moduleDao.GetModelPRHistory(filter);
        }

        public List<PR> GetModelPRHistoryPur(PRHistory filter)
        {
            return moduleDao.GetModelPRHistoryPur(filter);
        }

        public List<PR> GetModelPRProgress(PRProgress filter)
        {
            return moduleDao.GetModelPRProgress(filter);
        }        

        public List<PRDetail> GetModelPRHistoryExport(PRHistory filter)
        {
            return moduleDao.GetModelPRHistoryExport(filter);
        }

        public List<PRDetail> GetModelPRHistoryExportPur(PRHistory filter)
        {
            return moduleDao.GetModelPRHistoryExportPur(filter);
        }

        public List<PRDetail> GetModelPRProgressExport(PRProgress filter)
        {
            return moduleDao.GetModelPRProgressExport(filter);
        }

        public PRAksesUser GetAkses(string UserName)
        {
            return moduleDao.GetAkses(UserName);
        }

        public int UpdateFlagPR(string PRNo, string ApprovalPIC)
        {
            return moduleDao.UpdateFlagPR(PRNo, ApprovalPIC);
        }

        public int UpdateStatusBudget(string PRNo)
        {
            return moduleDao.UpdateStatusBudget(PRNo);
        }

        public int UpdateFlagPRReject(string PRNo, int Kol)
        {
            return moduleDao.UpdateFlagPRReject(PRNo, Kol);
        }

        public int UpdatePilihanPurchase(int Action, string PRNo)
        {
            return moduleDao.UpdatePilihanPurchase(Action, PRNo);
        }

        public List<PRApproval> GetPRApprove(string PRNo)
        {
            return moduleDao.GetPRApprove(PRNo);
        }

        public List<PRDetail> GetPRDetail(string PRNo, string Year)
        {
            return moduleDao.GetPRDetail(PRNo, Year);
        }

        public List<PRDetail> GetPRDetailWithoutBudget(string PRNo)
        {
            return moduleDao.GetPRDetailWithoutBudget(PRNo);
        }

        public List<PRBudget> GetPRBudget(string Year, string Sub, string Kol)
        {
            return moduleDao.GetPRBudget(Year, Sub, Kol);
        }

        public List<PRBudgetDetail> GetPRBudgetDetail(string Year, string Month, string Sub, string Account)
        {
            return moduleDao.GetPRBudgetDetail(Year, Month, Sub, Account);
        }

        public int UpdateApvRemark(string PRNo, string Remark)
        {
            return moduleDao.UpdateApvRemark(PRNo, Remark);
        }

        public int ApprovePR(string UserName, int Kol, string PRNo)
        {
            return moduleDao.ApprovePR(UserName, Kol, PRNo);
        }

        public int UpdateKomenPR(List<PRDetail> PRDetail)
        {
            return moduleDao.UpdateKomenPR(PRDetail);
        }

        public int UpdateInfoHargaPR(List<PRDetail> PRDetail)
        {
            return moduleDao.UpdateInfoHargaPR(PRDetail);
        }

        public int UpdateOverBudgetPR(string UserName, string PRNo)
        {
            return moduleDao.UpdateOverBudgetPR(UserName, PRNo);
        }
    }
}
