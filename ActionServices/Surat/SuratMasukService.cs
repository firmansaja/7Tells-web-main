using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.Surat;
using DataObjects;
using DataObjects.Surat;

namespace ActionServices.Surat
{
    public class SuratMasukService : ISuratMasukService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly ISuratMasukRepo suratMasuk = factory.SuratMasuk;
        public int Add(SuratMasukModel model, string dept)
        {
            return suratMasuk.Add(model, dept);
        }

        public int Delete(int Id)
        {
            return suratMasuk.Delete(Id);
        }

        public int Edit(SuratMasukModel model)
        {
            return suratMasuk.Edit(model);
        }

        public List<SuratMasukModel> Get(string dept, int flag, string status)
        {
            return suratMasuk.Get(dept, flag, status);
        }

        public List<SuratMasukModel> GetList()
        {
            return suratMasuk.GetList();
        }

        public SuratMasukModel GetRow(int id)
        {
            return suratMasuk.GetRow(id);
        }

        public string GetNoUrutAuto(string dept, int flag)
        {
            return suratMasuk.GetNoUrutAuto(dept, flag);
        }
    }
}
