using SIFIET.Aplicacion;
using SIFIET.GestionContenidos.Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionContenidos.Datos.Modelo;
using System.Diagnostics;

namespace SIFIET.Presentacion.Controllers
{
    public class BloquesController : Controller
    {
        //
        // GET: /Bloques/
        public ActionResult Index(string idBloque)
        {
            List<BLOQUE> bloques = FachadaSIFIET.ConsultarBloques();
            List<CATEGORIA> categorias = FachadaSIFIET.ConsultarCategorias("");
            ViewBag.bloque1 = new SelectList(categorias, "IDENTIFICADORCATEGORIA", "NOMBRECATEGORIA");
            ViewBag.bloque2 = new SelectList(categorias, "IDENTIFICADORCATEGORIA", "NOMBRECATEGORIA");
            ViewBag.bloque3 = new SelectList(categorias, "IDENTIFICADORCATEGORIA", "NOMBRECATEGORIA");
            ViewBag.bloque4 = new SelectList(categorias, "IDENTIFICADORCATEGORIA", "NOMBRECATEGORIA");
            return View(bloques);
        }


        //
        // POST: /Programa/Create

        [HttpPost]
        public ActionResult RegistrarBloques(string bloque1, string bloque2, string bloque3, string bloque4)
        {
            FachadaSIFIET.AsignarCategoriasBloques(bloque1, bloque2, bloque3, bloque4);
            return RedirectToAction("Index");
        }
	}
}