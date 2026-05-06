using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTells.Reports.RptPPAP;
using ActionServices;
using BusinessObjects.PPAP;
using DevExpress.XtraReports.UI;

namespace iTells.Reports
{
    public partial class ReportPPAP : System.Web.UI.Page
    {
        PPAPService service = new PPAPService();

        protected void Page_Load(object sender, EventArgs e)
        {
            RptPPAP();
        }

        private void RptPPAP()
        {
            string noPPAP = Session["NoPPAP"]?.ToString();
            string idStatus = Session["IdStatus"]?.ToString();

            RptPPAPAll lapAll = new RptPPAPAll();
            RptPPAPHeaderLandscape lapHeader = new RptPPAPHeaderLandscape();
            RptPPAPDetailLandscape lapDetail = new RptPPAPDetailLandscape();
            RptPPAPCheckItemLandscape lapCheckItem = new RptPPAPCheckItemLandscape();
            RptPPAPApprovalDeptLandscape lapApprovalDeptLandscape = new RptPPAPApprovalDeptLandscape();
            RptPPAPAttendance lapAttendantLandscape1 = new RptPPAPAttendance();
            RptPPAPAttendance lapAttendantLandscape2 = new RptPPAPAttendance();
            RptPPAPAttendance lapAttendantLandscape3 = new RptPPAPAttendance();
            RptPPAPAttendance lapAttendantLandscape4 = new RptPPAPAttendance();
            RptPPAPToolingStatus lapToolingStatus = new RptPPAPToolingStatus();
            RptPPAPQualityStatus lapQualityStatus = new RptPPAPQualityStatus();
            RptPPAPPreparationStatus lapPreparationStatus = new RptPPAPPreparationStatus();
            RptPPAPDocument lapDocument = new RptPPAPDocument();
            RptPPAPSupplier lapSupplier = new RptPPAPSupplier();
            RptPPAPPackingStatus lapPackingStatus = new RptPPAPPackingStatus();
            RptPPAPComparation lapComparation = new RptPPAPComparation();
            RptPPAPPaintingHeader lapPaintingHeader = new RptPPAPPaintingHeader();
            RptPPAPPaintingDetail lapPaintingDetail = new RptPPAPPaintingDetail();
            RptPPAPAssembly lapAssembly = new RptPPAPAssembly();

            var dataHeader =  service.GetPPAPById(noPPAP, idStatus);
            var dataSourceList = new List<PPAPHeaderModel> { dataHeader };
            var dataDetail = service.GetPPAPDetailById(noPPAP, idStatus);
            var dataCheckItem = service.GetPPAPCheckItemById(noPPAP, idStatus);
            var dataApprovalDept = service.GetPPAPApprovalDeptById(noPPAP, idStatus);
            var dataAttendance = service.GetPPAPAttendanceById(noPPAP, idStatus);

            XRSubreport subreportHeader = lapAll.FindControl("subReportHeader", true) as XRSubreport;
            if (subreportHeader != null)
            {
                lapHeader.DataSource = dataSourceList;
                subreportHeader.ReportSource = lapHeader;
            }

            #region Attendance
            List<PPAPAttendanceModel> dataAttendance1 = dataAttendance
                .Where(x => x.Seq <= 5)
                .ToList();

            if (dataAttendance1.Count > 0)
            {
                XRSubreport subreportAttendance1 = lapHeader.FindControl("subAttendance1", true) as XRSubreport;
                if (subreportAttendance1 != null)
                {
                    lapAttendantLandscape1.DataSource = dataAttendance1;
                    subreportAttendance1.ReportSource = lapAttendantLandscape1;
                }
            }

            List<PPAPAttendanceModel> dataAttendance2 = dataAttendance
                .Where(x => x.Seq >= 6 && x.Seq <= 10)
                .ToList();

            if (dataAttendance2.Count > 0)
            {
                XRSubreport subreportAttendance2 = lapHeader.FindControl("subAttendance2", true) as XRSubreport;
                if (subreportAttendance2 != null)
                {
                    lapAttendantLandscape2.DataSource = dataAttendance2;
                    subreportAttendance2.ReportSource = lapAttendantLandscape2;
                }
            }

            List<PPAPAttendanceModel> dataAttendance3 = dataAttendance
                .Where(x => x.Seq >= 11 && x.Seq <= 15)
                .ToList();

            if (dataAttendance3.Count > 0)
            {
                XRSubreport subreportAttendance3 = lapHeader.FindControl("subAttendance3", true) as XRSubreport;
                if (subreportAttendance3 != null)
                {
                    lapAttendantLandscape3.DataSource = dataAttendance3;
                    subreportAttendance3.ReportSource = lapAttendantLandscape3;
                }
            }

            List<PPAPAttendanceModel> dataAttendance4 = dataAttendance
                .Where(x => x.Seq >= 16 && x.Seq <= 20)
                .ToList();

            if (dataAttendance4.Count > 0)
            {
                XRSubreport subreportAttendance4 = lapHeader.FindControl("subAttendance4", true) as XRSubreport;
                if (subreportAttendance4 != null)
                {
                    lapAttendantLandscape4.DataSource = dataAttendance4;
                    subreportAttendance4.ReportSource = lapAttendantLandscape4;
                }
            }
            #endregion            

            XRSubreport subreportDetail = lapHeader.FindControl("subDetail", true) as XRSubreport;
            if (subreportDetail != null)
            {
                lapDetail.DataSource = dataDetail;
                subreportDetail.ReportSource = lapDetail;
            }

            XRSubreport subreportCheckItem = lapHeader.FindControl("subCheckItem", true) as XRSubreport;
            if (subreportCheckItem != null)
            {
                lapCheckItem.DataSource = dataCheckItem;
                subreportCheckItem.ReportSource = lapCheckItem;
            }

            XRSubreport subreportApprovalDeptLandscape = lapHeader.FindControl("subApprovalDept", true) as XRSubreport;
            if (subreportApprovalDeptLandscape != null)
            {
                lapApprovalDeptLandscape.DataSource = dataApprovalDept;
                subreportApprovalDeptLandscape.ReportSource = lapApprovalDeptLandscape;
            }

            List<string> items = dataHeader.Item.Split(',')
                                      .Select(x => x.Trim())
                                      .ToList();

            if (items.Contains("1"))
            {
                XRSubreport subreportToolingStatus = lapAll.FindControl("subReportTooling", true) as XRSubreport;
                if (subreportToolingStatus != null)
                {
                    var dataToolingStatus = service.GetPPAPToolingById(noPPAP, idStatus);
                    
                    lapToolingStatus.DataSource = dataToolingStatus;
                    subreportToolingStatus.ReportSource = lapToolingStatus;
                    subreportToolingStatus.ReportSource.Parameters["pCustId"].Value = dataHeader.CustID;
                    subreportToolingStatus.ReportSource.Parameters["pProject"].Value = dataHeader.ProjectName;
                }
            } else
            {
                lapAll.ghTooling.Visible = false;
            }

            if (items.Contains("2"))
            {
                XRSubreport subreportQualityStatus = lapAll.FindControl("subReportQuality", true) as XRSubreport;
                if (subreportQualityStatus != null)
                {
                    var dataQualityStatus = service.GetPPAPQualityById(noPPAP, idStatus);
                    
                    lapQualityStatus.DataSource = dataQualityStatus;
                    subreportQualityStatus.ReportSource = lapQualityStatus;
                    subreportQualityStatus.ReportSource.Parameters["pCustId"].Value = dataHeader.CustID;
                    subreportQualityStatus.ReportSource.Parameters["pProject"].Value = dataHeader.ProjectName;
                }
            }
            else
            {
                lapAll.ghQuality.Visible = false;
            }

            if (items.Contains("3"))
            {
                XRSubreport subreportPreparationStatus = lapAll.FindControl("subReportPreparation", true) as XRSubreport;
                if (subreportPreparationStatus != null)
                {
                    var dataPreparationStatus = service.GetPPAPPreparationById(noPPAP, idStatus);
                    
                    lapPreparationStatus.DataSource = dataPreparationStatus;
                    subreportPreparationStatus.ReportSource = lapPreparationStatus;
                    subreportPreparationStatus.ReportSource.Parameters["pCustId"].Value = dataHeader.CustID;
                    subreportPreparationStatus.ReportSource.Parameters["pProject"].Value = dataHeader.ProjectName;
                }
            }
            else
            {
                lapAll.ghPreparation.Visible = false;
            }

            if (items.Contains("4"))
            {
                XRSubreport subreportDocument = lapAll.FindControl("subReportDocument", true) as XRSubreport;
                if (subreportDocument != null)
                {
                    var dataDocument = service.GetPPAPRptDocumentById(noPPAP, idStatus);

                    lapDocument.DataSource = dataDocument;
                    subreportDocument.ReportSource = lapDocument;
                    subreportDocument.ReportSource.Parameters["pCustId"].Value = dataHeader.CustID;
                    subreportDocument.ReportSource.Parameters["pProject"].Value = dataHeader.ProjectName;
                }
            }
            else
            {
                lapAll.ghDocument.Visible = false;
            }

            if (items.Contains("5"))
            {
                XRSubreport subreportSupplier = lapAll.FindControl("subReportSupplier", true) as XRSubreport;
                if (subreportSupplier != null)
                {
                    var dataSupplier = service.GetPPAPSupplierById(noPPAP, idStatus);

                    lapSupplier.DataSource = dataSupplier;
                    subreportSupplier.ReportSource = lapSupplier;
                    subreportSupplier.ReportSource.Parameters["pCustId"].Value = dataHeader.CustID;
                    subreportSupplier.ReportSource.Parameters["pProject"].Value = dataHeader.ProjectName;
                }
            }
            else
            {
                lapAll.ghSupplier.Visible = false;
            }

            if (items.Contains("6"))
            {
                XRSubreport subreportPackingStatus = lapAll.FindControl("subReportPacking", true) as XRSubreport;
                if (subreportPackingStatus != null)
                {
                    var dataPackingStatus = service.GetPPAPPackingById(noPPAP, idStatus);
                    
                    lapPackingStatus.DataSource = dataPackingStatus;
                    subreportPackingStatus.ReportSource = lapPackingStatus;
                    subreportPackingStatus.ReportSource.Parameters["pCustId"].Value = dataHeader.CustID;
                    subreportPackingStatus.ReportSource.Parameters["pProject"].Value = dataHeader.ProjectName;
                }
            }
            else
            {
                lapAll.ghPacking.Visible = false;
            }

            if (items.Contains("7"))
            {
                XRSubreport subreportComparation = lapAll.FindControl("subReportComparation", true) as XRSubreport;
                if (subreportComparation != null)
                {
                    var dataComparation = service.GetPPAPComparationById(noPPAP, idStatus);

                    lapComparation.DataSource = dataComparation;
                    subreportComparation.ReportSource = lapComparation;
                    subreportComparation.ReportSource.Parameters["pCustId"].Value = dataHeader.CustID;
                    subreportComparation.ReportSource.Parameters["pProject"].Value = dataHeader.ProjectName;
                }
            }
            else
            {
                lapAll.ghComparation.Visible = false;
            }

            if (items.Contains("8"))
            {
                var dataPaintingHeader = service.GetPPAPPaintingById(noPPAP, idStatus);
                XRSubreport subreportPaintingDetail = lapPaintingHeader.FindControl("subDetail", true) as XRSubreport;
                if (dataPaintingHeader.Count > 0 && subreportPaintingDetail != null )
                {
                    var dataPaintingDetail = service.GetPPAPPaintingDetailById(noPPAP, idStatus);

                    lapPaintingHeader.DataSource = dataPaintingHeader;
                    subreportPaintingDetail.ReportSource = lapPaintingDetail;
                    subreportPaintingDetail.ReportSource.DataSource = dataPaintingDetail;

                    XRSubreport subreportPainting = lapAll.FindControl("subReportPainting", true) as XRSubreport;
                    subreportPainting.ReportSource = lapPaintingHeader;
                }
                else
                {
                    lapAll.ghPainting.Visible = false;
                }

                var dataAssembly = service.GetPPAPAssemblyById(noPPAP, idStatus);
                XRSubreport subreportAssembly = lapAll.FindControl("subReportAssembly", true) as XRSubreport;
                if (subreportAssembly != null && dataAssembly.Count > 0)
                {                  
                    lapAssembly.DataSource = dataAssembly;
                    subreportAssembly.ReportSource = lapAssembly;
                }
                else
                {
                    lapAll.ghAssembly.Visible = false;
                }
            }
            else
            {
                lapAll.ghPainting.Visible = false;
                lapAll.ghAssembly.Visible = false;
            }

            ASPxWebDocumentViewer1.OpenReport(lapAll);
        }
    }
}