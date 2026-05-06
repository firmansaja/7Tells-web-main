using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Shape;
using System.Data;
using BusinessObjects.PPAP;

namespace iTells.Reports.RptPPAP
{
    public partial class RptPPAPApprovalDeptLandscape : DevExpress.XtraReports.UI.XtraReport
    {
        public RptPPAPApprovalDeptLandscape()
        {
            InitializeComponent();
        }

        private void SetApprovalDept(XRShape shapeJudgment, XRTableCell cellUsername, XRBarCode codeUsername, XRTableCell cellComment, XRLine lineCheck, string dept)
        {
            if (shapeJudgment == null || cellUsername == null || codeUsername == null || cellComment == null) return;

            var datasource = this.DataSource as List<PPAPApprovalDeptModel>;

            if (datasource == null) return;

            var data = datasource.FirstOrDefault(x => x.Dept.ToLower() == dept);

            if (data != null)
            {
                string username = data.Username ?? "";
                string nik = data.NIK ?? "";
                string descr = data.Descr ?? "";
                string judgment = data.Judgment ?? "";
                string comment = data.Comment ?? "";
                bool isCheck = data.IsCheck;

                string tanggal = data.Tanggal != null
                     ? data.Tanggal.Value.ToString("dd/MM/yyyy HH:mm")
                     : "";

                lineCheck.Visible = true;
                shapeJudgment.Visible = false;

                if (isCheck)
                {
                    lineCheck.Visible = false;

                    if (!string.IsNullOrEmpty(username))
                    {
                        codeUsername.Text = $"{username};{nik};{descr};{tanggal}";
                        cellComment.Text = comment;

                        switch (judgment)
                        {
                            case "Oke":
                                shapeJudgment.Visible = true;
                                shapeJudgment.Shape = new ShapeEllipse();
                                shapeJudgment.Angle = 0;
                                break;

                            case "Pending":
                                shapeJudgment.Visible = true;
                                shapeJudgment.Shape = new ShapePolygon() { NumberOfSides = 3 };
                                shapeJudgment.Angle = 0;
                                break;

                            case "Cancel":
                                shapeJudgment.Visible = true;
                                shapeJudgment.Shape = new ShapeCross()
                                {
                                    VerticalLineWidth = 1,
                                    HorizontalLineWidth = 1
                                };
                                shapeJudgment.Angle = 50;
                                break;

                            //default:
                            //    shapeJudgment.Visible = false;
                            //    break;
                        }
                    }
                    else
                    {
                        SetControlEmpty(cellComment, codeUsername, shapeJudgment);
                    }
                }
                else
                {
                    SetControlEmpty(cellComment, codeUsername, shapeJudgment);
                }
            }
            else
            {
                SetControlEmpty(cellComment, codeUsername, shapeJudgment);
            }
        }

        private void SetControlEmpty(XRTableCell cellComment, XRBarCode codeUsername, XRShape shapeJudgment)
        {
            cellComment.Text = "";
            codeUsername.Visible = false;
            shapeJudgment.Visible = false;
        }

        private void xrMkt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetApprovalDept(shapeMkt, xrMktUsername, bcMktUsername, xrMktComment, lineMkt, "mkt");
        }

        private void xrPurch_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetApprovalDept(shapePurch, xrPurchUsername, bcPurchUsername, xrPurchComment, LinePurch, "purch");
        }

        private void xrPPIC_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetApprovalDept(shapePPIC, xrPPICUsername, bcPPICUsername, xrPPICComment, linePPIC, "ppic");
        }

        private void xrQuality_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetApprovalDept(shapeQuality, xrQualityUsername, bcQualityUsername, xrQualityComment, lineQuality, "quality");
        }

        private void xrEng_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetApprovalDept(shapeEng, xrEngUsername, bcEngUsername, xrEngComment, lineEng, "eng");
        }

        private void xrProdA_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetApprovalDept(shapeProdA, xrProdAUsername, bcProdAUsername, xrProdAComment, lineProdA, "proda");
        }

        private void xrProdB_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetApprovalDept(shapeProdB, xrProdBUsername, bcProdBUsername, xrProdBComment, lineProdB, "prodb");
        }

        private void xrVM_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetApprovalDept(shapeVM, xrVMUsername, bcVMUsername, xrVMComment, lineVM, "vm");
        }

        private void xrCosting_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetApprovalDept(shapeCosting, xrCostingUsername, bcCostingUsername, xrCostingComment, lineCosting, "costing");
        }

        private void xrNPD_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrNPDUsername == null || xrNPDComment == null) return;

            var datasource = this.DataSource as List<PPAPApprovalDeptModel>;

            if (datasource == null) return;

            var data = datasource.FirstOrDefault(x => x.Dept.ToLower() == "npd");

            if (data != null)
            {
                string username = data.Username ?? "";
                string nik = data.NIK ?? "";
                string descr = data.Descr ?? "";
                string judgment = data.Judgment ?? "";
                string comment = data.Comment ?? "";
                bool isCheck = data.IsCheck;

                string tanggal = data.Tanggal != null
                     ? data.Tanggal.Value.ToString("dd/MM/yyyy HH:mm")
                     : "";

                if (isCheck)
                {
                    if (!string.IsNullOrEmpty(username))
                    {
                        xrNPDComment.Text = comment;
                        bcNPDUsername.Text = $"{username};{nik};{descr};{tanggal}";
                    }
                    else
                    {
                        xrNPDComment.Text = "";
                        bcNPDUsername.Visible = false;
                    }
                }
                else
                {
                    xrNPDComment.Text = "";
                    bcNPDUsername.Visible = false;
                }
            }
            else
            {
                xrNPDComment.Text = "";
                bcNPDUsername.Visible = false;
            }
        }

        private void xrTM_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrTMUsername == null || xrTMComment == null) return;

            var datasource = this.DataSource as List<PPAPApprovalDeptModel>;

            if (datasource == null) return;

            var data = datasource.FirstOrDefault(x => x.Dept.ToLower() == "tm");

            if (data != null)
            {
                string username = data.Username ?? "";
                string nik = data.NIK ?? "";
                string descr = data.Descr ?? "";
                string judgment = data.Judgment ?? "";
                string comment = data.Comment ?? "";
                bool isCheck = data.IsCheck;

                string tanggal = data.Tanggal != null
                     ? data.Tanggal.Value.ToString("dd/MM/yyyy HH:mm")
                     : "";

                if (isCheck)
                {
                    if (!string.IsNullOrEmpty(username))
                    {
                        xrTMComment.Text = comment;
                        bcTMUsername.Text = $"{username};{nik};{descr};{tanggal}";
                    }
                    else
                    {
                        xrTMComment.Text = "";
                        bcTMUsername.Visible = false;
                    }
                }
                else
                {
                    xrTMComment.Text = "";
                    bcTMUsername.Visible = false;
                }
            }
            else
            {
                xrTMComment.Text = "";
                bcTMUsername.Visible = false;
            }
        }
    }
}
