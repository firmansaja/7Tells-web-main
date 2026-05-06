using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface ILineStoreDao
    {
        List<LineStorePartModel> GetLineStore();

        DataTable CheckPart(string invtId);

        DataTable GetJadwalLine(string username);

        int SimpanScanPart(List<LineStoreScanPartModel> data, string username);

        int SimpanScanPartPunch(List<LineStorePunchModel> data, string username);
    }
}
