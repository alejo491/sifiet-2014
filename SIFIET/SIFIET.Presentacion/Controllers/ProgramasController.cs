using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionProgramas.Datos.Modelo;
using System.Data.OleDb;

namespace SIFIET.Presentacion.Controllers
{
    public class ProgramasController : Controller
    {


        // GET: /Programa/

        public ViewResult Index(string estado = "Activo", string campo = "", string busqueda = "")
        {
            ViewBag.ResultadoOperacion = TempData["ResultadoOperacion"] as string;
            ViewBag.estado = estado;
            ViewBag.campo = campo;
            ViewBag.busqueda = busqueda;

            List<PROGRAMA> programas = FachadaSIFIET.ConsultarProgramasAcademicos(estado, campo, busqueda);
            if (String.IsNullOrEmpty(campo) && String.IsNullOrEmpty(busqueda))
            {
                ViewBag.ResultadoBusqueda = "Hay " + programas.Count() + " registro(s)";
            }
            else if (programas.Count() == 0)
            {
                ViewBag.ResultadoBusqueda = "No se encontraron registros";
            }
            else
            {
                ViewBag.ResultadoBusqueda = "Se encontro" + programas.Count() + " registro(s)";
            }

            return View(programas);
        }

        //
        // GET: /Programa/Details/5

        public ViewResult VisualizarProgramaAcademico(decimal idPrograma)
        {
            PROGRAMA programa = FachadaSIFIET.ConsultarProgramaAcademico(idPrograma);
            return View(programa);
        }

        //
        // GET: /Programa/Create

        public ActionResult RegistrarProgramaAcademico()
        {
            ViewBag.IDENTIFICADORFACULTAD = new SelectList(FachadaSIFIET.ConsultarFacultades(), "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
            return View();
        }

        //
        // POST: /Programa/Create

        [HttpPost]
        public ActionResult RegistrarProgramaAcademico(PROGRAMA objPrograma)
        {
            if (ModelState.IsValid)
            {
                if (FachadaSIFIET.RegistrarProgramaAcademico(objPrograma))
                {
                    TempData["ResultadoOperacion"] = "Programa creado con Exito.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo registrar este programa.";
                }

            }
            else
            {
                ViewBag.ResultadoOperacion = "Ocurrio un error, No se pudo registrar este programa";
            }

            ViewBag.IDENTIFICADORFACULTAD = new SelectList(FachadaSIFIET.ConsultarFacultades(), "IDENTIFICADORFACULTAD", "NOMBREFACULTAD", objPrograma.IDENTIFICADORFACULTAD);
            return View(objPrograma);
        }

        //
        // GET: /Programa/Edit/5

        public ActionResult EditarProgramaAcademico(decimal idPrograma)
        {
            PROGRAMA objPrograma = FachadaSIFIET.ConsultarProgramaAcademico(idPrograma);
            ViewBag.IDENTIFICADORFACULTAD = new SelectList(FachadaSIFIET.ConsultarFacultades(), "IDENTIFICADORFACULTAD", "NOMBREFACULTAD", objPrograma.IDENTIFICADORFACULTAD);
            return View(objPrograma);
        }

        //
        // POST: /Programa/Edit/5

        [HttpPost]
        public ActionResult EditarProgramaAcademico(PROGRAMA objPrograma)
        {
            if (ModelState.IsValid)
            {
                if (FachadaSIFIET.EditarProgramaAcademico(objPrograma))
                {
                    TempData["ResultadoOperacion"] = "Programa editado con Exito.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ResultadoOperacion"] = "Ocurrio un error, No se pudo editar el programa.";
                }
            }
            else
            {
                TempData["ResultadoOperacion"] = "Ocurrio un error, No se pudo editar el programa.";
            }
            ViewBag.IDENTIFICADORFACULTAD = new SelectList(FachadaSIFIET.ConsultarFacultades(), "IDENTIFICADORFACULTAD", "NOMBREFACULTAD", objPrograma.IDENTIFICADORFACULTAD);
            return View(objPrograma);
        }

        //
        // GET: /Programa/Delete/5

        public ActionResult EliminarProgramaAcademico(decimal idPrograma)
        {
            if (FachadaSIFIET.EliminarProgramaAcademico(idPrograma))
            {
                TempData["ResultadoOperacion"] = "Programa eliminado con Exito.";
            }
            else
            {
                TempData["ResultadoOperacion"] = "Ocurrio un error, no se pudo eliminar el programa";
            }
            return RedirectToAction("Index");
        }

        public ActionResult CargarInformacion()
        {
            if (Request.Files.Count > 0 && Request.Files["archivo"] != null)
            {
                //var nombreArchivo = Path.GetFileName(Request.Files["archivo"].FileName);
                string rutaArchivo = Server.MapPath("~/Uploads/") + Request.Files["archivo"].FileName;
                string extencionArchivo = System.IO.Path.GetExtension(Request.Files["archivo"].FileName);
                if (extencionArchivo == ".xls" || extencionArchivo == ".xlsx")
                {
                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        System.IO.File.Delete(rutaArchivo);
                    }
                    Request.Files["archivo"].SaveAs(rutaArchivo);
                    DataSet datosExcel = LeerRegistrosExcel(rutaArchivo, extencionArchivo);
                    if (datosExcel != null)
                    {
                        if (FachadaSIFIET.CargarInformacion(datosExcel))
                        {
                            ViewBag.ResultadoOperacion = "Los datos se cargaron correctamente.";
                            return View(datosExcel);
                        }
                        else
                        {
                            ViewBag.ResultadoOperacion = "Error, No se puedieron cargar los datos.";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.ResultadoOperacion = "Error al cargar el archivo.";
                        return View();
                    }
                }
            }
            return View();
        }

        private DataSet LeerRegistrosExcel(string rutaArchivo, string extencionArchivo)
        {
            DataSet ds = new DataSet();
            string cadenaConexionExcel = string.Empty;

            if (extencionArchivo == ".xls")
            {
                cadenaConexionExcel = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                                      rutaArchivo + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            //connection String for xlsx file format.
            else if (extencionArchivo == ".xlsx")
            {
                cadenaConexionExcel = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                      rutaArchivo + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            OleDbConnection conexionExcel = new OleDbConnection(cadenaConexionExcel);
            conexionExcel.Open();
            DataTable dt = new DataTable();

            dt = conexionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt == null)
            {
                //return null;
            }

            String[] hojasExcel = new string[dt.Rows.Count];
            int iterador = 0;
            foreach (DataRow row in dt.Rows)
            {
                hojasExcel[iterador] = row["TABLE_NAME"].ToString();
                iterador++;
            }

            OleDbConnection excelConnection1 = new OleDbConnection(cadenaConexionExcel);
            string query = string.Format("Select * from [{0}]", hojasExcel[0]);
            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
            {
                dataAdapter.Fill(ds);
            }
            conexionExcel.Close();
            return ds;
        }


    }
}