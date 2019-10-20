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
    public class GruposController : Controller
    {
        private AuditoriaContext db = new AuditoriaContext();

        // GET: ISO/Grupos
        public ActionResult Index()
        {
            var grupos = db.Grupos.Include(g => g.Anexo).Include(g => g.Anexo);
            return View(grupos.ToList());
        }

        // GET: ISO/Grupos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // GET: ISO/Grupos/Create
        public ActionResult Create()
        {
            ViewBag.IdAnexo = new SelectList(db.Anexos, "Id", "Titulo");
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio");
            return View();
        }

        // POST: ISO/Grupos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreGrupo,Descripcion,Consecutivo,IdDominio,IdAnexo")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Grupos.Add(grupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAnexo = new SelectList(db.Anexos, "Id", "Titulo", grupo.IdAnexo);
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio", grupo.IdDominio);
            return View(grupo);
        }

        // GET: ISO/Grupos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAnexo = new SelectList(db.Anexos, "Id", "Titulo", grupo.IdAnexo);
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio", grupo.IdDominio);
            return View(grupo);
        }

        // POST: ISO/Grupos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreGrupo,Descripcion,Consecutivo,IdDominio,IdAnexo")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAnexo = new SelectList(db.Anexos, "Id", "Titulo", grupo.IdAnexo);
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio", grupo.IdDominio);
            return View(grupo);
        }

        // GET: ISO/Grupos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // POST: ISO/Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grupo grupo = db.Grupos.Find(id);
            db.Grupos.Remove(grupo);
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
