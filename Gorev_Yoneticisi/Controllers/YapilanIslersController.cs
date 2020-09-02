using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gorev_Yoneticisi.Models;
using Microsoft.AspNet.Identity;

namespace Gorev_Yoneticisi.Controllers
{
    public class YapilanIslersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: YapilanIslers
        //[Authorize] FilterConfig içerisine ekledim.
        public ActionResult Index()
        {
            return View();
        }
        private IEnumerable<YapilanIsler> GetYapilanIslers()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            IEnumerable<YapilanIsler> yapilanIslers = db.YapilanIslers.ToList().Where(x => x.User == currentUser);
            int bitenisler = 0;
            foreach(YapilanIsler yapilan in yapilanIslers)
            {
                if(yapilan.Durum)
                { 
                bitenisler++;
                }
            }
            ViewBag.Percent = Math.Round(100f * ((float)bitenisler / (float) yapilanIslers.Count()));
            return yapilanIslers;
        }
        public ActionResult DerleYapilanisTable()
        {
            return PartialView("_Yapilanistable",GetYapilanIslers());
        }
        // GET: YapilanIslers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YapilanIsler yapilanIsler = db.YapilanIslers.Find(id);
            if (yapilanIsler == null)
            {
                return HttpNotFound();
            }
            return View(yapilanIsler);
        }

        // GET: YapilanIslers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YapilanIslers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Aciklama,DetayliPlanlama,Durum")] YapilanIsler yapilanIsler)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                yapilanIsler.User = currentUser;

                db.YapilanIslers.Add(yapilanIsler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yapilanIsler);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "Id,Aciklama,DetayliPlanlama")] YapilanIsler yapilanIsler)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                yapilanIsler.User = currentUser;
                yapilanIsler.Durum = false;
                db.YapilanIslers.Add(yapilanIsler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView("_Yapilanistable", GetYapilanIslers());
        }
        // GET: YapilanIslers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YapilanIsler yapilanIsler = db.YapilanIslers.Find(id);
            if (yapilanIsler == null)
            {
                return HttpNotFound();
            }
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            if(yapilanIsler.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(yapilanIsler);
        }

        // POST: YapilanIslers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Aciklama,DetayliPlanlama,Durum")] YapilanIsler yapilanIsler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yapilanIsler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yapilanIsler);
        }
        [HttpPost]
        public ActionResult AJAXEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YapilanIsler yapilanIsler = db.YapilanIslers.Find(id);
            if (yapilanIsler == null)
            {
                return HttpNotFound();
            }
            else
            {
                yapilanIsler.Durum = value;
                db.Entry(yapilanIsler).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Yapilanistable", GetYapilanIslers());
            }
            
        }
        // GET: YapilanIslers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YapilanIsler yapilanIsler = db.YapilanIslers.Find(id);
            if (yapilanIsler == null)
            {
                return HttpNotFound();
            }
            return View(yapilanIsler);
        }

        // POST: YapilanIslers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YapilanIsler yapilanIsler = db.YapilanIslers.Find(id);
            db.YapilanIslers.Remove(yapilanIsler);
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
