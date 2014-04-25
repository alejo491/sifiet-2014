using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionProgramas.Datos.Modelo;

namespace SIFIET.Presentacion.Controllers
{
    public class CursosController : Controller
    {
        public ActionResult Index(decimal? idCurso, string nombreCurso)
        {
            ViewData["Mensaje"] = Session["varsession"];
            ViewBag.Resultado = TempData["ResultadoOperacion"] as string;
            //var selectItems = new Dictionary<decimal, string> {{1, "Idenfificador"}, {2, "Nombre Curso"}};
            var one = new SelectListItem() {Value = "1", Text = "Identificador"};
            var two = new SelectListItem() {Value = "2", Text = "Nombre"};
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(one);
            lista.Add(two);
            ViewBag.campoBusqueda = new SelectList(lista,"value","text");
            try
            {
                if (idCurso == null | String.IsNullOrEmpty(nombreCurso))
                    return View(FachadaSIFIET.ConsultarCursos(0, nombreCurso));
                else
                {
                    if (idCurso == 1)
                        return View(FachadaSIFIET.ConsultarCursos(decimal.Parse(nombreCurso), ""));
                    if (idCurso == 2)
                        return View(FachadaSIFIET.ConsultarCursos(0, nombreCurso));
                    else
                        return View(FachadaSIFIET.ConsultarCursos(0, nombreCurso));
                }

            }
            catch (Exception)
            {

                return View(FachadaSIFIET.ConsultarCursos(0, ""));
            }
            
        }

        public ActionResult VisualizarCurso(decimal idCurso)
        {
            return View(FachadaSIFIET.VisualizarCurso(idCurso));
        }

        public ActionResult RegistrarCurso()
        {
            var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas("");
            ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarCurso(CURSO oCurso)
        {
            try
            {
                // TODO: Add insert logic here
                var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas("");
                ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
                if (!ModelState.IsValid) return View(oCurso);
                bool resultado = FachadaSIFIET.RegistrarCurso(oCurso);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Curso Agregado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Agregar el Curso";

                ViewBag.Mensaje = "false";

                return RedirectToAction("Index");
            }
            catch
            {
                return View(oCurso);
            }
        }

        public ActionResult ModificarCurso(decimal idCurso)
        {
            var oCurso = FachadaSIFIET.VisualizarCurso(idCurso) as CURSO;
            var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas("");
            ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
            return View(oCurso);
        }

        [HttpPost]
        public ActionResult ModificarCurso(CURSO oCurso)
        {
            try
            {
                var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas("");
                ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
                
                if (!ModelState.IsValid) return View(oCurso);
                var resultado = FachadaSIFIET.ModificarCurso(oCurso);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Curso Modificado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Modificar el Curso";
                ViewBag.Mensaje = "false";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(oCurso);
            }
        }
        
        public ActionResult EliminarCurso(decimal idCurso)
        {
            return View(FachadaSIFIET.VisualizarCurso(idCurso));
        }

        
        [HttpPost, ActionName("EliminarCurso")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarCursoConfirmacion(decimal idCurso)
        {
            try
            {
                var resultado = FachadaSIFIET.EliminarCurso(idCurso);
                if (resultado)
                    TempData["ResultadoOperacion"] = "Curso Eliminado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Eliminar el Curso";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}