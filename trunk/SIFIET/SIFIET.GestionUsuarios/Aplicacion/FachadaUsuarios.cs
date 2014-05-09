using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionUsuarios.Datos.Modelo;
using SIFIET.GestionUsuarios.Dominio.Servicios;

namespace SIFIET.GestionUsuarios.Aplicacion
{
    public static class FachadaUsuarios
    {
        public static List<USUARIO> ConsultarUsuarios()
        {
            return ServicioUsuarios.ConsultarUsuarios();
        }

        public static void RegistrarUsuario(USUARIO usuario, string[] roles)
        {
            ServicioUsuarios.RegistrarUsuario(usuario, roles);
        }

        public static USUARIO ConsultarUsuario(int idUsuario)
        {
            return ServicioUsuarios.ConsultarUsuario(idUsuario);
        }

        public static void ModificarUsuario(USUARIO oUsuario, string[] roles)
        {
            ServicioUsuarios.ModificarUsuario(oUsuario, roles);
        }

        public static void EliminarUsuario(int idUsuario)
        {
            ServicioUsuarios.EliminarUsuario(idUsuario);
        }

        public static List<USUARIO> BuscarUsuarioPorNombre(string nombre,string estado)
        {
            return ServicioUsuarios.BuscarUsuarioPorNombre(nombre,estado);
        }

        public static List<USUARIO> BuscarUsuarioPorApellido(string apellido,string estado)
        {
            return ServicioUsuarios.BuscarUsuarioPorApellido(apellido,estado);
        }

        public static List<USUARIO> BuscarUsuarioPorIdentificacion(string id,string estado)
        {
             return ServicioUsuarios.BuscarUsuarioPorIdentificacion(id,estado);
       }

        public static bool IdentificacionUsuarioExiste(string id)
        {
            return ServicioUsuarios.IdentificacionUsuarioExiste(id);
        }
    }
    }

