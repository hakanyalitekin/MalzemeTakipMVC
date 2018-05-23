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
    public class MalzemeController : Controller
    {
        private MalzemeTakipDB db = new MalzemeTakipDB();

        public ActionResult Index()
        {
            return View(db.ENV_MALZEME.ToList());
        }


        public ActionResult Ekle()
        {

            ViewBag.Tur = new SelectList(db.WEB_PORTALPARAMETRE.Where(x=> x.ParametreAdi=="env_Tur"), "ParametreKodu", "BirinciDegeri");
            ViewBag.AltTur = new SelectList(db.WEB_PORTALPARAMETRE.Where(x => x.ParametreAdi == "env_AltTur"), "ParametreKodu", "BirinciDegeri");

            return View();
        }


        #region Türe Göre Alt Tür Getirme Ekranı
        public class TurDTO
        {
            public string PrametreKodu { get; set; }
            public string BirinciDegeri { get; set; }
        }

        public ActionResult TurGetir(string _parametrekodu)
        {
            var list = (from p in db.WEB_PORTALPARAMETRE
                        where p.UcuncuDeger == _parametrekodu
                        select new TurDTO
                        {
                            PrametreKodu = p.ParametreKodu ,
                            BirinciDegeri = p.BirinciDegeri 

                        }).ToList();

            return Json(list);
        }

        #endregion


        [HttpPost]
        public ActionResult Ekle(ENV_MALZEME malzeme)
        {
            if (ModelState.IsValid)
            {
                db.ENV_MALZEME.Add(malzeme);
                malzeme.IslemTarihi = DateTime.Now;
                malzeme.AktifPasif = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(malzeme);
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENV_MALZEME malzeme = db.ENV_MALZEME.Find(id);
            if (malzeme == null)
            {
                return HttpNotFound();
            }
            return View(malzeme);
        }

        [HttpPost]
        public ActionResult Duzenle(ENV_MALZEME malzeme)
        {
            if (ModelState.IsValid)
            {
                malzeme.IslemTarihi = DateTime.Now;
                db.Entry(malzeme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(malzeme);
        }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENV_MALZEME malzeme = db.ENV_MALZEME.Find(id);
            if (malzeme == null)
            {
                return HttpNotFound();
            }
            return View(malzeme);
        }

        [HttpPost, ActionName("Sil")]
        public ActionResult DeleteConfirmed(int id)
        {
            ENV_MALZEME malzeme = db.ENV_MALZEME.Find(id);

            int id_ = id;
            var count = db.ENV_MALZEMEPERSONEL.Where(x => x.MalzemeId == id && x.PersonelId != 1).Count();
           
            if (count == 0) 
            {
                db.ENV_MALZEME.Remove(malzeme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
     
        }

        public ActionResult MalKabul()
        {
            List<SelectListItem> malzemeList = 
                (
                from malzeme in db.ENV_MALZEME.ToList()
                select new SelectListItem()
                {
                    Text = malzeme.Adi + " " + "("+ malzeme.SeriNo + ")",
                    Value = malzeme.Id.ToString()
                }).ToList();
            //ViewBag.MalzemeId = new SelectList(db.ENV_MALZEME, "Id", "Adi");
            ViewBag.MalzemeId = malzemeList;
            return View();
        }

        [HttpPost, ActionName("MalKabul")]
        public ActionResult MalKabul(ENV_MALZEMEHAREKET malzemeHareket)
        {
            if (ModelState.IsValid)
            {
                db.ENV_MALZEMEHAREKET.Add(malzemeHareket);
                malzemeHareket.IslemTarihi = DateTime.Now;
                malzemeHareket.HedefPersonalId = 1;
                malzemeHareket.KaynakPersonelId = 1;
                if (malzemeHareket.Acıklama == "" && malzemeHareket.Acıklama == null)
                    malzemeHareket.Acıklama = "Malkabul Hareketi...";


                ENV_MALZEMEPERSONEL objEnvMalzemePersonel = db.ENV_MALZEMEPERSONEL.FirstOrDefault(x=>x.MalzemeId == malzemeHareket.MalzemeId && x.PersonelId == malzemeHareket.HedefPersonalId) ;
                if (objEnvMalzemePersonel == null)
                {
                    ENV_MALZEMEPERSONEL malper = db.ENV_MALZEMEPERSONEL.Create();
                    db.ENV_MALZEMEPERSONEL.Add(malper);
                    malper.MalzemeId = malzemeHareket.MalzemeId;
                    malper.IslemTarihi = malzemeHareket.IslemTarihi;
                    malper.Adet = malzemeHareket.Adet;
                    malper.PersonelId = malzemeHareket.HedefPersonalId;
                    db.SaveChanges();
                }
                else
                {
                    objEnvMalzemePersonel.Adet += malzemeHareket.Adet;
                    objEnvMalzemePersonel.IslemTarihi = DateTime.Now;
                    db.Entry(objEnvMalzemePersonel).State = EntityState.Modified;
                    db.SaveChanges();                    
                }
                return RedirectToAction("Index");
            }

            return View(malzemeHareket);
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
