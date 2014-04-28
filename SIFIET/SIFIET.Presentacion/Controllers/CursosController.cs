﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionProgramas.Datos.Modelo;

namespace SIFIET.Presentacion.Controllers
{
    public class CursosController : Controller
    {
        public ActionResult Index(decimal? idCurso, string nombreCurso)
        {
            ViewData["Mensaje"] = Session["varsession"];
            ViewBag.Resultado = TempData["ResultadoOperacion"] as string;
            //var selectItems = new Dictionary<decimal, string> {{1, "Idenfificador"}, {2, "Nombre Curso"}};
            var one = new SelectListItem() { Value = "1", Text = "Identificador" };
            var two = new SelectListItem() { Value = "2", Text = "Nombre" };
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(one);
            lista.Add(two);
            ViewBag.campoBusqueda = new SelectList(lista, "value", "text");
            try
            {
                if (idCurso == null | String.IsNullOrEmpty(nombreCurso))
                    return View(FachadaSIFIET.ConsultarCursos(0, nombreCurso));
                else
                {
                    if (idCurso == 1)
                        return View(FachadaSIFIET.ConsultarCursos(decimal.Parse(nombreCurso), ""));
                    if (idCurso == 2)
                        return View(FachadaSIFIET.ConsultarCursos(0, nombreCurso));
                    else
                        return View(FachadaSIFIET.ConsultarCursos(0, nombreCurso));
                }

            }
            catch (Exception)
            {

                return View(FachadaSIFIET.ConsultarCursos(0, ""));
            }

        }

        public ActionResult VisualizarCurso(decimal idCurso)
        {
            return View(FachadaSIFIET.VisualizarCurso(idCurso));
        }

