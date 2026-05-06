using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Surat;
using DataObjects.Helpers;


namespace DataObjects.Surat
{
    public class SuratSecRepo : ISuratSecRepo
    {
        static Db db = new Db();
        public int Add(SuratSecModel model)
        {
            try
            {
                object[] pParams = {
                     "@NoUrut", model.NoUrut,
                     "@Dept", model.Dept,
                     "@Tanggal", model.Tanggal,
                     "@Tujuan", model.Tujuan,
                     "@Penerima", model.Penerima,
                     "@Perihal", model.Perihal,
                     "@Pengirim", model.Pengirim,
                     "@Media", model.Media,
                     "@CreatedBy", model.CreatedBy,
                     "@CreatedDate",model.CreatedDate
                };
                return db.Insert("InsertSuratSec", CommandType.StoredProcedure, pParams);
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
                object[] pParams = {
                    "@Id", Id
                };
                return db.Delete("HapusSuratSec", CommandType.StoredProcedure, pParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Edit(SuratSecModel model)
        {
            try
            {
                object[] pParams = {
                     "@Id", model.Id,
                     "@NoUrut", model.NoUrut,
                     "@Dept", model.Dept,
                     "@Tanggal", model.Tanggal,
                     "@Tujuan", model.Tujuan,
                     "@Penerima", model.Penerima,
                     "@Perihal", model.Perihal,
                     "@Pengirim", model.Pengirim,
                     "@Media", model.Media,
                     "@UpdatedBy", model.UpdatedBy,
                     "@UpdatedDate",model.UpdatedDate
                };
                return db.Update("UpdateSuratSecurity", CommandType.StoredProcedure, pParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SuratSecModel> Get()
        {
            try
            {
                return db.Read("ListSuratSecurity", CommandType.StoredProcedure, Make).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SuratSecModel GetRow(int Id)
        {
            try
            {

                object[] pParams = { "@Id", Id };
                return db.Read("ListSuratSecurity", CommandType.StoredProcedure, Make, pParams).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static readonly Func<IDataReader, SuratSecModel> Make = reader =>
         new SuratSecModel
         {
             Id = reader["Id"].AsInt(),
             NoSurat = reader["NoUrut"].AsString(),
             Dept = reader["Dept"].AsString(),
             Tanggal = reader["Tanggal"].AsDateTime(),
             Pengirim = reader["Pengirim"].AsString(),
             Perihal = reader["Perihal"].AsString(),
         };
    }
}

