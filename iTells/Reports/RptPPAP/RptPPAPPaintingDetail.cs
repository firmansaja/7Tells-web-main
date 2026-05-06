using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace iTells.Reports.RptPPAP
{
    public partial class RptPPAPPaintingDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public RptPPAPPaintingDetail()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string currentCode = Parameters["pCode"].Value?.ToString() ?? "";
            string rowCode = GetCurrentColumnValue("Code")?.ToString() ?? "";

            if (rowCode != currentCode)
            {
                e.Cancel = true;
            }
        }

        private void xrSelisih_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null) return;

            double spec = SafeDouble(GetCurrentColumnValue("Spec"));
            double actual = SafeDouble(GetCurrentColumnValue("Actual"));

            if (double.IsNaN(spec) || double.IsNaN(actual))
            {
                cell.Text = "";
                cell.ForeColor = Color.Black;
                return;
            }

            double selisih = spec - actual;

            cell.Text = selisih.ToString("n2");

            if (selisih < 0)
            {
                cell.ForeColor = Color.Red;
            }
            else
            {
                cell.ForeColor = Color.Black;
            }
        }

        private double SafeDouble(object value)
        {
            if (value == null || Convert.IsDBNull(value))
            {
                return double.NaN;
            }

            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return double.NaN;
            }
        }
    }
}
