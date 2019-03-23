using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PapeleriaMerida.DAL;

namespace PapeleriaMerida.Controllers
{
    public class AdminController : Controller
    {
        EmpresaDAL oEmpresaDAL;
        MensajeDAL oMensajeDAL;
        MarcaDAL oMarcaDAL;
        // GET: Admin


        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Index()
        {
            oMensajeDAL = new MensajeDAL();
            return View();
        }


        public ActionResult CatalogoPapeleria()
        {
            return View();
        }
        public ActionResult CatalogoNovedades()
        {
            return View();
        }
        public ActionResult CatalogoSticker()
        {
            return View();
        }
        public ActionResult CatalogoPapeles()
        {
            return View();
        }
        public ActionResult CatalogoBolsas()
        {
            return View();
        }
        public ActionResult Mensajes()
        {
            oMensajeDAL = new MensajeDAL();
            return View(oMensajeDAL.Mostrar());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnviarMensaje(string nombre, string correo, string asunto, string telefono, string mensaje)
        {
            oMensajeDAL = new MensajeDAL();
            if (ModelState.IsValid)
            {
                int Resp = 0;
                Resp = oMensajeDAL.Agregar(nombre, correo, asunto, telefono, mensaje);
                if(Resp==1)
                {
                    TempData["Mensaje"] = "Los Datos se han actualizado con éxito";
                    return RedirectToAction("Contacto", "Papeleria");
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Contacto", "Papeleria");
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Contacto", "Papeleria");
            }
        }

        public ActionResult DatosEmpresa()
        {
            oEmpresaDAL = new EmpresaDAL();
            return View(oEmpresaDAL.Obtener_Empresa());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarDatos(string telefono, string direccion,string correo,string sitio,string descripcion, string mision, string vision, string valores)
        {
            oEmpresaDAL = new EmpresaDAL();
            if (ModelState.IsValid)
            {
                int Resp = 0;
                Resp = oEmpresaDAL.Modificar(telefono, direccion, correo, sitio, descripcion, mision, vision, valores);

                if (Resp == 1)
                {
                    TempData["Mensaje"] = "Los Datos se han actualizado con éxito";
                    return RedirectToAction("DatosEmpresa", "Admin");
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Inicio", "Admin");
                }
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Inicio", "Admin");
            }
        }

        public ActionResult CerrarSesion()
        {
            Session.Abandon();
            return RedirectToAction("Inicio", "Papeleria");
        }

        public ActionResult Marcas()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarMarca(string nombre,string descripcion, HttpPostedFileBase imagen)
        {
            oMarcaDAL = new MarcaDAL();
            if (ModelState.IsValid)
            {
                int resp = 0;
                string ruta = ImagenMarca(imagen);
                if(ruta!="1")
                {
                    resp = oMarcaDAL.Agregar(nombre, descripcion, ruta);
                    if(resp==1)
                    {
                        TempData["Confirmacion"] = "Los Datos se han actualizado con éxito";
                        return RedirectToAction("Marcas", "Admin");
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Marcas", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Marcas", "Admin");
                }
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Marcas", "Admin");
            }
            
        }

        public string ImagenMarca(HttpPostedFileBase imagen)
        {
            string rutasave;
            string error = "1";
            SubirArchivoDAL oSubirArchivoDAL = new SubirArchivoDAL();
            if (imagen != null)
            {
                string ruta = Server.MapPath("~/images/Marcas/");
                ruta += imagen.FileName;
                oSubirArchivoDAL.SubirArchivo(ruta, imagen);
                string confirmacion = oSubirArchivoDAL.confirmacion;
                rutasave = imagen.FileName;
                if (confirmacion == "guardado")
                {
                    return rutasave;
                }
                else
                {
                    return error;
                }
            }
            else
            {
                return error;
            }
        }

        [ChildActionOnly]
        public ActionResult MostrarListaMarcas()
        {
            oMarcaDAL = new MarcaDAL();
            return PartialView(oMarcaDAL.Mostrar());
        }

        public ActionResult Carusel()
        {
            return View();
        }








    }
}