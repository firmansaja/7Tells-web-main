using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataObjects
{
    public interface IEbudgetPLDao
    {
        List<BO_EBudgetPL> GetFormatPL();
        List<BO_EBudgetPL> GetProfitLost(string tahun);
        List<BO_EBudgetPL> GetProfitLostByAccount(string acctid, string tahun);

        DataTable CreateDataToExcel_ProfitlostByAccount(string acctid, string tahun);
       

    }
}
