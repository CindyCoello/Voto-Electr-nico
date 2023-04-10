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
    public class DepartamentosController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();

        public ActionResult ListDepartamentos()
        {
            IEnumerable<tbDepartamentos> listDepartamentos = db.tbDepartamentos.ToList();
            return Json(new
            {
                data = listDepartamentos.Select(x => new
                {
                    depto_Id = x.depto_Id,
                    depto_Descripcion = x.depto_Descripcion

                })
                .OrderBy(x => x.depto_Id)
            }, JsonRequestBehavior.AllowGet);


        }




        // GET: Departamentos
        public ActionResult Index()
        {
            return View(db.tbDepartamentos.ToList());
        }

        // GET: Departamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamentos tbDepartamentos = db.tbDepartamentos.Find(id);
            if (tbDepartamentos == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamentos);
        }

        // GET: Departamentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "depto_Id,depto_Descripcion")] tbDepartamentos tbDepartamentos)
        {
            if (ModelState.IsValid)
            {
                db.tbDepartamentos.Add(tbDepartamentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbDepartamentos);
        }

        // GET: Departamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamentos tbDepartamentos = db.tbDepartamentos.Find(id);
            if (tbDepartamentos == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamentos);
        }

        // POST: Departamentos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "depto_Id,depto_Descripcion")] tbDepartamentos tbDepartamentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDepartamentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbDepartamentos);
        }

        // GET: Departamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamentos tbDepartamentos = db.tbDepartamentos.Find(id);
            if (tbDepartamentos == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamentos);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDepartamentos tbDepartamentos = db.tbDepartamentos.Find(id);
            db.tbDepartamentos.Remove(tbDepartamentos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GuardarAjax(string nombredepartamento, int idDepartamento, string accion)
        {
            try
            {
                
                if ( accion == "Editar")
                {
                   
                    var update = db.UDP_tbDepartamentos_Edit(idDepartamento, nombredepartamento);
                    foreach (UDP_tbDepartamentos_Edit_Result item in update)
                    {
                        if (item.resultado.StartsWith("-1"))
                        {
                            return Json("No se pudo modificar el registro");
                        }
                            

                    }
                    return Json("Modificado");
                }
                else
                {
                    if (accion == "Guardar")
                    {


                        var insert = db.UDP_tbDepartamentos_Insert(idDepartamento, nombredepartamento);
                        foreach (UDP_tbDepartamentos_Insert_Result item in insert)
                        {
                            if (item.resultado.StartsWith("-1"))
                            {
                                return Json("No se pudo ingresar el registro");
                            }


                        }
                        return Json("Ingresado");
                    }

                    else
                    {
                        var delete = db.UDP_tbDepartamentos_Delete(idDepartamento);
                        foreach (UDP_tbDepartamentos_Delete_Result item in delete)
                        {
                            if (item.resultado.StartsWith("-1"))
                                return Json("No se pudo Eliminar el registro");

                        }
                        return Json("Eliminado");

                    }

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
            tbDepartamentos tbDepartamentos = db.tbDepartamentos.Find(id);
            if (tbDepartamentos == null)
            {
                return HttpNotFound();
            }


            tbDepartamentos response = new tbDepartamentos();
            response.depto_Id = tbDepartamentos.depto_Id;
            response.depto_Descripcion = tbDepartamentos.depto_Descripcion;

            return Json(response);
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
