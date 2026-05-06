using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace ActionServices
{
    public class KlinikService : IKlinikService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IKlinikDao KlinikDao = factory.KlinikDao;
        
        public List<KlinikPeriksa> GetKlinikPeriksa(DateTime TglDari, DateTime TglSampai)
        {
            return KlinikDao.GetKlinikPeriksa(TglDari, TglSampai);
        }

    }
}
