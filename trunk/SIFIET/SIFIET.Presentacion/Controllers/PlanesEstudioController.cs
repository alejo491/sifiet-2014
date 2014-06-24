using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionProgramas.Datos.Modelo;
using System.Diagnostics;

namespace SIFIET.Presentacion.Controllers
{ 
    public class PlanesEstudioController : Controller
    {

        //
        // GET: /PlanEstudio/        
        [Authorize(Roles = "Plan de Estudio")]
        public ViewResult Index(string estado = "Activo", string campo = "", string busqueda = "")
        {

            ViewBag.ResultadoOperacion = TempData["ResultadoOperacion"] as string;
            ViewBag.estado = estado;
            ViewBag.campo = campo;
            ViewBag.busqueda = busqueda;

            List<PLANESTUDIO> planesEstudios = FachadaSIFIET.ConsultarPlanesEstudios(estado, campo, busqueda);
            if (String.IsNullOrEmpty(campo) && String.IsNullOrEmpty(busqueda))
            {
                ViewBag.ResultadoBusqueda = "Hay " + planesEstudios.Count() + " registro(s)";
            }
            else if (planesEstudios.Count() == 0)
            {
                ViewBag.ResultadoBusqueda = "No se encontraron registros";
            }
            else
            {
                ViewBag.ResultadoBusqueda = "Se encontro" + planesEstudios.Count() + " registro(s)";
            }
            return View(planesEstudios);
        }

        //
        // GET: /PlanEstudio/Details/5
         [Authorize(Roles = "Plan de Estudio")]
        public ViewResult VisualizarPlanEstudio(decimal idPlanEstudio)
        {
            PLANESTUDIO planEstudio = FachadaSIFIET.ConsultarPlanEstudio(idPlanEstudio);
            return View(planEstudio);
        }

        //
        // GET: /PlanEstudio/Create
         [Authorize(Roles = "Plan de Estudio")]
        public ActionResult RegistrarPlanEstudio()
        {
            ViewBag.IDENTIFICADORPROGRAMA = new SelectList(FachadaSIFIET.ConsultarProgramasAcademicos(), "IDENTIFICADORPROGRAMA", "NOMBREPROGRAMA");
            ViewBag.Asignaturas = new SelectList(FachadaSIFIET.ConsultarAsignaturas(0, "", "Activo"), "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
            return View();
        } 

        //
        // POST: /PlanEstudio/Create

        [HttpPost]
        [Authorize(Roles = "Plan de Estudio")]
        public ActionResult RegistrarPlanEstudio(PLANESTUDIO objPlanEstudio)
        {
                        
            if (ModelState.IsValid)
            {
                if (FachadaSIFIET.RegistrarPlanEstudio(objPlanEstudio))
                {
                    TempData["ResultadoOperacion"] = "Plan de Estudios creado con Exito.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo registrar este Plan de Estudios.";
                }

            }
            else
            {
                ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo registrar este Plan de Estudios. (Model)";
            }

            ViewBag.IDENTIFICADORPROGRAMA = new SelectList(FachadaSIFIET.ConsultarProgramasAcademicos(), "IDENTIFICADORPROGRAMA", "NOMBREPROGRAMA", objPlanEstudio.IDENTIFICADORPROGRAMA);
            return View(objPlanEstudio);
        }
        
        //
        // GET: /PlanEstudio/Edit/5
        [Authorize(Roles = "Plan de Estudio")]
        public ActionResult EditarPlanEstudio(decimal idPlanEstudio)
        {
            PLANESTUDIO objPlanEstudio = FachadaSIFIET.ConsultarPlanEstudio(idPlanEstudio);
            ViewBag.IDENTIFICADORPROGRAMA = new SelectList(FachadaSIFIET.ConsultarProgramasAcademicos(), "IDENTIFICADORPROGRAMA", "NOMBREPROGRAMA", objPlanEstudio.IDENTIFICADORPROGRAMA);
            return View(objPlanEstudio);
        }

        //
        // POST: /PlanEstudio/Edit/5

        [HttpPost]
        [Authorize(Roles = "Plan de Estudio")]
        public ActionResult EditarPlanEstudio(PLANESTUDIO objPlanEstudio)
        {
            if (ModelState.IsValid)
            {
                if (FachadaSIFIET.EditarPlanEstudio(objPlanEstudio))
                {
                    TempData["ResultadoOperacion"] = "Plan de Estudios editado con Exito.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo editar el Plan de Estudios.";
                }
            }
            else
            {
                ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo editar el Plan de Estudios.";
            }
            ViewBag.IDENTIFICADORPROGRAMA = new SelectList(FachadaSIFIET.ConsultarProgramasAcademicos(), "IDENTIFICADORPROGRAMA", "NOMBREPROGRAMA", objPlanEstudio.IDENTIFICADORPROGRAMA);
            return View(objPlanEstudio);
        }

        //
        // GET: /PlanEstudio/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult EliminarPlanEstudio(decimal idPlanEstudio)
        {
            if (FachadaSIFIET.EliminarPlanEstudio(idPlanEstudio))
            {
                TempData["ResultadoOperacion"] = "Plan de Estudio eliminado con Exito.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ResultadoOperacion"] = "Ocurrio un error, no se pudo eliminar el Plan de Estudio";
            }
            return RedirectToAction("Index");
        }


