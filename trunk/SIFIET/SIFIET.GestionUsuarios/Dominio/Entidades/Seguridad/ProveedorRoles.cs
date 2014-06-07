using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using SIFIET.GestionUsuarios.Datos.Modelo;

namespace SIFIET.GestionUsuarios.Dominio.Entidades.Seguridad
{
    public class ProveedorRoles : RoleProvider 
    {

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var db = new GestionUsuariosEntities();
            username += "@unicauca.edu.co";
            var usuario = (from e in db.USUARIOs where e.EMAILINSTITUCIONALUSUARIO.Equals(username) select e).FirstOrDefault();
            if (usuario == null) return new string[] {};
            var roles = usuario.ROLs.Select(rol => rol).ToList();
            var permisosRoles = roles.Select(rol => rol.PERMISOS).ToList();
            return (from permisoRol in permisosRoles from permiso in permisoRol select permiso.NOMBREPERMISO).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var db = new GestionUsuariosEntities();
            username += "@unicauca.edu.co";
            var usuario = (from e in db.USUARIOs where e.EMAILINSTITUCIONALUSUARIO.Equals(username) select e).FirstOrDefault();
            if (usuario == null) return false;
            var roles = usuario.ROLs.Select(rol => rol).ToList();
            /*for (int i = 0; i < roles.Count; i++)
            {
                var rol = roles.ElementAt(i).Trim();
                roles.RemoveAt(i);
                roles.Add(rol);
            }
            return roles.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
            */
            var permisosRoles = roles.Select(rol => rol.PERMISOS).ToList();
            var nombrespermisos =(from permisoRol in permisosRoles from permiso in permisoRol select permiso.NOMBREPERMISO).ToArray();
            return nombrespermisos.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
