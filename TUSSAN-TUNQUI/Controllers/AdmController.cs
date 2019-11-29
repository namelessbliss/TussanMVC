using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TUSSAN_TUNQUI.Controllers {
    public class AdmController : Controller {

        // GET: Adm
        public ActionResult Index() {
            if (Session["Empleado"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("~/Home");
            }

        }

        [HttpPost]
        public ActionResult CloseSession() {
            Session["Empleado"] = null;
            return Redirect("~/Home");
        }
    }
}