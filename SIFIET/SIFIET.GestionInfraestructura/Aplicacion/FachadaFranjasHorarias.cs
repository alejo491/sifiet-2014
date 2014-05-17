using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Datos.Modelo;
using SIFIET.GestionInfraestructura.Dominio.Servicios;

namespace SIFIET.GestionInfraestructura.Aplicacion
{
    public static class FachadaFranjasHorarias
    {
        public static List<FRANJA_HORARIA> ConsultarFranjaHoraria(decimal idSalon=0, string dia = "",
            string horaInicio = "", string horaFin = "")
        {
            return ServicioFranjasHorarias.ConsultarFranjaHoraria(idSalon, dia, horaInicio, horaFin);
        }

        public static List<string> ConsultarFranjaHorariaDisponible(decimal idSalon, string dia = "",
            string horaInicio = "", string horaFin = "")
        {
            return ServicioFranjasHorarias.ConsultarFranjaHorariaDisponible(idSalon,dia,horaInicio,horaFin);
        }
    }
}
