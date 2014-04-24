using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionUsuarios.Datos.Modelo;

namespace SIFIET.GestionUsuarios.Aplicacion
{
    public static class FachadaUsuarios
    {
        public static List<USUARIO> ConsultarUsuarios()
        {
            throw new NotImplementedException();
        }

        public static bool RegistrarUsuario(USUARIO usuario, string[] roles)
        {
            throw new NotImplementedException();
        }

        public static USUARIO ConsultarUsuario(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public static bool ModificarUsuario(USUARIO oUsuario, string[] roles)
        {
            throw new NotImplementedException();
        }

        public static bool EliminarUsuario(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public static List<USUARIO> BuscarUsuarioPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public static List<USUARIO> BuscarUsuarioPorApellido(string apellido)
        {
            throw new NotImplementedException();
        }

        public static List<USUARIO> BuscarUsuarioPorIdentificacion(int id)
        {
            throw new NotImplementedException();
        }
    }
}
