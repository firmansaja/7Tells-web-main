using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class EMobilReq
    {
        public string NoTrans { get; set; }
        public DateTime Tanggal { get; set; }
        public string Dept { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string CurrentLevel { get; set; }
        public string Approved { get; set; }
        public string Comment { get; set; }
    }

    public class EMobilReqDetail
    {
        public string Tujuan { get; set; }
        public string Keperluan { get; set; }
        public string Nik { get; set; }
        public string Nama { get; set; }
        public string BerangkatDari { get; set; }
        public DateTime BerangkatTgl { get; set; }
        public string KembaliDi { get; set; }
        public DateTime KembaliTgl { get; set; }
        public string TugasLuar { get; set; }
        public string PakaiMobil { get; set; }
        public string Catatan { get; set; }
        public string StatusJalan { get; set; }
        public string NoSuratJalan { get; set; }
    }

    public class MobilAksesUser
    {
        public string UserName { get; set; }
        public string Dept { get; set; }
        public string Site { get; set; }
        public int Level { get; set; }
    }

    public class EMobilCek
    {
        public string NoSuratJalan { get; set; }
        public DateTime Tanggal { get; set; }
        public string StatusMobil { get; set; }
        public string NoMobil { get; set; }
        public string PakaiSupir { get; set; }
        public string NamaSupir { get; set; }
        public string Catatan { get; set; }
        public DateTime BerangkatTgl { get; set; }
        public int KMAwal { get; set; }
        public DateTime KembaliTgl { get; set; }
        public int KMAkhir { get; set; }
        public string Status { get; set; }
        public string Approved { get; set; }
        public string Comment { get; set; }
        public int? CurrLvlApv { get; set; }
    }

    public class EMobilCekDetail
    {
        public string NoSuratJalan { get; set; }
        public string NoTrans { get; set; }
        public string Dept { get; set; }
        public string NIK { get; set; }
        public string Nama { get; set; }
        public string Tujuan { get; set; }
        public string Keperluan { get; set; }
        public string BerangkatDari { get; set; }
        public DateTime BerangkatTgl { get; set; }
        public string KembaliDi { get; set; }
        public DateTime KembaliTgl { get; set; }
        public string Catatan { get; set; }
    }
}
