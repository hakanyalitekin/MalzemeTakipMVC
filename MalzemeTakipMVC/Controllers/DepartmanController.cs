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
    public class DepartmanController : Controller
    {
        private MalzemeTakipDB db = new MalzemeTakipDB();

        public ActionResult Index()
        {
            return View(db.ENV_DEPARTMAN.Where(x=>x.AktifPasif==true).ToList());
        }


        public ActionResult Ekle()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult Ekle(ENV_DEPARTMAN departman)
        {
            if (ModelState.IsValid)
            {
                departman.AktifPasif = true;
                departman.IslemTarihi = DateTime.Now;
                db.ENV_DEPARTMAN.Add(departman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departman);
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENV_DEPARTMAN departman = db.ENV_DEPARTMAN.Find(id);
            if (departman == null)
            {
                return HttpNotFound();
            }
            return View(departman);
        }

        [HttpPost]
        public ActionResult Duzenle(ENV_DEPARTMAN departman)
        {
            if (ModelState.IsValid)
            {
                departman.IslemTarihi = DateTime.Now;
                db.Entry(departman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departman);
        }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENV_DEPARTMAN departman = db.ENV_DEPARTMAN.Find(id);
            if (departman == null)
            {
                return HttpNotFound();
            }
            return View(departman);
        }

        [HttpPost, ActionName("Sil")]
        public ActionResult DeleteConfirmed(int id)
        {
            ENV_DEPARTMAN departman = db.ENV_DEPARTMAN.Find(id);
            departman.AktifPasif = false;
            db.Entry(departman).State = EntityState.Modified;
            //db.ENV_DEPARTMAN.Remove(departman);
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
