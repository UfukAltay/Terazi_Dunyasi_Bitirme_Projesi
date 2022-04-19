using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Terazi_Dunyasi_Project_v12.Areas.WebPanel.Models;

namespace Terazi_Dunyasi_Project_v12.Areas.WebPanel.Controllers
{
    public class SepetController : Controller
    {
        // GET: WebArayuzu/Sepet
        public ActionResult Index()
        {
            Sepet sep = new Sepet();

            List<Sepet> sepetList = sep.sepetList();

            return View(sepetList);
        }
        public int SepetGuncelle()
        {
            int UrunID, Adet;
            UrunID = Convert.ToInt32(Request.Form["hdnUrunID"]);
            Adet = Convert.ToInt32(Request.Form["hdnAdet"]);

            Sepet sep = new Sepet();
            sep.Adet = Adet;
            sep.UrunID = UrunID;

            return sep.Guncelle();
        }

        public ActionResult SepetSil()
        {
            int UrunID = Convert.ToInt32(Request.Form["hdnUrunID"]);
            Sepet sep = new Sepet();
            sep.UrunID = UrunID;
            sep.Sil();


            return RedirectToAction("Index");
        }
    }
}