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
    public class EmpleadosController : Controller {
        private tussanbdEntities11 db = new tussanbdEntities11();

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

        // GET: Empleados
        public ActionResult Index() {
            if (isSessionSet() && empleado.idCargo == 1)
            {
                var empleado = db.Empleado.Include(e => e.Cargo);
                return View(empleado.ToList().Where(e => e.estado == 1));
            }
            else
                return redirectToHome();
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id) {
            if (isSessionSet() && empleado.idCargo == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Empleado empleado = db.Empleado.Find(id);
                if (empleado == null)
                {
                    return HttpNotFound();
                }
                return View(empleado);
            }
            else
                return redirectToHome();
        }

        // GET: Empleados/Create
        public ActionResult Create() {
            if (isSessionSet() && empleado.idCargo == 1)
            {

                ViewBag.idCargo = new SelectList(db.Cargo, "idCargo", "descripcionCargo");
                return View();
            }
            else
                return redirectToHome();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpleado,idCargo,DNI,nombreEmpleado,usuario,contraseña,estado")] Empleado empleado) {
            if (isSessionSet() && empleado.idCargo == 1)
            {

                if (ModelState.IsValid)
                {
                    db.Empleado.Add(empleado);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.idCargo = new SelectList(db.Cargo, "idCargo", "descripcionCargo", empleado.idCargo);
                return View(empleado);
            }
            else
                return redirectToHome();
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id) {
            if (isSessionSet() && empleado.idCargo == 1)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Empleado empleado = db.Empleado.Find(id);
                if (empleado == null)
                {
                    return HttpNotFound();
                }
                ViewBag.idCargo = new SelectList(db.Cargo, "idCargo", "descripcionCargo", empleado.idCargo);
                return View(empleado);
            }
            else
                return redirectToHome();
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmpleado,idCargo,DNI,nombreEmpleado,usuario,contraseña,estado")] Empleado empleado) {
            if (isSessionSet() && empleado.idCargo == 1)
            {

                if (ModelState.IsValid)
                {
                    db.Entry(empleado).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.idCargo = new SelectList(db.Cargo, "idCargo", "descripcionCargo", empleado.idCargo);
                return View(empleado);
            }
            else
                return redirectToHome();
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id) {
            if (isSessionSet() && empleado.idCargo == 1)
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Empleado empleado = db.Empleado.Find(id);
                if (empleado == null)
                {
                    return HttpNotFound();
                }
                return View(empleado);
            }
            else
                return redirectToHome();
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            if (isSessionSet() && empleado.idCargo == 1)
            {

                Empleado empleado = db.Empleado.Find(id);
                //db.Empleado.Remove(empleado);
                empleado.estado = 0;
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