        public ActionResult RegistrarCurso()
        {
            var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas("");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NOMBRESUSUARIO");
            ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarCurso(CURSO oCurso)
        {
            try
            {
                var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas("");
                var listaDocentes = FachadaSIFIET.ConsultarDocentes();
                ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
                ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NOMBRESUSUARIO");
                if (!ModelState.IsValid) return View(oCurso);
                bool resultado = FachadaSIFIET.RegistrarCurso(oCurso);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Curso Agregado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Agregar el Curso";

                ViewBag.Mensaje = "false";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(oCurso);
            }
        }

        public ActionResult ModificarCurso(decimal idCurso)
        {
            var oCurso = FachadaSIFIET.VisualizarCurso(idCurso) as CURSO;
            var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas("");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NOMBRESUSUARIO");
            return View(oCurso);
        }

        [HttpPost]
        public ActionResult ModificarCurso(CURSO oCurso)
        {
            try
            {
                var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas("");
                var listaDocentes = FachadaSIFIET.ConsultarDocentes();
                ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
                ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NOMBRESUSUARIO");
                
                if (!ModelState.IsValid) return View(oCurso);
                var resultado = FachadaSIFIET.ModificarCurso(oCurso);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Curso Modificado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Modificar el Curso";
                ViewBag.Mensaje = "false";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(oCurso);
            }
        }

        public ActionResult EliminarCurso(decimal idCurso)
        {
            return View(FachadaSIFIET.VisualizarCurso(idCurso));
        }


        [HttpPost, ActionName("EliminarCurso")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarCursoConfirmacion(decimal idCurso)
        {
            try
            {
                var resultado = FachadaSIFIET.EliminarCurso(idCurso);
                if (resultado)
                    TempData["ResultadoOperacion"] = "Curso Eliminado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Eliminar el Curso";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CargarArchivo()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CargarArchivo(HttpPostedFileBase archivo)
        {
            Boolean fileOK = false;
            String fileExtension = Path.GetExtension(archivo.FileName).ToLower();
            String[] allowedExtensions = { ".xls", ".xlsx" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }

            DataTable dt = new DataTable();
            DataTable aux = new DataTable();
            DataTable det = new DataTable();
            if (fileOK)
            {
                string fn = Path.GetFileName(archivo.FileName);
                string filePath = Server.MapPath(@"~\Uploads") + "\\" + fn;
                archivo.SaveAs(filePath);

                using (OleDbConnection cn = new OleDbConnection())
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        if (fileExtension == ".xls")
                        {
                            cn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath +
                                                  ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
                        }
                        if (fileExtension == ".xlsx")
                        {
                            cn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                                                    ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }

                        cn.Open();
                        aux = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        List<string> listhojas = new List<string>();
                        foreach (DataRow row in aux.Rows)
                        {
                            listhojas.Add(row["TABLE_NAME"].ToString());
                        }

                        cmd.Connection = cn;
                        List<int> validador = new List<int>();
                        foreach (string hoja in listhojas)
                        {
                            cmd.CommandText = "Select * from [" + hoja + "]";
                            using (OleDbDataAdapter adpaux = new OleDbDataAdapter(cmd))
                            {
                                adpaux.Fill(det);
                            }
                            if (det.Columns.Count > 1)
                            {
                                validador.Add(1);
                            }
                            else
                            {
                                validador.Add(0);
                            }
                            cmd.CommandText = "";
                            det.Reset();
                        }

                        int unelemento = 0;
                        for (int j = 0; j < validador.Count; j++)
                        {
                            unelemento = unelemento + validador[j];
                        }

                        string consulta = "";
                        int i = 0;
                        foreach (string hoja in listhojas)
                        {
                            if (unelemento == 1 && validador[i] == 1)
                            {
                                consulta = consulta + "Select * from [" + hoja + "]";
                            }
                            else
                            {
                                if (!listhojas.Last().Equals(hoja) && validador[i] == 1)
                                {
                                    consulta = consulta + "Select * from [" + hoja + "] union ";
                                }
                                else if (validador[i] == 1)
                                {
                                    consulta = consulta + "Select * from [" + hoja + "]";
                                }
                            }
                            i++;
                        }
                        cmd.CommandText = consulta;
                        using (OleDbDataAdapter adp = new OleDbDataAdapter(cmd))
                        {
                            adp.Fill(dt);
                        }
                    }
                    cn.Close();
                    System.IO.File.Delete(filePath);
                }
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["DatosSession"] = ds;
            int Ncol = 5;
            if (Ncol != ds.Tables[0].Columns.Count)
            {
                return RedirectToAction("CargarArchivo");
            }
            return RedirectToAction("CargarInformacion");
        }

        public ActionResult CargarInformacion()
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Session["DatosSession"];
            return View(ds);
        }

        public ActionResult EnviarDatos()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = (DataSet)Session["DatosSession"];
                StreamWriter wr =
                    new StreamWriter(
                        @"C:\InfoAlex\Windows 8.1\Proyecto II\Aplicacion\SIFIET\SIFIET.Presentacion\Uploads\file.txt");

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    wr.WriteLine(row[0] + "," + row[1] + "," + row[2] + "," + row[3] + "," + row[4]);
                }

                wr.Dispose();

                Session.Remove("DatosSession");
                bool retorno =
                    FachadaSIFIET.CargarInformacionCurso(@"C:\InfoAlex\Windows 8.1\Proyecto II\Aplicacion\SIFIET\SIFIET.Presentacion\Uploads\file.txt");
                if (retorno)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                Dispose();
                return RedirectToAction("Index");
            }
        }

        public ActionResult RegistrarHorario(decimal idCurso)
        {
            ViewBag.ListaSalones = FachadaSIFIET.ConsultarSalones(0, "");
            ViewBag.ListaDias = FachadaSIFIET.ConsultarSalones(0, "");
            ViewBag.ListaHoraInicio = FachadaSIFIET.ConsultarSalones(0, "");
            ViewBag.ListaHoraFin = FachadaSIFIET.ConsultarSalones(0, "");
            return View(FachadaSIFIET.VisualizarCurso(idCurso));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarHorario(FormCollection datos)
        {
            //AQUI VA LA LOGICA DE EDITAR LA FRANJA HORARIA
            ViewBag.Horario = FachadaSIFIET.ObtenarHorarioCurso(decimal.Parse(datos["idCurso"]));
            return View(FachadaSIFIET.VisualizarCurso(decimal.Parse(datos["idCurso"])));
        }

        public ActionResult EliminarHorario(FormCollection datos)
        {
            //AQUI VA LA LOGICA DE Eliminar LA FRANJA HORARIA
            return View();
        }
    }
}