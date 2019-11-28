using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TUSSAN_TUNQUI.Models;

namespace TUSSAN_TUNQUI.Controllers
{
    public class RegistrosController : Controller
    {
        private tussanbdEntities15 db = new tussanbdEntities15();

        // GET: Registros
        public ActionResult Index()
        {
            var registro = db.Registro.Include(r => r.Cliente).Include(r => r.Empleado).Include(r => r.Mercaderia).Include(r => r.TipoRegistro);
            return View(registro.ToList());
        }

        // GET: Registros/Details/5
        public ActionResult Details(int? id)
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

        // GET: Registros/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreEmpresa");
            ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNI");
            ViewBag.idMercaderia = new SelectList(db.Mercaderia, "idMercaderia", "descripcionMercaderia");
            ViewBag.idTipoRegistro = new SelectList(db.TipoRegistro, "idTipoRegistro", "descripcionTipo");
            return View();
        }

        // POST: Registros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRegistro,idCliente,idMercaderia,idTipoRegistro,idEmpleado,fecha,cantidad,areaCubica,estado")] Registro registro)
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

        // GET: Registros/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Registros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRegistro,idCliente,idMercaderia,idTipoRegistro,idEmpleado,fecha,cantidad,areaCubica,estado")] Registro registro)
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

        // GET: Registros/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Registros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registro registro = db.Registro.Find(id);
            db.Registro.Remove(registro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
