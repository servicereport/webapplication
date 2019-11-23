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
    public class DominiosController : Controller
    {
        private AuditoriaContext db = new AuditoriaContext();

        // GET: ISO/Dominios
        public ActionResult Index()
        {
            return View(db.Dominios.ToList());
        }


        // GET: ISO/Dominios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dominio dominio = db.Dominios.Find(id);
            if (dominio == null)
            {
                return HttpNotFound();
            }
            return View(dominio);
        }

        // GET: ISO/Dominios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ISO/Dominios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreDominio,Identificador,Consecutivo")] Dominio dominio)
        {
            if (ModelState.IsValid)
            {
                db.Dominios.Add(dominio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dominio);
        }

        // GET: ISO/Dominios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dominio dominio = db.Dominios.Find(id);
            if (dominio == null)
            {
                return HttpNotFound();
            }
            return View(dominio);
        }

        // POST: ISO/Dominios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreDominio,Identificador,Consecutivo")] Dominio dominio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dominio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dominio);
        }

        // GET: ISO/Dominios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dominio dominio = db.Dominios.Find(id);
            if (dominio == null)
            {
                return HttpNotFound();
            }
            return View(dominio);
        }

        // POST: ISO/Dominios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dominio dominio = db.Dominios.Find(id);
            db.Dominios.Remove(dominio);
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
