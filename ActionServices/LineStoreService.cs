using BusinessObjects;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;

namespace ActionServices
{
    public class LineStoreService : ILineStoreService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly ILineStoreDao lineStoreDao = factory.LineStoreDao;

        public List<LineStorePartModel> GetLineStore()
        {
            return lineStoreDao.GetLineStore();
        }

        public DataTable CheckPart(string invtId)
        {
            return lineStoreDao.CheckPart(invtId);
        }

        public DataTable GetJadwalLine(string username)
        {
            return lineStoreDao.GetJadwalLine(username);
        }

        public int SimpanScanPart(List<LineStoreScanPartModel> data, string username)
        {
            return lineStoreDao.SimpanScanPart(data, username);
        }

        public int SimpanScanPartPunch(List<LineStorePunchModel> data, string username)
        {
            return lineStoreDao.SimpanScanPartPunch(data, username);
        }

    }
}
