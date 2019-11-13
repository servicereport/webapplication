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

        public ActionResult Lista(int? idEmpresa)
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
            var auditorias = db.Auditorias.Where(a => a.IdEmpresa == idEmpresa).ToList();
            foreach (var item in auditorias)
            {
                item.Auditor = db.Usuarios.Where(a => a.Id == item.IdAuditor).FirstOrDefault();
                item.Estado = db.Estados.Where(a => a.Id == item.IdEstadoAuditoria).FirstOrDefault();
                item.EmpresaAuditoria = db.Empresas.Find(idEmpresa);
            }
            return View(auditorias);
        }

        public ActionResult Configurar(int? idAuditoria)
        {
            var auditoria = db.Auditorias.Where(a => a.Id == idAuditoria).First();
            var dominios = db.Dominios.Where(d => d.Activo == true).ToList();

            auditoria.Auditor = db.Usuarios.Where(a => a.Id == auditoria.IdAuditor).FirstOrDefault();
            auditoria.Estado = db.Estados.Where(a => a.Id == auditoria.IdEstadoAuditoria).FirstOrDefault();
            auditoria.EmpresaAuditoria = db.Empresas.Where(a => a.Id == auditoria.IdEmpresa).FirstOrDefault();
            var dominiosActuales = db.DominiosAuditoria.Where(d => d.IdAuditoria == auditoria.Id).ToList();

            foreach (var dominio in dominios)
            {
                var asociado = dominiosActuales.Where(d => d.IdDominio == dominio.Id).Any();
                if (!asociado)
                {
                    if (auditoria.Dominios == null)
                        auditoria.Dominios = new List<DominiosAuditoria>();
                    auditoria.Dominios.Add(new DominiosAuditoria() { IdDominio = dominio.Id, Dominio = dominio, Asociado = asociado });
                }

                else
                    auditoria.Dominios.Where(d => d.IdDominio == dominio.Id).First().Asociado = true;
                //item.Id = dominio.Id;
            }

            return View(auditoria);
        }

        public ActionResult Confirmar(int idAuditoria)
        {
            var auditoria = db.Auditorias.Where(a => a.Id == idAuditoria).First();
            auditoria.IdEstadoAuditoria = 2;
            db.Entry(auditoria).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Lista", "Auditorias");
        }

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

            var dominios = db.Dominios.Where(d => d.Activo == true).ToList();

            foreach (var item in empresa.Auditorias)
            {
                item.Auditor = db.Usuarios.Where(a => a.Id == item.IdAuditor).FirstOrDefault();
                item.Estado = db.Estados.Where(a => a.Id == item.IdEstadoAuditoria).FirstOrDefault();
                var dominiosActuales = db.DominiosAuditoria.Where(d => d.IdAuditoria == item.Id).ToList();

                foreach (var dominio in dominios)
                {
                    var asociado = dominiosActuales.Where(d => d.IdDominio == dominio.Id).Any();
                    if (!asociado)
                    {
                        if (item.Dominios == null)
                            item.Dominios = new List<DominiosAuditoria>();
                        item.Dominios.Add(new DominiosAuditoria() { IdDominio = dominio.Id, Dominio = dominio, Asociado = asociado });
                    }

                    else
                        item.Dominios.Where(d => d.IdDominio == dominio.Id).First().Asociado = true;
                    //item.Id = dominio.Id;
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
        public ActionResult Create(int? idEmpresa)
        {
            ViewBag.IdAuditor = new SelectList(db.Usuarios, "Id", "UserName");
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEntidad", idEmpresa);
            return View();
        }

        // POST: Auditoria/Auditorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdEmpresa,IdAuditor,FechaAuditoria")] Models.Auditoria.Auditoria auditoria)
        {
            if (ModelState.IsValid)
            {
                auditoria.FechaCreacion = DateTime.Now;
                auditoria.IdEstadoAuditoria = 1;
                db.Auditorias.Add(auditoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAuditor = new SelectList(db.Usuarios, "Id", "UserName", auditoria.IdAuditor);
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEntidad", auditoria.IdEmpresa);
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
        public ActionResult Edit([Bind(Include = "Id,IdAuditor,FechaAuditoria,FechaCreacion,IdEstadoAuditoria,IdEmpresa")] Models.Auditoria.Auditoria auditoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auditoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Lista", "Auditorias");
            }
            ViewBag.IdAuditor = new SelectList(db.Usuarios, "Id", "UserName", auditoria.IdAuditor);
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEntidad", auditoria.IdEmpresa);
            ViewBag.IdEstadoAuditoria = new SelectList(db.Estados, "Id", "Estado", auditoria.IdEstadoAuditoria);
            return View(auditoria);
        }

        public ActionResult Gestionar(int idAuditoria)
        {
            ViewBag.Respuestas = db.NivelesCalificacion.ToList();
            var auditoria = db.Auditorias.Find(idAuditoria);
            auditoria.Dominios = db.DominiosAuditoria.Where(d => d.IdAuditoria == idAuditoria).ToList();

            for (int i = 0; i < auditoria.Dominios.Count; i++)
            {
                int idDominio = auditoria.Dominios[i].IdDominio;
                auditoria.Dominios[i].Dominio = db.Dominios.Find(idDominio);

                auditoria.Dominios[i].Preguntas = (from p in db.Preguntas
                                 join g in db.Grupos on p.IdGrupo equals g.Id
                                 join a in db.Anexos on g.IdAnexo equals a.Id
                                 join d in db.Dominios on a.IdDominio equals d.Id
                                 where d.Id == idDominio
                                 select p).ToList();
            }
            auditoria.Dominios = auditoria.Dominios.OrderBy(d => d.Dominio.Identificador).ThenBy(d=> d.Dominio.Consecutivo).ToList();
            return View(auditoria);
        }

        [HttpGet]
        public bool Asociar(int idAuditoria, int idDominio)
        {
            var current = db.DominiosAuditoria.Where(ad => ad.IdAuditoria == idAuditoria && ad.IdDominio == idDominio).ToList();
            if (current.Any())
            {
                var auditoria = db.DominiosAuditoria.Find(current.First().Id);
                db.DominiosAuditoria.Remove(auditoria);
                db.SaveChanges();
            }
            else
            {
                db.DominiosAuditoria.Add(new DominiosAuditoria()
                {
                    IdAuditoria = idAuditoria,
                    IdDominio = idDominio
                });
                db.SaveChanges();
            }
            return true;
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
