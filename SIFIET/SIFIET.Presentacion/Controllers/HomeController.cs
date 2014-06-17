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
            List<BLOQUE> bloques = FachadaSIFIET.ConsultarBloques(idBloque);
            return View(bloques);
        }
    }
}
