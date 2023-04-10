using Newtonsoft.Json;
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
    public class ModulosController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();

        // GET: Modulos
        public ActionResult Index()
        {
            var tbModulos = db.tbModulos.Include(t => t.tbComponentes);
            return View(tbModulos.ToList());
        }



        public ActionResult ListModulos()
        {
            IEnumerable<tbModulos> listModulos = db.tbModulos
               .Include(t => t.tbComponentes).ToList();
            return Json(new
            {
                data = listModulos.Select(x => new
                {
                    mod_Id = x.mod_Id,
                    comp_Id = (x.tbComponentes != null)? x.tbComponentes.comp_Nombre:"",
                    mod_Nombre = x.mod_Nombre

                })
                .OrderBy(x => x.mod_Id)
            }, JsonRequestBehavior.AllowGet);


        }
        // GET: Modulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbModulos tbModulos = db.tbModulos.Find(id);
            if (tbModulos == null)
            {
                return HttpNotFound();
            }
            return View(tbModulos);
        }

        // GET: Modulos/Create
        public ActionResult Create()
        {
            ViewBag.comp_Id = new SelectList(db.tbComponentes, "comp_Id", "comp_Nombre");
            return View();
        }

        // POST: Modulos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mod_Id,comp_Id,mod_Nombre")] tbModulos tbModulos)
        {
            if (ModelState.IsValid)
            {
                db.tbModulos.Add(tbModulos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.comp_Id = new SelectList(db.tbComponentes, "comp_Id", "comp_Nombre", tbModulos.comp_Id);
            return View(tbModulos);
        }

        // GET: Modulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbModulos tbModulos = db.tbModulos.Find(id);
            if (tbModulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.comp_Id = new SelectList(db.tbComponentes, "comp_Id", "comp_Nombre", tbModulos.comp_Id);
            return View(tbModulos);
        }

        // POST: Modulos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mod_Id,comp_Id,mod_Nombre")] tbModulos tbModulos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbModulos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.comp_Id = new SelectList(db.tbComponentes, "comp_Id", "comp_Nombre", tbModulos.comp_Id);
            return View(tbModulos);
        }

        // GET: Modulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbModulos tbModulos = db.tbModulos.Find(id);
            if (tbModulos == null)
            {
                return HttpNotFound();
            }
            return View(tbModulos);
        }

        // POST: Modulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbModulos tbModulos = db.tbModulos.Find(id);
            db.tbModulos.Remove(tbModulos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ComponenteList()
        {
            string json = JsonConvert.SerializeObject(db.tbComponentes, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarAjax(int mod_Id, int comp_Id, string mod_Nombre ,string accion)
        {
            try
            {

                if (accion == "Editar")
                {
                  
                      var update = db.UDP_tbModulos_Edit(mod_Id, comp_Id, mod_Nombre);
                    foreach (UDP_tbModulos_Edit_Result item in update)
                    {
                        if (item.resultado.StartsWith("-1"))
                        {
                            return Json("No se pudo modificar el registro");
                        }


                    }
                    return Json("Modificado");
                }
                if (accion == "Guardar")
                {


                    var insert = db.UDP_tbModulos_Insert(comp_Id, mod_Nombre);
                    foreach (UDP_tbModulos_Insert_Result item in insert)
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
                    var delete = db.UDP_tbModulos_Delete(mod_Id);
                    foreach (UDP_tbModulos_Delete_Result item in delete)
                    {
                        if (item.resultado.StartsWith("-1"))
                            return Json("No se pudo Eliminar el registro");

                    }
                    return Json("Eliminado");

                }

            }
            catch (Exception e)
            {

                return Json($"Error: {e.Message}");
            }
        }


        public ActionResult EditAjax(int? id)
        {
            tbModulos tbModulos = db.tbModulos.Find(id);
            string json = JsonConvert.SerializeObject(tbModulos, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(json, JsonRequestBehavior.AllowGet);

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
