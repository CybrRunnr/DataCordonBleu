using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace DataCordonBleu_Framework.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Testing() {
            string imgPath = @"C:\Web\DataCordonBleu\DataCordonBleu-Framework\Uploads\test.jpg";
            string savePath = @"C:\Web\DataCordonBleu\DataCordonBleu-Framework\Exports\test.jpg";
            Bitmap bmp = new Bitmap(imgPath);

            //Gets X coordinate
            for (int x = 0; x < bmp.Width; x++) {
                //Gets X coordinate
                for (int y = 0; y < bmp.Height; y++) {
                    //Color: Struct that hold the ARGB values of a pixel
                    Color pixelColor = bmp.GetPixel(x, y);
                    Color newColor = Color.FromArgb(0, pixelColor.G, pixelColor.B);
                    bmp.SetPixel(x, y, newColor);
                }
            }

            //Image imgToSave;
            bmp.Save(savePath, ImageFormat.Jpeg);

            return View();
        }
    }
}