using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace iTells.Reports.RptPPAP
{
    public partial class RptPPAPSupplier : DevExpress.XtraReports.UI.XtraReport
    {
        public RptPPAPSupplier()
        {
            InitializeComponent();
        }

        private void cellPIS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = sender as XRTableCell;
            string statusValue = GetCurrentColumnValue("PIS")?.ToString();

            EditCell(statusValue, currentCell);
        }

        private void cellCPFP_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = sender as XRTableCell;
            string statusValue = GetCurrentColumnValue("CPFP")?.ToString();

            EditCell(statusValue, currentCell);
        }

        private void cellIRDCheckSheet_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = sender as XRTableCell;
            string statusValue = GetCurrentColumnValue("IRDCheckSheet")?.ToString();

            EditCell(statusValue, currentCell);
        }

        private void cellN1Data_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = sender as XRTableCell;
            string statusValue = GetCurrentColumnValue("N1Data")?.ToString();

            EditCell(statusValue, currentCell);
        }

        private void cellLimitSample_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell currentCell = sender as XRTableCell;
            string statusValue = GetCurrentColumnValue("LimitSample")?.ToString();

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
