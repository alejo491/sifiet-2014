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
    public class CategoriasController : Controller
    {

        public ViewResult Index(string busqueda = "")
        {

            ViewBag.ResultadoOperacion = TempData["ResultadoOperacion"] as string;
            ViewBag.busqueda = busqueda;
            
            List<CATEGORIA> categorias = FachadaSIFIET.ConsultarCategorias(busqueda);
            if (String.IsNullOrEmpty(busqueda))
            {
                ViewBag.ResultadoBusqueda = "Hay " + categorias.Count() + " registro(s)";
            }
            else if (categorias.Count() == 0)
            {
                ViewBag.ResultadoBusqueda = "No se encontraron registros";
            }
            else
            {
                ViewBag.ResultadoBusqueda = "Se encontro" + categorias.Count() + " registro(s)";
            }
            return View(categorias);
        }        

        //
        // GET: /Contenidos/Create
        public ActionResult RegistrarCategoria()
        {
            return View();
        }

        //
        // POST: /Contenidos/Create
        [HttpPost]
        public ActionResult RegistrarCategoria(CATEGORIA categoriaIn)
        {

            if (ModelState.IsValid)
            {
                var idCategoria = FachadaSIFIET.RegistrarCategoria(categoriaIn);
                if (idCategoria > 0)
                {
                    TempData["ResultadoOperacion"] = "Categoría creada con Éxito.";
                    categoriaIn = FachadaSIFIET.ConsultarCategoria(idCategoria);
                    return RedirectToAction("RegistrarAtributo",categoriaIn);
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

            return View(categoriaIn);            
        }

        public ActionResult RegistrarAtributo(CATEGORIA categoriaIn)
        {
            return View(categoriaIn);
        }

    }
}