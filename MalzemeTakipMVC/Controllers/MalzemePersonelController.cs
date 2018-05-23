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
    public class MalzemePersonelController : Controller
    {
        private MalzemeTakipDB db = new MalzemeTakipDB();

        // GET: MalzemePersonel
        public ActionResult Index()
        {
            var malzemePersonel = db.ENV_MALZEMEPERSONEL.Include(m => m.ENV_MALZEME).Include(m => m.ENV_PERSONEL).Where(x=> x.Adet >0);
            return View(malzemePersonel.ToList());
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
