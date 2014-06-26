using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using SIFIET.GestionProgramas.Datos.Modelo;

namespace SIFIET.GestionProgramas.Dominio.Servicios
{
    static class ServicioAsignaturas
    {
        public static List<ASIGNATURA> ConsultarAsignaturas(decimal idAsignatura, string nombreAsignatura, string estado)
        {
            try
            {
                var db = new GestionProgramasEntities();
                var lista = new List<ASIGNATURA>();               
                if (String.IsNullOrEmpty(nombreAsignatura))
                {
                    if(String.IsNullOrEmpty(estado))
                        lista = (from e in db.ASIGNATURAs where !e.ESTADOASIGNATURA.Equals("Eliminado") select e).ToList();
                    else
                        lista = (from e in db.ASIGNATURAs where e.ESTADOASIGNATURA.Equals(estado) select e).ToList();
                    return lista;
                }
                else
                {
                    if (idAsignatura == 1)
                    {
                        lista = (from e in db.ASIGNATURAs
                                 where (e.CODIGOASIGNATURA.ToLower().Contains(nombreAsignatura.ToLower()))&(e.ESTADOASIGNATURA.Equals(estado))
                                 select e).ToList();
                        return lista;
                    }
                    else
                    {
                        lista = (from e in db.ASIGNATURAs
                                 where (e.NOMBREASIGNATURA.ToLower().Contains(nombreAsignatura.ToLower())) & (e.ESTADOASIGNATURA.Equals(estado))
                                 select e).ToList();
                        return lista;
                    }

                }
            }
            catch (Exception)
            {
                return new List<ASIGNATURA>();
            }


        }
        public static ASIGNATURA VisualizarAsignatura(decimal idAsignatura)
        {
            try
            {
                var db = new GestionProgramasEntities();
                var asignatura = (from e in db.ASIGNATURAs
                                  where e.IDENTIFICADORASIGNATURA == idAsignatura
                                  select e).First();
                return asignatura;

            }
            catch (Exception)
            {
                return null;
            }

        }

