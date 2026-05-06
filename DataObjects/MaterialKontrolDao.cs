using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BusinessObjects;
using DataObjects.Helpers;

namespace DataObjects
{
    public class MaterialKontrolDao : IMaterialKontrolDao
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static Db db = new Db();
        public List<BO_MaterialKontrol> GetStokMaterial(string Year, string account,int month)
        {
            try
            {
                
                object[] Params = { "@Year", Year, "@account", account , "@month", month };
                return db.Read("[KM_GetFC_PRPO_Stok]", CommandType.StoredProcedure, MakeStokMaterial, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_MaterialKontrol> GetStokMaterialByAccount(string Year, string Site)
        {
            try
            {
                object[] Params = { "@Year", Year, "@Site", Site };
                return db.Read("KM_InsertFC", CommandType.StoredProcedure, MakeStokMaterialByAccount, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_MaterialKontrol_ByAccount> GetStokMaterialByAccountList(string Year)
        {
            try
            {

                object[] Params = { "@Year", Year };
                return db.Read("KM_GetDataByAccount", CommandType.StoredProcedure, MakeStokMaterialByAccountList, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_Fifo> GetFifo(BO_Fifo_Excell Filter)
        {
            try
            {

                object[] Params = { "@Site", Filter.Site, "@Fifo", Filter.Fifo, "@TglAwal", Filter.TglAwal, "@TglAkhir", Filter.TglAkhir, "@Account",Filter.Account };
                return db.Read("KM_FIFO_ReportView", CommandType.StoredProcedure, MakeFifo, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_Fifo> GetFifo_DownloadExcel(BO_Fifo_Excell Filter)
        {
            try
            {

                object[] Params = { "@Site", Filter.Site, "@Fifo", Filter.Fifo, "@TglAwal", Filter.TglAwal, "@TglAkhir", Filter.TglAkhir, "@Account", Filter.Account };
                return db.Read("KM_FIFO_ReportView", CommandType.StoredProcedure, MakeFifo, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_Fifo_Stok> GetStokFifo(DateTime tgl,string site)
        {
            try
            {

                object[] Params = { "@tgl", tgl , "@site", site };
                return db.Read("[KM_FIFO_STOK_F]", CommandType.StoredProcedure, MakeFifoStok, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                FifoHub.GetFifo();
                //MessagesHub.SendMessages();
            }
        }

        private void dependencyLPH_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                FifoHub.GetLPH();
                //MessagesHub.SendMessages();
            }
        }

        public List<BO_Fifo_Stok> GetStokFifo_Auto(DateTime tgl, string site)
        {
            try
            {
                //SqlDependency.Stop(_connString);
                SqlDependency.Start(_connString);
                var Kanban = new List<BO_Fifo_Stok>();
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"SELECT [WHID]
                                                              ,[InvtID]
                                                              ,[Descr]
                                                              ,[Satuan]
                                                              ,[Tgl]
                                                              ,[Awal]
                                                              ,[Masuk]
                                                              ,[Keluar]
                                                              ,[Akhir]
                                                              ,[Trans]
                                                              ,[SiteID]
                                                              ,[Nolot]
                                                          FROM [dbo].[FIFOLocation]", connection))
                    //using (var command = new SqlCommand(@"SELECT[id]
                    //                                            ,[nama]
                    //                                       FROM [dbo].[tb_test]", connection))

                    {
                        command.Notification = null;
                        var dependency = new SqlDependency(command);

                        dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        //while (reader.Read())
                        //{
                        //    Kanban.Add(item: new BO_Fifo_Stok
                        //    {

                        //        InvtID = reader["InvtID"].AsString(),
                        //        Desc = reader["Descr"].AsString(),
                        //        Site = reader["Site"].AsString(),
                        //        StokAwal = reader["StokAwal"].AsDouble(),
                        //        Masuk = reader["Masuk"].AsDouble(),
                        //        Keluar = reader["Keluar"].AsDouble(),
                        //        Balance = reader["Balance"].AsDouble(),
                        //        MinimalStok = reader["MinimalStok"].AsDouble(),
                        //    });
                        //}

                    }

                }
                //return Kanban;
                object[] Params = { "@tgl", tgl, "@site", site };
                return db.Read("[KM_FIFO_STOK_F]", CommandType.StoredProcedure, MakeFifoStok, Params).ToList();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BO_Fifo_Stok> GetStokDetail(string invtid, string site)
        {
            try
            {

                object[] Params = { "@invtid", invtid, "@site", site };
                return db.Read("[KM_FIFO_STOK_F_Modal]", CommandType.StoredProcedure, MakeDetailModal, Params).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BO_HasilProduksi> GetLPH_auto(DateTime tgl, string site, string area, string noMesin)
        {
            try
            {
                SqlDependency.Start(_connString);
                //var Kanban = new List<BO_Fifo_Stok>();
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"SELECT  IdTrans ,
                                                                    KodePart ,
                                                                    AlternateID ,
                                                                    InvtID ,
                                                                    [Desc] ,
                                                                    RawID ,
                                                                    Jam ,
                                                                    OK ,
                                                                    Scrap ,
                                                                    Reject ,
                                                                    TotalProduksi ,
                                                                    CouterMesin ,
                                                                    Keterangan ,
                                                                    CT ,
                                                                    CTAktual ,
                                                                    Waktu
                                                            FROM    dbo.LPH_HasilProduksi_New", connection))
                    {
                        command.Notification = null;
                        var dependency = new SqlDependency(command);

                        dependency.OnChange += new OnChangeEventHandler(dependencyLPH_OnChange);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                    }

                }
                object[] Params = { "@tgl", tgl, "@site", site, "@area", area, "@noMesin", noMesin };
                return db.Read("[LPH_Display]", CommandType.StoredProcedure, MakeLPH, Params).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BO_HasilProduksiMesin> GetLPHByMesin(DateTime tgl, string noMesin)
        {
            try
            {
                SqlDependency.Start(_connString);
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"SELECT  IdTrans ,
                                                                    KodePart ,
                                                                    AlternateID ,
                                                                    InvtID ,
                                                                    [Desc] ,
                                                                    RawID ,
                                                                    Jam ,
                                                                    OK ,
                                                                    Scrap ,
                                                                    Reject ,
                                                                    TotalProduksi ,
                                                                    CouterMesin ,
                                                                    Keterangan ,
                                                                    CT ,
                                                                    CTAktual ,
                                                                    Waktu
                                                            FROM    dbo.LPH_HasilProduksi_New", connection))
                    {
                        command.Notification = null;
                        var dependency = new SqlDependency(command);

                        dependency.OnChange += new OnChangeEventHandler(dependencyLPH_OnChange);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                    }

                }
                object[] Params = { "@Tgl", tgl, "@Mesin", noMesin };
                return db.Read("[LPH_Get_DisplayMesin]", CommandType.StoredProcedure, MakeLPHMesin, Params).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BO_NoMesin> GetNoMesin()
        {
            try
            {
                var strQuery = "SELECT DISTINCT KodeMesin AS NoMesin, KodeMesin AS [Desc] FROM dbo.LPH_Leader_Mapping";
                return db.Read(strQuery, CommandType.Text, MakeNoMesin).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        static readonly Func<IDataReader, BO_MaterialKontrol> MakeStokMaterial = reader =>
        new BO_MaterialKontrol
        {
            Desc = reader["Desc"].AsString(),
            InvtID = reader["InvtID"].AsString(),
            Account = reader["Account"].AsString(),
            Unit = reader["Unit"].AsString(),
            Site = reader["Site"].AsString(),
            FC01 = reader["FCJan"].AsDouble(),
            FC02 = reader["FCFeb"].AsDouble(),
            FC03 = reader["FCMar"].AsDouble(),
            FC04 = reader["FCApr"].AsDouble(),
            FC05 = reader["FCMei"].AsDouble(),
            FC06 = reader["FCJun"].AsDouble(),
            FC07 = reader["FCJul"].AsDouble(),
            FC08 = reader["FCAgu"].AsDouble(),
            FC09 = reader["FCSep"].AsDouble(),
            FC10 = reader["FCOkt"].AsDouble(),
            FC11 = reader["FCNov"].AsDouble(),
            FC12 = reader["FCDes"].AsDouble(),

           Min01 = reader["MinJan"].AsDouble(),
           Min02 = reader["MinFeb"].AsDouble(),
           Min03 = reader["MinMar"].AsDouble(),
           Min04 = reader["MinApr"].AsDouble(),
           Min05 = reader["MinMei"].AsDouble(),
           Min06 = reader["MinJun"].AsDouble(),
           Min07 = reader["MinJul"].AsDouble(),
           Min08 = reader["MinAgu"].AsDouble(),
           Min09 = reader["MinSep"].AsDouble(),
           Min10 = reader["MinOkt"].AsDouble(),
           Min11 = reader["MinNov"].AsDouble(),
           Min12 = reader["MinDes"].AsDouble(),

            Day01 = reader["DayJan"].AsDouble(),
            Day02 = reader["DayFeb"].AsDouble(),
            Day03 = reader["DayMar"].AsDouble(),
            Day04 = reader["DayApr"].AsDouble(),
            Day05 = reader["DayMei"].AsDouble(),
            Day06 = reader["DayJun"].AsDouble(),
            Day07 = reader["DayJul"].AsDouble(),
            Day08 = reader["DayAgu"].AsDouble(),
            Day09 = reader["DaySep"].AsDouble(),
            Day10 = reader["DayOkt"].AsDouble(),
            Day11 = reader["DayNov"].AsDouble(),
            Day12 = reader["DayDes"].AsDouble(),

            Day01N1 = reader["DayJanN1"].AsDouble(),
            Day02N1 = reader["DayFebN1"].AsDouble(),
            Day03N1 = reader["DayMarN1"].AsDouble(),
            Day04N1 = reader["DayAprN1"].AsDouble(),
            Day05N1 = reader["DayMeiN1"].AsDouble(),
            Day06N1 = reader["DayJunN1"].AsDouble(),
            Day07N1 = reader["DayJulN1"].AsDouble(),
            Day08N1 = reader["DayAguN1"].AsDouble(),
            Day09N1 = reader["DaySepN1"].AsDouble(),
            Day10N1 = reader["DayOktN1"].AsDouble(),
            Day11N1 = reader["DayNovN1"].AsDouble(),
            Day12N1 = reader["DayDesN1"].AsDouble(),

            STMin01 = reader["STMinJan"].AsDouble(),
            STMin02 = reader["STMinFeb"].AsDouble(),
            STMin03 = reader["STMinMar"].AsDouble(),
            STMin04 = reader["STMinApr"].AsDouble(),
            STMin05 = reader["STMinMei"].AsDouble(),
            STMin06 = reader["STMinJun"].AsDouble(),
            STMin07 = reader["STMinJul"].AsDouble(),
            STMin08 = reader["STMinAgu"].AsDouble(),
            STMin09 = reader["STMinSep"].AsDouble(),
            STMin10 = reader["STMinOkt"].AsDouble(),
            STMin11 = reader["STMinNov"].AsDouble(),
            STMin12 = reader["STMinDes"].AsDouble(),

            STAwal01 = reader["STAwalJan"].AsDouble(),
            STAwal02 = reader["STAwalFeb"].AsDouble(),
            STAwal03 = reader["STAwalMar"].AsDouble(),
            STAwal04 = reader["STAwalApr"].AsDouble(),
            STAwal05 = reader["STAwalMei"].AsDouble(),
            STAwal06 = reader["STAwalJun"].AsDouble(),
            STAwal07 = reader["STAwalJul"].AsDouble(),
            STAwal08 = reader["STAwalAgu"].AsDouble(),
            STAwal09 = reader["STAwalSep"].AsDouble(),
            STAwal10 = reader["STAwalOkt"].AsDouble(),
            STAwal11 = reader["STAwalNov"].AsDouble(),
            STAwal12 = reader["STAwalDes"].AsDouble(),

           STIN01 = reader["STINJan"].AsDouble(),
           STIN02 = reader["STINFeb"].AsDouble(),
           STIN03 = reader["STINMar"].AsDouble(),
           STIN04 = reader["STINApr"].AsDouble(),
           STIN05 = reader["STINMei"].AsDouble(),
           STIN06 = reader["STINJun"].AsDouble(),
           STIN07 = reader["STINJul"].AsDouble(),
           STIN08 = reader["STINAgu"].AsDouble(),
           STIN09 = reader["STINSep"].AsDouble(),
           STIN10 = reader["STINOkt"].AsDouble(),
           STIN11 = reader["STINNov"].AsDouble(),
           STIN12 = reader["STINDes"].AsDouble(),

            STOut01 = reader["STOutJan"].AsDouble(),
            STOut02 = reader["STOutFeb"].AsDouble(),
            STOut03 = reader["STOutMar"].AsDouble(),
            STOut04 = reader["STOutApr"].AsDouble(),
            STOut05 = reader["STOutMei"].AsDouble(),
            STOut06 = reader["STOutJun"].AsDouble(),
            STOut07 = reader["STOutJul"].AsDouble(),
            STOut08 = reader["STOutAgu"].AsDouble(),
            STOut09 = reader["STOutSep"].AsDouble(),
            STOut10 = reader["STOutOkt"].AsDouble(),
            STOut11 = reader["STOutNov"].AsDouble(),
            STOut12 = reader["STOutDes"].AsDouble(),

            ST01 = reader["STJan"].AsDouble(),
            ST02 = reader["STFeb"].AsDouble(),
            ST03 = reader["STMar"].AsDouble(),
            ST04 = reader["STApr"].AsDouble(),
            ST05 = reader["STMei"].AsDouble(),
            ST06 = reader["STJun"].AsDouble(),
            ST07 = reader["STJul"].AsDouble(),
            ST08 = reader["STAgu"].AsDouble(),
            ST09 = reader["STSep"].AsDouble(),
            ST10 = reader["STOkt"].AsDouble(),
            ST11 = reader["STNov"].AsDouble(),
            ST12 = reader["STDes"].AsDouble(),

            PR01 = reader["PRJan"].AsDouble(),
            PR02 = reader["PRFeb"].AsDouble(),
            PR03 = reader["PRMar"].AsDouble(),
            PR04 = reader["PRApr"].AsDouble(),
            PR05 = reader["PRMei"].AsDouble(),
            PR06 = reader["PRJun"].AsDouble(),
            PR07 = reader["PRJul"].AsDouble(),
            PR08 = reader["PRAgu"].AsDouble(),
            PR09 = reader["PRSep"].AsDouble(),
            PR10 = reader["PROkt"].AsDouble(),
            PR11 = reader["PRNov"].AsDouble(),
            PR12 = reader["PRDes"].AsDouble(),
            PO01 = reader["POJan"].AsDouble(),
            PO02 = reader["POFeb"].AsDouble(),
            PO03 = reader["POMar"].AsDouble(),
            PO04 = reader["POApr"].AsDouble(),
            PO05 = reader["POMei"].AsDouble(),
            PO06 = reader["POJun"].AsDouble(),
            PO07 = reader["POJul"].AsDouble(),
            PO08 = reader["POAgu"].AsDouble(),
            PO09 = reader["POSep"].AsDouble(),
            PO10 = reader["POOkt"].AsDouble(),
            PO11 = reader["PONov"].AsDouble(),
            PO12 = reader["PODes"].AsDouble(),
        };

        static readonly Func<IDataReader, BO_MaterialKontrol> MakeStokMaterialByAccount = reader =>
        new BO_MaterialKontrol
        {
            Desc = reader["Descr"].AsString(),
            Account = reader["InvtAcct"].AsString(),
            FC01 = reader["FCJan"].AsDouble(),
            FC02 = reader["FCFeb"].AsDouble(),
            FC03 = reader["FCMar"].AsDouble(),
            FC04 = reader["FCApr"].AsDouble(),
            FC05 = reader["FCMei"].AsDouble(),
            FC06 = reader["FCJun"].AsDouble(),
            FC07 = reader["FCJul"].AsDouble(),
            FC08 = reader["FCAgu"].AsDouble(),
            FC09 = reader["FCSep"].AsDouble(),
            FC10 = reader["FCOkt"].AsDouble(),
            FC11 = reader["FCNov"].AsDouble(),
            FC12 = reader["FCDes"].AsDouble(),

        };

        static readonly Func<IDataReader, BO_MaterialKontrol_ByAccount> MakeStokMaterialByAccountList = reader =>
      new BO_MaterialKontrol_ByAccount
      {
          Account = reader["Account"].AsString(),
          AccountName = reader["AccountName"].AsString(),
          Jan = reader["Jan"].AsDouble(),
          Feb = reader["Feb"].AsDouble(),
          Mar = reader["Mar"].AsDouble(),
          Apr = reader["Apr"].AsDouble(),
          Mei = reader["Mei"].AsDouble(),
          Jun = reader["Jun"].AsDouble(),
          Jul = reader["Jul"].AsDouble(),
          Agu = reader["Agu"].AsDouble(),
          Sep = reader["Sep"].AsDouble(),
          Okt = reader["Okt"].AsDouble(),
          Nov = reader["Nov"].AsDouble(),
          Des = reader["Des"].AsDouble(),
      };


        static readonly Func<IDataReader, BO_Fifo> MakeFifo = reader =>
     new BO_Fifo
     {
         Account = reader["Account"].AsString(),
         InvtID = reader["InvtID"].AsString(),
         InvtName = reader["Desc"].AsString(),
         Site = reader["Site"].AsString(),
         Satuan = reader["Satuan"].AsString(),
         Qty = reader["Qty"].AsDouble(),
     };

        static readonly Func<IDataReader, BO_Fifo_Stok> MakeFifoStok = reader =>
    new BO_Fifo_Stok
    {
       // Tgl = reader["Account"].AsDateTime(),
        InvtID = reader["InvtID"].AsString(),
        Desc = reader["Descr"].AsString(),
       // Account = reader["Account"].AsString(),
        Site = reader["Site"].AsString(),
        //Unit = reader["Satuan"].AsString(),
        StokAwal = reader["StokAwal"].AsDouble(),
        Masuk = reader["Masuk"].AsDouble(),
        Keluar = reader["Keluar"].AsDouble(),
       Balance = reader["Balance"].AsDouble(),
       MinimalStok = reader["MinimalStok"].AsDouble(),

    };

        static readonly Func<IDataReader, BO_Fifo_Stok> MakeDetailModal = reader =>
        new BO_Fifo_Stok
        {
        InvtID = reader["InvtID"].AsString(),
        gudang = reader["Gudang"].AsString(),
        stok = reader["Akhir"].AsDouble(),
        //Tgl = reader["Tgl"].AsDateTime(),
        //nolot = reader["Nolot"].AsString(),
        };


        static readonly Func<IDataReader, BO_HasilProduksi> MakeLPH = reader =>
           new BO_HasilProduksi
           {
               NoMesin = reader["NoMesin"].AsString(),
               InvtID = reader["InvtID"].AsString(),
               Desc = reader["Desc"].AsString(),
               AlternateID = reader["AlternateID"].AsString(),
               Customer = reader["Customer"].AsString(),
               Rencana = reader["Rencana"].AsDouble(),
               Target = reader["Target"].AsDouble(),
               OK = reader["OK"].AsDouble(),
               Reject = reader["Reject"].AsDouble(),
           };

        static readonly Func<IDataReader, BO_HasilProduksiMesin> MakeLPHMesin = reader =>
           new BO_HasilProduksiMesin
           {
               RawID = reader["RawID"].AsInt(),
               Jam = reader["Jam"].AsString(),
               Tipe = reader["Tipe"].AsString(),
               AlternateID = reader["PartNo"].AsString(),
               Desc = reader["Desc"].AsString(),
               CTAktual = reader["CTAktual"].AsDouble(),
               OK = reader["OK"].AsDouble(),
               Reject = reader["Reject"].AsDouble(),
               Waktu = reader["Waktu"].AsDouble(),
               RowGroup = reader["RowGroup"].AsInt(),
               TotalGroup = reader["TotalGroup"].AsInt(),
           };

        static readonly Func<IDataReader, BO_NoMesin> MakeNoMesin = reader =>
           new BO_NoMesin
           {
               NoMesin = reader["NoMesin"].AsString(),
               Desc = reader["Desc"].AsString()
           };

    }

}
