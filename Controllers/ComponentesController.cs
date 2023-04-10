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
    public class ComponentesController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();


        //public ActionResult Componentes()
        //{
        //    return View();
        //}


        public ActionResult ListComponentes()
        {
            IEnumerable<tbComponentes> listComponentes = db.tbComponentes.ToList();
            return Json(new
            {
                data = listComponentes.Select(x => new
                {
                    comp_Id = x.comp_Id,
                    comp_Nombre = x.comp_Nombre

                })
                .OrderBy(x => x.comp_Nombre)
            }, JsonRequestBehavior.AllowGet);


        }
        // GET: Componentes
        public ActionResult Index()
        {
            return View(db.tbComponentes.ToList());
        }

        // GET: Componentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbComponentes tbComponentes = db.tbComponentes.Find(id);
            if (tbComponentes == null)
            {
                return HttpNotFound();
            }
            return View(tbComponentes);
        }

        // GET: Componentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Componentes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "comp_Id,comp_Nombre")] tbComponentes tbComponentes)
        {
            if (ModelState.IsValid)
            {
                db.tbComponentes.Add(tbComponentes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbComponentes);
        }

        //GET: Componentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbComponentes tbComponentes = db.tbComponentes.Find(id);
            if (tbComponentes == null)
            {
                return HttpNotFound();
            }
            return View(tbComponentes);
        }

        // POST: Componentes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "comp_Id,comp_Nombre")] tbComponentes tbComponentes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbComponentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbComponentes);
        }

        // GET: Componentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbComponentes tbComponentes = db.tbComponentes.Find(id);
            if (tbComponentes == null)
            {
                return HttpNotFound();
            }
            return View(tbComponentes);
        }

        // POST: Componentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbComponentes tbComponentes = db.tbComponentes.Find(id);
            db.tbComponentes.Remove(tbComponentes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GuardarAjax(string nombreComponente, int idComponente)
        {
            try
            {
                tbComponentes tbComponentes;
                if (idComponente != 0)
                {
                    tbComponentes = new tbComponentes 
                    {   comp_Id    = idComponente,
                        comp_Nombre = nombreComponente
                    };

                    //db.Entry(tbComponentes).State = EntityState.Modified;
                    //db.SaveChanges();
                    //return Json("Registro Modificado correctamente");
                    var update = db.UDP_tbComponentes_Edit(idComponente,nombreComponente);
                    foreach (UDP_tbComponentes_Edit_Result item in update)
                    {
                        if (item.resultado.StartsWith("-1"))
                            return Json("No se pudo modificar el registro");

                    }
                    return Json("Modificado");
                }
                else
                {
                    var insert = db.UDP_tbComponentes_Insert(nombreComponente);
                    foreach (UDP_tbComponentes_Insert_Result item in insert)
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

        //public ActionResult EditAjax(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tbComponentes tbComponentes = db.tbComponentes.Find(id);
        //    if (tbComponentes == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return Json(tbComponentes);
        //}

        public ActionResult EditAjax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbComponentes tbComponentes = db.tbComponentes.Find(id);
            if (tbComponentes == null)
            {
                return HttpNotFound();
            }

            tbComponentes response = new tbComponentes();
            response.comp_Id = tbComponentes.comp_Id;
            response.comp_Nombre = tbComponentes.comp_Nombre;


            return Json(response);
        }


        public ActionResult DeleteAjax(int? id)
        {
            var delete = db.UDP_tbComponentes_Delete(id);
            foreach (UDP_tbComponentes_Delete_Result item in delete)
            {
                if (item.resultado.StartsWith("-1"))
                    return Json("No se pudo Eliminar el registro");

            }
            return Json("Eliminado");
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
