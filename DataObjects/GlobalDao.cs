using BusinessObjects;
using DataObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class GlobalDao : IGlobalDao
    {
        static Db db = new Db();

        public int InsertApvHistory(ApproveHistory ApvHistory)
        {
            try
            {
                int Result = -1;
                string Query = "";

                Query = @"T_ApproveHistory_Insert";

                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "UserName", Value = ApvHistory.UserName });
                Params.Add(new SqlParameter() { ParameterName = "MenuCode", Value = ApvHistory.MenuCode });
                Params.Add(new SqlParameter() { ParameterName = "DeptID", Value = ApvHistory.DeptID });
                Params.Add(new SqlParameter() { ParameterName = "NoTransaksi", Value = ApvHistory.NoTransaksi });
                Params.Add(new SqlParameter() { ParameterName = "LevelApprove", Value = ApvHistory.LevelApproved });
                Params.Add(new SqlParameter() { ParameterName = "StatusApproved", Value = ApvHistory.StatusApproved });
                Params.Add(new SqlParameter() { ParameterName = "ApprovedBy", Value = ApvHistory.ApprovedBy });
                Params.Add(new SqlParameter() { ParameterName = "IsActive", Value = ApvHistory.IsActive });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.StoredProcedure, Params, db.connectionString);
                               

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateApvHistoryFlagAll(ApproveHistory ApvHistory)
        {
            try
            {
                int Result = -1;
                string Query = "";

                Query = @"T_ApproveHistory_UpdateFlagAll";

                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "form", Value = ApvHistory.MenuCode });
                Params.Add(new SqlParameter() { ParameterName = "NoTransaksi", Value = ApvHistory.NoTransaksi });
                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
