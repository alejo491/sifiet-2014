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
                if (String.IsNullOrEmpty(nomSalon))
                {
                    lista = (from e in db.SALONs
                             select e).ToList();
                    return lista;
                }
                else
                {
                    if (idSalon == 1)
                    {
                        var id = decimal.Parse(nomSalon);
                        lista = (from e in db.SALONs
                            where
                                (e.IDENTIFICADORSALON == id)
                            select e).ToList();
                        return lista;
                    }
                    if (idSalon == 2)
                    {
                        lista = (from e in db.SALONs
                                 where
                                     (e.NOMBRESALON.ToLower().Contains(nomSalon.ToLower()))
                                 select e).ToList();
                        return lista;
                    }
                    else
                    {
                        lista = (from s in db.SALONs join f in db.FACULTADs on s.IDENTIFICADORFACULTAD equals f.IDENTIFICADORFACULTAD
                                 where
                                     (f.NOMBREFACULTAD.ToLower().Contains(nomSalon.ToLower()))
                                 select s).ToList();
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
                var oSalon = (from salon in db.SALONs
                    where
                        nuevoSalon.NOMBRESALON == salon.NOMBRESALON &&
                        nuevoSalon.IDENTIFICADORFACULTAD == salon.IDENTIFICADORFACULTAD
                    select salon).FirstOrDefault();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        internal static bool CrearFranjasHorarias(SALON oSalon)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                for (int d = 1; d <= 2; d++)
                {
                    for (int i = 6; i <= 8; i++)
                    {
                        var oFranjaHoraria = new FRANJA_HORARIA
                        {
                            ESTADOFRANJA = "Disponible",
                            HORAINICIOFRANJA = i.ToString().Trim(),
                            HORAFINFRANJA = (i + 1).ToString().Trim(),
                            IDENTIFICADORSALON = oSalon.IDENTIFICADORSALON,
                            DIAFRANJA = d.ToString().Trim(),
                        };
                        db.FRANJA_HORARIA.Add(oFranjaHoraria);
                    }
                }
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
                {
                    salon.ESTADOSALON = "Inactivo";
                }
                //db.SALONs.Remove(salon);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public static List<FRANJA_HORARIA> ObtenarHorarioCurso(decimal idCurso)
        {
            var db = new GestionInfraestructuraEntities();
            List<FRANJA_HORARIA> lista = (from e in db.FRANJA_HORARIA
                     
                         
                     select e).ToList();
            return lista;
        }
    }
}
