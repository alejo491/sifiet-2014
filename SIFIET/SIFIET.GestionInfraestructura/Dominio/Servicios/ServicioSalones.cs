using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Datos.Modelo;

namespace SIFIET.GestionInfraestructura.Dominio.Servicios
{
    static class ServicioSalones
    {
        /*
        public static List<SALON> ConsultarSalones(decimal idSalon, string nomSalon, string estado)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                var lista = new List<SALON>();
                if (String.IsNullOrEmpty(nomSalon))
                {
                    if (String.IsNullOrEmpty(estado))
                        lista = (from e in db.SALONs select e).ToList();
                    else
                        lista = (from e in db.SALONs where e.ESTADOSALON.Equals(estado) select e).ToList();
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
                        lista = (from s in db.SALONs
                                 join f in db.FACULTADs on s.IDENTIFICADORFACULTAD equals f.IDENTIFICADORFACULTAD
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
                                  nuevoSalon.NOMBRESALON.ToLower().Trim().Equals(salon.NOMBRESALON.ToLower().Trim()) &&
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
                            ESTADOFRANJA = "Activo",
                            HORAINICIOFRANJA = i.ToString().Trim(),
                            HORAFINFRANJA = (i + 1).ToString().Trim(),
                            IDENTIFICADORSALON = oSalon.IDENTIFICADORSALON,
                            DIAFRANJA = d.ToString().Trim(),
                        };
                        db.Database.ExecuteSqlCommand("Insert into FRANJA_HORARIA (IDENTIFICADORSALON, HORAINICIOFRANJA,HORAFINFRANJA,DIAFRANJA,ESTADOFRANJA) values (" +
                                              "'" + oFranjaHoraria.IDENTIFICADORSALON + "'," +
                                              "'" + oFranjaHoraria.HORAINICIOFRANJA + "'," +
                                              "'" + oFranjaHoraria.HORAFINFRANJA + "'," +
                                              "'" + oFranjaHoraria.DIAFRANJA + "'," +
                                              "'" + oFranjaHoraria.ESTADOFRANJA + "')");
                        db.SaveChanges();
                    }
                }
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

                var salon = db.SALONs.Find(idSalon);
                if (BorrarFranjasHorarias(idSalon))
                {
                    db.SALONs.Remove(salon);
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

        private static bool BorrarFranjasHorarias(decimal idSalon)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                var lstFranjas =
                    (from franja in db.FRANJA_HORARIA where franja.IDENTIFICADORSALON == idSalon select franja).ToList();
                foreach (var franja in lstFranjas)
                {
                    db.FRANJA_HORARIA.Remove(franja);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static List<FRANJA_HORARIA> ObtenerHorarioCurso(decimal idCurso)
        {
            var db = new GestionInfraestructuraEntities();
            List<FRANJA_HORARIA> lista = (from e in db.CURSOes
                                          where e.IDENTIFICADORCURSO == idCurso

                                          select e).FirstOrDefault().FRANJA_HORARIA.ToList();
            return lista;
        }

        public static bool VerificarExistenciaSalon(string nombreSalon, decimal IdFacultad)
        {
            var db = new GestionInfraestructuraEntities();
            var curso = (from e in db.SALONs
                         where e.NOMBRESALON.ToLower() == nombreSalon.ToLower()
                         && e.IDENTIFICADORFACULTAD== IdFacultad
                         select e).FirstOrDefault();
            if (curso != null)
            {
                return true;
            }
            return false;
        }

        public static bool VerificarCampoFacultad(string nombreFacultad)
        {
            var db = new GestionInfraestructuraEntities();
            var asig = (from e in db.FACULTADs
                        where e.NOMBREFACULTAD.ToLower() == nombreFacultad.ToLower()
                        select e).FirstOrDefault();
            if (asig != null)
            {
                return true;
            }
            return false;
        }

        public static string ObtenerIdFacultad(string nombreFacultad)
        {
            var db = new GestionInfraestructuraEntities();
            var asig = (from e in db.FACULTADs
                        where e.NOMBREFACULTAD.ToLower() == nombreFacultad.ToLower()
                        select e).FirstOrDefault();
            if (asig != null)
            {
                return asig.IDENTIFICADORFACULTAD.ToString();
            }
            return "";
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
                var db = new GestionInfraestructuraEntities();

                var salon = new SALON()
                {
                    IDENTIFICADORFACULTAD = decimal.Parse(campos[0]),
                    NOMBRESALON = campos[1],
                    ESTADOSALON = campos[2]
                };
                db.SALONs.Add(salon);
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
         * */
    }
}
