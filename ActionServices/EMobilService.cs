using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace ActionServices
{
    public class EMobilService : IEMobilService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IEMobilDao MobilDao= factory.MobilDao;

        public MobilAksesUser GetAkses(string UserName, string MenuCode)
        {
            return MobilDao.GetAkses(UserName, MenuCode);
        }

        public List<EMobilReq> GetReqMobil(string UserName, string Approved)
        {
            return MobilDao.GetMobilReq(UserName, Approved);
        }

        public EMobilReq GetReqHeaderByID(string NoTrans)
        {
            return MobilDao.GetReqHeaderByID(NoTrans);
        }

        public List<EMobilReqDetail> GetReqDetailByID(string NoTrans)
        {
            return MobilDao.GetReqDetailByID(NoTrans);
        }

        public int ApproveReq(EMobilReq MobilReq, ApproveHistory ApvHistory)
        {
            return MobilDao.ApproveReq(MobilReq, ApvHistory);
        }

        public List<EMobilCek> GetCekMobil(int Level, string Status)
        {
            return MobilDao.GetMobilCek(Level, Status);
        }

        public EMobilCek GetCekHeaderByID(string NoSuratJalan)
        {
            return MobilDao.GetCekHeaderByID(NoSuratJalan);
        }

        public List<EMobilCekDetail> GetCekDetailByID(string NoSuratJalan)
        {
            return MobilDao.GetCekDetailByID(NoSuratJalan);
        }

        public int ApproveCek(EMobilCek MobilCek, ApproveHistory ApvHistory)
        {
            return MobilDao.ApproveCek(MobilCek, ApvHistory);
        }
    }
}
