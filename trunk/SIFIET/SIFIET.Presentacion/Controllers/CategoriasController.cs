﻿using System;
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
        public ActionResult RegistrarAtributo(FormCollection collection)
        {
            var categoriaIn = FachadaSIFIET.ConsultarCategoria(decimal.Parse(collection["IDENTIFICADORCATEGORIA"]));
            
            if (ModelState.IsValid)
            {
                ATRIBUTO oAtributo = new ATRIBUTO()
                {
                    IDENTIFICADORCATEGORIA = decimal.Parse(collection["IDENTIFICADORCATEGORIA"].Trim()),
                    NOMBREATRIBUTO = collection["NOMBREATRIBUTO"],
                    OBLIGATORIOATRIBUTO = decimal.Parse(collection["OBLIGATORIOATRIBUTO"].Trim()),
                    PANELEDICIONATRIBUTO = decimal.Parse(collection["PANELEDICIONATRIBUTO"].Trim()),
                    TAMANIOATRIBUTO = decimal.Parse(collection["TAMANIOATRIBUTO"]),
                    TIPOATRIBUTO = collection["TIPOATRIBUTO"],
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

    }
}