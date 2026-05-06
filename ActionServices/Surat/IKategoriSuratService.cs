using BusinessObjects.Surat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices.Surat
{
    public interface IKategoriSuratService
    {
        List<KategoriSuratModel> Get();
        List<KategoriSuratModel> GetKategori();
        KategoriSuratModel GetRow(int id);
        int Add(KategoriSuratModel model);
        int Edit(KategoriSuratModel model);
        int Delete(int id);
    }
}
