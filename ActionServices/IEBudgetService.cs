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
    public interface IEBudgetService
    {
        List<BO_EBudget> GetModules(string th,string Dept,string User);
        List<BO_EBudget> GetBudgetPresentasi(string th, string Dept, string User);
        List<BO_EBudget> GetBudgetDetail(string acctid, string deptid, string tahun);
        List<BO_EBudget> Filter(string th,string dept,string acctid);
        List<BO_EBudget> FilterDetailBudget(string th, string dept, string acctid);
        List<BO_EBudget> FilterDetailBudget_Pl(string th, string dept, string acctid);
        List<BO_EBudget> GetBudgetDetail2(string acctid, string deptid, string tahun);
        List<BO_EBudget> ListBudgetOpen(string Status);
        List<BO_EBudget> GetBudgetDetailEdit(string IdNo);
        List<BO_EBudget> GetBudgetDetailEdit_Before(string IdNo,string semester);
        List<BO_EBudget> GetTotalAccount(string acctid,string dept,string tahun);
        List<BO_EBudget> GetApproveBOD(string acctid, string dept, string tahun);

        List<BO_EBudget> GetSummaryBudgetDept(string th, string dept,string user);
        //List<BO_EBudget> GetBudgetBaru(string IdNo);
        List<ComboList> ListComboAccount();
        List<ComboList> ListComboDept(string username);

        DataTable CreateExcell(string DeptID, string Tahun);
        DataTable CreateDataToExcel_SummaryDept(string DeptID, string Tahun, string user);
        DataTable CreateDataToExcel_SummaryDept_Detail(string DeptID, string Tahun, string user);
        DataTable CreateDataToExcel_BudgetSummaryAccount(string DeptID, string Tahun, string user);
        DataTable CreateDataToExcel_BudgetSummaryAccount_All(string DeptID, string Tahun, string user);
        DataTable CreateDataToExcel_BudgetDetail(string DeptID, string Tahun,string account);
        DataTable CreateDataToExcel_BudgetDetail_ByAccount(string DeptID, string Tahun, string account,string user);

        DataTable CreateDataToExcel_ProfitLost(string tahun);

        int SimpanBudget(BO_EBudget data);
        int DeleteBudget(BO_EBudget data);
        int SimpanBudget_baru(BO_EBudget data);

        int CekBarang(string Barang,string Tahun);

        int SimpanUploadExcel(BudgetDetail data);
        int SimpanUploadExcel_S2(BudgetDetail data);
        int EditUploadExcel(BudgetDetail data);
        int EditUploadExcel_S2(BudgetDetail data);
        int testRollback();

        List<BO_EBudgetvsActual> GetSummaryBudgetvsActualDept(string th, string dept, string user);
        
        List<BO_EBudgetvsActual> GetBudgetvsActualPresentasi(string th, string Dept, string User);

        DataTable CreateDataToExcel_ActualSummaryDept(string DeptID, string Tahun, string user);

        DataTable CreateDataToExcel_BudgetvsActualSummaryAccount(string DeptID, string Tahun, string user);

        DataTable CreateDataToExcel_BudgetvsActualSummaryAccount_All(string DeptID, string Tahun, string user);
    }

}

