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

        public static void RegistrarUsuario(USUARIO oUsuario, string[] roles)
        {
             FachadaUsuarios.RegistrarUsuario(oUsuario, roles);
        }

        public static USUARIO ConsultarUsuario(int idUsuario)
        {
            return FachadaUsuarios.ConsultarUsuario(idUsuario);
        }

        public static void ModificarUsuario(USUARIO oUsuario, string[] roles)
        {
             FachadaUsuarios.ModificarUsuario(oUsuario, roles);
        }

        public static void EliminarUsuario(int idUsuario)
        {
            FachadaUsuarios.EliminarUsuario(idUsuario);
        }

        public static List<USUARIO> ConsultarUsuarioPorNombre(string nombre,string estado)
        {
            return FachadaUsuarios.BuscarUsuarioPorNombre(nombre,estado);
        }

        public static List<USUARIO> ConsultarUsuarioPorApellido(string apellido,string estado)
        {
            return FachadaUsuarios.BuscarUsuarioPorApellido(apellido,estado);
        }

        public static List<USUARIO> ConsultarUsuarioPorIdentificacion(string id,string estado)
        {
            return FachadaUsuarios.BuscarUsuarioPorIdentificacion(id,estado);
        }

        public static bool IdentificacionUsuarioExiste(string id)
        {
            return FachadaUsuarios.IdentificacionUsuarioExiste(id);
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
            return FachadaRoles.RegistrarRol(oRol,lstPermisos);
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
        public static List<ROL> ConsultarRolPorNombre(string nombre,string estado)
        {
            return FachadaRoles.ConsultarRolPorNombre(nombre,estado);
        }

        public static bool ExisteNombreRol(string nombre)
        {
            return FachadaRoles.ExisteNombreRol(nombre);
        }

        public static List<ROL> ConsultarRolPorEstado(string estado)
        {
            return FachadaRoles.ConsultarRolPorEstado(estado);
        }

        public static List<string> ConsultarNombresPermisos()
        {
            return FachadaRoles.ConsultarNombresPermisos();
        } 
        #endregion
    }
}
