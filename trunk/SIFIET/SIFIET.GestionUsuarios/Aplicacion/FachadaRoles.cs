using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionUsuarios.Datos.Modelo;
using SIFIET.GestionUsuarios.Dominio.Servicios;

namespace SIFIET.GestionUsuarios.Aplicacion
{
    public static class FachadaRoles
    {
        public static List<ROL> ConsultarRoles()
        {
            return ServicioRoles.ConsultarRoles();
        }

        public static bool RegistrarRol(ROL oRol, List<PERMISO> lstPermisos)
        {
            return ServicioRoles.RegistrarRol(oRol, lstPermisos);
        }

        public static ROL ConsultarRol(string idRol)
        {
            return ServicioRoles.ConsultarRol(idRol);
        }

        public static bool ModificarRol(ROL oRol, List<PERMISO> lstPermisos)
        {
            return ServicioRoles.ModificarRol(oRol, lstPermisos);
        }

        public static bool EliminarRol(string idRol)
        {
            return ServicioRoles.EliminarRol(idRol);
        }

        public static bool ExisteNombreRol(string nombre)
        {
            return ServicioRoles.ExisteNombreRol(nombre);
        }

        public static List<ROL> ConsultarRolPorNombre(string nombre)
        {
            return ServicioRoles.ConsultarRolPorNombre(nombre);
        }
        public static List<ROL> ConsultarRolPorEstado(string estado)
        {
            return ServicioRoles.ConsultarRolPorEstado(estado);
        }

        public static List<string> ConsultarNombresPermisos()
        {
            return ServicioRoles.ConsultarNombresPermisos();
        } 
    }
}
