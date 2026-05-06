using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Shape;

namespace iTells.Reports.RptPPAP
{
    public partial class RptPPAPCheckItemLandscape : DevExpress.XtraReports.UI.XtraReport
    {
        public RptPPAPCheckItemLandscape()
        {
            InitializeComponent();
        }

        private void xrJudge_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null) return;

            object judgeValue = GetCurrentColumnValue("Judge");
            object isCheckValue = GetCurrentColumnValue("IsCheck");

            Boolean isCheck = (isCheckValue != null && isCheckValue != DBNull.Value) ? Convert.ToBoolean(isCheckValue) : false;
            shapeJudge.Visible = false;

            if (judgeValue != null && judgeValue != DBNull.Value)
            {
                string judge = judgeValue.ToString();

                switch (judge)
                {
                    case "Oke":
                        shapeJudge.Visible = true;
                        shapeJudge.Shape = new ShapeEllipse();
                        shapeJudge.Angle = 0;
                        break;
                    case "Pending":
                        shapeJudge.Visible = true;
                        shapeJudge.Shape = new ShapePolygon()
                        {
                            NumberOfSides = 3
                        };
                        shapeJudge.Angle = 0;
                        break;
                    case "Cancel":
                        shapeJudge.Visible = true;
                        shapeJudge.Shape = new ShapeCross()
                        {
                            VerticalLineWidth = 1,
                            HorizontalLineWidth = 1
                        };
                        shapeJudge.Angle = 50;
                        break;

                    //default:
                    //    shapeJudge.Visible = false;
                    //    break;
                }
            }

            if (isCheck == false)
            {
                cell.Text = "NR";
            }
        }

        private void xrProblem_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null) return;

            object isCheckValue = GetCurrentColumnValue("IsCheck");
            Boolean isCheck = (isCheckValue != null && isCheckValue != DBNull.Value) ? Convert.ToBoolean(isCheckValue) : false;

            if (isCheck)
            {
                xrNR.Visible = false;
            } else
            {
                xrNR.Visible = true;
            }
        }
    }
}
