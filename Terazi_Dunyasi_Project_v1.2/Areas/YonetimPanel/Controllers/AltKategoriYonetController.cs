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

namespace Terazi_Dunyasi_Project_v12.Areas.YonetimPanel.Controllers
{
    public class AltKategoriYonetController : Controller
    {
        private TDPEntities db = new TDPEntities();

        // GET: YonetimPanel/AltKategoriYonet
        public ActionResult Index()
        {
            var altKategorilers = db.AltKategorilers.Include(a => a.Kategoriler);
            return View(altKategorilers.ToList());
        }

        // GET: YonetimPanel/AltKategoriYonet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltKategoriler altKategoriler = db.AltKategorilers.Find(id);
            if (altKategoriler == null)
            {
                return HttpNotFound();
            }
            return View(altKategoriler);
        }

        // GET: YonetimPanel/AltKategoriYonet/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi");
            return View();
        }

        // POST: YonetimPanel/AltKategoriYonet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AltKategoriID,KategoriID,AltKategoriAdi,AltKategoriAciklama,FotoYol")] AltKategoriler altKategoriler, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    db.AltKategorilers.Add(altKategoriler);
                    db.SaveChanges();
                }
                else if (file.ContentLength > 0)
                {
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");

                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);

                    string TamYolYeri = "/Images/AltKategoriFoto/" + DosyaAdi + uzanti;

                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));

                    altKategoriler.FotoYol = TamYolYeri;

                    db.AltKategorilers.Add(altKategoriler);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi", altKategoriler.KategoriID);
            return View(altKategoriler);
        }

        // GET: YonetimPanel/AltKategoriYonet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltKategoriler altKategoriler = db.AltKategorilers.Find(id);
            if (altKategoriler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi", altKategoriler.KategoriID);
            return View(altKategoriler);
        }

        // POST: YonetimPanel/AltKategoriYonet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AltKategoriID,KategoriID,AltKategoriAdi,AltKategoriAciklama,FotoYol")] AltKategoriler altKategoriler, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    db.Entry(altKategoriler).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (file.ContentLength > 0)
                {
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");

                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);

                    string TamYolYeri = "/Images/AltKategoriFoto/" + DosyaAdi + uzanti;

                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));

                    altKategoriler.FotoYol = TamYolYeri;

                    db.Entry(altKategoriler).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi", altKategoriler.KategoriID);
            return View(altKategoriler);
        }

        // GET: YonetimPanel/AltKategoriYonet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltKategoriler altKategoriler = db.AltKategorilers.Find(id);
            if (altKategoriler == null)
            {
                return HttpNotFound();
            }
            return View(altKategoriler);
        }

        // POST: YonetimPanel/AltKategoriYonet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var SilinecekResim = db.AltKategorilers.Find(id);
            db.AltKategorilers.Remove(SilinecekResim);

            if (System.IO.File.Exists(Server.MapPath(SilinecekResim.FotoYol)))
            {
                System.IO.File.Delete(Server.MapPath(SilinecekResim.FotoYol));
            }

            AltKategoriler altKategoriler = db.AltKategorilers.Find(id);
            db.AltKategorilers.Remove(altKategoriler);
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
