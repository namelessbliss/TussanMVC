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
    public class MercaderiasController : Controller {
        private tussanbdEntities13 db = new tussanbdEntities13();

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

        // GET: Mercaderias
        public ActionResult Index() {
            if (isSessionSet())
            {

                var mercaderia = db.Mercaderia.Include(m => m.Cliente).Include(m => m.TipoMercaderia);
                return View(mercaderia.ToList());
            }
            else
                return redirectToHome();
        }

        // GET: Mercaderias/Details/5
        public ActionResult Details(int? id) {
            if (isSessionSet())
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Mercaderia mercaderia = db.Mercaderia.Find(id);
                if (mercaderia == null)
                {
                    return HttpNotFound();
                }
                return View(mercaderia);
            }
            else
                return redirectToHome();
        }

        // GET: Mercaderias/Create
        public ActionResult Create() {
            if (isSessionSet())
            {

                ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreEmpresa");
                ViewBag.idTipoMerca = new SelectList(db.TipoMercaderia, "idTipoMerca", "descripcionTipoMerca");
                return View();
            }
            else
                return redirectToHome();
        }

        // POST: Mercaderias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMercaderia,descripcionMercaderia,areaCubica,idCliente,idTipoMerca,categoria,unidad,cantidad")] Mercaderia mercaderia) {
            if (isSessionSet())
            {


                if (ModelState.IsValid)
                {
                    db.Mercaderia.Add(mercaderia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreEmpresa", mercaderia.idCliente);
                ViewBag.idTipoMerca = new SelectList(db.TipoMercaderia, "idTipoMerca", "descripcionTipoMerca", mercaderia.idTipoMerca);
                return View(mercaderia);
            }
            else
                return redirectToHome();
        }

        // GET: Mercaderias/Edit/5
        public ActionResult Edit(int? id) {
            if (isSessionSet())
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Mercaderia mercaderia = db.Mercaderia.Find(id);
                if (mercaderia == null)
                {
                    return HttpNotFound();
                }
                ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreEmpresa", mercaderia.idCliente);
                ViewBag.idTipoMerca = new SelectList(db.TipoMercaderia, "idTipoMerca", "descripcionTipoMerca", mercaderia.idTipoMerca);
                return View(mercaderia);
            }
            else
                return redirectToHome();
        }

        // POST: Mercaderias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMercaderia,descripcionMercaderia,areaCubica,idCliente,idTipoMerca,categoria,unidad,cantidad")] Mercaderia mercaderia) {
            if (isSessionSet())
            {

                if (ModelState.IsValid)
                {
                    db.Entry(mercaderia).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreEmpresa", mercaderia.idCliente);
                ViewBag.idTipoMerca = new SelectList(db.TipoMercaderia, "idTipoMerca", "descripcionTipoMerca", mercaderia.idTipoMerca);
                return View(mercaderia);
            }
            else
                return redirectToHome();
        }

        // GET: Mercaderias/Delete/5
        public ActionResult Delete(int? id) {
            if (isSessionSet() && empleado.idCargo == 1)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Mercaderia mercaderia = db.Mercaderia.Find(id);
                if (mercaderia == null)
                {
                    return HttpNotFound();
                }
                return View(mercaderia);
            }
            else
                return redirectToHome();
        }

        // POST: Mercaderias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            if (isSessionSet() && empleado.idCargo == 1)
            {

                Mercaderia mercaderia = db.Mercaderia.Find(id);
                db.Mercaderia.Remove(mercaderia);
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
