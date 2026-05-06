using System.Collections.Generic;
//using BusinessObjects.Surat.Models;
using BusinessObjects.Surat.Models;

namespace DataObjects.Surat.Data
{
    /// <summary>
    /// Provides an in-memory data store.
    /// 
    /// Note: The code in this class is not to be considered a "best practice" 
    /// example of how to do data persistence, but rather as workaround for
    /// not having a database to persist data to.
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// The collection of activities.
        /// </summary>
        public static List<Activity> Activities { get; set; }

        /// <summary>
        /// The collection of entries.
        /// </summary>
        public static List<Entry> Entries { get; set; }


        /// <summary>
        /// The collection of entries.
        /// </summary>
        public static List<SuratMasukEntry> SuratMasukEntries { get; set; }


        /// <summary>
        /// The collection of activities.
        /// </summary>
        public static List<SuratMasukActivity> SuratMasukActivities { get; set; }



        /// <summary>
        /// The collection of activities.
        /// </summary>
        public static List<SuratMasukActivity> ActMediaSurat { get; set; }
        /// <summary>
        /// The collection of activities.
        /// </summary>
        public static List<SuratMasukActivity> ActJenisSurat { get; set; }
        //public static object Data { get; set; }

        //public static object Data { get; set; }


        /// <summary>
        /// Static constructor used to initialize the data.
        /// </summary>
        static Data()
        {
            InitData();
        }

        private static void InitData()
        {
            // Create the collection of activities first
            // so we can reference them when creating the entries collection.
            var activities = new List<Activity>()
            {
                new Activity(Activity.ActivityType.SuratPermohonan, "Surat Keluar"),
                new Activity(Activity.ActivityType.SuratKeputusan, "Surat Keputusan"),
                new Activity(Activity.ActivityType.SuratKuasa, "Surat Kuasa"),
                //new Activity(Activity.ActivityType.SuratPerintah, "Surat Tugas"),
                new Activity(Activity.ActivityType.SuratPengantar, "Surat Pengantar"),
                new Activity(Activity.ActivityType.SuratEdaran, "Internal Memo"),
                new Activity(Activity.ActivityType.SuratUndangan, "Berita Acara"),
                new Activity(Activity.ActivityType.SuratPemberitahuan, "Surat Pemberitahuan"),
            };

            var entries = new List<Entry>()
            {
                new Entry(1, 2023, 6, 10,"1PGA","001/TSC1/1PGA/SPemberitahuan/10-2023",Activity.ActivityType.SuratPemberitahuan,"Kadisnaker Banten","Eksternal","Bimtek Produktivitas","Bp. Budi Santoso","Ali D.","Hardcopy","Pemberitahuan Ketenegakerjaan"),
                new Entry(2, 2023, 6, 10,"1PGA","002/TSC1/1PGA/SPengantar/10-2023",Activity.ActivityType.SuratPengantar,"PT. Yamaha Motor Indonesia","Eksternal","Hasil Audit Tahun 2022","Bp. Adam M","Ali D.","Hardcopy","Hasil Audit Tahun 2022"),

            };


            var suratmasukactivities = new List<SuratMasukActivity>()
            {
                new SuratMasukActivity(SuratMasukActivity.ActivityType.SuratPermohonan, "Surat Pemberitahuan"),
                new SuratMasukActivity(SuratMasukActivity.ActivityType.SuratKeputusan, "Surat Keputusan"),
                new SuratMasukActivity(SuratMasukActivity.ActivityType.SuratKuasa, "Surat Kuasa"),
                //new SuratMasukActivity(SuratMasukActivity.ActivityType.SuratPerintah, "Surat Perintah"),
                new SuratMasukActivity(SuratMasukActivity.ActivityType.SuratPengantar, "Surat Pengantar"),
                new SuratMasukActivity(SuratMasukActivity.ActivityType.SuratEdaran, "Internal Memo"),
                new SuratMasukActivity(SuratMasukActivity.ActivityType.SuratUndangan, "Berita Acara"),
                new SuratMasukActivity(SuratMasukActivity.ActivityType.SuratPemberitahuan, "Surat Masuk"),
            };

            var suratmasukentries = new List<SuratMasukEntry>()
            {
                new SuratMasukEntry(1, 2023, 6, 1,"1MKT","001","ADM/01402/M/20-23/01",SuratMasukActivity.ActivityType.SuratUndangan,"PT. Astra Daihatsu Motor","Eksternal","Meeting Harga","Bp. Arman","Dini H.","Softcopy","Meeting Harga"),
                  new SuratMasukEntry(2, 2023, 6, 1,"1PGA","002","ADM/015441//20-23/01",SuratMasukActivity.ActivityType.SuratPermohonan,"PT. Astra Daihatsu Motor","Eksternal","Undangan AFT","Bp. Frank","ali D","Softcopy","Undangan AFT"),
            };



            var mediasuratactivities = new List<SuratMasukActivity>()
            {
                new SuratMasukActivity(SuratMasukActivity.ActMediaSurat.Hardcopy, "Hardcopy"),
                new SuratMasukActivity(SuratMasukActivity.ActMediaSurat.Softcopy, "Softcopy"),
                //new SuratMasukActivity(SuratMasukActivity.ActMediaSurat.Email, "Email"),
            };


            var jenissuratactivities = new List<SuratMasukActivity>()
            {
                new SuratMasukActivity(SuratMasukActivity.ActJenisSurat.Internal, "Internal"),
                new SuratMasukActivity(SuratMasukActivity.ActJenisSurat.Eksternal, "Eksternal"),
            };



            Activities = activities;
            Entries = entries;
            SuratMasukActivities = suratmasukactivities;
            SuratMasukEntries = suratmasukentries;
            ActMediaSurat = mediasuratactivities;
            ActJenisSurat = jenissuratactivities;

        }
    }
}