using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Surat
{
    public class SuratMasukModel
    {
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }
        public string NoUrut { get; set; }
        public int Flag { get; set; }
        public string NoSurat { get; set; }
        public string Dari { get; set; }
        public string Pengirim { get; set; }
        public string Tujuan { get; set; }
        public string Dept { get; set; }
        public string Security { get; set; }
        public DateTime TglAmbil { get; set; }
        public string Penerima { get; set; }
        public string DeptTerima { get; set; }
        public string IdKategori { get; set; }
        public string Kategori { get; set; }
        public string Jenis { get; set; }
        public string Media { get; set; }
        public string Perihal { get; set; }
        public string Akses { get; set; }
        public string Rahasia { get; set; }
        public bool IsRahasia { get; set; }
        public string Ket { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }
    }

    public enum SKategori
    {
        BeritaAcara,
        SuratPermohonan,
        SuratKeputusan,
        SuratKuasa,
        SuratPerintah,
        SuratPengantar,
        SuratEdaran,
        SuratUndangan,
        SuratPemberitahuan
    }

    public class SMAksesUser
    {
        public string UserName { get; set; }
        public string Dept { get; set; }
        public int Kol { get; set; }
        public string Site { get; set; }
        public string Sub { get; set; }
    }
};
