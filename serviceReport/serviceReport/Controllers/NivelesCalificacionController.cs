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
    public class NivelesCalificacionController : Controller
    {
        private AuditoriaContext db = new AuditoriaContext();

        // GET: ISO/NivelesCalificacion
        public ActionResult Index()
        {
            return View(db.NivelesCalificacion.ToList());
        }

        // GET: ISO/NivelesCalificacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelCalificacion nivelCalificacion = db.NivelesCalificacion.Find(id);
            if (nivelCalificacion == null)
            {
                return HttpNotFound();
            }
            return View(nivelCalificacion);
        }

        // GET: ISO/NivelesCalificacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ISO/NivelesCalificacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreNivel,Porcentaje,Criterio")] NivelCalificacion nivelCalificacion)
        {
            if (ModelState.IsValid)
            {
                db.NivelesCalificacion.Add(nivelCalificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nivelCalificacion);
        }

        // GET: ISO/NivelesCalificacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelCalificacion nivelCalificacion = db.NivelesCalificacion.Find(id);
            if (nivelCalificacion == null)
            {
                return HttpNotFound();
            }
            return View(nivelCalificacion);
        }

        // POST: ISO/NivelesCalificacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreNivel,Porcentaje,Criterio")] NivelCalificacion nivelCalificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nivelCalificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nivelCalificacion);
        }

        // GET: ISO/NivelesCalificacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelCalificacion nivelCalificacion = db.NivelesCalificacion.Find(id);
            if (nivelCalificacion == null)
            {
                return HttpNotFound();
            }
            return View(nivelCalificacion);
        }

        // POST: ISO/NivelesCalificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NivelCalificacion nivelCalificacion = db.NivelesCalificacion.Find(id);
            db.NivelesCalificacion.Remove(nivelCalificacion);
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
