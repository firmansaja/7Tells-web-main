using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.PPIC;

namespace ActionServices
{
    public interface IBuktiMuatService
    {
        List<BuktiMuatModel> CheckBuktiMuat(string noSeq);

        int SimpanBuktiMuatOut(List<BuktiMuatModel> data, string username);
    }
}
