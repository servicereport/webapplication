using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using serviceReport.Models;
using serviceReport.Models.AccessBD;
using serviceReport.Models.ISO;

namespace serviceReport.Areas.ISO.Controllers
{
    public class RolesController : Controller
    {
        private AuditoriaContext db = new AuditoriaContext();

        // GET: ISO/Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: ISO/Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roll roll = db.Roles.Find(id);
            if (roll == null)
            {
                return HttpNotFound();
            }
            return View(roll);
        }

        // GET: ISO/Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ISO/Roles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RollName")] Roll roll)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(roll);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roll);
        }

        // GET: ISO/Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roll roll = db.Roles.Find(id);
            if (roll == null)
            {
                return HttpNotFound();
            }
            return View(roll);
        }

        // POST: ISO/Roles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RollName")] Roll roll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roll);
        }

        // GET: ISO/Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roll roll = db.Roles.Find(id);
            if (roll == null)
            {
                return HttpNotFound();
            }
            return View(roll);
        }

        // POST: ISO/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roll roll = db.Roles.Find(id);
            db.Roles.Remove(roll);
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
