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
    public class SuratSecService : ISuratSecService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly ISuratSecRepo suratSec = factory.SuratSec;
        public int Add(SuratSecModel model)
        {
            return suratSec.Add(model);
        }

        public int Delete(int id)
        {
            return suratSec.Delete(id);
        }

        public int Edit(SuratSecModel model)
        {
            return suratSec.Edit(model);
        }

        public List<SuratSecModel> Get()
        {
            return suratSec.Get();
        }

        public SuratSecModel GetRow(int id)
        {
            return suratSec.GetRow(id);
        }
    }
}
