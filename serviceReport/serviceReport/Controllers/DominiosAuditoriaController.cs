using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using serviceReport.Models.AccessBD;
using serviceReport.Models.Auditoria;

namespace serviceReport.Areas.Auditoria.Controllers
{
    public class DominiosAuditoriaController : Controller
    {
        private AuditoriaContext db = new AuditoriaContext();

        // GET: Auditoria/DominiosAuditoria
        public ActionResult Index()
        {
            var dominiosAuditoria = db.DominiosAuditoria.Include(d => d.Auditoria).Include(d => d.Dominio);
            return View(dominiosAuditoria.ToList());
        }

        // GET: Auditoria/DominiosAuditoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DominiosAuditoria dominiosAuditoria = db.DominiosAuditoria.Find(id);
            if (dominiosAuditoria == null)
            {
                return HttpNotFound();
            }
            return View(dominiosAuditoria);
        }

        // GET: Auditoria/DominiosAuditoria/Create
        public ActionResult Create()
        {
            ViewBag.IdAuditoria = new SelectList(db.Auditorias, "Id", "Id");
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio");
            return View();
        }

        // POST: Auditoria/DominiosAuditoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdDominio,IdAuditoria")] DominiosAuditoria dominiosAuditoria)
        {
            if (ModelState.IsValid)
            {
                db.DominiosAuditoria.Add(dominiosAuditoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAuditoria = new SelectList(db.Auditorias, "Id", "Id", dominiosAuditoria.IdAuditoria);
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio", dominiosAuditoria.IdDominio);
            return View(dominiosAuditoria);
        }

        // GET: Auditoria/DominiosAuditoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DominiosAuditoria dominiosAuditoria = db.DominiosAuditoria.Find(id);
            if (dominiosAuditoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAuditoria = new SelectList(db.Auditorias, "Id", "Id", dominiosAuditoria.IdAuditoria);
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio", dominiosAuditoria.IdDominio);
            return View(dominiosAuditoria);
        }

        // POST: Auditoria/DominiosAuditoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdDominio,IdAuditoria")] DominiosAuditoria dominiosAuditoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dominiosAuditoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAuditoria = new SelectList(db.Auditorias, "Id", "Id", dominiosAuditoria.IdAuditoria);
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio", dominiosAuditoria.IdDominio);
            return View(dominiosAuditoria);
        }

        // GET: Auditoria/DominiosAuditoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DominiosAuditoria dominiosAuditoria = db.DominiosAuditoria.Find(id);
            if (dominiosAuditoria == null)
            {
                return HttpNotFound();
            }
            return View(dominiosAuditoria);
        }

        // POST: Auditoria/DominiosAuditoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DominiosAuditoria dominiosAuditoria = db.DominiosAuditoria.Find(id);
            db.DominiosAuditoria.Remove(dominiosAuditoria);
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
