using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIFIET.GestionInfraestructura.Datos.Modelo;
using SIFIET.Aplicacion;

namespace SIFIET.Presentacion.Controllers
{
    public class SalonesController : Controller
    {
        public ActionResult Index(decimal? idSalon, string nombreSalon, string estado)
        {
            ViewData["Mensaje"] = Session["varsession"];
            ViewBag.Resultado = TempData["ResultadoOperacion"] as string;
            var identificacion = new SelectListItem() { Value = "1", Text = "Identificador" };
            var nombresalon = new SelectListItem() { Value = "2", Text = "Nombre" };
            var nombreFacultad = new SelectListItem() { Value = "3", Text = "Facultad" };
            var lista = new List<SelectListItem> {identificacion, nombresalon, nombreFacultad};
            ViewBag.campoBusqueda = new SelectList(lista, "value", "text");
                if (idSalon == null | String.IsNullOrEmpty(nombreSalon))
                    return View(FachadaSIFIET.ConsultarSalones(0, nombreSalon,estado));
                else
                {
                    var resultado = FachadaSIFIET.ConsultarSalones((decimal) idSalon, nombreSalon, estado);
                    if (resultado.Count == 0)
                        ViewBag.ResultadoBusqueda = "ListaVacia";
                    return View(resultado);
                }
        }

        public ActionResult VisualizarSalon(decimal idSalon)
        {
            return View(FachadaSIFIET.VisualizarSalon(idSalon));
        }

        public ActionResult RegistrarSalon()
        {
            var listaFacultades = FachadaSIFIET.ConsultarFacultades(0,"");
            ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarSalon(SALON oSalon)
        {
            try
            {
                // TODO: Add insert logic here
                var listaFacultades = FachadaSIFIET.ConsultarFacultades(0,"");
                ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
                if (!ModelState.IsValid) return View(oSalon);
                bool resultado = FachadaSIFIET.RegistrarSalon(oSalon);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Salon Agregado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Agregar el Salon";

                ViewBag.Mensaje = "false";

                return RedirectToAction("Index");
            }
            catch
            {
                return View(oSalon);
            }
        }

        public ActionResult ModificarSalon(decimal idSalon)
        {
            var oSalon = FachadaSIFIET.VisualizarSalon(idSalon) as SALON;
            var listaFacultades = FachadaSIFIET.ConsultarFacultades(0,"");
            ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");
            return View(oSalon);
        }

        [HttpPost]
        public ActionResult ModificarSalon(SALON oSalon)
        {
            try
            {
                var listaFacultades = FachadaSIFIET.ConsultarFacultades(0,"");
                ViewBag.ListaFacultades = new SelectList(listaFacultades, "IDENTIFICADORFACULTAD", "NOMBREFACULTAD");

                if (!ModelState.IsValid) return View(oSalon);
                var resultado = FachadaSIFIET.ModificarSalon(oSalon);

                if (resultado)
                    TempData["ResultadoOperacion"] = "Salon Modificado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Modificar el Salon";
                ViewBag.Mensaje = "false";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(oSalon);
            }
        }
        /*
        public ActionResult EliminarSalon(decimal idSalon)
        {
            return View(FachadaSIFIET.VisualizarSalon(idSalon));
        }


        [HttpPost, ActionName("EliminarSalon")]
        [ValidateAntiForgeryToken]*/
        public ActionResult EliminarSalon(decimal idSalon)
        {
            try
            {
                var resultado = FachadaSIFIET.EliminarSalon(idSalon);
                if (resultado)
                    TempData["ResultadoOperacion"] = "Salon Eliminado con Exito";
                else
                    TempData["ResultadoOperacion"] = "Fallo al Eliminar el Salon";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ResultadoOperacion"] = "Fallo al Eliminar el Salon";
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
            int Ncol = 3;
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
                validar = VerificarCampoFacultad(row[0].ToString());
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
                            erroresTupla = erroresTupla + "El campo Nombre Salon de la tupla " + numTupla +
                                           " parece estar vacio.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El Nombre Salon de la tupla " + numTupla +
                                       " supera la cantidad de caracteres permitidos (120).";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "La Facultad " + row[0] + " de la tupla " + numTupla +
                                        " no fue encontrado.";
                }

                validar = VerificarExistenciaSalon(row[1].ToString());
                if (!validar)
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
                            erroresTupla = erroresTupla + "El campo Nombre Salon de la tupla " + numTupla +
                                           " parece estar vacio.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El Nombre Salon de la tupla " + numTupla +
                                       " supera la cantidad de caracteres permitidos (120).";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El Salon " + row[1] + " de la tupla " + numTupla +
                                        " ya existe.";
                }

                if (row[2].ToString().Length < 30)
                {
                    if (!row[2].ToString().Equals(""))
                    {
                        if (row[2].ToString().Equals("Activo") || row[2].ToString().Equals("Inactivo"))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "El Estado " + row[2] + " de la tupla " + numTupla +
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

        private bool VerificarExistenciaSalon(string nombreSalon)
        {
            bool verificarC = false;
            verificarC = FachadaSIFIET.VerificarExistenciaSalon(nombreSalon);
            return verificarC;
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

        public ActionResult EnviarDatos()
        {
            /*try
            {*/
                DataSet ds = new DataSet();
                ds = (DataSet)Session["DatosSession"];
                string filePath = Server.MapPath(@"~\Uploads") + "\\file.txt";
                string IdFacultad;
                StreamWriter wr = new StreamWriter(filePath);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    IdFacultad = ObtenerIdFacultad(row[0].ToString());
                    wr.WriteLine(IdFacultad + "," + row[1] + "," + row[2]);
                }
                wr.Dispose();

                Session.Remove("DatosSession");
                bool retorno =
                    FachadaSIFIET.CargarInformacionSalon(filePath);
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