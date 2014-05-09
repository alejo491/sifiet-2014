using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Dominio.Servicios;
using SIFIET.GestionInfraestructura.Datos.Modelo;

namespace SIFIET.GestionInfraestructura.Aplicacion
{
    public static class FachadaSalones
    {
        public static List<SALON> ConsultarSalones(decimal idSalon, string nombreSalon, string estado)
        {
            return ServicioSalones.ConsultarSalones(idSalon,nombreSalon,estado);
        }

        public static SALON VisualizarSalon(decimal idSalon)
        {
            return ServicioSalones.VisualizarSalon(idSalon);
        }

        public static bool RegistrarSalon(SALON oSalon)
        {
            return ServicioSalones.RegistrarSalon(oSalon);
        }

        public static bool ModificarSalon(SALON oSalon)
        {
            return ServicioSalones.ModificarSalon(oSalon);
        }

        public static bool EliminarSalon(decimal idSalon)
        {
            return ServicioSalones.EliminarSalon(idSalon);
        }

        public static List<FRANJA_HORARIA> ObtenerHorarioCurso(decimal idCurso)
        {
            return ServicioSalones.ObtenerHorarioCurso(idCurso);
        }

    }
}

