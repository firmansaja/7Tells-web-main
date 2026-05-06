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
    public class TrollyDao : ITrollyDao
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static Db db = new Db();
        public int InsertTrollyOutTSC()
        {
            throw new NotImplementedException();
        }

        public DataTable CheckTrolly(string kodeQR)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "KodeQR", Value = kodeQR });
                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("usp_CTrolly_CheckTrollyById", CommandType.StoredProcedure, Params, db.connectionString);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CheckPart(string invtId, string project, string typeTrolly)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>();

                Params.Add(new SqlParameter() { ParameterName = "InvtId", Value = invtId });
                Params.Add(new SqlParameter() { ParameterName = "Project", Value = project });
                Params.Add(new SqlParameter() { ParameterName = "TypeTrolly", Value = typeTrolly });
                DataTable dt = new DataTable();

                dt = SQLHelpers.GetDataTableByParam("usp_CTrolly_CheckPart", CommandType.StoredProcedure, Params, db.connectionString);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<TrollyAllModel> CheckNoMobil(string noMobil)
        {
            try
            {
                object[] Params = { "@NoMobil", noMobil };
                return db.Read("usp_CTrolly_CheckNoMobilTSC", CommandType.StoredProcedure, MakeTrolly, Params).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<TrollyAllModel> CheckNoSuratJalan(string noTransDepo)
        {
            try
            {
                object[] Params = { "@NoTrans", noTransDepo };
                return db.Read("usp_CTrolly_CheckNoTransDepo", CommandType.StoredProcedure, MakeTrolly, Params).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SimpanTrollyInOutTSC(TrollyModel model, string deptID)
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

                                InsertTSCHeader(model);

                                UpdateDepoHeader(model);

                                //DeleteDetail(NoUpload);
                                for (int i = 0; i < model.Details.Count; i++)
                                {
                                    model.Details[i].NoTrans = noTrans;
                                    InsertTSCDetail(model.Details[i]);

                                    //Update last master trolley
                                    UpdateTrolly(model, model.Details[i].KodeQR);

                                    //Update flag trolly kembali ke TSC
                                    if (model.InOut == "IN")
                                    {
                                        UpdateTrollyFlag(model.TglTrans, model.Details[i].KodeQR);
                                    }

                                    //Update qty part keluar TSC
                                    if (model.InOut == "OUT")
                                    {
                                        UpdateTrollyQtyPart(model.Details[i].InvtId, model.Details[i].Qty);
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

            dt = SQLHelpers.GetDataTableByParam("[usp_CTrolly_GetAutoNumberTrollyTSC]", CommandType.StoredProcedure, Params, db.connectionString);
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

        public int InsertTSCHeader(TrollyModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoTrans", Value = model.NoTrans },
                    new SqlParameter() { ParameterName = "NoTransDepo", Value = model.NoTransDepo ?? "" },
                    new SqlParameter() { ParameterName = "InOut", Value = model.InOut },
                    new SqlParameter() { ParameterName = "TglTrans", Value = DateTime.Now },
                    new SqlParameter() { ParameterName = "CustId", Value = model.CustId },
                    new SqlParameter() { ParameterName = "NoMobil", Value = model.NoMobil },
                    new SqlParameter() { ParameterName = "NamaSupir", Value =  model.NamaSupir },
                    new SqlParameter() { ParameterName = "Username", Value =  model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_InsertTrollyTSCHeader", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateDepoHeader(TrollyModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoTrans", Value = model.NoTrans },
                    new SqlParameter() { ParameterName = "NoTransDepo", Value = model.NoTransDepo ?? ""},
                    new SqlParameter() { ParameterName = "Username", Value =  model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_UpdateTrollyDepoHeader", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertTSCDetail(TrollyDetailModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoTrans", Value = model.NoTrans },
                    new SqlParameter() { ParameterName = "KodeQR", Value = model.KodeQR },
                    new SqlParameter() { ParameterName = "Project", Value = model.Project ?? "" },
                    new SqlParameter() { ParameterName = "TypeTrolly", Value = model.TypeTrolly ?? "" },
                    new SqlParameter() { ParameterName = "InvtId", Value = model.InvtId ?? ""},
                    new SqlParameter() { ParameterName = "PartNo", Value = model.PartNo ?? ""},
                    new SqlParameter() { ParameterName = "PartName", Value = model.PartName ?? ""},
                    new SqlParameter() { ParameterName = "Qty", Value = model.Qty},
                    new SqlParameter() { ParameterName = "Satuan", Value = model.Satuan ?? ""}
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_InsertTrollyTSCDetail", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteTSCDetail(string NoTrans)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "NoTrans", Value = NoTrans });
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_DeleteTrollyTSCDetail", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdateTrolly(TrollyModel model, string kodeQR)
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
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_UpdateTrollyFlag", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdateTrollyQtyPart(string invtId, int qty)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>();
                Params.Add(new SqlParameter() { ParameterName = "InvtId", Value = invtId });
                Params.Add(new SqlParameter() { ParameterName = "Qty", Value = qty });
                Result = SQLHelpers.ExecQueryWithValue("usp_CTrolly_UpdateQtyPartOut", CommandType.StoredProcedure, Params, db.connectionString);

                return Result;
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
                FifoHub.GetTrolly();
            }
        }

        public List<TrollyInOutModel> GetTrollyInOut(FilterTrollyOutDepoModel filter)
        {
            try
            {
                SqlDependency.Start(_connString);
                var Trolly = new List<TrollyInOutModel>();
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"SELECT Id ,
                                                                NoTrans ,
                                                                NoTransDepo ,
                                                                InOut ,
                                                                TglTrans ,
                                                                CustId ,
                                                                NoMobil ,
                                                                NamaSupir ,
                                                                CreatedBy ,
                                                                CreatedDate ,
                                                                UpdatedBy ,
                                                                UpdatedDate
                                                        FROM    dbo.CTrolly_T_TSCTrollyHeader", connection))
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

                Trolly = db.Read("[usp_CTrolly_RptGetListTrollyInOut]", CommandType.StoredProcedure, MakeTrollyInOut, Params).ToList();

                return Trolly;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<TrollyInOutDetailModel> GetTrollyInOutDetail(string noTrans, string kodeQR)
        {
            try
            {
                object[] Params = { "@NoTrans", noTrans, "@KodeQR", kodeQR };
                return db.Read("usp_CTrolly_RptGetListTrollyInOutDetail", CommandType.StoredProcedure, MakeTrollyInOutDetail, Params).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        static readonly Func<IDataReader, TrollyAllModel> MakeTrolly = reader =>
          new TrollyAllModel
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
              TypeTrolly = reader["TypeTrolly"].AsString()
          };

        static readonly Func<IDataReader, TrollyInOutModel> MakeTrollyInOut = reader =>
          new TrollyInOutModel
          {
              NoTrans = reader["NoTrans"].AsString(),
              NoMobil = reader["NoMobil"].AsString(),
              KodeQR = reader["KodeQR"].AsString(),
              TglTransTSC = reader["TglTransTSC"].AsDateTime(),
              StatusTSC = reader["StatusTSC"].AsString(),
              QtyTrollyTSC = reader["QtyTrollyTSC"].AsInt(),
              QtyPartTSC = reader["QtyPartTSC"].AsInt(),
              Qty = reader["Qty"].AsInt(),
              TglTransDepo = reader["TglTransDepo"].AsDateTime(),
              StatusDepo = reader["StatusDepo"].AsString(),
              QtyTrollyDepo = reader["QtyTrollyDepo"].AsInt(),
              QtyPartDepo = reader["QtyPartDepo"].AsInt()
        };

        static readonly Func<IDataReader, TrollyInOutDetailModel> MakeTrollyInOutDetail = reader =>
          new TrollyInOutDetailModel
          {
              NoTrans = reader["NoTrans"].AsString(),
              NoMobil = reader["NoMobil"].AsString(),
              NamaSupir = reader["NamaSupir"].AsString(),
              KodeQR = reader["KodeQR"].AsString(),
              TglTransTSC = reader["TglTransTSC"].AsDateTime(),
              InvtIdTSC = reader["InvtIdTSC"].AsString(),
              PartNameTSC = reader["PartNameTSC"].AsString(),
              ColorTSC = reader["ColorTSC"].AsString(),
              QtyTrollyTSC = reader["QtyTSC"].AsInt(),
              QtyPartTSC = reader["QtyTSC"].AsInt(),
              StatusTSC = reader["StatusTSC"].AsString(),
              TglTransDepo = reader["TglTransDepo"].AsDateTime(),
              InvtIdDepo = reader["InvtIdDepo"].AsString(),
              PartNameDepo = reader["PartNameDepo"].AsString(),
              QtyTrollyDepo = reader["QtyDepo"].AsInt(),
              QtyPartDepo = reader["QtyDepo"].AsInt(),
              StatusDepo = reader["StatusDepo"].AsString(),
          };
    }
}
