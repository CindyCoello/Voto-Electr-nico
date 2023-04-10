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
    public class AldeasController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();


        public ActionResult ListAldeas()
        {
            IEnumerable<tbAldeas> listAldeas = db.tbAldeas
                 .Include(t => t.tbDepartamentos)
                 .Include(t => t.tbMunicipios).ToList();
            return Json(new
            {
                data = listAldeas.Select(x => new
                {
                    aldea_Id = x.aldea_Id,
                    aldea_Descripcion = x.aldea_Descripcion,
                    muni_Id = (x.tbMunicipios != null) ? x.tbMunicipios.muni_Descripcion : "",
                    depto_Id = (x.tbDepartamentos != null) ? x.tbDepartamentos.depto_Descripcion : ""

                })
                .OrderBy(x => x.aldea_Descripcion)
            }, JsonRequestBehavior.AllowGet);


        }

       
        // GET: Aldeas
        public ActionResult Index()
        {
            var tbAldeas = db.tbAldeas.Include(t => t.tbDepartamentos).Include(t => t.tbMunicipios);
            return View(tbAldeas.ToList());
        }

        // GET: Aldeas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAldeas tbAldeas = db.tbAldeas.Find(id);
            if (tbAldeas == null)
            {
                return HttpNotFound();
            }
            return View(tbAldeas);
        }

        // GET: Aldeas/Create
        public ActionResult Create()
        {
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion");
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion");
            return View();
        }

        // POST: Aldeas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aldea_Id,aldea_Descripcion,muni_Id,depto_Id")] tbAldeas tbAldeas)
        {
            if (ModelState.IsValid)
            {
                db.tbAldeas.Add(tbAldeas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbAldeas.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbAldeas.muni_Id);
            return View(tbAldeas);
        }

        // GET: Aldeas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAldeas tbAldeas = db.tbAldeas.Find(id);
            if (tbAldeas == null)
            {
                return HttpNotFound();
            }
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbAldeas.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbAldeas.muni_Id);
            return View(tbAldeas);
        }

        // POST: Aldeas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aldea_Id,aldea_Descripcion,muni_Id,depto_Id")] tbAldeas tbAldeas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAldeas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbAldeas.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbAldeas.muni_Id);
            return View(tbAldeas);
        }

        // GET: Aldeas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAldeas tbAldeas = db.tbAldeas.Find(id);
            if (tbAldeas == null)
            {
                return HttpNotFound();
            }
            return View(tbAldeas);
        }

        // POST: Aldeas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAldeas tbAldeas = db.tbAldeas.Find(id);
            db.tbAldeas.Remove(tbAldeas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      


        public ActionResult MunicipioList(int id)
        {
            try
            {
                string json = JsonConvert.SerializeObject(db.tbMunicipios.Where(x => x.depto_Id == id), Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("Error");
            }


        }

        public ActionResult DepartamentoList()
        {


            string json = JsonConvert.SerializeObject(db.tbDepartamentos, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AldeaList(int id, int muni_Id)
        {

            try
            {
                string json = JsonConvert.SerializeObject(db.tbAldeas.Where(x => x.depto_Id == id && x.muni_Id == muni_Id), Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("Error");
            }
        }



        public ActionResult GuardarAjax(int aldea_Id, string aldea_Descripcion, int muni_Id, int depto_Id, string accion)
        {
            try
            {

                if (accion == "Editar")
                {

                    var update = db.UDP_tbAldeas_Edit(aldea_Id, aldea_Descripcion, muni_Id, depto_Id);
                    foreach (UDP_tbAldeas_Edit_Result item in update)
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


                    var insert = db.UDP_tbAldeas_Insert(aldea_Descripcion, muni_Id, depto_Id);
                    foreach (UDP_tbAldeas_Insert_Result item in insert)
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
                    var delete = db.UDP_tbAldeas_Delete(aldea_Id);
                    foreach (UDP_tbAldeas_Delete_Result item in delete)
                    {
                        if (item.resultado.StartsWith("-2"))
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

            tbAldeas tbAldeas = db.tbAldeas.Find(id);
            if (tbAldeas == null)
            {
                return HttpNotFound();
            }

            tbAldeas response = new tbAldeas();
            response.aldea_Id = tbAldeas.aldea_Id;
            response.aldea_Descripcion = tbAldeas.aldea_Descripcion;
            response.muni_Id = tbAldeas.muni_Id;
            response.depto_Id = tbAldeas.depto_Id;


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
