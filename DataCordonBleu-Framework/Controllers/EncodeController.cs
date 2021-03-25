using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DataCordonBleu.Models;

namespace DataCordonBleu_Framework.Controllers {
    public class EncodeController : Controller {
        ////https://www.mikesdotnetting.com/Article/302/server-mappath-equivalent-in-asp-net-core
        //IWebHostEnvironment _HostingEnvironment;
        //public EncodeController(IWebHostEnvironment ihost) {
        //    _HostingEnvironment = ihost;
        //}

        public ActionResult Index() {
            Stuffer stf = new Stuffer();
            if (TempData["FilePath"] != null) {
                stf.FilePath = TempData["FilePath"].ToString();
            }
            return View(stf);
        }

        [HttpPost]
        public ActionResult Index(Stuffer stf) {
            if (ModelState.IsValid) {
            }
            return View(stf);
        }

        [HttpPost]
        //Source: https://www.c-sharpcorner.com/article/upload-files-in-asp-net-mvc-5/
        public ActionResult FromFile(HttpPostedFile file) {
            try {
                if (file.ContentLength > 0) {
                    string newFileName = Hasher.GetRandKey();
                    // Path.GetExtension: https://docs.microsoft.com/en-us/dotnet/api/system.io.path.getextension?view=net-5.0
                    string extension = Path.GetExtension(file.FileName);
                    string newFilePath = getFilePath(newFileName, extension);
                    TempData["FilePath"] = newFilePath;
                    file.SaveAs(newFilePath);
                }
                ViewBag.Message = "File uploaded successful";
                return RedirectToAction("Index");
            } catch {
                ViewBag.Message = "File uploaded failed";
                return RedirectToAction("Index");
            }
        }

        //public async Task<ActionResult> FromFile(IFormFile file) {
        //    long size = file.Length;
        //    if (size > 0) {
        //        string newFileName = Hasher.GetRandKey();
        //        // Path.GetExtension: https://docs.microsoft.com/en-us/dotnet/api/system.io.path.getextension?view=net-5.0
        //        string extension = Path.GetExtension(file.FileName);
        //        string newFilePath = getFilePath(newFileName, extension);
        //        TempData["FilePath"] = newFilePath;
        //        using (FileStream str = new FileStream(newFilePath, FileMode.Create)) {
        //            await file.CopyToAsync(str);
        //        }
        //    } else {

        //    }
        //    return RedirectToAction("Index");
        //}

        private string getFilePath(string fileName, string extension) {
            fileName = fileName.ToUpper();
            string folder = Server.MapPath("~/Uploads"); // = (_HostingEnvironment.ContentRootPath + @"\Data");
            string newFilePath = Path.Combine(folder, fileName, extension);
            //string newFilePath = dataFolder + @"\" + fileName + extension;
            return newFilePath;
        }
    }
}
