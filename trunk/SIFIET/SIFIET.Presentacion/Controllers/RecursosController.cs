using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
using SIFIET.GestionInfraestructura.Datos.Modelo;

namespace SIFIET.Presentacion.Controllers
{
    public class RecursosController:Controller
    {
        [Authorize]
        public ActionResult Index(decimal? idRecurso, string nombreRecursoIn, string estado)
        {
            ViewData["Mensaje"] = Session["varsession"];
            ViewBag.Resultado = TempData["ResultadoOperacion"] as string;
            var nombreRecurso = new SelectListItem() { Value = "2", Text = "Nombre" };
            var nombreFacultad = new SelectListItem() { Value = "3", Text = "Facultad" };
            var lista = new List<SelectListItem> { nombreRecurso, nombreFacultad };
            ViewBag.campoBusqueda = new SelectList(lista, "value", "text");
            if (idRecurso == null | String.IsNullOrEmpty(nombreRecursoIn))
                return View(FachadaSIFIET.ConsultarRecursos(0, nombreRecursoIn, estado));
            var resultado = FachadaSIFIET.ConsultarRecursos((decimal)idRecurso, nombreRecursoIn, estado);
            if (resultado.Count == 0)
                ViewBag.ResultadoBusqueda = "ListaVacia";
            return View(resultado);
        }
        [Authorize]
        public ActionResult VisualizarRecurso(decimal idRecurso)
        {
            return View(FachadaSIFIET.VisualizaRecurso(idRecurso));
        }
        //[Authorize(Roles = "Salon")]
        public ActionResult RegistrarRecurso()
        {
            var listaFacultades = FachadaSIFIET.ConsultarFacultades(0, "");
            var listaTiposRecurso = FachadaSIFIET.ConsultarTiposRecurso();
            ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
            ViewBag.ListaTiposRecurso = new SelectList(listaTiposRecurso, "IDTIPORECURSO", "NOMBRETIPORECURSO");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarRecurso(RECURSO oRecurso)
        {
            try
            {
                var listaFacultades = FachadaSIFIET.ConsultarFacultades(0, "");
                var listaTiposRecurso = FachadaSIFIET.ConsultarTiposRecurso();
                ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
                ViewBag.ListaTiposRecurso = new SelectList(listaTiposRecurso, "IDTIPORECURSO", "NOMBRETIPORECURSO");
                if (!ModelState.IsValid) return View(oRecurso);
                var resultado = FachadaSIFIET.RegistrarRecurso(oRecurso);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Salon Agregado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Agregar el Salon";

                ViewBag.Mensaje = "false";

                return RedirectToAction("Index");
            }
            catch
            {
                return View(oRecurso);
            }
        }
        //[Authorize(Roles = "Salon")]
        public ActionResult ModificarRecurso(decimal idRecurso)
        {
            var oRecurso = FachadaSIFIET.VisualizaRecurso(idRecurso) as RECURSO;
            var listaFacultades = FachadaSIFIET.ConsultarFacultades(0, "");
            var listaTiposRecurso = FachadaSIFIET.ConsultarTiposRecurso();
            ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
            ViewBag.ListaTiposRecurso = new SelectList(listaTiposRecurso, "IDTIPORECURSO", "NOMBRETIPORECURSO");
            return View(oRecurso);
        }

        [HttpPost]
        public ActionResult ModificarRecurso(RECURSO oRecurso)
        {
            try
            {
                var listaFacultades = FachadaSIFIET.ConsultarFacultades(0, "");
                var listaTiposRecurso = FachadaSIFIET.ConsultarTiposRecurso();
                ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
                ViewBag.ListaTiposRecurso = new SelectList(listaTiposRecurso, "IDTIPORECURSO", "NOMBRETIPORECURSO");
            
                if (!ModelState.IsValid) return View(oRecurso);
                var resultado = FachadaSIFIET.ModificarRecurso(oRecurso);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Recurso Modificado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Modificar el Recurso";
                ViewBag.Mensaje = "false";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(oRecurso);
            }
        }
        /*
        public ActionResult EliminarSalon(decimal idSalon)
        {
            return View(FachadaSIFIET.VisualizarSalon(idSalon));
        }


        [HttpPost, ActionName("EliminarSalon")]
        [ValidateAntiForgeryToken]*/
        // [Authorize(Roles = "Salon")]
        public ActionResult EliminarRecurso(decimal idRecurso)
        {
            try
            {
                var resultado = FachadaSIFIET.EliminarRecurso(idRecurso);
                if (resultado)
                    TempData["ResultadoOperacion"] = "Recurso Eliminado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Eliminar el Recurso";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ResultadoOperacion"] = "Fallo al Eliminar el Recurso";
                return RedirectToAction("Index");
            }
        }

        // Lectura del archivo Excel
        //[Authorize(Roles = "Salon")]
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
            int Ncol = 4;
            if (Ncol != ds.Tables[0].Columns.Count)
            {
                TempData["ArchivoSession"] = "El Archivo que se intentó cargar no contiene un formato valido";
                return RedirectToAction("CargarArchivo");
            }
            return RedirectToAction("CargarInformacion");
        }

        // Mostrar la infomacion antes de almacenar en la Base de Datos
        // [Authorize(Roles = "Salon")]
        public ActionResult CargarInformacion()
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Session["DatosSession"];
            bool valido = ValidarCampos(ds);
            if (valido)
            {
                TempData["DatosOkSession"] = "Los datos cargardos son validos";
                return View(ds);
            }
            TempData["DatosOkSession"] = "Los datos cargardos son invalidos";
            return View(ds);
        }

