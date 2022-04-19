using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Terazi_Dunyasi_Project_v12.Areas.WebPanel.Models;

namespace Terazi_Dunyasi_Project_v12.Areas.WebPanel.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: WebArayuzu/Urunler
        TDPEntities db = new TDPEntities();
        public ActionResult Index(int? ID)
        {
            if (!ID.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Urunler> urunler = db.Urunlers.Where(c => c.UrunID == ID).ToList();

            return View(urunler);
        }

        public ActionResult TumUrunler()
        {

            List<Urunler> urunler = db.Urunlers.ToList();

            return View(urunler);
        }
    }
}