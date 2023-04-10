using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotoElectronico.Models;
using VotoElectronico.Repositories;

namespace VotoElectronico.Controllers
{
    public class RoleController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();
        private MododuloRepository mododuloRepository = new MododuloRepository();
        // GET: Role

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListRoles()
        {
            IEnumerable<tbRoles> listRoles = db.tbRoles.ToList();
            return Json(new
            {
                data = listRoles.Select(x => new
                {
                    rol_Id = x.rol_Id,
                    rol_Nombre = x.rol_Nombre,
                    rol_EsActivo = x.rol_EsActivo

                })
                .OrderBy(x => x.rol_Id)
            }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult AgregarRol()
        {
            var model = new RoleWiewModel();

            model.LoadTreeViewData(db.tbComponentes.ToList(),
                db.tbModulos.ToList(),
                db.tbModuloPantallas.ToList());
            ViewBag.rol_Id = 0;
            return View(nameof(EditarRol), model);
        }

        public ActionResult EditarRol(int id)
        {
            var result = db.tbRoles.FirstOrDefault(x => x.rol_Id == id);
            if (result == null)
            {
                return View(nameof(Index));
            }

           
            var model = new RoleWiewModel();
            ViewBag.rol_Id = id;
            model.rol_Id = id;
            model.rol_Nombre = result.rol_Nombre;
            model.rol_EsActivo = result.rol_EsActivo;
            model.LoadList(mododuloRepository.ListModuloPantallas(id)
                .Select(x => x.modpan_Id));
            model.LoadTreeViewData(db.tbComponentes.ToList(),
                db.tbModulos.ToList(),
                db.tbModuloPantallas.ToList());
            model.ParseTreeViewData();
                return View(model);
        }

    }


}