using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using VerserAssetleasingServiceInterface.Models;

namespace VerserAssetleasingServiceInterface.Utils
{
    public class PDFBuilder
    {
        public static string generatePFD(JBHiFiCostmodelServiceRequestDetailsModel RequestQuoteModel)
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
           // Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "abc.txt");
            byte[] imgdata = System.IO.File.ReadAllBytes(Directory.GetParent(HttpContext.Current.Server.MapPath("."))+"\\Images\\VerserLogo.png");
            Stream stream = new MemoryStream(imgdata);
            image = XImage.FromStream(stream);
            gfx.DrawRectangle(new XPen(XColor.FromArgb(0, 0, 0)), 10, 50, 550, 180);
            gfx.DrawImage(image, 420, 80, 120, 100);
            gfx.DrawString("COST MODEL INFORMATION ", fontBoldHeadingUL17, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 150, 30);
            XRect Addrrect = new XRect(20, 180, 180, 50);
            gfx.DrawRectangle(XBrushes.White, Addrrect);


            gfx.DrawString("Customer Name : " + RequestQuoteModel.CostModelQuoteRequestModel.CustomerName, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 100);
            gfx.DrawString("Contact Name : " + RequestQuoteModel.CostModelQuoteRequestModel.customerContactName, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 120);
            gfx.DrawString("Email : " + RequestQuoteModel.CostModelQuoteRequestModel.email, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 140);
            gfx.DrawString("Start Date : " + DateTime.Now.ToShortDateString(), fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 160);
            gfx.DrawString("Site Address:    " + RequestQuoteModel.CostModelQuoteRequestModel.siteAddress, fontBold, XBrushes.Black, Addrrect, XStringFormats.TopLeft);

            gfx.DrawString("Opportunity Number : " + RequestQuoteModel.CostModelQuoteRequestModel.opportunityNumber, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 100);
            gfx.DrawString("Sales Manager : " + RequestQuoteModel.CostModelQuoteRequestModel.salesManager, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 120);
            gfx.DrawString("Project Manager : " + RequestQuoteModel.CostModelQuoteRequestModel.projectManager, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 140);
            gfx.DrawString("JMS Project : " + RequestQuoteModel.CostModelQuoteRequestModel.salesforceOpportunityName, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 160);
            gfx.DrawString("Verser Branch : " + RequestQuoteModel.CostModelQuoteRequestModel.verserBranch, fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 300, 180);

            //XRect Addrrect = new XRect(10, 240, 400, 170);
            gfx.DrawRectangle(new XPen(XColor.FromArgb(0, 0, 0)), 10, 350, 550, 400);
                
            var page1 = document.AddPage();
            var gfx1 = XGraphics.FromPdfPage(page1);
            //XTextFormatter format = new XGraphics.XTextFormatter();

        

            gfx1.DrawRectangle(new XPen(XColor.FromArgb(0, 0, 0)), 20, 50, 550, 450);
            int i = 10;

           
            gfx.DrawString("CATEGORY", fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 20, 380);
            gfx.DrawString("SERVICE", fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 200, 380);
            gfx.DrawString("QUANTITY", fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 400, 380);
            gfx.DrawString("PRICE", fontBold, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 500, 380);
            XTextFormatter tf = new XTextFormatter(gfx);
           
            foreach (JBHIFiCostModelServiceItems item in RequestQuoteModel.ServiceItemsLists)
            {
                XRect serviceCategory = new XRect(20, 410+i, 180, 400);
                XRect serviceDesc = new XRect(200, 410+i, 180, 400);
                XRect quantity = new XRect(400, 410+i, 180, 400);
                XRect price = new XRect(500, 410+i, 180, 400);
                gfx.DrawRectangle(XBrushes.White, serviceDesc);
                gfx.DrawRectangle(XBrushes.White, serviceCategory);
                gfx.DrawRectangle(XBrushes.White, quantity);
                gfx.DrawRectangle(XBrushes.White, price);


                tf.DrawString(item.ServiceCategory.Substring(0, 50), font, XBrushes.Black, serviceCategory, XStringFormats.TopLeft);
                tf.DrawString(item.ServiceDescription, font, XBrushes.Black, serviceDesc, XStringFormats.TopLeft);
                tf.DrawString(item.Quantity.ToString(), font, XBrushes.Black, quantity, XStringFormats.TopLeft);
                tf.DrawString(item.TotalPrice.ToString(), font, XBrushes.Black, price, XStringFormats.TopLeft);
                i = i + 30;
            }


            StringBuilder sb = new StringBuilder();
            XRect sb1 = new XRect(40, 70, 500, 400);
            gfx1.DrawRectangle(XBrushes.White, sb1);
            XTextFormatter tf1 = new XTextFormatter(gfx1);
           
            sb.Append("Quote Terms & Conditions	IMPORTANT - When placing orders please include full End User information - Company Name, Street Address, Phone Number, Contact Name and Email Address.\r\n\r\n");
           
            //sb.Clear();

            sb.Append("PLEASE NOTE: The goods and services as set out in this quotation are accepted as per Ingram Micro's Pty Limited’s (Verser) Customer Terms of Sale. Please refer to Verser Australia website for full details.\r\n\r\n");
            sb.Append("Unless otherwise stated, all prices quoted are in Australian Dollar Currency(AUD) and are exclusive of GST, freight, and configuration charges.\r\n\r\n");
            sb.Append("Freight costs are based on standard road delivery.Air freight or any other form of special delivery will incur additional costs.\r\n\r\n");
            sb.Append("Freight costs are based on standard road delivery.Air freight or any other form of special delivery will incur additional costs.\r\n\r\n");
            sb.Append("xThe pricing specified above is not combinable with any other offer.\r\n\r\n");
            sb.Append("Bundle pricing is subject to change in the event amendments are made to the quantity and/or bundle components ordered.Please contact your account manager should you require changes to the bundle lines.\r\n\r\n");
            sb.Append("This quotation is valid only to the company whose name appears above.\r\n\r\n");
            sb.Append("This quote has been prepared for one end user only. If this quotation is for more than one end user, please request separate quotations for each end user.\r\n\r\n");
            sb.Append("The Ingram Micro reference number and all part numbers product descriptions and pricing specified in this quote must be included in your order.\r\n\r\n");
            sb.Append("All trademarks, brand names, and product names are the property of their respective owners.\r\n\r\n");
            sb.Append("The items quoted above are correct at time of publishing and issuing the quotation. All errors and omissions are excluded and no other discounts apply.Ingram Micro is not responsible for compensating the customer should the item(s) listed in this quote be withdrawn by the Vendor.\r\n\r\n");
            tf1.DrawString(sb.ToString(), font, XBrushes.Black, sb1, XStringFormats.TopLeft);

            var fileName = Path.Combine(@"C:\Temp\"+Guid.NewGuid()+".pdf");
            document.Save(fileName);
            return fileName;
        }
    }
}