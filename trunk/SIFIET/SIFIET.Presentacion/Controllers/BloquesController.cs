using System;
using System.Collections.Generic;
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
            List<BLOQUE> bloques = FachadaSIFIET.ConsultarBloques(idBloque);
           
            return View(bloques);
        }
	}
}