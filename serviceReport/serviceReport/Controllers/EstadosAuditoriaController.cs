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
    public class EstadosAuditoriaController : Controller
    {
        private AuditoriaContext db = new AuditoriaContext();

        // GET: Auditoria/EstadosAuditoria
        public ActionResult Index()
        {
            return View(db.Estados.ToList());
        }

        // GET: Auditoria/EstadosAuditoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoAuditoria estadoAuditoria = db.Estados.Find(id);
            if (estadoAuditoria == null)
            {
                return HttpNotFound();
            }
            return View(estadoAuditoria);
        }

        // GET: Auditoria/EstadosAuditoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auditoria/EstadosAuditoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Estado")] EstadoAuditoria estadoAuditoria)
        {
            if (ModelState.IsValid)
            {
                db.Estados.Add(estadoAuditoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoAuditoria);
        }

        // GET: Auditoria/EstadosAuditoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoAuditoria estadoAuditoria = db.Estados.Find(id);
            if (estadoAuditoria == null)
            {
                return HttpNotFound();
            }
            return View(estadoAuditoria);
        }

        // POST: Auditoria/EstadosAuditoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Estado")] EstadoAuditoria estadoAuditoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoAuditoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoAuditoria);
        }

        // GET: Auditoria/EstadosAuditoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoAuditoria estadoAuditoria = db.Estados.Find(id);
            if (estadoAuditoria == null)
            {
                return HttpNotFound();
            }
            return View(estadoAuditoria);
        }

        // POST: Auditoria/EstadosAuditoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoAuditoria estadoAuditoria = db.Estados.Find(id);
            db.Estados.Remove(estadoAuditoria);
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
