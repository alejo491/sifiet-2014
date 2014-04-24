using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Datos.Modelo;
using SIFIET.GestionProgramas.Dominio.Servicios;

namespace SIFIET.GestionProgramas.Aplicacion
{
    public static class FachadaPlanesEstudio
    {
        public static List<PLANESTUDIO> ConsultarPlanesEstudios(string palabraBusqueda)
        {
            return ServicioPlanesEstudio.ConsultarPlanestudios(palabraBusqueda);
        }

    }
}
