using MalzemeTakipMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MalzemeTakipMVC.Controllers
{
    public class PersonelController : Controller
    {
        private MalzemeTakipDB db = new MalzemeTakipDB();

        public ActionResult Index()
        {
            var personel = db.ENV_PERSONEL.Include(p => p.ENV_DEPARTMAN);
            return View(personel.Where(x => x.AktifPasif == true).ToList());
        }

        public ActionResult Ekle()
        {
            ViewBag.DepartmanId = new SelectList(db.ENV_DEPARTMAN, "Id", "Adi");
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(ENV_PERSONEL personel)
        {
            if (ModelState.IsValid)
            {
                db.ENV_PERSONEL.Add(personel);
                personel.AktifPasif = true;
                personel.IslemTarihi = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmanId = new SelectList(db.ENV_DEPARTMAN, "Id", "Adi", personel.DepartmanId);
            return View(personel);
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENV_PERSONEL personel = db.ENV_PERSONEL.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmanId = new SelectList(db.ENV_DEPARTMAN, "Id", "Adi", personel.DepartmanId);
            return View(personel);
        }

        [HttpPost]
        public ActionResult Duzenle(ENV_PERSONEL personel)
        {
            if (ModelState.IsValid)
            {
                personel.IslemTarihi = DateTime.Now;
                db.Entry(personel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmanId = new SelectList(db.ENV_DEPARTMAN, "Id", "Adi", personel.DepartmanId);
            return View(personel);
        }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENV_PERSONEL personel = db.ENV_PERSONEL.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }
            return View(personel);
        }

        [HttpPost, ActionName("Sil")]
        public ActionResult DeleteConfirmed(int id)
        {
            ENV_PERSONEL personel = db.ENV_PERSONEL.Find(id);
            personel.AktifPasif = false;
            db.Entry(personel).State = EntityState.Modified;

            //db.ENV_PERSONEL.Remove(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public void GridExportToExcel()
        {

            string dosyaAdi = "ornek_dosya_adi";
            //IQueryable<ENV_PERSONEL> lstPersonel = db.ENV_PERSONEL.Include(p => p.ENV_DEPARTMAN); // Buraya veritabanınından gelen herhangi bir dataSource gelebilir.( DataTable, DataSet, kendi oluşturduğunuz, herhangi bir ICollection tipinde entitiy model)

            List<ENV_PERSONEL> lstPersonel = db.ENV_PERSONEL.ToList();
            List<ENV_DEPARTMAN> lstDepartman = db.ENV_DEPARTMAN.ToList();

            List<ExcPersonel> lstExcPersonel = new List<ExcPersonel>();
            foreach (var personel in lstPersonel)
            {
                lstExcPersonel.Add(new ExcPersonel()
                {
                    Id = personel.Id,
                    AdiSoyadi = personel.AdiSoyadi,
                    DepartmanAdi = lstDepartman.Find(x => x.Id == personel.DepartmanId).Adi
                });
            }
            var grid = new GridView();

            //grid.DataSource = table.ToList();
            grid.DataSource = lstExcPersonel;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + dosyaAdi + ".xls");

            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();
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
