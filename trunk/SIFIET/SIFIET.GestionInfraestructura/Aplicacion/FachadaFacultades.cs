using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Dominio.Servicios;
using SIFIET.GestionInfraestructura.Datos.Modelo;

namespace SIFIET.GestionInfraestructura.Aplicacion
{
    public static class FachadaFacultades
    {
        public static List<FACULTAD> ConsultarFacultades(decimal idFacultad, string nombreFacultad)
        {
            return ServicioFacultades.ConsultarFacultades(idFacultad, nombreFacultad);
        }

        public static List<FACULTAD> ConsultarFacultades()
        {
            return ServicioFacultades.ConsultarFacultades();
        }
    }
}
