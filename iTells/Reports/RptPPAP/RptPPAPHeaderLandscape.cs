using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace iTells.Reports.RptPPAP
{
    public partial class RptPPAPHeaderLandscape : DevExpress.XtraReports.UI.XtraReport
    {
        public RptPPAPHeaderLandscape()
        {
            InitializeComponent();
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            object val = GetCurrentColumnValue("IdStatus");

            shapeStatusPre.Visible = false;
            shapeStatus1.Visible = false;
            shapeStatus2.Visible = false;
            shapeStatusFinal.Visible = false;

            if (val != null && val != DBNull.Value)
            {
                string idStatus = val.ToString();

                switch (idStatus)
                {
                    case "1":
                        shapeStatusPre.Visible = true;
                        break;
                    case "2":
                        shapeStatus1.Visible = true;
                        break;
                    case "3":
                        shapeStatus2.Visible = true;
                        break;
                    case "4":
                        shapeStatusFinal.Visible = true;
                        break;
                }
            }
        }
    }
}
