using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Surat;
using DataObjects.Helpers;

namespace DataObjects.Surat
{
    public class KategoriSuratRepo : IKategoriSuratRepo
    {
        static Db db = new Db();
        public int Add(KategoriSuratModel model)
        {
            try
            {
                int Result = -1;

                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "IdKategori", Value = model.IdKategori },
                    new SqlParameter() { ParameterName = "Kategori", Value = model.Kategori },
                    new SqlParameter() { ParameterName = "Aktif", Value = model.Aktif },
                    new SqlParameter() { ParameterName = "FileName", Value = model.FileName ?? "" },
                    new SqlParameter() { ParameterName = "Username", Value = model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("ELetter_InsertKategoriSurat", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                hasil = SQLHelpers.ExecQueryWithValue("ELetter_DeleteKategoriSurat", CommandType.StoredProcedure, Params, db.connectionString);

                return hasil;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Edit(KategoriSuratModel model)
        {
            try
            {
                int Result = -1;
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "Id", Value = model.Id },
                    new SqlParameter() { ParameterName = "IdKategori", Value = model.IdKategori },
                    new SqlParameter() { ParameterName = "Kategori", Value = model.Kategori },
                    new SqlParameter() { ParameterName = "Aktif", Value = model.Aktif },
                    new SqlParameter() { ParameterName = "FileName", Value = model.FileName },
                    new SqlParameter() { ParameterName = "Username", Value = model.Username }
                };
                Result = SQLHelpers.ExecQueryWithValue("ELetter_UpdateKategoriSurat", CommandType.StoredProcedure, Params, db.connectionString);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KategoriSuratModel> Get()
        {
            try
            {
                return db.Read("ELetter_GetKategoriSurat", CommandType.StoredProcedure, Make).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KategoriSuratModel GetRow(int Id)
        {
            try
            {

                object[] pParams = { "@Id", Id };
                return db.Read("ELetter_GetKategoriSuratId", CommandType.StoredProcedure, Make, pParams).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static readonly Func<IDataReader, KategoriSuratModel> Make = reader =>
         new KategoriSuratModel
         {
             Id = reader["Id"].AsInt(),
             IdKategori = reader["IdKategori"].AsString(),
             Kategori = reader["Kategori"].AsString(),
             Aktif = reader["Aktif"].AsBool(),
             FileName = reader["FileName"].AsString(),
         };
    }
}
