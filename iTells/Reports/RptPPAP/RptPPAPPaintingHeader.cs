using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace iTells.Reports.RptPPAP
{
    public partial class RptPPAPPaintingHeader : DevExpress.XtraReports.UI.XtraReport
    {
        public RptPPAPPaintingHeader()
        {
            InitializeComponent();
        }

        private void cbToolsNichiban_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedTools(cb);
        }

        private void cbToolsMaskingKalp_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedTools(cb);
        }

        private void cbToolsLakban1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedTools(cb);
        }

        private void cbToolsLakban2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedTools(cb);
        }

        private void cbToolsMasking_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedTools(cb);
        }

        private void cbFDSPS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedFDS(cb);
        }

        private void cbFDSWI_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedFDS(cb);
        }

        private void cbFDSSCP_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedFDS(cb);
        }

        private void cbFDSTIS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedFDS(cb);
        }

        private void cbFDSSO_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedFDS(cb);
        }

        private void cbFDSBS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRCheckBox cb = sender as XRCheckBox;
            if (cb == null) return;

            CheckedFDS(cb);
        }

        private void CheckedTools(XRCheckBox cb)
        {
           string rawTools = Convert.ToString(GetCurrentColumnValue("ToolsEquipments"));

            if (!string.IsNullOrEmpty(rawTools))
            {
                string[] listTools = rawTools.Split(',')
                                             .Select(s => s.Trim())
                                             .ToArray();

                cb.Checked = listTools.Contains(cb.Text);
            }
            else
            {
                cb.Checked = false;
            }
        }

        private void CheckedFDS(XRCheckBox cb)
        {
            string rawTools = Convert.ToString(GetCurrentColumnValue("FollowDocumentStandard"));

            if (!string.IsNullOrEmpty(rawTools))
            {
                string[] listTools = rawTools.Split(',')
                                             .Select(s => s.Trim())
                                             .ToArray();

                cb.Checked = listTools.Contains(cb.Text);
            }
            else
            {
                cb.Checked = false;
            }
        }
    }
}
