using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIFIET.Presentacion.Controllers
{
    public class ProgramasController : Controller
    {
        //
        // GET: /Programas/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection datos)
        {
            return View();
        }

        //
        // GET: /Programas/Details/5 visualizar registrar modificar eliminar consultar
        public ActionResult VisualizarPrograma(int id)
        {
            return View();
        }

        //
        // GET: /Programas/Create
        public ActionResult RegistrarPrograma()
        {
            return View();
        }

        //
        // POST: /Programas/Create
        [HttpPost]
        public ActionResult RegistrarPrograma(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Programas/Edit/5
        public ActionResult ModificarPrograma(int id)
        {
            return View();
        }

        //
        // POST: /Programas/Edit/5
        [HttpPost]
        public ActionResult ModificarPrograma(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Programas/Delete/5
        public ActionResult EliminarPrograma(int id)
        {
            return View();
        }

        //
        // POST: /Programas/Delete/5
        [HttpPost]
        public ActionResult EliminarPrograma(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CargarDatos()
        {
            return null;
        }

        private DataSet LeerRegistrosExcel(string rutaArchivo, string extencionArchivo)
        {
            return null;
        }

    }
}
