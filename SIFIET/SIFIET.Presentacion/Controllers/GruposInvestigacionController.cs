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
                GRUPO_INVESTIGACION gInvestigacion = FachadaSIFIET.ConsultarGrupoInvestigacionPorCodigo(grupo.CODIGOGRUPOINVESTIGACION);
                if(gInvestigacion == null)
                {
                    FachadaSIFIET.RegistrarGrupoInvestigacion(grupo);
                    TempData["Mensaje"] = "Grupo de Investigación creado con éxito";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mensaje = "El grupo de investigación con el código "+grupo.CODIGOGRUPOINVESTIGACION+" ya se encuentra registrado";
                }
            }
            catch (Exception e)
            {

                ViewBag.Mensaje = "Error" + e.Message;
                return View(grupo);
            }

            return View(grupo);

        }
        
        //
        // GET: /Usuarios/Edit/5
        public ActionResult ModificarGrupoInvestigacion(int idGinvestigacion)
        {
            var listaDepartamentos = FachadaSIFIET.ConsultarDepartamentos();
            ViewBag.ListaDepartamentos = new SelectList(listaDepartamentos, "IDENTIFICADORDEPARTAMENTO", "NOMBREDEPARTAMENTO");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NombreCompletoDocente");

            return View(FachadaSIFIET.ConsultarGrupoInvestigacion(idGinvestigacion));
        }

        //
        // POST: /Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarGrupoInvestigacion([Bind(
                Include =
                    "IDENTIFICADORUSUARIO,IDENTIFICADORDEPARTAMENTO,NOMBREGRUPOINVESTIGACION,DESCRIPCIONGRUPOINVESTIGACION,ESTADOGRUPOINVESTIGACION,CODIGOGRUPOINVESTIGACION,IDENTIFICADORGRUPOINVES"
                )] GRUPO_INVESTIGACION grupo, FormCollection datos)
        {
            var listaDepartamentos = FachadaSIFIET.ConsultarDepartamentos();
            ViewBag.ListaDepartamentos = new SelectList(listaDepartamentos, "IDENTIFICADORDEPARTAMENTO", "NOMBREDEPARTAMENTO");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NombreCompletoDocente");
            if (!ModelState.IsValid) return View(grupo);
            try
            {                
                FachadaSIFIET.ModificarGrupoInvestigacion(grupo);
                ViewBag.Mensaje = "Modificacion Exitosa";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                ViewBag.Mensaje = "Error" + e.Message;
                return View(grupo);
            }            
        }

        
        
        //
        // GET: /Usuarios/Delete/5
        public ActionResult EliminarGrupoInvestigacion(int idGinvestigacion)
        {
            try
            {
                FachadaSIFIET.EliminarGrupoInvestigacion(idGinvestigacion);
                TempData["Mensaje"] = "Grupo de investigación eliminado con éxito";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
	}
}