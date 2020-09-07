using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerserAssetleasingServiceInterface.Authorize;
using VerserAssetleasingServiceInterface.Models;
using VerserAssetleasingServiceInterface.ServiceImplentationhelper;
using System.Data.OleDb;
using System.Data;

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
            string fileName = "";
            List<string> ssnList = new List<string>();
            // HttpPostedFileBase file = Request.Files["UploadExcelFile"];
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                            //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                fileName = "SSNList.xls";
                string mimeType = file.ContentType;
                if (!Directory.Exists(Server.MapPath(".") + "//UploadFile//"))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "//UploadFile//");
                }
                file.SaveAs(Server.MapPath(".") + "//UploadFile//SSNList.xls");
            }

            string sheetName = "Sheet1$";// Read the whole excel sheet document      
            DataTable sheetTable = loadSingleSheet(Server.MapPath(".") + "\\UploadFile\\" + fileName, sheetName);
            foreach (DataRow dr in sheetTable.Rows)
            {
                ssnList.Add(dr.ItemArray[0].ToString());
            }
            if (ssnList.Count > 0)
            {
                TempData["SSNList"] = ssnList;
            }
            else
            {
                TempData["SSNList"] = null;
            }
           
            foreach (string info in Directory.GetFiles(Server.MapPath(".") + "//UploadFile//"))
            {
                System.IO.File.Delete(info);
            }
            //TempData["SSNList"] = SSNList;
            Directory.Delete(Server.MapPath(".") + "\\UploadFile\\");
            RedirectToAction("GenerateReport", "Company");

            return Json(null);
        }
        private OleDbConnection returnConnection(string fileName)
        {
            return new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + "; Extended Properties='Excel 12.0 xml;HDR=YES;'");
        }

        private DataTable loadSingleSheet(string fileName, string sheetName)
        {
            DataTable sheetData = new DataTable();
            using (OleDbConnection conn = this.returnConnection(fileName))
            {
                conn.Open();
                // retrieve the data using data adapter
                OleDbDataAdapter sheetAdapter = new OleDbDataAdapter("select * from [" + sheetName + "]", conn);
                sheetAdapter.Fill(sheetData);
            }
            return sheetData;
        }

    }
}