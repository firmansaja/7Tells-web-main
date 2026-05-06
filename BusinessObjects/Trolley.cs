using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Trolley
    {
        public int Id { get; set; }
        public string NoTrans { get; set; }
        public string InOut { get; set; }
        public DateTime TglTrans { get; set; }
        public string CustId { get; set; }
        public string NoMobil { get; set; }
        public string NamaSupir { get; set; }
        public string Username { get; set; }
        public List<TrolleyDetail> Details { get; set; }
    }

    public class TrolleyDetail
    {
        public int Id { get; set; }
        public string NoTrans { get; set; }
        public string KodeTrolley { get; set; }
        public string JenisTrolley { get; set; }
        public string Proses { get; set; }
    }
}
