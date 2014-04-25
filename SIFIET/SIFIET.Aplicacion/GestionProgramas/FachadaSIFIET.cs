using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Aplicacion;
using SIFIET.GestionProgramas.Datos.Modelo;

namespace SIFIET.Aplicacion
{
    public partial class FachadaSIFIET
    {
        //Metodos dirigidos a Fachada Programas en el dominio de Gestion de Programas
        #region Metodos Gestion de Programas

        public static List<PROGRAMA> ConsultarProgramas()
        {
            return FachadaProgramas.ConsultarProgramas();
        }

        public static PROGRAMA ConsultarPrograma(string id)
        {
            return FachadaProgramas.ConsultarPrograma(id);
        }

        public static List<PROGRAMA> ConsultarProgramaPorNombre(string busqueda)
        {
            return FachadaProgramas.ConsultarProgramaPorNombre(busqueda);
        }


        public static bool RegistrarPrograma(PROGRAMA oPrograma)
        {
            return FachadaProgramas.RegistrarPrograma(oPrograma);
        }


        public static bool ModificarPrograma(PROGRAMA oPrograma)
        {
            return FachadaProgramas.ModificarPrograma(oPrograma);
        }

        public static bool EliminarPrograma(PROGRAMA oPrograma)
        {
            return FachadaProgramas.EliminarPrograma(oPrograma);
        }

        public static bool CargarDatos(DataSet datosExcel)
        {
            return FachadaProgramas.CargarDatos(datosExcel);
        }

        #endregion
        //Metodos dirigidos a Fachada Asignaturas en el dominio de Gestion de Programas
        #region Metodos Gestion de Asignaturas

        public static List<ASIGNATURA> ConsultarAsignaturas(string palabraBusqueda)
        {
            return FachadaAsignaturas.ConsultarAsignaturas(palabraBusqueda);
        }

        public static ASIGNATURA VisualizarAsignatura(decimal idAsignatura)
        {
            return FachadaAsignaturas.VisualizarAsignatura(idAsignatura);
        }

        public static bool RegistrarAsignatura(ASIGNATURA oAsignatura)
        {
            return FachadaAsignaturas.RegistrarAsignatura(oAsignatura);

        }

        public static bool ModificarAsignatura(ASIGNATURA oAsignatura)
        {
            return FachadaAsignaturas.ModificarAsignatura(oAsignatura);
        }

        public static bool EliminarAsignatura(decimal idAsignatura)
        {
            return FachadaAsignaturas.EliminarAsignatura(idAsignatura);
        }
        public static bool CargarInformacion(string archivo)
        {
            return FachadaAsignaturas.CargarInformacion(archivo);
        }
        #endregion
        //Metodos dirigidos a Fachada Plan Estudio en el dominio de Gestion de Programas
        #region Metodos Gestion Plan Estudio

        public static List<PLANESTUDIO> ConsultarPlanestudios(string palabraBusqueda)
        {
            return FachadaPlanesEstudio.ConsultarPlanesEstudios(palabraBusqueda);
        }
        #endregion
        //Metodos dirigidos a Fachada Gestion Cursos en el dominio de Gestion de Programas
        #region Metodos Gestion Cursos
        public static List<CURSO> ConsultarCursos(decimal idCurso, string nombreCurso)
        {
            return FachadaCursos.ConsultarCursos(idCurso, nombreCurso);
        }

        public static CURSO VisualizarCurso(decimal idCurso)
        {
            return FachadaCursos.VisualizarCurso(idCurso);
        }

        public static bool RegistrarCurso(CURSO oCurso)
        {
            return FachadaCursos.RegistrarCurso(oCurso);
        }

        public static bool ModificarCurso(CURSO oCurso)
        {
            return FachadaCursos.ModificarCurso(oCurso);
        }

        public static bool EliminarCurso(decimal idCurso)
        {
            return FachadaCursos.EliminarCurso(idCurso);
        }
        #endregion
    }
}
