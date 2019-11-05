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
    public class AnexosController : Controller
    {
        private AuditoriaContext db = new AuditoriaContext();

        // GET: ISO/Anexos
        public ActionResult Index(int? idDominio)
        {
            if (idDominio == null)
            {
                if(Session["idDominio"] != null)
                    idDominio = Convert.ToInt32(Session["idDominio"]);
                else
                    return RedirectToAction("Index", "Dominios");
            }                
            else
                Session["idDominio"] = idDominio;

            var dominio = db.Dominios.Where(d => d.Id == idDominio).Include(d=> d.Anexos).FirstOrDefault();
            
            if (dominio.Anexos.Any())
                ViewBag.IdAnexo = dominio.Anexos.First().Id;
            else
                ViewBag.IdAnexo = 0; 

            foreach (var anexo in dominio.Anexos)
            {
                anexo.Grupos = db.Grupos.Where(g => g.IdAnexo == anexo.Id).ToList();
                foreach (var grupo in anexo.Grupos)
                {
                    grupo.Preguntas = db.Preguntas.Where(p => p.IdGrupo == grupo.Id).ToList();
                }
            }

            return View(dominio);
        }

        // GET: ISO/Anexos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anexo anexo = db.Anexos.Find(id);
            if (anexo == null)
            {
                return HttpNotFound();
            }
            return View(anexo);
        }

        // GET: ISO/Anexos/Create
        public ActionResult Create(int idDominio)
        {
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio", idDominio);
            return View();
        }

        // POST: ISO/Anexos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descripcion,Consecutivo,Activo,IdDominio")] Anexo anexo)
        {
            if (ModelState.IsValid)
            {
                db.Anexos.Add(anexo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio", anexo.IdDominio);
            return View(anexo);
        }

        // GET: ISO/Anexos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anexo anexo = db.Anexos.Find(id);
            if (anexo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio", anexo.IdDominio);
            return View(anexo);
        }

        // POST: ISO/Anexos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descripcion,Consecutivo,Activo,IdDominio")] Anexo anexo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anexo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDominio = new SelectList(db.Dominios, "Id", "NombreDominio", anexo.IdDominio);
            return View(anexo);
        }

        // GET: ISO/Anexos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anexo anexo = db.Anexos.Find(id);
            if (anexo == null)
            {
                return HttpNotFound();
            }
            return View(anexo);
        }

        // POST: ISO/Anexos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anexo anexo = db.Anexos.Find(id);
            db.Anexos.Remove(anexo);
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
