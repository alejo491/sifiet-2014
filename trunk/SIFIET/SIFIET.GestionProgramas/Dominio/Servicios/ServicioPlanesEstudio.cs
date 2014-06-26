using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using SIFIET.GestionProgramas.Datos.Modelo;
using System.Diagnostics;

namespace SIFIET.GestionProgramas.Dominio.Servicios
{
    class ServicioPlanesEstudio
    {
        public static PLANESTUDIO ConsultarPlanEstudio(decimal idPlanEstudio)
        {
            var db = new GestionProgramasEntities();
            return db.PLANESTUDIOS.Find(idPlanEstudio);
        }

        public static List<PLANESTUDIO> ConsultarPlanesEstudios(string estado, string campo, string busqueda)
        {
            var db = new GestionProgramasEntities();
            var planesEstudios = from m in db.PLANESTUDIOS
                                 select m;

            planesEstudios = planesEstudios.Where(s => s.ESTADOPLANESTUDIOS.Contains(estado));

            if (!String.IsNullOrEmpty(campo) && !String.IsNullOrEmpty(busqueda))
            {
                switch (campo)
                {
                    case "Nombre":
                        planesEstudios = planesEstudios.Where(s => s.NOMBREPLANESTUDIOS.ToUpper().Contains(busqueda.ToUpper()));
                        break;
                    case "Programa":
                        planesEstudios = planesEstudios.Where(s => s.PROGRAMA.NOMBREPROGRAMA.ToUpper().Contains(busqueda.ToUpper()));
                        break;
                    default:
                        break;
                }


            }
            return planesEstudios.ToList();
        }

