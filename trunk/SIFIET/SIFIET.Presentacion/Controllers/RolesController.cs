using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.GestionUsuarios.Datos.Modelo;

namespace SIFIET.Presentacion.Controllers
{
    public class RolesController : Controller
    {
        //
        // GET: /Roles/
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
        // GET: /Roles/Details/5
        public ActionResult VisualizarRol(int id)
        {
            return View();
        }

        //
        // GET: /Roles/Create
        public ActionResult RegistrarRol()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult RegistrarRol(FormCollection collection)
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
        // GET: /Roles/Edit/5
        public ActionResult ModificarRol(int id)
        {
            return View();
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        public ActionResult ModificarRol(int id, FormCollection collection)
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
        // GET: /Roles/Delete/5
        public ActionResult EliminarRol(int id)
        {
            return View();
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost]
        public ActionResult EliminarRol(int id, FormCollection collection)
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
        private static List<PERMISO> CrearPermisos(FormCollection datos)
        {
            var permisos = new List<PERMISO>();
            var permiso = new PERMISO
            {
                NOMBREPERMISO = "Plan de Estudios",
                GESTIONARPERMISO = datos["Plan de Estudios"].Trim().Equals("Gestionar") ? 1 : 0
            };
            permisos.Add(permiso);
            permiso = new PERMISO
            {
                NOMBREPERMISO = "Usuarios",
                GESTIONARPERMISO = datos["Usuarios"].Trim().Equals("Gestionar") ? 1 : 0
            };
            permisos.Add(permiso);
            permiso = new PERMISO
            {
                NOMBREPERMISO = "Programas",
                GESTIONARPERMISO = datos["Programas"].Trim().Equals("Gestionar") ? 1 : 0
            };
            permisos.Add(permiso);
            permiso = new PERMISO
            {
                NOMBREPERMISO = "Infraestructura",
                GESTIONARPERMISO = datos["Infraestructura"].Trim().Equals("Gestionar") ? 1 : 0
            };
            permisos.Add(permiso);
            permiso = new PERMISO
            {
                NOMBREPERMISO = "Asignaturas",
                GESTIONARPERMISO = datos["Asignaturas"].Trim().Equals("Gestionar") ? 1 : 0
            };
            permisos.Add(permiso);
            permiso = new PERMISO
            {
                NOMBREPERMISO = "Salones",
                GESTIONARPERMISO = datos["Salones"].Trim().Equals("Gestionar") ? 1 : 0
            };
            permisos.Add(permiso);
            return permisos;
        }
    }
}
