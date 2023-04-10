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
    public class UsuariosController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();


        public ActionResult ListUsuarios()
        {
            IEnumerable<tbUsuarios> listUsuarios = db.tbUsuarios
                .Include(t => t.tbRoles).ToList();
            return Json(new
            {
                data = listUsuarios.Select(x => new
                {
                    usu_Id = x.usu_Id,
                    usu_Identidad = x.usu_Identidad,
                    usu_PrimerNombre = x.usu_PrimerNombre,
                    usu_PrimerApellido= x.usu_PrimerApellido,
                    usu_SegundoNombre= x.usu_SegundoNombre,
                    usu_SegundoApellido = x.usu_SegundoApellido,
                    usu_Telefono = x.usu_Telefono,
                    rol_Id = (x.tbRoles != null) ? x.tbRoles.rol_Nombre : "",
                    usu_EsActivo= x.usu_EsActivo,
                    usu_Contraseña = x.usu_Contraseña

                })
                .OrderBy(x => x.usu_Id)
            }, JsonRequestBehavior.AllowGet);


        }
        // GET: Usuarios
        public ActionResult Index()
        {
            var tbUsuarios = db.tbUsuarios.Include(t => t.tbRoles);
            return View(tbUsuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuarios tbUsuarios = db.tbUsuarios.Find(id);
            if (tbUsuarios == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.rol_Id = new SelectList(db.tbRoles, "rol_Id", "rol_Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "usu_Id,usu_Identidad,usu_PrimerNombre,usu_PrimerApellido,usu_SegundoNombre,usu_SegundoApellido,usu_Telefono,usu_Contraseña,rol_Id,usu_EsActivo")] tbUsuarios tbUsuarios)
        {
            if (ModelState.IsValid)
            {
                db.tbUsuarios.Add(tbUsuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rol_Id = new SelectList(db.tbRoles, "rol_Id", "rol_Nombre", tbUsuarios.rol_Id);
            return View(tbUsuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuarios tbUsuarios = db.tbUsuarios.Find(id);
            if (tbUsuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.rol_Id = new SelectList(db.tbRoles, "rol_Id", "rol_Nombre", tbUsuarios.rol_Id);
            return View(tbUsuarios);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "usu_Id,usu_Identidad,usu_PrimerNombre,usu_PrimerApellido,usu_SegundoNombre,usu_SegundoApellido,usu_Telefono,usu_Contraseña,rol_Id,usu_EsActivo")] tbUsuarios tbUsuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUsuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rol_Id = new SelectList(db.tbRoles, "rol_Id", "rol_Nombre", tbUsuarios.rol_Id);
            return View(tbUsuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuarios tbUsuarios = db.tbUsuarios.Find(id);
            if (tbUsuarios == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUsuarios tbUsuarios = db.tbUsuarios.Find(id);
            db.tbUsuarios.Remove(tbUsuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RolesList()
        {
            

            string json = JsonConvert.SerializeObject(db.tbRoles, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarAjax(tbUsuarios usuario)

        {
            try
            {

                if (usuario.accion == "Editar")
                {


                    var update = db.UDP_tbUsuario_Edit(usuario.usu_Id, usuario.usu_Identidad, usuario.usu_PrimerNombre, usuario.usu_PrimerApellido, usuario.usu_SegundoNombre,
                        usuario.usu_SegundoApellido, usuario.usu_Telefono, usuario.rol_Id, usuario.usu_EsActivo);
                    foreach (UDP_tbUsuario_Edit_Result item in update)
                    {
                        if (item.resultado.StartsWith("-1"))
                        {
                            return Json("No se pudo modificar el registro");
                        }


                    }
                    return Json("Modificado");
                }
                if (usuario.accion == "Guardar")
                {


                    var insert = db.UDP_tbUsuario_Insert(usuario.usu_Identidad, usuario.usu_PrimerNombre, usuario.usu_PrimerApellido, usuario.usu_SegundoNombre,
                        usuario.usu_SegundoApellido, usuario.usu_Telefono, usuario.passaword, usuario.rol_Id, true);
                    foreach (UDP_tbUsuario_Insert_Result item in insert)
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
                    //var delete = db.UDP_tbUsuario_Delete(usuario.usu_Id);
                    //foreach (UDP_tbUsuario_Delete_Result item in delete)
                    //{
                    //    if (item.resultado.StartsWith("-1"))
                    //        return Json("No se pudo Eliminar el registro");

                    //}
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
            tbUsuarios tbUsuarios = db.tbUsuarios.Find(id);
            if (tbUsuarios == null)
            {
                return HttpNotFound();
            }



            tbUsuarios response = new tbUsuarios();

            response.usu_Id = tbUsuarios.usu_Id;
            response.usu_Identidad = tbUsuarios.usu_Identidad;
            response.usu_PrimerNombre = tbUsuarios.usu_PrimerNombre;
            response.usu_PrimerApellido = tbUsuarios.usu_PrimerApellido;
            response.usu_SegundoNombre = tbUsuarios.usu_SegundoNombre;
            response.usu_SegundoApellido = tbUsuarios.usu_SegundoApellido;
            response.usu_Telefono = tbUsuarios.usu_Telefono;
            response.usu_Contraseña = tbUsuarios.usu_Contraseña;
            response.rol_Id = tbUsuarios.rol_Id;
            response.usu_EsActivo = tbUsuarios.usu_EsActivo;

            return Json(response);

        }

        

             public ActionResult DeleteAjax(int? id)
             {

                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    tbDepartamentos tbDepartamentos = db.tbDepartamentos.Find(id);
                    if (tbDepartamentos == null)
                    {
                        return HttpNotFound();
                    }
                  else
                   {
                    var delete = db.UDP_tbUsuario_Delete(id);
                    foreach (UDP_tbUsuario_Delete_Result item in delete)
                    {
                        if (item.resultado.StartsWith("-1"))
                            return Json("No se pudo Eliminar el registro");

                    }
                    return Json("Eliminar");
                 }
              }

            

        //public ActionResult EditAjax(int? id)
        //{
        //    tbUsuarios tbUsuarios = db.tbUsuarios.Find(id);
        //    string json = JsonConvert.SerializeObject(tbUsuarios, Formatting.Indented, new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    });

        //    return Json(json, JsonRequestBehavior.AllowGet);

        //}

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
