using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace iTells.Reports.RptPPAP
{
    public partial class RptPPAPDetailLandscape : DevExpress.XtraReports.UI.XtraReport
    {
        public RptPPAPDetailLandscape()
        {
            InitializeComponent();
        }

        private void xrPartNumber_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null) return;

            object levelValue = GetCurrentColumnValue("Level");
            object partNoValue = GetCurrentColumnValue("PartNumber");

            int currentLevel = (levelValue != null && levelValue != DBNull.Value) ? Convert.ToInt32(levelValue) : 0;
            string partNo = (partNoValue != null) ? partNoValue.ToString() : string.Empty;

            string indent = new string(' ', currentLevel * 5);
            cell.Text = indent + partNo;

            if (currentLevel == 1)
            {
                cell.Font = new Font("Arial", 6.5f, FontStyle.Bold);
            }
            else
            {
                cell.Font = new Font("Arial", 6.5f, FontStyle.Regular);
            }
        }

        private void xrPartName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null) return;

            object levelValue = GetCurrentColumnValue("Level");
            object partNameValue = GetCurrentColumnValue("PartName");

            int currentLevel = (levelValue != null && levelValue != DBNull.Value) ? Convert.ToInt32(levelValue) : 0;
            string partName = (partNameValue != null) ? partNameValue.ToString() : string.Empty;

            string indent = new string(' ', currentLevel * 5);
            cell.Text = indent + partName;

            if (currentLevel == 1)
            {
                cell.Font = new Font("Arial", 6.5f, FontStyle.Bold);
            }
            else
            {
                cell.Font = new Font("Arial", 6.5f, FontStyle.Regular);
            }
        }

        private void xrForecast_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null) return;

            object levelValue = GetCurrentColumnValue("Level");
            int level = (levelValue != null && levelValue != DBNull.Value) ? Convert.ToInt32(levelValue) : 0;

            if (level > 1)
            {
                cell.Text = "";
            }
        }
    }
}
