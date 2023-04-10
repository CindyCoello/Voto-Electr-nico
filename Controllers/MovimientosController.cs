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
    public class MovimientosController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();

        // GET: Movimientos
        public ActionResult Index()
        {
            var tbMovimientos = db.tbMovimientos.Include(t => t.tbPartidos);
            return View(tbMovimientos.ToList());
        }

        // GET: Movimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMovimientos tbMovimientos = db.tbMovimientos.Find(id);
            if (tbMovimientos == null)
            {
                return HttpNotFound();
            }
            return View(tbMovimientos);
        }

        // GET: Movimientos/Create
        public ActionResult Create()
        {
            ViewBag.part_Id = new SelectList(db.tbPartidos, "part_Id", "part_Nombre");
            return View();
        }

        // POST: Movimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mov_Id,part_Id,mov_Nombre,mov_Logo,mov_Eslogan")] tbMovimientos tbMovimientos, HttpPostedFileBase Logofile)
        {

            try
            {
                string extenxion = string.Empty;
                if (Logofile != null)
                {
                    extenxion = Path.GetExtension(Logofile.FileName);
                   
                    ModelState.Remove("Logofile");
                    tbMovimientos.mov_Logo = $"/Content/images/movimientos/{tbMovimientos.mov_Nombre}{extenxion}";

                }

                Logofile.SaveAs(Server.MapPath("~/Content/images/movimientos/" + tbMovimientos.mov_Nombre + extenxion));
                if (ModelState.IsValid)
                {
                    db.tbMovimientos.Add(tbMovimientos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.part_Id = new SelectList(db.tbPartidos, "part_Id", "part_Nombre");
                return View(tbMovimientos);
            }
            catch (Exception e)
            {
                ViewBag.part_Id = new SelectList(db.tbPartidos, "part_Id", "part_Nombre");
                return View(tbMovimientos);
            }
        }

        // GET: Movimientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMovimientos tbMovimientos = db.tbMovimientos.Find(id);
            if (tbMovimientos == null)
            {
                return HttpNotFound();
            }
            ViewBag.part_Id = new SelectList(db.tbPartidos, "part_Id", "part_Nombre", tbMovimientos.part_Id);
            return View(tbMovimientos);
        }

        // POST: Movimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mov_Id,part_Id,mov_Nombre,mov_Logo,mov_Eslogan")] tbMovimientos tbMovimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbMovimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.part_Id = new SelectList(db.tbPartidos, "part_Id", "part_Nombre", tbMovimientos.part_Id);
            return View(tbMovimientos);
        }

        // GET: Movimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMovimientos tbMovimientos = db.tbMovimientos.Find(id);
            if (tbMovimientos == null)
            {
                return HttpNotFound();
            }
            return View(tbMovimientos);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMovimientos tbMovimientos = db.tbMovimientos.Find(id);
            db.tbMovimientos.Remove(tbMovimientos);
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
