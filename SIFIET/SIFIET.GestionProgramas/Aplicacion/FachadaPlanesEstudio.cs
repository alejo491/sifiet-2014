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
        public static PLANESTUDIO ConsultarPlanEstudio(decimal idPlanEstudio)
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioPlanesEstudio.ConsultarPlanEstudio(idPlanEstudio);
        }

        public static List<PLANESTUDIO> ConsultarPlanesEstudios(string campo, string busqueda)
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioPlanesEstudio.ConsultarPlanesEstudios(campo, busqueda);
        }
        
        public static bool RegistrarPlanEstudio(PLANESTUDIO objPlanEstudio)
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioPlanesEstudio.RegistrarPlanEstudio(objPlanEstudio);
        }

        public static bool EditarPlanEstudio(PLANESTUDIO objPlanEstudio)
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioPlanesEstudio.EditarPlanEstudio(objPlanEstudio);
        }

        public static bool EliminarPlanEstudio(decimal idPlanEstudio)
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioPlanesEstudio.EliminarPlanEstudio(idPlanEstudio);
        }

        // metodo hecho para el modulo de asignatura
        public static List<PLANESTUDIO> ConsultarPlanesEstudios(string palabraBusqueda)
        {
            return ServicioPlanesEstudio.ConsultarPlanestudios(palabraBusqueda);
        }

    }
}
