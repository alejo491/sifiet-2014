using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.GestionInfraestructura.Datos.Modelo;
using SIFIET.Aplicacion;

namespace SIFIET.Presentacion.Controllers
{
    public class SalonesController : Controller
    {
        public ActionResult Index(decimal? idSalon, string nombreSalon, string estado)
        {
            ViewData["Mensaje"] = Session["varsession"];
            ViewBag.Resultado = TempData["ResultadoOperacion"] as string;
            var identificacion = new SelectListItem() { Value = "1", Text = "Identificador" };
            var nombresalon = new SelectListItem() { Value = "2", Text = "Nombre" };
            var nombreFacultad = new SelectListItem() { Value = "3", Text = "Facultad" };
            var lista = new List<SelectListItem> {identificacion, nombresalon, nombreFacultad};
            ViewBag.campoBusqueda = new SelectList(lista, "value", "text");
                if (idSalon == null | String.IsNullOrEmpty(nombreSalon))
                    return View(FachadaSIFIET.ConsultarSalones(0, nombreSalon,estado));
                else
                {
                    var resultado = FachadaSIFIET.ConsultarSalones((decimal) idSalon, nombreSalon, estado);
                    if (resultado.Count == 0)
                        ViewBag.ResultadoBusqueda = "ListaVacia";
                    return View(resultado);
                }
        }

        public ActionResult VisualizarSalon(decimal idSalon)
        {
            return View(FachadaSIFIET.VisualizarSalon(idSalon));
        }

        public ActionResult RegistrarSalon()
        {
            var listaFacultades = FachadaSIFIET.ConsultarFacultades(0,"");
            ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarSalon(SALON oSalon)
        {
            try
            {
                // TODO: Add insert logic here
                var listaFacultades = FachadaSIFIET.ConsultarFacultades(0,"");
                ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
                if (!ModelState.IsValid) return View(oSalon);
                bool resultado = FachadaSIFIET.RegistrarSalon(oSalon);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Salon Agregado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Agregar el Salon";

                ViewBag.Mensaje = "false";

                return RedirectToAction("Index");
            }
            catch
            {
                return View(oSalon);
            }
        }

        public ActionResult ModificarSalon(decimal idSalon)
        {
            var oSalon = FachadaSIFIET.VisualizarSalon(idSalon) as SALON;
            var listaFacultades = FachadaSIFIET.ConsultarFacultades(0,"");
            ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
            return View(oSalon);
        }

        [HttpPost]
        public ActionResult ModificarSalon(SALON oSalon)
        {
            try
            {
                var listaFacultades = FachadaSIFIET.ConsultarFacultades(0,"");
                ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");

                if (!ModelState.IsValid) return View(oSalon);
                var resultado = FachadaSIFIET.ModificarSalon(oSalon);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Salon Modificado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Modificar el Salon";
                ViewBag.Mensaje = "false";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(oSalon);
            }
        }
        /*
        public ActionResult EliminarSalon(decimal idSalon)
        {
            return View(FachadaSIFIET.VisualizarSalon(idSalon));
        }


        [HttpPost, ActionName("EliminarSalon")]
        [ValidateAntiForgeryToken]*/
        public ActionResult EliminarSalon(decimal idSalon)
        {
            try
            {
                var resultado = FachadaSIFIET.EliminarSalon(idSalon);
                if (resultado)
                    TempData["ResultadoOperacion"] = "Salon Eliminado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Eliminar el Salon";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ResultadoOperacion"] = "Fallo al Eliminar el Salon";
                return RedirectToAction("Index");
            }
        }
    }
}