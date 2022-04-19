using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Terazi_Dunyasi_Project_v12.Areas.WebPanel.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        TDPEntities db = new TDPEntities();

        public ActionResult YeniUyelik()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniUyelik(Uyeler model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Uyeler uye = new Uyeler();
            if (file != null)
            {
                //Sunucuya dosya kaydedilirken, sunucunun dosya sistemini, yolunu bilemeyeceğimiz için Server.MapPath() ile sitemizin ana dizinine gelmiş oluruz. Devamında da sitemizdeki yolu tanımlarız.
                file.SaveAs(Server.MapPath("~/Images/UyeFoto/") + file.FileName);
                uye.FotoYol = "/Images/UyeFoto/" + file.FileName;
            }
            uye.Ad = model.Ad;
            uye.Soyad = model.Soyad;
            uye.EPosta = model.EPosta;
            uye.Sifre = model.Sifre;
            uye.UyelikTarihi = DateTime.Now;

            db.Uyelers.Add(uye);
            db.SaveChanges();

            return RedirectToAction("UyelikBasarili");
        }

        public ActionResult UyelikBasarili()
        {
            return View();
        }

        public ActionResult GirisBasarili()
        {
            return PartialView();
        }

        public ActionResult UyeGiris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UyeGirisi()
        {
            //Request.Form["html elementinin name özelliği"] ile Post edilen formdaki elemanların değerlerini alabiliyoruz. Bu metod yalnızca Post ile çalışır.
            string posta = Request.Form["txtPosta"];
            string sifre = Request.Form["pswSifre"];
            if (String.IsNullOrEmpty(posta) && String.IsNullOrEmpty(sifre))
            {

                return Content("E-Posta adresinizi ve şifrenizi girmediniz.");
            }
            else if (String.IsNullOrEmpty(posta))
            {
                return Content("E-posta adresinizi girmediniz.");
            }
            else if (string.IsNullOrEmpty(sifre))
            {
                return Content("Şifrenizi girmediniz.");
            }
            else
            {
                
                var uye = (from i in db.Uyelers where i.Sifre == sifre && i.EPosta == posta select i).FirstOrDefault();

                if (uye == null) return Content("Kullanıcı adınızı ya da şifreyi hatalı girdiniz.");
                                
                Session["uyeId"] = uye.UyeID;
                Session["UyeAdi"] = uye.Ad;

                return RedirectToAction("GirisBasarili");
            }
        }

        public ActionResult UyeCikis()
        {
            Session["uyeId"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}