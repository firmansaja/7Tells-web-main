using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface ITrollyService
    {
        int InsertTrollyOutTSC();

        DataTable CheckTrolly(string kodeQR);

        DataTable CheckPart(string invtId, string project, string typeTrolly);

        List<TrollyAllModel> CheckNoMobil(string noMobil);

        List<TrollyAllModel> CheckNoSuratJalan(string noMobil);

        int SimpanTrollyInOutTSC(TrollyModel model, string deptID);

        List<TrollyInOutModel> GetTrollyInOut(FilterTrollyOutDepoModel filter);

        List<TrollyInOutDetailModel> GetTrollyInOutDetail(string noTrans, string kodeQR);
    }
}
