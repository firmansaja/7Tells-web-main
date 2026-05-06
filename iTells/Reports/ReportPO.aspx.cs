using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTells.Report;
using ActionServices;
using BusinessObjects;

namespace iTells.Reports.RptPO
{
    public partial class ReportPO : System.Web.UI.Page
    {
        POServices service = new POServices();
        protected void Page_Load(object sender, EventArgs e)
        {
          

            //string PONo = System.Web.HttpContext.Current.Session["PONumber"].ToString();
            //string Year = System.Web.HttpContext.Current.Session["Year"].ToString();
            //string RptName = System.Web.HttpContext.Current.Session["RptName"].ToString();

            RptPODetail(); 

        }


        private void RptPODetail()
        {
            string PONo = System.Web.HttpContext.Current.Session["PO"].ToString();
            string Year = System.Web.HttpContext.Current.Session["Year"].ToString();
            //string VendorName = System.Web.HttpContext.Current.Session["VendName"].ToString();
            //string User5 = System.Web.HttpContext.Current.Session["User5"].ToString();
            RptPODetail Report = new RptPODetail();

            //List<BO_POApproval> ListApproval = new List<BO_POApproval>();
            //ListApproval = service.GetPOApprove(PONo);

            //foreach (var item in ListApproval)
            //{
            //    if (item.Kol == 1)
            //    {
            //        Report.Parameters["User1"].Value = item.Status + " by " + item.UserName;
            //        Report.Parameters["Tgl1"].Value = "Tanggal : " + item.Tanggal;
            //    }
            //    else if (item.Kol == 2)
            //    {
            //        Report.Parameters["User2"].Value = item.Status + " by " + item.UserName;
            //        Report.Parameters["Tgl2"].Value = "Tanggal : " + item.Tanggal;
            //    }
            //    else if (item.Kol == 3)
            //    {
            //        Report.Parameters["User3"].Value = item.Status + " by " + item.UserName;
            //        Report.Parameters["Tgl3"].Value = "Tanggal : " + item.Tanggal;

            //    }
            //}

            Report.Parameters["PONumber"].Value = PONo.ToString();
            //Report.Name = "PO_" + PONo.ToString() + "_" + VendorName.ToString() + "_" + User5.ToString();
            Report.Name = "PO_" + PONo.ToString();
            //Report.Parameters["Year"].Value = Year;
            ASPxWebDocumentViewer1.OpenReport(Report);
        }

    }
}