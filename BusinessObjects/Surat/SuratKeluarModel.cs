using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Surat
{
   public class SuratKeluarModel
    {
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }
        public string NoSurat { get; set; }
        public string Pengirim { get; set; }
        public string Dept { get; set; }
        public string Instansi { get; set; }
        public string Tujuan { get; set; }
        public string IdKategori { get; set; }
        public string Kategori { get; set; }
        public string Jenis { get; set; }
        public string Media { get; set; }
        public string Perihal { get; set; }
        public string Akses { get; set; }
        public string AksesDept { get; set; }
        public bool IsRahasia { get; set; }
        public string Rahasia { get; set; }
        public string FileName { get; set; }
        public string Username { get; set; }
    }
}
