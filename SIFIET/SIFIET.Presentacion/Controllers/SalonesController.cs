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
        public ActionResult Index(decimal? idSalon, string nombreSalon)
        {
            ViewData["Mensaje"] = Session["varsession"];
            ViewBag.Resultado = TempData["ResultadoOperacion"] as string;
            var one = new SelectListItem() { Value = "1", Text = "Identificador" };
            var two = new SelectListItem() { Value = "2", Text = "Nombre" };
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(one);
            lista.Add(two);
            ViewBag.campoBusqueda = new SelectList(lista, "value", "text");
            try
            {
                if (idSalon == null | String.IsNullOrEmpty(nombreSalon))
                    return View(FachadaSIFIET.ConsultarSalones(0, nombreSalon));
                else
                {
                    if (idSalon == 1)
                        return View(FachadaSIFIET.ConsultarSalones(decimal.Parse(nombreSalon), ""));
                    if (idSalon == 2)
                        return View(FachadaSIFIET.ConsultarSalones(0, nombreSalon));
                    else
                        return View(FachadaSIFIET.ConsultarSalones(0, nombreSalon));
                }

            }
            catch (Exception)
            {

                return View(FachadaSIFIET.ConsultarSalones(0, ""));
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

        public ActionResult EliminarSalon(decimal idSalon)
        {
            return View(FachadaSIFIET.VisualizarSalon(idSalon));
        }


        [HttpPost, ActionName("EliminarSalon")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarSalonConfirmacion(decimal idSalon)
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
                return View();
            }
        }
    }
}