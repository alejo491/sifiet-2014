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
        public static List<ASIGNATURA> ConsultarAsignaturas(string palabraBusqueda)
        {
            return ServicioAsignaturas.ConsultarAsignaturas(palabraBusqueda);
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
    }
}
