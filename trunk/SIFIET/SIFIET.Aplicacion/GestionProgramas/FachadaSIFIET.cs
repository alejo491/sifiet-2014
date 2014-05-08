﻿using System;
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

        public static List<PROGRAMA> ConsultarProgramasAcademicos(string campo, string busqueda)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaProgramas.ConsultarProgramasAcademicos(campo, busqueda);
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

        public static bool CargarInformacion(DataSet datosExcel)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaProgramas.CargarInformacion(datosExcel);
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
        /*
        public static bool VerificarExistenciaAsignatura(string nombreAsignatura)
        {
            return FachadaCursos.VerificarExistenciaAsignatura(nombreAsignatura);
        }*/

        public static string ObtenerIdPlanEstudios(string nombre)
        {
            return FachadaAsignaturas.ObtenerIdPlanEstudios(nombre);
        }

        #endregion
        //Metodos dirigidos a Fachada Programas en el dominio de Plan de Estudios
        #region Metodos Gestion Plan Estudios

        public static PLANESTUDIO ConsultarPlanEstudio(decimal idPlanEstudio)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaPlanesEstudio.ConsultarPlanEstudio(idPlanEstudio);
        }

        public static List<PLANESTUDIO> ConsultarPlanesEstudios(string campo, string busqueda)
        {
            return SIFIET.GestionProgramas.Aplicacion.FachadaPlanesEstudio.ConsultarPlanesEstudios(campo, busqueda);
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

        // metodo hecho para el modulo de asignatura
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
        /*
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
        }*/
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

        /*
        public static bool CargarInformacionSalones(string archivo)
        {
            return FachadaSalones.CargarInformacion(archivo);
        }*/
    }
}
