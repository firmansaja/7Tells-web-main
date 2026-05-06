using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects.Helpers;

namespace DataObjects
{
    public class EBudgetDao : IEbudgetDao
    {
        static Db db = new Db();
        public List<BO_EBudget> GetModules(string th, string dept, string user)
        {
            try
            {

                object[] Params = { "@tahun", th, "@dept", dept, "@user", user };

                return db.Read("Ebudget_GetSummaryHeader", CommandType.StoredProcedure, Make, Params).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BO_EBudget> Filter(string th, string dept, string acctid)
        {
            string _Query = "select top 1 '" + th + "' as FilterTahun, '" + dept + "' as FilterDeptID,'" + acctid + "' as FilterAcctID From rekap_budget_dept ";
            return db.Read(_Query, CommandType.Text, Make1).ToList();
        }

        public List<BO_EBudget> GetBudgetDetail(string acctid, string deptid, string tahun)
        {
            try
            {

                object[] Params = { "@tahun", tahun, "@dept", deptid, "@acctid", acctid };

                return db.Read("Ebudget_GetDetailBudget", CommandType.StoredProcedure, MakeDetailBarang, Params).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BO_EBudget> FilterDetailBudget(string th, string dept, string acctid)
        {
            string _Query = "select top 1 '" + th + "' as FilterTahun, '" + dept + "' as FilterDeptID,'" + acctid + "' as FilterAcctID From rekap_budget_dept ";
            return db.Read(_Query, CommandType.Text, Make1).ToList();
        } public List<BO_EBudget> FilterDetailBudget_Pl(string th, string dept, string acctid)
        {
            string _Query = "select top 1 '" + th + "' as FilterTahun, '" + dept + "' as FilterDeptID,'" + acctid + "' as FilterAcctID From rekap_budget_dept ";
            return db.Read(_Query, CommandType.Text, Make2).ToList();
        }

        public List<BO_EBudget> GetBudgetDetail2(string acctid, string deptid, string tahun)
        {
            try
            {

                object[] Params = { "@tahun", tahun, "@dept", deptid, "@acctid", acctid };

                return db.Read("Ebudget_GetDetailBudget2", CommandType.StoredProcedure, MakeDetailBarang2, Params).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BO_EBudget> GetBudgetOpen(string Status)
        {
            //string _Query = "select * from BudgetSetting where Status = '" + Status + "' ";
            //return db.Read(_Query, CommandType.Text, MakeGetOpenBudget).ToList();

            return db.Read("Ebudget_GetOpenBudget", CommandType.StoredProcedure, MakeGetOpenBudget).ToList();
        }

        public List<BO_EBudget> GetBudgetDetailEdit(string IdNo)
        {
            object[] Params = { "@IdNo", IdNo };

            return db.Read("Ebudget_GetDetailBudgetEdit", CommandType.StoredProcedure, MakeDetailBarang2, Params).ToList();

        }

        public List<BO_EBudget> GetBudgetDetailEdit_Before(string IdNo,string semester)
        {
            object[] Params = { "@IdNo", IdNo, "@semester", semester };

            return db.Read("Ebudget_GetDetailBudgetEdit_Before", CommandType.StoredProcedure, MakeDetailBarang2, Params).ToList();

        }
        public List<BO_EBudget> GetTotalAccount(string acctid, string dept, string tahun)
        {
            object[] Params = {  "@acctid", acctid, "@dept", dept, "@tahun", tahun };

            return db.Read("Ebudget_GetTotalAccountSemester", CommandType.StoredProcedure, MakeGetTotalAccount, Params).ToList();

        }

        public List<BO_EBudget> GetApproveBOD(string acctid, string dept, string tahun)
        {
            object[] Params = { "@acctid", acctid, "@dept", dept, "@tahun", tahun };

            return db.Read("Ebudget_GetApproveBOD", CommandType.StoredProcedure, MakeGetTotalAccount, Params).ToList();

        }

        public int SimpanBudget(BO_EBudget data)
        {
            try
            {
                int Result = -1;

                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "IdNo", Value = data.IdNo });
                Params.Add(new SqlParameter() { ParameterName = "keterangan", Value = data.Keterangan });
                Params.Add(new SqlParameter() { ParameterName = "unit", Value = data.Unit });

                Params.Add(new SqlParameter() { ParameterName = "jan_qty", Value = data.jan_qty });
                Params.Add(new SqlParameter() { ParameterName = "jan_harga", Value = data.jan_harga });

                Params.Add(new SqlParameter() { ParameterName = "feb_qty", Value = data.feb_qty });
                Params.Add(new SqlParameter() { ParameterName = "feb_harga", Value = data.feb_harga });

                Params.Add(new SqlParameter() { ParameterName = "mar_qty", Value = data.mar_qty });
                Params.Add(new SqlParameter() { ParameterName = "mar_harga", Value = data.mar_harga });

                Params.Add(new SqlParameter() { ParameterName = "apr_qty", Value = data.apr_qty });
                Params.Add(new SqlParameter() { ParameterName = "apr_harga", Value = data.apr_harga });

                Params.Add(new SqlParameter() { ParameterName = "mei_qty", Value = data.mei_qty });
                Params.Add(new SqlParameter() { ParameterName = "mei_harga", Value = data.mei_harga });

                Params.Add(new SqlParameter() { ParameterName = "jun_qty", Value = data.jun_qty });
                Params.Add(new SqlParameter() { ParameterName = "jun_harga", Value = data.jun_harga });

                Params.Add(new SqlParameter() { ParameterName = "jul_qty", Value = data.jul_qty });
                Params.Add(new SqlParameter() { ParameterName = "jul_harga", Value = data.jul_harga });

                Params.Add(new SqlParameter() { ParameterName = "agt_qty", Value = data.agt_qty });
                Params.Add(new SqlParameter() { ParameterName = "agt_harga", Value = data.agt_harga });

                Params.Add(new SqlParameter() { ParameterName = "sep_qty", Value = data.sep_qty });
                Params.Add(new SqlParameter() { ParameterName = "sep_harga", Value = data.sep_harga });

                Params.Add(new SqlParameter() { ParameterName = "okt_qty", Value = data.okt_qty });
                Params.Add(new SqlParameter() { ParameterName = "okt_harga", Value = data.okt_harga });

                Params.Add(new SqlParameter() { ParameterName = "nov_qty", Value = data.nov_qty });
                Params.Add(new SqlParameter() { ParameterName = "nov_harga", Value = data.nov_harga });

                Params.Add(new SqlParameter() { ParameterName = "des_qty", Value = data.des_qty });
                Params.Add(new SqlParameter() { ParameterName = "des_harga", Value = data.des_harga });

                Result = SQLHelpers.ExecQueryWithValue("Ebudget_SimpanBudget", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SimpanBudget_Baru(BO_EBudget data)
        {
            try
            {
                int Result = -1;

                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "AcctID", Value = data.AcctID });
                Params.Add(new SqlParameter() { ParameterName = "AcctName", Value = data.AcctName });
                Params.Add(new SqlParameter() { ParameterName = "DeptID", Value = data.DeptID });
                Params.Add(new SqlParameter() { ParameterName = "SiteID", Value = data.SiteID });
                Params.Add(new SqlParameter() { ParameterName = "Tahun", Value = data.Tahun });
                Params.Add(new SqlParameter() { ParameterName = "Keterangan", Value = data.Keterangan });
                Params.Add(new SqlParameter() { ParameterName = "Unit", Value = data.Unit});


                Params.Add(new SqlParameter() { ParameterName = "jan_qty", Value = data.jan_qty });
                Params.Add(new SqlParameter() { ParameterName = "jan_harga", Value = data.jan_harga });

                Params.Add(new SqlParameter() { ParameterName = "feb_qty", Value = data.feb_qty });
                Params.Add(new SqlParameter() { ParameterName = "feb_harga", Value = data.feb_harga });

                Params.Add(new SqlParameter() { ParameterName = "mar_qty", Value = data.mar_qty });
                Params.Add(new SqlParameter() { ParameterName = "mar_harga", Value = data.mar_harga });

                Params.Add(new SqlParameter() { ParameterName = "apr_qty", Value = data.apr_qty });
                Params.Add(new SqlParameter() { ParameterName = "apr_harga", Value = data.apr_harga });

                Params.Add(new SqlParameter() { ParameterName = "mei_qty", Value = data.mei_qty });
                Params.Add(new SqlParameter() { ParameterName = "mei_harga", Value = data.mei_harga });

                Params.Add(new SqlParameter() { ParameterName = "jun_qty", Value = data.jun_qty });
                Params.Add(new SqlParameter() { ParameterName = "jun_harga", Value = data.jun_harga });

                Params.Add(new SqlParameter() { ParameterName = "jul_qty", Value = data.jul_qty });
                Params.Add(new SqlParameter() { ParameterName = "jul_harga", Value = data.jul_harga });

                Params.Add(new SqlParameter() { ParameterName = "agt_qty", Value = data.agt_qty });
                Params.Add(new SqlParameter() { ParameterName = "agt_harga", Value = data.agt_harga });

                Params.Add(new SqlParameter() { ParameterName = "sep_qty", Value = data.sep_qty });
                Params.Add(new SqlParameter() { ParameterName = "sep_harga", Value = data.sep_harga });

                Params.Add(new SqlParameter() { ParameterName = "okt_qty", Value = data.okt_qty });
                Params.Add(new SqlParameter() { ParameterName = "okt_harga", Value = data.okt_harga });

                Params.Add(new SqlParameter() { ParameterName = "nov_qty", Value = data.nov_qty });
                Params.Add(new SqlParameter() { ParameterName = "nov_harga", Value = data.nov_harga });

                Params.Add(new SqlParameter() { ParameterName = "des_qty", Value = data.des_qty });
                Params.Add(new SqlParameter() { ParameterName = "des_harga", Value = data.des_harga });

                Result = SQLHelpers.ExecQueryWithValue("[Ebudget_SimpanBudget_Baru]", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SimpanBudget_Upload(BudgetDetail data)
        {
            try
            {
                int Result = -1;

                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "AcctID", Value = (data.AcctID).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "AcctName", Value = (data.AcctName).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "DeptID", Value = (data.DeptID).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "SiteID", Value = (data.SiteID).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "Tahun", Value = (data.Tahun).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "Keterangan", Value = (data.Keterangan).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "Unit", Value = data.Unit ?? "" });


                Params.Add(new SqlParameter() { ParameterName = "jan_qty", Value = data.jan_qty });
                Params.Add(new SqlParameter() { ParameterName = "jan_harga", Value = data.jan_harga });

                Params.Add(new SqlParameter() { ParameterName = "feb_qty", Value = data.feb_qty });
                Params.Add(new SqlParameter() { ParameterName = "feb_harga", Value = data.feb_harga });

                Params.Add(new SqlParameter() { ParameterName = "mar_qty", Value = data.mar_qty });
                Params.Add(new SqlParameter() { ParameterName = "mar_harga", Value = data.mar_harga });

                Params.Add(new SqlParameter() { ParameterName = "apr_qty", Value = data.apr_qty });
                Params.Add(new SqlParameter() { ParameterName = "apr_harga", Value = data.apr_harga });

                Params.Add(new SqlParameter() { ParameterName = "mei_qty", Value = data.mei_qty });
                Params.Add(new SqlParameter() { ParameterName = "mei_harga", Value = data.mei_harga });

                Params.Add(new SqlParameter() { ParameterName = "jun_qty", Value = data.jun_qty });
                Params.Add(new SqlParameter() { ParameterName = "jun_harga", Value = data.jun_harga });

                Params.Add(new SqlParameter() { ParameterName = "jul_qty", Value = data.jul_qty });
                Params.Add(new SqlParameter() { ParameterName = "jul_harga", Value = data.jul_harga });

                Params.Add(new SqlParameter() { ParameterName = "agt_qty", Value = data.agt_qty });
                Params.Add(new SqlParameter() { ParameterName = "agt_harga", Value = data.agt_harga });

                Params.Add(new SqlParameter() { ParameterName = "sep_qty", Value = data.sep_qty });
                Params.Add(new SqlParameter() { ParameterName = "sep_harga", Value = data.sep_harga });

                Params.Add(new SqlParameter() { ParameterName = "okt_qty", Value = data.okt_qty });
                Params.Add(new SqlParameter() { ParameterName = "okt_harga", Value = data.okt_harga });

                Params.Add(new SqlParameter() { ParameterName = "nov_qty", Value = data.nov_qty });
                Params.Add(new SqlParameter() { ParameterName = "nov_harga", Value = data.nov_harga });

                Params.Add(new SqlParameter() { ParameterName = "des_qty", Value = data.des_qty });
                Params.Add(new SqlParameter() { ParameterName = "des_harga", Value = data.des_harga });

                Result = SQLHelpers.ExecQueryWithValue("Ebudget_SimpanBudget_Baru", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditUploadExcel(BudgetDetail data)
        {
            try
            {
                int Result = -1;

                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "IdNo", Value = data.IdNo });
                Params.Add(new SqlParameter() { ParameterName = "AcctID", Value = data.AcctID });
                Params.Add(new SqlParameter() { ParameterName = "AcctName", Value = data.AcctName });
                Params.Add(new SqlParameter() { ParameterName = "DeptID", Value = data.DeptID });
                Params.Add(new SqlParameter() { ParameterName = "SiteID", Value = data.SiteID });
                Params.Add(new SqlParameter() { ParameterName = "Tahun", Value = data.Tahun });
                Params.Add(new SqlParameter() { ParameterName = "Keterangan", Value = data.Keterangan });
                Params.Add(new SqlParameter() { ParameterName = "Unit", Value = data.Unit ?? "" });


                Params.Add(new SqlParameter() { ParameterName = "jan_qty", Value = data.jan_qty });
                Params.Add(new SqlParameter() { ParameterName = "jan_harga", Value = data.jan_harga });

                Params.Add(new SqlParameter() { ParameterName = "feb_qty", Value = data.feb_qty });
                Params.Add(new SqlParameter() { ParameterName = "feb_harga", Value = data.feb_harga });

                Params.Add(new SqlParameter() { ParameterName = "mar_qty", Value = data.mar_qty });
                Params.Add(new SqlParameter() { ParameterName = "mar_harga", Value = data.mar_harga });

                Params.Add(new SqlParameter() { ParameterName = "apr_qty", Value = data.apr_qty });
                Params.Add(new SqlParameter() { ParameterName = "apr_harga", Value = data.apr_harga });

                Params.Add(new SqlParameter() { ParameterName = "mei_qty", Value = data.mei_qty });
                Params.Add(new SqlParameter() { ParameterName = "mei_harga", Value = data.mei_harga });

                Params.Add(new SqlParameter() { ParameterName = "jun_qty", Value = data.jun_qty });
                Params.Add(new SqlParameter() { ParameterName = "jun_harga", Value = data.jun_harga });

                Params.Add(new SqlParameter() { ParameterName = "jul_qty", Value = data.jul_qty });
                Params.Add(new SqlParameter() { ParameterName = "jul_harga", Value = data.jul_harga });

                Params.Add(new SqlParameter() { ParameterName = "agt_qty", Value = data.agt_qty });
                Params.Add(new SqlParameter() { ParameterName = "agt_harga", Value = data.agt_harga });

                Params.Add(new SqlParameter() { ParameterName = "sep_qty", Value = data.sep_qty });
                Params.Add(new SqlParameter() { ParameterName = "sep_harga", Value = data.sep_harga });

                Params.Add(new SqlParameter() { ParameterName = "okt_qty", Value = data.okt_qty });
                Params.Add(new SqlParameter() { ParameterName = "okt_harga", Value = data.okt_harga });

                Params.Add(new SqlParameter() { ParameterName = "nov_qty", Value = data.nov_qty });
                Params.Add(new SqlParameter() { ParameterName = "nov_harga", Value = data.nov_harga });

                Params.Add(new SqlParameter() { ParameterName = "des_qty", Value = data.des_qty });
                Params.Add(new SqlParameter() { ParameterName = "des_harga", Value = data.des_harga });

                Result = SQLHelpers.ExecQueryWithValue("Ebudget_EditUpload_Excel", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditUploadExcel_S2(BudgetDetail data)
        {
            try
            {
                int Result = -1;

                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "IdNo", Value = data.IdNo });
                Params.Add(new SqlParameter() { ParameterName = "AcctID", Value = data.AcctID });
                Params.Add(new SqlParameter() { ParameterName = "AcctName", Value = data.AcctName });
                Params.Add(new SqlParameter() { ParameterName = "DeptID", Value = data.DeptID });
                Params.Add(new SqlParameter() { ParameterName = "SiteID", Value = data.SiteID });
                Params.Add(new SqlParameter() { ParameterName = "Tahun", Value = data.Tahun });
                Params.Add(new SqlParameter() { ParameterName = "Keterangan", Value = data.Keterangan });
                Params.Add(new SqlParameter() { ParameterName = "Unit", Value = data.Unit ?? "" });


                //Params.Add(new SqlParameter() { ParameterName = "jan_qty", Value = data.jan_qty });
                //Params.Add(new SqlParameter() { ParameterName = "jan_harga", Value = data.jan_harga });

                //Params.Add(new SqlParameter() { ParameterName = "feb_qty", Value = data.feb_qty });
                //Params.Add(new SqlParameter() { ParameterName = "feb_harga", Value = data.feb_harga });

                //Params.Add(new SqlParameter() { ParameterName = "mar_qty", Value = data.mar_qty });
                //Params.Add(new SqlParameter() { ParameterName = "mar_harga", Value = data.mar_harga });

                //Params.Add(new SqlParameter() { ParameterName = "apr_qty", Value = data.apr_qty });
                //Params.Add(new SqlParameter() { ParameterName = "apr_harga", Value = data.apr_harga });

                //Params.Add(new SqlParameter() { ParameterName = "mei_qty", Value = data.mei_qty });
                //Params.Add(new SqlParameter() { ParameterName = "mei_harga", Value = data.mei_harga });

                //Params.Add(new SqlParameter() { ParameterName = "jun_qty", Value = data.jun_qty });
                //Params.Add(new SqlParameter() { ParameterName = "jun_harga", Value = data.jun_harga });

                Params.Add(new SqlParameter() { ParameterName = "jul_qty", Value = data.jul_qty });
                Params.Add(new SqlParameter() { ParameterName = "jul_harga", Value = data.jul_harga });

                Params.Add(new SqlParameter() { ParameterName = "agt_qty", Value = data.agt_qty });
                Params.Add(new SqlParameter() { ParameterName = "agt_harga", Value = data.agt_harga });

                Params.Add(new SqlParameter() { ParameterName = "sep_qty", Value = data.sep_qty });
                Params.Add(new SqlParameter() { ParameterName = "sep_harga", Value = data.sep_harga });

                Params.Add(new SqlParameter() { ParameterName = "okt_qty", Value = data.okt_qty });
                Params.Add(new SqlParameter() { ParameterName = "okt_harga", Value = data.okt_harga });

                Params.Add(new SqlParameter() { ParameterName = "nov_qty", Value = data.nov_qty });
                Params.Add(new SqlParameter() { ParameterName = "nov_harga", Value = data.nov_harga });

                Params.Add(new SqlParameter() { ParameterName = "des_qty", Value = data.des_qty });
                Params.Add(new SqlParameter() { ParameterName = "des_harga", Value = data.des_harga });

                Result = SQLHelpers.ExecQueryWithValue("Ebudget_EditUpload_Excel_S2", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CreateExcell(string DeptId, string Tahun)
        {
            DataTable dt = new DataTable();

            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            dt = SQLHelpers.GetDataTableByParam("[Ebudget_CreateExcel]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        public DataTable CreateDataToExcel_SummaryDept(string DeptId, string Tahun, string user)
        {
            DataTable dt = new DataTable();
             List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            Params.Add(new SqlParameter() { ParameterName = "user", Value = user });
            dt = SQLHelpers.GetDataTableByParam("[Ebudget_Summary]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        public int CekBarang(string Barang,string tahun)
        {
            DataTable dt;
            int Result;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(new SqlParameter() { ParameterName = "IdNo", Value = Barang });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = tahun });

            dt = SQLHelpers.GetDataTableByParam("[EBudget_Cek_IdNo]", CommandType.StoredProcedure, Params, db.connectionString);
            if (dt.Rows.Count > 0)
            {
                Result = 1;
            }
            else
            {
                Result = 0;
            }
            return Result;

        }

        public int SimpanBudget_Upload_S2(BudgetDetail data)
        {
            try
            {
                int Result = -1;

                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "AcctID", Value = (data.AcctID).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "AcctName", Value = (data.AcctName).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "DeptID", Value = (data.DeptID).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "SiteID", Value = (data.SiteID).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "Tahun", Value = (data.Tahun).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "Keterangan", Value = (data.Keterangan).Trim() });
                Params.Add(new SqlParameter() { ParameterName = "Unit", Value = data.Unit == null ? "" : data.Unit.Trim() });


                //Params.Add(new SqlParameter() { ParameterName = "jan_qty", Value = data.jan_qty });
                //Params.Add(new SqlParameter() { ParameterName = "jan_harga", Value = data.jan_harga });

                //Params.Add(new SqlParameter() { ParameterName = "feb_qty", Value = data.feb_qty });
                //Params.Add(new SqlParameter() { ParameterName = "feb_harga", Value = data.feb_harga });

                //Params.Add(new SqlParameter() { ParameterName = "mar_qty", Value = data.mar_qty });
                //Params.Add(new SqlParameter() { ParameterName = "mar_harga", Value = data.mar_harga });

                //Params.Add(new SqlParameter() { ParameterName = "apr_qty", Value = data.apr_qty });
                //Params.Add(new SqlParameter() { ParameterName = "apr_harga", Value = data.apr_harga });

                //Params.Add(new SqlParameter() { ParameterName = "mei_qty", Value = data.mei_qty });
                //Params.Add(new SqlParameter() { ParameterName = "mei_harga", Value = data.mei_harga });

                //Params.Add(new SqlParameter() { ParameterName = "jun_qty", Value = data.jun_qty });
                //Params.Add(new SqlParameter() { ParameterName = "jun_harga", Value = data.jun_harga });

                Params.Add(new SqlParameter() { ParameterName = "jul_qty", Value = data.jul_qty });
                Params.Add(new SqlParameter() { ParameterName = "jul_harga", Value = data.jul_harga });

                Params.Add(new SqlParameter() { ParameterName = "agt_qty", Value = data.agt_qty });
                Params.Add(new SqlParameter() { ParameterName = "agt_harga", Value = data.agt_harga });

                Params.Add(new SqlParameter() { ParameterName = "sep_qty", Value = data.sep_qty });
                Params.Add(new SqlParameter() { ParameterName = "sep_harga", Value = data.sep_harga });

                Params.Add(new SqlParameter() { ParameterName = "okt_qty", Value = data.okt_qty });
                Params.Add(new SqlParameter() { ParameterName = "okt_harga", Value = data.okt_harga });

                Params.Add(new SqlParameter() { ParameterName = "nov_qty", Value = data.nov_qty });
                Params.Add(new SqlParameter() { ParameterName = "nov_harga", Value = data.nov_harga });

                Params.Add(new SqlParameter() { ParameterName = "des_qty", Value = data.des_qty });
                Params.Add(new SqlParameter() { ParameterName = "des_harga", Value = data.des_harga });

                Result = SQLHelpers.ExecQueryWithValue("Ebudget_SimpanBudget_Upload_S2", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_EBudget> GetSummaryBudgetDept(string tahun, string Dept,string user)
        {
            object[] Params = { "@tahun", tahun ,"@dept", Dept, "@user", user };

            return db.Read("Ebudget_Summary", CommandType.StoredProcedure, MakeSummary, Params).ToList();

        }

        public List<BO_EBudget> GetBudgetPresentasi(string th, string Dept, string User)
        {
            try
            {

                object[] Params = { "@tahun", th, "@dept", Dept, "@user", User };

                return db.Read("Ebudget_GetSummaryHeader_Presentasi_1", CommandType.StoredProcedure, Make, Params).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteBudget(BO_EBudget data)
        {
            try
            {
                int Result = -1;

                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "id", Value = data.IdNo });
                              

                Result = SQLHelpers.ExecQueryWithValue("Ebudget_DeleteBudget", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComboList> ListComboAccount()
        {
            try
            {


                return db.Read("Ebudget_GetAccount", CommandType.StoredProcedure, MakeCombo).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ComboList> ListComboDept(string username)
        {
            try
            {

                object[] Params = { "@user", username };

                return db.Read("Ebudget_GetDeptID", CommandType.StoredProcedure, MakeCombodept, Params).ToList();

                //object[] Params = { "@user", username };

                //return db.Read("Ebudget_GetDeptID", CommandType.StoredProcedure, Params, MakeCombodept).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CreateDataToExcel_BudgetSummaryAccount(string DeptId, string Tahun, string user)
        {
            DataTable dt = new DataTable();

            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            Params.Add(new SqlParameter() { ParameterName = "user", Value = user });
            dt = SQLHelpers.GetDataTableByParam("[Ebudget_GetSummaryHeader_Presentasi_1]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        public DataTable CreateDataToExcel_BudgetDetail(string DeptId, string Tahun, string account)
        {
            DataTable dt = new DataTable();

            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            Params.Add(new SqlParameter() { ParameterName = "acctid", Value = account });
            dt = SQLHelpers.GetDataTableByParam("[Ebudget_GetDetailBudget2]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        public DataTable CreateDataToExcel_ProfitLost( string Tahun)
        {
            DataTable dt = new DataTable();

            List<SqlParameter> Params = new List<SqlParameter>();

            //Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            //Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            dt = SQLHelpers.GetDataTableByParam("[EbudgetPL_GetProfitLost]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        public DataTable CreateDataToExcel_SummaryDept_Detail(string DeptId, string Tahun, string user)
        {
            DataTable dt = new DataTable();
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            Params.Add(new SqlParameter() { ParameterName = "user", Value = user });
            dt = SQLHelpers.GetDataTableByParam("[Ebudget_CreateDataToExcel_SummaryDept_Detail]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        public DataTable CreateDataToExcel_BudgetSummaryAccount_All(string DeptId, string Tahun, string user)
        {
            DataTable dt = new DataTable();

            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            Params.Add(new SqlParameter() { ParameterName = "user", Value = user });
            dt = SQLHelpers.GetDataTableByParam("[Ebudget_GetSummaryHeader_Presentasi_All]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        public DataTable CreateDataToExcel_BudgetDetail_ByAccount(string DeptId, string Tahun, string account, string user)
        {
            DataTable dt = new DataTable();
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            Params.Add(new SqlParameter() { ParameterName = "user", Value = user });
            Params.Add(new SqlParameter() { ParameterName = "acctid", Value = account });
            dt = SQLHelpers.GetDataTableByParam("[Ebudget_CreateDataToExcel_Detail_ByAccount]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        public int testRollback()
        {
            try
            {
                int Result = -1;

                try
                {
                    using (SqlConnection Conn = new SqlConnection(SQLHelpers.strConn))
                    {
                        Conn.Open();
                        using (SqlTransaction Transact = Conn.BeginTransaction())
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = Conn;
                            cmd.Transaction = Transact;

                            try
                            {


                                cmd.CommandText = "Update Tbtest set nama = 'Miftah' where kode = '1'";
                                cmd.ExecuteNonQuery();


                                cmd.CommandText = "Insert into Tbtest (Kode, Nama) VALUES ('abc', 'Description')";
                                cmd.ExecuteNonQuery();

                                //string QUERY = @"Update Tbtest 
                                //               SET Nama = 'Miftah'" +
                                //           " where [Kode] = '1'";
                                //SQLHelpers.ExecQueryByCommandSP(QUERY);

                                //string QUERY2 = @"INSERT INTO [Tbtest]
                                //               ([Kode]
                                //               ,[Nama])
                                //         VALUES
                                //               ('ABC'," +
                                //                "'Dian')";

                                //SQLHelpers.ExecQuery(QUERY2);

                                Transact.Commit();
                            }
                            catch (Exception ex)
                            {
                                Transact.Rollback();
                                throw ex;
                            }
                            finally
                            {
                                cmd = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_EBudgetvsActual> GetSummaryBudgetvsActualDept(string tahun, string Dept, string user)
        {
            object[] Params = { "@tahun", tahun, "@dept", Dept, "@user", user };

            return db.Read("Ebudget_BudgetvsActualSummary", CommandType.StoredProcedure, MakeSummaryBudgetvsActual, Params).ToList();

        }

        public List<BO_EBudgetvsActual> GetBudgetvsActualPresentasi(string th, string Dept, string User)
        {
            try
            {

                object[] Params = { "@tahun", th, "@dept", Dept, "@user", User };

                return db.Read("Ebudget_BudgetvsActualDetail", CommandType.StoredProcedure, MakeBudgetActual, Params).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CreateDataToExcel_ActualSummaryDept(string DeptId, string Tahun, string user)
        {
            DataTable dt = new DataTable();
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            Params.Add(new SqlParameter() { ParameterName = "user", Value = user });
            dt = SQLHelpers.GetDataTableByParam("[Ebudget_BudgetvsActualSummary]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        public DataTable CreateDataToExcel_BudgetvsActualSummaryAccount(string DeptId, string Tahun, string user)
        {
            DataTable dt = new DataTable();

            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            Params.Add(new SqlParameter() { ParameterName = "user", Value = user });
            dt = SQLHelpers.GetDataTableByParam("[Ebudget_BudgetvsActualDetail]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        public DataTable CreateDataToExcel_BudgetvsActualSummaryAccount_All(string DeptId, string Tahun, string user)
        {
            DataTable dt = new DataTable();

            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "dept", Value = DeptId });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = Tahun });
            Params.Add(new SqlParameter() { ParameterName = "user", Value = user });
            dt = SQLHelpers.GetDataTableByParam("[Ebudget_BudgetvsActualDetail]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        static readonly Func<IDataReader, BO_EBudget> Make = reader =>
           new BO_EBudget
           {
               RowNumber = reader["RNo"].AsInt(),
               No = reader["No"].AsInt(),
               AcctID = reader["AcctID"].AsString(),
               AcctName = reader["AcctName"].AsString(),
               DeptID = reader["DeptID"].AsString(),
               Tahun = reader["tahun"].AsString(),
               Jan = reader["jan"].AsDouble(),
               Feb = reader["feb"].AsDouble(),
               Mar = reader["mar"].AsDouble(),
               Apr = reader["apr"].AsDouble(),
               Mei = reader["mei"].AsDouble(),
               Jun = reader["jun"].AsDouble(),
               Jul = reader["jul"].AsDouble(),
               Agst = reader["Agst"].AsDouble(),
               Sept = reader["Sept"].AsDouble(),
               Okt = reader["okt"].AsDouble(),
               Nov = reader["nov"].AsDouble(),
               Des = reader["des"].AsDouble(),
               Subtotal = reader["subtotal"].AsDouble(),
           };

        static readonly Func<IDataReader, BO_EBudgetvsActual> MakeBudgetActual = reader =>
           new BO_EBudgetvsActual
           {
               RowNumber = reader["RNo"].AsInt(),
               No = reader["No"].AsInt(),
               AcctID = reader["Acct"].AsString(),
               AcctName = reader["AcctName"].AsString(),
               DeptID = reader["DeptID"].AsString(),
               Tahun = reader["Tahun"].AsString(),
               JanBudget = reader["JanBudget"].AsDouble(),
               FebBudget = reader["FebBudget"].AsDouble(),
               MarBudget = reader["MarBudget"].AsDouble(),
               AprBudget = reader["AprBudget"].AsDouble(),
               MeiBudget = reader["MeiBudget"].AsDouble(),
               JunBudget = reader["JunBudget"].AsDouble(),
               JulBudget = reader["JulBudget"].AsDouble(),
               AgtBudget = reader["AgtBudget"].AsDouble(),
               SepBudget = reader["SepBudget"].AsDouble(),
               OktBudget = reader["OktBudget"].AsDouble(),
               NovBudget = reader["NovBudget"].AsDouble(),
               DesBudget = reader["DesBudget"].AsDouble(),
               BudgetSubtotal = reader["BudgetSubtotal"].AsDouble(),
               JanActual = reader["JanActual"].AsDouble(),
               FebActual = reader["FebActual"].AsDouble(),
               MarActual = reader["MarActual"].AsDouble(),
               AprActual = reader["AprActual"].AsDouble(),
               MeiActual = reader["MeiActual"].AsDouble(),
               JunActual = reader["JunActual"].AsDouble(),
               JulActual = reader["JulActual"].AsDouble(),
               AgtActual = reader["AgtActual"].AsDouble(),
               SepActual = reader["SepActual"].AsDouble(),
               OktActual = reader["OktActual"].AsDouble(),
               NovActual = reader["NovActual"].AsDouble(),
               DesActual = reader["DesActual"].AsDouble(),
               ActualSubtotal = reader["ActualSubtotal"].AsDouble(),
           };

        static readonly Func<IDataReader, BO_EBudget> Make1 = reader =>
          new BO_EBudget
          {
              FilterTahun = reader["FilterTahun"].AsString(),
              FilterDeptID = reader["FilterDeptID"].AsString(),
              FilterAcctID = reader["FilterAcctID"].AsString(),
          };

        static readonly Func<IDataReader, BO_EBudget> Make2 = reader =>
         new BO_EBudget
         {
             FilterTahun = reader["FilterTahun"].AsString(),
             FilterDeptID = reader["FilterDeptID"].AsString(),
             FilterAcctID = reader["FilterAcctID"].AsString(),
         };

        static readonly Func<IDataReader, ComboList> MakeCombo = reader =>
         new ComboList
         {
             Value = reader["AcctID"].AsString(),
             Text = reader["AcctID"].AsString() + " / " + reader["AcctName"].AsString(),
         };

        static readonly Func<IDataReader, ComboList> MakeCombodept = reader =>
        new ComboList
        {
            Value = reader["DeptID"].AsString(),
            Text = reader["DeptID"].AsString().AsString(),
        };

        static readonly Func<IDataReader, BO_EBudget> MakeGetOpenBudget = reader =>
         new BO_EBudget
         {
             OpenTahun = reader["Tahun"].AsString(),
             OpenSemester = reader["Semester"].AsString(),
             OpenStatus = reader["Status"].AsString(),
         };

        static readonly Func<IDataReader, BO_EBudget> MakeDetailBarang = reader =>
          new BO_EBudget
          {
              AcctID = reader["AcctID"].AsString(),
              AcctName = reader["AcctName"].AsString(),
              DeptID = reader["DeptID"].AsString(),
              Tahun = reader["tahun"].AsString(),
              Keterangan = reader["Keterangan"].AsString(),
          };


        static readonly Func<IDataReader, BO_EBudget> MakeDetailBarang2 = reader =>
        new BO_EBudget
        {
            IdNo = reader["IdNo"].AsInt(),
            AcctID = reader["AcctID"].AsString(),
            AcctName = reader["AcctName"].AsString(),
            DeptID = reader["DeptID"].AsString(),
            Tahun = reader["tahun"].AsString(),
            Unit = reader["unit"].AsString(),
            Keterangan = reader["Keterangan"].AsString(),
            SiteID = reader["SiteID"].AsString(),

            jan_qty = reader["jan_qty"].AsDouble(),
            jan_harga = reader["jan_harga"].AsDouble(),
            Jan = reader["jan"].AsDouble(),

            feb_qty = reader["feb_qty"].AsDouble(),
            feb_harga = reader["feb_harga"].AsDouble(),
            Feb = reader["feb"].AsDouble(),

            mar_qty = reader["mar_qty"].AsDouble(),
            mar_harga = reader["mar_harga"].AsDouble(),
            Mar = reader["Mar"].AsDouble(),

            apr_qty = reader["apr_qty"].AsDouble(),
            apr_harga = reader["apr_harga"].AsDouble(),
            Apr = reader["Apr"].AsDouble(),

            mei_qty = reader["mei_qty"].AsDouble(),
            mei_harga = reader["mei_harga"].AsDouble(),
            Mei = reader["Mei"].AsDouble(),

            jun_qty = reader["jun_qty"].AsDouble(),
            jun_harga = reader["jun_harga"].AsDouble(),
            Jun = reader["Jun"].AsDouble(),

            jul_qty = reader["jul_qty"].AsDouble(),
            jul_harga = reader["jul_harga"].AsDouble(),
            Jul = reader["Jul"].AsDouble(),

            agt_qty = reader["agt_qty"].AsDouble(),
            agt_harga = reader["agt_harga"].AsDouble(),
            Agst = reader["Ags"].AsDouble(),

            sep_qty = reader["sep_qty"].AsDouble(),
            sep_harga = reader["sep_harga"].AsDouble(),
            Sept = reader["Sep"].AsDouble(),

            okt_qty = reader["okt_qty"].AsDouble(),
            okt_harga = reader["okt_harga"].AsDouble(),
            Okt = reader["Okt"].AsDouble(),

            nov_qty = reader["nov_qty"].AsDouble(),
            nov_harga = reader["nov_harga"].AsDouble(),
            Nov = reader["Nov"].AsDouble(),

            des_qty = reader["des_qty"].AsDouble(),
            des_harga = reader["des_harga"].AsDouble(),
            Des = reader["Des"].AsDouble(),

            Subtotal =  reader["SubTotal"].AsDouble(),

        };

        static readonly Func<IDataReader, BO_EBudget> MakeGetTotalAccount = reader =>
         new BO_EBudget
         {
             Semester1 = reader["semester1"].AsDouble(),
             Semester2 = reader["semester2"].AsDouble(),


         };

        static readonly Func<IDataReader, BO_EBudget> MakeSummary = reader =>
         new BO_EBudget
         {
             No = reader["No"].AsInt(),
             DeptID = reader["DeptID"].AsString(),
             DeptName = reader["Desc"].AsString(),
             SiteID = reader["SiteID"].AsString(),
             Tahun = reader["Tahun"].AsString(),
             Subtotal = reader["Subtotal"].AsDouble(),
         };

        static readonly Func<IDataReader, BO_EBudgetvsActual> MakeSummaryBudgetvsActual = reader =>
         new BO_EBudgetvsActual
         {
             No = reader["No"].AsInt(),
             DeptID = reader["DeptID"].AsString(),
             DeptName = reader["Desc"].AsString(),
             SiteID = reader["SiteID"].AsString(),
             Tahun = reader["Tahun"].AsString(),
             BudgetSubtotal = reader["BudgetSubtotal"].AsDouble(),
             ActualSubtotal = reader["ActualSubtotal"].AsDouble(),
         };


    }
}
