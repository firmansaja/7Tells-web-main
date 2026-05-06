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
    public interface IEbudgetDao
    {

       
        List<BO_EBudget> GetModules(string th,string Dept,string User);
        List<BO_EBudget> GetBudgetPresentasi(string th, string Dept, string User);
        List<BO_EBudget> GetBudgetDetail(string acctid, string deptid, string tahun);
        List<BO_EBudget> Filter(string th,string dept,string acctid);
        List<BO_EBudget> FilterDetailBudget(string th, string dept, string acctid);
        List<BO_EBudget> FilterDetailBudget_Pl(string th, string dept, string acctid);
        List<BO_EBudget> GetBudgetDetail2(string acctid, string deptid, string tahun);
        List<BO_EBudget> GetBudgetOpen(string Status);
        List<BO_EBudget> GetBudgetDetailEdit(string IdNo);
        List<BO_EBudget> GetBudgetDetailEdit_Before(string IdNo,string semester);
        List<BO_EBudget> GetTotalAccount(string acctid,string dept, string tahun);
        List<BO_EBudget> GetApproveBOD(string acctid, string dept, string tahun);
        List<ComboList> ListComboAccount();
        List<ComboList> ListComboDept(string username);
        List<BO_EBudget> GetSummaryBudgetDept(string tahun, string Dept, string user );

        int SimpanBudget(BO_EBudget data);
        int SimpanBudget_Baru(BO_EBudget data);
        int CekBarang(string Barang,string Tahun);
        int SimpanBudget_Upload(BudgetDetail data);
        int SimpanBudget_Upload_S2(BudgetDetail data);
        int DeleteBudget(BO_EBudget data);
        int EditUploadExcel(BudgetDetail data);
        int EditUploadExcel_S2(BudgetDetail data);
        DataTable CreateExcell(string DeptId, string Tahun);
        DataTable CreateDataToExcel_SummaryDept(string DeptId, string Tahun, string user);
        DataTable CreateDataToExcel_SummaryDept_Detail(string DeptId, string Tahun, string user);
        DataTable CreateDataToExcel_BudgetSummaryAccount(string DeptId, string Tahun, string user);
        DataTable CreateDataToExcel_BudgetSummaryAccount_All(string DeptId, string Tahun, string user);
        DataTable CreateDataToExcel_BudgetDetail(string DeptId, string Tahun,string account);
        DataTable CreateDataToExcel_BudgetDetail_ByAccount(string DeptId, string Tahun, string account,string user);
        DataTable CreateDataToExcel_ProfitLost(string Tahun);
        int testRollback();
        List<BO_EBudgetvsActual> GetSummaryBudgetvsActualDept(string tahun, string Dept, string user);
        List<BO_EBudgetvsActual> GetBudgetvsActualPresentasi(string th, string Dept, string User);
        DataTable CreateDataToExcel_ActualSummaryDept(string DeptId, string Tahun, string user);
        DataTable CreateDataToExcel_BudgetvsActualSummaryAccount(string DeptId, string Tahun, string user);
        DataTable CreateDataToExcel_BudgetvsActualSummaryAccount_All(string DeptId, string Tahun, string user);
    }
}
