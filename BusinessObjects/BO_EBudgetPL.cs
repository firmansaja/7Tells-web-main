using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Collections.Generic;

namespace BusinessObjects
{
    public class BO_EBudgetPL
    {

        public int RecNo { get; set; }
        public int Urut_Tampil { get; set; }
        public int No_Urut { get; set; }
        public string Ket { get; set; }
        public string Tipe { get; set; }
        public string AcctID { get; set; }
        public string AcctName { get; set; }

        public string DeptID { get; set; }
        public string DeptName { get; set; }

        public string SiteID { get; set; }

        public string Tahun { get; set; }


        public string Jml { get; set; }
        public double HasilAcct { get; set; }
        public double HasilJml { get; set; }
        public double HasilKurang { get; set; }

        


        public double HasilAcct1 { get; set; }
        public double HasilJml1 { get; set; }
        public double HasilKurang1 { get; set; }

        public double HasilAcct2 { get; set; }
        public double HasilJml2 { get; set; }
        public double HasilKurang2 { get; set; }

        public string Tambah { get; set; }
        public string Kurang { get; set; }


        public double Jan { get; set; }
        public double Feb { get; set; }
        public double Mar { get; set; }
        public double Apr { get; set; }
        public double Mei { get; set; }
        public double Jun { get; set; }
        public double Jul { get; set; }
        public double Ags { get; set; }
        public double Sep { get; set; }
        public double Okt { get; set; }
        public double Nov { get; set; }
        public double Des { get; set; }

        public double Total { get; set; }

        public double TotalActual { get; set; }



    }
}
