using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;

namespace iGYMMM
{
    public static class XLS_Creator
    {
        /*
        #region Clients Reports
        public static void RepoClients(List<ClntView1> LCV1)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
                                  // Export headers
                                  // Excel indexes start with [1,1]
            Type t = LCV1[0].GetType();
            var props = t.GetProperties();
            foreach (var prop in props)
                if (prop.Name != "Slctd")
                    ExcelSheet.Cells[RowIndex, ColumnIndex++] = prop.Name;

            // Export data
            foreach (var item in LCV1)
            {
                ColumnIndex = 1;
                RowIndex++;
                foreach (var prop in props)
                    if (prop.Name != "Slctd")
                    {
                        if (prop.Name == "LastBuyDate" || prop.Name == "LastPayDate")
                            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + ((long)prop.GetValue(item)).Long_ToDate_YYYYMMDD();
                        else
                        {
                            string tmp = prop.GetValue(item) == null ? "" : prop.GetValue(item).ToString();
                            if (tmp.Length > 0)
                                ExcelSheet.Cells[RowIndex, ColumnIndex++] = prop.GetValue(item).ToString().Substring(0, 1) == "0" ? "'" + prop.GetValue(item) : prop.GetValue(item);
                            else
                                ColumnIndex++;
                        }
                    }
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + props.Count() - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LCV1.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LCV1.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;

            //// Cleanup and release objects
            //ExcelSheet = null;
            //ExcelBook = null;
            //if (ExcelApp != null && Marshal.IsComObject(ExcelApp))
            //    Marshal.ReleaseComObject(ExcelApp);
            ////ExcelApp = null;
            ////ExcelApp.Quit();
        }
        #endregion Clients Reports

        //#region Client Transactions
        //public static void ClientTranzSales(List<Transaction> LTs, Client client, string ReportName, decimal TotalAmount)
        //{
        //    // https://forum.pdfsharp.net/
        //    // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
        //    // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
        //    // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


        //    //Create a MigraDoc document
        //    Document document = new Document();
        //    document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
        //    Section section = document.AddSection();
        //    // Create TextFrame
        //    TextFrame tf = section.AddTextFrame();
        //    tf.Left = ShapePosition.Left;
        //    tf.Top = ShapePosition.Top;
        //    tf.Height = Unit.FromCentimeter(0.46);
        //    //tf.RelativeHorizontal = RelativeHorizontal.Margin;
        //    tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
        //    // Create Paragraph
        //    Paragraph paragraph = section.AddParagraph();
        //    paragraph.Format.Font.Size = 12;
        //    paragraph.Format.Font.Bold = true;
        //    paragraph.Format.Alignment = ParagraphAlignment.Center;
        //    paragraph.AddText(ReportName.ReverseString());
        //    paragraph.AddLineBreak();

        //    paragraph = section.AddParagraph();
        //    paragraph.Format.Font.Size = 12;
        //    paragraph.Format.Font.Bold = true;
        //    paragraph.Format.Alignment = ParagraphAlignment.Right;
        //    paragraph.AddText(client.FullName.ReverseString());

        //    paragraph = section.AddParagraph();
        //    paragraph.Format.Font.Size = 12;
        //    //paragraph.Format.Font.Bold = true;
        //    paragraph.Format.Alignment = ParagraphAlignment.Right;
        //    paragraph.AddText(client.GetTelephons());
        //    paragraph.AddLineBreak();

        //    Table table = section.AddTable();
        //    table.Format.Alignment = ParagraphAlignment.Center;
        //    Column col = table.AddColumn(Unit.FromCentimeter(4.3));
        //    col = table.AddColumn(Unit.FromCentimeter(2.3));
        //    col = table.AddColumn(Unit.FromCentimeter(2.3));
        //    col = table.AddColumn(Unit.FromCentimeter(2.3));
        //    col = table.AddColumn(Unit.FromCentimeter(4.3));
        //    col = table.AddColumn(Unit.FromCentimeter(2.1));
        //    col = table.AddColumn(Unit.FromCentimeter(2.4));

        //    // Create the header of the table
        //    Row row = table.AddRow();
        //    row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.Format.Font.Bold = true;

        //    row.Cells[0].AddParagraph("הערות".ReverseString());
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[1].AddParagraph("יתרה".ReverseString());
        //    row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[2].AddParagraph("סך תשלום".ReverseString());
        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[3].AddParagraph("סך מכירה".ReverseString());
        //    row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[4].AddParagraph("תאור חומר".ReverseString());
        //    row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[5].AddParagraph("כמות".ReverseString());
        //    row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[6].AddParagraph("תאריך".ReverseString());
        //    row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[6].VerticalAlignment = VerticalAlignment.Center;

        //    DictConstsVars DCV = new DictConstsVars();
        //    foreach (var item in LTs)
        //    {
        //        row = table.AddRow();
        //        row.Borders.Bottom.Width = 0.164;
        //        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[6].Format.Alignment = ParagraphAlignment.Right;

        //        row.Cells[0].AddParagraph(item.Descrp.ReverseString());
        //        row.Cells[1].AddParagraph(item.SubTotal.ToString("0.00"));
        //        row.Cells[2].AddParagraph(item.DepoTotal.ToString("0.00"));
        //        row.Cells[3].AddParagraph(item.Total.ToString("0.00"));
        //        var CV = DCV.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.MatName);
        //        if (CV != null)
        //            row.Cells[4].AddParagraph(CV.HebConst.ReverseString());
        //        else
        //            row.Cells[4].AddParagraph(item.MatName.ReverseString());
        //        row.Cells[5].AddParagraph(item.QTY.ToString("0.00"));
        //        if (item.Date1 != 0)
        //            row.Cells[6].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD().PrepareOutput(30));
        //    }

        //    paragraph.AddLineBreak();
        //    paragraph = section.AddParagraph();
        //    paragraph.Format.Font.Size = 12;
        //    paragraph.Format.Font.Bold = true;
        //    paragraph.Format.Alignment = ParagraphAlignment.Right;
        //    paragraph.AddText(TotalAmount.ToString("0.00") + " חוקלה תרתי");

        //    if (!Directory.Exists(Properties.Settings.Default.PDFPath))
        //    {
        //        Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
        //        Thread.Sleep(1000);
        //    }
        //    PdfDocumentRenderer render = new PdfDocumentRenderer(true)
        //    {
        //        Document = document
        //    };
        //    render.RenderDocument();
        //    string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
        //    render.PdfDocument.Save(filename);
        //    Process.Start(filename);
        //}
        //public static void ClientTranzDepos(List<Deposit> LDs, Client client, string ReportName, decimal TotalAmount)
        //{
        //    // https://forum.pdfsharp.net/
        //    // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
        //    // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
        //    // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


        //    //Create a MigraDoc document
        //    Document document = new Document();
        //    document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
        //    Section section = document.AddSection();
        //    // Create TextFrame
        //    TextFrame tf = section.AddTextFrame();
        //    tf.Left = ShapePosition.Left;
        //    tf.Top = ShapePosition.Top;
        //    tf.Height = Unit.FromCentimeter(0.46);
        //    //tf.RelativeHorizontal = RelativeHorizontal.Margin;
        //    tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
        //    // Create Paragraph
        //    Paragraph paragraph = section.AddParagraph();
        //    paragraph.Format.Font.Size = 12;
        //    paragraph.Format.Font.Bold = true;
        //    paragraph.Format.Alignment = ParagraphAlignment.Center;
        //    paragraph.AddText(ReportName.ReverseString());

        //    paragraph = section.AddParagraph();
        //    paragraph.Format.Font.Size = 12;
        //    paragraph.Format.Font.Bold = true;
        //    paragraph.Format.Alignment = ParagraphAlignment.Center;
        //    paragraph.AddText(client.FullName.ReverseString());
        //    paragraph.AddLineBreak();

        //    paragraph = section.AddParagraph();
        //    paragraph.Format.Font.Size = 12;
        //    paragraph.Format.Font.Bold = true;
        //    paragraph.Format.Alignment = ParagraphAlignment.Right;
        //    paragraph.AddText(TotalAmount.ToString("0.00") + " םימולשת ךס");

        //    Table table = section.AddTable();
        //    table.Format.Alignment = ParagraphAlignment.Center;
        //    Column col = table.AddColumn(Unit.FromCentimeter(2.8));
        //    col = table.AddColumn(Unit.FromCentimeter(2.8));
        //    col = table.AddColumn(Unit.FromCentimeter(2.8));
        //    col = table.AddColumn(Unit.FromCentimeter(2.8));
        //    col = table.AddColumn(Unit.FromCentimeter(2.8));
        //    col = table.AddColumn(Unit.FromCentimeter(2.8));
        //    col = table.AddColumn(Unit.FromCentimeter(2.8));
        //    col = table.AddColumn(Unit.FromCentimeter(2.8));

        //    // Create the header of the table
        //    Row row = table.AddRow();
        //    row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.Format.Font.Bold = true;

        //    row.Cells[0].AddParagraph("מס טופס".ReverseString());
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[1].AddParagraph("סך תשלום".ReverseString());
        //    row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[2].AddParagraph("תאריך פרעון".ReverseString());
        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[3].AddParagraph("תאריך תשלום".ReverseString());
        //    row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[4].AddParagraph("שם בנק".ReverseString());
        //    row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[5].AddParagraph("מס צ'יק".ReverseString());
        //    row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[6].AddParagraph("אופן תשלום".ReverseString());
        //    row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[6].VerticalAlignment = VerticalAlignment.Center;

        //    DictConstsVars DCV = new DictConstsVars();
        //    foreach (var item in LDs)
        //    {
        //        row = table.AddRow();
        //        row.Borders.Bottom.Width = 0.164;
        //        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[6].Format.Alignment = ParagraphAlignment.Right;

        //        if (item.PaymntForm != null)
        //            row.Cells[0].AddParagraph(item.PaymntForm);
        //        row.Cells[1].AddParagraph(item.Total.ToString("0.00"));
        //        if (item.Date2 != 0)
        //            row.Cells[2].AddParagraph(item.Date2.int_ToDate_YYYYMMDD().PrepareOutput(30));
        //        if (item.Date1 != 0)
        //            row.Cells[3].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD().PrepareOutput(30));
        //        if (item.BnkName != null)
        //            row.Cells[4].AddParagraph(item.BnkName.ReverseString());
        //        if (item.CheekNumber != null)
        //            row.Cells[5].AddParagraph(item.CheekNumber.ReverseString());
        //        if (item.DepoType != null)
        //        {
        //            var CV = DCV.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType);
        //            if (CV != null)
        //                row.Cells[6].AddParagraph(CV.HebConst.ReverseString());
        //            else
        //                row.Cells[6].AddParagraph(item.DepoType.ReverseString());
        //        }
        //    }

        //    if (!Directory.Exists(Properties.Settings.Default.PDFPath))
        //    {
        //        Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
        //        Thread.Sleep(1000);
        //    }
        //    PdfDocumentRenderer render = new PdfDocumentRenderer(true)
        //    {
        //        Document = document
        //    };
        //    render.RenderDocument();
        //    string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
        //    render.PdfDocument.Save(filename);
        //    Process.Start(filename);
        //}
        //#endregion Client Transactions
        //#endregion Clients Reports

        #region Deposits Reports
        public static void RepoDepos000(List<DepoView> LDV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LDV = LDV.OrderBy(tt => tt.Date1).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "ת' תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "שם לקוח";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סוג תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סך תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "מס צ'יק";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "שם בנק";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "מס טופס";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "ת' פרעון";

            // Export data
            foreach (var item in LDV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + item.Date1.Long_ToDate_YYYYMMDD();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.FullName;
                var DType = GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType.ToString());
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = DType != null ? DType.HebConst.ToString() : "";
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.CheekNumber == null ? "" : item.CheekNumber;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.BnkName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.PaymntForm.ToString();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + item.Date2.int_ToDate_YYYYMMDD();
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 8 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LDV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LDV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        public static void RepoDepos001(List<DepoView> LDV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LDV = LDV.OrderBy(tt => tt.Date1).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "ת' תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סך תשלום";

            // Export data
            foreach (var item in LDV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + item.Date1.Long_ToDate_YYYYMMDD();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 2 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LDV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LDV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        public static void RepoDepos010(List<DepoView> LDV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LDV = LDV.OrderBy(tt => tt.FullName).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "שם לקוח";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סך תשלום";

            // Export data
            foreach (var item in LDV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.FullName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 2 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LDV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LDV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        public static void RepoDepos011(List<DepoView> LDV)
        {

            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LDV = LDV.OrderBy(tt => tt.Date1).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "ת' תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "שם לקוח";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סך תשלום";

            // Export data
            foreach (var item in LDV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + item.Date1.Long_ToDate_YYYYMMDD();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.FullName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 3 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LDV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LDV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        public static void RepoDepos100(List<DepoView> LDV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LDV = LDV.OrderBy(tt => tt.DepoType).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סוג תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סך תשלום";

            // Export data
            foreach (var item in LDV)
            {
                ColumnIndex = 1;
                RowIndex++;
                var DType = GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType.ToString());
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = DType != null ? DType.HebConst : "";
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 2 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LDV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LDV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        public static void RepoDepos101(List<DepoView> LDV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LDV = LDV.OrderBy(tt => tt.Date1).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "ת' תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סוג תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סך תשלום";

            // Export data
            foreach (var item in LDV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + item.Date1.Long_ToDate_YYYYMMDD();
                var DType = GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType.ToString());
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = DType != null ? DType.HebConst : "";
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 3 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LDV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LDV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        public static void RepoDepos110(List<DepoView> LDV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LDV = LDV.OrderBy(tt => tt.FullName).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "שם לקוח";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סוג תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סך תשלום";

            // Export data
            foreach (var item in LDV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.FullName;
                var DType = GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType.ToString());
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = DType != null ? DType.HebConst : "";
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 3 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LDV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LDV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        public static void RepoDepos111(List<DepoView> LDV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LDV = LDV.OrderBy(tt => tt.Date1).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "ת' תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "שם לקוח";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סוג תשלום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סך תשלום";

            // Export data
            foreach (var item in LDV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + item.Date1.Long_ToDate_YYYYMMDD();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.FullName;
                var DType = GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType.ToString());
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = DType != null ? DType.HebConst : "";
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 4 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LDV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LDV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        #endregion Deposits Reports

        #region Sales Reports
        public static void RepoSales111(List<SaleView> LSV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LSV = LSV.OrderBy(tt => tt.FullName).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "שם לקוח";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "תאריך קניה";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "תאור חומר";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "מ.פריט";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "כמות";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סכום";

            // Export data
            foreach (var item in LSV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.FullName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + item.Date1.Long_ToDate_YYYYMMDD();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.MatName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Price.ToString();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.QTY.ToString();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 6 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LSV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LSV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        public static void RepoSales110(List<SaleView> LSV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LSV = LSV.OrderBy(tt => tt.Date1).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "תאריך קניה";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "תאור חומר";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "מ.פריט";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "כמות";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סכום";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "על ידי";

            // Export data
            foreach (var item in LSV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + item.Date1.Long_ToDate_YYYYMMDD();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.MatName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Price.ToString();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.QTY.ToString();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.BuyerName;
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 6 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LSV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LSV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        public static void RepoSales101(List<SaleView> LSV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LSV = LSV.OrderBy(tt => tt.Date1).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "תאריך קניה";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "שם לקוח";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סכום";

            // Export data
            foreach (var item in LSV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + item.Date1.Long_ToDate_YYYYMMDD();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.FullName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 3 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LSV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LSV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }

        public static void RepoSales100(List<SaleView> LSV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LSV = LSV.OrderBy(tt => tt.Date1).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "תאריך קניה";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סכום";

            // Export data
            foreach (var item in LSV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = "'" + item.Date1.Long_ToDate_YYYYMMDD();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 2 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LSV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LSV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;

        }

        public static void RepoSales011(List<SaleView> LSV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LSV = LSV.OrderBy(tt => tt.FullName).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "שם לקוח";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "תאור חומר";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "מ.פריט";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "כמות";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סכום";

            // Export data
            foreach (var item in LSV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.FullName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.MatName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Price.ToString();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.QTY.ToString();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 5 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LSV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LSV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;

        }

        public static void RepoSales010(List<SaleView> LSV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LSV = LSV.OrderBy(tt => tt.FullName).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "תאור חומר";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "מ.פריט";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "כמות";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סכום";

            // Export data
            foreach (var item in LSV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.MatName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Price.ToString();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.QTY.ToString();
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 4 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LSV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LSV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;

        }

        public static void RepoSales001(List<SaleView> LSV)
        {
            Excel.Application ExcelApp = new Excel.Application();
            Excel._Workbook ExcelBook;
            Excel._Worksheet ExcelSheet;

            // Create object of excel
            ExcelBook = (Excel._Workbook)ExcelApp.Workbooks.Add(1);
            ExcelSheet = (Excel._Worksheet)ExcelBook.ActiveSheet;

            // הגדרת עמוד
            ExcelSheet.DisplayRightToLeft = true; // מימין A מגדירים שגיליון האקסל יהיה מימין לשמאל, כלומר העמודה
            //ExcelSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            ExcelSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            ExcelSheet.PageSetup.TopMargin = ExcelSheet.PageSetup.BottomMargin = 1.64;
            ExcelSheet.PageSetup.LeftMargin = ExcelSheet.PageSetup.RightMargin = 1.64;
            ExcelSheet.PageSetup.HeaderMargin = ExcelSheet.PageSetup.FooterMargin = 0.0;
            ExcelSheet.PageSetup.FitToPagesWide = 1;

            int RowIndex = 1;  // אינדיקס השורה באקסל
            int ColumnIndex = 1;  // אינדיקס העמודה באקסל
            LSV = LSV.OrderBy(tt => tt.FullName).ToList();

            // Export headers
            // Excel indexes start with [1,1]
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "שם לקוח";
            ExcelSheet.Cells[RowIndex, ColumnIndex++] = "סכום";

            // Export data
            foreach (var item in LSV)
            {
                ColumnIndex = 1;
                RowIndex++;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.FullName;
                ExcelSheet.Cells[RowIndex, ColumnIndex++] = item.Total.ToString("0.00");
            }

            // חישוב האות האחרונה שמייצגת את העמודה האחרונה באקסל
            // L למשל אם יש לנו 12 עמודות, אז האות האחרונה תצא
            string lastExcelCol = ((char)(65 + 2 - 1)).ToString();

            // Set font and alignments
            Excel.Range myRange;
            myRange = ExcelSheet.get_Range("A1", lastExcelCol + LSV.Count.ToString());
            myRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            Excel.Font x = myRange.Font;
            x.Name = "Arial";
            x.Size = 11;

            myRange.EntireColumn.AutoFit();

            // סידור נכון לעמודת התאריך            
            //ExcelSheet.Range["I:I"].NumberFormat = "DD/MM/YYYY";
            //ExcelSheet.Range["J:J"].NumberFormat = "MM/DD/YYYY";

            // Format table
            // Format table בסוף מעצבים את הטבלה
            Excel.Range SourceRange;
            SourceRange = ExcelSheet.get_Range("A1", lastExcelCol + (LSV.Count + 1).ToString());
            SourceRange.Worksheet.ListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, SourceRange, System.Type.Missing, Excel.XlYesNoGuess.xlYes, System.Type.Missing).Name = "Table1";
            SourceRange.Select();
            SourceRange.Worksheet.ListObjects["Table1"].TableStyle = "TableStyleMedium15";
            ExcelSheet.ListObjects["Table1"].ShowAutoFilter = false;
            SourceRange.RowHeight = 20;

            myRange.EntireColumn.AutoFit(); // autofit all columns
            ExcelApp.Visible = true;
        }
        #endregion Sales Reports

        #region Materials Report
        //public static void RepoMats(List<MatView> LMW)
        //{
        //    // https://forum.pdfsharp.net/
        //    // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
        //    // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
        //    // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


        //    //Create a MigraDoc document
        //    Document document = new Document();
        //    document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(7.99);
        //    document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
        //    Section section = document.AddSection();
        //    // Create TextFrame
        //    TextFrame tf = section.AddTextFrame();
        //    tf.Left = ShapePosition.Left;
        //    tf.Top = ShapePosition.Top;
        //    tf.Height = Unit.FromCentimeter(0.46);
        //    //tf.RelativeHorizontal = RelativeHorizontal.Margin;
        //    tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
        //    // Create Paragraph
        //    Paragraph paragraph = section.AddParagraph();
        //    paragraph.Format.Font.Size = 12;
        //    paragraph.Format.Font.Bold = true;
        //    paragraph.Format.Alignment = ParagraphAlignment.Center;
        //    paragraph.AddText("מחירון".ReverseString());

        //    paragraph.AddLineBreak();

        //    Table table = section.AddTable();
        //    table.Format.Alignment = ParagraphAlignment.Center;
        //    Column col = table.AddColumn(Unit.FromCentimeter(2.3));
        //    col = table.AddColumn(Unit.FromCentimeter(4.9));
        //    col = table.AddColumn(Unit.FromCentimeter(2.3));

        //    // Create the header of the table
        //    Row row = table.AddRow();
        //    row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.Format.Font.Bold = true;

        //    row.Cells[0].AddParagraph("מ.פריט".ReverseString());
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[1].AddParagraph("תאור חומר".ReverseString());
        //    row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[2].AddParagraph("מס חומר".ReverseString());
        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

        //    foreach (var item in LMW)
        //    {
        //        row = table.AddRow();
        //        row.Borders.Bottom.Width = 0.164;
        //        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[2].Format.Alignment = ParagraphAlignment.Right;

        //        row.Cells[0].AddParagraph(item.Price.ToString());
        //        row.Cells[1].AddParagraph(item.MatName.ReverseString());
        //        row.Cells[2].AddParagraph(item.MatNum.ToString());
        //    }

        //    if (!Directory.Exists(Properties.Settings.Default.PDFPath))
        //    {
        //        Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
        //        Thread.Sleep(1000);
        //    }
        //    PdfDocumentRenderer render = new PdfDocumentRenderer(true)
        //    {
        //        Document = document
        //    };
        //    render.RenderDocument();
        //    string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
        //    render.PdfDocument.Save(filename);
        //    Process.Start(filename);
        //}

        //public static void RepoMats(List<Material> LM)
        //{
        //    // https://forum.pdfsharp.net/
        //    // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
        //    // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
        //    // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


        //    //Create a MigraDoc document
        //    Document document = new Document();
        //    document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
        //    document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
        //    Section section = document.AddSection();
        //    // Create TextFrame
        //    TextFrame tf = section.AddTextFrame();
        //    tf.Left = ShapePosition.Left;
        //    tf.Top = ShapePosition.Top;
        //    tf.Height = Unit.FromCentimeter(0.46);
        //    //tf.RelativeHorizontal = RelativeHorizontal.Margin;
        //    tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
        //    // Create Paragraph
        //    Paragraph paragraph = section.AddParagraph();
        //    paragraph.Format.Font.Size = 12;
        //    paragraph.Format.Font.Bold = true;
        //    paragraph.Format.Alignment = ParagraphAlignment.Center;
        //    paragraph.AddText("מחירון".ReverseString());

        //    paragraph.AddLineBreak();

        //    Table table = section.AddTable();
        //    table.Format.Alignment = ParagraphAlignment.Center;
        //    Column col = table.AddColumn(Unit.FromCentimeter(2.1));
        //    col = table.AddColumn(Unit.FromCentimeter(2.1));
        //    col = table.AddColumn(Unit.FromCentimeter(2.1));
        //    col = table.AddColumn(Unit.FromCentimeter(2.1));
        //    col = table.AddColumn(Unit.FromCentimeter(3.1));
        //    col = table.AddColumn(Unit.FromCentimeter(5.3));
        //    col = table.AddColumn(Unit.FromCentimeter(2.1));

        //    // Create the header of the table
        //    Row row = table.AddRow();
        //    row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.Format.Font.Bold = true;

        //    row.Cells[0].AddParagraph("כ.מימום".ReverseString());
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[1].AddParagraph("כ.במלאי".ReverseString());
        //    row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[2].AddParagraph("מ.ללקוח".ReverseString());
        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[3].AddParagraph("מ.עלות".ReverseString());
        //    row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[4].AddParagraph("ברקוד".ReverseString());
        //    row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[5].AddParagraph("תאור חומר".ReverseString());
        //    row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

        //    row.Cells[6].AddParagraph("מס חומר".ReverseString());
        //    row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[6].VerticalAlignment = VerticalAlignment.Center;

        //    foreach (var item in LM)
        //    {
        //        row = table.AddRow();
        //        row.Borders.Bottom.Width = 0.164;
        //        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
        //        row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
        //        row.Cells[6].Format.Alignment = ParagraphAlignment.Right;

        //        // row.Cells[0].AddParagraph(item.MinQTY.ToString());
        //        row.Cells[1].AddParagraph(item.TotalQTY.ToString());
        //        row.Cells[2].AddParagraph(item.Price.ToString());
        //        row.Cells[3].AddParagraph(item.Price1.ToString());
        //        row.Cells[4].AddParagraph(item.BarCode.ToString());
        //        row.Cells[5].AddParagraph(item.MatName.ReverseString());
        //        row.Cells[6].AddParagraph(item.MatNum.ToString());
        //    }

        //    if (!Directory.Exists(Properties.Settings.Default.PDFPath))
        //    {
        //        Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
        //        Thread.Sleep(1000);
        //    }
        //    PdfDocumentRenderer render = new PdfDocumentRenderer(true)
        //    {
        //        Document = document
        //    };
        //    render.RenderDocument();
        //    string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
        //    render.PdfDocument.Save(filename);
        //    Process.Start(filename);
        //}

        #endregion Materials Report

        #region Messages Reports
        #endregion Messages Reports
    
    */
}
}