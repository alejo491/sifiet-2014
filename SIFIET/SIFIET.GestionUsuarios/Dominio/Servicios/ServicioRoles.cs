using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionUsuarios.Datos.Modelo;

namespace SIFIET.GestionUsuarios.Dominio.Servicios
{
    class ServicioRoles
    {
        public static List<ROL> ConsultarRoles()
        {
            try
            {
                var db = new GestionUsuariosEntities();
                var roles = db.ROLs.ToList();
                return roles;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool RegistrarRol(ROL oRol, List<PERMISO> lstPermisos)
        {
            var db = new GestionUsuariosEntities();
            oRol.PERMISOS = ConsultarPermisos(lstPermisos);
            try
            {
                db.Database.ExecuteSqlCommand("Insert into ROL (NOMBREROL, DESCRIPCIONROL,ESTADOROL) values (" +
                                              "'" + oRol.NOMBREROL + "'," +
                                              "'" + oRol.DESCRIPCIONROL + "'," +
                                              "'" + oRol.ESTADOROL + "')");
                db.SaveChanges();
                var oRolBd = (from e in db.ROLs
                              where e.NOMBREROL.Trim().Equals(oRol.NOMBREROL.Trim())
                                    && e.DESCRIPCIONROL.Trim().Equals(oRol.DESCRIPCIONROL.Trim())
                                    && e.ESTADOROL.Trim().Equals(oRol.ESTADOROL.Trim())
                              select e).FirstOrDefault();
                foreach (var permiso in oRol.PERMISOS)
                {
                    if (oRolBd != null)
                        db.Database.ExecuteSqlCommand(
                            "Insert into ROL_TIENE_PERMISOS(IDENTIFICADORROL,IDENTIFICADORPERMISO)" +
                            "values (" + oRolBd.IDENTIFICADORROL + "," + permiso.IDENTIFICADORPERMISO + ")");
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static ROL ConsultarRol(string idRol)
        {
            try
            {
                var db = new GestionUsuariosEntities();
                return db.ROLs.Find(decimal.Parse(idRol.Trim()));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool ModificarRol(ROL oRol, List<PERMISO> lstPermisos)
        {
            var db = new GestionUsuariosEntities();
            oRol.PERMISOS = ConsultarPermisos(lstPermisos);
            try
            {
                db.Database.ExecuteSqlCommand("Update ROL SET NOMBREROL= '"+oRol.NOMBREROL+"' ,"+
                    " DESCRIPCIONROL= '" + oRol.DESCRIPCIONROL + "' ," +
                    " ESTADOROL= '" + oRol.ESTADOROL + "'" +
                                              " WHERE IDENTIFICADORROL= " + oRol.IDENTIFICADORROL);
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("Delete from ROL_TIENE_PERMISOS" +
                            " WHERE IDENTIFICADORROL= " + oRol.IDENTIFICADORROL);
                db.SaveChanges();
                foreach (var permiso in oRol.PERMISOS)
                {
                        db.Database.ExecuteSqlCommand("Insert into ROL_TIENE_PERMISOS(IDENTIFICADORROL,IDENTIFICADORPERMISO)" +
                            "values (" + oRol.IDENTIFICADORROL + "," + permiso.IDENTIFICADORPERMISO + ")");
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
               return false;
            }
        }

        public static bool EliminarRol(string idRol)
        {
            try
            {
                var db = new GestionUsuariosEntities();
                /*db.Database.ExecuteSqlCommand("Delete from ROL_TIENE_PERMISOS" +
                            " WHERE IDENTIFICADORROL= " +int.Parse(idRol.Trim()));
                db.SaveChanges();*/
                var oRol = db.ROLs.Find(decimal.Parse(idRol.Trim()));
                oRol.ESTADOROL = "Desactivado";
                db.Entry(oRol).State = EntityState.Modified;
                db.SaveChanges();
                return true;
           }
           catch (Exception)
            {
               return false;;
            }
    }

        public static bool ExisteNombreRol(string nombre)
        {
            try
            {
                var db = new GestionUsuariosEntities();
                var lista = (from e in db.ROLs
                    where e.NOMBREROL.Trim() == nombre.Trim()
                    select e).ToList();
                return lista.Count > 0;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public static List<ROL> ConsultarRolPorNombre(string nombre,string estado)
        {
            try
            {
                var db = new GestionUsuariosEntities();
                var lstRols = (from e in db.ROLs
                    where e.NOMBREROL.Contains(nombre) && e.ESTADOROL.Trim().Equals(estado.Trim())
                    select e).ToList();
                return lstRols;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<ROL> ConsultarRolPorEstado(string estado)
        {
            try
            {
                var db = new GestionUsuariosEntities();
                var lstRols = (from e in db.ROLs
                    where e.ESTADOROL.Contains(estado)
                    select e).ToList();
                return lstRols;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<string> ConsultarNombresPermisos()
        {
            try
            {
                var db = new GestionUsuariosEntities();
                var agrupacion = from p in db.PERMISOS
                                 group p by p.NOMBREPERMISO.Trim() into permiso
                                 select permiso;

                return agrupacion.Select(grupo => grupo.Key).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /*Funcion que recibe una lista de permisos para asociarlos a un rol, 
         * verifica su exitencia en la base de datos si existe se copia el objeto desde la base de datos 
         * a un nueva lista, si no existe se crea el registro y de igual forma se agrega en la misma lista.
         */
        public static List<PERMISO> ConsultarPermisos(List<PERMISO> lstPermisosSinId)
        {
            var db = new GestionUsuariosEntities();
            var lstPermisosConId = new List<PERMISO>();
            foreach (var oPermiso in lstPermisosSinId)
            {
                 var result = (from e in db.PERMISOS
                                 where e.NOMBREPERMISO.Trim().Equals(oPermiso.NOMBREPERMISO.Trim())
                                 && e.GESTIONARPERMISO==oPermiso.GESTIONARPERMISO
                                 select e).FirstOrDefault();
                if (result != null)
                    lstPermisosConId.Add(db.PERMISOS.Find(result.IDENTIFICADORPERMISO));
                else
                {
                    db.Database.ExecuteSqlCommand("Insert into PERMISOS (NOMBREPERMISO, ESTADOPERMISO,GESTIONARPERMISO) values (" +
                                              "'" + oPermiso.NOMBREPERMISO + "'," +
                                              "'" + oPermiso.ESTADOPERMISO + "'," +
                                              + oPermiso.GESTIONARPERMISO + ")");
                    db.SaveChanges();
                    var idPermiso = (from e in db.PERMISOS
                        where e.NOMBREPERMISO.Trim().Equals(oPermiso.NOMBREPERMISO.Trim())
                              && e.GESTIONARPERMISO == oPermiso.GESTIONARPERMISO
                        select e.IDENTIFICADORPERMISO).FirstOrDefault();
                    lstPermisosConId.Add(db.PERMISOS.Find(idPermiso));
                }
            }
            return lstPermisosConId;
        }
    }
}
