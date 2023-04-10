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
    public class TipoCandidatoController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();



        public ActionResult ListTipoCandidato()
        {
            IEnumerable<tbTipoCandidato> listTipoCandidato = db.tbTipoCandidato.ToList();
            return Json(new
            {
                data = listTipoCandidato.Select(x => new
                {
                    tipcan_Id = x.tipcan_Id,
                    tipcan_Descripcion = x.tipcan_Descripcion

                })
                .OrderBy(x => x.tipcan_Descripcion)
            }, JsonRequestBehavior.AllowGet);


        }

        // GET: TipoCandidatoes
        public ActionResult Index()
        {
            return View(db.tbTipoCandidato.ToList());
        }

        // GET: TipoCandidatoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoCandidato tbTipoCandidato = db.tbTipoCandidato.Find(id);
            if (tbTipoCandidato == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoCandidato);
        }

        // GET: TipoCandidatoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoCandidatoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tipcan_Id,tipcan_Descripcion")] tbTipoCandidato tbTipoCandidato)
        {
            if (ModelState.IsValid)
            {
                db.tbTipoCandidato.Add(tbTipoCandidato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTipoCandidato);
        }

        // GET: TipoCandidatoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoCandidato tbTipoCandidato = db.tbTipoCandidato.Find(id);
            if (tbTipoCandidato == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoCandidato);
        }

        // POST: TipoCandidatoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tipcan_Id,tipcan_Descripcion")] tbTipoCandidato tbTipoCandidato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoCandidato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTipoCandidato);
        }

        // GET: TipoCandidatoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoCandidato tbTipoCandidato = db.tbTipoCandidato.Find(id);
            if (tbTipoCandidato == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoCandidato);
        }

        // POST: TipoCandidatoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoCandidato tbTipoCandidato = db.tbTipoCandidato.Find(id);
            db.tbTipoCandidato.Remove(tbTipoCandidato);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult GuardarAjax(string nombreTipoCandidato, int idtipCandidato)
        {
            try
            {
                tbTipoCandidato tbTipoCandidato;
                if (idtipCandidato != 0)
                {
                    tbTipoCandidato = new tbTipoCandidato
                    {
                        tipcan_Id = idtipCandidato,
                        tipcan_Descripcion = nombreTipoCandidato
                    };

                    //db.Entry(tbTipoCandidato).State = EntityState.Modified;
                    //db.SaveChanges();
                    //return Json("Registro Modificado correctamente");
                    var update = db.UDP_tbTipoCandidato_Edit(idtipCandidato, nombreTipoCandidato);
                    foreach (UDP_tbTipoCandidato_Edit_Result item in update)
                    {
                        if (item.resultado.StartsWith("-1"))
                            return Json("No se pudo modificar el registro");

                    }
                    return Json("Modificado");
                }
                else
                {
                    var insert = db.UDP_tbTipoCandidato_Insert(nombreTipoCandidato);
                    foreach (UDP_tbTipoCandidato_Insert_Result item in insert)
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

        public ActionResult EditAjax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoCandidato tbTipoCandidato = db.tbTipoCandidato.Find(id);
            if (tbTipoCandidato == null)
            {
                return HttpNotFound();
            }
            return Json(tbTipoCandidato);
        }


        public ActionResult DeleteAjax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoCandidato tbTipoCandidato = db.tbTipoCandidato.Find(id);
            if (tbTipoCandidato == null)
            {
                return HttpNotFound();

            }

            else
            {
                var delete = db.UDP_tbTipoCandidato_Delete(id);
                foreach (UDP_tbTipoCandidato_Delete_Result item in delete)
                {
                    if (item.resultado.StartsWith("-1"))
                        return Json("No se pudo Eliminar el registro");

                }
                return Json("Eliminado");
            }

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
