using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Datos.Modelo;
using SIFIET.GestionInfraestructura.Dominio.Servicios;

namespace SIFIET.GestionInfraestructura.Aplicacion
{
    public static class FachadaRecursos
    {
        public static List<RECURSO> ConsultarRecursos(decimal idRecurso, string nombreRecurso, string estado)
        {
            return ServicioRecursos.ConsultarRecursos(idRecurso, nombreRecurso, estado);
        }

        public static RECURSO VisualizarRecurso(decimal idRecurso)
        {
            return ServicioRecursos.VisualizarRecurso(idRecurso);
        }

        public static bool RegistrarRecurso(RECURSO oSalon)
        {
            return ServicioRecursos.RegistrarRecurso(oSalon);
        }

        public static bool ModificarRecurso(RECURSO oRecurso)
        {
            return ServicioRecursos.ModificarRecurso(oRecurso);
        }

        public static bool EliminarRecurso(decimal idRecurso)
        {
            return ServicioRecursos.EliminarRecurso(idRecurso);
        }

        public static List<FRANJA_HORARIA> ObtenerHorarioCurso(decimal idCurso)
        {
            return ServicioRecursos.ObtenerHorarioCurso(idCurso);
        }

        public static bool VerificarExistenciaRecurso(string nombreRecurso, string IdTipoRecurso)
        {
            return ServicioRecursos.VerificarExistenciaRecurso(nombreRecurso, decimal.Parse(IdTipoRecurso));
        }

        public static bool VerificarCampoFacultad(string nombreFacultad)
        {
            return ServicioRecursos.VerificarCampoFacultad(nombreFacultad);
        }

        public static string ObtenerIdFacultad(string nombreFacultad)
        {
            return ServicioRecursos.ObtenerIdFacultad(nombreFacultad);
        }

        public static string ObtenerIdTipoRecurso(string tipoRecurso)
        {
            return ServicioRecursos.ObtenerIdTipoRecurso(tipoRecurso);
        }

        public static bool CargarInformacion(string archivo)
        {
            return ServicioRecursos.CargarInformacion(archivo);
        }

        public static List<TIPORECURSO> ConsultarTiposRecurso()
        {
            return ServicioRecursos.ConsultarTiposRecurso();
        }

        public static bool VerificarExistenciaTipoRecurso(string tipoRecurso)
        {
            return ServicioRecursos.VerificarExistenciaTipoRecurso(tipoRecurso);
        }
    }
}
