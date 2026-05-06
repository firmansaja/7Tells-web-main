using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class BO_EBudget
    {
        public List<BudgetDetail> BD { get; set; }
        public int RowNumber { get; set; }
        public int IdNo { get; set; }
        public int No { get; set; }
        public string DeptName { get; set; }

        public string AcctID { get; set; }
        public string AcctName { get; set; }
        public double agt_harga { get; set; }
        public double agt_qty { get; set; }
        public double apr_harga { get; set; }
        public double apr_qty { get; set; }
        public string DeptID { get; set; }
        public double des_harga { get; set; }
        public double des_qty { get; set; }
        public double feb_harga { get; set; }
        public double feb_qty { get; set; }
        public double jan_harga { get; set; }
        public double jan_qty { get; set; }
        public double jul_harga { get; set; }
        public double jul_qty { get; set; }
        public double jun_harga { get; set; }
        public double jun_qty { get; set; }
        public string Keterangan { get; set; }
        public double mar_harga { get; set; }
        public double mar_qty { get; set; }
        public double mei_harga { get; set; }
        public double mei_qty { get; set; }
        public double nov_harga { get; set; }
        public double nov_qty { get; set; }
        public double okt_harga { get; set; }
        public double okt_qty { get; set; }
        public double sep_harga { get; set; }
        public double sep_qty { get; set; }
        public string SiteID { get; set; }
        public string Tahun { get; set; }
        public string Unit { get; set; }

        public double Jan { get; set; }
        public double Feb { get; set; }
        public double Mar { get; set; }
        public double Apr { get; set; }
        public double Mei { get; set; }
        public double Jun { get; set; }
        public double Jul { get; set; }
        public double Agst { get; set; }
        public double Sept { get; set; }
        public double Okt { get; set; }
        public double Nov { get; set; }
        public double Des { get; set; }
        public double Subtotal { get; set; }

        public double Semester1 { get; set; }
        public double Semester2 { get; set; }

        public double ApproveBODS1 { get; set; }
        public double ApproveBODS2 { get; set; }


        public string FilterTahun { get; set; }
        public string FilterDeptID { get; set; }
        public string FilterAcctID { get; set; }

        public string OpenTahun { get; set; }

        public string OpenSemester { get; set; }
        public string OpenStatus { get; set; }



    }

    public class BudgetDetail
    {


        public int RowNumber { get; set; }
        public int IdNo { get; set; }
        public int No { get; set; }
        public string AcctID { get; set; }
        public string AcctName { get; set; }
        public double agt_harga { get; set; }
        public double agt_qty { get; set; }
        public double apr_harga { get; set; }
        public double apr_qty { get; set; }
        public string DeptID { get; set; }
        public double des_harga { get; set; }
        public double des_qty { get; set; }
        public double feb_harga { get; set; }
        public double feb_qty { get; set; }
        public double jan_harga { get; set; }
        public double jan_qty { get; set; }
        public double jul_harga { get; set; }
        public double jul_qty { get; set; }
        public double jun_harga { get; set; }
        public double jun_qty { get; set; }
        public string Keterangan { get; set; }
        public double mar_harga { get; set; }
        public double mar_qty { get; set; }
        public double mei_harga { get; set; }
        public double mei_qty { get; set; }
        public double nov_harga { get; set; }
        public double nov_qty { get; set; }
        public double okt_harga { get; set; }
        public double okt_qty { get; set; }
        public double sep_harga { get; set; }
        public double sep_qty { get; set; }
        public string SiteID { get; set; }
        public string Tahun { get; set; }
        public string Unit { get; set; }


    }

    public class BudgetAksesUser
    {
        public string UserName { get; set; }
        public string Dept { get; set; }
        public int Kol { get; set; }
        public string Site { get; set; }
        public string Sub { get; set; }
    }

    public class BO_EBudgetvsActual
    {
        public List<BudgetDetail> BD { get; set; }
        public int RowNumber { get; set; }
        public int IdNo { get; set; }
        public int No { get; set; }
        public string DeptName { get; set; }
        public string AcctID { get; set; }
        public string AcctName { get; set; }
        public double agt_harga { get; set; }
        public double agt_qty { get; set; }
        public double apr_harga { get; set; }
        public double apr_qty { get; set; }
        public string DeptID { get; set; }
        public double des_harga { get; set; }
        public double des_qty { get; set; }
        public double feb_harga { get; set; }
        public double feb_qty { get; set; }
        public double jan_harga { get; set; }
        public double jan_qty { get; set; }
        public double jul_harga { get; set; }
        public double jul_qty { get; set; }
        public double jun_harga { get; set; }
        public double jun_qty { get; set; }
        public string Keterangan { get; set; }
        public double mar_harga { get; set; }
        public double mar_qty { get; set; }
        public double mei_harga { get; set; }
        public double mei_qty { get; set; }
        public double nov_harga { get; set; }
        public double nov_qty { get; set; }
        public double okt_harga { get; set; }
        public double okt_qty { get; set; }
        public double sep_harga { get; set; }
        public double sep_qty { get; set; }
        public string SiteID { get; set; }
        public string Tahun { get; set; }
        public string Unit { get; set; }
        public double JanBudget { get; set; }
        public double FebBudget { get; set; }
        public double MarBudget { get; set; }
        public double AprBudget { get; set; }
        public double MeiBudget { get; set; }
        public double JunBudget { get; set; }
        public double JulBudget { get; set; }
        public double AgtBudget { get; set; }
        public double SepBudget { get; set; }
        public double OktBudget { get; set; }
        public double NovBudget { get; set; }
        public double DesBudget { get; set; }
        public double BudgetSubtotal { get; set; }
        public double JanActual { get; set; }
        public double FebActual { get; set; }
        public double MarActual { get; set; }
        public double AprActual { get; set; }
        public double MeiActual { get; set; }
        public double JunActual { get; set; }
        public double JulActual { get; set; }
        public double AgtActual { get; set; }
        public double SepActual { get; set; }
        public double OktActual { get; set; }
        public double NovActual { get; set; }
        public double DesActual { get; set; }
        public double ActualSubtotal { get; set; }
        public double Semester1 { get; set; }
        public double Semester2 { get; set; }
        public double ApproveBODS1 { get; set; }
        public double ApproveBODS2 { get; set; }
        public string FilterTahun { get; set; }
        public string FilterDeptID { get; set; }
        public string FilterAcctID { get; set; }
        public string OpenTahun { get; set; }
        public string OpenSemester { get; set; }
        public string OpenStatus { get; set; }
    }

}
