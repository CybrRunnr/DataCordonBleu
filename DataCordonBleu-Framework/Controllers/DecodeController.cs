using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DataCordonBleu_Framework.Models;

namespace DataCordonBleu_Framework.Controllers {
    public class DecodeController : Controller {

        public ActionResult Index() {
            Stuffer unstf = (Stuffer)TempData["unstf"];
            if (unstf != null) {
                unstf.ExtractMessage();
            }
            return View(unstf);
        }

        public ActionResult FromFile(HttpPostedFileBase file, Stuffer unstf) {
            try {
                if (file.ContentLength > 0) {
                    //string newFileName = Hasher.GetRandKey().ToUpper();
                    //unstf.FileName = newFileName;
                    //string newFilePath = getFilePath(unstf.FileName);
                    //unstf.FilePath = newFilePath;
                    //file.SaveAs(unstf.FilePath);
                    Image temp = Image.FromStream(file.InputStream);

                    unstf.ImageBMP = new Bitmap(temp);
                    TempData["unstf"] = unstf;
                }
                ViewBag.Message = "File uploaded successful";
                return RedirectToAction("Index");
            } catch {
                ViewBag.Message = "File uploaded failed";
                return RedirectToAction("Index");
            }
        }

        private string getFilePath(string fileName) {
            fileName = fileName.ToUpper();
            fileName = fileName + ".png";
            string folder = Server.MapPath("~/Uploads"); // = (_HostingEnvironment.ContentRootPath + @"\Data");
            string newFilePath = Path.Combine(folder, fileName);
            //string newFilePath = dataFolder + @"\" + fileName + extension;
            return newFilePath;
        }
    }
}
