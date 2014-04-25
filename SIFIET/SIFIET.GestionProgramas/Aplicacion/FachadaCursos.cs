using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Dominio.Servicios;
using SIFIET.GestionProgramas.Datos.Modelo;

namespace SIFIET.GestionProgramas.Aplicacion
{
    public static class FachadaCursos
    {
        public static List<CURSO> ConsultarCursos(decimal idCurso, string nombreCurso)
        {
            return ServicioCursos.ConsultarCursos(idCurso,nombreCurso);
        }

        public static CURSO VisualizarCurso(decimal idCurso)
        {
            return ServicioCursos.VisualizarCurso(idCurso);
        }

        public static bool RegistrarCurso(CURSO oCurso)
        {
            return ServicioCursos.RegistrarCurso(oCurso);
        }

        public static bool ModificarCurso(CURSO oCurso)
        {
            return ServicioCursos.ModificarCurso(oCurso);
        }

        public static bool EliminarCurso(decimal idCurso)
        {
            return ServicioCursos.EliminarCurso(idCurso);
        }

    }
}
