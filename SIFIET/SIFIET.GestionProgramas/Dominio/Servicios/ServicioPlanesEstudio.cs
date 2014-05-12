using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using SIFIET.GestionProgramas.Datos.Modelo;

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
                objPlanEstudio.ESTADOPLANESTUDIOS = "Inactivo";
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
    }
}
