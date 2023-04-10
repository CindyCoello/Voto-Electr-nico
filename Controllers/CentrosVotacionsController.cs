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
    public class CentrosVotacionsController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();

        public ActionResult ListCentroVotacion()
        {
            IEnumerable<tbCentrosVotacion> listCentroVotacion = db.tbCentrosVotacion
                 .Include(t => t.tbDepartamentos)
                   .Include(t => t.tbMunicipios).ToList();
            return Json(new
            {
                data = listCentroVotacion.Select(x => new
                {
                    cenvot_Id = x.cenvot_Id,
                    depto_Id = (x.tbDepartamentos != null) ? x.tbDepartamentos.depto_Descripcion : "",
                    muni_Id = (x.tbMunicipios != null) ? x.tbMunicipios.muni_Descripcion : "",
                    cenvot_CodigoArea = x.cenvot_CodigoArea,
                    cenvot_CodigoSectorElectoral = x.cenvot_CodigoSectorElectoral,
                    cenvot_Nombre = x.cenvot_Nombre,
                    cenvot_Latitud = x.cenvot_Latitud,
                    cenvot_Longitud = x.cenvot_Longitud,
                    cenvot_TotalMesas = x.cenvot_TotalMesas


                })
                .OrderBy(x => x.cenvot_Nombre)
            }, JsonRequestBehavior.AllowGet);


        }

        // GET: CentrosVotacions
        public ActionResult Index()
        {
            var tbCentrosVotacion = db.tbCentrosVotacion.Include(t => t.tbDepartamentos).Include(t => t.tbMunicipios);
            return View(tbCentrosVotacion.ToList());
        }

        // GET: CentrosVotacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCentrosVotacion tbCentrosVotacion = db.tbCentrosVotacion.Find(id);
            if (tbCentrosVotacion == null)
            {
                return HttpNotFound();
            }
            return View(tbCentrosVotacion);
        }

        // GET: CentrosVotacions/Create
        public ActionResult Create()
        {
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion");
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion");
            return View();
        }

        // POST: CentrosVotacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cenvot_Id,depto_Id,muni_Id,cenvot_CodigoArea,cenvot_CodigoSectorElectoral,cenvot_Nombre,cenvot_Latitud,cenvot_Longitud,cenvot_TotalMesas")] tbCentrosVotacion tbCentrosVotacion)
        {
            if (ModelState.IsValid)
            {
                db.tbCentrosVotacion.Add(tbCentrosVotacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbCentrosVotacion.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbCentrosVotacion.muni_Id);
            return View(tbCentrosVotacion);
        }

        // GET: CentrosVotacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCentrosVotacion tbCentrosVotacion = db.tbCentrosVotacion.Find(id);
            if (tbCentrosVotacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbCentrosVotacion.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbCentrosVotacion.muni_Id);
            return View(tbCentrosVotacion);
        }

        // POST: CentrosVotacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cenvot_Id,depto_Id,muni_Id,cenvot_CodigoArea,cenvot_CodigoSectorElectoral,cenvot_Nombre,cenvot_Latitud,cenvot_Longitud,cenvot_TotalMesas")] tbCentrosVotacion tbCentrosVotacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCentrosVotacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbCentrosVotacion.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbCentrosVotacion.muni_Id);
            return View(tbCentrosVotacion);
        }

        // GET: CentrosVotacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCentrosVotacion tbCentrosVotacion = db.tbCentrosVotacion.Find(id);
            if (tbCentrosVotacion == null)
            {
                return HttpNotFound();
            }
            return View(tbCentrosVotacion);
        }

        // POST: CentrosVotacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCentrosVotacion tbCentrosVotacion = db.tbCentrosVotacion.Find(id);
            db.tbCentrosVotacion.Remove(tbCentrosVotacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DepartamentoList()
        {


            string json = JsonConvert.SerializeObject(db.tbDepartamentos, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(json, JsonRequestBehavior.AllowGet);
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
            catch (Exception e)
            {

                return Json($"Error: {e}");
            }


        }

       
       
   

        public ActionResult GuardarAjax(int cenvot_Id, string nombreCentroVotacion, int idDepartamento, int idMunicipio, int codigoArea, int codigoSetorElectoral, float latitud,float longitud, int totalMesas, string accion)
        {
            try
            {

                if (accion == "Editar")
                {

                    var update = db.UDP_tbCentrosVotacion_Edit(cenvot_Id,idDepartamento, idMunicipio, codigoArea, codigoSetorElectoral, nombreCentroVotacion, latitud, longitud, totalMesas);
                    foreach (UDP_tbCentrosVotacion_Edit_Result item in update)
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


                    var insert = db.UDP_tbCentrosVotacion_Insert(idDepartamento, idMunicipio, codigoArea, codigoSetorElectoral, nombreCentroVotacion, latitud, longitud, totalMesas);
                    foreach (UDP_tbCentrosVotacion_Insert_Result item in insert)
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
                    var delete = db.UDP_tbCentrosVotacion_Delete(idDepartamento);
                    foreach (UDP_tbCentrosVotacion_Delete_Result item in delete)
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

            tbCentrosVotacion tbCentrosVotacion = db.tbCentrosVotacion.Find(id);
            if (tbCentrosVotacion == null)
            {
                return HttpNotFound();
            }

            tbCentrosVotacion response = new tbCentrosVotacion();
            response.cenvot_Id = tbCentrosVotacion.cenvot_Id;
            response.depto_Id = tbCentrosVotacion.depto_Id;
            response.muni_Id = tbCentrosVotacion.muni_Id;
            response.cenvot_CodigoArea = tbCentrosVotacion.cenvot_CodigoArea;
            response.cenvot_CodigoSectorElectoral = tbCentrosVotacion.cenvot_CodigoSectorElectoral;
            response.cenvot_Nombre = tbCentrosVotacion.cenvot_Nombre;
            response.cenvot_Latitud = tbCentrosVotacion.cenvot_Latitud;
            response.cenvot_Longitud = tbCentrosVotacion.cenvot_Longitud;
            response.cenvot_TotalMesas = tbCentrosVotacion.cenvot_TotalMesas;


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
