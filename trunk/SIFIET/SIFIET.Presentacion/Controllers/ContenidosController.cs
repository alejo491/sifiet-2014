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
        [Authorize(Roles = "Contenido")]
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
        [Authorize(Roles = "Contenido")]
        public ActionResult VisualizarContenido(decimal idContenido)
        {
            var atributos = FachadaSIFIET.ConsultarAtributosDelContenido(idContenido);
            var etiquetasContenido = FachadaSIFIET.ConsultarEtiquetasDelContenido(idContenido);            
            ViewBag.ListaAtributos = atributos;
            ViewBag.ListaEtiquetas = etiquetasContenido;
            //ViewBag.ListaEtiquetas = new MultiSelectList(etiquetas, "IDENTIFICADORETIQUETA", "NOMBREETIQUETA",etiquetasContenido);
            return View(FachadaSIFIET.VisualizarContenido(idContenido));
        }

        //
        // GET: /Contenidos/Create
        [Authorize(Roles="Contenido")]
        public ActionResult RegistrarContenido(decimal? nomCategoria)
        {
            if (nomCategoria != null)
            {
                var categoria = FachadaSIFIET.ConsultarCategoria((decimal)nomCategoria);
                var atributos = categoria.ATRIBUTOes.ToList();
                var etiquetas = FachadaSIFIET.ConsultarEtiquetas("");
                ViewBag.ListaAtributos = atributos;
                ViewBag.ListaEtiquetas = new MultiSelectList(etiquetas, "IDENTIFICADORETIQUETA", "NOMBREETIQUETA");
                ViewBag.idCategoria = categoria.IDENTIFICADORCATEGORIA;
                return View();
            }
            return RedirectToAction("Index");            
        }

        //
        // POST: /Contenidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Contenido")]
        public ActionResult RegistrarContenido(CONTENIDO oContenido, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here                
                ViewBag.idCategoria = oContenido.IDENTIFICADORCATEGORIA;
                var categoria = FachadaSIFIET.ConsultarCategoria(oContenido.IDENTIFICADORCATEGORIA);
                var atributos = categoria.ATRIBUTOes.ToList();
                var datosAtributos = CrearAtributos(collection,atributos);
                var etiquetas = FachadaSIFIET.ConsultarEtiquetas("");
                ViewBag.ListaAtributos = datosAtributos;
                ViewBag.ListaEtiquetas = new MultiSelectList(etiquetas, "IDENTIFICADORETIQUETA", "NOMBREETIQUETA");
                var etiquetasContenido = CrearEtiquetas(collection,etiquetas);

                if (!ModelState.IsValid) return View(oContenido);
                bool resultado = FachadaSIFIET.RegistrarContenido(oContenido, datosAtributos, etiquetasContenido);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Contenido Agregado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Agregar el Contenido";

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
        [Authorize(Roles = "Contenido")]
        public ActionResult ModificarContenido(decimal idContenido)
        {
            var oContenido = FachadaSIFIET.VisualizarContenido(idContenido) as CONTENIDO;
            var atributos = FachadaSIFIET.ConsultarAtributosDelContenido(idContenido);
            var etiquetasContenido = FachadaSIFIET.ConsultarEtiquetasDelContenido(idContenido);
            var etiquetas = FachadaSIFIET.ConsultarEtiquetas("");
            ViewBag.ListaAtributos = atributos;
            MultiSelectList hola = new MultiSelectList(etiquetas, "IDENTIFICADORETIQUETA", "NOMBREETIQUETA", etiquetasContenido);
            ViewBag.ListaEtiquetas = new MultiSelectList(etiquetas, "IDENTIFICADORETIQUETA", "NOMBREETIQUETA",etiquetasContenido);
            return View(oContenido);
        }

        //
        // POST: /Contenidos/Edit/5
        [HttpPost]
        [Authorize(Roles = "Contenido")]
        public ActionResult ModificarContenido(CONTENIDO oContenido, FormCollection collection)
        {
            try
            {
                var atributos = FachadaSIFIET.ConsultarAtributosDelContenido(oContenido.IDENTIFICADORCONTENIDO);
                var etiquetasContenidoSeleccionadas = FachadaSIFIET.ConsultarEtiquetasDelContenido(oContenido.IDENTIFICADORCONTENIDO);
                var etiquetas = FachadaSIFIET.ConsultarEtiquetas("");
                var datosAtributos = CrearAtributos(collection, atributos);
                ViewBag.ListaAtributos = datosAtributos;
                ViewBag.ListaEtiquetas = new MultiSelectList(etiquetas, "IDENTIFICADORETIQUETA", "NOMBREETIQUETA", etiquetasContenidoSeleccionadas);
                                
                var etiquetasContenido = CrearEtiquetas(collection, etiquetas);

                if (!ModelState.IsValid) return View(oContenido);
                var resultado = FachadaSIFIET.ModificarContenido(oContenido, datosAtributos, etiquetasContenido);

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
        [Authorize(Roles = "Administrador")]
        public ActionResult EliminarContenido(decimal idContenido)
        {
            try
            {
                var resultado = FachadaSIFIET.EliminarContenido(idContenido);
                if (resultado)
                    TempData["ResultadoOperacion"] = "Contenido Eliminado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Eliminar el Contenido";
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        private static List<ATRIBUTO> CrearAtributos(FormCollection datos, List<ATRIBUTO> atributosActuales)
        {
            var atributos = new List<ATRIBUTO>();
            var nombresAtributos = new List<string>();
            var datosAtributo = new List<string>();
           nombresAtributos.AddRange(atributosActuales.Select(atributo => atributo.NOMBREATRIBUTO.Trim()));
           
            foreach (var item in nombresAtributos)
            {
                if (!String.IsNullOrEmpty(datos[item]))
                {
                    var dato = datos[item];
                    datosAtributo.Add(dato);
                }
                else
                    datosAtributo.Add("");
            }
            int cont = 0;
            foreach (var item in atributosActuales)
            {
                var auxAtributo = new ATRIBUTO();
                auxAtributo = item;
                auxAtributo.dato = datosAtributo.ElementAt(cont);
                atributos.Add(auxAtributo);
                cont++;
            }
            return atributos;
        }

        private static List<ETIQUETA> CrearEtiquetas(FormCollection datos, List<ETIQUETA> etiquetasActuales)
        {
            var etiquetas = new List<ETIQUETA>();
            var idEtiquetas = new List<decimal>();
            
            if (!String.IsNullOrEmpty(datos["ListaEtiquetas"]))
            {
                var dato = datos["ListaEtiquetas"];
                var IDsEtiquetas = dato.Split(',');
                foreach (var id in IDsEtiquetas)
                {
                    idEtiquetas.Add(decimal.Parse(id));
                }
            }            
           
           foreach (var item in etiquetasActuales)
           {
               foreach (var id in idEtiquetas)
               {
                   if(id == item.IDENTIFICADORETIQUETA)
                   {
                       var auxEtiquetas = new ETIQUETA();
                       auxEtiquetas = item;
                       etiquetas.Add(auxEtiquetas);
                   }                   
               }    
           }
            return etiquetas;
        }
    }
}