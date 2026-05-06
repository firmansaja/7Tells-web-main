using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace iTells.Reports.RptPPAP
{
    public partial class RptPPAPQualityStatus : DevExpress.XtraReports.UI.XtraReport
    {
        private int nomor = 0;
        public RptPPAPQualityStatus()
        {
            InitializeComponent();
        }

        private void xrNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null) return;
            object levelValue = GetCurrentColumnValue("Level");

            int currentLevel = (levelValue != null && levelValue != DBNull.Value) ? Convert.ToInt32(levelValue) : 0;
            if (currentLevel == 1)
            {
                nomor += 1;
                cell.Text = nomor.ToString();
            }
            else
            {
                cell.Text = "";
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

        private void cellCapabilityInj_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = sender as XRTableCell;
            string statusValue = GetCurrentColumnValue("CapabilityInjection")?.ToString();

            EditCell(statusValue, currentCell);
        }

        private void cellCapabilityPnt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = sender as XRTableCell;
            string statusValue = GetCurrentColumnValue("CapabilityPainting")?.ToString();

            EditCell(statusValue, currentCell);
        }

        private void cellCapabilityAssy_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = sender as XRTableCell;
            string statusValue = GetCurrentColumnValue("CapabilityAssembling")?.ToString();

            EditCell(statusValue, currentCell);
        }

        private void cellDataN1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = sender as XRTableCell;
            string statusValue = GetCurrentColumnValue("DataN1")?.ToString();

            EditCell(statusValue, currentCell);
        }

        private void cellLimitSampleStatus_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = sender as XRTableCell;
            string statusValue = GetCurrentColumnValue("LimitSampleStatus")?.ToString();

            EditCell(statusValue, currentCell);
        }

        private void EditCell(string status, XRTableCell cell)
        {
            if (status == null || cell == null) return;

            if (status.ToUpper() == "O")
            {
                cell.Text = "\u25CB";
                cell.Font = new Font("Segoe UI Symbol", 6.5f, FontStyle.Regular);
            }
            else if (status.ToUpper() == "PROGRESS")
            {
                cell.Text = "\u25B3";
                cell.Font = new Font("Segoe UI Symbol", 6.5f, FontStyle.Regular);
            }
            else if (status.ToUpper() == "X")
            {
                cell.Text = "\u2716";
                cell.Font = new Font("Segoe UI Symbol", 6.5f, FontStyle.Regular);
            }
            else
            {
                cell.Font = new Font("Arial", 6.5f, FontStyle.Regular);
            }
        }
    }
}
