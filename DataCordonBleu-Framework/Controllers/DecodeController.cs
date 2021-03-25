using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataCordonBleu.Models;

namespace DataCordonBleu_Framework.Controllers {
    public class DecodeController : Controller {
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Unstuffer unstf) {
            if (ModelState.IsValid) {

            }
            return View(unstf);
        }
    }
}
