using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.PPIC;
using DataObjects;
using System.Data;

namespace ActionServices
{
    public class BuktiMuatService : IBuktiMuatService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IBuktiMuatDao buktiMuatDao = factory.BuktiMuat;

        public List<BuktiMuatModel> CheckBuktiMuat(string noSeq)
        {
            return buktiMuatDao.CheckBuktiMuat(noSeq);
        }

        public int SimpanBuktiMuatOut(List<BuktiMuatModel> data, string username)
        {
            return buktiMuatDao.SimpanBuktiMuatOut(data, username);
        }
    }
}
