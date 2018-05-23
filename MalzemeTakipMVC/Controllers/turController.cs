using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MalzemeTakipMVC.Models;

namespace MalzemeTakipMVC.Controllers
{
    public class turController : Controller
    {
        private MalzemeTakipDB db = new MalzemeTakipDB();

        public ActionResult Index()
        {
            return View(db.WEB_PORTALPARAMETRE.Where(x=> x.ParametreAdi== "env_AltTur" ||  x.ParametreAdi == "env_Tur").ToList());
        }

        public ActionResult Ekle()
        {

            var Turler = new List<SelectListItem>();
            Turler.Add(new SelectListItem
            { Text = "Tür", Value = "env_Tur" });
            Turler.Add(new SelectListItem
            { Text = "Alt Tür", Value = "env_AltTur" });

            ViewBag.ParametreAdi = Turler;


            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(WEB_PORTALPARAMETRE wEB_PORTALPARAMETRE)
        {
            if (ModelState.IsValid)
            {
                
                wEB_PORTALPARAMETRE.IslemKullaniciId = 1;
                wEB_PORTALPARAMETRE.GecerlilikBaslangicTarihi = DateTime.Now;
                wEB_PORTALPARAMETRE.GecerlilikBitisTarihi = DateTime.Now;
                wEB_PORTALPARAMETRE.IslemTarihi = DateTime.Now;
                db.WEB_PORTALPARAMETRE.Add(wEB_PORTALPARAMETRE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var Turler = new List<SelectListItem>();
            Turler.Add(new SelectListItem
            { Text = "env_Tur", Value = "env_Tur" });
            Turler.Add(new SelectListItem
            { Text = "env_AltTur", Value = "env_AltTur" });
            ViewBag.TurSeviyesi = Turler;

            return View(wEB_PORTALPARAMETRE);
        }

        // GET: WEB_PORTALPARAMETRE/Edit/5
        public ActionResult Duzenle(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WEB_PORTALPARAMETRE wEB_PORTALPARAMETRE = db.WEB_PORTALPARAMETRE.Find(id);
            if (wEB_PORTALPARAMETRE == null)
            {
                return HttpNotFound();
            }
            return View(wEB_PORTALPARAMETRE);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle([Bind(Include = "Id,ParametreAdi,ParametreKodu,BirinciDegeri,IkinciDeger,UcuncuDeger,Aciklama,GecerlilikBaslangicTarihi,GecerlilikBitisTarihi,IslemKullaniciId,IslemTarihi")] WEB_PORTALPARAMETRE wEB_PORTALPARAMETRE)
        {
            if (ModelState.IsValid)
            {
                wEB_PORTALPARAMETRE.IslemKullaniciId = 1;
                wEB_PORTALPARAMETRE.GecerlilikBaslangicTarihi = DateTime.Now;
                wEB_PORTALPARAMETRE.GecerlilikBitisTarihi = DateTime.Now;
                wEB_PORTALPARAMETRE.IslemTarihi = DateTime.Now;
                db.Entry(wEB_PORTALPARAMETRE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wEB_PORTALPARAMETRE);
        }

        public ActionResult Sil(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WEB_PORTALPARAMETRE wEB_PORTALPARAMETRE = db.WEB_PORTALPARAMETRE.Find(id);
            if (wEB_PORTALPARAMETRE == null)
            {
                return HttpNotFound();
            }
            return View(wEB_PORTALPARAMETRE);
        }

        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            WEB_PORTALPARAMETRE wEB_PORTALPARAMETRE = db.WEB_PORTALPARAMETRE.Find(id);
            db.WEB_PORTALPARAMETRE.Remove(wEB_PORTALPARAMETRE);
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