        //Valida los campos uno a uno contenidos en el DataSet, mostrando informacion de errores por cada tupla

        public bool ValidarCampos(DataSet ds)
        {
            string erroresTupla = "", MsjBDerrores = "Los registros no pueden ser almacenados en la base da datos.";
            bool validar = false;
            int numTupla = 1;
            string IdFacultad;
            string IdTipoRecurso;
            List<int> validadorC = new List<int>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                validar = VerificarExistenciaTipoRecurso(row[0].ToString());
                if (validar) 
                {
                    if (row[0].ToString().Length < 38)
                    {
                        if (!row[0].ToString().Equals(""))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "El campo Tipo Recurso de la tupla " + numTupla +
                                           " parece estar vacio.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El Tipo Recurso de la tupla " + numTupla +
                                       " supera la cantidad de caracteres permitidos (38).";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El Tipo Recurso " + row[0] + " de la tupla " + numTupla +
                                        " no fue encontrado.";
                }

                IdFacultad = ObtenerIdFacultad(row[1].ToString());
                if (!IdFacultad.Equals(""))
                {
                    validar = VerificarExistenciaRecurso(row[2].ToString(), IdFacultad);
                }

                if (!validar)
                {
                    if (row[2].ToString().Length < 120)
                    {
                        if (!row[2].ToString().Equals(""))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "El campo Nombre Recurso de la tupla " + numTupla +
                                           " parece estar vacio.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El campo Nombre Recurso de la tupla " + numTupla +
                                       " supera la cantidad de caracteres permitidos (38).";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El Recurso " + row[2] + " de la tupla " + numTupla +
                                        " ya existe.";
                }

                validar = VerificarCampoFacultad(row[1].ToString());
                if (validar)
                {
                    if (row[1].ToString().Length < 120)
                    {
                        if (!row[1].ToString().Equals(""))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "El campo Nombre Facultad de la tupla " + numTupla +
                                           " parece estar vacio.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El Nombre Facultad de la tupla " + numTupla +
                                       " supera la cantidad de caracteres permitidos (120).";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "La Facultad " + row[1] + " de la tupla " + numTupla +
                                        " no fue encontrada.";
                }

                if (row[3].ToString().Length < 30)
                {
                    if (!row[3].ToString().Equals(""))
                    {
                        if (row[3].ToString().Equals("Activo") || row[3].ToString().Equals("Inactivo") || row[3].ToString().Equals("activo") || row[3].ToString().Equals("inactivo"))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "El Estado " + row[3] + " de la tupla " + numTupla +
                                           " no es valido.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El campo Estado de la tupla " + numTupla +
                                       " parece estar vacio.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El Estado de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (30).";
                }

                numTupla++;
            }

            foreach (var var in validadorC)
            {
                if (var.Equals(0))
                {
                    MsjBDerrores = MsjBDerrores + erroresTupla;
                    TempData["ErroresSession"] = MsjBDerrores;
                    return false;
                }
            }
            return true;
        }

        //Funciones necesarias para validacion de la informacion del DataSet, obtienen la existencia de tuplas o informacion de campos

        private bool VerificarExistenciaTipoRecurso(string TipoRecurso)
        {
            bool verificarTR = false;

            verificarTR = FachadaSIFIET.VerificarExistenciaTipoRecurso(TipoRecurso);
            return verificarTR;
        }

        private bool VerificarExistenciaRecurso(string nombreRecurso, string IdTipoRecurso)
        {
            bool verificarR = false;

            verificarR = FachadaSIFIET.VerificarExistenciaRecurso(nombreRecurso, IdTipoRecurso);
            return verificarR;
        }

        private bool VerificarCampoFacultad(string nombreFacultad)
        {
            bool verificarC = false;
            verificarC = FachadaSIFIET.VerificarCampoFacultad(nombreFacultad);
            return verificarC;
        }

        private string ObtenerIdFacultad(string nombreFacultad)
        {
            return FachadaSIFIET.ObtenerIdFacultad(nombreFacultad);
        }

        private string ObtenerIdTipoRecurso(string tipoRecurso)
        {
            return FachadaSIFIET.ObtenerIdTipoRecurso(tipoRecurso);
        }

        // [Authorize(Roles = "Salon")]
        public ActionResult EnviarDatos()
        {
            /*try
            {*/
            DataSet ds = new DataSet();
            ds = (DataSet)Session["DatosSession"];
            string filePath = Server.MapPath(@"~\Uploads") + "\\file.txt";
            string IdFacultad;
            string IdTipoRecurso;
            StreamWriter wr = new StreamWriter(filePath);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                IdFacultad = ObtenerIdFacultad(row[1].ToString());
                IdTipoRecurso = ObtenerIdTipoRecurso(row[0].ToString());
                wr.WriteLine(IdTipoRecurso + "," + IdFacultad + "," + row[2] + "," + row[3]);
            }
            wr.Dispose();

            Session.Remove("DatosSession");
            bool retorno =
                FachadaSIFIET.CargarInformacionRecurso(filePath);
            if (retorno)
            {
                TempData["UpSession"] = "El archivo fué cargado con exito";
                return RedirectToAction("Index");
            }
            TempData["UpSession"] = "El archivo no fué cargado con exito";
            return RedirectToAction("Index");
            /*}
            catch (Exception)
            {
                Dispose();
                return RedirectToAction("Index");
            }*/
        }
    }
}