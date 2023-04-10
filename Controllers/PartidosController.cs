using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VotoElectronico.Models;

namespace VotoElectronico.Controllers
{
    public class PartidosController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();

        // GET: Partidos
        public ActionResult Index()
        {
            return View(db.tbPartidos.ToList());
        }

        // GET: Partidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPartidos tbPartidos = db.tbPartidos.Find(id);
            if (tbPartidos == null)
            {
                return HttpNotFound();
            }
            return View(tbPartidos);
        }

        // GET: Partidos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "part_Id,part_Nombre,part_Siglas,part_Sede,part_ColorEmblema,part_Logo")] tbPartidos tbPartidos , HttpPostedFileBase Logofile)
        {

           
            try
            {
                string extenxion = string.Empty;
                if(Logofile != null)
                {
                    extenxion = Path.GetExtension(Logofile.FileName);
                    ModelState.Remove("Logofile");
                    tbPartidos.part_Logo = $"/Content/images/partidos/{tbPartidos.part_Nombre}{extenxion}";

                }

                Logofile.SaveAs(Server.MapPath("~/Content/images/partidos/" + tbPartidos.part_Nombre + extenxion));
                if (ModelState.IsValid)
                {
                    db.tbPartidos.Add(tbPartidos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(tbPartidos);
            }
            catch (Exception e)
            {

                return View();
            }
        }

        // GET: Partidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPartidos tbPartidos = db.tbPartidos.Find(id);
            if (tbPartidos == null)
            {
                return HttpNotFound();
            }
            return View(tbPartidos);
        }

        // POST: Partidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "part_Id,part_Nombre,part_Siglas,part_Sede,part_ColorEmblema,part_Logo")] tbPartidos tbPartidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPartidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbPartidos);
        }

        // GET: Partidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPartidos tbPartidos = db.tbPartidos.Find(id);
            if (tbPartidos == null)
            {
                return HttpNotFound();
            }
            return View(tbPartidos);
        }

        // POST: Partidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPartidos tbPartidos = db.tbPartidos.Find(id);
            db.tbPartidos.Remove(tbPartidos);
            db.SaveChanges();
            return RedirectToAction("Index");
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
