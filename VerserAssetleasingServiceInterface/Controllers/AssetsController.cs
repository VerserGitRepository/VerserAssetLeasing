using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerserAssetleasingServiceInterface.Authorize;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace VerserAssetleasingServiceInterface.Controllers
{
    [MyAuthorize]
    public class AssetsController : Controller
    {
        public ActionResult Index()
        {
            List<AssetsListViewModel> assetsdata = new List<AssetsListViewModel>();

            assetsdata = AssetsServicehelper.AssetsList().Result;

            return View(assetsdata);
        }
        [HttpGet]
        public ActionResult GetAssetsData(int Id)
        {
            List<AssetsListViewModel> companydata = new List<AssetsListViewModel>();

            companydata = AssetsServicehelper.GetAssetsData(Id.ToString()).Result;

            return Json(companydata, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ExportTimesSheetToExcel()
        {
            List<AssetsListViewModel> Companymodel = new List<AssetsListViewModel>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Companymodel = AssetsServicehelper.AssetsList().Result;
                GridView gv = new GridView();
                gv.DataSource = Companymodel;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=AssetList.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            return RedirectToAction("Index", "Company");
        }

        [HttpPost]
        public ActionResult ImportAssets()
        {
            var fileType = Request.Form["FileUpload"];
            bool errorOccurred = false;
            string fileName="";
           // HttpPostedFileBase file = Request.Files["UploadExcelFile"];
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                            //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                fileName = file.FileName;
                string mimeType = file.ContentType;
                if (!Directory.Exists(Server.MapPath(".") + "//UploadFile//"))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "//UploadFile//");
                  
                }
                file.SaveAs(Server.MapPath(".") + "//UploadFile//" + file.FileName);
            }
            
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open((Server.MapPath(".") + "\\UploadFile\\"+ fileName));
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[Path.GetFileNameWithoutExtension(Server.MapPath(".") + "\\UploadFile\\" + fileName)];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            Range cells = xlRange.Worksheet.Cells;
           
            int rowCount = xlRange.Rows.Count;
            EndUsersListViewModel data = new EndUsersListViewModel();
            for (int i = 2; i < rowCount; i++)
            {
                try
                {
                    data.FirstName = xlRange.Cells[i, 1].Value2.ToString();
                    data.LastName = xlRange.Cells[i, 2].Value2.ToString();
                    data.EmployeeNo = xlRange.Cells[i, 3].Value2.ToString();
                    data.Department = xlRange.Cells[i, 4].Value2.ToString();
                    data.CostCode = xlRange.Cells[i, 5].Value2.ToString();
                    data.EndUserStatus = xlRange.Cells[i, 6].Value2.ToString();


                    string commencementDate = xlRange.Cells[i, 7].Value2.ToString();
                    double d = double.Parse(commencementDate);
                    DateTime conv = DateTime.FromOADate(d);

                    data.CommencementDate = conv;
                    string terminationDate = xlRange.Cells[i, 7].Value2.ToString();
                    double e = double.Parse(terminationDate);
                    DateTime conv1 = DateTime.FromOADate(e);

                    data.TerminationDate = conv1;
                    data.EndUser_Company = Convert.ToInt32(xlRange.Cells[i, 9].Value2.ToString());
                    data.UserName = xlRange.Cells[i, 10].Value2.ToString();
                    data.EndUser_Site = Convert.ToInt32(xlRange.Cells[i, 11].Value2.ToString());
                    var obj = EndUsersServicehelper.AddEndUser(data);

                }
                catch (Exception ex)
                {
                    errorOccurred = true;
                    continue;
                }
                


            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            foreach (string info in Directory.GetFiles(Server.MapPath(".") + "\\UploadFile\\"))
            {
                System.IO.File.Delete(info);
            }
            Directory.Delete(Server.MapPath(".") + "\\UploadFile\\");

            return new JsonResult { Data = "The file is loaded.", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}