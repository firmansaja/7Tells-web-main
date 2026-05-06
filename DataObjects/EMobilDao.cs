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
    public class EMobilDao : IEMobilDao
    {
        static Db db = new Db();

        public MobilAksesUser GetAkses(string UserName, string MenuCode)
        {
            try
            {
                string Query = @"SELECT  UserName ,
                                        DeptID AS Dept ,
                                        CASE WHEN LEFT(DeptID, 1) = 1 THEN 'TNG'
                                             WHEN LEFT(DeptID, 1) = 3 THEN 'CKR'
                                        END AS Site ,
                                        LevelApprove AS [Level]
                                FROM    dbo.M_Approve
                                WHERE   MenuCode = @MenuCode
                                        AND UserName = @UserName";

                object[] Params = { "@UserName", UserName, "@MenuCode", MenuCode };
                return db.Read(Query, CommandType.Text, MakeAkses, Params).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EMobilReq> GetMobilReq(string UserName, string Approved)
        {
            try
            {
                object[] Params = { "@FrmName", "FrmReqKendaraan", "@UserName", UserName, "@Approved", Approved };
                return db.Read("Mbl_GetReqWithApv", CommandType.StoredProcedure, MakeMobilReq, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EMobilReq GetReqHeaderByID(string NoTrans)
        {
            try
            {
                string Query = @"SELECT  NoTrans ,
                                        Tanggal ,
                                        Dept ,
                                        '' AS [Level 1] ,
                                        '' AS [Level 2] ,
                                        '' AS [Level 3] ,
                                        Approved ,
                                        Comment ,
                                        CurrLvlApv AS CurrentLevel
                                FROM    dbo.T_MblReqHeader
                                WHERE   NoTrans = '" + NoTrans + "'";
                return db.Read(Query, CommandType.Text, MakeMobilReq).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EMobilReqDetail> GetReqDetailByID(string NoTrans)
        {
            try
            {
                string Query = @"SELECT  header.NoTrans ,
                                        header.Dept ,
                                        tujuan.SeqTujuan ,
                                        penumpang.Seq ,
                                        tujuan.Tujuan ,
                                        tujuan.Keperluan ,
                                        penumpang.Nik ,
                                        penumpang.Nama ,
                                        tujuan.BerangkatDari ,
                                        tujuan.BerangkatTgl ,
                                        tujuan.KembaliDi ,
                                        tujuan.KembaliTgl ,
                                        CASE WHEN penumpang.TugasLuar = 1 THEN 'checked'
                                             ELSE ''
                                        END AS TugasLuar ,
                                        CASE WHEN penumpang.PakaiMobil = 1 THEN 'checked'
                                             ELSE ''
                                        END AS PakaiMobil ,
                                        penumpang.Catatan ,
                                        penumpang.StatusJalan ,
                                        penumpang.NoSuratJalan
                                FROM    dbo.T_MblReqHeader AS header
                                        INNER JOIN dbo.T_MblReqTujuan AS tujuan ON tujuan.NoTrans = header.NoTrans
                                        INNER JOIN dbo.T_MblReqPenumpang AS penumpang ON penumpang.NoTrans = header.NoTrans
                                                                                         AND penumpang.SeqTujuan = tujuan.SeqTujuan                                
                                WHERE   header.NoTrans = @NoTrans
                                ORDER BY header.NoTrans ,
                                        tujuan.SeqTujuan ,
                                        penumpang.Seq";

                object[] Params = { "@NoTrans", NoTrans };
                return db.Read(Query, CommandType.Text, MakeMobilReqDetail, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ApproveReq(EMobilReq MobilReq, ApproveHistory ApvHistory)
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
                                string Query = "";
                                Query = @"UPDATE dbo.T_MblReqHeader
                                         SET    Approved = @Approved ,
                                                Comment = @Comment ,
                                                CurrLvlApv = @CurrLvlApv ,
                                                UserUbah = @UserUbah ,
                                                TglUbah = GETDATE()
                                         WHERE NoTrans = @NoTrans";

                                List<SqlParameter> Params = new List<SqlParameter>();
                                Params.Add(new SqlParameter() { ParameterName = "Approved", Value = MobilReq.Approved });
                                Params.Add(new SqlParameter() { ParameterName = "Comment", Value = MobilReq.Comment == null ? "" : MobilReq.Comment });
                                Params.Add(new SqlParameter() { ParameterName = "CurrLvlApv", Value = MobilReq.CurrentLevel });
                                Params.Add(new SqlParameter() { ParameterName = "UserUbah", Value = ApvHistory.UserName });
                                Params.Add(new SqlParameter() { ParameterName = "NoTrans", Value = MobilReq.NoTrans });
                                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                                GlobalDao globalSrv = new GlobalDao();
                                globalSrv.InsertApvHistory(ApvHistory);

                                if (MobilReq.Approved == "REJECTED")
                                {
                                    globalSrv.UpdateApvHistoryFlagAll(ApvHistory);
                                }

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

        public List<EMobilCek> GetMobilCek(int Level, string Status)
        {
            try
            {                
                object[] Params = { "@Level", Level, "@Status", Status };
                return db.Read("Mbl_GetCekKendaraan", CommandType.StoredProcedure, MakeMobilCek, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EMobilCek GetCekHeaderByID(string NoSuratJalan)
        {
            try
            {
                string Query = @"SELECT  NoSuratJalan ,
                                        Tanggal ,
                                        StatusMobil ,
                                        NoMobil ,
                                        CASE WHEN PakaiSupir = 1 THEN 'checked'
                                             ELSE ''
                                        END AS PakaiSupir ,
                                        NamaSupir ,
                                        Catatan ,
                                        BerangkatTgl ,
                                        KMAwal ,
                                        SecurityBerangkat ,
                                        KembaliTgl ,
                                        KMAkhir ,
                                        SecurityKembali ,
                                        Status ,
                                        Approved ,
                                        Comment ,
                                        CurrLvlApv ,
                                        UserApv ,
                                        BiayaBensin ,
                                        BiayaParkir ,
                                        BiayaLainLain
                                FROM    dbo.T_MblJalanHeader
                                WHERE   NoSuratJalan = '" + NoSuratJalan + "'";

                return db.Read(Query, CommandType.Text, MakeMobilCek).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EMobilCekDetail> GetCekDetailByID(string NoSuratJalan)
        {
            try
            {
                string Query = @"SELECT  NoSuratJalan ,
                                        NoTrans ,
                                        SeqTujuan ,
                                        Seq ,
                                        Dept ,
                                        NIK ,
                                        Nama ,
                                        Tujuan ,
                                        Keperluan ,
                                        BerangkatDari ,
                                        BerangkatTgl ,
                                        KembaliDi ,
                                        KembaliTgl ,
                                        Catatan
                                FROM    dbo.T_MblJalanDetail
                                WHERE   NoSuratJalan = '" + NoSuratJalan + "'";

                return db.Read(Query, CommandType.Text, MakeMobilCekDetail).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ApproveCek(EMobilCek MobilCek, ApproveHistory ApvHistory)
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
                                string Query = "";
                                Query = @"UPDATE  dbo.T_MblJalanHeader
                                         SET    Status = @Status ,
                                                Approved = @Approved ,
                                                Comment = @Comment ,
                                                CurrLvlApv = @CurrLvlApv ,
                                                UserApv = @UserApv
                                         WHERE NoSuratJalan = @NoSuratJalan";

                                List<SqlParameter> Params = new List<SqlParameter>();
                                Params.Add(new SqlParameter() { ParameterName = "Status", Value = MobilCek.Status });
                                Params.Add(new SqlParameter() { ParameterName = "Approved", Value = MobilCek.Approved });
                                Params.Add(new SqlParameter() { ParameterName = "Comment", Value = MobilCek.Comment == null ? "" : MobilCek.Comment });
                                Params.Add(new SqlParameter() { ParameterName = "CurrLvlApv", Value = MobilCek.CurrLvlApv == null ? "" : MobilCek.CurrLvlApv.ToString() });
                                Params.Add(new SqlParameter() { ParameterName = "UserApv", Value = ApvHistory.UserName });
                                Params.Add(new SqlParameter() { ParameterName = "NoSuratJalan", Value = MobilCek.NoSuratJalan });
                                Result = SQLHelpers.ExecQueryWithValue(Query, CommandType.Text, Params, db.connectionString);

                                //GlobalDao globalSrv = new GlobalDao();
                                //globalSrv.InsertApvHistory(ApvHistory);

                                //if (MobilCek.Approved == "REJECTED")
                                //{
                                //    globalSrv.UpdateApvHistoryFlagAll(ApvHistory);
                                //}

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

        static readonly Func<IDataReader, EMobilReq> MakeMobilReq = reader =>
           new EMobilReq
           {
               NoTrans = reader["NoTrans"].AsString(),
               Tanggal = reader["Tanggal"].AsDateTime(),
               Dept = reader["Dept"].AsString(),
               Level1 = reader["Level 1"].AsString(),
               Level2 = reader["Level 2"].AsString(),
               Level3 = reader["Level 3"].AsString(),
               CurrentLevel = reader["CurrentLevel"].AsString(),
               Approved = reader["Approved"].AsString(),
               Comment = reader["Comment"].AsString()
           };

        static readonly Func<IDataReader, EMobilReqDetail> MakeMobilReqDetail = reader =>
           new EMobilReqDetail
           {
               Tujuan = reader["Tujuan"].AsString(),
               Keperluan = reader["Keperluan"].AsString(),
               Nik = reader["Nik"].AsString(),
               Nama = reader["Nama"].AsString(),
               BerangkatDari = reader["BerangkatDari"].AsString(),
               BerangkatTgl = reader["BerangkatTgl"].AsDateTime(),
               KembaliDi = reader["KembaliDi"].AsString(),
               KembaliTgl = reader["KembaliTgl"].AsDateTime(),
               TugasLuar = reader["TugasLuar"].AsString(),
               PakaiMobil = reader["PakaiMobil"].AsString(),
               Catatan = reader["Catatan"].AsString(),
               StatusJalan = reader["StatusJalan"].AsString(),
               NoSuratJalan = reader["NoSuratJalan"].AsString()
           };

        static readonly Func<IDataReader, MobilAksesUser> MakeAkses = reader =>
           new MobilAksesUser
           {
               UserName = reader["UserName"].AsString(),
               Dept = reader["Dept"].AsString(),
               Site = reader["Site"].AsString(),
               Level = reader["Level"].AsInt(),
           };

        static readonly Func<IDataReader, EMobilCek> MakeMobilCek = reader =>
           new EMobilCek
           {
               NoSuratJalan = reader["NoSuratJalan"].AsString(),
               Tanggal = reader["Tanggal"].AsDateTime(),
               StatusMobil = reader["StatusMobil"].AsString(),
               NoMobil = reader["NoMobil"].AsString(),
               PakaiSupir = reader["PakaiSupir"].AsString(),
               NamaSupir = reader["NamaSupir"].AsString(),
               Catatan = reader["Catatan"].AsString(),
               BerangkatTgl = reader["BerangkatTgl"].AsDateTime(),
               KMAwal = reader["KMAwal"].AsInt(),
               KembaliTgl = reader["KembaliTgl"].AsDateTime(),
               KMAkhir = reader["KMAkhir"].AsInt(),
               Status = reader["Status"].AsString(),
               Approved = reader["Approved"].AsString(),
               Comment = reader["Comment"].AsString(),
               CurrLvlApv = reader["CurrLvlApv"].AsInt()
           };

        static readonly Func<IDataReader, EMobilCekDetail> MakeMobilCekDetail = reader =>
           new EMobilCekDetail
           {
               NoSuratJalan = reader["NoSuratJalan"].AsString(),
               NoTrans = reader["NoTrans"].AsString(),
               Dept = reader["Dept"].AsString(),
               NIK = reader["Nik"].AsString(),
               Nama = reader["Nama"].AsString(),
               Tujuan = reader["Tujuan"].AsString(),
               Keperluan = reader["Keperluan"].AsString(),
               BerangkatDari = reader["BerangkatDari"].AsString(),
               BerangkatTgl = reader["BerangkatTgl"].AsDateTime(),
               KembaliDi = reader["KembaliDi"].AsString(),
               KembaliTgl = reader["KembaliTgl"].AsDateTime(),
               Catatan = reader["Catatan"].AsString(),
               
           };
    }
}