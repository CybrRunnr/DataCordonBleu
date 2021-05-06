using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using DataCordonBleu_Framework.Models;

namespace DataCordonBleu_Framework.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Privacy() {
            return View();
        }

        /// <summary>
        /// TESTING AREA
        /// </summary>
        /// <returns></returns>
        public ActionResult Testing() {
            string imgPath = @"C:\Web\DataCordonBleu\DataCordonBleu-Framework\Uploads\test.jpg";
            string savePath = @"C:\Web\DataCordonBleu\DataCordonBleu-Framework\Exports\test.jpg";

            Bitmap bmp = new Bitmap(imgPath);
            string input = "Can I read this?";
            int block = 2;

            Stuffer stfr = new Stuffer(input, bmp, block);
            stfr.InsertMessage();

            //Source: https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.encoderparameter?redirectedfrom=MSDN&view=net-5.0
            //Source: https://stackoverflow.com/questions/1484759/quality-of-a-saved-jpg-in-c-sharp
            ImageCodecInfo encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            EncoderParameters jpegParams = new EncoderParameters(1);
            //
            jpegParams.Param[0] = new EncoderParameter(Encoder.Quality, 100);

            stfr.ImageBMP.Save(savePath, encoder, jpegParams);

            Bitmap temp = stfr.ImageBMP;

            Stuffer unstf = new Stuffer();
            unstf.ImageBMP = new Bitmap(savePath);
            //unstf.ImageBMP = temp;
            unstf.ExtractMessage();


            //int[][] msgArray = BitBlock.MessageToBinary(input, block);
            //string output = BitBlock.BinaryToMessage(msgArray, block);

            return View();


            //int[] foo = msgArray[0];
            //int backwards = BitBlock.BinaryArrayToInt(msgArray[0]);

            //Source: https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bitmap?view=netcore-3.1
            //Gets X coordinate
            //for (int x = 0; x < bmp.Width; x++) {
            //    //Gets X coordinate
            //    for (int y = 0; y < bmp.Height; y++) {
            //        //Color: Struct that hold the ARGB values of a pixel
            //        Color pixelColor = bmp.GetPixel(x, y);
            //        Color newColor = Color.FromArgb(0, pixelColor.G, pixelColor.B);
            //        bmp.SetPixel(x, y, newColor);
            //    }
            //}
            //bmp.Save(savePath, ImageFormat.Jpeg);
        }
    }
}