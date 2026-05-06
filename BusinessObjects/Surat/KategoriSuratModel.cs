using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Surat
{
    public class KategoriSuratModel
    {
        public int Id { get; set; }
        public string IdKategori { get;  set; }
        public string Kategori { get; set; }
        public bool Aktif { get; set; }
        public string FileName { get; set; }
        public string Username { get; set; }
    }
}
