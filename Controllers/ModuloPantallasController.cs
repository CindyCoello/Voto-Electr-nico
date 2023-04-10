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
    public class ModuloPantallasController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();

        public ActionResult ListModuloPantalla()
        {
            IEnumerable<tbModuloPantallas> listModuloPantalla = db.tbModuloPantallas
                 .Include(m => m.tbModulos).ToList();
            return Json(new
            {
                data = listModuloPantalla.Select(x => new
                {
                    modpan_Id= x.modpan_Id,
                    mod_Id = (x.tbModulos != null) ? x.tbModulos.mod_Nombre : "",
                    modpan_Nombre = x.modpan_Nombre
                    

                })
                .OrderBy(x => x.modpan_Id)
            }, JsonRequestBehavior.AllowGet);


        }

        // GET: ModuloPantallas
        public ActionResult Index()
        {
            var tbModuloPantallas = db.tbModuloPantallas.Include(t => t.tbModulos);
            return View(tbModuloPantallas.ToList());
        }

        // GET: ModuloPantallas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbModuloPantallas tbModuloPantallas = db.tbModuloPantallas.Find(id);
            if (tbModuloPantallas == null)
            {
                return HttpNotFound();
            }
            return View(tbModuloPantallas);
        }

        // GET: ModuloPantallas/Create
        public ActionResult Create()
        {
            ViewBag.mod_Id = new SelectList(db.tbModulos, "mod_Id", "mod_Nombre");
            return View();
        }

        // POST: ModuloPantallas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "modpan_Id,mod_Id,modpan_Nombre")] tbModuloPantallas tbModuloPantallas)
        {
            if (ModelState.IsValid)
            {
                db.tbModuloPantallas.Add(tbModuloPantallas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mod_Id = new SelectList(db.tbModulos, "mod_Id", "mod_Nombre", tbModuloPantallas.mod_Id);
            return View(tbModuloPantallas);
        }

        // GET: ModuloPantallas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbModuloPantallas tbModuloPantallas = db.tbModuloPantallas.Find(id);
            if (tbModuloPantallas == null)
            {
                return HttpNotFound();
            }
            ViewBag.mod_Id = new SelectList(db.tbModulos, "mod_Id", "mod_Nombre", tbModuloPantallas.mod_Id);
            return View(tbModuloPantallas);
        }

        // POST: ModuloPantallas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "modpan_Id,mod_Id,modpan_Nombre")] tbModuloPantallas tbModuloPantallas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbModuloPantallas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mod_Id = new SelectList(db.tbModulos, "mod_Id", "mod_Nombre", tbModuloPantallas.mod_Id);
            return View(tbModuloPantallas);
        }

        // GET: ModuloPantallas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbModuloPantallas tbModuloPantallas = db.tbModuloPantallas.Find(id);
            if (tbModuloPantallas == null)
            {
                return HttpNotFound();
            }
            return View(tbModuloPantallas);
        }

        // POST: ModuloPantallas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbModuloPantallas tbModuloPantallas = db.tbModuloPantallas.Find(id);
            db.tbModuloPantallas.Remove(tbModuloPantallas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ModuloList()
        {
            string json = JsonConvert.SerializeObject(db.tbModulos, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarAjax(int modpan_Id,int mod_Id,  string modpan_Nombre, string accion)
        {
            try
            {

                if (accion == "Editar")
                {

                    var update = db.UDP_tbModuloPantallas_Edit(modpan_Id,mod_Id, modpan_Nombre);
                    foreach (UDP_tbModuloPantallas_Edit_Result item in update)
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


                    var insert = db.UDP_tbModuloPantallas_Insert(mod_Id, modpan_Nombre);
                    foreach (UDP_tbModuloPantallas_Insert_Result item in insert)
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
                    
                    var delete = db.UDP_tbModuloPantallas_Delete(modpan_Id);
                    foreach (UDP_tbModuloPantallas_Delete_Result item in delete)
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


        //public ActionResult EditAjax(int? id)
        //{
        //    tbModuloPantallas tbModuloPantallas = db.tbModuloPantallas.Find(id);
        //    string json = JsonConvert.SerializeObject(tbModuloPantallas, Formatting.Indented, new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    });

        //    return Json(json, JsonRequestBehavior.AllowGet);

        //}


        public ActionResult EditAjax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbModuloPantallas tbModuloPantallas = db.tbModuloPantallas.Find(id);
            if (tbModuloPantallas == null)
            {
                return HttpNotFound();
            }

            tbModuloPantallas response = new tbModuloPantallas();
            response.modpan_Id = tbModuloPantallas.modpan_Id;
            response.mod_Id = tbModuloPantallas.mod_Id;
            response.modpan_Nombre = tbModuloPantallas.modpan_Nombre;



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
