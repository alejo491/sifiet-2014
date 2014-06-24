using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionContenidos.Datos.Modelo;
using SIFIET.GestionProgramas.Datos.Modelo;

namespace SIFIET.Presentacion.Controllers
{
    public class EtiquetasController :  Controller
    {        
        [Authorize(Roles = "Contenido")]
        public ViewResult Index(string busqueda = "")
        {

            ViewBag.ResultadoOperacion = TempData["ResultadoOperacion"] as string;
            ViewBag.busqueda = busqueda;

            List<ETIQUETA> etiquetas = FachadaSIFIET.ConsultarEtiquetas(busqueda);
            if (String.IsNullOrEmpty(busqueda))
            {
                ViewBag.ResultadoBusqueda = "Hay " + etiquetas.Count() + " registro(s)";
            }
            else if (etiquetas.Count() == 0)
            {
                ViewBag.ResultadoBusqueda = "No se encontraron registros";
            }
            else
            {
                ViewBag.ResultadoBusqueda = "Se encontro" + etiquetas.Count() + " registro(s)";
            }
            return View(etiquetas);
        }

        //
        // GET: /Contenidos/Details/idEtiqueta        
        [Authorize(Roles = "Contenido")]
        public ViewResult VisualizarEtiquetas(decimal idEtiqueta)
        {
            ETIQUETA etiqueta = FachadaSIFIET.ConsultarEtiqueta(idEtiqueta);
            return View(etiqueta);
        }

        //
        // GET: /Contenidos/Create
        [Authorize(Roles = "Contenido")]
        public ActionResult RegistrarEtiquetas()
        {
           return View();
        }

        //
        // POST: /Contenidos/Create
        [HttpPost]
        [Authorize(Roles = "Contenido")]
        public ActionResult RegistrarEtiquetas(ETIQUETA objEtiqueta)
        {

            if (ModelState.IsValid)
            {
                if (FachadaSIFIET.RegistrarEtiqueta(objEtiqueta))
                {
                    TempData["ResultadoOperacion"] = "Etiqueta creado con Exito.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo registrar esta Etiqueta.";
                }

            }
            else
            {
                ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo registrar esta Etiqueta. (Model)";
            }

            return View(objEtiqueta);
        }

        //
        // GET: /Contenidos/Edit/idEtiqueta
         [Authorize(Roles = "Contenido")]
        public ActionResult EditarEtiquetas(decimal idEtiqueta)
        {
            ETIQUETA objEtiqueta = FachadaSIFIET.ConsultarEtiqueta(idEtiqueta);
            return View(objEtiqueta);
        }

        //
        // POST: /Contenidos/Edit/idEtiqueta
        [HttpPost]
        [Authorize(Roles = "Contenido")]
        public ActionResult EditarEtiquetas(ETIQUETA objEtiqueta)
        {
            if (ModelState.IsValid)
            {
                if (FachadaSIFIET.EditarEtiqueta(objEtiqueta))
                {
                    TempData["ResultadoOperacion"] = "Etiqueta editada con Exito.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo editar la Etiqueta.";
                }
            }
            else
            {
                ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo editar la Etiqueta.";
            }
            return View(objEtiqueta);
        }

        //
        // GET: /Contenidos/Delete/idEtiqueta
        [Authorize(Roles = "Administrador")]
        public ActionResult EliminarEtiquetas(decimal idEtiqueta)
        {
            if (FachadaSIFIET.EliminarEtiqueta(idEtiqueta))
            {
                TempData["ResultadoOperacion"] = "Etiqueta eliminada con Exito.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ResultadoOperacion"] = "Ocurrio un error, no se pudo eliminar la Etiqueta";
            }
            return RedirectToAction("Index");
        }

    }
}