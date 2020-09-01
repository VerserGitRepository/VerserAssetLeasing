using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
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

            string fileName = System.Web.HttpContext.Current.Server.MapPath("~/Assets/logger.txt");
            StreamWriter sw;
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file     
            sw = File.CreateText(fileName);


            try
            {
                // Check if file already exists. If yes, delete it.     
              
                sw.WriteLine("New file created: {0}", DateTime.Now.ToString());
                sw.WriteLine("Author: Mahesh Chand");
                sw.WriteLine("Add one more line ");
                sw.WriteLine("Add one more line ");



                // Write file contents on console.     



                string path = System.Web.HttpContext.Current.Server.MapPath("~/Assets/SampleCostModel.xlsx");
                sw.WriteLine("Created path! ");
                oXL = new Microsoft.Office.Interop.Excel.Application();
                sw.WriteLine("Created instance! " + oXL.Workbooks.Count);
                oXL.Visible = true;
                //oXL.DisplayAlerts = false;
                mWorkBook = oXL.Workbooks.Open(path);
                sw.WriteLine("Opening Path for the workbook.");
                //Get all the sheets in the workbook
                mWorkSheets = mWorkBook.Worksheets;
                //Get the allready exists sheet
                mWSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)mWorkSheets.get_Item("Project Details");
                sw.WriteLine("Getting the project details tab.");
                Microsoft.Office.Interop.Excel.Range range = mWSheet1.UsedRange;
                int colCount = range.Columns.Count;
                int rowCount = range.Rows.Count;
                sw.WriteLine("Row count and column count");
                mWSheet1.Cells[2, 2] = RequestQuoteModel.CostModelQuoteRequestModel.CustomerName;
                mWSheet1.Cells[3, 2] = RequestQuoteModel.CostModelQuoteRequestModel.opportunityNumber;
                mWSheet1.Cells[4, 2] = RequestQuoteModel.CostModelQuoteRequestModel.siteAddress;
                mWSheet1.Cells[5, 2] = RequestQuoteModel.CostModelQuoteRequestModel.customerContactName;
                mWSheet1.Cells[6, 2] = "No";
                mWSheet1.Cells[7, 2] = RequestQuoteModel.CostModelQuoteRequestModel.opportunityName;
                mWSheet1.Cells[8, 2] = RequestQuoteModel.CostModelQuoteRequestModel.salesforceOpportunityName;
                mWSheet1.Cells[2, 4] = RequestQuoteModel.CostModelQuoteRequestModel.verserBranch;
                mWSheet1.Cells[3, 4] = RequestQuoteModel.CostModelQuoteRequestModel.salesManager;
                mWSheet1.Cells[4, 4] = RequestQuoteModel.CostModelQuoteRequestModel.projectManager;
                mWSheet1.Cells[5, 4] = RequestQuoteModel.CostModelQuoteRequestModel.startDate;
                mWSheet1.Cells[6, 4] = RequestQuoteModel.CostModelQuoteRequestModel.startDate;
                mWSheet1.Cells[7, 4] = RequestQuoteModel.CostModelQuoteRequestModel.approver;

                int descRowCount = 11;
                for (int index = 1; index < RequestQuoteModel.ServiceItemsLists.Count + 1; index++)
                {
                    decimal total = Convert.ToDecimal(RequestQuoteModel.ServiceItemsLists[index - 1].TotalPrice) * RequestQuoteModel.ServiceItemsLists[index - 1].Quantity;
                    mWSheet1.Cells[descRowCount + index, 1] = RequestQuoteModel.ServiceItemsLists[index - 1].ServiceDescription;
                    mWSheet1.Cells[descRowCount + index, 2] = RequestQuoteModel.ServiceItemsLists[index - 1].TotalPrice;
                    mWSheet1.Cells[descRowCount + index, 3] = RequestQuoteModel.ServiceItemsLists[index - 1].Quantity;
                    mWSheet1.Cells[descRowCount + index, 4] = total;
                }
                //mWSheet1.Cells[58, 2] = RequestQuoteModel.CostModelQuoteRequestModel.gsT_10;
                //mWSheet1.Cells[59, 2] = RequestQuoteModel.CostModelQuoteRequestModel.totaL_Excl_GST;
                //mWSheet1.Cells[60, 2] = RequestQuoteModel.CostModelQuoteRequestModel.totaL_Excl_GST;
                sw.WriteLine("Saving path of the file.");
                mWorkBook.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/Assets/CostModelNewQuote.xls"));
                sw.WriteLine("File saved.");
                mWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
                sw.WriteLine("File closed.");
                mWSheet1 = null;
                mWorkBook = null;
                oXL.Quit();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception Ex)
            {
                sw.WriteLine("exception closed." + Ex.ToString());
            }
            finally
            {
                sw.Close();
            }
            


        }
    }
}