﻿using System;
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

        public static List<PLANESTUDIO> ConsultarPlanesEstudios(string estado, string campo, string busqueda)
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioPlanesEstudio.ConsultarPlanesEstudios(estado, campo, busqueda);
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

        public static List<ASIGNATURA_PERTENECE_PLAN_ESTU> ConsultarAsignaturasPorPlanEstudio(decimal idPlanEstudio) {
            return ServicioPlanesEstudio.ConsultarAsignaturasPorPlanEstudio(idPlanEstudio);
        }

        public static bool RegistrarAsignaturaPlanEstudio(ASIGNATURA_PERTENECE_PLAN_ESTU objAsignaturaPlanEstudio) {
            return ServicioPlanesEstudio.RegistrarAsignaturaPlanEstudio(objAsignaturaPlanEstudio);
        }

        public static bool EliminarAsignaturaPlanEstudio(decimal idPlanEstudio, decimal idAsignatura) 
        {
            return ServicioPlanesEstudio.EliminarAsignaturaPlanEstudio(idPlanEstudio, idAsignatura);
        }

        // metodo hecho para el modulo de asignatura
        public static List<PLANESTUDIO> ConsultarPlanesEstudios(string palabraBusqueda)
        {
            return ServicioPlanesEstudio.ConsultarPlanestudios(palabraBusqueda);
        }


        public static byte[] PlanEstudiosPDF(decimal idPlan)
        {
            return ServicioPlanesEstudio.PlanEstudiosPDF(idPlan);
        }
    }
}
