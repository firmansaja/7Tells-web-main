using BusinessObjects;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public class GlobalService : IGlobalService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IGlobalDao GlobalDao = factory.GlobalDao;

        public int InsertApvHistory(ApproveHistory ApvHistory)
        {
            return GlobalDao.InsertApvHistory(ApvHistory);
        }
    }
}
