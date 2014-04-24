using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionUsuarios.Aplicacion;
using SIFIET.GestionUsuarios.Datos.Modelo;

namespace SIFIET.Aplicacion
{
    public partial class FachadaSIFIET
    {
        //Metodos dirigidos a Fachada Usuarios en el Dominio Gestion de Usuarios
        #region Metodos Gestion de Usuarios
        public static List<USUARIO> ConsultarUsuarios()
        {
            return FachadaUsuarios.ConsultarUsuarios();
        }

        public static bool RegistrarUsuario(USUARIO oUsuario, string[] roles)
        {
            return FachadaUsuarios.RegistrarUsuario(oUsuario, roles);
        }

        public static USUARIO ConsultarUsuario(string idUsuario)
        {
            return FachadaUsuarios.ConsultarUsuario(idUsuario);
        }

        public static bool ModificarUsuario(USUARIO oUsuario, string[] roles)
        {
            return FachadaUsuarios.ModificarUsuario(oUsuario, roles);
        }

        public static bool EliminarUsuario(string idUsuario)
        {
            return FachadaUsuarios.EliminarUsuario(idUsuario);
        }

        public static List<USUARIO> ConsultarUsuarioPorNombre(string nombre)
        {
            return FachadaUsuarios.BuscarUsuarioPorNombre(nombre);
        }

        public static List<USUARIO> ConsultarUsuarioPorApellido(string apellido)
        {
            return FachadaUsuarios.BuscarUsuarioPorApellido(apellido);
        }

        public static List<USUARIO> ConsultarUsuarioPorIdentificacion(int id)
        {
            return FachadaUsuarios.BuscarUsuarioPorIdentificacion(id);
        }

        #endregion


        //Metodos dirigidos a Fachada Roles en el Dominio Gestion de Usuarios
        #region Metodos Gestion de Roles
        public static List<ROL> ConsultarRoles()
        {
            return FachadaRoles.ConsultarRoles();
        }
        public static bool RegistrarRoles(ROL oRol, List<PERMISO> lstPermisos)
        {
            return FachadaRoles.RegistrarRoles(oRol,lstPermisos);
        }

        public static ROL ConsultarRol(string idRol)
        {
            return FachadaRoles.ConsultarRol(idRol);
        }

        public static bool ModificarRol(ROL oRol, List<PERMISO> lstPermisos)
        {
            return FachadaRoles.ModificarRol(oRol,lstPermisos);
        }

        public static bool EliminarRol(string idRol)
        {
            return FachadaRoles.EliminarRol(idRol);
        }
        public static List<ROL> BuscarRolPorNombre(string nombre)
        {
            return FachadaRoles.BuscarRolPorNombre(nombre);
        }

        public static bool ExisteNombre(string nombre)
        {
            return FachadaRoles.ExisteNombre(nombre);
        }

        public static List<ROL> BuscarRolPorEstado(string estado)
        {
            return FachadaRoles.BuscarRolPorEstado(estado);
        }
        #endregion
    }
}