        //
        // Gestion de Asinaturas por plan de estudio
        [Authorize(Roles = "Plan de Estudio")]
        public ActionResult GestionAsignaturasPlanEstudio(decimal idPlanEstudio = 0)
        {
            ViewBag.ResultadoOperacion = TempData["ResultadoOperacion"] as string;
            if (idPlanEstudio == 0) {
                return RedirectToAction("Index");
            }
            PLANESTUDIO objPlanEstudio = FachadaSIFIET.ConsultarPlanEstudio(idPlanEstudio);

            int semestre = (int) objPlanEstudio.PROGRAMA.DURACIONPROGRAMA;
            List<int> semestres = new List<int>();
            for (int i = 1; i <= semestre; i++ )
            {
                semestres.Add(i);
            }
            ViewBag.idPlanEstudio = idPlanEstudio;
            ViewBag.SEMESTRE = new SelectList(semestres);

            List<ASIGNATURA> listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0, "", "Activo");
            List<ASIGNATURA_PERTENECE_PLAN_ESTU> listaAsignaturasPlanesEstudio = FachadaSIFIET.ConsultarAsignaturasPorPlanEstudio(idPlanEstudio);

            foreach(ASIGNATURA_PERTENECE_PLAN_ESTU item in listaAsignaturasPlanesEstudio)
            {
                ASIGNATURA asignatura = listaAsignaturas.First(obj => obj.NOMBREASIGNATURA == item.ASIGNATURA.NOMBREASIGNATURA);
                listaAsignaturas.Remove(asignatura);
            }
            ViewBag.NombrePlanEstudio = objPlanEstudio.NOMBREPLANESTUDIOS;
            ViewBag.IDENTIFICADORASIGNATURA = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");                        
            return View(listaAsignaturasPlanesEstudio);
        }

        [Authorize(Roles = "Plan de Estudio")]
        public ActionResult RegistrarAsignaturaPlanEstudio(ASIGNATURA_PERTENECE_PLAN_ESTU objAsignaturaPlanEstudio)
        {

            if (objAsignaturaPlanEstudio.SEMESTRE == null || objAsignaturaPlanEstudio.IDENTIFICADORASIGNATURA == null) {
                TempData["ResultadoOperacion"] = "Debe seleccionar un semestre y una asignatura para agregar al plan de estudios";
            }
            else
            {
                if (FachadaSIFIET.RegistrarAsignaturaPlanEstudio(objAsignaturaPlanEstudio))
                {
                    TempData["ResultadoOperacion"] = "Asignatura agregada con exito al plan de estudio";

                }
                else
                {
                    TempData["ResultadoOperacion"] = "Ocurrio un error, No se pudo agregar la asignatura al plan de estudio.";

                }
            }
            
                
            return RedirectToAction("GestionAsignaturasPlanEstudio", new { idPlanEstudio = objAsignaturaPlanEstudio.IDENTIFICADORPLANESTUDIOS });
        }
        [Authorize(Roles = "Plan de Estudio")]
        public ActionResult EliminarAsignaturaPlanEstudio(decimal idPlanEstudio, decimal idAsignatura)
        {
            if (FachadaSIFIET.EliminarAsignaturaPlanEstudio(idPlanEstudio, idAsignatura))
            {
                TempData["ResultadoOperacion"] = "Asignatura desagregada con exito del plan de estudio";
            }
            else
            {
                TempData["ResultadoOperacion"] = "Ocurrio un error, no se pudo desagregar la asignatura del plan de estudio";
            }
            return RedirectToAction("GestionAsignaturasPlanEstudio", new { idPlanEstudio = idPlanEstudio });
        }
    }
}