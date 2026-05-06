using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Shape;
using BusinessObjects.PPAP;

namespace iTells.Reports.RptPPAP
{
    public partial class RptPPAPDocument : DevExpress.XtraReports.UI.XtraReport
    {
        public RptPPAPDocument()
        {
            InitializeComponent();
        }

        private void RptPPAPDocument_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var datasource = this.DataSource as List<PPAPRptDocumentModel>;
            if (datasource == null) return;

            var listPart = datasource
                .Select(r => new {
                    r.Code,
                    r.Numbering,
                    r.PartNumber,
                    r.PartName,
                    r.InvtID
                })
                .Distinct()
                .ToList();

            int totalPart = listPart.Count;
            float columnWidth = cellItemPart.WidthF / totalPart;
            float columnHeight = cellItemPart.HeightF;

            // Baris ke-2 (Numbering)
            XRTableRow headerRow2 = new XRTableRow();
            AddBaseCells(headerRow2);
            foreach (var part in listPart)
            {
                headerRow2.Cells.Add(new XRTableCell
                {
                    Borders = BorderSide.Right | BorderSide.Bottom,
                    Text = part.Numbering,
                    WidthF = columnWidth,
                    TextAlignment = TextAlignment.MiddleCenter
                });
            }
            AddEndCells(headerRow2);
            xrTableHeader.Rows.Add(headerRow2);

            // Baris ke-3 (Part Name)
            XRTableRow headerRow3 = new XRTableRow();
            AddBaseCells(headerRow3);
            foreach (var part in listPart)
            {
                headerRow3.Cells.Add(new XRTableCell
                {
                    Borders = BorderSide.Right | BorderSide.Bottom,
                    Text = part.PartName.ToString(),
                    WidthF = columnWidth,
                    TextAlignment = TextAlignment.MiddleCenter
                });
            }
            AddEndCells(headerRow3);
            xrTableHeader.Rows.Add(headerRow3);

            // Baris ke-4 (Part Number)
            XRTableRow headerRow4 = new XRTableRow();
            AddBaseCells(headerRow4);
            foreach (var part in listPart)
            {
                headerRow4.Cells.Add(new XRTableCell
                {
                    Borders = BorderSide.Right | BorderSide.Bottom,
                    Text = part.PartNumber.ToString(),
                    WidthF = columnWidth,
                    TextAlignment = TextAlignment.MiddleCenter
                });
            }
            AddEndCells(headerRow4);
            xrTableHeader.Rows.Add(headerRow4);

            // Baris ke-5 (Invt ID)
            XRTableRow headerRow5 = new XRTableRow();
            AddBaseCells(headerRow5);
            foreach (var part in listPart)
            {
                headerRow5.Cells.Add(new XRTableCell
                {
                    Borders = BorderSide.Right | BorderSide.Bottom,
                    Text = string.IsNullOrWhiteSpace(part.InvtID) ? "-" : part.InvtID.Trim(),
                    WidthF = columnWidth,
                    TextAlignment = TextAlignment.MiddleCenter
                });
            }
            AddEndCells(headerRow5);
            xrTableHeader.Rows.Add(headerRow5);

            // --- DETAIL ITEMS ---
            var listItems = datasource
                .Select(r => new {
                    r.CodeItem,
                    r.Item,
                    No = r.No.ToString() ?? "",
                    PIC = r.PIC ?? "",
                    r.IsEdit
        })
                .Distinct()
                .ToList();

            foreach (var item in listItems)
            {
                FontStyle style = item.IsEdit ? FontStyle.Regular : FontStyle.Bold;
                XRTableRow row = new XRTableRow();

                row.Cells.Add(new XRTableCell { Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom, Text = item.No, WidthF = cellNo.WidthF, TextAlignment = TextAlignment.MiddleCenter });
                row.Cells.Add(new XRTableCell { Borders = BorderSide.Right | BorderSide.Bottom, Font = new Font("Arial", 6, style), Text = item.Item, WidthF = cellItem.WidthF, TextAlignment = TextAlignment.MiddleLeft });

                foreach (var part in listPart)
                {
                    var statusVal = datasource
                        .Where(r => r.CodeItem == item.CodeItem && r.Code == part.Code)
                        .Select(r => r.Status)
                        .FirstOrDefault();

                    row.Cells.Add(CreateCellWithDynamicShape(statusVal, columnWidth, columnHeight));
                }

                AddEndCells(row);
                xrTableHeader.Rows.Add(row);
            }
        }

        private XRTableCell CreateCellWithDynamicShape(object value, float width, float height)
        {
            XRTableCell cell = new XRTableCell()
            {
                Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom,
                HeightF = height,
                TextAlignment = TextAlignment.MiddleCenter,
                WidthF = width
            };

            string textValue = (value != null && value != DBNull.Value) ? value.ToString().Trim().ToUpper() : "";

            XRShape shape = new XRShape()
            {
                Borders = BorderSide.None,
                Padding = new PaddingInfo(1, 0, 1, 0),
                SizeF = new SizeF(8, 8),
                Visible = true
            };

            switch (textValue)
            {
                case "O":
                    shape.Shape = new ShapeEllipse();
                    shape.ForeColor = Color.White;
                    cell.BackColor = Color.Green;
                    break;
                case "PROGRESS":
                    shape.Shape = new ShapePolygon() { NumberOfSides = 3 };
                    shape.ForeColor = Color.Black;
                    shape.Angle = 0;
                    cell.BackColor = Color.Yellow;
                    break;
                case "X":
                    shape.Shape = new ShapeCross() { HorizontalLineWidth = 1, VerticalLineWidth = 1 };
                    shape.ForeColor = Color.White;
                    shape.Angle = 45;
                    cell.BackColor = Color.Red;
                    break;
                case "NR":
                    cell.BackColor = Color.DarkGray;
                    cell.Font = new Font("Arial", 6, FontStyle.Regular);
                    cell.ForeColor = Color.White;
                    shape.Visible = false;
                    break;
                default:
                    shape.Visible = false;
                    break;
            }

            if (shape.Visible)
            {
                cell.Controls.Add(shape);
            }
            else
            {
                cell.Text = textValue;
            }

            cell.BeforePrint += (s, e) => {
                shape.LocationF = new PointF((cell.WidthF - shape.WidthF) / 2, 4);
            };

            return cell;
        }

        private void AddBaseCells(XRTableRow row)
        {
            row.Cells.Add(new XRTableCell() { Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom, WidthF = cellNo.WidthF });
            row.Cells.Add(new XRTableCell() { Borders = BorderSide.Right | BorderSide.Bottom, WidthF = cellItem.WidthF });
        }

        private void AddEndCells(XRTableRow row)
        {
            row.Cells.Add(new XRTableCell() { Borders = BorderSide.Right | BorderSide.Bottom, WidthF = cellRemark.WidthF });
            row.Cells.Add(new XRTableCell() { Borders = BorderSide.Right | BorderSide.Bottom, WidthF = cellDueDate.WidthF });
        }
    }
}
