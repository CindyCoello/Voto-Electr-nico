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
    public class EstadosCivilsController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();
        public ActionResult ListEstadoCivil()
        {
            IEnumerable<tbEstadosCivil> listEstadoCivil = db.tbEstadosCivil.ToList();
            return Json(new
            {
                data = listEstadoCivil.Select(x => new
                {
                    estCiv_Id = x.estCiv_Id,
                    estCiv_Descripcion = x.estCiv_Descripcion

                })
                .OrderBy(x => x.estCiv_Descripcion)
            }, JsonRequestBehavior.AllowGet);


        }


        // GET: EstadosCivils
        public ActionResult Index()
        {
            return View(db.tbEstadosCivil.ToList());
        }

        // GET: EstadosCivils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadosCivil tbEstadosCivil = db.tbEstadosCivil.Find(id);
            if (tbEstadosCivil == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadosCivil);
        }

        // GET: EstadosCivils/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadosCivils/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "estCiv_Id,estCiv_Descripcion")] tbEstadosCivil tbEstadosCivil)
        {
            if (ModelState.IsValid)
            {
                db.tbEstadosCivil.Add(tbEstadosCivil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbEstadosCivil);
        }

        // GET: EstadosCivils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadosCivil tbEstadosCivil = db.tbEstadosCivil.Find(id);
            if (tbEstadosCivil == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadosCivil);
        }

        // POST: EstadosCivils/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "estCiv_Id,estCiv_Descripcion")] tbEstadosCivil tbEstadosCivil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEstadosCivil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbEstadosCivil);
        }

        // GET: EstadosCivils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadosCivil tbEstadosCivil = db.tbEstadosCivil.Find(id);
            if (tbEstadosCivil == null)
            {
                return HttpNotFound();
            }
            return View(tbEstadosCivil);
        }

        // POST: EstadosCivils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEstadosCivil tbEstadosCivil = db.tbEstadosCivil.Find(id);
            db.tbEstadosCivil.Remove(tbEstadosCivil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult GuardarAjax(string nombreEstadoCivil, int idEstadoCivil)
        {
            try
            {
                tbEstadosCivil tbEstadosCivil;
                if (idEstadoCivil != 0)
                {
                    tbEstadosCivil = new tbEstadosCivil
                    {
                       estCiv_Id  = idEstadoCivil,
                        estCiv_Descripcion = nombreEstadoCivil
                    };

                    //db.Entry(tbEstadosCivil).State = EntityState.Modified;
                    //db.SaveChanges();
                    //return Json("Registro Modificado correctamente");
                    var update = db.UDP_tbEstadosCivil_Edit(idEstadoCivil,nombreEstadoCivil);
                    foreach (UDP_tbEstadosCivil_Edit_Result item in update)
                    {
                        if (item.resultado.StartsWith("-1"))
                            return Json("No se pudo modificar el registro");

                    }
                    return Json("Modificado");
                }
                else
                {
                    var insert = db.UDP_tbEstadosCivil_Insert(nombreEstadoCivil);
                    foreach (UDP_tbEstadosCivil_Insert_Result item in insert)
                    {
                        if (item.resultado.StartsWith("-1"))
                            return Json("No se pudo ingresar el registro");

                    }
                    return Json("Ingresado");
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
            tbEstadosCivil tbEstadosCivil = db.tbEstadosCivil.Find(id);
            if (tbEstadosCivil == null)
            {
                return HttpNotFound();
            }
            return Json(tbEstadosCivil);
        }



        public ActionResult DeleteAjax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstadosCivil tbEstadosCivil = db.tbEstadosCivil.Find(id);
            if (tbEstadosCivil == null)
            {
                return HttpNotFound();

            }

            else
            {
                var delete = db.UDP_tbEstadosCivil_Delete(id);
                foreach (UDP_tbEstadosCivil_Delete_Result item in delete)
                {
                    if (item.resultado.StartsWith("-1"))
                        return Json("No se pudo Eliminar el registro");

                }
                return Json("Eliminado");
            }

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
