﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionUsuarios.Datos.Modelo;

namespace SIFIET.Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        //
        // GET: /Usuarios/
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
                List<USUARIO> lista = FachadaSIFIET.ConsultarUsuarioPorNombre((datos["valorbusqueda"]));
                if (lista.Count == 0)
                {
                    ViewBag.Mensaje = "No se han encontrado registros con el dato indicado, por favor intentelo de nuevo";
                }
                return View(lista);
            }
            if (datos["criterio"].Equals("apellido"))
            {
                
                
                    List<USUARIO> lista = FachadaSIFIET.ConsultarUsuarioPorApellido(datos["valorbusqueda"]);
                    if (lista.Count == 0)
                    {
                        ViewBag.Mensaje =
                            "No se han encontrado registros con el dato indicado, por favor intentelo de nuevo";
                    }
                    return View(lista);
           
            }
            if (datos["criterio"].Equals("identificacion"))
            {
                int x;
                if (int.TryParse(datos["valorbusqueda"], out x))
                        {
                        List<USUARIO> lista = FachadaSIFIET.ConsultarUsuarioPorIdentificacion(datos["valorbusqueda"]);
                        if (lista.Count == 0)
                        {
                            ViewBag.Mensaje = "No se han encontrado registros con el dato indicado, por favor intentelo de nuevo";
                        }
                        return View(lista);
                    }
            }

            ViewBag.Mensaje =
                        "valor numerico";
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
            ViewBag.roles = FachadaSIFIET.ConsultarRoles();
            ViewBag.rolesasignados = oUsuario.ROLs.ToList();
            return View(oUsuario);
        }

        //
        // POST: /Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarUsuario(
            [Bind(
                Include =
                    "IDENTIFICADORUSUARIO,EMAILINSTITUCIONALUSUARIO,PASSWORDUSUARIO,IDENTIFICACIONUSUARIO,NOMBRESUSUARIO,APELLIDOSUSUARIO,ESTADOUSUARIO"
                )] USUARIO usuario, FormCollection datos)
        {
            ViewBag.roles = FachadaSIFIET.ConsultarRoles();

            ViewBag.rolesasignados = FachadaSIFIET.ConsultarUsuario(int.Parse(usuario.IDENTIFICADORUSUARIO.ToString())).ROLs.ToList();
            bool ban = true;
            if (datos["roles"] == null)
            {
                ViewBag.ErrorRol = "Este campo es obligatorio";
                ban = false;

            }

            if (!ModelState.IsValid || !ban) return View(usuario);
            try
            {


                var roles = datos["roles"].Split(',');
                FachadaSIFIET.ModificarUsuario(usuario, roles);
                TempData["Mensaje"] = "Usuaro Editado con Éxito";
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

        
    }
}