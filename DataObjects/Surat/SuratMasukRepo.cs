using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Surat;
using BusinessObjects;
using DataObjects.Helpers;
using System.Data.SqlClient;

namespace DataObjects.Surat
{
    public class SuratMasukRepo : ISuratMasukRepo
    {
        static Db db = new Db();
        
        public int Add(SuratMasukModel model, string dept)
        {
            try
            {
                int Result = -1;

                //string noUrut = GetNoUrutAuto(dept, model.Flag);

                if (model.TglAmbil == default(DateTime) || model.TglAmbil < new DateTime(1900, 1, 1, 0, 0, 0)) {
                    model.TglAmbil = new DateTime(1900, 1, 1, 0, 0, 0);
                }

                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "NoUrut", Value = model.NoUrut },
                    new SqlParameter() { ParameterName = "Flag", Value = model.Flag },
                    new SqlParameter() { ParameterName = "Tanggal", Value = model.Tanggal },
                    new SqlParameter() { ParameterName = "NoSurat", Value = model.NoSurat ?? "" },
                    new SqlParameter() { ParameterName = "Dari", Value = model.Dari ?? "" },
                    new SqlParameter() { ParameterName = "Pengirim", Value = model.Pengirim ?? "" },
                    new SqlParameter() { ParameterName = "Tujuan", Value = model.Tujuan ?? "" },
                    new SqlParameter() { ParameterName = "Dept", Value = model.Dept ?? "" },
                    new SqlParameter() { ParameterName = "Security", Value = model.Security ?? "" },
                    new SqlParameter() { ParameterName = "TglAmbil", Value = model.TglAmbil },
                    new SqlParameter() { ParameterName = "Penerima", Value = model.Penerima ?? "" },
                    new SqlParameter() { ParameterName = "DeptPenerima", Value = model.DeptTerima ?? "" },
                    new SqlParameter() { ParameterName = "IdKategori", Value = model.IdKategori ?? "-"  },
                    new SqlParameter() { ParameterName = "Kategori", Value = model.Kategori ?? "-" },
                    new SqlParameter() { ParameterName = "Jenis", Value = model.Jenis ?? "" },
                    new SqlParameter() { ParameterName = "Media", Value = model.Media },
                    new SqlParameter() { ParameterName = "Perihal", Value = model.Perihal ?? "" },
                    new SqlParameter() { ParameterName = "Akses", Value = model.Akses },
                    new SqlParameter() { ParameterName = "IsRahasia", Value = model.IsRahasia },
                    new SqlParameter() { ParameterName = "Rahasia", Value = model.Rahasia },
                    new SqlParameter() { ParameterName = "Ket", Value = model.Ket ?? "" },
                    new SqlParameter() { ParameterName = "FileName", Value = model.FileName ?? "" },
                    new SqlParameter() { ParameterName = "Status", Value = model.Status ?? ""  },
                    new SqlParameter() { ParameterName = "Username", Value = model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("ELetter_InsertSuratMasuk", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNoUrutAuto(string dept, int flag)
        {
            DataTable dt;
            string Result;

            List<SqlParameter> Params = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "Dept", Value = dept },
                new SqlParameter() { ParameterName = "Flag", Value = flag },
            };

            dt = SQLHelpers.GetDataTableByParam("[ELetter_AutoNoUrutSuratMasuk]", CommandType.StoredProcedure, Params, db.connectionString);
            if (dt.Rows.Count > 0)
            {
                Result = dt.Rows[0]["NoUrut"].ToString();
            }
            else
            {
                Result = "";
            }
            return Result;

        }

        public int Delete(int Id)
        {
            try
            {
                int hasil = -1;

                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "Id", Value = Id },
                };
                hasil = SQLHelpers.ExecQueryWithValue("ELetter_DeleteSuratMasuk", CommandType.StoredProcedure, Params, db.connectionString);

                return hasil;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Edit(SuratMasukModel model)
        {
            try
            {
                int Result = -1;

                if (model.TglAmbil == default(DateTime))
                {
                    model.TglAmbil = new DateTime(1900, 1, 1, 0, 0, 0);
                }

                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "Id", Value = model.Id },
                    new SqlParameter() { ParameterName = "Tanggal", Value = model.Tanggal },
                    new SqlParameter() { ParameterName = "NoSurat", Value = model.NoSurat ?? ""  },
                    new SqlParameter() { ParameterName = "Dari", Value = model.Dari ?? "" },
                    new SqlParameter() { ParameterName = "Pengirim", Value = model.Pengirim ?? "" },
                    new SqlParameter() { ParameterName = "Tujuan", Value = model.Tujuan ?? "" },
                    new SqlParameter() { ParameterName = "Dept", Value = model.Dept ?? "" },
                    new SqlParameter() { ParameterName = "Security", Value = model.Security ?? "" },
                    new SqlParameter() { ParameterName = "TglAmbil", Value = model.TglAmbil },
                    new SqlParameter() { ParameterName = "Penerima", Value = model.Penerima ?? "" },
                    new SqlParameter() { ParameterName = "DeptPenerima", Value = model.DeptTerima ?? "" },
                    new SqlParameter() { ParameterName = "IdKategori", Value = model.IdKategori ?? "-" },
                    new SqlParameter() { ParameterName = "Kategori", Value = model.Kategori ?? "-" },
                    new SqlParameter() { ParameterName = "Jenis", Value = model.Jenis },
                    new SqlParameter() { ParameterName = "Media", Value = model.Media },
                    new SqlParameter() { ParameterName = "Perihal", Value = model.Perihal ?? "" },
                    new SqlParameter() { ParameterName = "IsRahasia", Value = model.IsRahasia },
                    new SqlParameter() { ParameterName = "Rahasia", Value = model.Rahasia },
                    new SqlParameter() { ParameterName = "Akses", Value = model.Akses },
                    new SqlParameter() { ParameterName = "Ket", Value = model.Ket ?? "" },
                    new SqlParameter() { ParameterName = "FileName", Value = model.FileName ?? ""  },
                    new SqlParameter() { ParameterName = "Status", Value = model.Status ?? ""  },
                    new SqlParameter() { ParameterName = "Username", Value = model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("ELetter_UpdateSuratMasuk", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SuratMasukModel> Get(string dept, int flag, string status)
        {
            try
            {
                object[] Params = { "@Dept", dept, "@Flag", flag, "@Status", status };

                return db.Read("ELetter_GetSuratMasuk", CommandType.StoredProcedure, Make, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SuratMasukModel> GetList()
        {
            try
            {
                return db.Read("ListSuratMasuk", CommandType.StoredProcedure, Make).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SuratMasukModel GetRow(int Id)
        {
            try
            {

                object[] pParams = { "@Id", Id };
                return db.Read("ELetter_GetSuratMasukId", CommandType.StoredProcedure, Make, pParams).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static readonly Func<IDataReader, SuratMasukModel> Make = reader =>
         new SuratMasukModel
         {
             Id = reader["Id"].AsInt(),
             NoUrut= reader["NoUrut"].AsString(),
             Flag = reader["Flag"].AsInt(),
             Tanggal = reader["Tanggal"].AsDateTime(),
             NoSurat = reader["NoSurat"].AsString(),
             Dari = reader["Dari"].AsString(),
             Pengirim = reader["Pengirim"].AsString(),
             Tujuan = reader["Tujuan"].AsString(),
             Dept = reader["Dept"].AsString(),
             Security = reader["Security"].AsString(),
             TglAmbil = reader["TglAmbil"].AsDateTime(),
             Penerima = reader["Penerima"].AsString(),
             DeptTerima = reader["DeptPenerima"].AsString(),
             IdKategori = reader["IdKategori"].AsString(),
             Kategori = reader["Kategori"].AsString(),
             Jenis = reader["Jenis"].AsString(),
             Media = reader["Media"].AsString(),
             Perihal = reader["Perihal"].AsString(),
             Akses = reader["Akses"].AsString(),
             Rahasia = reader["Rahasia"].AsString(),
             IsRahasia = reader["IsRahasia"].AsBool(),
             Ket = reader["Ket"].AsString(),
             FileName = reader["FileName"].AsString(),
             Status = reader["Status"].AsString(),
         };
    }
}

