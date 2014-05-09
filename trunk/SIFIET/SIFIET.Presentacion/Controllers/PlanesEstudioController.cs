﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionProgramas.Datos.Modelo;
using PagedList;
using System.Diagnostics;

namespace SIFIET.Presentacion.Controllers
{ 
    public class PlanesEstudioController : Controller
    {

        //
        // GET: /PlanEstudio/

        public ViewResult Index(string campo, string busqueda, int? page)
        {

            ViewBag.Resultado = TempData["ResultadoOperacion"] as string;
            List<PLANESTUDIO> programas = FachadaSIFIET.ConsultarPlanesEstudios("",campo, busqueda);
            if (String.IsNullOrEmpty(campo) && String.IsNullOrEmpty(busqueda))
            {
                ViewBag.ResultadoBusqueda = "Hay " + programas.Count() + " registro(s)";
            }
            else if (programas.Count() == 0)
            {
                ViewBag.ResultadoBusqueda = "No se encontraron registros";
            }
            else
            {
                ViewBag.ResultadoBusqueda = "Se encontro" + programas.Count() + " registro(s)";
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(programas.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /PlanEstudio/Details/5

        public ViewResult VisualizarPlanEstudio(decimal idPlanEstudio)
        {
            PLANESTUDIO planEstudio = FachadaSIFIET.ConsultarPlanEstudio(idPlanEstudio);
            return View(planEstudio);
        }

        //
        // GET: /PlanEstudio/Create

        public ActionResult RegistrarPlanEstudio()
        {
            ViewBag.IDENTIFICADORPROGRAMA = new SelectList(FachadaSIFIET.ConsultarProgramasAcademicos(), "IDENTIFICADORPROGRAMA", "NOMBREPROGRAMA");
            return View();
        } 

        //
        // POST: /PlanEstudio/Create

        [HttpPost]
        public ActionResult RegistrarPlanEstudio(PLANESTUDIO objPlanEstudio)
        {
            Debug.WriteLine(objPlanEstudio.FECHAINICIOPLANESTUDIOS);
            Debug.WriteLine(objPlanEstudio.FECHAFINPLANESTUDIOS);
            if (ModelState.IsValid)
            {
                objPlanEstudio.ESTADOPLANESTUDIOS = "Activo";
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
                ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo registrar este Plan de Estudios.";
            }

            ViewBag.IDENTIFICADORPROGRAMA = new SelectList(FachadaSIFIET.ConsultarProgramasAcademicos(), "IDENTIFICADORPROGRAMA", "NOMBREPROGRAMA", objPlanEstudio.IDENTIFICADORPROGRAMA);
            return View(objPlanEstudio);
        }
        
        //
        // GET: /PlanEstudio/Edit/5
 
        public ActionResult EditarPlanEstudio(decimal idPlanEstudio)
        {
            PLANESTUDIO objPlanEstudio = FachadaSIFIET.ConsultarPlanEstudio(idPlanEstudio);
            ViewBag.IDENTIFICADORPROGRAMA = new SelectList(FachadaSIFIET.ConsultarProgramasAcademicos(), "IDENTIFICADORPROGRAMA", "NOMBREPROGRAMA", objPlanEstudio.IDENTIFICADORPROGRAMA);
            return View(objPlanEstudio);
        }

        //
        // POST: /PlanEstudio/Edit/5

        [HttpPost]
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
    }
}