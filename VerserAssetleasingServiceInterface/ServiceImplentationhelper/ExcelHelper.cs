using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using VerserAssetleasingServiceInterface.Models;
using Excel = Microsoft.Office.Interop.Excel;
namespace VerserAssetleasingServiceInterface.ServiceImplentationhelper
{
    public class ExcelHelper
    {
        private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private static Microsoft.Office.Interop.Excel.Sheets mWorkSheets;
        private static Microsoft.Office.Interop.Excel.Worksheet mWSheet1;
        private static Microsoft.Office.Interop.Excel.Application oXL;

        public static void GenerateExcelCostModel(JBHiFiCostmodelServiceRequestDetailsModel RequestQuoteModel)
        {
           
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Assets/SampleCostModel.xlsx");
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;
            oXL.DisplayAlerts = false;
            mWorkBook = oXL.Workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            //Get all the sheets in the workbook
            mWorkSheets = mWorkBook.Worksheets;
            //Get the allready exists sheet
            mWSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)mWorkSheets.get_Item("Pricing Summary");
            Microsoft.Office.Interop.Excel.Range range = mWSheet1.UsedRange;
            int colCount = range.Columns.Count;
            int rowCount = range.Rows.Count;
            for (int index = 1; index < RequestQuoteModel.ServiceItemsLists.Count+1; index++)
            {
                decimal total = Convert.ToDecimal(RequestQuoteModel.ServiceItemsLists[index-1].TotalPrice) * RequestQuoteModel.ServiceItemsLists[index-1].Quantity;
                mWSheet1.Cells[rowCount + index, 1] = RequestQuoteModel.ServiceItemsLists[index-1].ServiceDescription;
                mWSheet1.Cells[rowCount + index, 2] = RequestQuoteModel.ServiceItemsLists[index-1].TotalPrice;
                mWSheet1.Cells[rowCount + index, 3] = RequestQuoteModel.ServiceItemsLists[index - 1].Quantity;
                mWSheet1.Cells[rowCount + index, 4] = total;
            }
            mWorkBook.SaveAs("C:\\temp\\testexcel.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
            Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
            Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value);
            mWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
            mWSheet1 = null;
            mWorkBook = null;
            oXL.Quit();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

           
        }
    }
}