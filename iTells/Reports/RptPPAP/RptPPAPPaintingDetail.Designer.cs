namespace iTells.Reports.RptPPAP
{
    partial class RptPPAPPaintingDetail
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
            this.XrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.XrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.XrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrActual = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSelisih = new DevExpress.XtraReports.UI.XRTableCell();
            this.XrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.pCode = new DevExpress.XtraReports.Parameters.Parameter();
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
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // XrTable1
            // 
            this.XrTable1.Dpi = 254F;
            this.XrTable1.Font = new System.Drawing.Font("Arial", 6.5F);
            this.XrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.XrTable1.Name = "XrTable1";
            this.XrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.XrTableRow1});
            this.XrTable1.SizeF = new System.Drawing.SizeF(1950F, 35F);
            this.XrTable1.StylePriority.UseFont = false;
            this.XrTable1.StylePriority.UseTextAlignment = false;
            this.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // XrTableRow1
            // 
            this.XrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.XrTableCell1,
            this.XrTableCell2,
            this.XrTableCell3,
            this.XrTableCell4,
            this.XrTableCell5,
            this.xrActual,
            this.xrSelisih,
            this.XrTableCell8});
            this.XrTableRow1.Dpi = 254F;
            this.XrTableRow1.Name = "XrTableRow1";
            this.XrTableRow1.Weight = 1D;
            // 
            // XrTableCell1
            // 
            this.XrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell1.Dpi = 254F;
            this.XrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Item]")});
            this.XrTableCell1.Multiline = true;
            this.XrTableCell1.Name = "XrTableCell1";
            this.XrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell1.StylePriority.UseBorders = false;
            this.XrTableCell1.Text = "XrTableCell1";
            this.XrTableCell1.Weight = 1.1999999694824219D;
            // 
            // XrTableCell2
            // 
            this.XrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.XrTableCell2.Dpi = 254F;
            this.XrTableCell2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SubItem]")});
            this.XrTableCell2.Multiline = true;
            this.XrTableCell2.Name = "XrTableCell2";
            this.XrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell2.StylePriority.UseBorders = false;
            this.XrTableCell2.StylePriority.UseTextAlignment = false;
            this.XrTableCell2.Text = "XrTableCell2";
            this.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.XrTableCell2.Weight = 1.1999999694824219D;
            // 
            // XrTableCell3
            // 
            this.XrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell3.Dpi = 254F;
            this.XrTableCell3.Multiline = true;
            this.XrTableCell3.Name = "XrTableCell3";
            this.XrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell3.StylePriority.UseBorders = false;
            this.XrTableCell3.Weight = 0.60000006103515624D;
            // 
            // XrTableCell4
            // 
            this.XrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell4.Dpi = 254F;
            this.XrTableCell4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProductName]")});
            this.XrTableCell4.Multiline = true;
            this.XrTableCell4.Name = "XrTableCell4";
            this.XrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell4.StylePriority.UseBorders = false;
            this.XrTableCell4.StylePriority.UseTextAlignment = false;
            this.XrTableCell4.Text = "XrTableCell4";
            this.XrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.XrTableCell4.Weight = 5.1000001307128242D;
            // 
            // XrTableCell5
            // 
            this.XrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell5.Dpi = 254F;
            this.XrTableCell5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Spec]")});
            this.XrTableCell5.Multiline = true;
            this.XrTableCell5.Name = "XrTableCell5";
            this.XrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell5.StylePriority.UseBorders = false;
            this.XrTableCell5.StylePriority.UseTextAlignment = false;
            this.XrTableCell5.Text = "XrTableCell5";
            this.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.XrTableCell5.Weight = 0.90000004916720922D;
            // 
            // xrActual
            // 
            this.xrActual.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrActual.Dpi = 254F;
            this.xrActual.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Actual]")});
            this.xrActual.Multiline = true;
            this.xrActual.Name = "xrActual";
            this.xrActual.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrActual.StylePriority.UseBorders = false;
            this.xrActual.StylePriority.UseTextAlignment = false;
            this.xrActual.Text = "xrActual";
            this.xrActual.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrActual.Weight = 0.90000004916720944D;
            // 
            // xrSelisih
            // 
            this.xrSelisih.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrSelisih.Dpi = 254F;
            this.xrSelisih.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Spec]-[Actual]")});
            this.xrSelisih.Multiline = true;
            this.xrSelisih.Name = "xrSelisih";
            this.xrSelisih.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrSelisih.StylePriority.UseBorders = false;
            this.xrSelisih.StylePriority.UseTextAlignment = false;
            this.xrSelisih.Text = "xrSelisih";
            this.xrSelisih.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrSelisih.TextFormatString = "{0:n2}";
            this.xrSelisih.Weight = 0.90000004916720944D;
            this.xrSelisih.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrSelisih_BeforePrint);
            // 
            // XrTableCell8
            // 
            this.XrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.XrTableCell8.Dpi = 254F;
            this.XrTableCell8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "([Spec]-[Actual]) / [Actual]")});
            this.XrTableCell8.Multiline = true;
            this.XrTableCell8.Name = "XrTableCell8";
            this.XrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.XrTableCell8.StylePriority.UseBorders = false;
            this.XrTableCell8.StylePriority.UseTextAlignment = false;
            this.XrTableCell8.Text = "XrTableCell8";
            this.XrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.XrTableCell8.TextFormatString = "{0:p2}";
            this.XrTableCell8.Weight = 0.90000004916720944D;
            // 
            // pCode
            // 
            this.pCode.AllowNull = true;
            this.pCode.Description = "Code";
            this.pCode.Name = "pCode";
            this.pCode.Visible = false;
            // 
            // RptPPAPPaintingDetail
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(85, 85, 85, 85);
            this.PageHeight = 2970;
            this.PageWidth = 4200;
            this.PaperKind = System.Drawing.Printing.PaperKind.A3;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pCode});
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this.XrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        internal DevExpress.XtraReports.UI.XRTable XrTable1;
        internal DevExpress.XtraReports.UI.XRTableRow XrTableRow1;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell1;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell2;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell3;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell4;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell5;
        internal DevExpress.XtraReports.UI.XRTableCell xrActual;
        internal DevExpress.XtraReports.UI.XRTableCell xrSelisih;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell8;
        private DevExpress.XtraReports.Parameters.Parameter pCode;
    }
}
