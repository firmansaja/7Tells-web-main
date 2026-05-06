using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class BO_PO
    {
        public string PONumber { get; set; }
        public string PRNo { get; set; }
        public string CreateUser { get; set; }
        public string PODate { get; set; }

        public string ClassID { get; set; }
        public string VendorName { get; set; }
        public string VendAttn { get; set; }
        public string VendAddr1 { get; set; }
        public string VendAddr2 { get; set; }
        public string VendCity { get; set; }
        public string VendPhone { get; set; }
        public string VendFax { get; set; }
        public string ShipName { get; set; }
        public string ShipAddr1 { get; set; }
        public string ShipAddr2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public string ShipCountry { get; set; }
        public string Negara { get; set; }
        public string Terms { get; set; }
        public string BillName { get; set; }
        public string BillAttn { get; set; }
        public string BillAddr1 { get; set; }
        public string BillAddr2 { get; set; }
        public string BillCity { get; set; }
        public string BillState { get; set; }
        public string BillZip { get; set; }
        public string BillPhone { get; set; }
        public string BillFax { get; set; }
        public string BillCountry { get; set; }
        public string CuryID { get; set; }
        public string Negara2 { get; set; }
        public string User5 { get; set; }
        public string Revisi { get; set; }
        public double TotalAmount { get; set; }
        public string Tax { get; set; }
        public string TaxF { get; set; }
        public string SNote { get; set; }

        public int? Kol2 { get; set; }
        public int? Kol3 { get; set; }

        public string User6 { get; set; }
        public string Email { get; set; }
        public string TglEmail { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }


    }

    public class BO_PODetail
    {
        public string PONumber { get; set; }
        public string LineID { get; set; }
        public string InvtID { get; set; }
        public string Description { get; set; }
        public string PRNo { get; set; }
        public string Account { get; set; }
        public string PromDate { get; set; }
        public string ReqNbr { get; set; }
        public double QtyOrd { get; set; }
        public string Unit { get; set; }
        public double CuryUnitCost { get; set; }
        public double total { get; set; }
        public string No { get; set; }
        public string Komentar { get; set; }
        public string cek { get; set; }
        public string User5 { get; set; }

        public string UserName { get; set; }
        public int Kol { get; set; }





    }

    public class POAksesUser
    {
        public string user_id { get; set; }
        public string Jenis { get; set; }
        public int Kol { get; set; }
        public string VendId { get; set; }
        public string Email { get; set; }
    }

    public class BO_POApproval
    {
        public int? Kol { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Tanggal { get; set; }
    }

    public class BO_POExport
    {
        public string PONbr { get; set; }
        public string PRNo { get; set; }
        public DateTime PODate { get; set; }
        public string Name { get; set; }
        public string ShipName { get; set; }
        public string ShipAddr1 { get; set; }
        public string ShipAddr2 { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public string ShipCountry { get; set; }
        public string Negara { get; set; }
        public string Terms { get; set; }
        public string BillName { get; set; }
        public string BillAttn { get; set; }
        public string BillAddr1 { get; set; }
        public string BillAddr2 { get; set; }
        public string BillCity { get; set; }
        public string BillZip { get; set; }
        public string BillState { get; set; }
        public string BillPhone { get; set; }
        public string BillFax { get; set; }
        public string BillCountry { get; set; }
        public string CuryID { get; set; }
        public string User5 { get; set; }
        public string Negara2 { get; set; }
        public string LineID { get; set; }
        public string InvtID { get; set; }
        public string AlternateID { get; set; }
        public string TranDesc { get; set; }
        public string User1 { get; set; }
        public DateTime PromDate { get; set; }
        public double QtyOrd { get; set; }
        public double QtyRcvd { get; set; }
        public string PurchUnit { get; set; }
        public double CuryUnitCost { get; set; }
        public string totb { get; set; }
        public string ReqNbr { get; set; }
        public string VendId { get; set; }
        public string Level { get; set; }
        public string Note { get; set; }
        public string CirculationNo { get; set; }
        public string SiteId { get; set; }

    }

    public class POHistory
    {
        public DateTime? DTDari { get; set; }
        public DateTime? DTSampai { get; set; }
        public string PONumber { get; set; }
        public string PRNumber { get; set; }
        public string Level { get; set; }
        public string Supplier { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }

        public DateTime DTAwal { get; set; }
        public DateTime DTakhir { get; set; }
    }

    public class PODetailHistory
    {
        public DateTime? DTDari { get; set; }
    }

}
