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
    public class CandidatosController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();


        public ActionResult ListCandidatos()
        {
            IEnumerable<tbCandidatos> listCandidatos = db.tbCandidatos
                 .Include(p => p.tbPartidos.tbMovimientos)
                 .Include(m => m.tbMovimientos)
                 .Include(tc => tc.tbTipoCandidato)
                 .Include(ce => ce.tbCentroElectoral)
                  .Include(de => de.tbDepartamentos)
                 .Include(mu => mu.tbMunicipios).ToList();

            return Json(new
            {
                data = listCandidatos.Select(x => new
                {

                    part_Id = (x.tbPartidos != null) ? x.tbPartidos.part_Nombre : "",
                    cand_Id = x.cand_Id



                })
                .OrderBy(x => x.cand_Id)
            }, JsonRequestBehavior.AllowGet);


        }

        // GET: Candidatos
        public ActionResult Index()
        {
            var tbCandidatos = db.tbCandidatos.Include(t => t.tbCentroElectoral).Include(t => t.tbDepartamentos).Include(t => t.tbMunicipios).Include(t => t.tbPartidos).Include(t => t.tbTipoCandidato).Include(t => t.tbMovimientos);
            return View(tbCandidatos.ToList());
        }

        // GET: Candidatos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCandidatos tbCandidatos = db.tbCandidatos.Find(id);
            if (tbCandidatos == null)
            {
                return HttpNotFound();
            }
            return View(tbCandidatos);
        }

        // GET: Candidatos/Create
        public ActionResult Create()
        {
            ViewBag.censo_Id = new SelectList(db.tbCentroElectoral, "censo_Id", "censo_Identidad");
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion");
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion");
            ViewBag.part_Id = new SelectList(db.tbPartidos, "part_Id", "part_Nombre");
            ViewBag.tipcan_Id = new SelectList(db.tbTipoCandidato, "tipcan_Id", "tipcan_Descripcion");
            ViewBag.mov_Id = new SelectList(db.tbMovimientos, "mov_Id", "mov_Nombre");
            return View();
        }

        // POST: Candidatos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cand_Id,part_Id,mov_Id,tipcan_Id,censo_Id,depto_Id,muni_Id,cand_Posicion")] tbCandidatos tbCandidatos)
        {
            if (ModelState.IsValid)
            {
                db.tbCandidatos.Add(tbCandidatos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.censo_Id = new SelectList(db.tbCentroElectoral, "censo_Id", "censo_Identidad", tbCandidatos.censo_Id);
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbCandidatos.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbCandidatos.muni_Id);
            ViewBag.part_Id = new SelectList(db.tbPartidos, "part_Id", "part_Nombre", tbCandidatos.part_Id);
            ViewBag.tipcan_Id = new SelectList(db.tbTipoCandidato, "tipcan_Id", "tipcan_Descripcion", tbCandidatos.tipcan_Id);
            ViewBag.mov_Id = new SelectList(db.tbMovimientos, "mov_Id", "mov_Nombre", tbCandidatos.mov_Id);
            return View(tbCandidatos);
        }

        // GET: Candidatos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCandidatos tbCandidatos = db.tbCandidatos.Find(id);
            if (tbCandidatos == null)
            {
                return HttpNotFound();
            }
            ViewBag.censo_Id = new SelectList(db.tbCentroElectoral, "censo_Id", "censo_Identidad", tbCandidatos.censo_Id);
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbCandidatos.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbCandidatos.muni_Id);
            ViewBag.part_Id = new SelectList(db.tbPartidos, "part_Id", "part_Nombre", tbCandidatos.part_Id);
            ViewBag.tipcan_Id = new SelectList(db.tbTipoCandidato, "tipcan_Id", "tipcan_Descripcion", tbCandidatos.tipcan_Id);
            ViewBag.mov_Id = new SelectList(db.tbMovimientos, "mov_Id", "mov_Nombre", tbCandidatos.mov_Id);
            return View(tbCandidatos);
        }

        // POST: Candidatos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cand_Id,part_Id,mov_Id,tipcan_Id,censo_Id,depto_Id,muni_Id,cand_Posicion")] tbCandidatos tbCandidatos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCandidatos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.censo_Id = new SelectList(db.tbCentroElectoral, "censo_Id", "censo_Identidad", tbCandidatos.censo_Id);
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbCandidatos.depto_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbCandidatos.muni_Id);
            ViewBag.part_Id = new SelectList(db.tbPartidos, "part_Id", "part_Nombre", tbCandidatos.part_Id);
            ViewBag.tipcan_Id = new SelectList(db.tbTipoCandidato, "tipcan_Id", "tipcan_Descripcion", tbCandidatos.tipcan_Id);
            ViewBag.mov_Id = new SelectList(db.tbMovimientos, "mov_Id", "mov_Nombre", tbCandidatos.mov_Id);
            return View(tbCandidatos);
        }

        // GET: Candidatos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCandidatos tbCandidatos = db.tbCandidatos.Find(id);
            if (tbCandidatos == null)
            {
                return HttpNotFound();
            }
            return View(tbCandidatos);
        }

        // POST: Candidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCandidatos tbCandidatos = db.tbCandidatos.Find(id);
            db.tbCandidatos.Remove(tbCandidatos);
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

                return Json("error" +e);
            }


        }
        //
        public ActionResult MoviminetosList(int id)
        {
            try
            {
                string json = JsonConvert.SerializeObject(db.tbMovimientos.Where(x => x.part_Id == id), Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json("error" +e);
            }


        }

        public ActionResult MovimientoList()
        {
            string json = JsonConvert.SerializeObject(db.tbMovimientos, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PartidotoList()
        {
            string json = JsonConvert.SerializeObject(db.tbPartidos, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TipoCandidatoList()
        {
            string json = JsonConvert.SerializeObject(db.tbTipoCandidato, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(json, JsonRequestBehavior.AllowGet);

        }


        public ActionResult CensoList()
        {
            string json = JsonConvert.SerializeObject(db.tbCentroElectoral, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(json, JsonRequestBehavior.AllowGet);

        }



        public ActionResult GuardarAjax(int modpan_Id, int mod_Id, string modpan_Nombre, string accion)
        {
            try
            {

                if (accion == "Editar")
                {

                    var update = db.UDP_tbModuloPantallas_Edit(modpan_Id, mod_Id, modpan_Nombre);
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


        public ActionResult EditAjax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tbCandidatos tbCandidatos = db.tbCandidatos.Find(id);
            if (tbCandidatos == null)
            {
                return HttpNotFound();
            }

            tbCandidatos response = new tbCandidatos();
            response.cand_Id = tbCandidatos.cand_Id;
            response.part_Id = tbCandidatos.part_Id;
            response.mov_Id = tbCandidatos.mov_Id;
            response.censo_Id = tbCandidatos.censo_Id;
            response.depto_Id = tbCandidatos.depto_Id;
            response.muni_Id = tbCandidatos.muni_Id;
            response.cand_Posicion = tbCandidatos.cand_Posicion;



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
