using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IEMobilDao
    {
        MobilAksesUser GetAkses(string UserName, string MenuCode);

        List<EMobilReq> GetMobilReq(string UserName, string Approved);

        EMobilReq GetReqHeaderByID(string NoTrans);

        List<EMobilReqDetail> GetReqDetailByID(string NoTrans);

        int ApproveReq (EMobilReq MobilReq, ApproveHistory ApvHistory);

        List<EMobilCek> GetMobilCek(int Level, string Status);

        EMobilCek GetCekHeaderByID(string NoSuratJalan);

        List<EMobilCekDetail> GetCekDetailByID(string NoSuratJalan);

        int ApproveCek(EMobilCek MobilCek, ApproveHistory ApvHistory);
    }
}
