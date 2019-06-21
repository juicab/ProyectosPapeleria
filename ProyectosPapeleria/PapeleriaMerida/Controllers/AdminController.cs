using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PapeleriaMerida.DAL;
using PapeleriaMerida.Models;
using System.Data;
using System.IO;

namespace PapeleriaMerida.Controllers
{
    public class AdminController : Controller
    {
        EmpresaDAL oEmpresaDAL;
        MensajeDAL oMensajeDAL;
        MarcaDAL oMarcaDAL;
        CatalogosDAL oCatalogosDAL;
        CaruselDAL oCaruselDAL;
        MarcasDAL oMarcasDAL;
        ProductoDAL oProductoDAL;
        OpinionesDAL oOpinionesDAL;
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
            oCatalogosDAL = new CatalogosDAL();
            return View(oCatalogosDAL.ObtenerCatPapeleria());
        }
        public ActionResult CatalogoNovedades()
        {
            oCatalogosDAL = new CatalogosDAL();
            return View(oCatalogosDAL.ObtenerCatNovedades());
        }
        public ActionResult CatalogoSticker()
        {
            oCatalogosDAL = new CatalogosDAL();
            return View(oCatalogosDAL.ObtenerCatStickers());
        }
        public ActionResult CatalogoPapeles()
        {
            oCatalogosDAL = new CatalogosDAL();
            return View(oCatalogosDAL.ObtenerCatPapeles());
        }
        public ActionResult CatalogoBolsas()
        {
            oCatalogosDAL = new CatalogosDAL();
            return View(oCatalogosDAL.ObtenerCatBolsas());
        }




        public ActionResult Mensajes()
        {
            oMensajeDAL = new MensajeDAL();
            return View(oMensajeDAL.Mostrar());
        }

