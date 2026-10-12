using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface IPOServices
    {
        POAksesUser GetAkses(string UserName);
        List<BO_PO> GetPO(POAksesUser pOakses);
        List<BO_PO> GetPOByUser(POAksesUser pOakses,DateTime dtdari,DateTime dtsampai);
        List<BO_PO> GetPOApproveSM(POAksesUser pOakses);
        List<BO_PO> GetPONonConfirm(POAksesUser pOakses);
        List<BO_PO> GetPOSupplierApprove(POAksesUser pOakses);
        BO_PO GetPOHeaderById(string PONo);
        List<BO_PODetail> GetPODetail(string PONo);
        BO_PO GetPoTotalAmount(string PONo);

        List<BO_POApproval> GetPOApprove(string PONo);

        List<BO_POExport> GetModelPOHistoryExport(POHistory filter);
        List<BO_POExport> GetModelPOHistory(POHistory filter);

        int UpdateApproval(List<BO_PODetail> PODetail, string UserName, int Kol);
        int RejectApproval(List<BO_PODetail> PODetail, string UserName, int Kol);

        int ApprovePO(string UserName, int Kol, string PONo);
        int RejectPO(string UserName, int Kol, string PONo);
        int UpdateUrgent(string PONo);
        int PrintPO(string PONo);
        int SendEmail(string PONo);

        List<Dictionary<string, object>> GetHistoryPrice(string InvtID);

    }
}
