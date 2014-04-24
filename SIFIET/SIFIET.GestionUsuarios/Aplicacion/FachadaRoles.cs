using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionUsuarios.Datos.Modelo;

namespace SIFIET.GestionUsuarios.Aplicacion
{
    public static class FachadaRoles
    {
        public static List<ROL> ConsultarRoles()
        {
            throw new NotImplementedException();
        }

        public static bool RegistrarRoles(ROL oRol, List<PERMISO> lstPermisos)
        {
            throw new NotImplementedException();
        }

        public static ROL ConsultarRol(string idRol)
        {
            throw new NotImplementedException();
        }

        public static bool ModificarRol(ROL oRol, List<PERMISO> lstPermisos)
        {
            throw new NotImplementedException();
        }

        public static bool EliminarRol(string idRol)
        {
            throw new NotImplementedException();
        }

        public static List<ROL> BuscarRolPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public static bool ExisteNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public static List<ROL> BuscarRolPorEstado(string estado)
        {
            throw new NotImplementedException();
        }
    }
}
