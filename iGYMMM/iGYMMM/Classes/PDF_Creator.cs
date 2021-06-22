using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iGYMMM
{
    public static class PDF_Creator
    {/*
        #region Clients Reports
        public static void RepoClients(List<Client> LC, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(3.4));
            col = table.AddColumn(Unit.FromCentimeter(3.4));
            col = table.AddColumn(Unit.FromCentimeter(3.4));
            col = table.AddColumn(Unit.FromCentimeter(8.3));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סלולרי 1".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("טלפון 1".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("יתרה".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[2].MergeDown = 1;

            row.Cells[3].AddParagraph("שם לקוח".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[3].MergeDown = 1;

            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("תאריך קניה אחרון".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("טלפון 2".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row.Borders.Bottom.Width = 1.64;

            foreach (var item in LC)
            {
                Row row1 = table.AddRow();
                Row row2 = table.AddRow();
                row2.Borders.Bottom.Width = 0.164;
                row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row1.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row1.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row1.Cells[2].MergeDown = 1;
                row1.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row1.Cells[3].MergeDown = 1;
                row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row2.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row2.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row2.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row2.Cells[2].VerticalAlignment = VerticalAlignment.Center;

                row1.Cells[0].AddParagraph(item.Cel1.PrepareOutput(20));
                row1.Cells[1].AddParagraph(item.Tel1.PrepareOutput(20));
                row1.Cells[2].AddParagraph(item.ClntBalance.ToString());
                row1.Cells[3].AddParagraph(item.FullName.PrepareOutput(60));
                if (item.LastBuyDate != 0)
                    row2.Cells[0].AddParagraph(item.LastBuyDate.Long_ToDate_YYYYMMDD());
                row2.Cells[1].AddParagraph(item.Tel2.PrepareOutput(20));
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoCustomClnts(List<Client> LC, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147

            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(3.1));
            col = table.AddColumn(Unit.FromCentimeter(3.1));
            col = table.AddColumn(Unit.FromCentimeter(3.1));
            col = table.AddColumn(Unit.FromCentimeter(6.1));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("טלפון 2".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("טלפון 1".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("יתרה".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("שם לקוח".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;


            foreach (var item in LC)
            {
                Row row1 = table.AddRow();
                row1.Borders.Bottom.Width = 0.164;
                row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row1.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row1.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row1.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[3].Format.Alignment = ParagraphAlignment.Right;

                row1.Cells[0].AddParagraph(item.Tel2.PrepareOutput(20));
                row1.Cells[1].AddParagraph(item.Tel1.PrepareOutput(20));
                row1.Cells[2].AddParagraph(item.ClntBalance.ToString());
                row1.Cells[3].AddParagraph(item.FullName.PrepareOutput(60));
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        #region Client Transactions
        public static void ClientTranzSales(List<Transaction> LTs, Client client, string ReportName, decimal TotalAmount)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(client.FullName.ReverseString());

            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            //paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(client.GetTelephons());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(4.3));
            col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(4.3));
            col = table.AddColumn(Unit.FromCentimeter(2.1));
            col = table.AddColumn(Unit.FromCentimeter(2.4));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("הערות".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("יתרה".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("סך תשלום".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("סך מכירה".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[4].AddParagraph("תאור חומר".ReverseString());
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[5].AddParagraph("כמות".ReverseString());
            row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[6].AddParagraph("תאריך".ReverseString());
            row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[6].VerticalAlignment = VerticalAlignment.Center;

            DictConstsVars DCV = new DictConstsVars();
            foreach (var item in LTs)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[6].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Descrp.ReverseString());
                row.Cells[1].AddParagraph(item.SubTotal.ToString("0.00"));
                row.Cells[2].AddParagraph(item.DepoTotal.ToString("0.00"));
                row.Cells[3].AddParagraph(item.Total.ToString("0.00"));
                var CV = DCV.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.MatName);
                if (CV != null)
                    row.Cells[4].AddParagraph(CV.HebConst.ReverseString());
                else
                    row.Cells[4].AddParagraph(item.MatName.ReverseString());
                row.Cells[5].AddParagraph(item.QTY.ToString("0.00"));
                if (item.Date1 != 0)
                    row.Cells[6].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD().PrepareOutput(30));
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(TotalAmount.ToString("0.00") + " חוקלה תרתי");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }
        public static void ClientTranzDepos(List<Deposit> LDs, Client client, string ReportName, decimal TotalAmount)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());

            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(client.FullName.ReverseString());
            paragraph.AddLineBreak();

            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(TotalAmount.ToString("0.00") + " םימולשת ךס");

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("מס טופס".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("סך תשלום".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("תאריך פרעון".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("תאריך תשלום".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[4].AddParagraph("שם בנק".ReverseString());
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[5].AddParagraph("מס צ'יק".ReverseString());
            row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[6].AddParagraph("אופן תשלום".ReverseString());
            row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[6].VerticalAlignment = VerticalAlignment.Center;

            DictConstsVars DCV = new DictConstsVars();
            foreach (var item in LDs)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[6].Format.Alignment = ParagraphAlignment.Right;

                if (item.PaymntForm != null)
                    row.Cells[0].AddParagraph(item.PaymntForm);
                row.Cells[1].AddParagraph(item.Total.ToString("0.00"));
                if (item.Date2 != 0)
                    row.Cells[2].AddParagraph(item.Date2.int_ToDate_YYYYMMDD().PrepareOutput(30));
                if (item.Date1 != 0)
                    row.Cells[3].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD().PrepareOutput(30));
                if (item.BnkName != null)
                    row.Cells[4].AddParagraph(item.BnkName.ReverseString());
                if (item.CheekNumber != null)
                    row.Cells[5].AddParagraph(item.CheekNumber.ReverseString());
                if (item.DepoType != null)
                {
                    var CV = DCV.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType);
                    if (CV != null)
                        row.Cells[6].AddParagraph(CV.HebConst.ReverseString());
                    else
                        row.Cells[6].AddParagraph(item.DepoType.ReverseString());
                }
            }

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }
        #endregion Client Transactions
        #endregion Clients Reports

        #region Deposits Reports
        public static void RepoDepos000(List<DepoView> LDV, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.2));
            col = table.AddColumn(Unit.FromCentimeter(1.9));
            col = table.AddColumn(Unit.FromCentimeter(2.2));
            col = table.AddColumn(Unit.FromCentimeter(1.9));
            col = table.AddColumn(Unit.FromCentimeter(2.2));
            col = table.AddColumn(Unit.FromCentimeter(1.9));
            col = table.AddColumn(Unit.FromCentimeter(5.0));
            col = table.AddColumn(Unit.FromCentimeter(2.2));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("ת' פרעון".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("מס טופס".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("שם בנק".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("מס צ'יק".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[4].AddParagraph("סך תשלום".ReverseString());
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[5].AddParagraph("סוג תשלום".ReverseString());
            row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[6].AddParagraph("שם לקוח".ReverseString());
            row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[6].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[7].AddParagraph("ת' תשלום".ReverseString());
            row.Cells[7].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[7].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LDV)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[7].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[7].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Date2.int_ToDate_YYYYMMDD());
                row.Cells[1].AddParagraph(item.PaymntForm.ToString());
                row.Cells[2].AddParagraph(item.BnkName.ReverseString().ToString());
                row.Cells[3].AddParagraph(item.CheekNumber == null ? "" : item.CheekNumber);
                row.Cells[4].AddParagraph(item.Total.ToString());
                var DType = GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType.ToString());
                row.Cells[5].AddParagraph(DType != null ? DType.HebConst.ReverseString().ToString() : "");
                row.Cells[6].AddParagraph(item.FullName.ReverseString());
                row.Cells[7].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoDepos001(List<DepoView> LDV, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(13.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.9));
            col = table.AddColumn(Unit.FromCentimeter(2.9));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סך תשלום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("ת' תשלום".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LDV)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoDepos010(List<DepoView> LDV, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(7.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.9));
            col = table.AddColumn(Unit.FromCentimeter(5.9));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סך תשלום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("שם לקוח".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LDV)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.FullName.ReverseString());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoDepos011(List<DepoView> LDV, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(8.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.7));
            col = table.AddColumn(Unit.FromCentimeter(5.7));
            col = table.AddColumn(Unit.FromCentimeter(2.9));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סך תשלום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("שם לקוח".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("ת' תשלום".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LDV)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.FullName.ReverseString());
                row.Cells[2].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoDepos100(List<DepoView> LDV, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(9.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.9));
            col = table.AddColumn(Unit.FromCentimeter(2.9));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סך תשלום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("סוג תשלום".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LDV)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                var DType = GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType.ToString());
                row.Cells[1].AddParagraph(DType != null ? DType.HebConst.ReverseString().ToString() : "");
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoDepos101(List<DepoView> LDV, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(11.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.7));
            col = table.AddColumn(Unit.FromCentimeter(2.7));
            col = table.AddColumn(Unit.FromCentimeter(2.7));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סך תשלום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("סוג תשלום".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("ת' תשלום".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LDV)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                var DType = GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType.ToString());
                row.Cells[1].AddParagraph(DType != null ? DType.HebConst.ReverseString().ToString() : "");
                row.Cells[2].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoDepos110(List<DepoView> LDV, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(6.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.7));
            col = table.AddColumn(Unit.FromCentimeter(2.2));
            col = table.AddColumn(Unit.FromCentimeter(5.2));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סך תשלום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("סוג תשלום".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("שם לקוח".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LDV)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                var DType = GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType.ToString());
                row.Cells[1].AddParagraph(DType != null ? DType.HebConst.ReverseString().ToString() : "");
                row.Cells[2].AddParagraph(item.FullName.ReverseString());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoDepos111(List<DepoView> LDV, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(4.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.7));
            col = table.AddColumn(Unit.FromCentimeter(2.7));
            col = table.AddColumn(Unit.FromCentimeter(6.2));
            col = table.AddColumn(Unit.FromCentimeter(2.7));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סך תשלום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("סוג תשלום".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("שם לקוח".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("ת' תשלום".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LDV)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                var DType = GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == item.DepoType.ToString());
                row.Cells[1].AddParagraph(DType != null ? DType.HebConst.ReverseString().ToString() : "");
                row.Cells[2].AddParagraph(item.FullName.ReverseString());
                row.Cells[3].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoDepos(List<DepoView> LDV, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));
            col = table.AddColumn(Unit.FromCentimeter(2.8));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("מס טופס".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("סך תשלום".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("תאריך פרעון".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("תאריך תשלום".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[4].AddParagraph("שם בנק".ReverseString());
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[5].AddParagraph("מס צ'יק".ReverseString());
            row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[6].AddParagraph("אופן תשלום".ReverseString());
            row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
            // Create the header of the table

            DictConstsVars DCV = new DictConstsVars();
            foreach (var item in LDV)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[6].Format.Alignment = ParagraphAlignment.Right;

                if (item.PaymntForm != null)
                    row.Cells[0].AddParagraph(item.PaymntForm);
                row.Cells[1].AddParagraph(item.Total.ToString());
                if (item.Date2 != 0)
                    row.Cells[2].AddParagraph(item.Date2.int_ToDate_YYYYMMDD().PrepareOutput(30));
                if (item.Date1 != 0)
                    row.Cells[3].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD().PrepareOutput(30));
                if (item.BnkName != null)
                    row.Cells[4].AddParagraph(item.BnkName.ReverseString());
                if (item.CheekNumber != null)
                    row.Cells[5].AddParagraph(item.CheekNumber);
                if (item.DepoType != null)
                {
                    var CV = DCV.l_ConstVar.First(tt => tt.EngConst == item.DepoType);
                    if (CV != null)
                        row.Cells[6].AddParagraph(CV.HebConst.PrepareOutput(20));
                    else
                        row.Cells[6].AddParagraph(item.DepoType.ToString());
                }
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }
        #endregion Deposits Reports

        #region Sales Reports
        public static void RepoSales111(List<SaleView> LS, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(4.9));
            col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(5.9));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סכום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("כמות".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("מ.פריט".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("תאור חומר".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[4].AddParagraph("תאריך קניה".ReverseString());
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[5].AddParagraph("שם לקוח".ReverseString());
            row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LS)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.QTY.ToString());
                row.Cells[2].AddParagraph(item.Price.ToString());
                row.Cells[3].AddParagraph(item.MatName.ReverseString());
                row.Cells[4].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD());
                row.Cells[5].AddParagraph(item.FullName.ReverseString());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoSales110(List<SaleView> LS, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(5.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(4.9));
            col = table.AddColumn(Unit.FromCentimeter(2.3));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סכום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("כמות".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("מ.פריט".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("תאור חומר".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[4].AddParagraph("תאריך קניה".ReverseString());
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LS)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.QTY.ToString());
                row.Cells[2].AddParagraph(item.Price.ToString());
                row.Cells[3].AddParagraph(item.MatName.ReverseString());
                row.Cells[4].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoSales101(List<SaleView> LS, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(8.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(5.9));
            col = table.AddColumn(Unit.FromCentimeter(2.3));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סכום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("שם לקוח".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("תאריך קניה".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LS)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.FullName.ReverseString());
                row.Cells[2].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoSales100(List<SaleView> LS, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(12.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(3.3));
            col = table.AddColumn(Unit.FromCentimeter(3.3));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סכום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("תאריך קניה".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LS)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.Date1.Long_ToDate_YYYYMMDD());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoSales011(List<SaleView> LS, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(2.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(4.9));
            col = table.AddColumn(Unit.FromCentimeter(5.9));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סכום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("כמות".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("מ.פריט".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("תאור חומר".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[4].AddParagraph("שם לקוח".ReverseString());
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LS)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.QTY.ToString());
                row.Cells[2].AddParagraph(item.Price.ToString());
                row.Cells[3].AddParagraph(item.MatName.ReverseString());
                row.Cells[4].AddParagraph(item.FullName.ReverseString());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoSales010(List<SaleView> LS, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(7.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.4));
            col = table.AddColumn(Unit.FromCentimeter(2.4));
            col = table.AddColumn(Unit.FromCentimeter(2.4));
            col = table.AddColumn(Unit.FromCentimeter(5.1));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סכום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("כמות".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("מ.פריט".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("תאור חומר".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LS)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.QTY.ToString());
                row.Cells[2].AddParagraph(item.Price.ToString());
                row.Cells[3].AddParagraph(item.MatName.ReverseString());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoSales001(List<SaleView> LS, string Total_Amount, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(9.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(3.3));
            col = table.AddColumn(Unit.FromCentimeter(6.9));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סכום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("שם לקוח".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LS)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.FullName.ReverseString());
            }

            paragraph.AddLineBreak();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Underline = Underline.Single;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(Total_Amount + " יפסכ םוכיס");

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoSales(List<Sale> LS, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(3.1));
            col = table.AddColumn(Unit.FromCentimeter(3.1));
            col = table.AddColumn(Unit.FromCentimeter(3.1));
            col = table.AddColumn(Unit.FromCentimeter(6.2));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סך מכירות".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("כמות".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("מ.פריט".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("תאור חומר".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LS)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.QTY.ToString());
                row.Cells[2].AddParagraph(item.Price.ToString());
                row.Cells[3].AddParagraph(item.MatName.ReverseString());
            }

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoSales(List<SaleView> LSV, string ReportName)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(ReportName.ReverseString());
            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(3.1));
            col = table.AddColumn(Unit.FromCentimeter(3.1));
            col = table.AddColumn(Unit.FromCentimeter(3.1));
            col = table.AddColumn(Unit.FromCentimeter(6.2));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("סך מכירות".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("כמות".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("מ.פריט".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("תאור חומר".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LSV)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Total.ToString());
                row.Cells[1].AddParagraph(item.QTY.ToString());
                row.Cells[2].AddParagraph(item.Price.ToString());
                row.Cells[3].AddParagraph(item.MatName.ReverseString());
            }

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }
        #endregion Sales Reports

        #region Materials Report
        public static void RepoMats(List<MatView> LMW)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(7.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText("מחירון".ReverseString());

            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.3));
            col = table.AddColumn(Unit.FromCentimeter(4.9));
            col = table.AddColumn(Unit.FromCentimeter(2.3));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("מ.פריט".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("תאור חומר".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("מס חומר".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LMW)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.Price.ToString());
                row.Cells[1].AddParagraph(item.MatName.ReverseString());
                row.Cells[2].AddParagraph(item.MatNum.ToString());
            }

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }

        public static void RepoMats(List<Material> LM)
        {
            // https://forum.pdfsharp.net/
            // http://www.pdfsharp.net/wiki/Invoice-sample.ashx
            // http://www.pdfsharp.net/wiki/AllPages.aspx?Cat=Samples
            // PM> Install-Package PDFsharp-MigraDoc-GDI -Version 1.50.5147


            //Create a MigraDoc document
            Document document = new Document();
            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(0.99);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(0.99);
            Section section = document.AddSection();
            // Create TextFrame
            TextFrame tf = section.AddTextFrame();
            tf.Left = ShapePosition.Left;
            tf.Top = ShapePosition.Top;
            tf.Height = Unit.FromCentimeter(0.46);
            //tf.RelativeHorizontal = RelativeHorizontal.Margin;
            tf.AddParagraph(DateTime.Now.ToStringDateTimeFormatDateTimeYYYYMMDD());
            // Create Paragraph
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText("מחירון".ReverseString());

            paragraph.AddLineBreak();

            Table table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Center;
            Column col = table.AddColumn(Unit.FromCentimeter(2.0));
            col = table.AddColumn(Unit.FromCentimeter(2.2));
            col = table.AddColumn(Unit.FromCentimeter(2.0));
            col = table.AddColumn(Unit.FromCentimeter(2.0));
            col = table.AddColumn(Unit.FromCentimeter(3.3));
            col = table.AddColumn(Unit.FromCentimeter(5.4));
            col = table.AddColumn(Unit.FromCentimeter(2.0));

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;

            row.Cells[0].AddParagraph("כ.מימום".ReverseString());
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("כ.במלאי".ReverseString());
            row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("מ.ללקוח".ReverseString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("מ.עלות".ReverseString());
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[4].AddParagraph("ברקוד".ReverseString());
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[5].AddParagraph("תאור חומר".ReverseString());
            row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[6].AddParagraph("מס חומר".ReverseString());
            row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[6].VerticalAlignment = VerticalAlignment.Center;

            foreach (var item in LM)
            {
                row = table.AddRow();
                row.Borders.Bottom.Width = 0.164;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[6].Format.Alignment = ParagraphAlignment.Right;

                row.Cells[0].AddParagraph(item.MinQTY.ToString());
                row.Cells[1].AddParagraph(item.TotalQTY.ToString());
                row.Cells[2].AddParagraph(item.Price.ToString());
                row.Cells[3].AddParagraph(item.Price1.ToString());
                row.Cells[4].AddParagraph(item.BarCode.ToString());
                row.Cells[5].AddParagraph(item.MatName.ReverseString());
                row.Cells[6].AddParagraph(item.MatNum.ToString());
            }

            if (!Directory.Exists(Properties.Settings.Default.PDFPath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PDFPath);
                Thread.Sleep(1000);
            }
            PdfDocumentRenderer render = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            render.RenderDocument();
            string filename = Properties.Settings.Default.PDFPath + @"\" + DateTime.Now.DateToString_YYYYMMDDHHMM() + ".pdf";
            render.PdfDocument.Save(filename);
            Process.Start(filename);
        }
        #endregion Materials Report

        #region Messages Reports
        #endregion Messages Reports
        */
    }
}
