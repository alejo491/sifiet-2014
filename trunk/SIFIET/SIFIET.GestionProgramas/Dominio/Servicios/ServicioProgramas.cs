using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Datos.Modelo;
using System.Data.Entity;
using System.Data;
using System.Diagnostics;


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
                Debug.WriteLine(objPrograma.CODIGOSNIESPROGRAMA);
                objPrograma.ESTADOPROGRAMA = "Inactivo";
                objPrograma.operacion = "eliminar";
                db.Entry(objPrograma).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*public static bool CargarInformacion(DataSet datosExcel)
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
        }*/

        internal static bool CargarInformacion(string archivo)
        {
            String linea;
            StreamReader f = new StreamReader(archivo);
            try
            {
                while ((linea = f.ReadLine()) != null)
                {
                    string[] campos = linea.Split(',');
                    var db = new GestionProgramasEntities();

                    var prog = new PROGRAMA();
                    {
                        prog.IDENTIFICADORFACULTAD = decimal.Parse(campos[0]);
                        prog.NOMBREPROGRAMA = campos[1];
                        prog.DESCRIPCIONPROGRAMA = campos[2];
                        prog.JORNADAPROGRAMA = campos[3];
                        prog.DURACIONPROGRAMA = decimal.Parse(campos[4]);
                        prog.ADMISIONPROGRAMA = campos[5];
                        prog.CODIGOSNIESPROGRAMA = decimal.Parse(campos[6]);
                        prog.PERIODODURACIONPROGRAMA = campos[7];
                        prog.ESTADOPROGRAMA = campos[8];
                        prog.MODALIDADPROGRAMA = campos[9];
                    }
                    db.PROGRAMAs.Add(prog);
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

        public static bool VerificarExistenciaPrograma(string nombrePrograma)
        {
            var db = new GestionProgramasEntities();
            var curso = (from e in db.PROGRAMAs
                         where e.NOMBREPROGRAMA.ToLower() == nombrePrograma.ToLower()
                         select e).FirstOrDefault();
            if (curso != null)
            {
                return true;
            }
            return false;
        }

        public static string ObtenerIdFacultad(string nombreFacultad)
        {
            var db = new GestionProgramasEntities();
            var asig = (from e in db.FACULTADs
                        where e.NOMBREFACULTAD.ToLower() == nombreFacultad.ToLower()
                        select e).FirstOrDefault();
            if (asig != null)
            {
                return asig.IDENTIFICADORFACULTAD.ToString();
            }
            return "";
        }

        public static bool VerificarExistenciaFacultad(string nombreFacultad)
        {
            var db = new GestionProgramasEntities();
            var curso = (from e in db.FACULTADs
                         where e.NOMBREFACULTAD.ToLower() == nombreFacultad.ToLower()
                         select e).FirstOrDefault();
            if (curso != null)
            {
                return true;
            }
            return false;
        }
    }
}
