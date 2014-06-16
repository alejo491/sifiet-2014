using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIFIET.Aplicacion;
using SIFIET.GestionContenidos.Datos.Modelo;
using System.Web.Mvc;

namespace SIFIET.Presentacion.Controllers
{
    public class ContenidosController : Controller
    {    
        [Authorize]
        public ActionResult Index(decimal? idContenido, string nombreContenido, string estado)
        {
            ViewData["Mensaje"] = Session["varsession"];
            ViewBag.Resultado = TempData["ResultadoOperacion"] as string;
            var listaCategorias = FachadaSIFIET.ConsultarCategorias("");
            ViewBag.ListaCategorias = new SelectList(listaCategorias, "IDENTIFICADORCATEGORIA", "NOMBRECATEGORIA");
            var categoria = new SelectListItem() { Value = "2", Text = "Categoria" };
            var nombre = new SelectListItem() { Value = "1", Text = "Nombre" };
            var etiqueta = new SelectListItem() { Value = "3", Text = "Etiqueta" };
            var lista = new List<SelectListItem> { nombre, categoria, etiqueta };
            ViewBag.campoBusqueda = new SelectList(lista, "value", "text");
            if (idContenido == null | String.IsNullOrEmpty(nombreContenido))
                return View(FachadaSIFIET.ConsultarContenidos(0, nombreContenido,estado));
            else
            {
                var resultado = FachadaSIFIET.ConsultarContenidos((decimal)idContenido, nombreContenido,estado);
                if (resultado.Count == 0)
                    ViewBag.ResultadoBusqueda = "ListaVacia";
                return View(resultado);
            }
        }

        //
        // GET: /Contenidos/Details/5
        [Authorize]
        public ActionResult VisualizarContenido(decimal idContenido)
        {
            return View(FachadaSIFIET.VisualizarContenido(idContenido));
        }

        //
        // GET: /Contenidos/Create
        //[Authorize(Roles="Contenidos")]
        public ActionResult RegistrarContenido(decimal? nomCategoria)
        {
            var listaCategorias = FachadaSIFIET.ConsultarCategorias("");
            var categoria = FachadaSIFIET.ConsultarCategoria((decimal)nomCategoria);
            var atributos = categoria.ATRIBUTOes.ToList();            
            ViewBag.ListaAtributos = atributos;            
            ViewBag.ListaCategorias = new SelectList(listaCategorias, "IDENTIFICADORCATEGORIA", "NOMBRECATEGORIA");
            return View();
        }

        //
        // POST: /Contenidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Contenidos")]
        public ActionResult RegistrarContenido(CONTENIDO oContenido)
        {
            try
            {
                // TODO: Add insert logic here
                var categoria = FachadaSIFIET.ConsultarCategoria(oContenido.IDENTIFICADORCATEGORIA);
                var atributos = categoria.ATRIBUTOes.ToList();
                ViewBag.ListaAtributos = atributos; 
                var listaCategorias = FachadaSIFIET.ConsultarCategorias("");
                ViewBag.ListaCategorias = new SelectList(listaCategorias, "IDENTIFICADORCATEGORIA", "NOMBRECATEGORIA");
                if (!ModelState.IsValid) return View(oContenido);
                bool resultado = FachadaSIFIET.RegistrarContenido(oContenido, atributos);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Contenido Agregada con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Agregoar la Contenido";

                ViewBag.Mensaje = "false";

                return RedirectToAction("Index");
            }
            catch
            {
                return View(oContenido);
            }
        }

        //
        // GET: /Contenidos/Edit/5
        [Authorize(Roles = "Contenidos")]
        public ActionResult ModificarContenido(decimal idContenido)
        {
            var oContenido = FachadaSIFIET.VisualizarContenido(idContenido) as CONTENIDO;
            var listaCategorias = FachadaSIFIET.ConsultarCategorias("");
            ViewBag.ListaCategorias = new SelectList(listaCategorias, "IDENTIFICADORCATEGORIA", "NOMBRECATEGORIA");
            return View(oContenido);
        }

        //
        // POST: /Contenidos/Edit/5
        [HttpPost]
        [Authorize(Roles = "Contenidos")]
        public ActionResult ModificarContenido(CONTENIDO oContenido)
        {
            try
            {
                var listaCategorias = FachadaSIFIET.ConsultarCategorias("");
                ViewBag.ListaCategorias = new SelectList(listaCategorias, "IDENTIFICADORCATEGORIA", "NOMBRECATEGORIA");
                if (!ModelState.IsValid) return View(oContenido);
                var resultado = FachadaSIFIET.ModificarContenido(oContenido);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Contenido Modificada con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Modificar la Contenido";
                ViewBag.Mensaje = "false";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(oContenido);
            }
        }        
        [Authorize(Roles = "Contenidos")]
        public ActionResult EliminarContenido(decimal idContenido)
        {
            try
            {
                var resultado = FachadaSIFIET.EliminarContenido(idContenido);
                if (resultado)
                    TempData["ResultadoOperacion"] = "Contenido Eliminada con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Eliminar la Contenido";
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}