using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TUSSAN_TUNQUI.Models;


namespace TUSSAN_TUNQUI.Controllers {
    public class HomeController : Controller {


        [HttpGet]
        public ActionResult Index() {
            return View();
        }

        /*
        public ActionResult IntranetAdmin()
        {
            string nombre = Request.Form["txtnom"].ToString();
            string contraseña = Request.Form["txtcontra"].ToString();

            usuario usu = new usuario();
            usu.login(nombre, contraseña);
            string sql = string.Format("select * from Empleado where usuario = '"+nombre+"' and contraseña = '"+contraseña+"'");
            DB db = new DB();
            db.transaccion(sql);            
            return View();
        }              
        */


    }
}