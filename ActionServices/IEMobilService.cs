using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface IEMobilService
    {
        MobilAksesUser GetAkses(string UserName, string MenuCode);

        List<EMobilReq> GetReqMobil(string UserName, string Approved);

        EMobilReq GetReqHeaderByID(string NoTrans);

        List<EMobilReqDetail> GetReqDetailByID(string NoTrans);

        int ApproveReq(EMobilReq MobilReq, ApproveHistory ApvHistory);

        List<EMobilCek> GetCekMobil(int Level, string Status);

        EMobilCek GetCekHeaderByID(string NoSuratJalan);

        List<EMobilCekDetail> GetCekDetailByID(string NoSuratJalan);

        int ApproveCek(EMobilCek MobilCek, ApproveHistory ApvHistory);
    }
}
