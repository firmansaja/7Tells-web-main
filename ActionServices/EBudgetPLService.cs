using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace ActionServices
{
    public class EBudgetPLService : IEbudgetPLService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IEbudgetPLDao BudgetPLDao = factory.BudgetPLDao;

        public DataTable CreateDataToExcel_ProfitlostByAccount(string acctid, string tahun)
        {
            return BudgetPLDao.CreateDataToExcel_ProfitlostByAccount(acctid, tahun);
        }

        public List<BO_EBudgetPL> GetFormatPL()
        {
            return BudgetPLDao.GetFormatPL();
        }

        public List<BO_EBudgetPL> GetProfitLost(string tahun)
        {
            return BudgetPLDao.GetProfitLost(tahun);
        }

        public List<BO_EBudgetPL> GetProfitLostByAccount(string acctid, string tahun)
        {
            return BudgetPLDao.GetProfitLostByAccount(acctid,tahun);
        }
    }
}
