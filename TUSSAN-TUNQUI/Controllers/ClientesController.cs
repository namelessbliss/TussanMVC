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
    public class ClientesController : Controller {



        private Boolean isSessionSet() {
            if (Session["Empleado"] != null)
                return true;
            else
                return false;
        }

        private ActionResult redirectToHome() {
            return Redirect("~/Home");
        }

        private tussanbdEntities12 db = new tussanbdEntities12();

        // GET: Clientes
        public ActionResult Index() {
            if (isSessionSet())
            {
                return View(db.Cliente.ToList());
            }
            else
                return redirectToHome();
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id) {
            if (isSessionSet())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cliente cliente = db.Cliente.Find(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            else
                return redirectToHome();
        }

        // GET: Clientes/Create
        public ActionResult Create() {
            if (isSessionSet())
            {
                return View();
            }
            else
                return redirectToHome();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCliente,nombreEmpresa,dueñoEmpresa,RUC,estado")] Cliente cliente) {
            if (isSessionSet())
            {
                if (ModelState.IsValid)
                {
                    db.Cliente.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(cliente);
            }
            else
                return redirectToHome();
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id) {
            if (isSessionSet())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cliente cliente = db.Cliente.Find(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            else
                return redirectToHome();
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCliente,nombreEmpresa,dueñoEmpresa,RUC,estado")] Cliente cliente) {
            if (isSessionSet())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cliente).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cliente);
            }
            else
                return redirectToHome();
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id) {
            if (isSessionSet())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cliente cliente = db.Cliente.Find(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            else
                return redirectToHome();
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            if (isSessionSet())
            {
                Cliente cliente = db.Cliente.Find(id);
                db.Cliente.Remove(cliente);
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
