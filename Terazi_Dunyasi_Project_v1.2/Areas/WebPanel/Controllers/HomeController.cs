using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Terazi_Dunyasi_Project_v12.Areas.WebPanel.Models;

namespace Terazi_Dunyasi_Project_v12.Areas.WebPanel.Controllers
{
    public class HomeController : Controller
    {
        TDPEntities db = new TDPEntities();

        // GET: WebPanel/Home
        public ActionResult Index()
        {
            List<Urunler> urunler = db.Urunlers.Take(5).OrderBy(c => c.UrunFiyati).ToList();
            return View(urunler);
        }

        public ActionResult KategoriListesi()
        {
            List<Kategoriler> katList = db.Kategorilers.ToList();
            return PartialView(katList);
        }

        public ActionResult AltKategoriListesi()
        {
            List<AltKategoriler> altkatlist = db.AltKategorilers.ToList();
            return PartialView(altkatlist);
        }

        public int SepeteAt()
        {
            int UrunID = int.Parse(Request.Form["hdnUrunID"]);

            Sepet sep = new Sepet();
            sep.Adet = 1;
            sep.UrunID = UrunID;

            int i = sep.Ekle();
            return i;
        }

        public string SepetCount()
        {
            Sepet sep = new Sepet();

            return sep.sepetList().Sum(c => c.Adet).ToString();
        }
    }
}