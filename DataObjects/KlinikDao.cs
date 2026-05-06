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
    public class KlinikDao : IKlinikDao
    {
        static Db db = new Db();

        public List<KlinikPeriksa> GetKlinikPeriksa(DateTime TglDari, DateTime TglSampai)
        {
            try
            {
                string Query = @"SELECT  NoTrans ,
                                        Tanggal ,
                                        NIK ,
                                        Nama ,
                                        Dept ,
                                        NamaDokter ,
                                        Diagnosa ,
                                        TotalObat ,
                                        TotalHargaObat ,
                                        UserBuat ,
                                        TglBuat ,
                                        UserUbah ,
                                        TglUbah
                                FROM    dbo.T_KlinikPeriksa
                                WHERE   Tanggal >= @TglMulai
                                        AND Tanggal <= @TglSelesai";

                object[] Params = { "@TglMulai", TglDari, "@TglSelesai", TglSampai };
                return db.Read(Query, CommandType.Text, MakePeriksa, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static readonly Func<IDataReader, KlinikPeriksa> MakePeriksa = reader =>
           new KlinikPeriksa
           {
               NoTrans = reader["NoTrans"].AsString(),
               Tanggal = reader["Tanggal"].AsDateTime(),
               NIK = reader["NIK"].AsString(),
               Nama = reader["Nama"].AsString(),
               Dept = reader["Dept"].AsString(),
               NamaDokter = reader["NamaDokter"].AsString(),
               Diagnosa = reader["Diagnosa"].AsString(),
               TotalObat = reader["TotalObat"].AsInt(),
               TotalHargaObat = reader["TotalHargaObat"].AsDouble()
           };
    }
}
