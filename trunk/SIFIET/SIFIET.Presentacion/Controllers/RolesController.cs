using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionUsuarios.Datos.Modelo;

namespace SIFIET.Presentacion.Controllers
{
    public class RolesController : Controller
    {
        //
        // GET: /Roles/
        [Authorize]
        public ActionResult Index(string criterio, string valorbusqueda, string estado="Activo")
        {
            var oRoles = new List<ROL>();
            if (valorbusqueda == null ||valorbusqueda.Trim().Equals(""))
            {
                oRoles = FachadaSIFIET.ConsultarRolPorEstado(estado.Trim());
            }
            else
            {
                oRoles = FachadaSIFIET.ConsultarRolPorNombre(valorbusqueda.Trim(), estado.Trim());
            }
            if (!oRoles.Any())
            {
                ViewBag.Mensaje = "Ningun Rol Encontrado";
                //oRoles = FachadaSIFIET.ConsultarRoles();
                oRoles=new List<ROL>();
            }
            return View(oRoles);
        }

        /*[HttpPost]
        public ActionResult Index(FormCollection datos)
        {
            var oRoles = new List<ROL>();
            if (datos["criterio"].Equals("nombre"))
            {
                oRoles = FachadaSIFIET.ConsultarRolPorNombre((datos["valorbusqueda"].Trim()),datos["estado"].Trim());
            }
            else
            {
                oRoles = FachadaSIFIET.ConsultarRolPorEstado(datos["estado"].Trim());
            }
            if (!oRoles.Any())
            {
                ViewBag.Mensaje = "Ningun Rol Encontrado";
                oRoles = FachadaSIFIET.ConsultarRoles();
            }
            return View(oRoles);
        }*/

        //
        // GET: /Roles/Details/5
        public ActionResult VisualizarRol(int idRol)
        {
            return View(FachadaSIFIET.ConsultarRol(idRol.ToString()));
        }

        //
        // GET: /Roles/Create
        public ActionResult RegistrarRol()
        {
            ViewBag.lstNombresPermisos = FachadaSIFIET.ConsultarNombresPermisos();
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarRol(FormCollection collection)
        {
                var permisos = CrearPermisos(collection);
                var oRol = new ROL()
                {
                    DESCRIPCIONROL = collection["DESCRIPCIONROL"].Trim(),
                    NOMBREROL = collection["NOMBREROL"].Trim(),
                    ESTADOROL = "Activo"         
                };
                var existeRol = FachadaSIFIET.ExisteNombreRol(collection["NOMBREROL"].Trim());
            if (!ModelState.IsValid || existeRol)
            {
                ViewBag.lstNombresPermisos = FachadaSIFIET.ConsultarNombresPermisos();
                if (existeRol)
                {
                    ViewBag.ErrorNombreRol = "Ya Existe Un Rol Con este Nombre";
                }
                return View(oRol);
            }
            if (FachadaSIFIET.RegistrarRoles(oRol, permisos))
            {
                TempData["Mensaje"] = "Rol Creado con Exito";
                return RedirectToAction("Index");
            }
            ViewBag.Mensaje = "Error: No se ha podido registrar el Rol";
            return View(oRol);
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult ModificarRol(int idRol)
        {
            var oRol = FachadaSIFIET.ConsultarRol(idRol.ToString().Trim());
            TempData["PermisosActuales"] = oRol.PERMISOS;
            ViewBag.lstNombresPermisos = FachadaSIFIET.ConsultarNombresPermisos();
            return View(oRol);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarRol(FormCollection collection)
        {
            //var permisos = CrearPermisos(collection,(IEnumerable<PERMISO>) TempData["PermisosActuales"]);
            var permisos = CrearPermisos(collection);
            bool existeRol = FachadaSIFIET.ExisteNombreRol(collection["NOMBREROL"].Trim());
            if (!ModelState.IsValid ||( !collection["NOMBREROL"].Trim().Equals(collection["NombreActual"].Trim()) && existeRol))
            {
                var oRolActual = FachadaSIFIET.ConsultarRol(collection["IDENTIFICADORROL"].Trim());
                ViewBag.lstNombresPermisos = FachadaSIFIET.ConsultarNombresPermisos();
                TempData["PermisosActuales"] = oRolActual.PERMISOS;
                if (existeRol)
                {
                    ViewBag.ErrorNombreRol = "Ya Existe Un Rol Con El Nombre ' " + collection["NOMBREROL"].Trim() + " '";
                }
                else
                {
                    @ViewBag.ErrorNombreRol = "";
                }
                return View(oRolActual);
            }
            var oRol = new ROL()
            {
                IDENTIFICADORROL = decimal.Parse(collection["IDENTIFICADORROL"]),
                DESCRIPCIONROL = collection["DESCRIPCIONROL"].Trim(),
                NOMBREROL = collection["NOMBREROL"].Trim(),
                ESTADOROL = collection["ESTADOROL"]
            };
            if (FachadaSIFIET.ModificarRol(oRol, permisos))
            {
                TempData["Mensaje"] = "Rol Modificado con Exito";
                return RedirectToAction("Index");
            }
            ViewBag.Mensaje = "Error: No se ha podido registrar el Rol";
            return View(oRol);
        }

        //
        // GET: /Roles/Delete/5
        public ActionResult EliminarRol(int idRol)
        {
            return View(FachadaSIFIET.ConsultarRol(idRol.ToString()));
        }

        //
        // POST: /Roles/Delete/5
        public ActionResult ConfirmarEliminarRol(int idRol)
        {
            if (FachadaSIFIET.EliminarRol(idRol.ToString()))
            {
                TempData["Mensaje"] = "Rol Eliminado con Exito";
            }
            else {
                TempData["Mensaje"] = "Error al Eliminar el Rol";
            }
            return RedirectToAction("Index");

        }
        private static List<PERMISO> CrearPermisos(FormCollection datos, IEnumerable<PERMISO> permisosActuales=null)
        {
            var permisos = new List<PERMISO>();
            var nombresPermiso = new List<string>();
            if (permisosActuales == null)
            {
                nombresPermiso = FachadaSIFIET.ConsultarNombresPermisos();
            }
            else
            {
                nombresPermiso.AddRange(permisosActuales.Select(permiso => permiso.NOMBREPERMISO.Trim()));
            }
            foreach (var item in nombresPermiso)
            {
                if (int.Parse(datos[item.Trim()]) == 1)
                {
                    var permiso = new PERMISO()
                    {
                        ESTADOPERMISO = "Activo",
                        NOMBREPERMISO = item.Trim(),
                        GESTIONARPERMISO = int.Parse(datos[item.Trim()]),
                    };
                    permisos.Add(permiso);
                }
            }
            return permisos;
        }
    }
}
