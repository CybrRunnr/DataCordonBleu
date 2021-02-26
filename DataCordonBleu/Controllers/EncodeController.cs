using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCordonBleu.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DataCordonBleu.Controllers {
    public class EncodeController : Controller {
        public IActionResult Index() {
            Stuffer stf = new Stuffer();
            if (ViewBag.FileName != null) {
                stf.FileName = ViewBag.FileName;
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
                TempData["FileName"] = file.FileName;
                string filePath = @"C:\Web\DataCordonBleu\DataCordonBleu\Data\" + @"\" + file.FileName;
                using (FileStream str = new FileStream(filePath, FileMode.Create)) {
                    await file.CopyToAsync(str);
                }
            } else {

            }
            return RedirectToAction("Index");
        }
    }
}
