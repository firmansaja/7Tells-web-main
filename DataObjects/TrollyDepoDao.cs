using BusinessObjects;
using DataObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataObjects
{
    public class TrollyDepoDao : ITrollyDepoDao
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static Db db = new Db();

        public int InsertTrollyInDepo()
        {
            throw new NotImplementedException();
        }

        public List<TrollyDepoAllModel> CheckNoMobil(string noMobil)
        {
            try
            {
                object[] Params = { "@NoMobil", noMobil };
                return db.Read("usp_CTrolly_CheckNoMobil", CommandType.StoredProcedure, MakeTrolly, Params).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<TrollyDepoAllModel> CheckNoTransTSC(string noTrans)
        {
            try
            {
                object[] Params = { "@NoTrans", noTrans };
                return db.Read("usp_CTrolly_CheckNoTransTSC", CommandType.StoredProcedure, MakeTrolly, Params).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CheckKodeQR(string kodeQR)
        {      
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "KodeQR", Value = kodeQR });
                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("usp_CTrolly_CheckKodeQR", CommandType.StoredProcedure, Params, db.connectionString);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SimpanTrollyInOutDepo(TrollyDepoModel model, string deptID)
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
                                var noTrans = GetAutoNumber(model.InOut, deptID);
                                model.NoTrans = noTrans;

                                InsertDepoHeader(model);

                                UpdateTSCHeader(model);

                                //DeleteDetail(NoUpload);
                                for (int i = 0; i < model.Details.Count; i++)
                                {
                                    model.Details[i].NoTrans = noTrans;
                                    InsertDepoDetail(model.Details[i]);

                                    //Update last master trolley
                                    UpdateTrolly(model, model.Details[i].KodeQR);

                                    //Update flag trolly kembali ke Depo
                                    if (model.InOut == "IN")
                                    {
                                        UpdateTrollyFlag(model.TglTrans, model.Details[i].KodeQR);
                                    }
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

            dt = SQLHelpers.GetDataTableByParam("[usp_CTrolly_GetAutoNumberTrollyDepo]", CommandType.StoredProcedure, Params, db.connectionString);
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

        public int InsertDepoHeader(TrollyDepoModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoTrans", Value = model.NoTrans },
                    new SqlParameter() { ParameterName = "NoTransTSC", Value = model.NoTransTSC ?? ""},
                    new SqlParameter() { ParameterName = "InOut", Value = model.InOut },
                    new SqlParameter() { ParameterName = "TglTrans", Value = DateTime.Now },
                    new SqlParameter() { ParameterName = "CustId", Value = model.CustId },
                    new SqlParameter() { ParameterName = "NoMobil", Value = model.NoMobil },
                    new SqlParameter() { ParameterName = "NamaSupir", Value =  model.NamaSupir },
                    new SqlParameter() { ParameterName = "Username", Value =  model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_InsertTrollyDepoHeader", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateTSCHeader(TrollyDepoModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoTrans", Value = model.NoTrans },
                    new SqlParameter() { ParameterName = "NoTransTSC", Value = model.NoTransTSC ?? ""},
                    new SqlParameter() { ParameterName = "Username", Value =  model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_UpdateTrollyTSCHeader", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertDepoDetail(TrollyDepoDetailModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoTrans", Value = model.NoTrans },
                    new SqlParameter() { ParameterName = "KodeQR", Value = model.KodeQR },
                    new SqlParameter() { ParameterName = "Project", Value = model.Project },
                    new SqlParameter() { ParameterName = "TypeTrolly", Value = model.TypeTrolly },
                    new SqlParameter() { ParameterName = "InvtId", Value = model.InvtId ?? ""},
                    new SqlParameter() { ParameterName = "PartNo", Value = model.PartNo ?? ""},
                    new SqlParameter() { ParameterName = "PartName", Value = model.PartName ?? ""},
                    new SqlParameter() { ParameterName = "Qty", Value = model.Qty},
                    new SqlParameter() { ParameterName = "Satuan", Value = model.Satuan ?? ""}
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_InsertTrollyDepoDetail", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteDepoDetail(string NoTrans)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "NoTrans", Value = NoTrans });
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_DeleteTrollyDepoDetail", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdateTrolly(TrollyDepoModel model, string kodeQR)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "InOut", Value = model.InOut });
                Params.Add(new SqlParameter() { ParameterName = "TglTrans", Value = model.TglTrans });
                Params.Add(new SqlParameter() { ParameterName = "CustId", Value = model.CustId });
                Params.Add(new SqlParameter() { ParameterName = "KodeQR", Value = kodeQR });
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_UpdateTrollyInOut", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdateTrollyFlag(DateTime tglTrans, string kodeQR)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "TglTrans", Value = tglTrans });
                Params.Add(new SqlParameter() { ParameterName = "KodeQR", Value = kodeQR });
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_UpdateTrollyFlagDepo", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<TrollyDepoModel> GetLapTrollyOutDepo(FilterTrollyOutDepoModel filter)
        {
            try
            {
                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                object[] Params = { "@DTDari", stDate, "@DTSampai", enDate };
                return db.Read("usp_CTrolly_GetLapTrollyOutDepo", CommandType.StoredProcedure, MakeTrollyOut, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dependencyTrolly_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                FifoHub.GetTrollyDepo();
            }
        }

        public List<TrollyInOutDepoModel> GetTrollyInOutDepo(FilterTrollyOutDepoModel filter)
        {
            try
            {
                SqlDependency.Start(_connString);
                var Trolly = new List<TrollyInOutDepoModel>();
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"SELECT Id ,
                                                                NoTrans ,
                                                                NoTransTSC ,
                                                                InOut ,
                                                                TglTrans ,
                                                                CustId ,
                                                                NoMobil ,
                                                                NamaSupir ,
                                                                CreatedBy ,
                                                                CreatedDate ,
                                                                UpdatedBy ,
                                                                UpdatedDate
                                                        FROM    dbo.CTrolly_T_DepoTrollyHeader", connection))
                    {
                        command.Notification = null;
                        var dependency = new SqlDependency(command);

                        dependency.OnChange += new OnChangeEventHandler(dependencyTrolly_OnChange);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();
                    }
                }

                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                object[] Params = { "@DTDari", stDate, "@DTSampai", enDate };

                Trolly = db.Read("[usp_CTrolly_RptGetListTrollyInOutDepo]", CommandType.StoredProcedure, MakeTrollyInOut, Params).ToList();

                return Trolly;

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

        public DataTable CheckPart(string invtId)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "InvtId", Value = invtId });
                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("usp_CTrolly_CheckPartDepoToADM", CommandType.StoredProcedure, Params, db.connectionString);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
               
        public string GetAutoNumberDepoToADM()
        {
            DataTable dt;
            string Result;

            List<SqlParameter> Params = new List<SqlParameter>();

            dt = SQLHelpers.GetDataTableByParam("[usp_CTrolly_GetAutoNumberDepoToADM]", CommandType.StoredProcedure, Params, db.connectionString);
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

        public int SimpanPartDepoToADM(List<TrollyInOutDepoToADMModel> data, string username)
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
                                var noTrans = GetAutoNumberDepoToADM();

                                DateTime today = DateTime.Now;

                                //Insert line store;
                                for (int i = 0; i < data.Count; i++)
                                {
                                    data[i].NoTrans = noTrans;
                                    data[i].TglTrans = today;
                                    InsertPartDepoToADM(data[i], username);

                                    //Update qty part
                                    UpdateQtyPartDepo(data[i]);
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

        public int InsertPartDepoToADM(TrollyInOutDepoToADMModel model, string username)
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
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_InsertPartDepoToADM", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateQtyPartDepo(TrollyInOutDepoToADMModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "InvtId", Value = model.InvtId },
                    new SqlParameter() { ParameterName = "Qty", Value = model.Qty }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_UpdateQtyDepo", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void dependencyTrollyStok_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                FifoHub.GetTrollyStokDepo();
            }
        }

        public List<TrollyStokDepoModel> GetTrollyStokDepo(FilterTrollyOutDepoModel filter)
        {
            try
             {
                SqlDependency.Start(_connString);
                var Trolly = new List<TrollyStokDepoModel>();
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"SELECT  Id ,
                                                                NoTrans ,
                                                                TglTrans ,
                                                                TglShif ,
                                                                Shift ,
                                                                Line ,
                                                                InvtId ,
                                                                PartNo ,
                                                                JobNo ,
                                                                PartName ,
                                                                Qty
                                                        FROM    dbo.CTrolly_T_DepoToADM", connection))
                    {
                        command.Notification = null;
                        var dependency = new SqlDependency(command);

                        dependency.OnChange += new OnChangeEventHandler(dependencyTrollyStok_OnChange);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();
                    }
                }

                string stDate = filter.DTDari.HasValue ? filter.DTDari.Value.ToString("yyyyMMdd") : string.Empty;
                string enDate = filter.DTSampai.HasValue ? filter.DTSampai.Value.ToString("yyyyMMdd") : string.Empty;
                //object[] Params = { "@DTDari", stDate, "@DTSampai", enDate };
                object[] Params = { "@Tgl", stDate };

                Trolly = db.Read("[usp_CTrolly_RptGetListStockDepo]", CommandType.StoredProcedure, MakeTrollyStok, Params).ToList();

                return Trolly;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        static readonly Func<IDataReader, TrollyDepoAllModel> MakeTrolly = reader =>
           new TrollyDepoAllModel
           {
               Id = reader["Id"].AsInt(),
               NoTrans = reader["NoTrans"].AsString(),
               InOut = reader["InOut"].AsString(),
               TglTrans = reader["TglTrans"].AsDateTime(),
               CustId = reader["CustId"].AsString(),
               NoMobil = reader["NoMobil"].AsString(),
               NamaSupir = reader["NamaSupir"].AsString(),
               KodeQR = reader["KodeQR"].AsString(),
               Project = reader["Project"].AsString(),
               TypeTrolly = reader["TypeTrolly"].AsString(),
               InvtId = reader["InvtId"].AsString(),
               PartNo = reader["PartNo"].AsString(),
               PartName = reader["PartName"].AsString(),
               Qty = reader["Qty"].AsInt(),
               Satuan = reader["Satuan"].AsString()
           };

        static readonly Func<IDataReader, TrollyDepoModel> MakeTrollyOut = reader =>
          new TrollyDepoModel
          {
              NoTrans = reader["NoTrans"].AsString(),
              TglTrans = reader["TglTrans"].AsDateTime(),
              NoMobil = reader["NoMobil"].AsString(),
              NamaSupir = reader["NamaSupir"].AsString(),
          };

        static readonly Func<IDataReader, TrollyInOutDepoModel> MakeTrollyInOut = reader =>
          new TrollyInOutDepoModel
          {
              NoTrans = reader["NoTrans"].AsString(),
              NoMobil = reader["NoMobil"].AsString(),
              KodeQR = reader["KodeQR"].AsString(),
              TglTransDepo = reader["TglTransDepo"].AsDateTime(),
              StatusDepo = reader["StatusDepo"].AsString(),
              TglTransTSC = reader["TglTransTSC"].AsDateTime(),
              StatusTSC = reader["StatusTSC"].AsString()
          };

        static readonly Func<IDataReader, TrollyStokDepoModel> MakeTrollyStok = reader =>
          new TrollyStokDepoModel
          {
              InvtId = reader["InvtId"].AsString(),
              PartName = reader["PartName"].AsString(),
              Type = reader["Type"].AsString(),
              JobNo = reader["JobNo"].AsString(),
              StdStok = reader["StdStockDepo"].AsInt(),
              StokAwal = reader["StockAwal"].AsInt(),
              OutTsc1 = reader["OutTSC1"].AsInt(),
              OutTsc2 = reader["OutTSC2"].AsInt(),
              InDepo1 = reader["InDepo1"].AsInt(),
              InDepo2 = reader["InDepo2"].AsInt(),
              OutAdm1 = reader["OutADM1"].AsInt(),
              OutAdm2 = reader["OutADM2"].AsInt(),
              OutDepo = reader["OutDepo"].AsInt(),
              StokAkhir = reader["StockAkhir"].AsInt()
          };

    }
}
