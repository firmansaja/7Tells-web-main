using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IPODao
    {
        POAksesUser GetAkses(string UserName);
        List<BO_PO> GetModelPO(POAksesUser poakses);

        List<BO_PO> GetModelPOByUser(POAksesUser poakses,DateTime dtdari,DateTime dtsampai);
        List<BO_PO> GetModelPOApproveSM(POAksesUser poakses);
        List<BO_PO> GetModelPONonConfirm(POAksesUser poakses);
        List<BO_POExport> GetModelPOHistoryExport(POHistory filter);
        List<BO_POExport> GetModelPOHistory(POHistory filter);
        List<BO_PO> GetModelPOSupplierApprove(POAksesUser poakses);

        BO_PO GetPOHeaderById(string PONo);

        List<BO_PODetail> GetPODetail(string PONo);

        BO_PO GetPOTotalAmount(string PONo);

        List<BO_POApproval> GetPOApprove(string PONo);
        int UpdateApproval(List<BO_PODetail> PODetail, string UserName, int Kol);
        int RejectApproval(List<BO_PODetail> PODetail, string UserName, int Kol);

        int ApprovePO(string UserName, int Kol, string PONo);
        int RejectPO(string UserName, int Kol, string PONo);
        int UpdateUrgent(string PONo);
        int PrintPO(string PONo);
        int SendEmail(string PONo);


    }
}
