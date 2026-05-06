using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Surat;
using DataObjects.Helpers;
using System.Data.SqlClient;


namespace DataObjects.Surat
{
    public class SuratKeluarRepo : ISuratKeluarRepo
    {
        static Db db = new Db();
        public int Add(SuratKeluarModel model)
        {
            try
            {
                int Result = -1;

                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "Tanggal", Value = model.Tanggal },
                    new SqlParameter() { ParameterName = "NoSurat", Value = model.NoSurat },
                    new SqlParameter() { ParameterName = "Pengirim", Value = model.Pengirim ?? ""  },
                    new SqlParameter() { ParameterName = "Dept", Value = model.Dept ?? "" },
                    new SqlParameter() { ParameterName = "Instansi", Value = model.Instansi ?? "" },
                    new SqlParameter() { ParameterName = "Tujuan", Value = model.Tujuan ?? "" },
                    new SqlParameter() { ParameterName = "IdKategori", Value = model.IdKategori },
                    new SqlParameter() { ParameterName = "Kategori", Value = model.Kategori },
                    new SqlParameter() { ParameterName = "Jenis", Value = model.Jenis ?? "" },
                    new SqlParameter() { ParameterName = "Media", Value = model.Media },
                    new SqlParameter() { ParameterName = "Perihal", Value = model.Perihal ?? "" },
                    new SqlParameter() { ParameterName = "Akses", Value = model.Akses },
                    new SqlParameter() { ParameterName = "AksesDept", Value = model.AksesDept },
                    new SqlParameter() { ParameterName = "IsRahasia", Value = model.IsRahasia },
                    new SqlParameter() { ParameterName = "Rahasia", Value = model.Rahasia ?? "" },
                    new SqlParameter() { ParameterName = "FileName", Value = model.FileName ?? "" },
                    new SqlParameter() { ParameterName = "Username", Value = model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("ELetter_InsertSuratKeluar", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNoSuratAuto(string dept)
        {
            string site = string.Concat("TSC", dept.Substring(0, 1));
            string _dept = dept.Substring(dept.Length - 3, 3);

            DataTable dt;
            string Result;

            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(new SqlParameter() { ParameterName = "Site", Value = site });
            Params.Add(new SqlParameter() { ParameterName = "Dept", Value = _dept });

            dt = SQLHelpers.GetDataTableByParam("[ELetter_AutoNoSuratSuratKeluar]", CommandType.StoredProcedure, Params, db.connectionString);
            if (dt.Rows.Count > 0)
            {
                Result = dt.Rows[0]["NoSurat"].ToString();
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
                hasil = SQLHelpers.ExecQueryWithValue("ELetter_DeleteSuratKeluar", CommandType.StoredProcedure, Params, db.connectionString);

                return hasil;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Edit(SuratKeluarModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "Id", Value = model.Id },
                    new SqlParameter() { ParameterName = "NoSurat", Value = model.NoSurat },
                    new SqlParameter() { ParameterName = "Tanggal", Value = model.Tanggal },
                    new SqlParameter() { ParameterName = "Pengirim", Value = model.Pengirim },
                    new SqlParameter() { ParameterName = "Dept", Value = model.Dept ?? "" },
                    new SqlParameter() { ParameterName = "Instansi", Value = model.Instansi ?? "" },
                    new SqlParameter() { ParameterName = "Tujuan", Value = model.Tujuan },
                    new SqlParameter() { ParameterName = "IdKategori", Value = model.IdKategori },
                    new SqlParameter() { ParameterName = "Kategori", Value = model.Kategori },
                    new SqlParameter() { ParameterName = "Jenis", Value = model.Jenis },
                    new SqlParameter() { ParameterName = "Media", Value = model.Media },
                    new SqlParameter() { ParameterName = "Perihal", Value = model.Perihal ?? ""  },
                    new SqlParameter() { ParameterName = "Akses", Value = model.Akses },
                    new SqlParameter() { ParameterName = "AksesDept", Value = model.AksesDept },
                    new SqlParameter() { ParameterName = "IsRahasia", Value = model.IsRahasia },
                    new SqlParameter() { ParameterName = "Rahasia", Value = model.Rahasia ?? "" },
                    new SqlParameter() { ParameterName = "FileName", Value = model.FileName ?? "" },
                    new SqlParameter() { ParameterName = "Username", Value = model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("ELetter_UpdateSuratKeluar", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SuratKeluarModel> Get(string dept)
        {
            try
            {
                object[] Params = { "@Dept", dept };

                return db.Read("ELetter_GetSuratKeluar", CommandType.StoredProcedure, Make, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SuratKeluarModel GetRow(int Id)
        {
            try
            {

                object[] pParams = { "@Id", Id };
                return db.Read("ELetter_GetSuratKeluarId", CommandType.StoredProcedure, Make, pParams).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KategoriSuratModel GetKategori(string idKategori)
        {
            try
            {

                object[] pParams = { "@IdKategori", idKategori };
                return db.Read("ELetter_GetKategoriSuratKode", CommandType.StoredProcedure, MakeKategori, pParams).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static readonly Func<IDataReader, SuratKeluarModel> Make = reader =>
         new SuratKeluarModel
         {
             Id = reader["Id"].AsInt(),
             Tanggal = reader["Tanggal"].AsDateTime(),
             NoSurat = reader["NoSurat"].AsString(),
             Pengirim = reader["Pengirim"].AsString(),
             Dept = reader["Dept"].AsString(),
             Instansi = reader["Instansi"].AsString(),
             Tujuan = reader["Tujuan"].AsString(),
             Jenis = reader["Jenis"].AsString(),
             Perihal = reader["Perihal"].AsString(),
             Media = reader["Media"].AsString(),             
             IdKategori = reader["IdKategori"].AsString(),
             Kategori = reader["Kategori"].AsString(),
             IsRahasia = reader["IsRahasia"].AsBool(),
             Rahasia = reader["Rahasia"].AsString(),
             Akses = reader["Akses"].AsString(),
             AksesDept = reader["AksesDept"].AsString(),
             FileName = reader["FileName"].AsString(),
         };

        static readonly Func<IDataReader, KategoriSuratModel> MakeKategori = reader =>
         new KategoriSuratModel
         {
             Id = reader["Id"].AsInt(),
             IdKategori = reader["IdKategori"].AsString(),
             Kategori = reader["Kategori"].AsString(),
             FileName = reader["FileName"].AsString(),
         };
    }
}

