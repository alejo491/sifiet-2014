﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionUsuarios.Datos.Modelo;
﻿using SIFIET.GestionUsuarios.Dominio.Entidades.Seguridad;
using System.Web.Security;

namespace SIFIET.Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        //
        // GET: /Usuarios/
        [Authorize]
        public ActionResult Index()
        {
            List<USUARIO> lista = FachadaSIFIET.ConsultarUsuarios();
            if (lista.Count == 0)
            {
                ViewBag.Mensaje = "No hay registros disponibles";
            }
            return View(lista);
        }

        [HttpPost]
        public ActionResult Index(FormCollection datos)
        {
            
            if (datos["criterio"].Equals("nombre"))
            {
                if (datos["valorbusqueda"].Equals(""))
                {
                    ViewBag.Mensaje = "valor vacio";
                }
                else
                {
                    List<USUARIO> lista = FachadaSIFIET.ConsultarUsuarioPorNombre(datos["valorbusqueda"],
                        datos["ESTADOUSUARIO"]);

                    if (lista.Count == 0)
                    {
                        ViewBag.Mensaje =
                            "No se han encontrado registros con el dato indicado, por favor intentelo de nuevo";
                    }
                    return View(lista);
                }
            }
            if (datos["criterio"].Equals("apellido"))
            {
                if (datos["valorbusqueda"].Equals(""))
                {
                    ViewBag.Mensaje =
                        "valor vacio";
                }
                else
                {
                    List<USUARIO> lista = FachadaSIFIET.ConsultarUsuarioPorApellido(datos["valorbusqueda"],
                        datos["ESTADOUSUARIO"]);
                    if (lista.Count == 0)
                    {
                        ViewBag.Mensaje =
                            "No se han encontrado registros con el dato indicado, por favor intentelo de nuevo";
                    }
                    return View(lista);
                }
            }
            if (datos["criterio"].Equals("identificacion"))
            {
                int x;
                if (int.TryParse(datos["valorbusqueda"], out x))
                {
                    List<USUARIO> lista = FachadaSIFIET.ConsultarUsuarioPorIdentificacion(datos["valorbusqueda"],
                        datos["ESTADOUSUARIO"]);
                    if (lista.Count == 0)
                    {
                        ViewBag.Mensaje =
                            "No se han encontrado registros con el dato indicado, por favor intentelo de nuevo";
                    }
                    return View(lista);
                }
                else
                {
                    ViewBag.Mensaje =
                        "valor numerico";
                }
            }

            
            return View(FachadaSIFIET.ConsultarUsuarios());


            




        }

        //
        // GET: /Usuarios/Details/5
        public ActionResult VisualizarUsuario(int idUsuario)
        
        {
            
            return View(FachadaSIFIET.ConsultarUsuario(idUsuario));
        }

        //
        // GET: /Usuarios/Create
        public ActionResult RegistrarUsuario()
        {
            ViewBag.roles = FachadaSIFIET.ConsultarRoles();
            return View();
        }

        //
        // POST: /Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarUsuario([Bind(
                Include =
                    "EMAILINSTITUCIONALUSUARIO,PASSWORDUSUARIO,IDENTIFICACIONUSUARIO,NOMBRESUSUARIO,APELLIDOSUSUARIO"
                )] USUARIO usuario, FormCollection datos)
        {
            bool ban = true;
            ViewBag.roles = FachadaSIFIET.ConsultarRoles();
            if (datos["roles"]==null)
            {

                ViewBag.ErrorRol = "No se han seleccionado Roles";
                ban = false;
            }
            if (!ModelState.IsValid || !ban) return View(usuario);
           try
           {
               usuario.EMAILINSTITUCIONALUSUARIO = usuario.EMAILINSTITUCIONALUSUARIO + "@unicauca.edu.co";
                var roles = datos["roles"].Split(',');
                usuario.ESTADOUSUARIO = "Activo";
                FachadaSIFIET.RegistrarUsuario(usuario, roles);
                TempData["Mensaje"] = "Usuaro Creado con Éxito";
                return RedirectToAction("Index");
            }
           catch (Exception e)
           {

               ViewBag.Mensaje = "Error" + e.Message;
               return View(usuario);
            }

            return View(usuario);

        }

        //
        // GET: /Usuarios/Edit/5
        public ActionResult ModificarUsuario(int idUsuario)
        {
            USUARIO oUsuario = FachadaSIFIET.ConsultarUsuario(idUsuario);
            oUsuario.EMAILINSTITUCIONALUSUARIO = oUsuario.EMAILINSTITUCIONALUSUARIO.Split('@').ElementAt(0);
            ViewBag.roles = FachadaSIFIET.ConsultarRoles();
            ViewBag.rolesasignados = oUsuario.ROLs.ToList();
            return View(oUsuario);
        }

        //
        // POST: /Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarUsuario(FormCollection datos)
         {
            ViewBag.roles = FachadaSIFIET.ConsultarRoles();
            
            List<ROL> rolesActuales =new List<ROL>();
            foreach (var rol in datos["roles"].Split(','))
            {
                rolesActuales.Add(FachadaSIFIET.ConsultarRol(rol));
            }


            ViewBag.rolesasignados = rolesActuales;

            bool existe = FachadaSIFIET.IdentificacionUsuarioExiste(datos["identificacion"].Trim());
            var usuario = new USUARIO()
            {
                IDENTIFICADORUSUARIO = decimal.Parse(datos["IDENTIFICADORUSUARIO"]),
                EMAILINSTITUCIONALUSUARIO = datos["EMAILINSTITUCIONALUSUARIO"].Trim()+"@unicauca.edu.co",
                IDENTIFICACIONUSUARIO = datos["identificacion"].Trim(),
                NOMBRESUSUARIO = datos["NOMBRESUSUARIO"].Trim(),
                APELLIDOSUSUARIO = datos["APELLIDOSUSUARIO"].Trim(),
                PASSWORDUSUARIO = datos["PASSWORDUSUARIO"].Trim(),
                ESTADOUSUARIO = datos["ESTADOUSUARIO"].Trim()
                
                
            };


            if (!ModelState.IsValid || (!datos["identificacion"].Trim().Equals(datos["IDENTIFICACIONUSUARIO"].Trim()) && existe))
            {

                ViewBag.rolesasignados = rolesActuales;
                if (existe)
                {
                    ViewBag.ErrorIdUsuario = "Ya Existe Un Usuario Con Esta Identificacion ";
                }
                return View(usuario);
            }

            
            
            try
            {


                var roles = datos["roles"].Split(',');
                FachadaSIFIET.ModificarUsuario(usuario, roles);
                TempData["Mensaje"] = "Usuario Editado con Éxito";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                ViewBag.Mensaje = "Error" + e.Message;
                return View();
            }


        }

        //
        // GET: /Usuarios/Delete/5
        public ActionResult EliminarUsuario(int idUsuario)
        {
            try
            {
                FachadaSIFIET.EliminarUsuario(idUsuario);
                TempData["Mensaje"] = "Usuaro Eliminado con Éxito";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult LogOn()
        {
            return View();

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();            
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (new ProveedorMembrecias().ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "El usuario o password es incorrecto");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        
    }
}