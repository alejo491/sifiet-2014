using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIFIET.Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        //
        // GET: /Usuarios/
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
        // GET: /Usuarios/Details/5
        public ActionResult VisualizarUsuario(int id)
        {
            return View();
        }

        //
        // GET: /Usuarios/Create
        public ActionResult RegistrarUsuario()
        {
            return View();
        }

        //
        // POST: /Usuarios/Create
        [HttpPost]
        public ActionResult RegistrarUsuario(FormCollection collection)
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
        // GET: /Usuarios/Edit/5
        public ActionResult ModificarUsuario(int id)
        {
            return View();
        }

        //
        // POST: /Usuarios/Edit/5
        [HttpPost]
        public ActionResult ModificarUsuario(int id, FormCollection collection)
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
        // GET: /Usuarios/Delete/5
        public ActionResult EliminarUsuario(int id)
        {
            return View();
        }

        //
        // POST: /Usuarios/Delete/5
        [HttpPost]
        public ActionResult EliminarUsuario(int id, FormCollection collection)
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
    }
}
