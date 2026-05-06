namespace iTells.Reports.RptPPAP
{
    partial class RptPPAPAll
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.ghHeader = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.subReportHeader = new DevExpress.XtraReports.UI.XRSubreport();
            this.ghTooling = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.subReportTooling = new DevExpress.XtraReports.UI.XRSubreport();
            this.ghQuality = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.SubReportQuality = new DevExpress.XtraReports.UI.XRSubreport();
            this.ghPreparation = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.subReportPreparation = new DevExpress.XtraReports.UI.XRSubreport();
            this.ghDocument = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.subReportDocument = new DevExpress.XtraReports.UI.XRSubreport();
            this.ghSupplier = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.subReportSupplier = new DevExpress.XtraReports.UI.XRSubreport();
            this.ghPacking = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.subReportPacking = new DevExpress.XtraReports.UI.XRSubreport();
            this.ghComparation = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.subReportComparation = new DevExpress.XtraReports.UI.XRSubreport();
            this.ghPainting = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.subReportPainting = new DevExpress.XtraReports.UI.XRSubreport();
            this.ghAssembly = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.subReportAssembly = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 85F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 85F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            // 
            // ghHeader
            // 
            this.ghHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subReportHeader});
            this.ghHeader.Dpi = 254F;
            this.ghHeader.HeightF = 58.42F;
            this.ghHeader.Level = 9;
            this.ghHeader.Name = "ghHeader";
            this.ghHeader.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // subReportHeader
            // 
            this.subReportHeader.Dpi = 254F;
            this.subReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subReportHeader.Name = "subReportHeader";
            this.subReportHeader.SizeF = new System.Drawing.SizeF(4030F, 58.42F);
            // 
            // ghTooling
            // 
            this.ghTooling.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subReportTooling});
            this.ghTooling.Dpi = 254F;
            this.ghTooling.HeightF = 58.42F;
            this.ghTooling.Level = 8;
            this.ghTooling.Name = "ghTooling";
            this.ghTooling.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // subReportTooling
            // 
            this.subReportTooling.Dpi = 254F;
            this.subReportTooling.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subReportTooling.Name = "subReportTooling";
            this.subReportTooling.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pCustId", null, "CustID"));
            this.subReportTooling.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pProject", null, "ProjectName"));
            this.subReportTooling.SizeF = new System.Drawing.SizeF(4030F, 58.42F);
            // 
            // ghQuality
            // 
            this.ghQuality.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.SubReportQuality});
            this.ghQuality.Dpi = 254F;
            this.ghQuality.HeightF = 58.42F;
            this.ghQuality.Level = 7;
            this.ghQuality.Name = "ghQuality";
            this.ghQuality.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // SubReportQuality
            // 
            this.SubReportQuality.Dpi = 254F;
            this.SubReportQuality.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.SubReportQuality.Name = "SubReportQuality";
            this.SubReportQuality.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pCustId", null, "CustID"));
            this.SubReportQuality.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pProject", null, "ProjectName"));
            this.SubReportQuality.SizeF = new System.Drawing.SizeF(4030F, 58.42F);
            // 
            // ghPreparation
            // 
            this.ghPreparation.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subReportPreparation});
            this.ghPreparation.Dpi = 254F;
            this.ghPreparation.HeightF = 58.42F;
            this.ghPreparation.Level = 6;
            this.ghPreparation.Name = "ghPreparation";
            this.ghPreparation.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // subReportPreparation
            // 
            this.subReportPreparation.Dpi = 254F;
            this.subReportPreparation.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subReportPreparation.Name = "subReportPreparation";
            this.subReportPreparation.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pCustId", null, "CustID"));
            this.subReportPreparation.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pProject", null, "ProjectName"));
            this.subReportPreparation.SizeF = new System.Drawing.SizeF(4030F, 58.42F);
            // 
            // ghDocument
            // 
            this.ghDocument.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subReportDocument});
            this.ghDocument.Dpi = 254F;
            this.ghDocument.HeightF = 58.42F;
            this.ghDocument.Level = 5;
            this.ghDocument.Name = "ghDocument";
            this.ghDocument.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // subReportDocument
            // 
            this.subReportDocument.Dpi = 254F;
            this.subReportDocument.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subReportDocument.Name = "subReportDocument";
            this.subReportDocument.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pNPWO", null, "NoNPWO"));
            this.subReportDocument.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pCustId", null, "CustID"));
            this.subReportDocument.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pProject", null, "ProjectName"));
            this.subReportDocument.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pMassProduction", null, "MassproductionDate"));
            this.subReportDocument.SizeF = new System.Drawing.SizeF(4030F, 58.42F);
            // 
            // ghSupplier
            // 
            this.ghSupplier.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subReportSupplier});
            this.ghSupplier.Dpi = 254F;
            this.ghSupplier.HeightF = 58.42F;
            this.ghSupplier.Level = 4;
            this.ghSupplier.Name = "ghSupplier";
            this.ghSupplier.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // subReportSupplier
            // 
            this.subReportSupplier.Dpi = 254F;
            this.subReportSupplier.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subReportSupplier.Name = "subReportSupplier";
            this.subReportSupplier.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pCustId", null, "CustID"));
            this.subReportSupplier.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pProject", null, "ProjectName"));
            this.subReportSupplier.SizeF = new System.Drawing.SizeF(4030F, 58.42F);
            // 
            // ghPacking
            // 
            this.ghPacking.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subReportPacking});
            this.ghPacking.Dpi = 254F;
            this.ghPacking.HeightF = 58.42F;
            this.ghPacking.Level = 3;
            this.ghPacking.Name = "ghPacking";
            this.ghPacking.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // subReportPacking
            // 
            this.subReportPacking.Dpi = 254F;
            this.subReportPacking.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subReportPacking.Name = "subReportPacking";
            this.subReportPacking.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pCustId", null, "CustID"));
            this.subReportPacking.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pProject", null, "ProjectName"));
            this.subReportPacking.SizeF = new System.Drawing.SizeF(4030F, 58.42F);
            // 
            // ghComparation
            // 
            this.ghComparation.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subReportComparation});
            this.ghComparation.Dpi = 254F;
            this.ghComparation.HeightF = 58.42F;
            this.ghComparation.Level = 2;
            this.ghComparation.Name = "ghComparation";
            this.ghComparation.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // subReportComparation
            // 
            this.subReportComparation.Dpi = 254F;
            this.subReportComparation.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subReportComparation.Name = "subReportComparation";
            this.subReportComparation.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pCustId", null, "CustID"));
            this.subReportComparation.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pProject", null, "ProjectName"));
            this.subReportComparation.SizeF = new System.Drawing.SizeF(4030F, 58.42F);
            // 
            // ghPainting
            // 
            this.ghPainting.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subReportPainting});
            this.ghPainting.Dpi = 254F;
            this.ghPainting.HeightF = 58.42F;
            this.ghPainting.Level = 1;
            this.ghPainting.Name = "ghPainting";
            // 
            // subReportPainting
            // 
            this.subReportPainting.Dpi = 254F;
            this.subReportPainting.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subReportPainting.Name = "subReportPainting";
            this.subReportPainting.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pCustId", null, "CustID"));
            this.subReportPainting.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("pProject", null, "ProjectName"));
            this.subReportPainting.SizeF = new System.Drawing.SizeF(4030F, 58.42F);
            // 
            // ghAssembly
            // 
            this.ghAssembly.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subReportAssembly});
            this.ghAssembly.Dpi = 254F;
            this.ghAssembly.HeightF = 58.42F;
            this.ghAssembly.Name = "ghAssembly";
            // 
            // subReportAssembly
            // 
            this.subReportAssembly.Dpi = 254F;
            this.subReportAssembly.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subReportAssembly.Name = "subReportAssembly";
            this.subReportAssembly.SizeF = new System.Drawing.SizeF(4030F, 58.42F);
            // 
            // RptPPAPAll
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ghHeader,
            this.ghTooling,
            this.ghQuality,
            this.ghPreparation,
            this.ghDocument,
            this.ghSupplier,
            this.ghPacking,
            this.ghComparation,
            this.ghPainting,
            this.ghAssembly});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(85, 85, 85, 85);
            this.PageHeight = 2970;
            this.PageWidth = 4200;
            this.PaperKind = System.Drawing.Printing.PaperKind.A3;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.GroupHeaderBand ghHeader;
        private DevExpress.XtraReports.UI.XRSubreport subReportHeader;
        internal DevExpress.XtraReports.UI.XRSubreport subReportTooling;
        internal DevExpress.XtraReports.UI.XRSubreport SubReportQuality;
        internal DevExpress.XtraReports.UI.XRSubreport subReportPreparation;
        internal DevExpress.XtraReports.UI.XRSubreport subReportDocument;
        internal DevExpress.XtraReports.UI.XRSubreport subReportSupplier;
        internal DevExpress.XtraReports.UI.XRSubreport subReportPacking;
        internal DevExpress.XtraReports.UI.XRSubreport subReportComparation;
        internal DevExpress.XtraReports.UI.XRSubreport subReportPainting;
        internal DevExpress.XtraReports.UI.XRSubreport subReportAssembly;
        public DevExpress.XtraReports.UI.GroupHeaderBand ghTooling;
        public DevExpress.XtraReports.UI.GroupHeaderBand ghQuality;
        public DevExpress.XtraReports.UI.GroupHeaderBand ghPreparation;
        public DevExpress.XtraReports.UI.GroupHeaderBand ghDocument;
        public DevExpress.XtraReports.UI.GroupHeaderBand ghSupplier;
        public DevExpress.XtraReports.UI.GroupHeaderBand ghPacking;
        public DevExpress.XtraReports.UI.GroupHeaderBand ghComparation;
        public DevExpress.XtraReports.UI.GroupHeaderBand ghPainting;
        public DevExpress.XtraReports.UI.GroupHeaderBand ghAssembly;
    }
}
