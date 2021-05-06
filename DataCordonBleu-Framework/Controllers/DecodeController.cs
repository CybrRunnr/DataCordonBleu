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
    public class DecodeController : BaseController {

        public ActionResult Index() {
            return View();
        }

        public ActionResult FromFile(HttpPostedFileBase file, Stuffer unstf) {
            try {
                if (file.ContentLength > 0) {
                    //Converts file to image
                    Image temp = Image.FromStream(file.InputStream);
                    unstf.ImageBMP = new Bitmap(temp);
                    unstf.ExtractMessage();
                }
                ViewBag.Message = "File uploaded successful";
                return View("Success", unstf);
            } catch {
                ViewBag.Message = "File uploaded failed";
                return RedirectToAction("Index");
            }
        }
    }
}
