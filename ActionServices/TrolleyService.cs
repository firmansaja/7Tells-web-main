using BusinessObjects;
using DataObjects;
using System;
using System.Data;

namespace ActionServices
{
    public class TrolleyService : ITrolleyService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly ITrolleyDao trolleyDao = factory.TrolleyDao;

        public int InsertTrolleyIn()
        {
            return trolleyDao.InsertTrolleyIn();
        }

        public DataTable CheckTrolley(string KodeTrolley)
        {
            return trolleyDao.CheckTrolley(KodeTrolley);
        }

        public int SimpanTrolleyInOut(Trolley model, string deptID)
        {
            return trolleyDao.SimpanTrolleyInOut(model, deptID);
        }
    }
}
