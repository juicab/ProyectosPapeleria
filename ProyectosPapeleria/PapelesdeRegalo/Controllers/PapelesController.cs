﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using PapelesdeRegalo.DAL;

namespace PapelesdeRegalo.Controllers
{
    public class PapelesController : Controller
    {

        MensajeDAL oMensajeDAL;
        // GET: Papeles
        public ActionResult Inicio()
        {
            return View();
        }
        public ActionResult Granmark()
        {
            return View();
        }
        public ActionResult Galas()
        {
            return View();
        }
        public ActionResult Bolsas()
        {
            return View();
        }
        public ActionResult Regalos()
        {
            return View();
        }
        public ActionResult QuiénesSomos()
        {
            return View();
        }
        public ActionResult Contacto()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnviarCorreo(String nombre, String correo, String asunto, String mensaje)
        {
            String mensajenuevo = mensaje + " Datos del cliente, Nombre:" + nombre + ", Correo para contacto:" + correo;
            if (ModelState.IsValid)
            {
                MailMessage corre = new MailMessage();
                corre.From = new MailAddress("jesus.uicabcauich@gmail.com"); //correo que se usa para enviar los correos
                corre.To.Add("sam12mario1@gmail.com");
                corre.Subject = asunto;
                corre.Body = mensajenuevo;
                corre.IsBodyHtml = true;
                corre.Priority = MailPriority.Normal;

                //configuracion del servidor smtp
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                String sCuentaCorreo = "jesus.uicabcauich@gmail.com";
                String sPassCorreo = "yermoherrera";
                smtp.Credentials = new System.Net.NetworkCredential(sCuentaCorreo, sPassCorreo);
                smtp.Send(corre);
                ViewBag.Mensaje = "Mensaje Enviado Correctamente";
                return RedirectToAction("Contacto", "Papeles");
            }

            else
            {
                return RedirectToAction("Inicio,Papeles");
            }
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
                    return RedirectToAction("Contacto", "Papeles");
                }
                else
                {
                    ViewBag.error = "Al parecer hubo un Error";
                    return RedirectToAction("Contacto", "Papeles");
                }

            }
            else
            {
                ViewBag.error = "Al parecer hubo un Error";
                return RedirectToAction("Contacto", "Papeles");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EnviarMensajeQuery(string nombre, string correo, string asunto, string telefono, string mensaje)
        {
            oMensajeDAL = new MensajeDAL();
            if (ModelState.IsValid)
            {
                int Resp = 0;
                Resp = oMensajeDAL.Agregar(nombre, correo, asunto, telefono, mensaje);
                if (Resp == 1)
                {
                    return Json(Resp);
                }
                else
                {
                    return Json(Resp);
                }

            }
            else
            {
                return Json(1);
            }
        }


    }
}