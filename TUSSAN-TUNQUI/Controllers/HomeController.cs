using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TUSSAN_TUNQUI.Models;


namespace TUSSAN_TUNQUI.Controllers {
    public class HomeController : Controller {

        private tussanbdEntities15 db = new tussanbdEntities15();

        [HttpGet]
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult login(string username, string password) {
            Empleado empleado;
            if (username.ToString().Length > 0 && password.ToString().Length > 0)
            {
                if (Session["Empleado"] == null)
                {
                    try
                    {
                        empleado = db.Empleado.Where(d => d.usuario == username && d.contraseña == password && d.estado == 1).First();
                    }
                    catch (Exception e)
                    {
                        var error = e.Message;
                        empleado = null;
                    }


                    if (empleado != null)
                    {
                        //Creamos y Almacenamos empleado en la sesión
                        Session["Empleado"] = empleado;
                        return Redirect("~/Adm");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Usuario o contraseña incorrecto";
                        return View("Index");
                    }
                }
                else
                {
                    empleado = Session["Empleado"] as Empleado;
                    return Redirect("~/Adm");
                }

            }
            return RedirectToAction("Index");
        }



    }
}