using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.PPIC;

namespace DataObjects
{
    public interface IBuktiMuatDao
    {
        List<BuktiMuatModel> CheckBuktiMuat(string noSuratJalan);

        int SimpanBuktiMuatOut(List<BuktiMuatModel> data, string username);
    }
}
