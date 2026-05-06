using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTells.Report;
using ActionServices;
using BusinessObjects;
using iTells.Reports.RptMbl;

namespace iTells.Reports
{
    public partial class ReportMbl : System.Web.UI.Page
    {
        //EMobilService service = new EMobilService();

        protected void Page_Load(object sender, EventArgs e)
        {

            string NoSuratJalan = System.Web.HttpContext.Current.Session["NoSuratJalan"].ToString();
            RptMblDetail(NoSuratJalan);

        }

        private void RptMblDetail(string NoSuratJalan)
        {
            RptMblSuratJalan Report = new RptMblSuratJalan();           

            Report.Parameters["NoSuratJalan"].Value = NoSuratJalan;
            ASPxWebDocumentViewer1.OpenReport(Report);
        }
    }
}