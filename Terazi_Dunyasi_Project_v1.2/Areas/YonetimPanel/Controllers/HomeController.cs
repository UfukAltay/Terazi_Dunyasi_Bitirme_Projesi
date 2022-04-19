using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Helpers;

namespace Terazi_Dunyasi_Project_v12.Areas.YonetimPanel.Controllers
{
    public class HomeController : Controller
    {
        // GET: YonetimPanel/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}