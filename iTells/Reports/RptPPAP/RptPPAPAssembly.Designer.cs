namespace iTells.Reports.RptPPAP
{
    partial class RptPPAPAssembly
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
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.XrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.XrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.XrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.XrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.XrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.XrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.XrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.XrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.XrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.XrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.XrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XrTable1)).BeginInit();
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
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.XrTable1});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 35F;
            this.Detail.Name = "Detail";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.XrTable2,
            this.XrTable3});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 105F;
            this.GroupHeader1.KeepTogether = true;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // XrTable2
            // 
            this.XrTable2.Dpi = 254F;
            this.XrTable2.Font = new System.Drawing.Font("Arial", 6.5F, System.Drawing.FontStyle.Bold);
            this.XrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.XrTable2.Name = "XrTable2";
            this.XrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.XrTableRow2});
            this.XrTable2.SizeF = new System.Drawing.SizeF(900F, 35F);
            this.XrTable2.StylePriority.UseFont = false;
            // 
            // XrTableRow2
            // 
            this.XrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.XrTableCell4});
            this.XrTableRow2.Dpi = 254F;
            this.XrTableRow2.Name = "XrTableRow2";
            this.XrTableRow2.Weight = 1D;
            // 
            // XrTableCell4
            // 
            this.XrTableCell4.BackColor = System.Drawing.Color.Yellow;
            this.XrTableCell4.Dpi = 254F;
            this.XrTableCell4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PartName]")});
            this.XrTableCell4.Multiline = true;
            this.XrTableCell4.Name = "XrTableCell4";
            this.XrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell4.StylePriority.UseBackColor = false;
            this.XrTableCell4.StylePriority.UseTextAlignment = false;
            this.XrTableCell4.Text = "XrTableCell4";
            this.XrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.XrTableCell4.Weight = 1.5D;
            // 
            // XrTable3
            // 
            this.XrTable3.Dpi = 254F;
            this.XrTable3.Font = new System.Drawing.Font("Arial", 6.5F, System.Drawing.FontStyle.Bold);
            this.XrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 35F);
            this.XrTable3.Name = "XrTable3";
            this.XrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.XrTableRow3,
            this.XrTableRow4});
            this.XrTable3.SizeF = new System.Drawing.SizeF(2100F, 70F);
            this.XrTable3.StylePriority.UseFont = false;
            this.XrTable3.StylePriority.UseTextAlignment = false;
            this.XrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // XrTableRow3
            // 
            this.XrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.XrTableCell5,
            this.XrTableCell6,
            this.XrTableCell15});
            this.XrTableRow3.Dpi = 254F;
            this.XrTableRow3.Name = "XrTableRow3";
            this.XrTableRow3.Weight = 1D;
            // 
            // XrTableCell5
            // 
            this.XrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell5.Dpi = 254F;
            this.XrTableCell5.Multiline = true;
            this.XrTableCell5.Name = "XrTableCell5";
            this.XrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell5.StylePriority.UseBorders = false;
            this.XrTableCell5.StylePriority.UseTextAlignment = false;
            this.XrTableCell5.Text = "ASSEMBLY";
            this.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.XrTableCell5.Weight = 3.1496062992125986D;
            // 
            // XrTableCell6
            // 
            this.XrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell6.Dpi = 254F;
            this.XrTableCell6.Multiline = true;
            this.XrTableCell6.Name = "XrTableCell6";
            this.XrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell6.StylePriority.UseBorders = false;
            this.XrTableCell6.Weight = 3.5433070866141727D;
            // 
            // XrTableCell15
            // 
            this.XrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell15.Dpi = 254F;
            this.XrTableCell15.Multiline = true;
            this.XrTableCell15.Name = "XrTableCell15";
            this.XrTableCell15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell15.StylePriority.UseBorders = false;
            this.XrTableCell15.Text = "C/T (s)";
            this.XrTableCell15.Weight = 1.5748031496062991D;
            // 
            // XrTableRow4
            // 
            this.XrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.XrTableCell8,
            this.XrTableCell9,
            this.XrTableCell10,
            this.XrTableCell12,
            this.XrTableCell14,
            this.XrTableCell16});
            this.XrTableRow4.Dpi = 254F;
            this.XrTableRow4.Name = "XrTableRow4";
            this.XrTableRow4.Weight = 1D;
            // 
            // XrTableCell8
            // 
            this.XrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell8.Dpi = 254F;
            this.XrTableCell8.Multiline = true;
            this.XrTableCell8.Name = "XrTableCell8";
            this.XrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell8.StylePriority.UseBorders = false;
            this.XrTableCell8.StylePriority.UseTextAlignment = false;
            this.XrTableCell8.Text = "COMPONENT PART";
            this.XrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.XrTableCell8.Weight = 3.1496062992125982D;
            // 
            // XrTableCell9
            // 
            this.XrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell9.Dpi = 254F;
            this.XrTableCell9.Multiline = true;
            this.XrTableCell9.Name = "XrTableCell9";
            this.XrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell9.StylePriority.UseBorders = false;
            this.XrTableCell9.Text = "QTY";
            this.XrTableCell9.Weight = 0.787402055394931D;
            // 
            // XrTableCell10
            // 
            this.XrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell10.Dpi = 254F;
            this.XrTableCell10.Multiline = true;
            this.XrTableCell10.Name = "XrTableCell10";
            this.XrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell10.StylePriority.UseBorders = false;
            this.XrTableCell10.Text = "PROCESS NAME";
            this.XrTableCell10.Weight = 1.9685036967119833D;
            // 
            // XrTableCell12
            // 
            this.XrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell12.Dpi = 254F;
            this.XrTableCell12.Multiline = true;
            this.XrTableCell12.Name = "XrTableCell12";
            this.XrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell12.StylePriority.UseBorders = false;
            this.XrTableCell12.Text = "MAN POWER";
            this.XrTableCell12.Weight = 0.78740157480314965D;
            // 
            // XrTableCell14
            // 
            this.XrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell14.Dpi = 254F;
            this.XrTableCell14.Multiline = true;
            this.XrTableCell14.Name = "XrTableCell14";
            this.XrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell14.StylePriority.UseBorders = false;
            this.XrTableCell14.Text = "ACT";
            this.XrTableCell14.Weight = 0.78740157480314965D;
            // 
            // XrTableCell16
            // 
            this.XrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell16.Dpi = 254F;
            this.XrTableCell16.Multiline = true;
            this.XrTableCell16.Name = "XrTableCell16";
            this.XrTableCell16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell16.StylePriority.UseBorders = false;
            this.XrTableCell16.Text = "SPEC";
            this.XrTableCell16.Weight = 0.78740133450725858D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.XrLabel1});
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 50F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // XrLabel1
            // 
            this.XrLabel1.Dpi = 254F;
            this.XrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.XrLabel1.Multiline = true;
            this.XrLabel1.Name = "XrLabel1";
            this.XrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrLabel1.SizeF = new System.Drawing.SizeF(254F, 50F);
            // 
            // XrTable1
            // 
            this.XrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTable1.Dpi = 254F;
            this.XrTable1.Font = new System.Drawing.Font("Arial", 6.5F);
            this.XrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.XrTable1.Name = "XrTable1";
            this.XrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.XrTableRow1});
            this.XrTable1.SizeF = new System.Drawing.SizeF(2100F, 35F);
            this.XrTable1.StylePriority.UseBorders = false;
            this.XrTable1.StylePriority.UseFont = false;
            this.XrTable1.StylePriority.UseTextAlignment = false;
            this.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // XrTableRow1
            // 
            this.XrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.XrTableCell1,
            this.XrTableCell2,
            this.XrTableCell3,
            this.XrTableCell7,
            this.XrTableCell11,
            this.XrTableCell13});
            this.XrTableRow1.Dpi = 254F;
            this.XrTableRow1.Name = "XrTableRow1";
            this.XrTableRow1.Weight = 1D;
            // 
            // XrTableCell1
            // 
            this.XrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell1.Dpi = 254F;
            this.XrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ComponentPart]")});
            this.XrTableCell1.Multiline = true;
            this.XrTableCell1.Name = "XrTableCell1";
            this.XrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell1.StylePriority.UseBorders = false;
            this.XrTableCell1.Text = "XrTableCell1";
            this.XrTableCell1.Weight = 2.7559057495323138D;
            // 
            // XrTableCell2
            // 
            this.XrTableCell2.Dpi = 254F;
            this.XrTableCell2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Qty]")});
            this.XrTableCell2.Multiline = true;
            this.XrTableCell2.Name = "XrTableCell2";
            this.XrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell2.Text = "XrTableCell2";
            this.XrTableCell2.Weight = 0.68897642236462187D;
            // 
            // XrTableCell3
            // 
            this.XrTableCell3.Dpi = 254F;
            this.XrTableCell3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProcessName]")});
            this.XrTableCell3.Multiline = true;
            this.XrTableCell3.Name = "XrTableCell3";
            this.XrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell3.Text = "XrTableCell3";
            this.XrTableCell3.Weight = 1.7224411004305975D;
            // 
            // XrTableCell7
            // 
            this.XrTableCell7.Dpi = 254F;
            this.XrTableCell7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ManPower]")});
            this.XrTableCell7.Multiline = true;
            this.XrTableCell7.Name = "XrTableCell7";
            this.XrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell7.Text = "XrTableCell7";
            this.XrTableCell7.Weight = 0.688976431161136D;
            // 
            // XrTableCell11
            // 
            this.XrTableCell11.Dpi = 254F;
            this.XrTableCell11.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Actual]")});
            this.XrTableCell11.Multiline = true;
            this.XrTableCell11.Name = "XrTableCell11";
            this.XrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell11.Text = "XrTableCell11";
            this.XrTableCell11.Weight = 0.68897643738304915D;
            // 
            // XrTableCell13
            // 
            this.XrTableCell13.Dpi = 254F;
            this.XrTableCell13.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Spec]")});
            this.XrTableCell13.Multiline = true;
            this.XrTableCell13.Name = "XrTableCell13";
            this.XrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell13.Text = "XrTableCell13";
            this.XrTableCell13.Weight = 0.68897633225358956D;
            // 
            // RptPPAPAssembly
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.GroupHeader1,
            this.GroupFooter1});
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
            ((System.ComponentModel.ISupportInitialize)(this.XrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        internal DevExpress.XtraReports.UI.XRTable XrTable2;
        internal DevExpress.XtraReports.UI.XRTableRow XrTableRow2;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell4;
        internal DevExpress.XtraReports.UI.XRTable XrTable3;
        internal DevExpress.XtraReports.UI.XRTableRow XrTableRow3;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell5;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell6;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell15;
        internal DevExpress.XtraReports.UI.XRTableRow XrTableRow4;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell8;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell9;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell10;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell12;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell14;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell16;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        internal DevExpress.XtraReports.UI.XRLabel XrLabel1;
        internal DevExpress.XtraReports.UI.XRTable XrTable1;
        internal DevExpress.XtraReports.UI.XRTableRow XrTableRow1;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell1;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell2;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell3;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell7;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell11;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell13;
    }
}
