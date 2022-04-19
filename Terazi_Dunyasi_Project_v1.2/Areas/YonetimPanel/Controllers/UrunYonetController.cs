using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Terazi_Dunyasi_Project_v12;
using Terazi_Dunyasi_Project_v12.Areas.YonetimPanel.ViewModel;

namespace Terazi_Dunyasi_Project_v12.Areas.YonetimPanel.Controllers
{
    public class UrunYonetController : Controller
    {
        private TDPEntities db = new TDPEntities();

        // GET: YonetimPanel/UrunYonet
        public ActionResult Index()
        {
            var urunlers = db.Urunlers.Include(u => u.AltKategoriler).Include(u => u.Kategoriler);

            return View(urunlers.ToList());
        }

        // GET: YonetimPanel/UrunYonet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: YonetimPanel/UrunYonet/Create
        public ActionResult Create()
        {
            TDPEntities db = new TDPEntities();
            UrunViewModel model = new UrunViewModel();

            List<Kategoriler> katList = db.Kategorilers.ToList();

            model.KategoriListesi = (from kat in katList
                                     select new SelectListItem
                                     {
                                         Selected = false,
                                         Text = kat.KategoriAdi,
                                         Value = kat.KategoriID.ToString()
                                     }).ToList();

            model.KategoriListesi.Insert(0, new SelectListItem { Selected = true, Text = "Kategori Seçiniz", Value = "" });

            //ViewBag.AltKategoriID = new SelectList(db.AltKategorilers, "AltKategoriID", "AltKategoriAdi");
            //ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi");

            return View(model);
        }
        [HttpPost]
        public JsonResult GetAltKategoriList(int id)
        {
            TDPEntities db = new TDPEntities();
            UrunViewModel model = new UrunViewModel();

            List<AltKategoriler> gelenAltKategori = db.AltKategorilers.Where(m => m.KategoriID == id).ToList();

            List<SelectListItem> altkatlist = (from altkt in gelenAltKategori
                                               select new SelectListItem
                                               {
                                                   Selected = false,
                                                   Text = altkt.AltKategoriAdi,
                                                   Value = altkt.AltKategoriID.ToString()
                                               }).ToList();

            return Json(altkatlist, JsonRequestBehavior.AllowGet);
        }

        // POST: YonetimPanel/UrunYonet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunID,KategoriID,AltKategoriID,UrunAdi,UrunFiyati,StokDurumu,UrunAciklama,FotoYol")] Urunler urunler, HttpPostedFileBase file, UrunViewModel model)
        {
            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi", urunler.KategoriID);
            ViewBag.AltKategoriID = new SelectList(db.AltKategorilers, db.AltKategorilers.Where(x => x.KategoriID == urunler.KategoriID), "AltKategoriAdi");

            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    db.Urunlers.Add(urunler);
                    db.SaveChanges();
                }
                else if (file.ContentLength > 0)
                {
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");

                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);

                    string TamYolYeri = "/Images/UrunFoto/" + DosyaAdi + uzanti;

                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));

                    urunler.FotoYol = TamYolYeri;

                    db.Urunlers.Add(urunler);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(urunler);
        }

        // GET: YonetimPanel/UrunYonet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.AltKategoriID = new SelectList(db.AltKategorilers, "AltKategoriID", "AltKategoriAdi", urunler.AltKategoriID);
            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi", urunler.KategoriID);
            return View(urunler);
        }

        // POST: YonetimPanel/UrunYonet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunID,KategoriID,AltKategoriID,UrunAdi,UrunFiyati,StokDurumu,UrunAciklama,FotoYol")] Urunler urunler, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    db.Entry(urunler).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (file.ContentLength > 0)
                {
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");

                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);

                    string TamYolYeri = "/Images/UrunFoto/" + DosyaAdi + uzanti;

                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));

                    urunler.FotoYol = TamYolYeri;

                    db.Entry(urunler).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.AltKategoriID = new SelectList(db.AltKategorilers, "AltKategoriID", "AltKategoriAdi", urunler.AltKategoriID);
            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi", urunler.KategoriID);
            return View(urunler);
        }

        // GET: YonetimPanel/UrunYonet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: YonetimPanel/UrunYonet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var SilinecekResim = db.Urunlers.Find(id);
            db.Urunlers.Remove(SilinecekResim);

            if (System.IO.File.Exists(Server.MapPath(SilinecekResim.FotoYol)))
            {
                System.IO.File.Delete(Server.MapPath(SilinecekResim.FotoYol));
            }
            
            Urunler urunler = db.Urunlers.Find(id);
            db.Urunlers.Remove(urunler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
