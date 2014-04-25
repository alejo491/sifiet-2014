using System;
using System.Collections.Generic;
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

        internal static bool RegistrarCurso(CURSO nuevoCurso)
        {
            try
            {
                var db = new GestionProgramasEntities();
                db.CURSOes.Add(nuevoCurso);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
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
    }
    
}