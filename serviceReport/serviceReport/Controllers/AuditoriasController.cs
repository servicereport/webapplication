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
    public class AuditoriasController : Controller
    {
        private AuditoriaContext db = new AuditoriaContext();

        // GET: Auditoria/Auditorias
        public ActionResult Index(int? idEmpresa)
        {
            if (idEmpresa == null)
            {
                if (Session["idEmpresa"] != null)
                    idEmpresa = Convert.ToInt32(Session["idEmpresa"]);
                else
                    return RedirectToAction("Index", "Empresas");
            }
            else
                Session["idEmpresa"] = idEmpresa;

            var empresa = db.Empresas.Where(e => e.Id == idEmpresa).FirstOrDefault();
            empresa.Auditorias = db.Auditorias.Where(a => a.IdEmpresa == idEmpresa).ToList();

            var dominios = db.Dominios.Where(d=> d.Activo == true).ToList();
            
            foreach (var item in empresa.Auditorias)
            {
                item.Auditor = db.Usuarios.Where(a => a.Id == item.IdAuditor).FirstOrDefault();
                item.Estado = db.Estados.Where(a => a.Id==item.IdEstadoAuditoria).FirstOrDefault();
                var dominiosActuales = db.DominiosAuditoria.Where(d => d.IdAuditoria == item.Id).ToList();

                foreach (var dominio in dominios)
                {
                    dominio.Asociado = dominiosActuales.Where(d => d.IdDominio == dominio.Id).Any();
                    item.Dominios..Add(dominio);
                }
            }

            return View(empresa);
        }

        // GET: Auditoria/Auditorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            return View(auditoria);
        }

        // GET: Auditoria/Auditorias/Create
        public ActionResult Create()
        {
            ViewBag.IdAuditor = new SelectList(db.Usuarios, "Id", "UserName");
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEntidad");
            ViewBag.IdEstadoAuditoria = new SelectList(db.Estados, "Id", "Estado");
            return View();
        }

        // POST: Auditoria/Auditorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdEmpresa,IdAuditor,FechaCreacion,FechaAuditoria,IdEstadoAuditoria")] Models.Auditoria.Auditoria auditoria)
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

        // GET: Auditoria/Auditorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAuditor = new SelectList(db.Usuarios, "Id", "UserName", auditoria.IdAuditor);
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEntidad", auditoria.IdEmpresa);
            ViewBag.IdEstadoAuditoria = new SelectList(db.Estados, "Id", "Estado", auditoria.IdEstadoAuditoria);
            return View(auditoria);
        }

        // POST: Auditoria/Auditorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdEmpresa,IdAuditor,FechaCreacion,FechaAuditoria,IdEstadoAuditoria")] Models.Auditoria.Auditoria auditoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auditoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Auditorias");
            }
            ViewBag.IdAuditor = new SelectList(db.Usuarios, "Id", "UserName", auditoria.IdAuditor);
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEntidad", auditoria.IdEmpresa);
            ViewBag.IdEstadoAuditoria = new SelectList(db.Estados, "Id", "Estado", auditoria.IdEstadoAuditoria);
            return View(auditoria);
        }

        // GET: Auditoria/Auditorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            return View(auditoria);
        }

        // POST: Auditoria/Auditorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var auditoria = db.Auditorias.Find(id);
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
