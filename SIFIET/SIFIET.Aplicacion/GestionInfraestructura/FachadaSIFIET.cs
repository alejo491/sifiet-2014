﻿using System;
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
        #endregion
    }
}
