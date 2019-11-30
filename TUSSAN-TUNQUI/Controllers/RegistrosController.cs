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
    public class RegistrosController : Controller {
        private tussanbdEntities15 db = new tussanbdEntities15();

        Empleado empleado;
        private Boolean isSessionSet() {
            if (Session["Empleado"] != null)
            {
                getSession();
                return true;
            }
            else
                return false;
        }

        private ActionResult redirectToHome() {
            return Redirect("~/Home");
        }

        private Empleado getSession() {
            if (empleado == null)
                empleado = Session["Empleado"] as Empleado;
            return empleado;
        }

        // GET: Registros
        public ActionResult Index() {
            if (isSessionSet())
            {

                var registro = db.Registro.Include(r => r.Cliente).Include(r => r.Empleado).Include(r => r.Mercaderia).Include(r => r.TipoRegistro);
                return View(registro.ToList().Where(r => r.estado == 1));
            }
            else
                return redirectToHome();
        }

        // GET: Registros/Details/5
        public ActionResult Details(int? id) {
            if (isSessionSet())
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Registro registro = db.Registro.Find(id);
                if (registro == null)
                {
                    return HttpNotFound();
                }
                return View(registro);
            }
            else
                return redirectToHome();
        }

        // GET: Registros/Create
        public ActionResult Create() {
            if (isSessionSet())
            {
                Empleado empleado = Session["Empleado"] as Empleado;
                ViewBag.idCliente = new SelectList(db.Cliente.Where(c => c.estado == 1), "idCliente", "nombreEmpresa");
                ViewBag.idEmpleado = new SelectList(db.Empleado.Where(e => e.idEmpleado == empleado.idEmpleado), "idEmpleado", "nombreEmpleado");
                //ViewBag.idMercaderia = new SelectList(db.Mercaderia, "idMercaderia", "descripcionMercaderia" );
                ViewBag.idMercaderia = new SelectList("");
                ViewBag.idTipoRegistro = new SelectList(db.TipoRegistro, "idTipoRegistro", "descripcionTipo");
                return View();
            }
            else
                return redirectToHome();
        }

        /**
         * Obtine lista de mercaderias segun el cliente seleccionado
         * Es recepcionado por un jquery que parsea el json y populate en la lista de mercaderias
         * */
        public JsonResult getListaMercaderias(int idCliente) {
            List<Mercaderia> listaMercas;
            db.Configuration.ProxyCreationEnabled = false;
            listaMercas = db.Mercaderia.Where(x => x.idCliente == idCliente).ToList();
            return Json(listaMercas, JsonRequestBehavior.AllowGet);
        }

        // POST: Registros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRegistro,idCliente,idMercaderia,idTipoRegistro,idEmpleado,fecha,cantidad,areaCubica,estado")] Registro registro) {
            if (isSessionSet())
            {

                if (ModelState.IsValid)
                {
                    db.Registro.Add(registro);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreEmpresa", registro.idCliente);
                ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNI", registro.idEmpleado);
                ViewBag.idMercaderia = new SelectList(db.Mercaderia, "idMercaderia", "descripcionMercaderia", registro.idMercaderia);
                ViewBag.idTipoRegistro = new SelectList(db.TipoRegistro, "idTipoRegistro", "descripcionTipo", registro.idTipoRegistro);
                return View(registro);
            }
            else
                return redirectToHome();
        }

        // GET: Registros/Edit/5
        public ActionResult Edit(int? id) {
            if (isSessionSet())
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Registro registro = db.Registro.Find(id);
                if (registro == null)
                {
                    return HttpNotFound();
                }
                ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreEmpresa", registro.idCliente);
                ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNI", registro.idEmpleado);
                ViewBag.idMercaderia = new SelectList(db.Mercaderia, "idMercaderia", "descripcionMercaderia", registro.idMercaderia);
                ViewBag.idTipoRegistro = new SelectList(db.TipoRegistro, "idTipoRegistro", "descripcionTipo", registro.idTipoRegistro);
                return View(registro);
            }
            else
                return redirectToHome();
        }

        // POST: Registros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRegistro,idCliente,idMercaderia,idTipoRegistro,idEmpleado,fecha,cantidad,areaCubica,estado")] Registro registro) {
            if (isSessionSet())
            {

                if (ModelState.IsValid)
                {
                    db.Entry(registro).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreEmpresa", registro.idCliente);
                ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNI", registro.idEmpleado);
                ViewBag.idMercaderia = new SelectList(db.Mercaderia, "idMercaderia", "descripcionMercaderia", registro.idMercaderia);
                ViewBag.idTipoRegistro = new SelectList(db.TipoRegistro, "idTipoRegistro", "descripcionTipo", registro.idTipoRegistro);
                return View(registro);
            }
            else
                return redirectToHome();
        }

        // GET: Registros/Delete/5
        public ActionResult Delete(int? id) {
            if (isSessionSet() && empleado.idCargo == 1)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Registro registro = db.Registro.Find(id);
                if (registro == null)
                {
                    return HttpNotFound();
                }
                return View(registro);
            }
            else
                return redirectToHome();
        }

        // POST: Registros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            if (isSessionSet() && empleado.idCargo == 1)
            {

                Registro registro = db.Registro.Find(id);
                //db.Registro.Remove(registro);
                registro.estado = 0;
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return redirectToHome();
        }

        protected override void Dispose(bool disposing) {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
