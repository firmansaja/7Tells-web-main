using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class PR
    {
        public string PRNo { get; set; }
        public string Description { get; set; }
        public DateTime PRDate { get; set; }
        public string PurchaseType { get; set; }
        public string ApprovalPIC { get; set; }
        public string Remark { get; set; }
        public int? Kol { get; set; }
        public string Site { get; set; }
        public string Sub { get; set; }
        public string Dept { get; set; }
        public string Tanggal { get; set; }
        public string OverBudget { get; set; }
        public int? Kol2 { get; set; }
        public int? Kol3 { get; set; }
        public double? QtyPO { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string UserBuat { get; set; }
        public string DeptCreate { get; set; }
    }

    public class PRAksesUser
    {
        public string UserName { get; set; }
        public string Dept { get; set; }
        public int Kol { get; set; }
        public string Site { get; set; }
        public string Sub { get; set; }
    }

    public class PRApproval
    {
        public int? Kol { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Tanggal { get; set; }
    }

    public class PRDetail
    {
        public string PRNo { get; set; }
        public string Cek { get; set; }
        public string Cek2 { get; set; }
        public string Komentar { get; set; }
        public string Komentar2 { get; set; }
        public double? Harga { get; set; }
        public int? No { get; set; }
        public string NamaBarang { get; set; }
        public string InvtID { get; set; }
        public string InvtSpec { get; set; }
        public double? Qty { get; set; }
        public double? QtyPO { get; set; }
        public string UnitQty { get; set; }
        public double? Price { get; set; }
        public string Remark { get; set; }
        public string Account { get; set; }
        public string AccountDesc { get; set; }
        public string SubAccount { get; set; }
        public string PODate { get; set; }
        public double SisaBudget { get; set; }
        public double Budget { get; set; }
        public string DelDate { get; set; }
        public string Tanggal { get; set; }
        public string Dept { get; set; }
        public string PurchaseType { get; set; }
        public string Status { get; set; }
        public string Informasi { get; set; }
        public string TanggalApprove { get; set; }
    }

    public class PRHistory
    {
        public DateTime? DTDari { get; set; }
        public DateTime? DTSampai { get; set; }
        public string Cabang { get; set; }
        public string Dept { get; set; }
        public string PurchaseType { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
    }

    public class PRProgress
    {
        public DateTime? DTDari { get; set; }
        public DateTime? DTSampai { get; set; }
        public string Cabang { get; set; }
        public string Dept { get; set; }
        public string PurchaseType { get; set; }
        public string FileName { get; set; }
    }

    public class PRBudget
    {
        public string Sub { get; set; }
        public string SubDesc { get; set; }
        public string Account { get; set; }
        public string AccountDesc { get; set; }
        public double Budget01 { get; set; }
        public double Budget02 { get; set; }
        public double Budget03 { get; set; }
        public double Budget04 { get; set; }
        public double Budget05 { get; set; }
        public double Budget06 { get; set; }
        public double Budget07 { get; set; }
        public double Budget08 { get; set; }
        public double Budget09 { get; set; }
        public double Budget10 { get; set; }
        public double Budget11 { get; set; }
        public double Budget12 { get; set; }
        public double Actual01 { get; set; }
        public double Actual02 { get; set; }
        public double Actual03 { get; set; }
        public double Actual04 { get; set; }
        public double Actual05 { get; set; }
        public double Actual06 { get; set; }
        public double Actual07 { get; set; }
        public double Actual08 { get; set; }
        public double Actual09 { get; set; }
        public double Actual10 { get; set; }
        public double Actual11 { get; set; }
        public double Actual12 { get; set; }
        public double Sisa01 { get; set; }
        public double Sisa02 { get; set; }
        public double Sisa03 { get; set; }
        public double Sisa04 { get; set; }
        public double Sisa05 { get; set; }
        public double Sisa06 { get; set; }
        public double Sisa07 { get; set; }
        public double Sisa08 { get; set; }
        public double Sisa09 { get; set; }
        public double Sisa10 { get; set; }
        public double Sisa11 { get; set; }
        public double Sisa12 { get; set; }    }

    public class PRBudgetDetail
    {
        public string PRNo { get; set; }
        public int? XSeq { get; set; }
        public string InvtDescr { get; set; }
        public string InvtSpec { get; set; }
        public string InvtId { get; set; }
        public double? QtyPR { get; set; }
        public double? QtyPO { get; set; }
        public double? QtySisa { get; set; }
        public double HargaPR { get; set; }
        public double HargaPO { get; set; }
        public double AmountPR { get; set; }
        public double AmountPO { get; set; }
        public double? QtyRCV { get; set; }
        public string Remark { get; set; }
        public string Account { get; set; }
        public string AccountDesc { get; set; }
        public string SubAccount { get; set; }
        public string SubAccountDesc { get; set; }
        public string DelDate { get; set; }
    }
}
