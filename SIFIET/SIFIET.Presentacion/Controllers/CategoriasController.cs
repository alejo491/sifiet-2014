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
        [Authorize(Roles = "Contenido")]
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
        [Authorize]
        public ActionResult RegistrarCategoria()
        {
            return View();
        }

        //
        // POST: /Contenidos/Create
        [HttpPost]
        [Authorize(Roles = "Contenido")]
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
        [Authorize]
        public ActionResult RegistrarAtributo(CATEGORIA categoriaIn)
        {
            categoriaIn = FachadaSIFIET.ConsultarCategoria(categoriaIn.IDENTIFICADORCATEGORIA);
            ViewBag.categoria = categoriaIn;
            ViewBag.listaAtributos = categoriaIn.ATRIBUTOes;
            ViewBag.tipoAtributo = new SelectList(new List<SelectListItem>
            {
                new SelectListItem(){Value="string",Text = "Cadena de Caracteres"},
                new SelectListItem(){Value="date",Text = "Fecha"},
                new SelectListItem(){Value="image",Text = "Imagen"},
            },"value","text");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Contenido")]
        public ActionResult RegistrarAtributo(ATRIBUTO inAtributo)
        {
            var categoriaIn = FachadaSIFIET.ConsultarCategoria(inAtributo.IDENTIFICADORCATEGORIA);
            
            if (ModelState.IsValid)
            {
                ATRIBUTO oAtributo = new ATRIBUTO()
                {
                    IDENTIFICADORCATEGORIA = inAtributo.IDENTIFICADORCATEGORIA,
                    NOMBREATRIBUTO = inAtributo.NOMBREATRIBUTO,
                    OBLIGATORIOATRIBUTO = inAtributo.OBLIGATORIOATRIBUTO,
                    PANELEDICIONATRIBUTO = inAtributo.PANELEDICIONATRIBUTO,
                    TAMANIOATRIBUTO = inAtributo.TAMANIOATRIBUTO,
                    TIPOATRIBUTO = inAtributo.TIPOATRIBUTO,
                };
                if (!FachadaSIFIET.exiteAtributoCategoria(oAtributo))
                {
                    var idAtributo = FachadaSIFIET.RegistrarAtributo(oAtributo);
                    if (idAtributo > 0)
                    {
                        TempData["Mensaje"] = "Atributo creado con Éxito.";
                    }
                    else
                    {
                        @TempData["Mensaje"] = "Ocurrio un error, No se pudo registrar el Atributo.";
                    }
                }
                else
                {
                    @TempData["Mensaje"] = "Ya existe un Atributo con el nombre "+oAtributo.NOMBREATRIBUTO+" en Esta categoria";
                   
                }
            }
            else
            {
                @TempData["Mensaje"] = "Ocurrio un error, No se pudo registrar el Atributo.";
            }
            ViewBag.categoria = categoriaIn;
            ViewBag.listaAtributos = categoriaIn.ATRIBUTOes;
            ViewBag.tipoAtributo = new SelectList(new List<SelectListItem>
            {
                new SelectListItem(){Value="string",Text = "Cadena de Caracteres"},
                new SelectListItem(){Value="date",Text = "Fecha"},
                new SelectListItem(){Value="image",Text = "Imagen"},
            }, "value", "text");
            return View();
        }
        [Authorize(Roles = "Administrador")]
        public ActionResult EliminarAtributo(int idCategoria, int idAtributo)
        {
            ViewData["idCategoria"] = idCategoria;
            if (FachadaSIFIET.EliminarAtributoContenido(idCategoria, idAtributo))
            {
                TempData["Mensaje"] = "Atributo Eliminado con Éxito";
            }
            else
            {
                TempData["Mensaje"] = "Ha ocurrido un error al Eliminar el Atributo";
            }
            var categoriaIn = FachadaSIFIET.ConsultarCategoria(idCategoria);
            return RedirectToAction("RegistrarAtributo",categoriaIn);
        }
        [Authorize(Roles = "Contenido")]
        public ActionResult EliminarCategoria(int idCategoria)
        {
            var resultado = FachadaSIFIET.EliminarCategoria(idCategoria);
            if (resultado)
                TempData["ResultadoOperacion"] = "Categoria Eliminada con Exito";
            else
                TempData["ResultadoOperacion"] = "Fallo al Eliminar la Categoria";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Contenido")]
        public ActionResult ModificarCategoria(int idCategoria)
        {
            var oCategoria = FachadaSIFIET.ConsultarCategoria(idCategoria);

            return View(oCategoria);
        }

        [HttpPost]
        [Authorize(Roles = "Contenido")]
        public ActionResult ModificarCategoria(FormCollection datos)
        {

            bool existe = FachadaSIFIET.NombreCategoriaExiste(datos["nombre"].Trim());

            CATEGORIA categoriaIn = new CATEGORIA()
            {
                IDENTIFICADORCATEGORIA = Decimal.Parse(datos["IDENTIFICADORCATEGORIA"].Trim()),
                DESCRIPCIONCATEGORIA = datos["DESCRIPCIONCATEGORIA"].Trim(),
                NOMBRECATEGORIA = datos["nombre"].Trim(),
                ESTADOCATEGORIA = datos["ESTADOCATEGORIA"].Trim(),
                VISIBLEPRINCIPALCATEGORIA = decimal.Parse(datos["VISIBLEPRINCIPALCATEGORIA"].Trim())

            };
            if (!ModelState.IsValid || (!datos["nombre"].Trim().Equals(datos["NOMBRECATEGORIA"].Trim()) && existe))
            {
                ViewBag.ErrorNombre = "Ingrese otro nombre, ya existe una Etiqueta usando ese nombre";

                ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo registrar esta Categoria. (Model)";
            }
            else
            {
                var idCategoria = FachadaSIFIET.ModificarCategoria(categoriaIn);
                if (idCategoria)
                {
                    TempData["ResultadoOperacion"] = "Categoría Modificada con Éxito.";
                    categoriaIn = FachadaSIFIET.ConsultarCategoria(categoriaIn.IDENTIFICADORCATEGORIA);
                    ViewBag.categoria = categoriaIn;
                    ViewBag.listaAtributos = categoriaIn.ATRIBUTOes;
                    ViewBag.tipoAtributo = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem(){Value="string",Text = "Cadena de Caracteres"},
                        new SelectListItem(){Value="date",Text = "Fecha"},
                        new SelectListItem(){Value="image",Text = "Imagen"},
                    }, "value", "text");
                    return RedirectToAction("RegistrarAtributo", categoriaIn);
                }
                else
                {
                    ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo registrar esta Categoria.";
                }
                
            }

            return View(categoriaIn);    
        }
        [Authorize(Roles = "Contenido")]
        public ActionResult VisualizarCategoria(int idCategoria)
        {
            CATEGORIA categoriaIn = FachadaSIFIET.ConsultarCategoria(idCategoria);
            ViewBag.categoria = categoriaIn;
            ViewBag.listaAtributos = categoriaIn.ATRIBUTOes;
            return View();
        }

    }
}