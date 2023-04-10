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
    public class MesasElectoralesController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();

        public ActionResult ListMesaElectoral()
        {
            IEnumerable<tbMesasElectorales> listMesaElectoral = db.tbMesasElectorales
                 .Include(t => t.tbCentrosVotacion).ToList();
            return Json(new
            {
                data = listMesaElectoral.Select(x => new
                {
                    mesa_Id= x.mesa_Id,
                    cenvot_Id = (x.tbCentrosVotacion != null) ? x.tbCentrosVotacion.cenvot_Nombre : ""


                })
                .OrderBy(x => x.mesa_Id)
            }, JsonRequestBehavior.AllowGet);


        }

        // GET: MesasElectorales
        public ActionResult Index()
        {
            var tbMesasElectorales = db.tbMesasElectorales.Include(t => t.tbCentrosVotacion);
            return View(tbMesasElectorales.ToList());
        }

        // GET: MesasElectorales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMesasElectorales tbMesasElectorales = db.tbMesasElectorales.Find(id);
            if (tbMesasElectorales == null)
            {
                return HttpNotFound();
            }
            return View(tbMesasElectorales);
        }

        // GET: MesasElectorales/Create
        public ActionResult Create()
        {
            ViewBag.cenvot_Id = new SelectList(db.tbCentrosVotacion, "cenvot_Id", "cenvot_Nombre");
            return View();
        }

        // POST: MesasElectorales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mesa_Id,cenvot_Id")] tbMesasElectorales tbMesasElectorales)
        {
            if (ModelState.IsValid)
            {
                db.tbMesasElectorales.Add(tbMesasElectorales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cenvot_Id = new SelectList(db.tbCentrosVotacion, "cenvot_Id", "cenvot_Nombre", tbMesasElectorales.cenvot_Id);
            return View(tbMesasElectorales);
        }

        // GET: MesasElectorales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMesasElectorales tbMesasElectorales = db.tbMesasElectorales.Find(id);
            if (tbMesasElectorales == null)
            {
                return HttpNotFound();
            }
            ViewBag.cenvot_Id = new SelectList(db.tbCentrosVotacion, "cenvot_Id", "cenvot_Nombre", tbMesasElectorales.cenvot_Id);
            return View(tbMesasElectorales);
        }

        // POST: MesasElectorales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mesa_Id,cenvot_Id")] tbMesasElectorales tbMesasElectorales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbMesasElectorales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cenvot_Id = new SelectList(db.tbCentrosVotacion, "cenvot_Id", "cenvot_Nombre", tbMesasElectorales.cenvot_Id);
            return View(tbMesasElectorales);
        }

        // GET: MesasElectorales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMesasElectorales tbMesasElectorales = db.tbMesasElectorales.Find(id);
            if (tbMesasElectorales == null)
            {
                return HttpNotFound();
            }
            return View(tbMesasElectorales);
        }

        // POST: MesasElectorales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMesasElectorales tbMesasElectorales = db.tbMesasElectorales.Find(id);
            db.tbMesasElectorales.Remove(tbMesasElectorales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CentroVotacionList()
        {
           

            string json = JsonConvert.SerializeObject(db.tbCentrosVotacion, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarAjax(int mesa_Id, int cenvot_Id, string accion)
        {
            try
            {

                if (accion == "Editar")
                {

                    var update = db.UDP_tbMesasElectorales_Edit(mesa_Id, cenvot_Id);
                    foreach (UDP_tbMesasElectorales_Edit_Result item in update)
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


                    var insert = db.UDP_tbMesasElectorales_Insert(cenvot_Id);
                    foreach (UDP_tbMesasElectorales_Insert_Result item in insert)
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
                    var delete = db.UDP_tbMesasElectorales_Delete(mesa_Id);
                    foreach (UDP_tbMesasElectorales_Delete_Result item in delete)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbMesasElectorales tbMesasElectorales = db.tbMesasElectorales.Find(id);
            if (tbMesasElectorales == null)
            {
                return HttpNotFound();
            }

            tbMesasElectorales response = new tbMesasElectorales();
            response.mesa_Id = tbMesasElectorales.mesa_Id;
            response.cenvot_Id = tbMesasElectorales.cenvot_Id;
           


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
