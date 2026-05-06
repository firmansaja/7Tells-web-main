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
    public class SuratKeluarService : ISuratKeluarService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly ISuratKeluarRepo suratKeluar = factory.SuratKeluar;
        public int Add(SuratKeluarModel model)
        {
            return suratKeluar.Add(model);
        }

        public int Delete(int id)
        {
            return suratKeluar.Delete(id);
        }

        public int Edit(SuratKeluarModel model)
        {
            return suratKeluar.Edit(model);
        }

        public List<SuratKeluarModel> Get(string dept)
        {
            return suratKeluar.Get(dept);
        }

        public SuratKeluarModel GetRow(int id)
        {
            return suratKeluar.GetRow(id);
        }

        public KategoriSuratModel GetKategori(string idKategori)
        {
            return suratKeluar.GetKategori(idKategori);
        }

        public String GetNoSuratAuto(string dept)
        {
            return suratKeluar.GetNoSuratAuto(dept);
        }        
    }
}