        public static bool RegistrarPlanEstudio(PLANESTUDIO objPlanEstudio)
        {
            var db = new GestionProgramasEntities();
            try
            {
                db.PLANESTUDIOS.Add(objPlanEstudio);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EditarPlanEstudio(PLANESTUDIO objPlanEstudio)
        {
            var db = new GestionProgramasEntities();
            try
            {
                db.Entry(objPlanEstudio).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EliminarPlanEstudio(decimal idPlanEstudio)
        {
            var db = new GestionProgramasEntities();
            try
            {
                PLANESTUDIO objPlanEstudio = db.PLANESTUDIOS.Find(idPlanEstudio);
                objPlanEstudio.ESTADOPLANESTUDIOS = "Eliminado";
                objPlanEstudio.operacion = "eliminar";
                db.Entry(objPlanEstudio).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // metodo buscar asignaturas que pertenecen a un plan de estudio
        public static List<ASIGNATURA_PERTENECE_PLAN_ESTU> ConsultarAsignaturasPorPlanEstudio(decimal idPlanEstudio)
        {
            var db = new GestionProgramasEntities();
            var asignaturasPlanEstudio = from m in db.ASIGNATURA_PERTENECE_PLAN_ESTU          
                                         select m;

            asignaturasPlanEstudio = asignaturasPlanEstudio.Where(s => s.IDENTIFICADORPLANESTUDIOS == idPlanEstudio);
            asignaturasPlanEstudio = asignaturasPlanEstudio.OrderBy(s => s.SEMESTRE);
            return asignaturasPlanEstudio.ToList();
                
        }

        // registra una asignatura al plan de estudio
        public static bool RegistrarAsignaturaPlanEstudio(ASIGNATURA_PERTENECE_PLAN_ESTU objAsignaturaPlanEstudio)
        {
            var db = new GestionProgramasEntities();
            try
            {           
                db.ASIGNATURA_PERTENECE_PLAN_ESTU.Add(objAsignaturaPlanEstudio);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // eliminar una asignatura del plan de estudio
        public static bool EliminarAsignaturaPlanEstudio(decimal idPlanEstudio, decimal idAsignatura)
        {
            var db = new GestionProgramasEntities();
            try
            {

                var asignaturasPlanEstudio = (from e in db.ASIGNATURA_PERTENECE_PLAN_ESTU where e.IDENTIFICADORPLANESTUDIOS == idPlanEstudio && e.IDENTIFICADORASIGNATURA == idAsignatura select e).FirstOrDefault();
                if( asignaturasPlanEstudio != null) {
                     db.ASIGNATURA_PERTENECE_PLAN_ESTU.Remove(asignaturasPlanEstudio);
                     db.SaveChanges();
                     return true;
                }
                return false;
                
            }
            catch (Exception)
            {
                return false;
            }
        }


        // metodo hecho para el modulo de asignatura
        public static List<PLANESTUDIO> ConsultarPlanestudios(string palabraBusqueda)
        {
            try
            {
                var db = new GestionProgramasEntities();
                List<PLANESTUDIO> lista = new List<PLANESTUDIO>();
                if (String.IsNullOrEmpty(palabraBusqueda))
                {
                    lista = (from e in db.PLANESTUDIOS select e).ToList();
                    return lista;
                }
                else
                {
                    lista = (from e in db.PLANESTUDIOS
                             where
                                 (e.NOMBREPLANESTUDIOS.Contains(palabraBusqueda) |
                                  e.IDENTIFICADORPLANESTUDIOS == decimal.Parse(palabraBusqueda))
                             select e).ToList();
                    return lista;

                }

            }
            catch (Exception)
            {
                return new List<PLANESTUDIO>();
            }
        }

        public static byte[] PlanEstudiosPDF(decimal idPlan)
        {

            var plan = ConsultarPlanEstudio(idPlan);

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
            titulo.Clear();
            titulo.Add(plan.NOMBREPLANESTUDIOS);
            doc.Add(titulo);
            doc.Add(new Paragraph(" "));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("CODIGO:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(plan.CODIGOPLANESTUDIOS.ToUpper());
            doc.Add(texto);
            doc.Add(new Paragraph("NOMBRE:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(plan.NOMBREPLANESTUDIOS.ToUpper());
            doc.Add(texto);
            doc.Add(new Paragraph("FECHA INICIACION:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(plan.FECHAINICIOPLANESTUDIOS.ToUpper());
            doc.Add(texto);
            doc.Add(new Paragraph("FECHA FINALIZACION:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(plan.FECHAFINPLANESTUDIOS.ToUpper());
            doc.Add(texto);
            doc.Add(new Paragraph("DESCRIPCION:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            texto.Clear();
            texto.Add(plan.DESCRIPCIONPLANESTUDIOS.ToUpper());
            doc.Add(texto);
            doc.Add(new Paragraph("ASIGNATURAS:", new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false))));
            doc.Add(new Paragraph(" "));
            if (plan.ASIGNATURA_PERTENECE_PLAN_ESTU.Count==0)
            {
                
                texto.Clear();
            texto.Add("NINGUNA");
            doc.Add(texto);
            }
            else
            {
                List<ASIGNATURA_PERTENECE_PLAN_ESTU> lista=(from f in plan.ASIGNATURA_PERTENECE_PLAN_ESTU
                                                                orderby f.SEMESTRE
                                                            select f).ToList();
                PdfPTable tabla = new PdfPTable(3);
                PdfPCell cel1 = new PdfPCell(new Phrase("Semestre"));
                
                PdfPCell cel2 = new PdfPCell(new Phrase("Codigo"));
                PdfPCell cel3 = new PdfPCell(new Phrase("Nombre"));
                cel1.BackgroundColor = new BaseColor(138,138,138);
                
                
                cel2.BackgroundColor = new BaseColor(138,138,138);
                cel3.BackgroundColor = new BaseColor(138,138,138);

                tabla.AddCell(cel1);
                
                tabla.AddCell(cel2);
                tabla.AddCell(cel3);
                
                
                foreach (var x in lista)
                {
                    
                        PdfPCell semestre =new PdfPCell();
                        semestre.HorizontalAlignment = 1;
                        semestre.VerticalAlignment = 1;
                        
                        semestre.AddElement(new Phrase(x.SEMESTRE.ToString()));
                        tabla.AddCell(semestre);
                        tabla.AddCell(x.ASIGNATURA.CODIGOASIGNATURA);
                        tabla.AddCell(x.ASIGNATURA.NOMBREASIGNATURA);
                        
                       
                        
                    
                }

                doc.Add(tabla);
            }

            doc.Close();



            buf = new byte[pdfTemp.Position];
            pdfTemp.Position = 0;
            pdfTemp.Read(buf, 0, buf.Length);

            return buf;

        }
    }
}
