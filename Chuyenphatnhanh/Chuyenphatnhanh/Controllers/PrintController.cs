using Chuyenphatnhanh.Util;
using PdfRpt.Core.Contracts;
using PdfRpt.FluentInterface;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Drawing;
using PdfRpt.Core.Helper;

namespace Chuyenphatnhanh.Controllers
{
    public class PrintController : BaseController
    {
        // GET: Print
        public ActionResult Index()
        {
            return View();
        }
        public IPdfReportData gennrateRecipt(BillHdrTblForm bill, List<BillTblForm> list)
        {


            IPdfReportData document = new PdfReport().DocumentPreferences(doc =>
           {
               doc.RunDirection(PdfRunDirection.LeftToRight);
               doc.Orientation(PageOrientation.Landscape);
               doc.PageSize(PdfPageSize.A5);
               doc.DocumentMetadata(new DocumentMetadata { Author = "GiapPD1", Application = "PdfRpt", Keywords = "Phiếu gửi", Subject = "Phiếu gửiS", Title = "Phiếu gửi" });
           }).DefaultFonts(fonts =>
           {
               fonts.Path(Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\arial.ttf",
                                 Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\verdana.ttf");
           })
            .PagesFooter(footer =>
            {
                footer.XHtmlFooter(rptFooter =>
                {
                    rptFooter.PageFooterProperties(new XFooterBasicProperties
                    {
                        RunDirection = PdfRunDirection.LeftToRight,
                        ShowBorder = false,
                        PdfFont = footer.PdfFont,
                        TotalPagesCountTemplateHeight = 10,
                        TotalPagesCountTemplateWidth = 50
                    });
                    rptFooter.AddPageFooter(pageFooter =>
                    { 
                        var date = DateTime.Now.ToString("dd/MM/yyyy");
                        return string.Format(@"<table style='width=100%;font-size:9pt;font-family:tahoma;'>
														<tr> 
															<td  align='right'>{0}</td>
														 </tr>
												</table>",  date);
                    });
                });
            })
            .PagesHeader(header =>
            {

                header.XHtmlHeader(rptHeader =>
                {
                    header.CacheHeader(cache: true);
                    rptHeader.PageHeaderProperties(new XHeaderBasicProperties
                    {
                        RunDirection = PdfRunDirection.LeftToRight,
                        ShowBorder = false
                    });
                    rptHeader.AddPageHeader(pageHeader =>
                    {

                        Barcode128 code128 = new Barcode128();
                        code128.CodeType = Barcode.CODE128;
                        code128.ChecksumText = true;
                        code128.GenerateChecksum = true;
                        code128.StartStopText = true;
                        code128.Code = bill.BILL_HDR_ID;
                        Bitmap bm = new Bitmap(code128.CreateDrawingImage(Color.Black, Color.White));
                        //   
                        System.IO.FileStream fs = System.IO.File.Open(AppPath.ApplicationPath + "\\Pdf\\Images\\" + bill.BILL_HDR_ID + ".png", FileMode.Create);

                        bm.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                        fs.Close();


                        var CustNameFrom = bill.Cust_From_Name;
                        var CustNameTo = bill.Cust_To_Name;
                        var AddFrom = bill.AddressFrom;
                        var AddTo = bill.AddressTo;
                        var CustPhoneFrom = bill.Cust_From_Phone;
                        var CustPhoneTo = bill.Cust_To_Phone;
                        var photo = System.IO.Path.Combine(AppPath.ApplicationPath + "\\Pdf\\Images\\" + bill.BILL_HDR_ID + ".png");
                        var image = string.Format("<img {0} src='{1}' />", "style='display:block;'", photo);
                        logger.Info(image);
                        return string.Format(@"     <table style ='width: 100%;font-size:15pt;font-family:tahoma;'>
                                                        
                                                        <tr>
                                                            <td align='right'>{0}</td>
                                                        </tr>
                                                        <tr>
                                                            <td align='right'>{1}</td>
                                                        </tr>
                                                        <tr>
                                                            <td align='center'>{2}</td>
                                                        </tr>
                                                        <tr>
                                                            <td align='center'>-----------------------------------------------------------------------------------------------------------------------</td>
                                                        </tr>
                                                    </table>
                                                    <table style='width: 100%;font-size:9pt;font-family:tahoma;'>
                                                         
													    <tr>
														    <td align='left' width='1.5'>Người gửi: </td><td align='left' width='3.5'>{3}</td>
                                                            <td align='left' width='1.5'>Người nhận: </td><td align='left' width='3.5'>{4}</td>
													    </tr>
													    <tr>
														    <td align='left'>SĐT Người gửi: </td><td align='left'>{5}</td>
                                                            <td align='left'>SĐT Người nhận: </td><td align='left'>{6}</td>
													    </tr>
                                                        <tr>
														    <td align='left'>Địa chỉ Người gửi: </td><td align='left'>{7}</td>
                                                            <td align='left'>Địa chỉ Người nhận: </td><td align='left'>{8}</td>
													    </tr>
                                                        <tr>
														    <td align='left'>-</td><td align='left'>-</td>
                                                            <td align='left'>-</td><td align='left'>-</td>
													    </tr>
												    </table>", image, bill.BILL_HDR_ID, "PHIẾU GỬI", CustNameFrom, CustNameTo, CustPhoneFrom, CustPhoneTo, AddFrom, AddTo);
                    });
                    rptHeader.GroupHeaderProperties(new XHeaderBasicProperties
                    {
                        RunDirection = PdfRunDirection.LeftToRight,
                        ShowBorder = true,
                        SpacingBeforeTable = 15f
                    });
                    rptHeader.AddGroupHeader(groupHeader =>
                    {
                        return string.Format(@"");
                    });
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.ClassicTemplate);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                table.NumberOfDataRowsPerPage(20);
            })
            .MainTableDataSource(dataSource =>
            {
                dataSource.StronglyTypedList(list);
            })
            .MainTableSummarySettings(summarySettings =>
            {
                //summarySettings.OverallSummarySettings("Summary");
                //summarySettings.PreviousPageSummarySettings("Previous Page Summary");
                //summarySettings.PageSummarySettings("Page Summary");
            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("STT");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<BillTblForm>(x => x.NAME);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(2);
                    column.HeaderCell("Tên hàng hóa");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<BillTblForm>(x => x.WEIGHT);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Cân nặng");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<BillTblForm>(x => x.AMOUNT);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Giá vận chuyển");

                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<BillTblForm>(x => x.COMMENT);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("Ghi chú");
                });
            })
            .MainTableEvents(events =>
            {
                events.DataSourceIsEmpty(message: "There is no data available to display."); 
                events.CellCreated(args =>
                {
                    args.Cell.BasicProperties.CellPadding = 4f;
                });
                events.MainTableAdded(args =>
                {
                    var taxTable = new PdfGrid(2);  // Create a clone of the MainTable's structure  
                    taxTable.RunDirection = 2;
                    taxTable.SetWidths(new float[] { 5, 5 });
                    taxTable.WidthPercentage = 100f;
                    taxTable.SpacingBefore = 15f;

                    taxTable.AddSimpleRow(
                        (data, cellProperties) =>
                        {
                            data.Value = "Người gửi" +
                            "\n (Kí ghi rõ họ tên)";
                            cellProperties.ShowBorder = false;
                            cellProperties.PdfFont = args.PdfFont;
                        },
                        (data, cellProperties) =>
                        {
                            data.Value = "Nhân viên" +
                            "\n (Kí ghi rõ họ tên)";
                            cellProperties.ShowBorder = false;
                            cellProperties.PdfFont = args.PdfFont;
                        });
                    args.PdfDoc.Add(taxTable);
                });
            })
            .Export(export =>
            {
                export.ToExcel();
                export.ToCsv();
                export.ToXml();
            })
            .Generate(data => data.AsPdfFile(AppPath.ApplicationPath + "\\Pdf\\" + bill.BILL_HDR_ID + ".pdf"));


            return document;
        }

    }
}