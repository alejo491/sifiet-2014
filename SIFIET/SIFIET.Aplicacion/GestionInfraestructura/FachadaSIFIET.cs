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

        #region Metodos Gestion Infraestructura Fachada Salones
        public static List<SALON> ConsultarSalones(decimal idSalon, string nombreSalon, string estado)
        {
            return FachadaSalones.ConsultarSalones(idSalon, nombreSalon, estado);
        }

        public static SALON VisualizarSalon(decimal idSalon)
        {
            return FachadaSalones.VisualizarSalon(idSalon);
        }

        public static bool RegistrarSalon(SALON oSalon)
        {
            return FachadaSalones.RegistrarSalon(oSalon);
        }

        public static bool ModificarSalon(SALON oSalon)
        {
            return FachadaSalones.ModificarSalon(oSalon);
        }

        public static bool EliminarSalon(decimal idSalon)
        {
            return FachadaSalones.EliminarSalon(idSalon);
        }

        public static bool CargarInformacionSalon(string archivo)
        {
            return FachadaSalones.CargarInformacion(archivo);
        }

        public static bool VerificarExistenciaSalon(string nombreSalon, string IdFacultad)
        {
            return FachadaSalones.VerificarExistenciaSalon(nombreSalon, IdFacultad);
        }

        public static bool VerificarCampoFacultad(string nombreFacultad)
        {
            return FachadaSalones.VerificarCampoFacultad(nombreFacultad);
        }

        public static string ObtenerIdFacultad(string nombreFacultad)
        {
            return FachadaSalones.ObtenerIdFacultad(nombreFacultad);
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
            return FachadaSalones.ObtenerHorarioCurso(idCurso);
        }

        public static List<string> ConsultarFranjaHorariaDisponible(decimal idSalon, string dia = "",
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
