using BusinessObjects.PPIC;
using DataObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;

namespace DataObjects
{
    public class BuktiMuatDao : IBuktiMuatDao
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static Db db = new Db();

        public List<BuktiMuatModel> CheckBuktiMuat(string noSeq)
        {
            try
            {
                object[] Params = { "@NoSeq", noSeq };
                return db.Read("usp_Muat_CheckBuktiMuat", CommandType.StoredProcedure, MakeBuktiMuat, Params).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SimpanBuktiMuatOut(List<BuktiMuatModel> data, string username)
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
                                for (int i = 0; i < data.Count; i++)
                                {
                                    InsertBuktiMuatOut(data[i], username);
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

        public int InsertBuktiMuatOut(BuktiMuatModel model, string username)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoUpload", Value = model.NoUpload },
                    new SqlParameter() { ParameterName = "NoMobil", Value = model.NoMobil },
                    new SqlParameter() { ParameterName = "NoMobilAktual", Value = model.NoMobilAktual },
                    new SqlParameter() { ParameterName = "PartNo", Value = model.PartNo },
                    new SqlParameter() { ParameterName = "ItemNumber", Value = model.ItemNo },
                    new SqlParameter() { ParameterName = "ItemName", Value = model.ItemName },
                    new SqlParameter() { ParameterName = "Lokasi", Value = model.Lokasi },
                    new SqlParameter() { ParameterName = "UserCode", Value =  model.UserCode },
                    new SqlParameter() { ParameterName = "PF", Value = model.PF },
                    new SqlParameter() { ParameterName = "OrderNo", Value = model.OrderNo },
                    new SqlParameter() { ParameterName = "OrderQuantity", Value =  model.OrderQty },
                    new SqlParameter() { ParameterName = "Username", Value =  username }
                };
                Result = SQLHelpers.ExecQueryWithValue("usp_Muat_InsertBuktiMuatOut", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static readonly Func<IDataReader, BuktiMuatModel> MakeBuktiMuat = reader =>
          new BuktiMuatModel
          {
              NoUpload = reader["NoUpload"].AsString(),
              TglKirim = reader["TglKirim"].AsDateTime(),
              Tujuan = reader["Tujuan"].AsString(),
              NoMobil = reader["GroupTruk"].AsInt(),
              PartNo = reader["PartNo"].AsString(),
              ItemNo = reader["ItemNumber"].AsString(),
              ItemName = reader["ItemName"].AsString(),
              Lokasi = reader["Lokasi"].AsString(),
              UserCode = reader["UserCode"].AsString(),
              PF = reader["PF"].AsString(),
              OrderNo = reader["OrderNo"].AsString(),
              OrderQty = reader["OrderQuantity"].AsInt()

          };
    }
}
