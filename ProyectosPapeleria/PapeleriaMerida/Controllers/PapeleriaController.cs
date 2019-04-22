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
        MarcasDAL oMarcasDAL;
        CatalogosDAL oCatalogoDAL;
        CaruselDAL oCaruselDAL;
        ProductoDAL oProductos;
        OpinionesDAL oOpinionesDAL;
        // GET: Papeleria


       public ActionResult Reparacion()
        {
            return View();
        }

        public ActionResult Catalogos()
        {
            return View();
        }


        [ChildActionOnly]
        public ActionResult ListaOpiniones()
        {
            oOpinionesDAL = new OpinionesDAL();
            return PartialView(oOpinionesDAL.Mostrar());

        }


        [ChildActionOnly]
        public ActionResult ListaCategoria()
        {
            oMarcasDAL = new MarcasDAL();
            return PartialView(oMarcasDAL.MostrarParaMaster());

        }

        [ChildActionOnly]
        public ActionResult ListaProductos(int id)
        {
            oProductos = new ProductoDAL();
            return PartialView(oProductos.MostrarProductosCategoria(id));
        }
        public ActionResult Error()
        {
            return RedirectToAction("Inicio", "Papeleria");
        }
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
        [ChildActionOnly]
        public ActionResult MostrarListaMarcasPagina()
        {
            oMarcasDAL = new MarcasDAL();
            
            return PartialView(oMarcasDAL.Mostrar());
        }

        [ChildActionOnly]
        public ActionResult MostrarListaCatalogos()
        {
            oCatalogoDAL = new CatalogosDAL();
            return PartialView(oCatalogoDAL.Mostrar());
        }

        [ChildActionOnly]
        public ActionResult ListaCatalogos()
        {
            oCatalogoDAL = new CatalogosDAL();
            return PartialView(oCatalogoDAL.Mostrar());
        }

        [ChildActionOnly]
        public ActionResult MostrarListaCarusel()
        {
            oCaruselDAL = new CaruselDAL();
            return PartialView(oCaruselDAL.Mostrar());
        }


        public ActionResult Categoría(string id)
        {
            oMarcasDAL = new MarcasDAL();
            return View(oMarcasDAL.ObtenerMarcaSeleccionadaNombre(id));
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