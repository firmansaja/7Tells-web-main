using BusinessObjects;
using DataObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataObjects
{
    public class TrolleyDao : ITrolleyDao
    {
        static Db db = new Db();
        public List<PR> GetPR()
        {
            throw new NotImplementedException();
        }

        public int InsertTrolleyIn()
        {
            throw new NotImplementedException();
        }

        public DataTable CheckTrolley(string KodeTrolley)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "KodeTrolley", Value = KodeTrolley });
                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("usp_Trolley_CheckTrolleyById", CommandType.StoredProcedure, Params, db.connectionString);
                
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SimpanTrolleyInOut(Trolley model, string deptID)
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
                                var NoUpload = GetAutoNumber(model.InOut, deptID);
                                model.NoTrans = NoUpload;

                                InsertHeader(model);

                                //DeleteDetail(NoUpload);
                                for (int i = 0; i < model.Details.Count; i++)
                                {
                                    model.Details[i].NoTrans = NoUpload;
                                    InsertDetail(model.Details[i]);

                                    //Update last master trolley
                                    UpdateTrolley(model, model.Details[i].KodeTrolley);
                                }

                                Result = 1;
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

        public string GetAutoNumber(string InOut, string Site)
        {
            DataTable dt;
            string Result;

            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(new SqlParameter() { ParameterName = "InOut", Value = InOut });
            Params.Add(new SqlParameter() { ParameterName = "Site", Value = Site });

            dt = SQLHelpers.GetDataTableByParam("[usp_Trolley_GetAutoNumberInOutTrolley]", CommandType.StoredProcedure, Params, db.connectionString);
            if (dt.Rows.Count > 0)
            {
                Result = dt.Rows[0]["NoUpload"].ToString();
            }
            else
            {
                Result = "";
            }
            return Result;

        }

        public int InsertHeader(Trolley model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoTrans", Value = model.NoTrans },
                    new SqlParameter() { ParameterName = "InOut", Value = model.InOut },
                    new SqlParameter() { ParameterName = "TglTrans", Value = DateTime.Now },
                    new SqlParameter() { ParameterName = "CustId", Value = model.CustId },
                    new SqlParameter() { ParameterName = "NoMobil", Value = model.NoMobil },
                    new SqlParameter() { ParameterName = "NamaSupir", Value =  model.NamaSupir },
                    new SqlParameter() { ParameterName = "Username", Value =  model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_Trolley_InsertInOutTrolleyHeader", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertDetail(TrolleyDetail model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoTrans", Value = model.NoTrans },
                    new SqlParameter() { ParameterName = "KodeTrolley", Value = model.KodeTrolley }, 
                    new SqlParameter() { ParameterName = "JenisTrolley", Value = model.JenisTrolley },
                    new SqlParameter() { ParameterName = "Proses", Value = model.Proses == null ? "" : model.Proses }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_Trolley_InsertInOutTrolleyDetail", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteDetail(string NoTrans)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "NoTrans", Value = NoTrans });
                Result = SQLHelpers.ExecQueryWithValue("usp_Trolley_DeleteInOutTrolleyDetail", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public int UpdateTrolley(Trolley model, string KdTrolley)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "InOut", Value = model.InOut });
                Params.Add(new SqlParameter() { ParameterName = "TglTrans", Value = model.TglTrans });
                Params.Add(new SqlParameter() { ParameterName = "CustId", Value = model.CustId });
                Params.Add(new SqlParameter() { ParameterName = "KdTrolley", Value = KdTrolley });
                Result = SQLHelpers.ExecQueryWithValue("usp_Trolley_UpdateTrolleyInOut", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
