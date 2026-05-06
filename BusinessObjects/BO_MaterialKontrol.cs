using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class BO_MaterialKontrol
    {

        public string InvtID { get; set; }
        public string Desc { get; set; }
        public string SubDesc { get; set; }
        public string Account { get; set; }
        public string Site { get; set; }
        public string Unit { get; set; }
        public string AccountDesc { get; set; }
        public double PR01 { get; set; }
        public double PR02 { get; set; }
        public double PR03 { get; set; }
        public double PR04 { get; set; }
        public double PR05 { get; set; }
        public double PR06 { get; set; }
        public double PR07 { get; set; }
        public double PR08 { get; set; }
        public double PR09 { get; set; }
        public double PR10 { get; set; }
        public double PR11 { get; set; }
        public double PR12 { get; set; }
        public double PO01 { get; set; }
        public double PO02 { get; set; }
        public double PO03 { get; set; }
        public double PO04 { get; set; }
        public double PO05 { get; set; }
        public double PO06 { get; set; }
        public double PO07 { get; set; }
        public double PO08 { get; set; }
        public double PO09 { get; set; }
        public double PO10 { get; set; }
        public double PO11 { get; set; }
        public double PO12 { get; set; }
        public double FC01 { get; set; }
        public double FC02 { get; set; }
        public double FC03 { get; set; }
        public double FC04 { get; set; }
        public double FC05 { get; set; }
        public double FC06 { get; set; }
        public double FC07 { get; set; }
        public double FC08 { get; set; }
        public double FC09 { get; set; }
        public double FC10 { get; set; }
        public double FC11 { get; set; }
        public double FC12 { get; set; }

        public double STMin01 { get; set; }
        public double STMin02 { get; set; }
        public double STMin03 { get; set; }
        public double STMin04 { get; set; }
        public double STMin05 { get; set; }
        public double STMin06 { get; set; }
        public double STMin07 { get; set; }
        public double STMin08 { get; set; }
        public double STMin09 { get; set; }
        public double STMin10 { get; set; }
        public double STMin11 { get; set; }
        public double STMin12 { get; set; }

        public double Min01 { get; set; }
        public double Min02 { get; set; }
        public double Min03 { get; set; }
        public double Min04 { get; set; }
        public double Min05 { get; set; }
        public double Min06 { get; set; }
        public double Min07 { get; set; }
        public double Min08 { get; set; }
        public double Min09 { get; set; }
        public double Min10 { get; set; }
        public double Min11 { get; set; }
        public double Min12 { get; set; }

        public double Day01 { get; set; }
        public double Day02 { get; set; }
        public double Day03 { get; set; }
        public double Day04 { get; set; }
        public double Day05 { get; set; }
        public double Day06 { get; set; }
        public double Day07 { get; set; }
        public double Day08 { get; set; }
        public double Day09 { get; set; }
        public double Day10 { get; set; }
        public double Day11 { get; set; }
        public double Day12 { get; set; }

        public double Day01N1 { get; set; }
        public double Day02N1 { get; set; }
        public double Day03N1 { get; set; }
        public double Day04N1 { get; set; }
        public double Day05N1 { get; set; }
        public double Day06N1 { get; set; }
        public double Day07N1 { get; set; }
        public double Day08N1 { get; set; }
        public double Day09N1 { get; set; }
        public double Day10N1 { get; set; }
        public double Day11N1 { get; set; }
        public double Day12N1 { get; set; }



        public double STAwal01 { get; set; }
        public double STAwal02 { get; set; }
        public double STAwal03 { get; set; }
        public double STAwal04 { get; set; }
        public double STAwal05 { get; set; }
        public double STAwal06 { get; set; }
        public double STAwal07 { get; set; }
        public double STAwal08 { get; set; }
        public double STAwal09 { get; set; }
        public double STAwal10 { get; set; }
        public double STAwal11 { get; set; }
        public double STAwal12 { get; set; }

        public double STIN01 { get; set; }
        public double STIN02 { get; set; }
        public double STIN03 { get; set; }
        public double STIN04 { get; set; }
        public double STIN05 { get; set; }
        public double STIN06 { get; set; }
        public double STIN07 { get; set; }
        public double STIN08 { get; set; }
        public double STIN09 { get; set; }
        public double STIN10 { get; set; }
        public double STIN11 { get; set; }
        public double STIN12 { get; set; }

        public double STOut01 { get; set; }
        public double STOut02 { get; set; }
        public double STOut03 { get; set; }
        public double STOut04 { get; set; }
        public double STOut05 { get; set; }
        public double STOut06 { get; set; }
        public double STOut07 { get; set; }
        public double STOut08 { get; set; }
        public double STOut09 { get; set; }
        public double STOut10 { get; set; }
        public double STOut11 { get; set; }
        public double STOut12 { get; set; }

        public double ST01 { get; set; }
        public double ST02 { get; set; }
        public double ST03 { get; set; }
        public double ST04 { get; set; }
        public double ST05 { get; set; }
        public double ST06 { get; set; }
        public double ST07 { get; set; }
        public double ST08 { get; set; }
        public double ST09 { get; set; }
        public double ST10 { get; set; }
        public double ST11 { get; set; }
        public double ST12 { get; set; }

        public double MinStok { get; set; }

    }

    public class BO_MaterialKontrol_ByAccount
    {
        public string Account { get; set; }
        public string AccountName { get; set; }
        public double Jan { get; set; }
        public double Feb { get; set; }
        public double Mar { get; set; }
        public double Apr { get; set; }
        public double Mei { get; set; }
        public double Jun { get; set; }
        public double Jul { get; set; }
        public double Agu { get; set; }
        public double Sep { get; set; }
        public double Okt { get; set; }
        public double Nov { get; set; }
        public double Des { get; set; }
    }

    public class BO_Fifo
    {
        public string InvtID { get; set; }
        public string InvtName { get; set; }
        public string Account { get; set; }
        public string Site { get; set; }
        public string Satuan { get; set; }
        public double Qty { get; set; }
        
    }
    public class BO_Fifo_Excell
    {
        public DateTime? TglAwal { get; set; }
        public DateTime? TglAkhir { get; set; }
        public string Fifo { get; set; }
        public string Site { get; set; }
        public string Account { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
    }

    public class BO_Fifo_Stok
    {
        public DateTime? Tgl { get; set; }
        public string InvtID { get; set; }
        public string Desc { get; set; }
        public string SubDesc { get; set; }
        public string Account { get; set; }
        public string Site { get; set; }
        public string Unit { get; set; }
        public string AccountDesc { get; set; }
        public double StokAwal { get; set; }
        public double Masuk { get; set; }
        public double Keluar { get; set; }
        public double Balance { get; set; }
        public double MinimalStok { get; set; }

        public double stok { get; set; }
        public string gudang { get; set; }
        public string nolot { get; set; }
    }
    
    public class BO_LPH
    {
        public DateTime? TglProduksi { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string IdTrans { get; set; }
        public string NoMesin { get; set; }
        public string Site { get; set; }
        public int Shift { get; set; }
        public string Leader { get; set; }
        public string Operator1 { get; set; }
        public string Operator2 { get; set; }
        public string Operator3 { get; set; }
               

        public string KodePart { get; set; }
        public string AlternateID { get; set; }
        public string InvtID { get; set; }
        public string Desc { get; set; }
        public int RawID { get; set; }
        public string Jam { get; set; }
        public double OK { get; set; }
        public double Rencana { get; set; }
        public float Scrap { get; set; }
        public double Reject { get; set; }
        public float TotalProduksi { get; set; }
        public float CT { get; set; }
        public float CTAktual { get; set; }
        public float Waktu { get; set; }
        public string Keterangan { get; set; }  
    }

    public class BO_NoMesin
    {
        public string NoMesin { get; set; }
        public string Desc { get; set; }
    }

    public class BO_HasilProduksi
    {
        public string NoMesin { get; set; }
        public string AlternateID { get; set; }
        public string InvtID { get; set; }
        public string Desc { get; set; }
        public string Customer { get; set; }
        public double Rencana { get; set; }
        public double Target { get; set; }
        public double OK { get; set; }
        public double Reject { get; set; }
    }

    public class BO_HasilProduksiMesin
    {
        public string NoMesin { get; set; }
        public string AlternateID { get; set; }
        public string InvtID { get; set; }
        public string Desc { get; set; }
        public string Customer { get; set; }
        public int RawID { get; set; }
        public string Jam { get; set; }
        public string Tipe { get; set; }
        public double Rencana { get; set; }
        public double OK { get; set; }
        public double Reject { get; set; }
        public double CT { get; set; }
        public double CTAktual { get; set; }
        public double Waktu { get; set; }
        public int RowGroup { get; set; }
        public int TotalGroup { get; set; }
    }
}
