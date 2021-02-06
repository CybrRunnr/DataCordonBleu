using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCordonBleu.Models;

namespace DataCordonBleu.Controllers {
    public class DecodeController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Unstuffer unstf) {
            if (ModelState.IsValid) {

            }
            return View(unstf);
        }
    }
}
