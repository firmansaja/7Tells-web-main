using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using BusinessObjects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface IEbudgetPLService 
    {
        List<BO_EBudgetPL> GetFormatPL();
        List<BO_EBudgetPL> GetProfitLost(string tahun);
        List<BO_EBudgetPL> GetProfitLostByAccount(string acctid, string tahun);
        DataTable CreateDataToExcel_ProfitlostByAccount(string acctid, string tahun);
    }
}
