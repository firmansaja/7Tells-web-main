using BusinessObjects;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;

namespace ActionServices
{
    public class TrollyService : ITrollyService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly ITrollyDao trollyDao = factory.TrollyDao;

        public int InsertTrollyOutTSC()
        {
            return trollyDao.InsertTrollyOutTSC();
        }

        public DataTable CheckTrolly(string kodeQR)
        {
            return trollyDao.CheckTrolly(kodeQR);
        }

        public DataTable CheckPart(string invtId, string project, string typeTrolly)
        {
            return trollyDao.CheckPart(invtId, project, typeTrolly);
        }

        public List<TrollyAllModel> CheckNoMobil(string noMobil)
        {
            return trollyDao.CheckNoMobil(noMobil);
        }

        public List<TrollyAllModel> CheckNoSuratJalan(string noTransDepo)
        {
            return trollyDao.CheckNoSuratJalan(noTransDepo);
        }

        public int SimpanTrollyInOutTSC(TrollyModel model, string deptID)
        {
            return trollyDao.SimpanTrollyInOutTSC(model, deptID);
        }

        public List<TrollyInOutModel> GetTrollyInOut(FilterTrollyOutDepoModel filter)
        {
            return trollyDao.GetTrollyInOut(filter);
        }

        public List<TrollyInOutDetailModel> GetTrollyInOutDetail(string noTrans, string kodeQR)
        {
            return trollyDao.GetTrollyInOutDetail(noTrans, kodeQR);
        }
    }
}
