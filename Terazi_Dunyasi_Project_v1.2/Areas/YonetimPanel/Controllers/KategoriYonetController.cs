using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Terazi_Dunyasi_Project_v12;
using System.Web.Helpers;
using System.IO;

namespace Terazi_Dunyasi_Project_v12.Areas.YonetimPanel.Controllers
{
    public class KategoriYonetController : Controller
    {
        private TDPEntities db = new TDPEntities();

        // GET: YonetimPanel/KategoriYonet
        public ActionResult Index()
        {
            return View(db.Kategorilers.ToList());
        }

        // GET: YonetimPanel/KategoriYonet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = db.Kategorilers.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // GET: YonetimPanel/KategoriYonet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YonetimPanel/KategoriYonet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KategoriID,KategoriAdi,KategoriAciklama,FotoYol")] Kategoriler kategoriler, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    db.Kategorilers.Add(kategoriler);
                    db.SaveChanges();
                }
                else if (file.ContentLength > 0)
                {
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");

                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);

                    string TamYolYeri = "/Images/KategoriFoto/" + DosyaAdi + uzanti;

                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));

                    kategoriler.FotoYol = TamYolYeri;                                    
                                        
                    db.Kategorilers.Add(kategoriler);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(kategoriler);
        }

        // GET: YonetimPanel/KategoriYonet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = db.Kategorilers.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // POST: YonetimPanel/KategoriYonet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategoriID,KategoriAdi,KategoriAciklama,FotoYol")] Kategoriler kategoriler,  HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    db.Entry(kategoriler).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (file.ContentLength > 0)
                {
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");

                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);

                    string TamYolYeri = "/Images/KategoriFoto/" + DosyaAdi + uzanti;

                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));

                    kategoriler.FotoYol = TamYolYeri;

                    db.Entry(kategoriler).State = EntityState.Modified;
                    db.SaveChanges();
                }
                                
                return RedirectToAction("Index");
            }
            return View(kategoriler);
        }

        // GET: YonetimPanel/KategoriYonet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = db.Kategorilers.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // POST: YonetimPanel/KategoriYonet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var SilinecekResim = db.Kategorilers.Find(id);
            db.Kategorilers.Remove(SilinecekResim);

            if (System.IO.File.Exists(Server.MapPath(SilinecekResim.FotoYol)))
            {
                System.IO.File.Delete(Server.MapPath(SilinecekResim.FotoYol));
            }

            Kategoriler kategoriler = db.Kategorilers.Find(id);
            db.Kategorilers.Remove(kategoriler);
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
