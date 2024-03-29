﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DataCordonBleu_Framework.Models;

namespace DataCordonBleu_Framework.Controllers {
    public class EncodeController : BaseController {

        public ActionResult Index() {
            return View();
        }

        #region Old Code
        //[HttpPost]
        //public ActionResult Index(int x) {
        //    Stuffer stf = (Stuffer)TempData["stf"];
        //    if (stf != null) {
        //        stf.InsertMessage();

        //        //Stuffer testing = new Stuffer();
        //        //testing.ImageBMP = stf.ImageBMP;

        //        //testing.ExtractMessage();

        //        string path = Path.Combine(Server.MapPath("~/Exports"), stf.FileName + ".png");
        //        try {
        //            stf.ImageBMP.Save(path, ImageFormat.Png);
        //        } catch (Exception) {

        //            throw;
        //        }
        //    }
        //    return View("Success",stf);
        //}

        //[HttpPost]
        //public ActionResult Index(Stuffer stf) {
        //    Stuffer unstf = new Stuffer();
        //    //if (ModelState.IsValid) {
        //    string imgPath = @"C:\Web\DataCordonBleu\DataCordonBleu-Framework\Uploads\test.png";
        //    string savePath = @"C:\Web\DataCordonBleu\DataCordonBleu-Framework\Exports\test.png";
        //    Bitmap bmpUpload = new Bitmap(imgPath);
        //    stf.ImageBMP = bmpUpload;
        //    stf.InsertMessage();

        //    //stf.ImageBMP.Save(savePath, ImageFormat.Png);
        //    unstf.BlockSize = stf.BlockSize;
        //    unstf.ImageBMP = stf.ImageBMP; //new Bitmap(savePath);
        //    unstf.ExtractMessage();
        //    //}
        //    return View(unstf);
        //}
        #endregion

        [HttpPost]
        //Source: https://www.c-sharpcorner.com/article/upload-files-in-asp-net-mvc-5/
        public ActionResult FromFile(HttpPostedFileBase file, Stuffer stf) {
            try {
                if (file.ContentLength > 0 && file != null) {
                    //Save the original img to uploads
                    fileCreation(stf);
                    file.SaveAs(stf.FilePath);
                    stf.ImageBMP = new Bitmap(stf.FilePath);

                    //Encode message and save img to exports
                    stf.InsertMessage();
                    string path = Path.Combine(Server.MapPath("~/Exports"), stf.FileName + ".png");
                    stf.ImageBMP.Save(path, ImageFormat.Png);
                }
                ViewBag.Message = "File uploaded successful";
                return View("Success", stf);
            } catch {
                ViewBag.Message = "File uploaded failed";
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Creates the File name and path. Stores vaules in the provided Stuffer
        /// </summary>
        /// <param name="stf">Stuffer object containing user input</param>
        public void fileCreation(Stuffer stf) {
            string newFileName = Hasher.GetRandKey().ToUpper();
            stf.FileName = newFileName;
            string newFilePath = getFilePath(stf.FileName);
            stf.FilePath = newFilePath;
        }

        #region Old Code
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
        #endregion
    }
}
