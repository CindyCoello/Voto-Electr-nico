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
    public class MunicipiosController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();


       

        public ActionResult ListMunicipios()
        {
            IEnumerable<tbMunicipios> listMunicipios = db.tbMunicipios
                .Include(t => t.tbDepartamentos).ToList();
            return Json(new
            {
                data = listMunicipios.Select(x => new
                {
                   
                    muni_Codigo = x.muni_Codigo,
                    muni_Descripcion = x.muni_Descripcion,
                    depto_Id = (x.tbDepartamentos !=null)? x.tbDepartamentos.depto_Descripcion:"",
                    muni_Id = x.muni_Id


                })
                .OrderBy(x => x.muni_Id)
            }, JsonRequestBehavior.AllowGet);


        }


        // GET: Municipios


        public ActionResult Index()
        {
            var tbMunicipios = db.tbMunicipios.Include(t => t.tbDepartamentos);
            return View(tbMunicipios.ToList());
        }

        // GET: Municipios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMunicipios tbMunicipios = db.tbMunicipios.Find(id);
            if (tbMunicipios == null)
            {
                return HttpNotFound();
            }
            return View(tbMunicipios);
        }

        // GET: Municipios/Create
        public ActionResult Create()
        {
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion");
            return View();
        }

        // POST: Municipios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "muni_Id,muni_Descripcion,depto_Id")] tbMunicipios tbMunicipios)
        {
            if (ModelState.IsValid)
            {
                db.tbMunicipios.Add(tbMunicipios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbMunicipios.depto_Id);
            return View(tbMunicipios);
        }

        // GET: Municipios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMunicipios tbMunicipios = db.tbMunicipios.Find(id);
            if (tbMunicipios == null)
            {
                return HttpNotFound();
            }
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbMunicipios.depto_Id);
            return View(tbMunicipios);
        }

        // POST: Municipios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "muni_Id,muni_Descripcion,depto_Id")] tbMunicipios tbMunicipios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbMunicipios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbMunicipios.depto_Id);
            return View(tbMunicipios);
        }

        // GET: Municipios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMunicipios tbMunicipios = db.tbMunicipios.Find(id);
            if (tbMunicipios == null)
            {
                return HttpNotFound();
            }
            return View(tbMunicipios);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMunicipios tbMunicipios = db.tbMunicipios.Find(id);
            db.tbMunicipios.Remove(tbMunicipios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DepartamentosList()
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
            catch (Exception)
            {

                return Json("Error");
            }


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


        public ActionResult GuardarAjax(int muni_Id, int muni_Codigo, string descripcionMunicipio, int idDepartamento, string accion)
        {
            try
            {

                if (accion == "Editar")
                {

                    var update = db.UDP_tbMunicipios_Edit(muni_Id, muni_Codigo, descripcionMunicipio, idDepartamento);
                    foreach (UDP_tbMunicipios_Edit_Result item in update)
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


                    var insert = db.UDP_tbMunicipios_Insert(muni_Codigo, descripcionMunicipio, idDepartamento);
                    foreach (UDP_tbMunicipios_Insert_Result item in insert)
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
                    var delete = db.UDP_tbMunicipios_Delete(muni_Id);
                    foreach (UDP_tbMunicipios_Delete_Result1 item in delete)
                    {
                        if (item.resultado.StartsWith("1"))
                            return Json("No se pudo Eliminar el registro");

                    }
                    return Json("Eliminado");

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                string mensaje = $"Error :{e.InnerException.Message}";
                return Json(mensaje);
            }
        }


        //public ActionResult EditAjax(int? id)
        //{
        //    tbMunicipios tbMunicipios = db.tbMunicipios.Find(id);
        //    string json = JsonConvert.SerializeObject(tbMunicipios, Formatting.Indented, new JsonSerializerSettings
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
            tbMunicipios tbMunicipios = db.tbMunicipios.Find(id);
            if (tbMunicipios == null)
            {
                return HttpNotFound();
            }


            tbMunicipios response = new tbMunicipios();
            
            response.muni_Codigo = tbMunicipios.muni_Codigo;
            response.muni_Descripcion = tbMunicipios.muni_Descripcion;
            response.depto_Id = tbMunicipios.depto_Id;
            response.muni_Id = tbMunicipios.muni_Id;


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
