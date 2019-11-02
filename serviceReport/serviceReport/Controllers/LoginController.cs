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

namespace serviceReport.Controllers
{
    public class LoginController : Controller
    {
        private AuditoriaContext db = new AuditoriaContext();

        // GET: Login/Create
        public ActionResult Index()
        {
			ViewBag.Error = false;
			return View();
        }

        // POST: Login/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,UserName,Password")] Usuario usuario)
        {
			var user = db.Usuarios.Where(u => u.UserName.ToUpper().Equals(usuario.UserName) && u.Password.Equals(usuario.Password)).FirstOrDefault();
			if(user == null)
			{
				ViewBag.Error = true;
				ViewBag.Message = "Credenciales ingresadas no arrojaron resultados.";
				return View(usuario);
			}
			else
			{
				Session["Usuario"] = user;
				return RedirectToAction("Index", "Dominios/index");
			}
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
