using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VerserAssetleasingServiceInterface.Models;

namespace VerserAssetleasingServiceInterface.Utils
{
    public class PDFBuilder
    {
        public static void generatePFD(JBHiFiCostmodelServiceRequestDetailsModel RequestQuoteModel)
        {
            Console.Write("Test");



            var document = new PdfDocument();

            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);

            const XFontStyle BoldItalicUnderline = XFontStyle.Bold | XFontStyle.Underline;

            var font = new XFont("sans-serif", 9);
            var fontBold = new XFont("sans-serif", 10, XFontStyle.Bold);
            var fontBold11 = new XFont("sans-serif", 11, XFontStyle.Bold);
            var font11 = new XFont("sans-serif", 11);
            var fontBoldHeading = new XFont("sans-serif", 11, BoldItalicUnderline);
            var fontBoldHeading13 = new XFont("sans-serif", 13, XFontStyle.Bold);
            var fontBoldHeadingUL17 = new XFont("sans-serif", 17, BoldItalicUnderline);
            var fontItalic = new XFont("sans-serif", 10, XFontStyle.Italic);
            var fontItalicX = new XFont("sans-serif", 13, XFontStyle.Italic);
            XPen lineRed = new XPen(XColors.Black, 0.5);


            //var assembly = typeof(Program).GetType().Assembly;
            var imageName = Directory.GetCurrentDirectory() + "\\frogs.jpg";
            XImage image;
            XImage image1;
            gfx.DrawRectangle(new XPen(XColor.FromArgb(0, 0, 0)), 10, 120, 550, 180);

            gfx.DrawString("COST MODEL INFORMATION ", fontBoldHeadingUL17, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 200, 30);
            XRect Addrrect = new XRect(20, 240, 180, 50);
            gfx.DrawRectangle(XBrushes.White, Addrrect);


            gfx.DrawString("Customer Name : " + RequestQuoteModel.CostModelQuoteRequestModel.CustomerName, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 170);
            gfx.DrawString("Contact Name : " + RequestQuoteModel.CostModelQuoteRequestModel.customerContactName, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 190);
            gfx.DrawString("Email : " + RequestQuoteModel.CostModelQuoteRequestModel.email, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 210);
            gfx.DrawString("Start Date : " + DateTime.Now.ToShortDateString(), fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 230);
            gfx.DrawString("Site Address:    " + RequestQuoteModel.CostModelQuoteRequestModel.siteAddress, fontBold, XBrushes.Black, Addrrect, XStringFormats.TopLeft);

            gfx.DrawString("Opportunity Number : " + RequestQuoteModel.CostModelQuoteRequestModel.opportunityNumber, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 170);
            gfx.DrawString("Sales Manager : " + RequestQuoteModel.CostModelQuoteRequestModel.salesManager, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 190);
            gfx.DrawString("Project Manager : " + RequestQuoteModel.CostModelQuoteRequestModel.projectManager, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 210);
            gfx.DrawString("JMS Project : " + RequestQuoteModel.CostModelQuoteRequestModel.salesforceOpportunityName, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 230);
            gfx.DrawString("Verser Branch : " + RequestQuoteModel.CostModelQuoteRequestModel.verserBranch, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 250);

            //XRect Addrrect = new XRect(10, 240, 400, 170);
            gfx.DrawRectangle(new XPen(XColor.FromArgb(0, 0, 0)), 10, 350, 550, 400);
                
            var page1 = document.AddPage();
            var gfx1 = XGraphics.FromPdfPage(page1);

            gfx1.DrawString("By signing you accept that the deliverables stated in this document have been completed in totality or partially due to issues", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 40, 100);
            gfx1.DrawString("arising and those issues being documented accordingly.", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 40, 110);
           
            gfx1.DrawString("Date Signed : " + DateTime.Now.ToShortDateString(), fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 40, 170);

            gfx1.DrawString("Date Signed : " + DateTime.Now.ToShortDateString(), fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 170);

            gfx1.DrawRectangle(new XPen(XColor.FromArgb(0, 0, 0)), 10, 315, 550, 350);
            int i = 10;

            XRect Addrrect1 = new XRect(20, 240, 180, 50);
            gfx.DrawRectangle(XBrushes.White, Addrrect);

            foreach (JBHIFiCostModelServiceItems item in RequestQuoteModel.ServiceItemsLists)
            {
                gfx.DrawString(item.ServiceDescription + " Quantity - " + item.Quantity +" Total -"+ item.TotalPrice, font11, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 420 + i);
                i = i + 20;
            }




            gfx1.DrawString("If a risk is considered H or M, ensure that either:", fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 600);
            gfx1.DrawString("-   The customer controls the risk; and/or", fontItalic, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 40, 620);
            gfx1.DrawString("-   The Company has considered the risk and applied safety controls", fontItalic, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 40, 630);


            var fileName = Path.Combine(@"C:\Temp\"+Guid.NewGuid()+".pdf");
            document.Save(fileName);
        }
    }
}