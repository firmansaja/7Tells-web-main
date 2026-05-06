using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Surat
{
   public class SuratSecModel
    {
        public int Id { get; set; }
        public string NoUrut { get; set; }
        public string NoSurat { get; set; }
        public string Dept { get; set; }
        public DateTime Tanggal{ get; set; }
        public string Tujuan { get; set; }
        public string Jenis { get; set; }
        public string Penerima { get; set; }
        public string Perihal { get; set; }
        public string Pengirim { get; set; }
        public string Media { get; set; }
        public string Kategori { get; set; }
        public string IdKategori { get; set; }
        public string Id_Kategori { get; set; }
        public string Akses { get; set; }
        public string Rahasia { get; set; }
        public string file { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ReceivedDate { get; set; }

    }
}
