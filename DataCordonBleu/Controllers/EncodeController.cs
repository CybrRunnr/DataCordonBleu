using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataCordonBleu.Models;

namespace DataCordonBleu.Controllers {
    public class EncodeController : Controller {
        //https://www.mikesdotnetting.com/Article/302/server-mappath-equivalent-in-asp-net-core
        IWebHostEnvironment _HostingEnvironment;
        public EncodeController(IWebHostEnvironment ihost) {
            _HostingEnvironment = ihost;
        }

        public IActionResult Index() {
            Stuffer stf = new Stuffer();
            if (TempData["FilePath"] != null) {
                stf.FilePath = TempData["FilePath"].ToString();
            }
            return View(stf);
        }

        [HttpPost]
        public IActionResult Index(Stuffer stf) {
            if (ModelState.IsValid) {
            }
            return View(stf);
        }

        [HttpPost]
        public async Task<IActionResult> FromFile(IFormFile file) {
            long size = file.Length;
            if (size > 0) {
                string newFileName = Hasher.GetRandKey();
                // Path.GetExtension: https://docs.microsoft.com/en-us/dotnet/api/system.io.path.getextension?view=net-5.0
                string extension = Path.GetExtension(file.FileName);
                string newFilePath = getFilePath(newFileName, extension);
                TempData["FilePath"] = newFileName;
                using (FileStream str = new FileStream(newFilePath, FileMode.Create)) {
                    await file.CopyToAsync(str);
                }
            } else {

            }
            return RedirectToAction("Index");
        }

        private string getFilePath(string fileName, string extension) {
            fileName = fileName.ToUpper();
            string dataFolder = (_HostingEnvironment.ContentRootPath + @"\Data");
            string newFilePath = dataFolder + @"\" + fileName + extension;
            return newFilePath;
        }
    }
}
