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
    public class CentroElectoralController : Controller
    {
        private VotoElectronicoEntities3 db = new VotoElectronicoEntities3();


       


        public ActionResult ListCenso()
        {
            IEnumerable<tbCentroElectoral> listCenso = db.tbCentroElectoral.Include(t => t.tbAldeas)
                                                                         .Include(t => t.tbCentrosVotacion)
                                                                         .Include(t => t.tbDepartamentos)
                                                                         .Include(t => t.tbEstadosCivil)
                                                                         .Include(t => t.tbMunicipios)
                                                                         .Include(t => t.tbPoblados)
                                                                         .Where(y => y.censo_EsHabilitado == true)
                                                                         .ToList();

            return Json(new
            {
                data = listCenso.Select(x => new {
                    censo_Id = x.censo_Id,
                    censo_Identidad = x.censo_Identidad,

                    censo_PrimerNombre = x.censo_PrimerNombre,
                    censo_PrimerApellido = x.censo_PrimerApellido,
                    censo_CodigoSexo = x.censo_CodigoSexo,
                    estadoCivil = (x.tbEstadosCivil != null) ? x.tbEstadosCivil.estCiv_Descripcion : "",
                    pobl_Id = (x.tbPoblados != null) ? x.tbPoblados.pobl_Descripcion : "",
                    aldea_Id = (x.tbAldeas != null) ? x.tbAldeas.aldea_Descripcion : "",
                    muni_Id = (x.tbMunicipios != null) ? x.tbMunicipios.muni_Descripcion : ""




                })
                .OrderBy(x => x.censo_Id)
            }, JsonRequestBehavior.AllowGet);
        }


        //Estados Civiles
        public ActionResult Estadolist()
        {
            

            IEnumerable<tbEstadosCivil> listEstados = db.tbEstadosCivil.ToList();
            return Json(new
            {
                data = listEstados.Select(x => new
                {
                    estCiv_Id = x.estCiv_Id,
                    estCiv_Descripcion = x.estCiv_Descripcion,
                })

            }, JsonRequestBehavior.AllowGet);
        }


        //Departamento lista

        public ActionResult DeptoList()
        {
           
            IEnumerable<tbDepartamentos> listDepto = db.tbDepartamentos.ToList();
            return Json(new
            {
                data = listDepto.Select(x => new
                {
                    depto_Id = x.depto_Id,
                    depto_Descripcion = x.depto_Descripcion
                })

            }, JsonRequestBehavior.AllowGet);
        }

        //Municipios Lista
        public ActionResult MunicipiosList(int id)
        {
           
            IEnumerable<tbMunicipios> listMun = db.tbMunicipios.Include(x => x.tbDepartamentos).ToList();
            return Json(new
            {
                data = listMun.Select(x => new
                {
                    muni_Id = x.muni_Id,
                    muni_Descripcion = x.muni_Descripcion,
                    depto_Id = x.depto_Id
                }).Where(x => x.depto_Id == id)

            }, JsonRequestBehavior.AllowGet);
        }


        //Centros de Votacion Lista
        public ActionResult CentroList(int id, int muni_Id)
        {
            


            IEnumerable<tbCentrosVotacion> listCentros = db.tbCentrosVotacion.Include(x => x.tbMunicipios.tbDepartamentos).ToList();
            return Json(new
            {
                data = listCentros.Select(x => new
                {
                    cenvot_Id = x.cenvot_Id,
                    cenvot_Nombre = x.cenvot_Nombre,
                    depto_Id = x.depto_Id,
                    muni_Id = x.muni_Id
                }).Where(x => x.depto_Id == id && x.muni_Id == muni_Id)

            }, JsonRequestBehavior.AllowGet);

        }



        //aldeas Lista
        public ActionResult AldeasList(int id, int muni_Id)
        {
           


            IEnumerable<tbAldeas> listAldeas = db.tbAldeas.Include(x => x.tbMunicipios.tbDepartamentos).ToList();
            return Json(new
            {
                data = listAldeas.Select(x => new
                {
                    aldea_Id = x.aldea_Id,
                    aldea_Descripcion = x.aldea_Descripcion,
                    depto_Id = x.depto_Id,
                    muni_Id = x.muni_Id
                }).Where(x => x.depto_Id == id && x.muni_Id == muni_Id)

            }, JsonRequestBehavior.AllowGet);
        }


        //Poblados Lista
        public ActionResult PobladoList(int Id, int idmunicipio, int idaldea)
        {
           

            IEnumerable<tbPoblados> listPoblados = db.tbPoblados.Include(x => x.tbAldeas.tbMunicipios.tbDepartamentos).ToList();
            return Json(new
            {
                data = listPoblados.Select(x => new
                {
                    pobl_Id = x.pobl_Id,
                    pobl_Descripcion = x.pobl_Descripcion,
                    depto_Id = x.depto_Id,
                    muni_Id = x.muni_Id,
                    aldea_Id = x.aldea_Id
                }).Where(x => x.depto_Id == Id && x.muni_Id == idmunicipio && x.aldea_Id == idaldea)

            }, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult MostrarDatosCensoAjax2(int? id)
        {
            var tbCentroElectoral = db.tbCentroElectoral.Include(t => t.tbAldeas)
                                                                          .Include(t => t.tbCentrosVotacion)
                                                                          .Include(t => t.tbDepartamentos)
                                                                          .Include(t => t.tbEstadosCivil)
                                                                          .Include(t => t.tbMunicipios)
                                                                          .Include(t => t.tbPoblados)
                                                                          .Where(y => y.censo_EsHabilitado == true);
            IEnumerable<tbCentroElectoral> listCenso = tbCentroElectoral.ToList();

            return Json(new
            {
                data = listCenso.Select(x => new
                {
                    censo_Id = x.censo_Id,
                    censo_Identidad = x.censo_Identidad,
                    censo_PrimerNombre = x.censo_PrimerNombre,
                    censo_SegundoNombre = x.censo_SegundoNombre,
                    censo_PrimerApellido = x.censo_PrimerApellido,
                    censo_SegundoApellido = x.censo_SegundoApellido,
                    censo_CodigoSexo = x.censo_CodigoSexo,
                    censo_FechaNacimiento = x.censo_FechaNacimiento.ToString("yyyy-MM-dd"),
                    estCiv_Id = x.estCiv_Id,
                    depto_Id = x.depto_Id,
                    muni_Id = x.muni_Id,
                    aldea_Id = x.aldea_Id,
                    pobl_Id = x.pobl_Id,
                    censo_CodigoArea = x.censo_CodigoArea,
                    censo_CodigoSectorElectoral = x.censo_CodigoSectorElectoral,
                    cenvot_Id = x.cenvot_Id,
                    censo_NumeroLinea = x.censo_NumeroLinea


                }).OrderBy(x => x.censo_Id).Where(x => x.censo_Id == id)

            }, JsonRequestBehavior.AllowGet);

        }

        // GET: CentroElectoral
        public ActionResult Index()
        {
            var tbCentroElectoral = db.tbCentroElectoral.Include(t => t.tbAldeas).Include(t => t.tbDepartamentos).Include(t => t.tbEstadosCivil).Include(t => t.tbMunicipios).Include(t => t.tbPoblados);
            return View(tbCentroElectoral.ToList());
        }

        // GET: CentroElectoral/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCentroElectoral tbCentroElectoral = db.tbCentroElectoral.Find(id);
            if (tbCentroElectoral == null)
            {
                return HttpNotFound();
            }
            return View(tbCentroElectoral);
        }

        // GET: CentroElectoral/Create
        public ActionResult Create()
        {
            ViewBag.aldea_Id = new SelectList(db.tbAldeas, "aldea_Id", "aldea_Descripcion");
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion");
            ViewBag.estCiv_Id = new SelectList(db.tbEstadosCivil, "estCiv_Id", "estCiv_Descripcion");
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion");
            ViewBag.pobl_Id = new SelectList(db.tbPoblados, "pobl_Id", "pobl_Descripcion");
            return View();
        }

        // POST: CentroElectoral/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "censo_Id,censo_Identidad,censo_PrimerNombre,censo_SegundoNombre,censo_PrimerApellido,censo_SegundoApellido,censo_CodigoSexo,censo_FechaNacimiento,estCiv_Id,depto_Id,muni_Id,aldea_Id,pobl_Id,censo_CodigoArea,censo_CodigoSectorElectoral,cenvot_Id,censo_NumeroLinea,censo_EsHabilitado")] tbCentroElectoral tbCentroElectoral)
        {
            if (ModelState.IsValid)
            {
                db.tbCentroElectoral.Add(tbCentroElectoral);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aldea_Id = new SelectList(db.tbAldeas, "aldea_Id", "aldea_Descripcion", tbCentroElectoral.aldea_Id);
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbCentroElectoral.depto_Id);
            ViewBag.estCiv_Id = new SelectList(db.tbEstadosCivil, "estCiv_Id", "estCiv_Descripcion", tbCentroElectoral.estCiv_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbCentroElectoral.muni_Id);
            ViewBag.pobl_Id = new SelectList(db.tbPoblados, "pobl_Id", "pobl_Descripcion", tbCentroElectoral.pobl_Id);
            return View(tbCentroElectoral);
        }

        // GET: CentroElectoral/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCentroElectoral tbCentroElectoral = db.tbCentroElectoral.Find(id);
            if (tbCentroElectoral == null)
            {
                return HttpNotFound();
            }
            ViewBag.aldea_Id = new SelectList(db.tbAldeas, "aldea_Id", "aldea_Descripcion", tbCentroElectoral.aldea_Id);
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbCentroElectoral.depto_Id);
            ViewBag.estCiv_Id = new SelectList(db.tbEstadosCivil, "estCiv_Id", "estCiv_Descripcion", tbCentroElectoral.estCiv_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbCentroElectoral.muni_Id);
            ViewBag.pobl_Id = new SelectList(db.tbPoblados, "pobl_Id", "pobl_Descripcion", tbCentroElectoral.pobl_Id);
            return View(tbCentroElectoral);
        }

        // POST: CentroElectoral/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "censo_Id,censo_Identidad,censo_PrimerNombre,censo_SegundoNombre,censo_PrimerApellido,censo_SegundoApellido,censo_CodigoSexo,censo_FechaNacimiento,estCiv_Id,depto_Id,muni_Id,aldea_Id,pobl_Id,censo_CodigoArea,censo_CodigoSectorElectoral,cenvot_Id,censo_NumeroLinea,censo_EsHabilitado")] tbCentroElectoral tbCentroElectoral)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCentroElectoral).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aldea_Id = new SelectList(db.tbAldeas, "aldea_Id", "aldea_Descripcion", tbCentroElectoral.aldea_Id);
            ViewBag.depto_Id = new SelectList(db.tbDepartamentos, "depto_Id", "depto_Descripcion", tbCentroElectoral.depto_Id);
            ViewBag.estCiv_Id = new SelectList(db.tbEstadosCivil, "estCiv_Id", "estCiv_Descripcion", tbCentroElectoral.estCiv_Id);
            ViewBag.muni_Id = new SelectList(db.tbMunicipios, "muni_Id", "muni_Descripcion", tbCentroElectoral.muni_Id);
            ViewBag.pobl_Id = new SelectList(db.tbPoblados, "pobl_Id", "pobl_Descripcion", tbCentroElectoral.pobl_Id);
            return View(tbCentroElectoral);
        }

        // GET: CentroElectoral/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCentroElectoral tbCentroElectoral = db.tbCentroElectoral.Find(id);
            if (tbCentroElectoral == null)
            {
                return HttpNotFound();
            }
            return View(tbCentroElectoral);
        }

        // POST: CentroElectoral/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCentroElectoral tbCentroElectoral = db.tbCentroElectoral.Find(id);
            db.tbCentroElectoral.Remove(tbCentroElectoral);
            db.SaveChanges();
            return RedirectToAction("Index");
        }









        public ActionResult GuardarCensoAjax(string Censoid, string identidadCenso, string primerNombreCenso,
                                               string segundoNombreCenso, string primerApellidoCenso,
                                               string segundoApellidoCenso, string sexoCenso, string fechaNacCensoE,
                                               string idEstado, string idDepto, string idMun, string idAldea, string idPoblado,
                                               string codigoAreaCensoE, string sectorElectoralCensoE, string idCentro,
                                               string numeroLineaCensoE, bool esHabilitadoCenso, string accion)
        {
            try
            {

                int idCenso = int.Parse(Censoid);


                if (idCenso != 0 && accion == "Editar")
                {
                    int PobladoId = int.Parse(idPoblado);
                    int CentroId = int.Parse(idCentro);
                    int EstadoId = int.Parse(idEstado);
                    int AldeaId = int.Parse(idAldea);
                    int MunId = int.Parse(idMun);
                    int DeptoId = int.Parse(idDepto);
                    System.DateTime fechaNacCenso = System.DateTime.Parse(fechaNacCensoE);
                    int codigoAreaCenso = int.Parse(codigoAreaCensoE);
                    int numeroLineaCenso = int.Parse(numeroLineaCensoE);
                    int sectorElectoralCenso = int.Parse(sectorElectoralCensoE);

                    var update = db.UDP_tbCentroElectoral_Edit(idCenso, identidadCenso, primerNombreCenso,
                                                                segundoNombreCenso, primerApellidoCenso,
                                                                segundoApellidoCenso, sexoCenso, fechaNacCenso,
                                                                EstadoId, DeptoId, MunId, AldeaId, PobladoId,
                                                                codigoAreaCenso, sectorElectoralCenso, CentroId,
                                                                numeroLineaCenso, esHabilitadoCenso);
                    foreach (UDP_tbCentroElectoral_Edit_Result item in update)
                    {
                        if (item.resultado.StartsWith("-1"))
                        {
                            return Json("No se pudo actualizar el registro");
                        }
                    }
                    return Json("Editar");
                }
                if (idCenso != 0 && accion == "Delete")
                {

                    var delete = db.UDP_tbCentroElectoral_Delete(idCenso);
                    foreach (UDP_tbCentroElectoral_Delete_Result item in delete)
                    {
                        if (item.resultado.StartsWith("-1"))
                        {
                            return Json("No se pudo Eliminar el registro");
                        }
                    }
                    return Json("DeleteLogic");
                }
                else
                {
                    int PobladoId = int.Parse(idPoblado);
                    int CentroId = int.Parse(idCentro);
                    int EstadoId = int.Parse(idEstado);
                    int AldeaId = int.Parse(idAldea);
                    int MunId = int.Parse(idMun);
                    int DeptoId = int.Parse(idDepto);
                    System.DateTime fechaNacCenso = System.DateTime.Parse(fechaNacCensoE);
                    int codigoAreaCenso = int.Parse(codigoAreaCensoE);
                    int numeroLineaCenso = int.Parse(numeroLineaCensoE);
                    int sectorElectoralCenso = int.Parse(sectorElectoralCensoE);
                    var insert = db.UDP_tbCentroElectoral_Insert(identidadCenso, primerNombreCenso,
                                                                segundoNombreCenso, primerApellidoCenso,
                                                                segundoApellidoCenso, sexoCenso, fechaNacCenso,
                                                                EstadoId, DeptoId, MunId, AldeaId, PobladoId,
                                                                codigoAreaCenso, sectorElectoralCenso, CentroId,
                                                                numeroLineaCenso, esHabilitadoCenso);
                    foreach (UDP_tbCentroElectoral_Insert_Result item in insert)
                    {
                        if (item.resultado.StartsWith("-1"))
                        {
                            return Json("No se pudo Ingresar el registro");
                        }
                    }
                    return Json("Nuevo");
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

            tbCentroElectoral tbCentroElectoral = db.tbCentroElectoral.Find(id);
            if (tbCentroElectoral == null)
            {
                return HttpNotFound();
            }

            tbCentroElectoral response = new tbCentroElectoral();
            response.censo_Id = tbCentroElectoral.censo_Id;
            response.censo_Identidad = tbCentroElectoral.censo_Identidad;
            response.censo_PrimerNombre = tbCentroElectoral.censo_PrimerNombre;
            response.censo_SegundoNombre = tbCentroElectoral.censo_SegundoNombre;
            response.censo_PrimerApellido = tbCentroElectoral.censo_PrimerApellido;

            response.censo_SegundoApellido = tbCentroElectoral.censo_PrimerApellido;
            response.censo_CodigoSexo = tbCentroElectoral.censo_CodigoSexo;
            response.censo_FechaNacimiento = tbCentroElectoral.censo_FechaNacimiento;
            response.estCiv_Id = tbCentroElectoral.estCiv_Id;
            response.depto_Id = tbCentroElectoral.depto_Id;
            response.aldea_Id = tbCentroElectoral.aldea_Id;
            response.pobl_Id = tbCentroElectoral.pobl_Id;
            response.censo_CodigoArea = tbCentroElectoral.censo_CodigoArea;
            response.censo_CodigoSectorElectoral = tbCentroElectoral.censo_CodigoSectorElectoral;
            response.cenvot_Id = tbCentroElectoral.cenvot_Id;
            response.censo_NumeroLinea = tbCentroElectoral.censo_NumeroLinea;
            response.censo_EsHabilitado = tbCentroElectoral.censo_EsHabilitado;
            

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
