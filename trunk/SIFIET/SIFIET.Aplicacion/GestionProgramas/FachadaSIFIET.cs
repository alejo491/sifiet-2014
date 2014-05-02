using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Aplicacion;
using SIFIET.GestionInfraestructura.Datos.Modelo;
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

        public static bool CargarInformacionAsignatura(string archivo)
        {
            return FachadaAsignaturas.CargarInformacion(archivo);
        }

        public static bool VerificarCampoPlanEstudios(string nombrePlanEstudios)
        {
            return FachadaAsignaturas.VerificarCampoPlanEstudios(nombrePlanEstudios);
        }

        public static bool VerificarCampoCoRequisitosAsignatura(string coRequisitosAsignatura)
        {
            return FachadaAsignaturas.VerificarCampoCoRequisitosAsignatura(coRequisitosAsignatura);
        }

        public static bool VerificarCampoPreRequisitosAsignatura(string preRequisitosAsignatura)
        {
            return FachadaAsignaturas.VerificarCampoPreRequisitosAsignatura(preRequisitosAsignatura);
        }

        public static bool VerificarExistenciaAsignatura(string nombreAsignatura)
        {
            return FachadaCursos.VerificarExistenciaAsignatura(nombreAsignatura);
        }

        public static string ObtenerIdPlanEstudios(string nombre)
        {
            return FachadaAsignaturas.ObtenerIdPlanEstudios(nombre);
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

        public static bool CargarInformacionCurso(string archivo)
        {
            return FachadaCursos.CargarInformacion(archivo);
        }

        public static bool VerificarCampoDocente(string nombre)
        {
            return FachadaCursos.VerificarCampoDocente(nombre);
        }

        public static string ObtenerIdAsignatura(string nombre)
        {
            return FachadaCursos.ObtenerIdAsignatura(nombre);
        }

        public static string ObtenerIdUsuario(string nombreDocente)
        {
            return FachadaCursos.ObtenerIdUsuario(nombreDocente);
        }

        public static bool VerificarCampoAsignatura(string nombreAsignatura)
        {
            return FachadaCursos.VerificarCampoAsignatura(nombreAsignatura);
        }

        public static bool VerificarExistenciaCurso(string nombreCurso)
        {
            return FachadaCursos.VerificarExistenciaCurso(nombreCurso);
        }
        #endregion
        //Metodos dirigidos a Fachada Gestion de Grupos de Investigacion en el dominio de Gestion de Programas
        #region Metodos Gestion Grupos de Investigacion

        public static List<GRUPO_INVESTIGACION> ConsultarGruposInvestigacion()
        {
            return FachadaGInvestigacion.ConsultarGruposInvestigacion();
        }

        public static GRUPO_INVESTIGACION ConsultarGrupoInvestigacion(int idGinvestigacion )
        {
            return FachadaGInvestigacion.ConsultarGrupoInvestigacion(idGinvestigacion);
        }

        public static List<DEPARTAMENTO> ConsultarDepartamentos()
        {
            return FachadaGInvestigacion.ConsultarDepartamentos();
        
        }

        public static List<GRUPO_INVESTIGACION> ConsultarGruposInvestigacionPorNombre(string busqueda)
        {

            return FachadaGInvestigacion.ConsultarGruposInvestigacionPorNombre(busqueda);
        }

        public static GRUPO_INVESTIGACION ConsultarGrupoInvestigacionPorNombre(string nombre)
        {
            return FachadaGInvestigacion.ConsultarGrupoInvestigacionPorCodigo(nombre);
        }

        public static void RegistrarGrupoInvestigacion(GRUPO_INVESTIGACION grupo)
        {
            FachadaGInvestigacion.RegistrarGrupoInvestigacion(grupo);
        }

        public static List<DOCENTE> ConsultarDocentes()
        {

            return FachadaGInvestigacion.ConsultarDocentes();
        }

        public static void ModificarGrupoInvestigacion(GRUPO_INVESTIGACION grupo)
        {
            FachadaGInvestigacion.ModificarGrupoInvestigacion(grupo);
        }

        public static void EliminarGrupoInvestigacion(int idGinvestigacion)
        {
            FachadaGInvestigacion.EliminarGrupoInvestigacion(idGinvestigacion);
        }

        #endregion


        public static List<FRANJA_HORARIA> ObtenarHorarioCurso(decimal idCurso)
        {
            return FachadaSalones.ObtenarHorarioCurso(idCurso);
        }

        public static bool CargarInformacionSalones(string archivo)
        {
            return FachadaSalones.CargarInformacion(archivo);
        }
    }
}
