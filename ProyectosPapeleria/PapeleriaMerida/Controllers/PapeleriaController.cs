using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PapeleriaMerida.DAL;

namespace PapeleriaMerida.Controllers
{
    public class PapeleriaController : Controller
    {

        EmpresaDAL oEmpresaDAL;
        // GET: Papeleria
        public ActionResult Inicio()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NuestrasMarcas()
        {
            return View();
        }
        public ActionResult QuienesSomos()
        {
            oEmpresaDAL = new EmpresaDAL();
            return View(oEmpresaDAL.Obtener_Empresa());
        }
        public ActionResult Contacto()
        {
            oEmpresaDAL = new EmpresaDAL();
            return View(oEmpresaDAL.Obtener_Empresa());
        }

        public ActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IniciarSesion(string usuario, string contra)
        {
            if (ModelState.IsValid)
            {
                if (usuario == "admin" && contra == "18191819")
                {
                    TempData["Saludo"] = "Administrador";
                    Session["Usuario"] = usuario;
                    return RedirectToAction("Index","Admin");
                }
                else
                {
                    TempData["Ini"] = "Al parecer hubo un Error";
                    return RedirectToAction("IniciarSesion", "Papeleria");
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("IniciarSesion", "Papeleria");
            }
        }


    }
}