using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Datos.Modelo;
using SIFIET.GestionProgramas.Dominio.Servicios;

namespace SIFIET.GestionProgramas.Aplicacion
{
    public static class FachadaAsignaturas
    {
        public static List<ASIGNATURA> ConsultarAsignaturas(decimal idAsignatura, string nombreAsignatura, string estado)
        {
            return ServicioAsignaturas.ConsultarAsignaturas(idAsignatura, nombreAsignatura, estado);
        }

        public static ASIGNATURA VisualizarAsignatura(decimal idAsignatura)
        {
            return ServicioAsignaturas.VisualizarAsignatura(idAsignatura);
        }

        public static bool RegistrarAsignatura(ASIGNATURA oAsignatura)
        {
            return ServicioAsignaturas.RegistrarAsignatura(oAsignatura);
        }

        public static bool ModificarAsignatura(ASIGNATURA oAsignatura)
        {
            return ServicioAsignaturas.ModificarAsignatura(oAsignatura);
        }

        public static bool EliminarAsignatura(decimal idAsignatura)
        {
            return ServicioAsignaturas.EliminarAsignatura(idAsignatura);
        }

        public static bool CargarInformacion(string archivo)
        {
            return ServicioAsignaturas.CargarInformacion(archivo);
        }

        public static bool VerificarCampoPlanEstudios(string nombrePlanEstudios)
        {
            return ServicioAsignaturas.VerificarCampoPlanEstudios(nombrePlanEstudios);
        }

        public static bool VerificarCampoCoRequisitosAsignatura(string coRequisitosAsignatura)
        {
            return ServicioAsignaturas.VerificarCampoCoRequisitosAsignatura(coRequisitosAsignatura);
        }

        public static bool VerificarCampoPreRequisitosAsignatura(string preRequisitosAsignatura)
        {
            return ServicioAsignaturas.VerificarCampoPreRequisitosAsignatura(preRequisitosAsignatura);
        }

        public static string ObtenerIdPlanEstudios(string nombre)
        {
            return ServicioAsignaturas.ObtenerIdPlanEstudios(nombre);
        }
    }
}
