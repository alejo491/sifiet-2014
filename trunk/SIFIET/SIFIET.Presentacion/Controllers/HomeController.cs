using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.GestionContenidos.Datos.Modelo;
using SIFIET.Aplicacion;

namespace SIFIET.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string idBloque)
        {
            ViewBag.Message = "BIENVENIDO A SIFIET";
            List<BLOQUE> bloques = FachadaSIFIET.ConsultarBloques();
            return View(bloques);
        }

        public ActionResult DetallarCategoria(int idCategoriaIn)
        {            
            CATEGORIA categoriaIn = FachadaSIFIET.ConsultarCategoria(idCategoriaIn);
            ViewBag.nombreCategoria = categoriaIn.NOMBRECATEGORIA;
            List<CONTENIDO> lstContenidos = categoriaIn.CONTENIDOes.ToList();
            return View(lstContenidos);
        }
        public ActionResult VisualizarContenido(int idContenidoIn)
        {
            CONTENIDO contenido = FachadaSIFIET.VisualizarContenido(idContenidoIn);
            return View(contenido);
        }
    }
}
