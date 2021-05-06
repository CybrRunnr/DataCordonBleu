using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataCordonBleu_Framework.Controllers {
    public class BaseController : Controller {
        protected string getFilePath(string fileName) {
            fileName = fileName.ToUpper();
            fileName = fileName + ".png";
            string folder = Server.MapPath("~/Uploads"); // = (_HostingEnvironment.ContentRootPath + @"\Data");
            string newFilePath = Path.Combine(folder, fileName);
            //string newFilePath = dataFolder + @"\" + fileName + extension;
            return newFilePath;
        }
    }
}