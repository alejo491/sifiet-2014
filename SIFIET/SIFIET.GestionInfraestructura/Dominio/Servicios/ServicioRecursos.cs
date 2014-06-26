using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Datos.Modelo;

namespace SIFIET.GestionInfraestructura.Dominio.Servicios
{
    class ServicioRecursos
    {
        public static List<RECURSO> ConsultarRecursos(decimal idRecurso, string nomRecurso, string estado)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                var lista = new List<RECURSO>();
                if (String.IsNullOrEmpty(nomRecurso))
                {
                    if (String.IsNullOrEmpty(estado))
                        lista = (from e in db.RECURSOes where !e.ESTADORECURSO.Equals("Eliminado") select e).ToList();
                    else
                        lista = (from e in db.RECURSOes where e.ESTADORECURSO.Equals(estado) select e).ToList();
                    return lista;
                }
                else
                {
                    if (idRecurso == 1)
                    {
                        var id = decimal.Parse(nomRecurso);
                        lista = (from e in db.RECURSOes
                                 where
                                     (e.IDENTIFICADORRECURSO == id)
                                 select e).ToList();
                        return lista;
                    }
                    if (idRecurso == 2)
                    {
                        lista = (from e in db.RECURSOes
                                 where
                                     (e.NOMBRERECURSO.ToLower().Contains(nomRecurso.ToLower())) && !e.ESTADORECURSO.Equals("Eliminado")
                                 select e).ToList();
                        return lista;
                    }
                    else
                    {
                        lista = (from s in db.RECURSOes
                                 join f in db.FACULTADs on s.IDENTIFICADORFACULTAD equals f.IDENTIFICADORFACULTAD
                                 where
                                     (f.NOMBREFACULTAD.ToLower().Contains(nomRecurso.ToLower())) && !s.ESTADORECURSO.Equals("Eliminado")
                                 select s).ToList();
                        return lista;
                    }

                }
            }
            catch (Exception)
            {
                return new List<RECURSO>();
            }


        }
        public static RECURSO VisualizarRecurso(decimal idRecurso)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                var recurso = (from e in db.RECURSOes
                             where e.IDENTIFICADORRECURSO == idRecurso
                             select e).FirstOrDefault();
                return recurso;

            }
            catch (Exception)
            {
                return null;
            }

        }

        internal static bool RegistrarRecurso(RECURSO nuevoRecurso)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                db.RECURSOes.Add(nuevoRecurso);
                db.SaveChanges();
                var oSalon = (from salon in db.RECURSOes
                              where
                                  nuevoRecurso.NOMBRERECURSO.ToLower().Trim().Equals(salon.NOMBRERECURSO.ToLower().Trim()) &&
                                  nuevoRecurso.IDENTIFICADORFACULTAD == salon.IDENTIFICADORFACULTAD
                              select salon).FirstOrDefault();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        internal static bool CrearFranjasHorarias(RECURSO oRecurso)
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
                            IDENTIFICADORRECURSO = oRecurso.IDENTIFICADORRECURSO,
                            DIAFRANJA = d.ToString().Trim(),
                        };
                        db.Database.ExecuteSqlCommand("Insert into FRANJA_HORARIA (IDENTIFICADORRECURSO, HORAINICIOFRANJA,HORAFINFRANJA,DIAFRANJA,ESTADOFRANJA) values (" +
                                              "'" + oFranjaHoraria.IDENTIFICADORRECURSO + "'," +
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

        internal static bool ModificarRecurso(RECURSO recursoModificado)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();

                var recurso = (from sal in db.RECURSOes where sal.IDENTIFICADORRECURSO == recursoModificado.IDENTIFICADORRECURSO select sal).First();
                {
                    recurso.IDENTIFICADORFACULTAD = recursoModificado.IDENTIFICADORFACULTAD;
                    recurso.NOMBRERECURSO = recursoModificado.NOMBRERECURSO;
                    recurso.ESTADORECURSO = recursoModificado.ESTADORECURSO;
                    recurso.IDTIPORECURSO = recursoModificado.IDTIPORECURSO;
                }
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        internal static bool EliminarRecurso(decimal idRecurso)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                if (BorrarFranjasHorarias(idRecurso))
                {
                    var recurso = (from sal in db.RECURSOes where sal.IDENTIFICADORRECURSO == idRecurso select sal).First();
                    {
                        recurso.ESTADORECURSO = "Eliminado";
                    }
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

        private static bool BorrarFranjasHorarias(decimal idRecurso)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                var lstFranjas =
                    (from franja in db.FRANJA_HORARIA where franja.IDENTIFICADORRECURSO == idRecurso select franja).ToList();
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
            var firstOrDefault = (from e in db.CURSOes
                where e.IDENTIFICADORCURSO == idCurso
                select e).FirstOrDefault();
            if (firstOrDefault == null) return new List<FRANJA_HORARIA>();
            var lista = firstOrDefault.FRANJA_HORARIA.ToList();
            return lista;
        }

        public static bool VerificarExistenciaRecurso(string nombreSalon, decimal idFacultad)
        {
            var db = new GestionInfraestructuraEntities();
            var curso = (from e in db.RECURSOes
                         where e.NOMBRERECURSO.ToLower().Trim().Equals(nombreSalon.ToLower().Trim())
                         && e.IDENTIFICADORFACULTAD == idFacultad
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

        public static string ObtenerIdTipoRecurso(string tipoRecurso)
        {
            var db = new GestionInfraestructuraEntities();
            var asig = (from e in db.TIPORECURSOes
                        where e.NOMBRETIPORECURSO.ToLower() == tipoRecurso.ToLower()
                        select e).FirstOrDefault();
            if (asig != null)
            {
                return asig.IDTIPORECURSO.ToString();
            }
            return "";
        }

        internal static bool CargarInformacion(string archivo)
        {
            var f = new StreamReader(archivo);
            try
            {
                String linea;
                while ((linea = f.ReadLine()) != null)
                {
                    var campos = linea.Split(',');
                    var db = new GestionInfraestructuraEntities();

                    var salon = new RECURSO()
                    {
                        IDTIPORECURSO = decimal.Parse(campos[0]),
                        IDENTIFICADORFACULTAD = decimal.Parse(campos[1]),
                        NOMBRERECURSO = campos[2],
                        ESTADORECURSO = campos[3]
                    };
                    db.RECURSOes.Add(salon);
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

        public static List<TIPORECURSO> ConsultarTiposRecurso()
        {
            var db = new GestionInfraestructuraEntities();
            return db.TIPORECURSOes.ToList();
        }

        public static bool VerificarExistenciaTipoRecurso(string tipoRecurso)
        {
            var db = new GestionInfraestructuraEntities();
            var curso = (from e in db.TIPORECURSOes
                         where e.NOMBRETIPORECURSO.ToLower().Trim().Equals(tipoRecurso.ToLower().Trim())
                         select e).FirstOrDefault();
            if (curso != null)
            {
                return true;
            }
            return false;
        }
    }
}
