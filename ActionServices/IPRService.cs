using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface IPRService
    {
        PR GetPRModelById(string PRNo);

        PR GetPRHeaderById(string PRNo);

        List<PR> GetPR();

        List<PR> GetModelPR(PRAksesUser prakses);

        List<PR> GetModelPRByUser(PRAksesUser prakses);

        List<PR> GetModelPRByAll(PRAksesUser prakses);

        List<PR> GetModelPRHistory(PRHistory filter);

        List<PR> GetModelPRHistoryPur(PRHistory filter);
        
        List<PRDetail> GetModelPRHistoryExport(PRHistory filter);

        List<PRDetail> GetModelPRHistoryExportPur(PRHistory filter);
        
        List<PR> GetModelPRProgress(PRProgress filter);
        
        List<PRDetail> GetModelPRProgressExport(PRProgress filter);

        List<PR> GetModelPRPurchase();

        List<PR> GetPRById(string PRNo);

        PRAksesUser GetAkses(string UserName);

        int Update(PR pr);

        int UpdateFlagPR(string PRNo, string ApprovalPIC);

        int UpdateStatusBudget(string PRNo);

        int UpdateFlagPRReject(string PRNo, int Kol);

        int UpdatePilihanPurchase(int Action, string PRNo);

        List<PRApproval> GetPRApprove(string PRNo);

        List<PRDetail> GetPRDetail(string PRNo, string Year);

        List<PRDetail> GetPRDetailWithoutBudget(string PRNo);

        List<PRBudget> GetPRBudget(string Year, string Sub, string Kol);

        List<PRBudgetDetail> GetPRBudgetDetail(string Year, string Month, string Sub, string Account);

        int UpdateApvRemark(string PRNo, string Remark);

        int ApprovePR(string UserName, int Kol, string PRNo);

        int UpdateKomenPR(List<PRDetail> PRDetail);

        int UpdateInfoHargaPR(List<PRDetail> PRDetail);

        int UpdateOverBudgetPR(string UserName, string PRNo);
    }
}
