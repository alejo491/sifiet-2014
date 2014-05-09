using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult Index(decimal? idAsignatura, string nombreAsignatura, string estado)
        {
            ViewData["Mensaje"] = Session["varsession"];
            ViewBag.Resultado = TempData["ResultadoOperacion"] as string;
            var identificacion = new SelectListItem() { Value = "1", Text = "Codigo" };
            var nombresalon = new SelectListItem() { Value = "2", Text = "Nombre" };
            var lista = new List<SelectListItem> { identificacion, nombresalon };
            ViewBag.campoBusqueda = new SelectList(lista, "value", "text");
            if (idAsignatura == null | String.IsNullOrEmpty(nombreAsignatura))
                return View(FachadaSIFIET.ConsultarAsignaturas(0, nombreAsignatura,estado));
            else
            {
                var resultado = FachadaSIFIET.ConsultarAsignaturas((decimal)idAsignatura, nombreAsignatura,estado);
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
            var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0, "","Activo");
            ViewBag.ListaAsignaturas = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA");
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
                var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0, "","Activo");
                ViewBag.ListaAsignaturas = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA");
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
            var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0, "","Activo");
            ViewBag.ListaAsignaturasCorrequisitos = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA", oAsignatura.ListaCorrequisitos);
            ViewBag.ListaAsignaturasPrerequisitos = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA", oAsignatura.ListaPrerequisitos);
            return View(oAsignatura);
        }

        //
        // POST: /Asignaturas/Edit/5
        [HttpPost]
        public ActionResult ModificarAsignatura(ASIGNATURA oAsignatura)
        {
            try
            {
                var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0, "","Activo");
                ViewBag.ListaAsignaturasCorrequisitos = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA", oAsignatura.ListaCorrequisitos);
                ViewBag.ListaAsignaturasPrerequisitos = new MultiSelectList(listaAsignaturas, "NOMBREASIGNATURA", "NOMBREASIGNATURA", oAsignatura.ListaPrerequisitos);

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
            int Ncol = 11;
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
            List<int> validadorC = new List<int>();
            foreach (DataRow row in ds.Tables[0].Rows)
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
                        erroresTupla = erroresTupla + "El campo Plan de Estudios de la tupla " + numTupla +
                                       " parece estar vacio.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El nombre Plan de Estudios de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (120).";
                }

                validar = VerificarExistenciaAsignatura(row[1].ToString(), row[10].ToString());
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
                            erroresTupla = erroresTupla + "El campo Nombre Asignatura de la tupla " + numTupla +
                                           " parece estar vacio.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El Nombre Asignatura de la tupla " + numTupla +
                                       " supera la cantidad de caracteres permitidos (120).";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El Curso " + row[1] + " de la tupla " + numTupla +
                                        " ya existe.";
                }

                if (row[2].ToString().Length < 250)
                {
                    if (!row[2].ToString().Equals(""))
                    {
                        validar = VerificarCampoCoRequisitosAsignatura(row[2].ToString());
                        if (validar)
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "El CoRequisito(s) " + row[2] + " en la tupla " + numTupla +
                                    " no fue encontrado.";
                        }
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El campo CoRequisito(s) de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (250).";
                }

                if (row[3].ToString().Length < 250)
                {
                    if (!row[3].ToString().Equals(""))
                    {
                        validar = VerificarCampoPreRequisitosAsignatura(row[3].ToString());
                        if (validar)
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "El PreRequisito(s) " + row[3] +
                                           " en la tupla " + numTupla + " no fue encontrado.";
                        }
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El campo PreRequisito(s) de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (250).";
                }

                if (!row[4].ToString().Equals(""))
                {
                    if (row[4].ToString().Equals("1") || row[4].ToString().Equals("2") || row[4].ToString().Equals("3") || row[4].ToString().Equals("4") || row[4].ToString().Equals("5") ||
                        row[4].ToString().Equals("6") || row[4].ToString().Equals("7") || row[4].ToString().Equals("8") || row[4].ToString().Equals("9") || row[4].ToString().Equals("10"))
                    {
                        validadorC.Add(1);
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El Semetre " + row[4] + " de la tupla " + numTupla +
                                           " no es valido.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El campo Semestre de la tupla " + numTupla +
                                       " parece estar vacio.";
                }

                if (!row[5].ToString().Equals(""))
                {
                    if (row[5].ToString().Equals("2") || row[5].ToString().Equals("4") || row[5].ToString().Equals("6"))
                    {
                        validadorC.Add(1);
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El numero de Creditos " + row[5] + " de la tupla " + numTupla + " " +
                                       " no es valido.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El campo Creditos de la tupla " + numTupla +
                                       " parece estar vacio.";
                }
                if (row[6].ToString().Length < 50)
                {
                    if (!row[6].ToString().Equals(""))
                    {
                        if (row[6].ToString().Equals("Presencial") || row[6].ToString().Equals("Semi-presencial"))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "La Modalidad " + row[6] + " de la tupla " + numTupla +
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

                if (row[7].ToString().Length < 50)
                {
                    if (!row[7].ToString().Equals(""))
                    {
                        validadorC.Add(1);
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El campo Clasificacion de la tupla " + numTupla +
                                       " parece estar vacio.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "La Clasificacion de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (50).";
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

                if (row[9].ToString().Length < 250)
                {
                    if (!row[9].ToString().Equals(""))
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

                if (row[10].ToString().Length < 15)
                {
                    if (!row[10].ToString().Equals(""))
                    {
                        validadorC.Add(1);
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El campo Codigo de la tupla " + numTupla +
                                       " parece estar vacio.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El Codigo de la tupla " + numTupla +
                                   " supera la cantidad de caracteres permitidos (15).";
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

        private bool VerificarCampoPreRequisitosAsignatura(string preRequisitosAsignatura)
        {
            bool verificarC = false;
            verificarC = FachadaSIFIET.VerificarCampoPreRequisitosAsignatura(preRequisitosAsignatura);
            return verificarC;
        }

        private bool VerificarCampoCoRequisitosAsignatura(string coRequisitosAsignatura)
        {
            bool verificarC = false;
            verificarC = FachadaSIFIET.VerificarCampoCoRequisitosAsignatura(coRequisitosAsignatura);
            return verificarC;
        }

        private bool VerificarExistenciaAsignatura(string nombreAsignatura, string codigoAsignatura)
        {
            bool verificarC = false;
            verificarC = FachadaSIFIET.VerificarExistenciaAsignatura(nombreAsignatura, codigoAsignatura);
            return verificarC;
        }

        private string ObtenerIdPlanEstudios(string nombre)
        {
            return FachadaSIFIET.ObtenerIdPlanEstudios(nombre);
        }

        public ActionResult EnviarDatos()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = (DataSet)Session["DatosSession"];
                string filePath = Server.MapPath(@"~\Uploads") + "\\file.txt";
                string IdPlanEstudios;
                StreamWriter wr = new StreamWriter(filePath);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    IdPlanEstudios = ObtenerIdPlanEstudios(row[0].ToString());
                    wr.WriteLine(IdPlanEstudios + "," + row[1] + "," + row[2] + "," + row[3] + "," + row[4] +
                                 "," + row[5] + "," + row[6] + "," + row[7] + "," + row[8] + "," +
                                 row[9] + "," + row[10]);
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
