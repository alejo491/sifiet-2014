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
                GRUPO_INVESTIGACION gInvestigacionCodigo = FachadaSIFIET.ConsultarGrupoInvestigacionPorCodigo(grupo.CODIGOGRUPOINVESTIGACION);
                GRUPO_INVESTIGACION gInvestigacionNombre = FachadaSIFIET.ConsultarGrupoInvestigacionPorNombre(grupo.NOMBREGRUPOINVESTIGACION);

                bool bandNombre = false;
                bool bandCodigo = false;

                if (gInvestigacionNombre == null )
                {
                    bandNombre = true;
                }
                if (gInvestigacionCodigo == null)
                {
                    bandCodigo = true;
                }

                if (bandCodigo && bandNombre)
                {      
                    FachadaSIFIET.RegistrarGrupoInvestigacion(grupo);
                    TempData["Mensaje"] = "Grupo de Investigación creado con éxito";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mensaje = "El código y el nombre del grupo deben ser únicos";
                    return View(grupo);
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
        public ActionResult ModificarGrupoInvestigacion(FormCollection datos)
        {
            var listaDepartamentos = FachadaSIFIET.ConsultarDepartamentos();
            ViewBag.ListaDepartamentos = new SelectList(listaDepartamentos, "IDENTIFICADORDEPARTAMENTO", "NOMBREDEPARTAMENTO");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NombreCompletoDocente");

            GRUPO_INVESTIGACION gInvestigacionActual = FachadaSIFIET.ConsultarGrupoInvestigacion(int.Parse(datos["IDENTIFICADORGRUPOINVES"]));

            ViewBag.lstDepartamentos = listaDepartamentos;
            ViewBag.lstDocentes = listaDocentes;
            ViewBag.DepartamentoRegistrado = gInvestigacionActual.DEPARTAMENTO;
            ViewBag.DocenteRegistrado = gInvestigacionActual.DOCENTE;            
            
            if (!ModelState.IsValid) return View(gInvestigacionActual);
            GRUPO_INVESTIGACION grupo = new GRUPO_INVESTIGACION();
            try
            {                
                grupo.NOMBREGRUPOINVESTIGACION = datos["NOMBREGRUPOINVESTIGACION"];
                grupo.CODIGOGRUPOINVESTIGACION = datos["CODIGOGRUPOINVESTIGACION"];
                grupo.DESCRIPCIONGRUPOINVESTIGACION = datos["DESCRIPCIONGRUPOINVESTIGACION"];
                grupo.ESTADOGRUPOINVESTIGACION = datos["ESTADOGRUPOINVESTIGACION"];
                grupo.IDENTIFICADORGRUPOINVES = int.Parse(datos["IDENTIFICADORGRUPOINVES"]);
                grupo.IDENTIFICADORDEPARTAMENTO = int.Parse(datos["departamento"]);
                grupo.IDENTIFICADORUSUARIO = int.Parse(datos["docente"]);
                FachadaSIFIET.ModificarGrupoInvestigacion(grupo);
                TempData["Mensaje"] = "Grupo de investigación editado con exito";
                return RedirectToAction("Index");
                               
            }
            catch (Exception e)
            {
                //ViewBag.Mensaje = "Error" + e.Message;
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