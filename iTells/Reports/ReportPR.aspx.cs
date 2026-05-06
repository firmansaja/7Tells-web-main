using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTells.Report;
using ActionServices;
using BusinessObjects;

namespace iTells.Areas.User.Views.PR
{
    public partial class ReportPR : System.Web.UI.Page
    {
        PRService service = new PRService();

        protected void Page_Load(object sender, EventArgs e)        {

            string PRNo = System.Web.HttpContext.Current.Session["PRNo"].ToString();
            string Year = System.Web.HttpContext.Current.Session["Year"].ToString();
            string RptName = System.Web.HttpContext.Current.Session["RptName"].ToString();

            if (PRNo != "")
            {
                if (RptName == "RptPRDetail")
                {
                    RptPRDetail();
                }
                else if (RptName == "RptPRDetailAll") {
                    RptPRDetailAll();
                }
                else if (RptName == "RptPRDetailUser")
                {
                    RptPRDetailUser();
                }
                else if (RptName == "RptPRDetailPur")
                {
                    RptPRDetailPur();
                }
            }
        }

        private void RptPRDetail()
        {
            string PRNo = System.Web.HttpContext.Current.Session["PRNo"].ToString();
            string Year = System.Web.HttpContext.Current.Session["Year"].ToString();
            RptPRDetail Report = new RptPRDetail();            

            List<PRApproval> ListApproval = new List<PRApproval>();
            ListApproval = service.GetPRApprove(PRNo);

            foreach (var item in ListApproval)
            {
                if (item.Kol == 1)
                {
                    Report.Parameters["User1"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl1"].Value = "Tanggal : " + item.Tanggal;
                }
                else if (item.Kol == 2)
                {
                    Report.Parameters["User2"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl2"].Value = "Tanggal : " + item.Tanggal;
                }
                else if (item.Kol == 3)
                {
                    Report.Parameters["User3"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl3"].Value = "Tanggal : " + item.Tanggal;

                }
            }

            Report.Parameters["PRNo"].Value = PRNo;
            Report.Parameters["Year"].Value = Year;
            ASPxWebDocumentViewer1.OpenReport(Report);
        }

        private void RptPRDetailAll()
        {
            string PRNo = System.Web.HttpContext.Current.Session["PRNo"].ToString();
            string Year = System.Web.HttpContext.Current.Session["Year"].ToString();
            RptPRDetailAll Report = new RptPRDetailAll();

            List<PRApproval> ListApproval = new List<PRApproval>();
            ListApproval = service.GetPRApprove(PRNo);

            foreach (var item in ListApproval)
            {
                if (item.Kol == 1)
                {
                    Report.Parameters["User1"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl1"].Value = "Tanggal : " + item.Tanggal;
                }
                else if (item.Kol == 2)
                {
                    Report.Parameters["User2"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl2"].Value = "Tanggal : " + item.Tanggal;
                }
                else if (item.Kol == 3)
                {
                    Report.Parameters["User3"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl3"].Value = "Tanggal : " + item.Tanggal;

                }
            }

            Report.Parameters["PRNo"].Value = PRNo;
            Report.Parameters["Year"].Value = Year;
            ASPxWebDocumentViewer1.OpenReport(Report);
        }

        private void RptPRDetailUser()
        {
            string PRNo = System.Web.HttpContext.Current.Session["PRNo"].ToString();
            string Year = System.Web.HttpContext.Current.Session["Year"].ToString();
            RptPRDetailUser Report = new RptPRDetailUser();

            List<PRApproval> ListApproval = new List<PRApproval>();
            ListApproval = service.GetPRApprove(PRNo);

            foreach (var item in ListApproval)
            {
                if (item.Kol == 1)
                {
                    Report.Parameters["User1"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl1"].Value = "Tanggal : " + item.Tanggal;
                }
                else if (item.Kol == 2)
                {
                    Report.Parameters["User2"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl2"].Value = "Tanggal : " + item.Tanggal;
                }
                else if (item.Kol == 3)
                {
                    Report.Parameters["User3"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl3"].Value = "Tanggal : " + item.Tanggal;

                }
            }

            Report.Parameters["PRNo"].Value = PRNo;
            Report.Parameters["Year"].Value = Year;
            ASPxWebDocumentViewer1.OpenReport(Report);
        }

        private void RptPRDetailPur()
        {
            string PRNo = System.Web.HttpContext.Current.Session["PRNo"].ToString();
            string Year = System.Web.HttpContext.Current.Session["Year"].ToString();
            RptPRDetailPur Report = new RptPRDetailPur();

            List<PRApproval> ListApproval = new List<PRApproval>();
            ListApproval = service.GetPRApprove(PRNo);

            foreach (var item in ListApproval)
            {
                if (item.Kol == 1)
                {
                    Report.Parameters["User1"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl1"].Value = "Tanggal : " + item.Tanggal;
                }
                else if (item.Kol == 2)
                {
                    Report.Parameters["User2"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl2"].Value = "Tanggal : " + item.Tanggal;
                }
                else if (item.Kol == 3)
                {
                    Report.Parameters["User3"].Value = item.Status + " by " + item.UserName;
                    Report.Parameters["Tgl3"].Value = "Tanggal : " + item.Tanggal;

                }
            }

            Report.Parameters["PRNo"].Value = PRNo;
            Report.Parameters["Year"].Value = Year;
            ASPxWebDocumentViewer1.OpenReport(Report);
        }
    }
}