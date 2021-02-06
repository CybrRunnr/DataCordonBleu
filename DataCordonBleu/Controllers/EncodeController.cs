using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCordonBleu.Controllers {
    public class EncodeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
