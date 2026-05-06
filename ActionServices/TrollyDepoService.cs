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
    public class TrollyDepoService : ITrollyDepoService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly ITrollyDepoDao trollyDepoDao = factory.TrollyDepoDao;

        public int InsertTrollyInDepo()
        {
            return trollyDepoDao.InsertTrollyInDepo();
        }

        public List<TrollyDepoAllModel> CheckNoMobil(string noMobil)
        {
            return trollyDepoDao.CheckNoMobil(noMobil);
        }

        public List<TrollyDepoAllModel> CheckNoTransTSC(string noTrans)
        {
            return trollyDepoDao.CheckNoTransTSC(noTrans);
        }

        public DataTable CheckKodeQR(string kodeQR)
        {
            return trollyDepoDao.CheckKodeQR(kodeQR);
        }

        public int SimpanTrollyInOutDepo(TrollyDepoModel model, string deptID)
        {
            return trollyDepoDao.SimpanTrollyInOutDepo(model, deptID);
        }

        public List<TrollyDepoModel> GetLapTrollyOutDepo(FilterTrollyOutDepoModel filter)
        {
            return trollyDepoDao.GetLapTrollyOutDepo(filter);
        }

        public List<TrollyInOutDepoModel> GetTrollyInOutDepo(FilterTrollyOutDepoModel Filter)
        {
            return trollyDepoDao.GetTrollyInOutDepo(Filter);
        }

        public DataTable GetJadwalLine(string username)
        {
            return trollyDepoDao.GetJadwalLine(username);
        }

        public DataTable CheckPart(string invtId)
        {
            return trollyDepoDao.CheckPart(invtId);
        }

        public int SimpanPartDepoToADM(List<TrollyInOutDepoToADMModel> data, string username)
        {
            return trollyDepoDao.SimpanPartDepoToADM(data, username);
        }

        public List<TrollyStokDepoModel> GetTrollyStokDepo(FilterTrollyOutDepoModel Filter)
        {
            return trollyDepoDao.GetTrollyStokDepo(Filter);
        }

    }
}
