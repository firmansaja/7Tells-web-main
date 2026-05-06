using BusinessObjects;
using DataObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;

namespace DataObjects
{
    public class LineStoreDao : ILineStoreDao
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static Db db = new Db();

        private void dependencyLineStore_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                FifoHub.GetLineStore();
            }
        }

        public List<LineStorePartModel> GetLineStore()
        {
            try
            {
                SqlDependency.Start(_connString);
                var Trolly = new List<LineStorePartModel>();
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    //using (var command = new SqlCommand(@"SELECT  Id ,
                    //                                            NoTrans ,
                    //                                            TglTrans ,
                    //                                            InvtId ,
                    //                                            PartNo ,
                    //                                            JobNo ,
                    //                                            PartName ,
                    //                                            Qty
                    //                                    FROM    dbo.CTrolly_T_LineStore", connection))
                    using (var command = new SqlCommand(@"SELECT  Id ,
                                                                InvtId ,
                                                                Project ,
                                                                PartNo ,
                                                                JobNo ,
                                                                Type ,
                                                                PartName ,
                                                                Color ,
                                                                Qty ,
                                                                QtyMin ,
                                                                QtyMax ,
                                                                DeliveryHour ,
                                                                IsAktif
                                                        FROM    dbo.CTrolly_M_Part;", connection))
                    {
                        command.Notification = null;
                        var dependency = new SqlDependency(command);

                        dependency.OnChange += new OnChangeEventHandler(dependencyLineStore_OnChange);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();
                    }
                }

                //string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                //string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                //object[] Params = { "@DTDari", stDate, "@DTSampai", enDate };

                Trolly = db.Read("[usp_CTrolly_GetListLineStore]", CommandType.StoredProcedure, MakeLineStore).ToList();

                return Trolly;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CheckPart(string invtId)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "InvtId", Value = invtId });
                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("usp_CTrolly_LSCheckPart", CommandType.StoredProcedure, Params, db.connectionString);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetJadwalLine(string username)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "Username", Value = username });
                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("usp_LS_GetJadwalLine", CommandType.StoredProcedure, Params, db.connectionString);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SimpanScanPart(List<LineStoreScanPartModel> data, string username)
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
                                var noTrans = GetAutoNumber();

                                DateTime today = DateTime.Now;

                                //Insert line store;
                                for (int i = 0; i < data.Count; i++)
                                {
                                    data[i].NoTrans = noTrans;
                                    data[i].TglTrans = today;
                                    InsertLineStore(data[i], username);

                                    //Update qty part
                                    UpdateQtyPart(data[i]);
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

        public string GetAutoNumber()
        {
            DataTable dt;
            string Result;

            List<SqlParameter> Params = new List<SqlParameter>();

            dt = SQLHelpers.GetDataTableByParam("[usp_CTrolly_GetAutoNumberLineStore]", CommandType.StoredProcedure, Params, db.connectionString);
            if (dt.Rows.Count > 0)
            {
                Result = dt.Rows[0]["NoTrans"].ToString();
            }
            else
            {
                Result = "";
            }
            return Result;

        }

        public int InsertLineStore(LineStoreScanPartModel model, string username)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoTrans", Value = model.NoTrans },
                    new SqlParameter() { ParameterName = "TglTrans", Value = model.TglTrans },
                    new SqlParameter() { ParameterName = "Shift", Value = model.Shift },
                    new SqlParameter() { ParameterName = "Line", Value = model.Line },
                    new SqlParameter() { ParameterName = "InvtId", Value = model.InvtId },
                    new SqlParameter() { ParameterName = "PartNo", Value = model.PartNo },
                    new SqlParameter() { ParameterName = "JobNo", Value = model.JobNo ?? "" },
                    new SqlParameter() { ParameterName = "PartName", Value = model.PartName },
                    new SqlParameter() { ParameterName = "Qty", Value = model.Qty },
                    new SqlParameter() { ParameterName = "Username", Value =  username }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_InsertScanPart", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateQtyPart(LineStoreScanPartModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "InvtId", Value = model.InvtId },
                    new SqlParameter() { ParameterName = "Qty", Value = model.Qty }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_UpdateQtyAfterScan", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SimpanScanPartPunch(List<LineStorePunchModel> data, string username)
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
                                var noTrans = GetAutoNumberPunch();

                                DateTime today = DateTime.Now;

                                //Insert line store;
                                for (int i = 0; i < data.Count; i++)
                                {
                                    data[i].NoTrans = noTrans;
                                    data[i].TglTrans = today;
                                    InsertPunch(data[i], username);

                                    //Update qty part
                                    UpdateQtyPartAfterPunch(data[i]);
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

        public string GetAutoNumberPunch()
        {
            DataTable dt;
            string Result;

            List<SqlParameter> Params = new List<SqlParameter>();

            dt = SQLHelpers.GetDataTableByParam("usp_LS_GetAutoNumberPunch", CommandType.StoredProcedure, Params, db.connectionString);
            if (dt.Rows.Count > 0)
            {
                Result = dt.Rows[0]["NoTrans"].ToString();
            }
            else
            {
                Result = "";
            }
            return Result;

        }

        public int InsertPunch(LineStorePunchModel model, string username)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoTrans", Value = model.NoTrans },
                    new SqlParameter() { ParameterName = "TglTrans", Value = model.TglTrans },
                    new SqlParameter() { ParameterName = "Shift", Value = model.Shift },
                    new SqlParameter() { ParameterName = "InvtId", Value = model.InvtId },
                    new SqlParameter() { ParameterName = "InvtIdPunch", Value = model.InvtIdPunch ?? ""},
                    new SqlParameter() { ParameterName = "PartNo", Value = model.PartNo },
                    new SqlParameter() { ParameterName = "JobNo", Value = model.JobNo ?? "" },
                    new SqlParameter() { ParameterName = "PartName", Value = model.PartName },
                    new SqlParameter() { ParameterName = "Qty", Value = model.Qty },
                    new SqlParameter() { ParameterName = "Username", Value =  username }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_LS_InsertScanPartPunch", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateQtyPartAfterPunch(LineStorePunchModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "InvtId", Value = model.InvtId },
                     new SqlParameter() { ParameterName = "InvtIdPunch", Value = model.InvtIdPunch },
                    new SqlParameter() { ParameterName = "Qty", Value = model.Qty }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_LS_UpdateQtyAfterPunch", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static readonly Func<IDataReader, LineStorePartModel> MakeLineStore = reader =>
          new LineStorePartModel
          {
              InvtId = reader["InvtId"].AsString(),
              PartNo = reader["PartNo"].AsString(),
              JobNo = reader["JobNo"].AsString(),
              PartName = reader["PartName"].AsString(),
              Qty = reader["Qty"].AsInt(),
              QtyMin = reader["QtyMin"].AsInt(),
              QtyMax = reader["QtyMax"].AsInt(),
              Delivery = reader["Delivery"].AsInt(),
              Ratio = reader["Ratio"].AsDouble(),
              Status = reader["Status"].AsString(),
          };
    }
}
