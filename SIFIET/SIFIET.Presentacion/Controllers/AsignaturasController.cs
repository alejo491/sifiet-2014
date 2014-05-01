﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionProgramas.Datos.Modelo;

namespace SIFIET.Presentacion.Controllers
{
    public class AsignaturasController : Controller
    {
        //
        // GET: /Asignaturas/
        public ActionResult Index(decimal? idAsignatura, string nombreAsignatura)
        {
            ViewData["Mensaje"] = Session["varsession"];
            ViewBag.Resultado = TempData["ResultadoOperacion"] as string;
            var identificacion = new SelectListItem() { Value = "1", Text = "Codigo" };
            var nombresalon = new SelectListItem() { Value = "2", Text = "Nombre" };
            var lista = new List<SelectListItem> {identificacion, nombresalon};
            ViewBag.campoBusqueda = new SelectList(lista, "value", "text");
            if (idAsignatura == null | String.IsNullOrEmpty(nombreAsignatura))
                return View(FachadaSIFIET.ConsultarAsignaturas(0, nombreAsignatura));
            else
            {
                var resultado = FachadaSIFIET.ConsultarAsignaturas((decimal)idAsignatura, nombreAsignatura);
                if (resultado.Count == 0)
                    ViewBag.ResultadoBusqueda = "ListaVacia";
                return View(resultado);
            }
        }

        //
        // GET: /Asignaturas/Details/5
        public ActionResult VisualizarAsignatura(decimal idAsignatura)
        {
            return View(FachadaSIFIET.VisualizarAsignatura(idAsignatura));
        }

        //
        // GET: /Asignaturas/Create
        public ActionResult RegistrarAsignatura()
        {
            var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0,"");
            ViewBag.ListaAsignaturas = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA");
            var listaPlanesEstudios = FachadaSIFIET.ConsultarPlanestudios("");
            ViewBag.ListaPlanesEstudios = new SelectList(listaPlanesEstudios, "IDENTIFICADORPLANESTUDIOS", "NOMBREPLANESTUDIOS");
            return View();
        }

        //
        // POST: /Asignaturas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarAsignatura(ASIGNATURA oAsignatura)
        {
            try
            {
                // TODO: Add insert logic here
                var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0,"");
                ViewBag.ListaAsignaturas = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA");
                var listaPlanesEstudios = FachadaSIFIET.ConsultarPlanestudios("");
                ViewBag.ListaPlanesEstudios = new SelectList(listaPlanesEstudios, "IDENTIFICADORPLANESTUDIOS", "NOMBREPLANESTUDIOS");
                if (!ModelState.IsValid) return View(oAsignatura);
                bool resultado = FachadaSIFIET.RegistrarAsignatura(oAsignatura);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Asignatura Agregada con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Agregoar la Asignatura";

                ViewBag.Mensaje = "false";

                return RedirectToAction("Index");
            }
            catch
            {
                return View(oAsignatura);
            }
        }

        //
        // GET: /Asignaturas/Edit/5
        public ActionResult ModificarAsignatura(decimal idAsignatura)
        {
            var oAsignatura = FachadaSIFIET.VisualizarAsignatura(idAsignatura) as ASIGNATURA;
            var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0,"");
            ViewBag.ListaAsignaturasCorrequisitos = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA", oAsignatura.ListaCorrequisitos);
            ViewBag.ListaAsignaturasPrerequisitos = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA", oAsignatura.ListaPrerequisitos);
            var listaPlanesEstudios = FachadaSIFIET.ConsultarPlanestudios("");
            ViewBag.ListaPlanesEstudios = new SelectList(listaPlanesEstudios, "IDENTIFICADORPLANESTUDIOS", "NOMBREPLANESTUDIOS");
            return View(oAsignatura);
        }

        //
        // POST: /Asignaturas/Edit/5
        [HttpPost]
        public ActionResult ModificarAsignatura(ASIGNATURA oAsignatura)
        {
            try
            {
                var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0,"");
                ViewBag.ListaAsignaturasCorrequisitos = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA", oAsignatura.ListaCorrequisitos);
                ViewBag.ListaAsignaturasPrerequisitos = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA", oAsignatura.ListaPrerequisitos);
                var listaPlanesEstudios = FachadaSIFIET.ConsultarPlanestudios("");
                ViewBag.ListaPlanesEstudios = new SelectList(listaPlanesEstudios, "IDENTIFICADORPLANESTUDIOS", "NOMBREPLANESTUDIOS");

                if (!ModelState.IsValid) return View(oAsignatura);
                var resultado = FachadaSIFIET.ModificarAsignatura(oAsignatura);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Asignatura Modificada con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Modificar la Asignatura";
                ViewBag.Mensaje = "false";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(oAsignatura);
            }
        }
        //
        // GET: /Asignaturas/Delete/5
        /*
        public ActionResult EliminarAsignatura(decimal idAsignatura)
        {
            return View(FachadaSIFIET.VisualizarAsignatura(idAsignatura));
        }

        //
        // POST: /Asignaturas/Delete/5
        [HttpPost, ActionName("EliminarAsignatura")]
        [ValidateAntiForgeryToken]*/
        public ActionResult EliminarAsignatura(decimal idAsignatura)
        {
            try
            {
                var resultado = FachadaSIFIET.EliminarAsignatura(idAsignatura);
                if (resultado)
                    TempData["ResultadoOperacion"] = "Asignatura Eliminada con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Eliminar la Asignatura";
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
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
                }
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["DatosSession"] = ds;
            int Ncol = 12;
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
                StreamWriter wr = new StreamWriter(
                    @"C:\InfoAlex\Windows 8.1\Proyecto II\Aplicacion\SIFIET\SIFIET.Presentacion\Uploads\file.txt");

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    wr.WriteLine(row[0] + "," + row[1] + "," + row[2] + "," + row[3] + "," + row[4] +
                                 "," + row[5] + "," + row[6] + "," + row[7] + "," + row[8] + "," +
                                 row[9] + "," + row[10] + "," + row[11]);
                }
                wr.Dispose();

                Session.Remove("DatosSession");
                bool retorno =
                    FachadaSIFIET.CargarInformacionAsignatura(
                        @"C:\InfoAlex\Windows 8.1\Proyecto II\Aplicacion\SIFIET\SIFIET.Presentacion\Uploads\file.txt");
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
    }
}