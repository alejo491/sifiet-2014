using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Datos.Modelo;

namespace SIFIET.GestionProgramas.Dominio.Servicios
{
    static class ServicioCursos
    {
        public static List<CURSO> ConsultarCursos(decimal idCurso, string nomCurso)
        {
          try
          {
                var db = new GestionProgramasEntities();
                var lista = new List<CURSO>();
                if (String.IsNullOrEmpty(nomCurso) & idCurso == 0)
                {
                    lista = (from e in db.CURSOes
                             select e).ToList();
                    return lista;
                }
                else
                {
                    if (idCurso != 0)
                    {
                        lista = (from e in db.CURSOes
                            where
                                (e.IDENTIFICADORCURSO == idCurso)
                            select e).ToList();
                        return lista;
                    }
                    else
                    {
                        lista = (from e in db.CURSOes
                                 where
                                     (e.NOMBRECURSO.ToLower().Contains(nomCurso.ToLower()))
                                 select e).ToList();
                        return lista;
                    }

                }
           }
           catch (Exception)
           {
               return new List<CURSO>();
           }


        }
        public static CURSO VisualizarCurso(decimal idCurso)
        {
            try
            {
                var db = new GestionProgramasEntities();
                var curso = (from e in db.CURSOes
                                  where e.IDENTIFICADORCURSO == idCurso
                                  select e).FirstOrDefault();
                return curso;

            }
            catch (Exception)
            {
                return null;
            }

        }

        internal static decimal RegistrarCurso(CURSO nuevoCurso)
        {
            try
            {
                var db = new GestionProgramasEntities();
                db.CURSOes.Add(nuevoCurso);
                db.SaveChanges();
                var oCurso = (from curso in db.CURSOes where curso.IDENTIFICADORASIGNATURA==nuevoCurso.IDENTIFICADORASIGNATURA && curso.NOMBRECURSO.ToLower().Trim().Equals(nuevoCurso.NOMBRECURSO.ToLower().Trim()) && curso.IDENTIFICADORUSUARIO==nuevoCurso.IDENTIFICADORUSUARIO select curso).FirstOrDefault();
                if (oCurso != null)
                    return oCurso.IDENTIFICADORCURSO;
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        internal static bool ModificarCurso(CURSO cursoModificado)
        {
            try
            {
                var db = new GestionProgramasEntities();

                var curso = (from curs in db.CURSOes where curs.IDENTIFICADORCURSO == cursoModificado.IDENTIFICADORCURSO select curs).First();
                {
                    curso.IDENTIFICADORASIGNATURA = cursoModificado.IDENTIFICADORASIGNATURA;
                    curso.IDENTIFICADORUSUARIO = cursoModificado.IDENTIFICADORUSUARIO;
                    curso.NOMBRECURSO = cursoModificado.NOMBRECURSO;
                    curso.ESTADOCURSO = cursoModificado.ESTADOCURSO;
                }
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        internal static bool EliminarCurso(decimal idCurso)
        {
            try
            {
                var db = new GestionProgramasEntities();

                var curso = (from cur in db.CURSOes where cur.IDENTIFICADORCURSO == idCurso select cur).First();
                db.CURSOes.Remove(curso);
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
                    string[] campos = linea.Split(',');
                    var db = new GestionProgramasEntities();

                    var curso = new CURSO()
                    {
                        IDENTIFICADORASIGNATURA = decimal.Parse(campos[0]),
                        IDENTIFICADORUSUARIO = decimal.Parse(campos[1]),
                        NOMBRECURSO = campos[2],
                        ESTADOCURSO = campos[3]
                    };
                    db.CURSOes.Add(curso);
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

        public static bool VerificarCampoDocente(string nombreDocente, string apellidoDocente)
        {
            var db = new GestionProgramasEntities();
            var asig = (from e in db.DOCENTEs
                        where e.NOMBRESUSUARIO.ToLower() == nombreDocente.ToLower()
                        && e.APELLIDOSUSUARIO.ToLower() == apellidoDocente.ToLower()
                        select e).FirstOrDefault();
            if (asig != null)
            {
                return true;
            }
            return false;
        }

        public static string ObtenerIdAsignatura(string nombre)
        {
            var db = new GestionProgramasEntities();
            var asig = (from e in db.ASIGNATURAs
                        where e.NOMBREASIGNATURA.ToLower() == nombre.ToLower()
                        select e).FirstOrDefault();
            if (asig != null)
            {
                return asig.IDENTIFICADORASIGNATURA.ToString();
            }
            return "";
        }

        public static string ObtenerIdUsuario(string nombreDocente, string apellidoDocente)
        {
            var db = new GestionProgramasEntities();
            var asig = (from e in db.DOCENTEs
                        where e.NOMBRESUSUARIO.ToLower() == nombreDocente.ToLower()
                        && e.APELLIDOSUSUARIO.ToLower() == apellidoDocente.ToLower()
                        select e).FirstOrDefault();
            if (asig != null)
            {
                return asig.IDENTIFICADORUSUARIO.ToString();
            }
            return "";
        }

        public static bool VerificarCampoAsignatura(string nombreAsignatura)
        {
            var db = new GestionProgramasEntities();
            var asig = (from e in db.ASIGNATURAs
                        where e.NOMBREASIGNATURA.ToLower() == nombreAsignatura.ToLower()
                        select e).FirstOrDefault();
            if (asig != null)
            {
                return true;
            }
            return false;
        }

        public static bool VerificarExistenciaCurso(string nombreCurso)
        {
            var db = new GestionProgramasEntities();
            var curso = (from e in db.CURSOes
                         where e.NOMBRECURSO.ToLower() == nombreCurso.ToLower()
                         select e).FirstOrDefault();
            if (curso != null)
            {
                return true;
            }
            return false;
        }
    }
    
}