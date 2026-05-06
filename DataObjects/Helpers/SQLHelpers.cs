using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Helpers
{
    public class SQLHelpers
    {
        public static TransactionHelper gh_Trans;
        public static string strConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static DataTable GetDataTable(string pQuery, int pTimeOut = 3000)
        {
            DataTable dta = new DataTable();
            SqlDataAdapter da = null;

            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CommandType.Text;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    da = new SqlDataAdapter(gh_Trans.Command);
                    //da.Fill(dta);
                    da.Fill(dta);
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = strConn;
                        conn.Open();
                        da = new SqlDataAdapter(pQuery, conn);
                        da.Fill(dta);
                    }
                }
                da = null;
                return dta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetDataTableSP(string pQuery, int pTimeOut = 3000)
        {
            DataTable dta = new DataTable();
            SqlDataAdapter da = null;

            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CommandType.StoredProcedure;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    da = new SqlDataAdapter(gh_Trans.Command);
                    //da.Fill(dta);
                    da.Fill(dta);
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = strConn;
                        conn.Open();
                        da = new SqlDataAdapter(pQuery, conn);
                        da.Fill(dta);
                    }
                }
                da = null;
                return dta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int ExecQuery(string pQuery, int pTimeOut = 30)
        {
            int pRowAff = -1;
            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CommandType.Text;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    pRowAff = gh_Trans.Command.ExecuteNonQuery();
                }
                else
                {
                    using (SqlConnection Conn1 = new SqlConnection())
                    {
                        Conn1.ConnectionString = strConn;
                        SqlCommand cmd = new SqlCommand(pQuery, Conn1);
                        cmd.CommandTimeout = pTimeOut;
                        Conn1.Open();
                        pRowAff = cmd.ExecuteNonQuery();
                    }
                }
                return pRowAff;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDataTableByCommand(string pQuery, SqlParameter[] pParam = null, int pTimeOut = 30)
        {
            DataTable dta = new DataTable();
            SqlDataAdapter da = null;
            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CommandType.StoredProcedure;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    gh_Trans.Command.Parameters.Clear();
                    if (pParam != null)
                    {
                        for (int i = 0; i <= pParam.Length - 1; i++)
                        {
                            gh_Trans.Command.Parameters.Add(pParam[i]);
                        }
                    }
                    da = new SqlDataAdapter(gh_Trans.Command);
                    da.Fill(dta);
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = strConn;
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = pQuery;
                        cmd.CommandTimeout = pTimeOut;
                        cmd.Connection = conn;
                        if (pParam != null)
                        {
                            for (int i = 0; i <= pParam.Length - 1; i++)
                            {
                                cmd.Parameters.Add(pParam[i]);
                            }
                        }
                        conn.Open();
                        da = new SqlDataAdapter(cmd);
                        da.Fill(dta);
                    }
                }
                da = null;
                return dta;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static int ExecQueryByCommandSP(string pQuery, SqlParameter[] pParam = null, string pConnStr = "", int pTimeOut = 30)
        {
            int pRowAff = -1;
            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CommandType.StoredProcedure;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    gh_Trans.Command.Parameters.Clear();
                    if (pParam != null)
                    {
                        for (int i = 0; i <= pParam.Length - 1; i++)
                        {
                            gh_Trans.Command.Parameters.Add(pParam[i]);
                        }
                    }
                    pRowAff = gh_Trans.Command.ExecuteNonQuery();
                }
                else
                {
                    using (SqlConnection Conn1 = new SqlConnection())
                    {
                        if (!string.IsNullOrEmpty(pConnStr))
                        {
                            Conn1.ConnectionString = pConnStr;
                        }
                        else
                        {
                            Conn1.ConnectionString = strConn;
                        }
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = pQuery;
                        cmd.CommandTimeout = pTimeOut;
                        cmd.Connection = Conn1;
                        if (pParam != null)
                        {
                            for (int i = 0; i <= pParam.Length - 1; i++)
                            {
                                cmd.Parameters.Add(pParam[i]);
                            }
                        }
                        Conn1.Open();
                        pRowAff = cmd.ExecuteNonQuery();
                    }
                }
                return pRowAff;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int ExecQueryWithValue(string pQuery, CommandType CmdType, List<SqlParameter> parameters, string pConnStr, int pTimeOut = 0)
        {
            int pRowAff = -1;
            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CmdType;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    gh_Trans.Command.Parameters.Clear();
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                            gh_Trans.Command.Parameters.AddWithValue("@" + param.ParameterName, param.Value);
                    }

                    pRowAff = gh_Trans.Command.ExecuteNonQuery();
                }
                else
                    using (SqlConnection Conn1 = new SqlConnection())
                    {
                        Conn1.ConnectionString = pConnStr;
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CmdType;
                        cmd.CommandText = pQuery;
                        cmd.CommandTimeout = pTimeOut;
                        cmd.Connection = Conn1;
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                                cmd.Parameters.AddWithValue("@" + param.ParameterName, param.Value);
                        }
                        Conn1.Open();
                        pRowAff = cmd.ExecuteNonQuery();
                    }
                return pRowAff;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDataTableByParam(string pQuery, CommandType CmdType, List<SqlParameter> parameters, string pConnStr, int pTimeOut = 0)
        {
            DataTable dta = new DataTable();
            SqlDataAdapter da = null;
            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CmdType;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    gh_Trans.Command.Parameters.Clear();
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                            gh_Trans.Command.Parameters.AddWithValue("@" + param.ParameterName, param.Value);
                    }
                    da = new SqlDataAdapter(gh_Trans.Command);
                    da.Fill(dta);
                }
                else
                    using (SqlConnection conn = new SqlConnection(pConnStr))
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            CommandType = CmdType,
                            CommandText = pQuery,
                            CommandTimeout = pTimeOut,
                            Connection = conn
                        };
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                                cmd.Parameters.AddWithValue("@" + param.ParameterName, param.Value);
                        }
                        conn.Open();
                        da = new SqlDataAdapter(cmd);
                        da.Fill(dta);
                    }
                da = null;
                return dta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }



    public class TransactionHelper
    {
        private SqlCommand _Cmd = new SqlCommand();
        public SqlCommand Command
        {
            get
            {
                return _Cmd;
            }
            set
            {
                _Cmd = value;
            }
        }
    }

    public class StandartComboBox
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
