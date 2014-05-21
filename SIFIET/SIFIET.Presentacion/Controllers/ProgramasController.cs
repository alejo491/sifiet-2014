using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        //Implementacion Robinson...
        /*
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
        */

        // Lectura del archivo Excel

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
            int Ncol = 10;
            if (Ncol != ds.Tables[0].Columns.Count)
            {
                TempData["ArchivoSession"] = "El Archivo que se intentó cargar no contiene un formato valido";
                return RedirectToAction("CargarArchivo");
            }
            return RedirectToAction("CargarInformacion");
        }

        // Mostrar la infomacion antes de almacenar en la Base de Datos

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
            bool validar;
            int numTupla = 1;
            string[] campos;
            List<int> validadorC = new List<int>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                validar = VerificarExistenciaFacultad(row[0].ToString());
                if (validar)
                {
                    if (row[0].ToString().Length < 120)
                    {
                        if (!row[0].ToString().Equals(""))
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
                    erroresTupla = erroresTupla + "La Facultad " + row[0] + " de la tupla " + numTupla +
                                        " no fue encontrada.";
                }

                validar = VerificarExistenciaPrograma(row[1].ToString());
                if (!validar)
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
                            erroresTupla = erroresTupla + "El campo Nombre Programa de la tupla " + numTupla +
                                           " parece estar vacio.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El Nombre Programa de la tupla " + numTupla +
                                       " supera la cantidad de caracteres permitidos (120).";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El Programa " + row[1] + " de la tupla " + numTupla +
                                        " ya existe.";
                }

                if (row[2].ToString().Length < 250)
                {
                    if (!row[2].ToString().Equals(""))
                    {
                        validadorC.Add(1);
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El campo Descripcion de la tupla " + numTupla +
                                       " parece estar vacio.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "La Descripcion de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (250).";
                }

                if (row[3].ToString().Length < 30)
                {
                    if (!row[3].ToString().Equals(""))
                    {
                        if (row[3].ToString().Equals("Diurna") || row[3].ToString().Equals("Nocturna"))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "La Jornada " + row[3] + " de la tupla " + numTupla +
                                           " no es valida.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El campo Jornada de la tupla " + numTupla +
                                            " parece estar vacio.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "La Jornada de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (30).";
                }

                if (!row[4].ToString().Equals(""))
                {
                    if (row[4].ToString().Equals("6") || row[4].ToString().Equals("7") || row[4].ToString().Equals("8") ||
                        row[4].ToString().Equals("9") || row[4].ToString().Equals("10") || row[4].ToString().Equals("11") || row[4].ToString().Equals("12"))
                    {
                        validadorC.Add(1);
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "La duracion " + row[4] + " del programa en la tupla " + numTupla + " " +
                                       " no es valida.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El campo duracion del programa en la tupla " + numTupla +
                                       " parece estar vacio.";
                }

                if (row[5].ToString().Length < 30)
                {
                    if (!row[5].ToString().Equals(""))
                    {
                        if (row[5].ToString().Equals("Anual") || row[5].ToString().Equals("Semestral"))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "La Admisión " + row[5] + " de la tupla " + numTupla +
                                           " no es valida.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El campo Admisión de la tupla " + numTupla +
                                            " parece estar vacio.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "La Admisión de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (30).";
                }

                
                if (!row[6].ToString().Equals(""))
                {
                    validadorC.Add(1);
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El campo CodigoSNIES de la tupla " + numTupla +
                                    " parece estar vacio.";
                }

                if (row[7].ToString().Length < 30)
                {
                    if (!row[7].ToString().Equals(""))
                    {
                        validadorC.Add(1);
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El periodo duración del programa de la tupla " + numTupla +
                                       " parece estar vacio.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El periodo duración del programa de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (30).";
                }

                if (row[8].ToString().Length < 30)
                {
                    if (!row[8].ToString().Equals(""))
                    {
                        if (row[8].ToString().Equals("Activo") || row[8].ToString().Equals("Inactivo"))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "El Estado " + row[8] + " de la tupla " + numTupla +
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

                if (row[9].ToString().Length < 50)
                {
                    if (!row[9].ToString().Equals(""))
                    {
                        if (row[9].ToString().Equals("Presencial") || row[9].ToString().Equals("Semi-presencial"))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "La Modalidad " + row[9] + " de la tupla " + numTupla +
                                           " no es valida.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El campo Modalidad de la tupla " + numTupla +
                                            " parece estar vacio.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "La Modalidad de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (50).";
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

        private bool VerificarExistenciaFacultad(string nombreFacultad)
        {
            bool verificarC = false;
            verificarC = FachadaSIFIET.VerificarExistenciaFacultad(nombreFacultad);
            return verificarC;
        }

        private bool VerificarExistenciaPrograma(string nombrePrograma)
        {
            bool verificarC = false;
            verificarC = FachadaSIFIET.VerificarExistenciaPrograma(nombrePrograma);
            return verificarC;
        }

        private string ObtenerIdPrograma(string nombrePrograma)
        {
            return FachadaSIFIET.ObtenerIdPrograma(nombrePrograma);
        }

        public ActionResult EnviarDatos()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = (DataSet)Session["DatosSession"];
                string filePath = Server.MapPath(@"~\Uploads") + "\\file.txt";
                string IdPrograma;
                StreamWriter wr = new StreamWriter(filePath);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    IdPrograma = ObtenerIdPrograma(row[0].ToString());
                    wr.WriteLine(IdPrograma + "," + row[1] + "," + row[2] + "," + row[3] +
                                 "," + row[4] + "," + row[5] + "," + row[6] + "," + row[7] + "," +
                                 row[8] + "," + row[9]);
                }
                wr.Dispose();

                Session.Remove("DatosSession");
                bool retorno =
                    FachadaSIFIET.CargarInformacionAsignatura(filePath);
                if (retorno)
                {
                    TempData["UpSession"] = "El archivo fué cargado con exito";
                    return RedirectToAction("Index");
                }
                TempData["UpSession"] = "El archivo no fué cargado con exito";
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