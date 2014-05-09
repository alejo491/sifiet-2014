using SIFIET.GestionProgramas.Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionProgramas.Dominio.Servicios
{
    static class ServicioGInvestigacion
    {
        public static List<GRUPO_INVESTIGACION> ConsultarGruposInvestigacion()
        {
            var db = new GestionProgramasEntities();
            List<GRUPO_INVESTIGACION> lista = (from e in db.GRUPO_INVESTIGACION
                                               select e).ToList();
            return lista;
        }


        public static List<GRUPO_INVESTIGACION> ConsultarGruposInvestigacionPorNombre(string busqueda,string estado)
        {

            var db = new GestionProgramasEntities();
            List<GRUPO_INVESTIGACION> lista = (from e in db.GRUPO_INVESTIGACION
                                               where e.NOMBREGRUPOINVESTIGACION.Contains(busqueda) && e.ESTADOGRUPOINVESTIGACION.Equals(estado)
                                               select e).ToList();
            return lista;
        }

        public static List<GRUPO_INVESTIGACION> ConsultarGruposInvestigacionPorCodigo(string busqueda, string estado)
        {

            var db = new GestionProgramasEntities();
            List<GRUPO_INVESTIGACION> lista = (from e in db.GRUPO_INVESTIGACION
                                               where e.CODIGOGRUPOINVESTIGACION.Contains(busqueda) && e.ESTADOGRUPOINVESTIGACION.Equals(estado)
                                               select e).ToList();
            return lista;
        }

        public static GRUPO_INVESTIGACION ConsultarGrupoInvestigacionPorCodigo(string codigo)
        {

            var db = new GestionProgramasEntities();
            GRUPO_INVESTIGACION gInvestigacion = (from g in db.GRUPO_INVESTIGACION
                                               where g.CODIGOGRUPOINVESTIGACION == codigo
                                               select g).FirstOrDefault();
            return gInvestigacion;
        }

        public static GRUPO_INVESTIGACION ConsultarGrupoInvestigacionPorNombre(string nombre)
        {

            var db = new GestionProgramasEntities();
            GRUPO_INVESTIGACION gInvestigacion = (from g in db.GRUPO_INVESTIGACION
                                                  where g.NOMBREGRUPOINVESTIGACION == nombre
                                                  select g).FirstOrDefault();
            return gInvestigacion;
        }

        public static GRUPO_INVESTIGACION ConsultarGrupoInvestigacion(int idGinvestigacion)
        {
            var db = new GestionProgramasEntities();
            GRUPO_INVESTIGACION lista = (from e in db.GRUPO_INVESTIGACION
                                               where e.IDENTIFICADORGRUPOINVES == idGinvestigacion
                                               select e).FirstOrDefault();
            return lista;
        }
        public static void RegistrarGrupoInvestigacion(GRUPO_INVESTIGACION grupo)
        {
            var db = new GestionProgramasEntities();
            grupo.ESTADOGRUPOINVESTIGACION = "Activo";
            grupo.EdicionOmodificacion = "registro";
            db.GRUPO_INVESTIGACION.Add(grupo);
            db.SaveChanges();
        }

        public static List<DEPARTAMENTO> ConsultarDepartamentos()
        {
            var db = new GestionProgramasEntities();
            List<DEPARTAMENTO> lista = (from e in db.DEPARTAMENTOes
                                               select e).ToList();
            return lista;

        }

        public static List<DOCENTE> ConsultarDocentes()
        {

            var db = new GestionProgramasEntities();
            List<DOCENTE> lista = (from e in db.DOCENTEs
                                               select e).ToList();
            return lista;
        }

        internal static void ModificarGrupoInvestigacion(GRUPO_INVESTIGACION grupoIvestigacionModificado)
        {
            var db = new GestionProgramasEntities();
            var grupo = (from g in db.GRUPO_INVESTIGACION where g.IDENTIFICADORGRUPOINVES == grupoIvestigacionModificado.IDENTIFICADORGRUPOINVES select g).First();
            grupo.EdicionOmodificacion = "modificacion";
            grupo.IDENTIFICADORUSUARIO = grupoIvestigacionModificado.IDENTIFICADORUSUARIO;
            grupo.IDENTIFICADORDEPARTAMENTO = grupoIvestigacionModificado.IDENTIFICADORDEPARTAMENTO;
            grupo.NOMBREGRUPOINVESTIGACION = grupoIvestigacionModificado.NOMBREGRUPOINVESTIGACION;
            grupo.ESTADOGRUPOINVESTIGACION = grupoIvestigacionModificado.ESTADOGRUPOINVESTIGACION;
            grupo.DESCRIPCIONGRUPOINVESTIGACION = grupoIvestigacionModificado.DESCRIPCIONGRUPOINVESTIGACION;
            grupo.CODIGOGRUPOINVESTIGACION = grupoIvestigacionModificado.CODIGOGRUPOINVESTIGACION;            
            db.SaveChanges();
        }

        public static void EliminarGrupoInvestigacion(int idGinvestigacion)
        {
            var db = new GestionProgramasEntities();
            var grupoInvestigacion = db.GRUPO_INVESTIGACION.Find(idGinvestigacion);
            db.GRUPO_INVESTIGACION.Remove(grupoInvestigacion);
            db.SaveChanges();
        }
    }
}
