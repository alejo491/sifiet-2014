using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Datos.Modelo;
using System.Data.Entity;
using System.Data;


namespace SIFIET.GestionProgramas.Dominio.Servicios
{
    class ServicioProgramas
    {

        public static PROGRAMA ConsultarProgramaAcademico(decimal idPrograma)
        {
            var db = new GestionProgramasEntities();
            return db.PROGRAMAs.Find(idPrograma);
        }



        public static List<PROGRAMA> ConsultarProgramasAcademicos(string estado, string campo, string busqueda)
        {
            var db = new GestionProgramasEntities();
            var programas = from m in db.PROGRAMAs
                            select m;

            programas = programas.Where(s => s.ESTADOPROGRAMA.Contains(estado));

            if (!String.IsNullOrEmpty(campo) && !String.IsNullOrEmpty(busqueda))
            {
                switch (campo)
                {
                    case "Nombre":
                        programas = programas.Where(s => s.NOMBREPROGRAMA.ToUpper().Contains(busqueda.ToUpper()));
                        break;
                    case "Facultad":
                        programas = programas.Where(s => s.FACULTAD.NOMBREFACULTAD.ToUpper().Contains(busqueda.ToUpper()));
                        break;
                    default:
                        break;
                }


            }
            return programas.ToList();
        }

        public static List<PROGRAMA> ConsultarProgramasAcademicos()
        {
            var db = new GestionProgramasEntities();
            var programas = from m in db.PROGRAMAs
                            select m;
            return programas.ToList();
        }

        public static bool RegistrarProgramaAcademico(PROGRAMA objPrograma)
        {

            var db = new GestionProgramasEntities();
            try
            {
                db.PROGRAMAs.Add(objPrograma);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EditarProgramaAcademico(PROGRAMA objPrograma)
        {
            var db = new GestionProgramasEntities();
            try
            {
                db.Entry(objPrograma).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EliminarProgramaAcademico(decimal idPrograma)
        {
            var db = new GestionProgramasEntities();
            try
            {
                PROGRAMA objPrograma = db.PROGRAMAs.Find(idPrograma);
                objPrograma.ESTADOPROGRAMA = "Inactivo";
                db.Entry(objPrograma).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CargarInformacion(DataSet datosExcel)
        {
            var db = new GestionProgramasEntities();
            if (datosExcel != null)
            {
                try
                {
                    for (int i = 0; i < datosExcel.Tables[0].Rows.Count; i++)
                    {
                        PROGRAMA programa = new PROGRAMA();
                        programa.IDENTIFICADORFACULTAD = decimal.Parse(datosExcel.Tables[0].Rows[i][0].ToString());
                        programa.NOMBREPROGRAMA = datosExcel.Tables[0].Rows[i][1].ToString();
                        programa.DESCRIPCIONPROGRAMA = datosExcel.Tables[0].Rows[i][2].ToString();
                        programa.JORNADAPROGRAMA = datosExcel.Tables[0].Rows[i][3].ToString();
                        programa.DURACIONPROGRAMA = decimal.Parse(datosExcel.Tables[0].Rows[i][4].ToString());
                        programa.ADMISIONPROGRAMA = datosExcel.Tables[0].Rows[i][5].ToString();
                        programa.CODIGOSNIESPROGRAMA = decimal.Parse(datosExcel.Tables[0].Rows[i][6].ToString());
                        programa.PERIODODURACIONPROGRAMA = datosExcel.Tables[0].Rows[i][7].ToString();
                        programa.ESTADOPROGRAMA = datosExcel.Tables[0].Rows[i][8].ToString();
                        programa.MODALIDADPROGRAMA = datosExcel.Tables[0].Rows[i][9].ToString();
                        db.PROGRAMAs.Add(programa);
                        db.SaveChanges();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }


    }
}
