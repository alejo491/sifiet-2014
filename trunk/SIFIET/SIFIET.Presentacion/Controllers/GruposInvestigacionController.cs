using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SIFIET.GestionProgramas.Datos.Modelo;
using SIFIET.Aplicacion;

namespace SIFIET.Presentacion.Controllers
{
    public class GruposInvestigacionController : Controller
    {
        //
        // GET: //GruposInvestigacion
        public ActionResult Index()
        {
            List<GRUPO_INVESTIGACION> lista = FachadaSIFIET.ConsultarGruposInvestigacion();
            if (lista.Count == 0)
            {
                ViewBag.Mensaje = "No hay registros disponibles";
            }
            return View(lista);
        }
        
        [HttpPost]
        public ActionResult Index(FormCollection datos)
        {
            
                List<GRUPO_INVESTIGACION> lista = FachadaSIFIET.ConsultarGruposInvestigacionPorNombre((datos["valorbusqueda"]));
                if (lista.Count == 0)
                {
                    ViewBag.Mensaje = "No se han encontrado registros con el dato indicado, por favor intentelo de nuevo";
                }
                return View(lista);
            



            




        }
        
        //
        // GET: /Usuarios/Details/5
        public ActionResult VisualizarGrupoInvestigacion(int idGinvestigacion)
        {
            return View(FachadaSIFIET.ConsultarGrupoInvestigacion(idGinvestigacion));
        }
        
        //
        // GET: /Usuarios/Create
        public ActionResult RegistrarGrupoInvestigacion()
        {
            var listaDepartamentos = FachadaSIFIET.ConsultarDepartamentos();
            ViewBag.ListaDepartamentos = new SelectList(listaDepartamentos, "IDENTIFICADORDEPARTAMENTO", "NOMBREDEPARTAMENTO");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NombreCompletoDocente");
            return View();
        }
        
        //
        // POST: /Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarGrupoInvestigacion([Bind(
                Include =
                    "IDENTIFICADORUSUARIO,IDENTIFICADORDEPARTAMENTO,NOMBREGRUPOINVESTIGACION,DESCRIPCIONGRUPOINVESTIGACION,ESTADOGRUPOINVESTIGACION,CODIGOGRUPOINVESTIGACION"
                )] GRUPO_INVESTIGACION grupo, FormCollection datos)
        {

            var listaDepartamentos = FachadaSIFIET.ConsultarDepartamentos();
            ViewBag.ListaDepartamentos = new SelectList(listaDepartamentos, "IDENTIFICADORDEPARTAMENTO", "NOMBREDEPARTAMENTO");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NombreCompletoDocente");
            if (!ModelState.IsValid) return View(grupo);
            try
            {
                
                FachadaSIFIET.RegistrarGrupoInvestigacion(grupo);
                ViewBag.Mensaje = "Registro Exitoso";
            }
            catch (Exception e)
            {

                ViewBag.Mensaje = "Error" + e.Message;
                return View(grupo);
            }

            return View();

        }
        /*
        //
        // GET: /Usuarios/Edit/5
        public ActionResult ModificarUsuario(int idUsuario)
        {
            ViewBag.roles = FachadaSIFIET.ConsultarRoles();

            return View(FachadaSIFIET.ConsultarUsuario(idUsuario));
        }

        //
        // POST: /Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarUsuario(
            [Bind(
                Include =
                    "IDENTIFICADORUSUARIO,EMAILINSTITUCIONALUSUARIO,PASSWORDUSUARIO,IDENTIFICACIONUSUARIO,NOMBRESUSUARIO,APELLIDOSUSUARIO,ESTADO"
                )] USUARIO usuario, FormCollection datos)
        {
            ViewBag.roles = FachadaSIFIET.ConsultarRoles();

            if (datos["roles"] == null)
            {
                ViewBag.ErrorRol = "Este campo es obligatorio";
            }
            if (!ModelState.IsValid) return View(usuario);
            try
            {
                var roles = datos["roles"].Split(',');
                FachadaSIFIET.ModificarUsuario(usuario, roles);
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
            return View(FachadaSIFIET.ConsultarUsuario(idUsuario));
        }

        //
        // POST: /Usuarios/Delete/5
        [HttpPost]
        public ActionResult EliminarUsuario(int id, FormCollection collection)
        {
            try
            {
                FachadaSIFIET.EliminarUsuario(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
	}
}