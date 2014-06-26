using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Datos.Modelo;
using SIFIET.GestionInfraestructura.Aplicacion;
using SIFIET.GestionProgramas.Aplicacion;

namespace SIFIET.Aplicacion
{
    public partial class FachadaSIFIET
    {
        #region Metodos Gestion Infraestructura Fachada Facultades
        public static List<FACULTAD> ConsultarFacultades(decimal idFacultad, string nombreFacultad)
        {
            return GestionInfraestructura.Aplicacion.FachadaFacultades.ConsultarFacultades(idFacultad, nombreFacultad);
        }

        public static List<FACULTAD> ConsultarFacultades()
        {
            return GestionInfraestructura.Aplicacion.FachadaFacultades.ConsultarFacultades();
        }
        #endregion 

        #region Metodos Gestion Infraestructura Fachada Recursos

        public static List<RECURSO> ConsultarRecursos(decimal idRecurso, string nombreRecurso, string estado)
        {
            return FachadaRecursos.ConsultarRecursos(idRecurso, nombreRecurso, estado);
        }

        public static RECURSO VisualizaRecurso(decimal idRecurso)
        {
            return FachadaRecursos.VisualizarRecurso(idRecurso);
        }

        public static bool RegistrarRecurso(RECURSO oRecurso)
        {
            return FachadaRecursos.RegistrarRecurso(oRecurso);
        }

        public static bool ModificarRecurso(RECURSO oRecurso)
        {
            return FachadaRecursos.ModificarRecurso(oRecurso);
        }

        public static bool EliminarRecurso(decimal idRecurso)
        {
            return FachadaRecursos.EliminarRecurso(idRecurso);
        }

        public static bool CargarInformacionRecurso(string archivo)
        {
            return FachadaRecursos.CargarInformacion(archivo);
        }

        public static bool VerificarExistenciaRecurso(string nombreRecurso, string IdTipoRecurso)
        {
            return FachadaRecursos.VerificarExistenciaRecurso(nombreRecurso, IdTipoRecurso);
        }

        public static bool VerificarExistenciaTipoRecurso(string tipoRecurso)
        {
            return FachadaRecursos.VerificarExistenciaTipoRecurso(tipoRecurso);
        }

        public static bool VerificarCampoFacultad(string nombreFacultad)
        {
            return FachadaRecursos.VerificarCampoFacultad(nombreFacultad);
        }

        public static string ObtenerIdFacultad(string nombreFacultad)
        {
            return FachadaRecursos.ObtenerIdFacultad(nombreFacultad);
        }

        public static string ObtenerIdTipoRecurso(string tipoRecurso)
        {
            return FachadaRecursos.ObtenerIdTipoRecurso(tipoRecurso);
        }

        public static List<TIPORECURSO> ConsultarTiposRecurso()
        {
            return FachadaRecursos.ConsultarTiposRecurso();
        }

        #endregion

        #region Metodos Gestion de Infraestructura Fachada FranjaHorarias
        public static List<FRANJA_HORARIA> ConsultarFranjaHoraria(decimal idSalon = 0, string dia = "",
            string horaInicio = "", string horaFin = "")
        {
            return FachadaFranjasHorarias.ConsultarFranjaHoraria(idSalon, dia, horaInicio, horaFin);
        }
        public static List<FRANJA_HORARIA> ObtenerHorarioCurso(decimal idCurso)
        {
            return FachadaRecursos.ObtenerHorarioCurso(idCurso);
        }

        public static List<string> ConsultarFranjaHorariaDisponible(string idSalon, string dia = "",
            string horaInicio = "", string horaFin = "")
        {
            return FachadaFranjasHorarias.ConsultarFranjaHorariaDisponible(idSalon, dia, horaInicio, horaFin);
        }

        public static void RegistrarFranjaHoraria(FRANJA_HORARIA franja, decimal idCurso)
        {
            FachadaFranjasHorarias.RegistrarFranjaHoraria(franja, idCurso);

        }

        public static void EliminarFranjaHoraria(int idCurso ,int idHorario )
        {
            FachadaFranjasHorarias.EliminarFranjaHoraria(idCurso, idHorario);
        }

        #endregion
    }
}
