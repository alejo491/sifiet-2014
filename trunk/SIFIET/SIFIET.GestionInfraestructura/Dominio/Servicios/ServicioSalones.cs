using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Datos.Modelo;

namespace SIFIET.GestionInfraestructura.Dominio.Servicios
{
    static class ServicioSalones
    {
        public static List<SALON> ConsultarSalones(decimal idSalon,string nomSalon)
        {
        try
          {
                var db = new GestionInfraestructuraEntities();
                var lista = new List<SALON>();
                if (String.IsNullOrEmpty(nomSalon) & idSalon == 0)
                {
                    lista = (from e in db.SALONs
                             select e).ToList();
                    return lista;
                }
                else
                {
                    if (idSalon != 0)
                    {
                        lista = (from e in db.SALONs
                            where
                                (e.IDENTIFICADORSALON == idSalon)
                            select e).ToList();
                        return lista;
                    }
                    else
                    {
                        lista = (from e in db.SALONs
                                 where
                                     (e.NOMBRESALON.ToLower().Contains(nomSalon.ToLower()))
                                 select e).ToList();
                        return lista;
                    }

                }
           }
           catch (Exception)
           {
               return new List<SALON>();
           }


        }
        public static SALON VisualizarSalon(decimal idSalon)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                var salon = (from e in db.SALONs
                                  where e.IDENTIFICADORSALON == idSalon
                                  select e).FirstOrDefault();
                return salon;

            }
            catch (Exception)
            {
                return null;
            }

        }

        internal static bool RegistrarSalon(SALON nuevoSalon)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                db.SALONs.Add(nuevoSalon);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        internal static bool ModificarSalon(SALON salonModificado)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();

                var salon = (from sal in db.SALONs where sal.IDENTIFICADORSALON == salonModificado.IDENTIFICADORSALON select sal).First();
                {
                    salon.IDENTIFICADORFACULTAD = salonModificado.IDENTIFICADORFACULTAD;
                    salon.NOMBRESALON = salonModificado.NOMBRESALON;
                    salon.ESTADOSALON = salonModificado.ESTADOSALON;
                }
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        internal static bool EliminarSalon(decimal idSalon)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();

                var salon = (from sal in db.SALONs where sal.IDENTIFICADORSALON == idSalon select sal).First();
                db.SALONs.Remove(salon);
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
