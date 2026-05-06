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
    public class EBudgetService : IEBudgetService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IEbudgetDao BudgetDao = factory.BudgetDao;

        public int CekBarang(string Barang, string Tahun)
        {
            return BudgetDao.CekBarang(Barang, Tahun);
        }

        public DataTable CreateExcell(string DeptID, string Tahun)
        {
            return BudgetDao.CreateExcell(DeptID, Tahun);
        }



        public List<BO_EBudget> Filter(string th, string dept, string acctid)
        {
            return BudgetDao.Filter(th, dept, acctid);
        }

        public List<BO_EBudget> FilterDetailBudget(string th, string dept, string acctid)
        {
            return BudgetDao.FilterDetailBudget(th, dept, acctid);
        }

        public List<BO_EBudget> FilterDetailBudget_Pl(string th, string dept, string acctid)
        {
            return BudgetDao.FilterDetailBudget_Pl(th, dept, acctid);
        }

        public List<BO_EBudget> GetBudgetDetail(string acctid, string deptid, string tahun)
        {
            return BudgetDao.GetBudgetDetail(acctid, deptid, tahun);
        }

        public List<BO_EBudget> GetBudgetDetail2(string acctid, string deptid, string tahun)
        {
            return BudgetDao.GetBudgetDetail2(acctid, deptid, tahun);
        }

        public List<BO_EBudget> GetBudgetDetailEdit(string IdNo)
        {
            return BudgetDao.GetBudgetDetailEdit(IdNo);
        }

        public List<BO_EBudget> GetBudgetDetailEdit_Before(string IdNo, string semester)
        {
            return BudgetDao.GetBudgetDetailEdit_Before(IdNo, semester);
        }

        public List<BO_EBudget> GetModules(string th, string dept, string User)
        {
            return BudgetDao.GetModules(th, dept, User);
        }

        public List<BO_EBudget> ListBudgetOpen(string Status)
        {
            return BudgetDao.GetBudgetOpen(Status);
        }

        public int SimpanBudget(BO_EBudget data)
        {
            return BudgetDao.SimpanBudget(data);
        }

        public int SimpanBudget_baru(BO_EBudget data)
        {
            return BudgetDao.SimpanBudget_Baru(data);
        }

        public int SimpanUploadExcel(BudgetDetail data)
        {
            return BudgetDao.SimpanBudget_Upload(data);
        }

        public int EditUploadExcel(BudgetDetail data)
        {
            return BudgetDao.EditUploadExcel(data);
        }

        public int SimpanUploadExcel_S2(BudgetDetail data)
        {
            return BudgetDao.SimpanBudget_Upload_S2(data);
        }

        public int EditUploadExcel_S2(BudgetDetail data)
        {
            return BudgetDao.EditUploadExcel_S2(data);
        }

        public List<BO_EBudget> GetTotalAccount(string acctid, string dept, string tahun)
        {
            return BudgetDao.GetTotalAccount(acctid, dept, tahun);
        }

        public List<BO_EBudget> GetApproveBOD(string acctid, string dept, string tahun)
        {
            return BudgetDao.GetApproveBOD(acctid, dept, tahun);
        }



        public List<BO_EBudget> GetSummaryBudgetDept(string th, string Dept, string user)
        {
            return BudgetDao.GetSummaryBudgetDept(th, Dept, user);
        }

        public List<BO_EBudget> GetBudgetPresentasi(string th, string Dept, string User)
        {
            return BudgetDao.GetBudgetPresentasi(th, Dept, User);
        }

        public int DeleteBudget(BO_EBudget data)
        {
            return BudgetDao.DeleteBudget(data);
        }

        public List<ComboList> ListComboAccount()
        {
            return BudgetDao.ListComboAccount();
        }

        public List<ComboList> ListComboDept(string username)
        {
            return BudgetDao.ListComboDept(username);
        }

        public DataTable CreateDataToExcel_SummaryDept(string DeptID, string Tahun, string user)
        {
            return BudgetDao.CreateDataToExcel_SummaryDept(DeptID, Tahun, user);
        }

        public DataTable CreateDataToExcel_BudgetSummaryAccount(string DeptID, string Tahun, string user)
        {
            return BudgetDao.CreateDataToExcel_BudgetSummaryAccount(DeptID, Tahun, user);
        }

        public DataTable CreateDataToExcel_BudgetDetail(string DeptID, string Tahun, string account)
        {
            return BudgetDao.CreateDataToExcel_BudgetDetail(DeptID, Tahun, account);
        }

        public DataTable CreateDataToExcel_ProfitLost(string tahun)
        {
            return BudgetDao.CreateDataToExcel_ProfitLost(tahun);
        }

        public DataTable CreateDataToExcel_SummaryDept_Detail(string DeptID, string Tahun, string user)
        {
            return BudgetDao.CreateDataToExcel_SummaryDept_Detail(DeptID, Tahun, user);
        }

        public DataTable CreateDataToExcel_BudgetSummaryAccount_All(string DeptID, string Tahun, string user)
        {
            return BudgetDao.CreateDataToExcel_BudgetSummaryAccount_All(DeptID, Tahun, user);
        }

        public DataTable CreateDataToExcel_BudgetDetail_ByAccount(string DeptID, string Tahun, string account, string user)
        {
            return BudgetDao.CreateDataToExcel_BudgetDetail_ByAccount(DeptID, Tahun, account, user);
        }

        public int testRollback()
        {
            return BudgetDao.testRollback();
        }

        public List<BO_EBudgetvsActual> GetSummaryBudgetvsActualDept(string th, string Dept, string user)
        {
            return BudgetDao.GetSummaryBudgetvsActualDept(th, Dept, user);
        }

        public List<BO_EBudgetvsActual> GetBudgetvsActualPresentasi(string th, string Dept, string User)
        {
            return BudgetDao.GetBudgetvsActualPresentasi(th, Dept, User);
        }

        public DataTable CreateDataToExcel_ActualSummaryDept(string DeptID, string Tahun, string user)
        {
            return BudgetDao.CreateDataToExcel_ActualSummaryDept(DeptID, Tahun, user);
        }

        public DataTable CreateDataToExcel_BudgetvsActualSummaryAccount(string DeptID, string Tahun, string user)
        {
            return BudgetDao.CreateDataToExcel_BudgetvsActualSummaryAccount(DeptID, Tahun, user);
        }

        public DataTable CreateDataToExcel_BudgetvsActualSummaryAccount_All(string DeptID, string Tahun, string user)
        {
            return BudgetDao.CreateDataToExcel_BudgetvsActualSummaryAccount_All(DeptID, Tahun, user);
        }

    }
}
