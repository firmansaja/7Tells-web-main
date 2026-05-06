using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Surat;

namespace ActionServices.Surat
{
    public interface ISuratKeluarService
    {
        List<SuratKeluarModel> Get(string dept);
        SuratKeluarModel GetRow(int id);
        int Add(SuratKeluarModel model);
        int Edit(SuratKeluarModel model);
        int Delete(int id);
        KategoriSuratModel GetKategori(string idKategori);
        string GetNoSuratAuto(string dept);
    }
}
