using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VotoElectronico.Models;

namespace VotoElectronico.Controllers
{
    public class ClientesController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.tbCliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return View(tbCliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cli_Id,cli_Nombre,cli_RTN,cli_Telefono,cli_Dirección")] tbCliente tbCliente)
        {
            if (ModelState.IsValid)
            {
                db.tbCliente.Add(tbCliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbCliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return View(tbCliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cli_Id,cli_Nombre,cli_RTN,cli_Telefono,cli_Dirección")] tbCliente tbCliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbCliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return View(tbCliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCliente tbCliente = db.tbCliente.Find(id);
            db.tbCliente.Remove(tbCliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GuardarAjax(int idCliente,string nombreCliente, string rtnCliente, string telefonoCliente, string direccionCliente)
        {
            try
            {
                tbCliente tbCliente;

                if (idCliente != 0)
                {
                    tbCliente = new tbCliente
                    {
                        cli_Id = idCliente,
                        cli_Nombre = nombreCliente,
                        cli_RTN = rtnCliente,
                        cli_Telefono = telefonoCliente,
                        cli_Dirección = direccionCliente
                    };

                    db.Entry(tbCliente).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json("Registro ingresado correctamente");
                }
                else
                {
                    tbCliente = new tbCliente()
                    {
                        cli_Nombre = nombreCliente,
                        cli_RTN = rtnCliente,
                        cli_Telefono = telefonoCliente,
                        cli_Dirección = direccionCliente
                    };
                    db.tbCliente.Add(tbCliente);
                    db.SaveChanges();
                    return Json("Registro ingresado correctamente");
                }


            }
            catch (Exception e)
            {

                return Json($"Error: {e.Message}");
            }
        }

        public ActionResult EditAjax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {
                return HttpNotFound();
            }
            return Json(tbCliente);
        }



        public ActionResult DeleteAjax(int? id)
        {
           
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
            tbCliente tbCliente = db.tbCliente.Find(id);
            if (tbCliente == null)
            {

                return HttpNotFound();
            }
            else if(tbCliente != null)
            {
                db.tbCliente.Remove(tbCliente);
                db.SaveChanges();
            }


            return Json(tbCliente);

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
