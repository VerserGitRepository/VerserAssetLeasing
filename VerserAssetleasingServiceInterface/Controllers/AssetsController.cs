﻿using System;
using System.Collections.Generic;
using System.IO;
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
        [OutputCache(CacheProfile = "OneHour", VaryByHeader = "X-Requested-With", Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            var assetsdata = new List<AssetsListViewModel>();
           // assetsdata = AssetsServicehelper.AssetsList().Result;
            return View(assetsdata);
        }
        [HttpGet]
        [OutputCache(CacheProfile = "OneHour", VaryByHeader = "X-Requested-With", Location = OutputCacheLocation.Server)]
        public ActionResult GetAssetsData(int Id)
        {
            var assets = new List<JBHiFiAssetsModel>();
            assets = AssetsServicehelper.GetAssetsData(Id.ToString()).Result;
            return Json(assets, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ExportTimesSheetToExcel(FormCollection form)
        {
           
            string Orderno = form["hiddenAssetId"];
            var assets = new List<JBHiFiAssetsModel>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                assets = AssetsServicehelper.GetAssetsData(Orderno).Result;
                GridView gv = new GridView();
                gv.DataSource = assets;
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
        public JsonResult ImportSSN()
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
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            Range cells = xlRange.Worksheet.Cells;
           
            int rowCount = xlRange.Rows.Count;
            List<string> SSNList = new List<string>();
            for (int i = 2; i < rowCount; i++)
            {
                try
                {
                   
                    SSNList.Add(xlRange.Cells[i, 1].Value2.ToString());
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
            TempData["SSNList"] = SSNList;
            Directory.Delete(Server.MapPath(".") + "\\UploadFile\\");
            RedirectToAction("GenerateReport", "Company");

            return Json(null);
        }
    }
}