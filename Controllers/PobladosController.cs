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
    public class PobladosController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();

        public ActionResult ListPoblados()
        {
            IEnumerable<tbPoblados> listPoblados = db.tbPoblados
                .Include(t => t.tbAldeas.tbDepartamentos.tbMunicipios).ToList();
            return Json(new
            {
                data = listPoblados.Select(x => new
                {
                    pobl_Id = x.pobl_Id,
                    pobl_Descripcion = x.pobl_Descripcion,
                    aldea_Id = (x.tbAldeas != null) ? x.tbAldeas.aldea_Descripcion : "",
                    muni_Id = (x.tbMunicipios!=null )?x.tbMunicipios.muni_Descripcion:"",
                    depto_Id = (x.tbDepartamentos != null) ? x.tbDepartamentos.depto_Descripcion : ""




                })
                .OrderBy(x => x.pobl_Id)
            }, JsonRequestBehavior.AllowGet);


        }


        // GET: Poblados
        public ActionResult Index()
        {
            var tbPoblados = db.tbPoblados.Include(t => t.tbAldeas).Include(t => t.tbDepartamentos).Include(t => t.tbMunicipios);
            return View(tbPoblados.ToList());
        }

        // GET: Poblados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPoblados tbPoblados = db.tbPoblados.Find(id);
            if (tbPoblados == null)
            {
                return HttpNotFound();
            }
            return View(tbPoblados);
        }

        // GET: Poblados/Create
        public ActionResult Create()
        {
            ViewBag.aldea_Id = new SelectList(db.tbAldeas, "aldea_Id", "aldea_Descripcion");
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion");
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion");
            return View();
        }

        // POST: Poblados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pobl_Id,pobl_Descripcion,muni_Id,depto_Id,aldea_Id")] tbPoblados tbPoblados)
        {
            if (ModelState.IsValid)
            {
                db.tbPoblados.Add(tbPoblados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aldea_Id = new SelectList(db.tbAldeas, "aldea_Id", "aldea_Descripcion", tbPoblados.aldea_Id);
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbPoblados.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbPoblados.muni_Id);
            return View(tbPoblados);
        }

        // GET: Poblados/Edit/5
        public ActionResult MostrarDatosPobladosAjax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPoblados tbPoblados = db.tbPoblados.Find(id);
            if (tbPoblados == null)
            {
                return HttpNotFound();
            }
            ViewBag.aldea_Id = new SelectList(db.tbAldeas, "aldea_Id", "aldea_Descripcion", tbPoblados.aldea_Id);
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbPoblados.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbPoblados.muni_Id);

            string json = JsonConvert.SerializeObject(tbPoblados, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return View(json);
        }

        // POST: Poblados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pobl_Id,pobl_Descripcion,muni_Id,depto_Id,aldea_Id")] tbPoblados tbPoblados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPoblados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aldea_Id = new SelectList(db.tbAldeas, "aldea_Id", "aldea_Descripcion", tbPoblados.aldea_Id);
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbPoblados.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbPoblados.muni_Id);
            return View(tbPoblados);
        }

        // GET: Poblados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPoblados tbPoblados = db.tbPoblados.Find(id);
            if (tbPoblados == null)
            {
                return HttpNotFound();
            }
            return View(tbPoblados);
        }

        // POST: Poblados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPoblados tbPoblados = db.tbPoblados.Find(id);
            db.tbPoblados.Remove(tbPoblados);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DeptoList()
        {
          

            string json = JsonConvert.SerializeObject(db.tbDepartamentos, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MunicipiosList(int id)
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
                Console.WriteLine(e.Message);
                string mensaje = $"Error :{e.InnerException.Message}";
                return Json(mensaje);
              
            }


        }

        public ActionResult AldeasList(int id, int muni_Id)
        {

            try
            {
                string json = JsonConvert.SerializeObject(db.tbAldeas.Where(x => x.depto_Id == id && x.muni_Id == muni_Id), Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                string mensaje = $"Error :{e.InnerException.Message}";
                return Json(mensaje);
            }
        }


      
       
        public ActionResult GuardarPobladosAjax(int idPobla, string nombrePobla, int idMun, int idDepto, int idAldea, string accion)
        {
            try
            {

                if (idPobla !=0 && accion == "Editar")
                {
                    
                    

                    var update = db.UDP_tbPoblados_Edit(idPobla, nombrePobla, idMun, idDepto, idAldea);
                    foreach (UDP_tbPoblados_Edit_Result item in update)
                    {
                        if (item.resultado.StartsWith("-1"))
                        {
                            return Json("No se pudo modificar el registro");
                        }


                    }
                    return Json("Modificado");
                }


                if (idPobla != 0 && accion == "Eliminar")
                {
                    var delete = db.UDP_tbPoblados_Delete(idPobla);
                    foreach (UDP_tbPoblados_Delete_Result item in delete)
                    {
                        if (item.resultado.StartsWith("-1"))
                            return Json("No se pudo Eliminar el registro");

                    }
                    return Json("Eliminado");

                }
                else 
                {
                  

                    var insert = db.UDP_tbPoblados_Insert(nombrePobla, idMun, idDepto, idAldea);
                    foreach (UDP_tbPoblados_Insert_Result item in insert)
                    {
                        if (item.resultado.StartsWith("-1"))
                        {
                            return Json("No se pudo ingresar el registro");
                        }


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

            tbPoblados tbPoblados = db.tbPoblados.Find(id);
            if (tbPoblados == null)
            {
                return HttpNotFound();
            }

            tbPoblados response = new tbPoblados();
            response.pobl_Id = tbPoblados.pobl_Id;
            response.pobl_Descripcion = tbPoblados.pobl_Descripcion;
            response.muni_Id = tbPoblados.muni_Id;
            response.depto_Id = tbPoblados.depto_Id;
            response.aldea_Id = tbPoblados.aldea_Id;


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
