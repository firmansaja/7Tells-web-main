using BusinessObjects.Surat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataObjects.Surat
{
   public interface ISuratKeluarRepo
    {
        List<SuratKeluarModel> Get(string dept);
        SuratKeluarModel GetRow(int Id);
        int Add(SuratKeluarModel model);
        int Edit(SuratKeluarModel model);
        int Delete(int Id);
        KategoriSuratModel GetKategori(string idKategori);
        string GetNoSuratAuto(string dept);
    }
}
