using BusinessObjects.Surat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Surat
{
    public interface IKategoriSuratRepo
    {
        List<KategoriSuratModel> Get();
        KategoriSuratModel GetRow(int Id);
        int Add(KategoriSuratModel model);
        int Edit(KategoriSuratModel model);
        int Delete(int Id);

    }
}
