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
    public class EbudgetPLDao : IEbudgetPLDao
    {
        static Db db = new Db();

        public List<BO_EBudgetPL> GetFormatPL()
        {
           
            return db.Read("EbudgetPL_GetFormarPL", CommandType.StoredProcedure, MakeGetFormatPL).ToList();

        }

        public List<BO_EBudgetPL> GetProfitLost(string tahun)
        {
            object[] Params = { "@tahun", tahun};
            return db.Read("EbudgetPL_GetProfitLost", CommandType.StoredProcedure, MakeGetFormatProfitLost, Params).ToList();
        }

        public List<BO_EBudgetPL> GetProfitLostByAccount(string acctid, string tahun)
        {
            try
            {

                object[] Params = { "@tahun", tahun, "@acctid", acctid };

                return db.Read("EbudgetPL_GetProfitLostByAccount", CommandType.StoredProcedure, MakeGetFormatProfitLostByAccount, Params).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CreateDataToExcel_ProfitlostByAccount(string acctid, string tahun)
        {
            DataTable dt = new DataTable();
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(new SqlParameter() { ParameterName = "acctid", Value = acctid });
            Params.Add(new SqlParameter() { ParameterName = "tahun", Value = tahun });
            dt = SQLHelpers.GetDataTableByParam("[EbudgetPL_GetProfitLostByAccount]", CommandType.StoredProcedure, Params, db.connectionString);

            return dt;
        }

        static readonly Func<IDataReader, BO_EBudgetPL> MakeGetFormatPL = reader =>
         new BO_EBudgetPL
         {
             //RecNo = reader["RecNo"].AsInt(),
             Urut_Tampil = reader["Urut_Tampil"].AsInt(),
             No_Urut = reader["No_Urut"].AsInt(),
             Ket = reader["ket"].AsString(),
             Tipe = reader["Tipe"].AsString(),
             AcctID = reader["AcctID"].AsString(),

             Tambah = reader["Tambah"].AsString(),
             Kurang = reader["Kurang"].AsString(),


         };

        static readonly Func<IDataReader, BO_EBudgetPL> MakeGetFormatProfitLost = reader =>
        new BO_EBudgetPL
        {
            RecNo = reader["ID"].AsInt(),
            AcctID = reader["AcctID"].AsString(),
            AcctName = reader["AcctName"].AsString(),
            HasilAcct = reader["Total"].AsDouble(),
            HasilAcct1 = reader["Semester1"].AsDouble(),
            HasilAcct2 = reader["Semester2"].AsDouble(),
            Tahun = reader["Tahun"].AsString(),

            Jan = reader["Januari"].AsDouble(),
            Feb = reader["Februari"].AsDouble(),
            Mar = reader["Maret"].AsDouble(),
            Apr = reader["April"].AsDouble(),
            Mei = reader["Mei"].AsDouble(),
            Jun = reader["Juni"].AsDouble(),
            Jul = reader["juli"].AsDouble(),
            Ags = reader["Agustus"].AsDouble(),
            Sep = reader["September"].AsDouble(),
            Okt = reader["Oktober"].AsDouble(),
            Nov = reader["November"].AsDouble(),
            Des = reader["Desember"].AsDouble(),

            TotalActual = reader["TotalActual"].AsDouble(),

        };


        static readonly Func<IDataReader, BO_EBudgetPL> MakeGetFormatProfitLostByAccount = reader =>
        new BO_EBudgetPL
        {

            AcctID = reader["AcctID"].AsString(),
            AcctName = reader["AcctName"].AsString(),

            DeptID = reader["DeptID"].AsString(),
            DeptName = reader["DeptName"].AsString(),

            SiteID = reader["SiteID"].AsString(),
            Tahun = reader["Tahun"].AsString(),

            Jan = reader["jan"].AsDouble(),
            Feb = reader["feb"].AsDouble(),
            Mar = reader["mar"].AsDouble(),
            Apr = reader["apr"].AsDouble(),
            Mei = reader["mei"].AsDouble(),
            Jun = reader["jun"].AsDouble(),
            Jul = reader["jul"].AsDouble(),
            Ags = reader["ags"].AsDouble(),
            Sep = reader["sep"].AsDouble(),
            Okt = reader["okt"].AsDouble(),
            Nov = reader["nov"].AsDouble(),
            Des = reader["des"].AsDouble(),

            Total = reader["total"].AsDouble(),



        };







    }
}