        internal static bool RegistrarAsignatura(ASIGNATURA nuevAsignatura)
        {
            try
            {
                var db = new GestionProgramasEntities();

                var asg = new ASIGNATURA();
                {
                    asg.EdicionOmodificacion = "registrar";
                    asg = nuevAsignatura;
                }
                db.ASIGNATURAs.Add(asg);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        internal static bool ModificarAsignatura(ASIGNATURA asignaturaModificada)
        {
            try
            {
                var db = new GestionProgramasEntities();

                var asignatura = (from asig in db.ASIGNATURAs where asig.IDENTIFICADORASIGNATURA == asignaturaModificada.IDENTIFICADORASIGNATURA select asig).First();
                {
                    asignatura.EdicionOmodificacion = "modificacion";
                    asignatura.CODIGOASIGNATURA = asignaturaModificada.CODIGOASIGNATURA;
                    //asignatura.IDENTIFICADORPLANESTUDIOS = asignaturaModificada.IDENTIFICADORPLANESTUDIOS;
                    asignatura.NOMBREASIGNATURA = asignaturaModificada.NOMBREASIGNATURA;
                    asignatura.CORREQUISITOSASIGNATURA = asignaturaModificada.CORREQUISITOSASIGNATURA;
                    asignatura.PREREQUISITOSASIGNATURA = asignaturaModificada.PREREQUISITOSASIGNATURA;
                    //asignatura.SEMESTREASIGNATURA = asignaturaModificada.SEMESTREASIGNATURA;
                    asignatura.CREDITOSASIGNATURA = asignaturaModificada.CREDITOSASIGNATURA;
                    asignatura.MODALIDADASIGNATURA = asignaturaModificada.MODALIDADASIGNATURA;
                    asignatura.CLASIFICACIONASIGNATURA = asignaturaModificada.CLASIFICACIONASIGNATURA;
                    asignatura.ESTADOASIGNATURA = asignaturaModificada.ESTADOASIGNATURA;
                    asignatura.DESCRIPCIONASIGNATURA = asignaturaModificada.DESCRIPCIONASIGNATURA;
                }
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        internal static bool EliminarAsignatura(decimal idAsignatura)
        {
            try
            {
                var db = new GestionProgramasEntities();

                var asg = (from asig in db.ASIGNATURAs where asig.IDENTIFICADORASIGNATURA == idAsignatura select asig).First();
                {
                    asg.EdicionOmodificacion = "modificacion";
                    asg.ESTADOASIGNATURA = "Eliminado";
                }
                //db.ASIGNATURAs.Remove(asg);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        internal static bool CargarInformacion(string archivo)
        {
            String linea;
            StreamReader f = new StreamReader(archivo);
            try
            {
                while ((linea = f.ReadLine()) != null)
                {
                    string[] campos = linea.Split('|');
                    var db = new GestionProgramasEntities();

                    var asg = new ASIGNATURA();
                    {
                        asg.NOMBREASIGNATURA = campos[0];
                        asg.CORREQUISITOSASIGNATURA = campos[1];
                        asg.PREREQUISITOSASIGNATURA = campos[2];
                        asg.CREDITOSASIGNATURA = decimal.Parse(campos[3]);
                        asg.MODALIDADASIGNATURA = campos[4];
                        asg.CLASIFICACIONASIGNATURA = campos[5];
                        asg.ESTADOASIGNATURA = campos[6];
                        asg.DESCRIPCIONASIGNATURA = campos[7];
                        asg.CODIGOASIGNATURA = campos[8];
                        asg.EdicionOmodificacion = "registrar";
                    }
                    db.ASIGNATURAs.Add(asg);
                    db.SaveChanges();
                }
                f.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool VerificarCampoPlanEstudios(string nombrePlanEstudios)
        {
            var db = new GestionProgramasEntities();
            var plan = (from e in db.PLANESTUDIOS
                        where e.NOMBREPLANESTUDIOS.ToLower() == nombrePlanEstudios.ToLower()
                        select e).FirstOrDefault();
            if (plan != null)
            {
                return true;
            }
            return false;
        }

        public static bool VerificarCampoCoRequisitosAsignatura(string coRequisitosAsignatura)
        {
            var db = new GestionProgramasEntities();
            var plan = (from e in db.ASIGNATURAs
                        where e.NOMBREASIGNATURA.ToLower() == coRequisitosAsignatura.ToLower()
                        select e).FirstOrDefault();
            if (plan != null)
            {
                return true;
            }
            return false;
        }

        public static bool VerificarCampoPreRequisitosAsignatura(string preRequisitosAsignatura)
        {
            var db = new GestionProgramasEntities();
            var plan = (from e in db.ASIGNATURAs
                        where e.NOMBREASIGNATURA.ToLower() == preRequisitosAsignatura.ToLower()
                        select e).FirstOrDefault();
            if (plan != null)
            {
                return true;
            }
            return false;
        }

        public static bool VerificarExistenciaAsignatura(string nombreAsignatura, string codigoAsignatura)
        {
            var db = new GestionProgramasEntities();
            var curso = (from e in db.ASIGNATURAs
                         where e.NOMBREASIGNATURA.ToLower() == nombreAsignatura.ToLower()
                         || e.CODIGOASIGNATURA.ToLower() == codigoAsignatura.ToLower()
                         select e).FirstOrDefault();
            if (curso != null)
            {
                return true;
            }
            return false;
        }

        public static byte[] AsignaturaPDF(decimal idAsignatura)
        {

            var asignatura = VisualizarAsignatura(idAsignatura);

            BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);


            byte[] buf = null;
            MemoryStream pdfTemp = new MemoryStream();


            Document doc = new iTextSharp.text.Document();
            PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, pdfTemp);

            writer.CloseStream = false;
            doc.Open();
            var texto = new Paragraph();

            texto.Font = new Font(font);
            texto.IndentationLeft = 100;
            var titulo = new Paragraph("UNIVERSIDAD DEL CAUCA", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false)));
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);
            doc.Add(new Paragraph(" "));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("CODIGO:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(asignatura.CODIGOASIGNATURA.ToUpper());
            doc.Add(texto);
            doc.Add(new Paragraph("NOMBRE:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(asignatura.NOMBREASIGNATURA.ToUpper());
            doc.Add(texto);
            doc.Add(new Paragraph("INTENSIDAD HORARIA:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(asignatura.CREDITOSASIGNATURA.ToString().ToUpper());
            doc.Add(texto);
            doc.Add(new Paragraph("MODALIDAD:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(asignatura.MODALIDADASIGNATURA.ToUpper());
            doc.Add(texto);
            doc.Add(new Paragraph("CLASIFICACION:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(asignatura.CLASIFICACIONASIGNATURA.ToUpper());
            doc.Add(texto);
            
            doc.Add(new Paragraph("CORREQUISITOS:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            if (asignatura.ListaCorrequisitos.Count == 0)
            {
                texto.Clear();
                texto.Add("NINGUNO");
                doc.Add(texto);
            }
            else
            {
                foreach (var x in asignatura.ListaCorrequisitos)
                {
                    texto.Clear();
                    texto.Add(x.ToUpper());
                    doc.Add(texto);
                }
            }

            
            doc.Add(new Paragraph("PREREQUISITOS:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            if (asignatura.ListaPrerequisitos.Count == 0)
            {
                texto.Clear();
                texto.Add("NINGUNO");
                doc.Add(texto);
            }
            else
            {
                foreach (var x in asignatura.ListaPrerequisitos)
                {
                    texto.Clear();
                    texto.Add(x.ToUpper());
                    doc.Add(texto);
                }
            }
           
            doc.Add(new Paragraph("DESCRIPCION:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(asignatura.DESCRIPCIONASIGNATURA.ToUpper());
            doc.Add(texto);
            
            doc.Close();



            buf = new byte[pdfTemp.Position];
            pdfTemp.Position = 0;
            pdfTemp.Read(buf, 0, buf.Length);

            return buf;
        
        }
    }
}
