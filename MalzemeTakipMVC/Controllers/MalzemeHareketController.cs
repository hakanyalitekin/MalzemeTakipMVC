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
    public class MalzemeHareketController : Controller
    {
        private MalzemeTakipDB db = new MalzemeTakipDB();

        public ActionResult Index()
        {
            var malzemeHareket = db.ENV_MALZEMEHAREKET.Include(m => m.ENV_MALZEME).Include(m => m.ENV_PERSONEL).Include(m => m.ENV_PERSONEL1);
            ViewBag.SayfaNo = 15;
            return View(malzemeHareket.ToList());
        }
        [HttpPost]
        public ActionResult Index(string TumSayfa)
        {
            var malzemeHareket = db.ENV_MALZEMEHAREKET.Include(m => m.ENV_MALZEME).Include(m => m.ENV_PERSONEL).Include(m => m.ENV_PERSONEL1);
            ViewBag.SayfaNo = Convert.ToInt32(TumSayfa);
            return View(malzemeHareket.ToList());
        }

        public ActionResult Detay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENV_MALZEMEHAREKET malzemeHareket = db.ENV_MALZEMEHAREKET.Find(id);
            if (malzemeHareket == null)
            {
                return HttpNotFound();
            }
            return View(malzemeHareket);
        }



        public ActionResult AtamaYap()
        {
            ViewBag.MalzemeId = new SelectList(db.ENV_MALZEME, "Id", "Adi");
            ViewBag.KaynakPersonelId = new SelectList(db.ENV_PERSONEL.Where(x=> x.AktifPasif == true), "Id", "AdiSoyadi");
            ViewBag.HedefPersonalId = new SelectList(db.ENV_PERSONEL.Where(x=> x.AktifPasif==true), "Id", "AdiSoyadi");
            return View();
        }





        // POST: MalzemeHareket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AtamaYap([Bind(Include = "Id,MalzemeId,KaynakPersonelId,HedefPersonalId,Adet,Acıklama,IslemTarihi")] ENV_MALZEMEHAREKET malzemeHareket)
        {
            if (ModelState.IsValid)
            {
                // TODO: Adet eksiye dusunce patlatmak icin burada transaction yapisi kullanilmali BEGIN-COMMIT-ROLLBACK.
                db.ENV_MALZEMEHAREKET.Add(malzemeHareket);
                malzemeHareket.IslemTarihi = DateTime.Now;
                db.SaveChanges();

                // Hedef personelin adedini duruma gore guncellemeliyiz.
                ENV_MALZEMEPERSONEL objEnvMalzemePersonelHedef = db.ENV_MALZEMEPERSONEL.FirstOrDefault(x => x.MalzemeId == malzemeHareket.MalzemeId && x.PersonelId == malzemeHareket.HedefPersonalId);
                if (objEnvMalzemePersonelHedef == null)
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
                    objEnvMalzemePersonelHedef.Adet += malzemeHareket.Adet;
                    objEnvMalzemePersonelHedef.IslemTarihi = DateTime.Now;
                    db.Entry(objEnvMalzemePersonelHedef).State = EntityState.Modified;
                    db.SaveChanges();
                }
                // Kaynaktan eksilteme..
                ENV_MALZEMEPERSONEL objEnvMalzemePersonelKaynak = db.ENV_MALZEMEPERSONEL.FirstOrDefault(x => x.MalzemeId == malzemeHareket.MalzemeId && x.PersonelId == malzemeHareket.KaynakPersonelId);
                objEnvMalzemePersonelKaynak.Adet -= malzemeHareket.Adet;
                objEnvMalzemePersonelKaynak.IslemTarihi = DateTime.Now;
                db.Entry(objEnvMalzemePersonelKaynak).State = EntityState.Modified;
                db.SaveChanges();
                                
                return RedirectToAction("Index");
            }

            ViewBag.MalzemeId = new SelectList(db.ENV_MALZEME, "Id", "Adi", malzemeHareket.MalzemeId);
            ViewBag.KaynakPersonelId = new SelectList(db.ENV_PERSONEL, "Id", "AdiSoyadi", malzemeHareket.KaynakPersonelId);
            ViewBag.HedefPersonalId = new SelectList(db.ENV_PERSONEL, "Id", "AdiSoyadi", malzemeHareket.HedefPersonalId);
            return View(malzemeHareket);
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENV_MALZEMEHAREKET malzemeHareket = db.ENV_MALZEMEHAREKET.Find(id);
            if (malzemeHareket == null)
            {
                return HttpNotFound();
            }
            ViewBag.MalzemeId = new SelectList(db.ENV_MALZEME, "Id", "Adi", malzemeHareket.MalzemeId);
            ViewBag.KaynakPersonelId = new SelectList(db.ENV_PERSONEL, "Id", "AdiSoyadi", malzemeHareket.KaynakPersonelId);
            ViewBag.HedefPersonalId = new SelectList(db.ENV_PERSONEL, "Id", "AdiSoyadi", malzemeHareket.HedefPersonalId);
            return View(malzemeHareket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle([Bind(Include = "Id,MalzemeId,KaynakPersonelId,HedefPersonalId,Adet,Acıklama,IslemTarihi")] ENV_MALZEMEHAREKET malzemeHareket)
        {
            if (ModelState.IsValid)
            {
                malzemeHareket.IslemTarihi = DateTime.Now;

                db.Entry(malzemeHareket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MalzemeId = new SelectList(db.ENV_MALZEME, "Id", "Adi", malzemeHareket.MalzemeId);
            ViewBag.KaynakPersonelId = new SelectList(db.ENV_PERSONEL, "Id", "AdiSoyadi", malzemeHareket.KaynakPersonelId);
            ViewBag.HedefPersonalId = new SelectList(db.ENV_PERSONEL, "Id", "AdiSoyadi", malzemeHareket.HedefPersonalId);
            return View(malzemeHareket);
        }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENV_MALZEMEHAREKET malzemeHareket = db.ENV_MALZEMEHAREKET.Find(id);
            if (malzemeHareket == null)
            {
                return HttpNotFound();
            }
            return View(malzemeHareket);
        }

        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ENV_MALZEMEHAREKET malzemeHareket = db.ENV_MALZEMEHAREKET.Find(id);
            db.ENV_MALZEMEHAREKET.Remove(malzemeHareket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Kişiye Göre Malzeme Getirme Ekranı
        public class MalzemeDTO
        {
            public int Id { get; set; }
            public string Adi { get; set; }

        }

        public ActionResult MalzemeGetir(int per_id)
        {
            var list = (from p in db.ENV_MALZEME
                        join g in db.ENV_MALZEMEPERSONEL on p.Id equals g.MalzemeId
                        where g.PersonelId == per_id && g.Adet > 0
                        select new MalzemeDTO
                        {
                            Id = p.Id,
                            Adi = p.Adi + "(" + p.SeriNo +")"
                            
                        }).ToList();

            return Json(list);
        }

        #endregion




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
