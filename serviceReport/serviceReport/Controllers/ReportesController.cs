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

namespace serviceReport.Controllers
{
    public class ReportesController : Controller
    {
        private AuditoriaContext db = new AuditoriaContext();

        // GET: Reportes
        public ActionResult Index()
        {
            var auditorias = db.Auditorias.Include(a => a.Auditor).Include(a => a.EmpresaAuditoria).Include(a => a.Estado);
            return View(auditorias.ToList());
        }

        // GET: Reportes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            return View(auditoria);
        }

        // GET: Reportes/Create
        public ActionResult Create()
        {
            ViewBag.IdAuditor = new SelectList(db.Usuarios, "Id", "UserName");
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEntidad");
            ViewBag.IdEstadoAuditoria = new SelectList(db.Estados, "Id", "Estado");
            return View();
        }

        // POST: Reportes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdEmpresa,IdAuditor,FechaCreacion,FechaAuditoria,IdEstadoAuditoria")] Auditoria auditoria)
        {
            if (ModelState.IsValid)
            {
                db.Auditorias.Add(auditoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAuditor = new SelectList(db.Usuarios, "Id", "UserName", auditoria.IdAuditor);
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEntidad", auditoria.IdEmpresa);
            ViewBag.IdEstadoAuditoria = new SelectList(db.Estados, "Id", "Estado", auditoria.IdEstadoAuditoria);
            return View(auditoria);
        }

        // GET: Reportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAuditor = new SelectList(db.Usuarios, "Id", "UserName", auditoria.IdAuditor);
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEntidad", auditoria.IdEmpresa);
            ViewBag.IdEstadoAuditoria = new SelectList(db.Estados, "Id", "Estado", auditoria.IdEstadoAuditoria);
            return View(auditoria);
        }

        // POST: Reportes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdEmpresa,IdAuditor,FechaCreacion,FechaAuditoria,IdEstadoAuditoria")] Auditoria auditoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auditoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAuditor = new SelectList(db.Usuarios, "Id", "UserName", auditoria.IdAuditor);
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEntidad", auditoria.IdEmpresa);
            ViewBag.IdEstadoAuditoria = new SelectList(db.Estados, "Id", "Estado", auditoria.IdEstadoAuditoria);
            return View(auditoria);
        }

        // GET: Reportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            return View(auditoria);
        }

        // POST: Reportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Auditoria auditoria = db.Auditorias.Find(id);
            db.Auditorias.Remove(auditoria);
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
