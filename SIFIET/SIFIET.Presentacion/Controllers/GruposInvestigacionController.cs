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
            var identificacion = new SelectListItem() { Value = "1", Text = "Codigo" };
            var nombresalon = new SelectListItem() { Value = "2", Text = "Nombre" };
            var listaItems = new List<SelectListItem> { identificacion, nombresalon };
            ViewBag.filtroBusqueda = new SelectList(listaItems, "value", "text");

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
            var identificacion = new SelectListItem() { Value = "1", Text = "Codigo" };
            var nombresalon = new SelectListItem() { Value = "2", Text = "Nombre" };
            var listaItems = new List<SelectListItem> { identificacion, nombresalon };
            ViewBag.filtroBusqueda = new SelectList(listaItems, "value", "text");

            List<GRUPO_INVESTIGACION> lista = new List<GRUPO_INVESTIGACION>();
            if (datos["filtroBusquedaGInvestigacion"].Equals("1"))
            {
                lista = FachadaSIFIET.ConsultarGruposInvestigacionPorCodigo(datos["valorbusqueda"], datos["estado"]);
            } 
            if (datos["filtroBusquedaGInvestigacion"].Equals("2"))
            {
                lista = FachadaSIFIET.ConsultarGruposInvestigacionPorNombre(datos["valorbusqueda"], datos["estado"]);
            }   
         
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
        public ActionResult RegistrarGrupoInvestigacion(GRUPO_INVESTIGACION grupo)
        {
            var listaDepartamentos = FachadaSIFIET.ConsultarDepartamentos();
            ViewBag.ListaDepartamentos = new SelectList(listaDepartamentos, "IDENTIFICADORDEPARTAMENTO", "NOMBREDEPARTAMENTO");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NombreCompletoDocente");
            if (!ModelState.IsValid) return View(grupo);
            try
            {                 
                FachadaSIFIET.RegistrarGrupoInvestigacion(grupo);
                TempData["Mensaje"] = "Grupo de Investigación creado con éxito";
                return RedirectToAction("Index");                
            }
            catch (Exception e)
            {

                ViewBag.Mensaje = "Error" + e.Message;
                return View(grupo);
            }            
        }
        
        //
        // GET: /Usuarios/Edit/5
        public ActionResult ModificarGrupoInvestigacion(int idGinvestigacion)
        {
            var listaDepartamentos = FachadaSIFIET.ConsultarDepartamentos();
            ViewBag.ListaDepartamentos = new SelectList(listaDepartamentos, "IDENTIFICADORDEPARTAMENTO", "NOMBREDEPARTAMENTO");            
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NombreCompletoDocente");

            GRUPO_INVESTIGACION gInvestigacion = FachadaSIFIET.ConsultarGrupoInvestigacion(idGinvestigacion);

            ViewBag.lstDepartamentos = listaDepartamentos;
            ViewBag.lstDocentes = listaDocentes;
            ViewBag.DepartamentoRegistrado = gInvestigacion.DEPARTAMENTO;
            ViewBag.DocenteRegistrado = gInvestigacion.DOCENTE;

            return View(gInvestigacion);
        }

        //
        // POST: /Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarGrupoInvestigacion(GRUPO_INVESTIGACION grupo)
        {
            var listaDepartamentos = FachadaSIFIET.ConsultarDepartamentos();
            ViewBag.ListaDepartamentos = new SelectList(listaDepartamentos, "IDENTIFICADORDEPARTAMENTO", "NOMBREDEPARTAMENTO");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NombreCompletoDocente");
                       
            if (!ModelState.IsValid) return View(grupo);            
            try
            {                                
                FachadaSIFIET.ModificarGrupoInvestigacion(grupo);
                TempData["Mensaje"] = "Grupo de investigación editado con exito";
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