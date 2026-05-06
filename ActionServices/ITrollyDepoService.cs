using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface ITrollyDepoService
    {
        int InsertTrollyInDepo();

        List<TrollyDepoAllModel> CheckNoMobil(string noMobil);

        List<TrollyDepoAllModel> CheckNoTransTSC(string noTrans);

        DataTable CheckKodeQR(string kodeQR);

        int SimpanTrollyInOutDepo(TrollyDepoModel model, string deptID);

        List<TrollyDepoModel> GetLapTrollyOutDepo(FilterTrollyOutDepoModel filter);

        List<TrollyInOutDepoModel> GetTrollyInOutDepo(FilterTrollyOutDepoModel Filter);

        DataTable GetJadwalLine(string username);

        DataTable CheckPart(string invtId);

        int SimpanPartDepoToADM(List<TrollyInOutDepoToADMModel> data, string username);

        List<TrollyStokDepoModel> GetTrollyStokDepo(FilterTrollyOutDepoModel Filter);
    }
}
