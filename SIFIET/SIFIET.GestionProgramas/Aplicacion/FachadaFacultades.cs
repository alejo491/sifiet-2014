using SIFIET.GestionProgramas.Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionProgramas.Aplicacion
{
    public static class FachadaFacultades
    {
        public static List<FACULTAD> obtenerNombreFacultades()
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioFacultades.obtenerNombreFacultades();
        }
    }
}
