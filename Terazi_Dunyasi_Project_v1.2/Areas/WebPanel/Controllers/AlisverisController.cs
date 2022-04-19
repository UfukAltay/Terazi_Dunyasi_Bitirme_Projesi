using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Terazi_Dunyasi_Project_v12.Areas.WebPanel.Models;

namespace Terazi_Dunyasi_Project_v12.Areas.WebPanel.Controllers
{
    public class AlisverisController : Controller
    {
        TDPEntities db = new TDPEntities();
        // GET: WebArayuzu/Alisveris
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Kargo()
        {
            List<Kargolar> kargoList = db.Kargolars.ToList();

            return View(kargoList);
        }

        [Authorize]
        public ActionResult Ode()
        {
            string mustID = (string)Session["MusteriID"];

            List<Sepet> sepetim = (List<Sepet>)Session["Sepet"];


            Uyeler siparisVeren = db.Uyelers.Find(mustID);

            int kargocuID = int.Parse(Request.Form["ddlKargo"]);



            Siparisler ord = new Siparisler();
            ord.KargoID = kargocuID;
            ord.UyeID = siparisVeren.UyeID;
            //ord.GonderiAdresi = siparisVeren.UyeAdres;
            ord.SiparisTarihi = DateTime.Now;

            db.Siparislers.Add(ord);
            db.SaveChanges();

            foreach (var item in sepetim)
            {
                SiparisDetay detay = new SiparisDetay();
                detay.SiparisID = ord.SiparisID;
                detay.UrunID = item.UrunID;
                detay.Adet = (short)item.Adet;
                detay.Fiyat = db.Urunlers.Find(item.UrunID).UrunFiyati.Value;

                db.SiparisDetays.Add(detay);
                db.SaveChanges();
            }

            Session["Sepet"] = null;

            return View();
        }
    }
}