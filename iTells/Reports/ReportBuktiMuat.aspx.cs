using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTells.Reports.RptPPIC;

namespace iTells.Reports
{
    public partial class ReportBuktiMuat : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string NoSeq = System.Web.HttpContext.Current.Session["NoSeq"].ToString();
            RptBuktiMuatOut(NoSeq);

        }

        private void RptBuktiMuatOut(string NoSeq)
        {
            RptBuktiMuatOut Report = new RptBuktiMuatOut();

            Report.Parameters["NoSeq"].Value = NoSeq;
            ASPxWebDocumentViewer1.OpenReport(Report);
        }
    }
}