        public ActionResult VerMensaje(int id)
        {
            oMensajeDAL = new MensajeDAL();
            return View(oMensajeDAL.ObtenerMensajeSeleccionado(id));
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
                if (Resp == 1)
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnviarMensajeCompu(string nombre, string correo, string asunto, string telefono, string mensaje)
        {
            oMensajeDAL = new MensajeDAL();
            if (ModelState.IsValid)
            {
                int Resp = 0;
                Resp = oMensajeDAL.AgregarCompu(nombre, correo, asunto, telefono, mensaje);
                if (Resp == 1)
                {
                    TempData["Mensaje"] = "Los Datos se han actualizado con éxito";
                    return RedirectToAction("Reparacion", "Papeleria");
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Reparacion", "Papeleria");
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Reparacion", "Papeleria");
            }
        }

        public ActionResult DatosEmpresa()
        {
            oEmpresaDAL = new EmpresaDAL();
            return View(oEmpresaDAL.Obtener_Empresa());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarDatos(string telefono, string direccion, string correo, string sitio, string descripcion, string mision, string vision, string valores)
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
        public ActionResult GuardarMarca(string nombre, string descripcion, HttpPostedFileBase imagen)
        {
            oMarcaDAL = new MarcaDAL();
            if (ModelState.IsValid)
            {
                int resp = 0;
                string ruta = ImagenMarca(imagen);
                if (ruta != "1")
                {
                    resp = oMarcaDAL.Agregar(nombre, descripcion, ruta);
                    if (resp == 1)
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

        public ActionResult ModificarMarca(int id)
        {
            oMarcaDAL = new MarcaDAL();
            return View(oMarcaDAL.ObtenerMarcaSeleccionada(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarCambiosMarca(string nombre, string descripcion, HttpPostedFileBase imagen, int cod)
        {
            oMarcaDAL = new MarcaDAL();
            if (ModelState.IsValid)
            {
                if (imagen != null)
                {
                    int resp = 0;
                    string ruta = ImagenMarca(imagen);
                    if (ruta != "1")
                    {
                        resp = oMarcaDAL.ModificarConURL(nombre, descripcion, ruta, cod);
                        if (resp == 1)
                        {
                            TempData["cambio"] = "Los Datos se han actualizado con éxito";
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
                    int resp2 = 0;
                    resp2 = oMarcaDAL.ModificarSinURL(nombre, descripcion, cod);
                    if (resp2 == 1)
                    {
                        TempData["cambio"] = "Los Datos se han actualizado con éxito";
                        return RedirectToAction("Marcas", "Admin");
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Marcas", "Admin");
                    }
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Marcas", "Admin");
            }

        }

        public ActionResult EliminarMarca(int id)
        {
            oMarcaDAL = new MarcaDAL();
            int resp = 0;
            resp = oMarcaDAL.Eliminar(id);
            if (resp == 1)
            {
                TempData["eli"] = "Los Datos se han eliminado con éxito";
                return RedirectToAction("Marcas", "Admin");
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Marcas", "Admin");
            }

        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarCatPape(HttpPostedFileBase file)
        {
            oCatalogosDAL = new CatalogosDAL();
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    int resp = 0;
                    string ruta = SubirArchivo(file);
                    if (ruta != "1")
                    {
                        resp = oCatalogosDAL.ModificarPapeleria(ruta);
                        if (resp == 1)
                        {
                            TempData["cambio"] = "Los Datos se han actualizado con éxito";
                            return RedirectToAction("CatalogoPapeleria", "Admin");
                        }
                        else
                        {
                            ViewBag.error = "Al parecer hubo un Error";
                            return RedirectToAction("CatalogoPapeleria", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("CatalogoPapeleria", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("CatalogoPapeleria", "Admin");
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("CatalogoPapeleria", "Admin");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarCatNove(HttpPostedFileBase file)
        {
            oCatalogosDAL = new CatalogosDAL();
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    int resp = 0;
                    string ruta = SubirArchivo(file);
                    if (ruta != "1")
                    {
                        resp = oCatalogosDAL.ModificarNovedades(ruta);
                        if (resp == 1)
                        {
                            TempData["cambio"] = "Los Datos se han actualizado con éxito";
                            return RedirectToAction("CatalogoNovedades", "Admin");
                        }
                        else
                        {
                            ViewBag.error = "Al parecer hubo un Error";
                            return RedirectToAction("CatalogoNovedades", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("CatalogoNovedades", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("CatalogoNovedades", "Admin");
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("CatalogoNovedades", "Admin");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarCatBolsas(HttpPostedFileBase file)
        {
            oCatalogosDAL = new CatalogosDAL();
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    int resp = 0;
                    string ruta = SubirArchivo(file);
                    if (ruta != "1")
                    {
                        resp = oCatalogosDAL.ModificarBolsas(ruta);
                        if (resp == 1)
                        {
                            TempData["cambio"] = "Los Datos se han actualizado con éxito";
                            return RedirectToAction("CatalogoBolsas", "Admin");
                        }
                        else
                        {
                            ViewBag.error = "Al parecer hubo un Error";
                            return RedirectToAction("CatalogoBolsas", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("CatalogoBolsas", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("CatalogoBolsas", "Admin");
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("CatalogoBolsas", "Admin");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarCatPapeles(HttpPostedFileBase file)
        {
            oCatalogosDAL = new CatalogosDAL();
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    int resp = 0;
                    string ruta = SubirArchivo(file);
                    if (ruta != "1")
                    {
                        resp = oCatalogosDAL.ModificarPapeles(ruta);
                        if (resp == 1)
                        {
                            TempData["cambio"] = "Los Datos se han actualizado con éxito";
                            return RedirectToAction("CatalogoPapeles", "Admin");
                        }
                        else
                        {
                            ViewBag.error = "Al parecer hubo un Error";
                            return RedirectToAction("CatalogoPapeles", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("CatalogoPapeles", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("CatalogoPapeles", "Admin");
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("CatalogoPapeles", "Admin");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarCatSticker(HttpPostedFileBase file)
        {
            oCatalogosDAL = new CatalogosDAL();
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    int resp = 0;
                    string ruta = SubirArchivo(file);
                    if (ruta != "1")
                    {
                        resp = oCatalogosDAL.ModificarStickers(ruta);
                        if (resp == 1)
                        {
                            TempData["cambio"] = "Los Datos se han actualizado con éxito";
                            return RedirectToAction("CatalogoSticker", "Admin");
                        }
                        else
                        {
                            ViewBag.error = "Al parecer hubo un Error";
                            return RedirectToAction("CatalogoSticker", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("CatalogoSticker", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("CatalogoSticker", "Admin");
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("CatalogoSticker", "Admin");
            }

        }

        public string SubirArchivo(HttpPostedFileBase file)
        {
            string rutasave;
            string error = "1";
            
            string RutaArchivoCompu = "C:\\Catalogos\\" + file.FileName; ;
            string NombreArchivo = file.FileName;

            SubirArchivoDAL oSubirArchivoDAL = new SubirArchivoDAL();
            if (file != null)
            {
                string ruta = Server.MapPath("~/admin/docs/");
                ruta += file.FileName;
                //oSubirArchivoDAL.SubirArchivo(ruta, file);
                oSubirArchivoDAL.Upload(RutaArchivoCompu, NombreArchivo);
                string confirmacion = oSubirArchivoDAL.confirmacion;
                rutasave = file.FileName;
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






        public ActionResult Carusel()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarCarusel(string titulo1, string titulo2, HttpPostedFileBase imagen)
        {
            oCaruselDAL = new CaruselDAL();
            if (ModelState.IsValid)
            {
                int resp = 0;
                string ruta = ImagenCarusel(imagen);
                if (ruta != "1")
                {
                    resp = oCaruselDAL.Agregar(titulo1, titulo2, ruta);
                    if (resp == 1)
                    {
                        TempData["Confirmacion"] = "Los Datos se han actualizado con éxito";
                        return RedirectToAction("Carusel", "Admin");
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Carusel", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Carusel", "Admin");
                }
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Carusel", "Admin");
            }

        }

        public string ImagenCarusel(HttpPostedFileBase imagen)
        {
            string rutasave;
            string error = "1";
            SubirArchivoDAL oSubirArchivoDAL = new SubirArchivoDAL();
            if (imagen != null)
            {
                string ruta = Server.MapPath("~/images/Carusel/");
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
        public ActionResult MostrarListaCarusel()
        {
            oCaruselDAL = new CaruselDAL();
            return PartialView(oCaruselDAL.Mostrar());
        }

        public ActionResult ModificarCarusel(int id)
        {
            oCaruselDAL = new CaruselDAL();
            return View(oCaruselDAL.ObtenerCaruselSeleccionado(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarCambiosCarusel(string titulo1, string titulo2, HttpPostedFileBase imagen, int cod)
        {
            oCaruselDAL = new CaruselDAL();
            if (ModelState.IsValid)
            {
                if (imagen != null)
                {
                    int resp = 0;
                    string ruta = ImagenCarusel(imagen);
                    if (ruta != "1")
                    {
                        resp = oCaruselDAL.ModificarConURL(titulo1, titulo2, ruta, cod);
                        if (resp == 1)
                        {
                            TempData["cambio"] = "Los Datos se han actualizado con éxito";
                            return RedirectToAction("Carusel", "Admin");
                        }
                        else
                        {
                            ViewBag.error = "Al parecer hubo un Error";
                            return RedirectToAction("Carusel", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Carusel", "Admin");
                    }
                }
                else
                {
                    int resp2 = 0;
                    resp2 = oCaruselDAL.ModificarSinURL(titulo1, titulo2, cod);
                    if (resp2 == 1)
                    {
                        TempData["cambio"] = "Los Datos se han actualizado con éxito";
                        return RedirectToAction("Carusel", "Admin");
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Carusel", "Admin");
                    }
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Carusel", "Admin");
            }

        }

        public ActionResult EliminarCarusel(int id)
        {
            oCaruselDAL = new CaruselDAL();
            int resp = 0;
            resp = oCaruselDAL.Eliminar(id);
            if (resp == 1)
            {
                TempData["eli"] = "Los Datos se han eliminado con éxito";
                return RedirectToAction("Carusel", "Admin");
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Carusel", "Admin");
            }

        }



        public ActionResult Marca()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarMarcas(string nombre, string descripcion, string slogan, HttpPostedFileBase logo, HttpPostedFileBase banner)
        {
            oMarcasDAL = new MarcasDAL();
            if (ModelState.IsValid)
            {
                int resp = 0;
                string log = ImagenMarcaLogo(logo);
                string ban = ImagenMarcaBanner(banner);
                if (log != "1" && ban != "1")
                {
                    resp = oMarcasDAL.Agregar(nombre, descripcion, slogan, log, ban);
                    if (resp == 1)
                    {
                        TempData["Confirmacion"] = "Los Datos se han actualizado con éxito";
                        return RedirectToAction("Marca", "Admin");
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Marca", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Marca", "Admin");
                }
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Marca", "Admin");
            }

        }
        public string ImagenMarcaLogo(HttpPostedFileBase imagen)
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
        public string ImagenMarcaBanner(HttpPostedFileBase imagen)
        {
            string rutasave;
            string error = "1";
            SubirArchivoDAL oSubirArchivoDAL = new SubirArchivoDAL();
            if (imagen != null)
            {
                string ruta = Server.MapPath("~/images/Banner/");
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
        public ActionResult MostrarListaMarca()
        {
            oMarcasDAL = new MarcasDAL();
            return PartialView(oMarcasDAL.Mostrar());
        }
        public ActionResult ModificarMarcas(int id)
        {
            oMarcasDAL = new MarcasDAL();
            return View(oMarcasDAL.ObtenerMarcaSeleccionada(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarCambiosMarcas(string nombre, string descripcion, string slogan, HttpPostedFileBase logo, HttpPostedFileBase banner, int cod)
        {
            oMarcasDAL = new MarcasDAL();
            if (ModelState.IsValid)
            {
                if (logo != null && banner != null)
                {
                    int resp = 0;
                    string log = ImagenMarcaLogo(logo);
                    string ban = ImagenMarcaBanner(banner);
                    if (log != "1" && ban != "1")
                    {
                        resp = oMarcasDAL.ModificarCompleto(nombre, descripcion, slogan, log, ban, cod);
                        if (resp == 1)
                        {
                            TempData["cambio"] = "Los Datos se han actualizado con éxito";
                            return RedirectToAction("Marca", "Admin");
                        }
                        else
                        {
                            ViewBag.error = "Al parecer hubo un Error";
                            return RedirectToAction("Marca", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Marca", "Admin");
                    }
                }
                else
                {
                    if (logo != null && banner == null)
                    {
                        int resp2 = 0;
                        string logoo = ImagenMarcaLogo(logo);
                        if (logoo != "1")
                        {
                            resp2 = oMarcasDAL.ModificarDatosConLogo(nombre, descripcion, slogan, logoo, cod);
                            if (resp2 == 1)
                            {
                                TempData["cambio"] = "Los Datos se han actualizado con éxito";
                                return RedirectToAction("Marca", "Admin");
                            }
                            else
                            {
                                ViewBag.error = "Al parecer hubo un Error";
                                return RedirectToAction("Marca", "Admin");
                            }
                        }
                        else
                        {
                            ViewBag.error = "Al parecer hubo un Error";
                            return RedirectToAction("Marca", "Admin");
                        }
                    }
                    else
                    {
                        if (logo == null && banner != null)
                        {
                            int resp3 = 0;
                            string logooo = ImagenMarcaBanner(banner);
                            if (logooo != "1")
                            {
                                resp3 = oMarcasDAL.ModificarDatosConBanner(nombre, descripcion, slogan, logooo, cod);
                                if (resp3 == 1)
                                {
                                    TempData["cambio"] = "Los Datos se han actualizado con éxito";
                                    return RedirectToAction("Marca", "Admin");
                                }
                                else
                                {
                                    ViewBag.error = "Al parecer hubo un Error";
                                    return RedirectToAction("Marca", "Admin");
                                }
                            }
                            else
                            {
                                ViewBag.error = "Al parecer hubo un Error";
                                return RedirectToAction("Marca", "Admin");
                            }
                        }
                        else
                        {
                            if (logo == null && banner == null)
                            {
                                int resp4 = 0;
                                resp4 = oMarcasDAL.ModificarSoloDatos(nombre, descripcion, slogan, cod);
                                if (resp4 == 1)
                                {
                                    TempData["cambio"] = "Los Datos se han actualizado con éxito";
                                    return RedirectToAction("Marca", "Admin");
                                }
                                else
                                {
                                    ViewBag.error = "Al parecer hubo un Error";
                                    return RedirectToAction("Marca", "Admin");
                                }
                            }
                            else
                            {
                                ViewBag.error = "Al parecer hubo un Error";
                                return RedirectToAction("Marca", "Admin");
                            }
                        }
                    }
                }
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Marcas", "Admin");
            }
        }
        public ActionResult EliminarMarcas(int id)
        {
            oMarcasDAL = new MarcasDAL();
            int resp = 0;
            resp = oMarcasDAL.Eliminar(id);
            if (resp == 1)
            {
                TempData["eli"] = "Los Datos se han eliminado con éxito";
                return RedirectToAction("Marca", "Admin");
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Marca", "Admin");
            }

        }




        public ActionResult Productos()
        {
            var oProductoModel = new Models.ProductoModel();
            oProductoDAL = new ProductoDAL();
            ViewBag.idMarca = new SelectList(oProductoModel.ListaMarcas = oProductoDAL.ListaMarcas(), "idMarca", "nomMarca");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarProducto(string nombre, string descripcion, HttpPostedFileBase imagen, int idMarca)
        {
            oProductoDAL = new ProductoDAL();
            if (ModelState.IsValid)
            {
                int resp = 0;
                string ruta = ImagenProducto(imagen);
                if (ruta != "1")
                {
                    resp = oProductoDAL.Agregar(nombre, descripcion, idMarca, ruta);
                    if (resp == 1)
                    {
                        TempData["Confirmacion"] = "Los Datos se han actualizado con éxito";
                        return RedirectToAction("Productos", "Admin");
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Productos", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Productos", "Admin");
                }
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Productos", "Admin");
            }

        }

        public string ImagenProducto(HttpPostedFileBase imagen)
        {
            string rutasave;
            string error = "1";
            SubirArchivoDAL oSubirArchivoDAL = new SubirArchivoDAL();
            if (imagen != null)
            {
                string ruta = Server.MapPath("~/images/Productos/");
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
        public ActionResult MostrarListaProductos()
        {
            oProductoDAL = new ProductoDAL();
            return PartialView(oProductoDAL.Mostrar());
        }
        public ActionResult ModificarProducto(int id)
        {
            oProductoDAL = new ProductoDAL();
            var oProductoModel = new Models.ProductoModel();
            ViewBag.idMarca = new SelectList(oProductoModel.ListaMarcas = oProductoDAL.ListaMarcas(), "idMarca", "nomMarca");
            return View(oProductoDAL.ObtenerProductoSeleccionado(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarCambiosProducto(string nombre, string descripcion, HttpPostedFileBase imagen, int idMarca, int cod)
        {
            oProductoDAL = new ProductoDAL();
            if (ModelState.IsValid)
            {
                if (imagen != null)
                {
                    int resp = 0;
                    string ruta = ImagenProducto(imagen);
                    if (ruta != "1")
                    {
                        resp = oProductoDAL.ModificarConImagen(nombre, descripcion, idMarca, ruta, cod);
                        if (resp == 1)
                        {
                            TempData["cambio"] = "Los Datos se han actualizado con éxito";
                            return RedirectToAction("Productos", "Admin");
                        }
                        else
                        {
                            ViewBag.error = "Al parecer hubo un Error";
                            return RedirectToAction("Productos", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Productos", "Admin");
                    }
                }
                else
                {
                    int resp2 = 0;
                    resp2 = oProductoDAL.ModificarSinImagen(nombre, descripcion, idMarca, cod);
                    if (resp2 == 1)
                    {
                        TempData["cambio"] = "Los Datos se han actualizado con éxito";
                        return RedirectToAction("Productos", "Admin");
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Productos", "Admin");
                    }
                }
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Productos", "Admin");
            }
        }
        public ActionResult EliminarProducto(int id)
        {
            oProductoDAL = new ProductoDAL();
                int resp = 0;
                resp = oProductoDAL.Eliminar(id);
                if (resp == 1)
                {
                    TempData["eli"] = "Los Datos se han eliminado con éxito";
                    return RedirectToAction("Productos", "Admin");
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Productos", "Admin");
                }
            

        }






        public ActionResult Opiniones()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarOpinion(string opinion, HttpPostedFileBase imagen)
        {
            oOpinionesDAL = new OpinionesDAL();
            if (ModelState.IsValid)
            {
                int resp = 0;
                string ruta = ImagenOpinion(imagen);
                if (ruta != "1")
                {
                    resp = oOpinionesDAL.Agregar(opinion, ruta);
                    if (resp == 1)
                    {
                        TempData["Confirmacion"] = "Los Datos se han actualizado con éxito";
                        return RedirectToAction("Opiniones", "Admin");
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Opiniones", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Opiniones", "Admin");
                }
            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Opiniones", "Admin");
            }
        }
        public string ImagenOpinion(HttpPostedFileBase imagen)
        {
            string rutasave;
            string error = "1";
            SubirArchivoDAL oSubirArchivoDAL = new SubirArchivoDAL();
            if (imagen != null)
            {
                string ruta = Server.MapPath("~/images/Clientes/");
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
        public ActionResult MostrarListaOpiniones()
        {
            oOpinionesDAL = new OpinionesDAL();
            return PartialView(oOpinionesDAL.Mostrar());
        }

        public ActionResult ModificarOpiniones(int id)
        {
            oOpinionesDAL = new OpinionesDAL();
            return View(oOpinionesDAL.ObtenerOpinionSeleccionada(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarCambiosOpinion(string opinion, HttpPostedFileBase imagen, int cod)
        {
            oOpinionesDAL = new OpinionesDAL();
            if (ModelState.IsValid)
            {
                if (imagen != null)
                {
                    int resp = 0;
                    string ruta = ImagenOpinion(imagen);
                    if (ruta != "1")
                    {
                        resp = oOpinionesDAL.CambiosConImagen(opinion, ruta, cod);
                        if (resp == 1)
                        {
                            TempData["cambio"] = "Los Datos se han actualizado con éxito";
                            return RedirectToAction("Opiniones", "Admin");
                        }
                        else
                        {
                            ViewBag.error = "Al parecer hubo un Error";
                            return RedirectToAction("Opiniones", "Admin");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Opiniones", "Admin");
                    }
                }
                else
                {
                    int resp2 = oOpinionesDAL.CambiosSinImagen(opinion, cod);
                    if (resp2 == 1)
                    {
                        TempData["cambio"] = "Los Datos se han actualizado con éxito";
                        return RedirectToAction("Opiniones", "Admin");
                    }
                    else
                    {
                        ViewBag.error = "Al parecer hubo un Error";
                        return RedirectToAction("Opiniones", "Admin");
                    }
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Opiniones", "Admin");
            }






        }
        public ActionResult EliminarOpinion(int id)
        {
                oOpinionesDAL = new OpinionesDAL();
                int resp = oOpinionesDAL.Eliminar(id);
                if(resp==1)
                {
                    TempData["eli"] = "Los Datos se han eliminado con éxito";
                    return RedirectToAction("Opiniones", "Admin");
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Opiniones", "Admin");
                }
        }



    }
}