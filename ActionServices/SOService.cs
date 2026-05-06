using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;


namespace ActionServices
{
    public class SOService : ISoService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly ISoDao SoDao = factory.SoDao;

        public List<BO_So> GetByOrdeNbr(string OrdNbr)
        {
            return SoDao.GetByOrdeNbr(OrdNbr);
        }

        public List<BO_So> GetOs_SO(string CustOrdNbr)
        {
            return SoDao.GetOs_SO(CustOrdNbr);
        }
    }
}
