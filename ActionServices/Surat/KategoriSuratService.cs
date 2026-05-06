using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Surat;
using DataObjects;
using DataObjects.Surat;

namespace ActionServices.Surat
{
    public class KategoriSuratService : IKategoriSuratService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IKategoriSuratRepo kategoriSurat = factory.KategoriSurat;
        public int Add(KategoriSuratModel model)
        {
            return kategoriSurat.Add(model);
        }

        public int Delete(int id)
        {
            return kategoriSurat.Delete(id);
        }

        public int Edit(KategoriSuratModel model)
        {
            return kategoriSurat.Edit(model);
        }

        public List<KategoriSuratModel> Get()
        {
            return kategoriSurat.Get();
        }


        public List<KategoriSuratModel> GetKategori()
        {
            return kategoriSurat.Get();
        }

        public KategoriSuratModel GetRow(int id)
        {
            return kategoriSurat.GetRow(id);
        }
    }
}
