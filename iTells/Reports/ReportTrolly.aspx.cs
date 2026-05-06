using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTells.Report;
using ActionServices;
using BusinessObjects;
using iTells.Reports.RptTrolly;

namespace iTells.Reports
{
    public partial class ReportTrolly : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoSuratJalan = System.Web.HttpContext.Current.Session["NoTrans"].ToString();
            RptTrollyOutDepo(NoSuratJalan);

        }

        private void RptTrollyOutDepo(string NoSuratJalan)
        {
            RptTrollyOut Report = new RptTrollyOut();

            Report.Parameters["NoTrans"].Value = NoSuratJalan;
            ASPxWebDocumentViewer1.OpenReport(Report);
        }
    }
}