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

        public static PROGRAMA ConsultarProgramaAcademico(decimal idPrograma)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaProgramas.ConsultarProgramaAcademico(idPrograma);
        }

        public static List<PROGRAMA> ConsultarProgramasAcademicos(string estado, string campo, string busqueda)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaProgramas.ConsultarProgramasAcademicos(estado, campo,busqueda);
        }

        public static List<PROGRAMA> ConsultarProgramasAcademicos()
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaProgramas.ConsultarProgramasAcademicos();
        }

        public static bool RegistrarProgramaAcademico(PROGRAMA objPrograma)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaProgramas.RegistrarProgramaAcademico(objPrograma);
        }
        public static bool EditarProgramaAcademico(PROGRAMA objPrograma)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaProgramas.EditarProgramaAcademico(objPrograma);
        }

        public static bool EliminarProgramaAcademico(decimal idPrograma)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaProgramas.EliminarProgramaAcademico(idPrograma);
        }

        public static bool CargarInformacion(string archivo)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaProgramas.CargarInformacion(archivo);
        }

        public static bool VerificarExistenciaFacultad(string nombreFacultad)
        {
            return FachadaProgramas.VerificarExistenciaFacultad(nombreFacultad);
        }

        public static bool VerificarExistenciaPrograma(string nombrePrograma)
        {
            return FachadaProgramas.VerificarExistenciaPrograma(nombrePrograma);
        }

        public static string ObtenerIdFacultadProg(string nombreFacultad)
        {
            return FachadaProgramas.ObtenerIdFacultad(nombreFacultad);
        }
        public static bool CargarInformacionPrograma(string archivo)
        {
            return FachadaProgramas.CargarInformacion(archivo);
        }

        #endregion
        //Metodos dirigidos a Fachada Asignaturas en el dominio de Gestion de Programas
        #region Metodos Gestion de Asignaturas

        public static List<GestionProgramas.Datos.Modelo.ASIGNATURA> ConsultarAsignaturas(decimal idAsignatura, string nombreAsignatura, string estado)
        {
            return FachadaAsignaturas.ConsultarAsignaturas(idAsignatura, nombreAsignatura, estado);
        }

        public static GestionProgramas.Datos.Modelo.ASIGNATURA VisualizarAsignatura(decimal idAsignatura)
        {
            return FachadaAsignaturas.VisualizarAsignatura(idAsignatura);
        }

        public static bool RegistrarAsignatura(GestionProgramas.Datos.Modelo.ASIGNATURA oAsignatura)
        {
            return FachadaAsignaturas.RegistrarAsignatura(oAsignatura);

        }

        public static bool ModificarAsignatura(GestionProgramas.Datos.Modelo.ASIGNATURA oAsignatura)
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

        public static bool VerificarExistenciaAsignatura(string nombreAsignatura, string codigoAsignatura)
        {
            return FachadaAsignaturas.VerificarExistenciaAsignatura(nombreAsignatura, codigoAsignatura);
        }
        #endregion
        //Metodos dirigidos a Fachada Plan de Estudio en el dominio de Plan de Estudios
        #region Metodos Gestion Plan Estudios

        public static PLANESTUDIO ConsultarPlanEstudio(decimal idPlanEstudio)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaPlanesEstudio.ConsultarPlanEstudio(idPlanEstudio);
        }

        public static List<PLANESTUDIO> ConsultarPlanesEstudios(string estado, string campo, string busqueda)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaPlanesEstudio.ConsultarPlanesEstudios(estado, campo, busqueda);
        }

        public static bool RegistrarPlanEstudio(PLANESTUDIO objPlanEstudio)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaPlanesEstudio.RegistrarPlanEstudio(objPlanEstudio);
        }

        public static bool EditarPlanEstudio(PLANESTUDIO objPlanEstudio)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaPlanesEstudio.EditarPlanEstudio(objPlanEstudio);
        }

        public static bool EliminarPlanEstudio(decimal idPlanEstudio)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaPlanesEstudio.EliminarPlanEstudio(idPlanEstudio);
        }

        public static List<ASIGNATURA_PERTENECE_PLAN_ESTU> ConsultarAsignaturasPorPlanEstudio(decimal idPlanEstudio) {
            return FachadaPlanesEstudio.ConsultarAsignaturasPorPlanEstudio(idPlanEstudio);
        }

        public static bool RegistrarAsignaturaPlanEstudio(ASIGNATURA_PERTENECE_PLAN_ESTU objAsignaturaPlanEstudio) {
            return FachadaPlanesEstudio.RegistrarAsignaturaPlanEstudio(objAsignaturaPlanEstudio);
        }

        public static bool EliminarAsignaturaPlanEstudio(decimal idPlanEstudio, decimal idAsignatura)
        {
            return FachadaPlanesEstudio.EliminarAsignaturaPlanEstudio(idPlanEstudio, idAsignatura);
        }

        public static byte[] PlanEstudiosPDF(decimal idPlan )
        {
            return FachadaPlanesEstudio.PlanEstudiosPDF(idPlan);
        }


        // metodo hecho para el modulo de asignatura
        public static List<PLANESTUDIO> ConsultarPlanestudios(string palabraBusqueda)
        {
            return FachadaPlanesEstudio.ConsultarPlanesEstudios(palabraBusqueda);
        }
        #endregion
        //Metodos dirigidos a Fachada Gestion Cursos en el dominio de Gestion de Programas
        #region Metodos Gestion Cursos
        public static List<GestionProgramas.Datos.Modelo.CURSO> ConsultarCursos(decimal idCurso, string nombreCurso)
        {
            return FachadaCursos.ConsultarCursos(idCurso, nombreCurso);
        }

        public static GestionProgramas.Datos.Modelo.CURSO VisualizarCurso(decimal idCurso)
        {
            return FachadaCursos.VisualizarCurso(idCurso);
        }

        public static decimal RegistrarCurso(GestionProgramas.Datos.Modelo.CURSO oCurso)
        {
            return FachadaCursos.RegistrarCurso(oCurso);
        }

        public static bool ModificarCurso(GestionProgramas.Datos.Modelo.CURSO oCurso)
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

        public static bool VerificarCampoDocente(string nombre, string apellido)
        {
            return FachadaCursos.VerificarCampoDocente(nombre, apellido);
        }

        public static string ObtenerIdAsignatura(string nombre)
        {
            return FachadaCursos.ObtenerIdAsignatura(nombre);
        }

        public static string ObtenerIdUsuario(string nombreDocente, string apellidoDocente)
        {
            return FachadaCursos.ObtenerIdUsuario(nombreDocente, apellidoDocente);
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

        public static List<GRUPO_INVESTIGACION> ConsultarGruposInvestigacionPorNombre(string busqueda, string estado)
        {

            return FachadaGInvestigacion.ConsultarGruposInvestigacionPorNombre(busqueda,estado);
        }

        public static List<GRUPO_INVESTIGACION> ConsultarGruposInvestigacionPorCodigo(string busqueda, string estado)
        {

            return FachadaGInvestigacion.ConsultarGruposInvestigacionPorCodigo(busqueda, estado);
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

        /*
        public static bool CargarInformacionSalones(string archivo)
        {
            return FachadaSalones.CargarInformacion(archivo);
        }*/

        public static byte[] AsignaturaPDF(decimal idAsignatura)
        {
            return GestionProgramas.Aplicacion.FachadaAsignaturas.AsignaturaPDF(idAsignatura);
        }

    }
}
