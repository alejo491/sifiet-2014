﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using SIFIET.Aplicacion;
﻿using SIFIET.GestionInfraestructura.Datos.Modelo;
﻿using CURSO = SIFIET.GestionProgramas.Datos.Modelo.CURSO;

namespace SIFIET.Presentacion.Controllers
{
    public class CursosController : Controller
    {        
        [Authorize(Roles = "Cursos")]
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
                if (idCurso == 1)
                    return View(FachadaSIFIET.ConsultarCursos(decimal.Parse(nombreCurso), ""));
                if (idCurso == 2)
                    return View(FachadaSIFIET.ConsultarCursos(0, nombreCurso));
                else
                    return View(FachadaSIFIET.ConsultarCursos(0, nombreCurso));
            }
            catch (Exception)
            {

                return View(FachadaSIFIET.ConsultarCursos(0, ""));
            }

        }       
        [Authorize(Roles = "Cursos")]
        public ActionResult VisualizarCurso(decimal idCurso)
        {
            return View(FachadaSIFIET.VisualizarCurso(idCurso));
        }
        //[Authorize(Roles = "Cursos")]
        public ActionResult RegistrarCurso()
        {
            var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0,"","Activo");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NOMBRESUSUARIO");
            ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cursos")]
        public ActionResult RegistrarCurso(CURSO oCurso)
        {
            //try
            //{
                var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0,"","Activo");
                var listaDocentes = FachadaSIFIET.ConsultarDocentes();
                ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
                ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NOMBRESUSUARIO");
                if (!ModelState.IsValid) return View(oCurso);
                var idCurso = FachadaSIFIET.RegistrarCurso(oCurso);
                if (idCurso > 0)
                {
                    TempData["ResultadoOperacion"] = "Curso Agregado con Exito";
                    oCurso = FachadaSIFIET.VisualizarCurso(idCurso);
                    return RedirectToAction("RegistrarHorario", oCurso);
                }
                TempData["ResultadoOperacion"] = "Fallo al Agregar el Curso";
                return RedirectToAction("Index");
           // }
            //catch
            //{
                //return View(oCurso);
            //}
        }
       // [Authorize(Roles = "Cursos")]
        public ActionResult ModificarCurso(decimal idCurso)
        {
            var oCurso = FachadaSIFIET.VisualizarCurso(idCurso);
            //Consultar Asignaturas estaba con dos argumentos
            var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0,"","Activo");
            var listaDocentes = FachadaSIFIET.ConsultarDocentes();
            ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
            ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NOMBRESUSUARIO");
            return View(oCurso);
        }

        [HttpPost]
        [Authorize(Roles = "Cursos")]
        public ActionResult ModificarCurso(CURSO oCurso)
        {
            try
            {
                var listaAsignaturas = FachadaSIFIET.ConsultarAsignaturas(0, "", "Activo");
                var listaDocentes = FachadaSIFIET.ConsultarDocentes();
                ViewBag.ListaAsignaturas = new SelectList(listaAsignaturas, "IDENTIFICADORASIGNATURA", "NOMBREASIGNATURA");
                ViewBag.ListaDocentes = new SelectList(listaDocentes, "IDENTIFICADORUSUARIO", "NOMBRESUSUARIO");
                
                if (!ModelState.IsValid) return View(oCurso);
                var resultado = FachadaSIFIET.ModificarCurso(oCurso);

                if (resultado)
                {
                    TempData["ResultadoOperacion"] = "Curso Modificado con Exito";
                    oCurso = FachadaSIFIET.VisualizarCurso(oCurso.IDENTIFICADORCURSO);
                    return RedirectToAction("ModificarHorario", oCurso);
                }
                else
                {
                    TempData["ResultadoOperacion"] = "Fallo al Modificar el Curso";
                    ViewBag.Mensaje = "false";
                    return RedirectToAction("Index");
                }                    
            }
            catch
            {
                return View(oCurso);
            }
        }
        [Authorize(Roles = "Cursos")]
        public ActionResult ModificarHorario(CURSO oCurso)
        {
            ViewBag.ListaSalones = FachadaSIFIET.ConsultarRecursos(0, "", "Activo");
            ViewBag.Horario = FachadaSIFIET.ObtenerHorarioCurso(oCurso.IDENTIFICADORCURSO);            
            return View(oCurso);
        }
       // [Authorize(Roles = "Cursos")]
        public ActionResult EliminarCurso(decimal idCurso)
        {
            var resultado = FachadaSIFIET.EliminarCurso(idCurso);
            if (resultado)
                TempData["ResultadoOperacion"] = "Curso Eliminado con Exito";
            else
                TempData["ResultadoOperacion"] = "Fallo al Eliminar el Curso";
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("EliminarCurso")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cursos")]
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

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ObtenerListaDias(string id)
        {
            var lstFranjaDia = FachadaSIFIET.ConsultarFranjaHorariaDisponible(id.Trim());
            var myData = lstFranjaDia.Select(a => new SelectListItem()
            {
                Value = a,
                Text = a
            });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ObtenerListaHoraInicio(string dia, string idSalon)
        {
            var lstFranjaDia = FachadaSIFIET.ConsultarFranjaHorariaDisponible(idSalon.Trim(),dia);
            var myData = lstFranjaDia.Select(a => new SelectListItem()
            {
                Value = a,
                Text = a
            });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ObtenerListaHoraFin(string dia, string idSalon, string horaInicio)
        {
            var lstFranjaDia = FachadaSIFIET.ConsultarFranjaHorariaDisponible(idSalon.Trim(),dia,horaInicio);
            var myData = lstFranjaDia.Select(a => new SelectListItem()
            {
                Value = a,
                Text = a
            });
            return Json(myData, JsonRequestBehavior.AllowGet);
        }
       // [Authorize(Roles = "Cursos")]
        public ActionResult RegistrarHorario(CURSO oCurso)
        {
            ViewBag.ListaSalones = FachadaSIFIET.ConsultarRecursos(0, "","Activo");
            ViewBag.Horario = FachadaSIFIET.ObtenerHorarioCurso(oCurso.IDENTIFICADORCURSO);
           // ViewBag.ListaDias = ViewBag.ListaFrajaDia = FachadaSIFIET.ConsultarFranjaHoraria();
            //ViewBag.ListaHoraInicio = FachadaSIFIET.ConsultarSalones(0, "", "Activo");
            //ViewBag.ListaHoraFin = FachadaSIFIET.ConsultarSalones(0, "", "Activo");            
            return View(oCurso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cursos")]
        public ActionResult RegistrarHorario(FormCollection datos)
        {

            ViewBag.ListaSalones = FachadaSIFIET.ConsultarRecursos(0, "", "Activo");

            try
            {
                FRANJA_HORARIA franja =new FRANJA_HORARIA()
                {
                    IDENTIFICADORCURSO = decimal.Parse(datos["idCurso"]),
                    IDENTIFICADORRECURSO = decimal.Parse(datos["IDENTIFICADORRECURSO"]),
                    HORAFINFRANJA = datos["HORAFINFRANJA"],
                    HORAINICIOFRANJA = datos["HORAINICIOFRANJA"],
                    DIAFRANJA = datos["DIAFRANJA"],
                    ESTADOFRANJA = "Disponible"

                };
                if (comprobarFranja(franja))
                {
                    FachadaSIFIET.RegistrarFranjaHoraria(franja, decimal.Parse(datos["idCurso"]));
                    TempData["Mensaje"] = "Horario Agregado con Éxito";
                }
                else
                {
                    TempData["Mensaje"] = "Todos los Campos Son Requeridos, Intentelo nuevamente";
                }
            }
            catch (Exception)
            {
                TempData["Mensaje"] = "Error al Agregar el Horario";
                
            }



            ViewBag.Horario = FachadaSIFIET.ObtenerHorarioCurso(decimal.Parse(datos["idCurso"]));
            ViewData["idCurso"] = datos["idCurso"];
            return View(FachadaSIFIET.VisualizarCurso(decimal.Parse(datos["idCurso"])));
        }

        private bool comprobarFranja(FRANJA_HORARIA oFranjaHoraria)
        {
            var ban = true;

            if (oFranjaHoraria.IDENTIFICADORRECURSO == 0)
            {
                ban = false;
            }
            if (oFranjaHoraria.DIAFRANJA.Trim().Equals("Seleccionar"))
            {
                ban = false;
            }
            if (oFranjaHoraria.HORAINICIOFRANJA.Trim().Equals("Seleccionar"))
            {
                ban = false;
            }
            if (oFranjaHoraria.HORAFINFRANJA.Trim().Equals("Seleccionar"))
            {
                ban = false;
            }
            return ban;
        }
       // [Authorize(Roles = "Cursos")]
        public ActionResult EliminarHorario(int idCurso,int idHorario)
        {
            ViewBag.Horario = FachadaSIFIET.ObtenerHorarioCurso(idCurso);
            ViewData["idCurso"] = idCurso;

            TempData["Mensaje"] = "Horario Eliminado con Éxito";
            FachadaSIFIET.EliminarFranjaHoraria(idCurso, idHorario);
            return RedirectToAction("RegistrarHorario", FachadaSIFIET.VisualizarCurso(idCurso));
        }        
        
        // Lectura del archivo Excel
        [Authorize(Roles = "Cursos")]
        public ActionResult CargarArchivo()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "Cursos")]
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
                TempData["ArchivoSession"] = "El Archivo que se intentó cargar no contiene un formato valido";
                return RedirectToAction("CargarArchivo");
            }
            return RedirectToAction("CargarInformacion");
        }
        //[Authorize(Roles = "Cursos")]
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
            int numTupla = 1;
            bool validar;
            List<int> validadorC = new List<int>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                validar = VerificarCampoDocente(row[1].ToString(), row[2].ToString());
                if (validar)
                {
                    if (!row[1].ToString().Equals("") || !row[2].ToString().Equals(""))
                    {
                        validadorC.Add(1);
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "Los campos Nombre y Apellido de docente en la tupla " + numTupla +
                                            " parecen estar vacios.";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El Docente " + row[1] + " " + row[2] + " de la tupla " + numTupla +
                                            " no fue encontrado.";
                }

                validar = VerificarCampoAsignatura(row[0].ToString());
                if (validar)
                {
                    if (!row[0].ToString().Equals(""))
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
                    erroresTupla = erroresTupla + "La Asignatura " + row[0] + " de la tupla " + numTupla +
                                            " no fue encontrado.";
                }

                validar = VerificarExistenciaCurso(row[3].ToString());
                if (!validar)
                {
                    if (row[3].ToString().Length < 120)
                    {
                        if (!row[3].ToString().Equals(""))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "El campo Nombre Curso de la tupla " + numTupla +
                                           " parece estar vacio.";
                        }
                    }
                    else
                    {
                        validadorC.Add(0);
                        erroresTupla = erroresTupla + "El campo NombreCurso de la tupla " + numTupla +
                                       " supera la cantidad de caracteres permitidos (120).";
                    }
                }
                else
                {
                    validadorC.Add(0);
                    erroresTupla = erroresTupla + "El Curso " + row[3] + " de la tupla " + numTupla +
                                        " ya existe";
                }
                if (row[4].ToString().Length < 30)
                {
                    if (!row[4].ToString().Equals(""))
                    {
                        if (row[4].ToString().Equals("Activo") || row[4].ToString().Equals("Inactivo") || row[4].ToString().Equals("activo") || row[4].ToString().Equals("inactivo"))
                        {
                            validadorC.Add(1);
                        }
                        else
                        {
                            validadorC.Add(0);
                            erroresTupla = erroresTupla + "El Estado " + row[4] + " de la tupla " + numTupla +
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
                    erroresTupla = erroresTupla + "El campo Estado de la tupla " + numTupla +
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

        private bool VerificarExistenciaCurso(string nombreCurso)
        {
            bool verificarC = false;
            verificarC = FachadaSIFIET.VerificarExistenciaCurso(nombreCurso);
            return verificarC;
        }

        private bool VerificarCampoAsignatura(string nombreAsignatura)
        {
            bool verificarC = false;
            verificarC = FachadaSIFIET.VerificarCampoAsignatura(nombreAsignatura);
            return verificarC;
        }

        public bool VerificarCampoDocente(string nombreDocente, string apellidoDocente)
        {
            bool verificarC = false;
            verificarC = FachadaSIFIET.VerificarCampoDocente(nombreDocente, apellidoDocente);
            return verificarC;
        }

        public string ObtenerIdAsignatura(string nombre)
        {
            return FachadaSIFIET.ObtenerIdAsignatura(nombre);
        }

        public string ObtenerIdUsuario(string nombreDocente, string apellidoDocente)
        {
            return FachadaSIFIET.ObtenerIdUsuario(nombreDocente, apellidoDocente);
        }
        //[Authorize(Roles = "Cursos")]
        public ActionResult EnviarDatos()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = (DataSet)Session["DatosSession"];
                string filePath = Server.MapPath(@"~\Uploads") + "\\file.txt";
                string IdAsignatura, IdUsuario;
                StreamWriter wr =
                    new StreamWriter(filePath);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    IdAsignatura = ObtenerIdAsignatura(row[0].ToString());
                    IdUsuario = ObtenerIdUsuario(row[1].ToString(), row[2].ToString());
                    wr.WriteLine(IdAsignatura + "," + IdUsuario + "," + row[3] + "," + row[4]);
                }

                wr.Dispose();

                Session.Remove("DatosSession");
                bool retorno =
                    FachadaSIFIET.CargarInformacionCurso(filePath);
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