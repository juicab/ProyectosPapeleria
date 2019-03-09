using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PapeleriaMerida.Controllers
{
    public class PapeleriaController : Controller
    {
        // GET: Papeleria
        public ActionResult Inicio()
        {
            return View();
        }
        public ActionResult NuestrasMarcas()
        {
            return View();
        }
        public ActionResult QuienesSomos()
        {
            return View();
        }
        public ActionResult Contacto()
        {
            return View();
        }


    }
}