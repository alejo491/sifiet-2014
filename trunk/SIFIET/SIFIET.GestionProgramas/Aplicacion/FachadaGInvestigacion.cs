using SIFIET.GestionProgramas.Datos.Modelo;
using SIFIET.GestionProgramas.Dominio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionProgramas.Aplicacion
{
    public static class FachadaGInvestigacion
    {
        public static List<GRUPO_INVESTIGACION> ConsultarGruposInvestigacion()
        {
            return ServicioGInvestigacion.ConsultarGruposInvestigacion();
        }

        public static List<GRUPO_INVESTIGACION> ConsultarGruposInvestigacionPorNombre(string busqueda)
        {

            return ServicioGInvestigacion.ConsultarGruposInvestigacionPorNombre(busqueda);
        }

        public static GRUPO_INVESTIGACION ConsultarGrupoInvestigacionPorCodigo(string codigo)
        {
            return ServicioGInvestigacion.ConsultarGrupoInvestigacionPorCodigo(codigo);
        }

        public static GRUPO_INVESTIGACION ConsultarGrupoInvestigacionPorNombre(string nombre)
        {
            return ServicioGInvestigacion.ConsultarGrupoInvestigacionPorNombre(nombre);
        }

        public static GRUPO_INVESTIGACION ConsultarGrupoInvestigacion(int idGinvestigacion)
        {
            return ServicioGInvestigacion.ConsultarGrupoInvestigacion(idGinvestigacion);
        }

        public static void RegistrarGrupoInvestigacion(GRUPO_INVESTIGACION grupo)
        {
            ServicioGInvestigacion.RegistrarGrupoInvestigacion(grupo);
        }

        public static List<DEPARTAMENTO> ConsultarDepartamentos()
        {
            return ServicioGInvestigacion.ConsultarDepartamentos();

        }

        public static List<DOCENTE> ConsultarDocentes()
        {

            return ServicioGInvestigacion.ConsultarDocentes();
        }

        public static void ModificarGrupoInvestigacion(GRUPO_INVESTIGACION grupo)
        {
            ServicioGInvestigacion.ModificarGrupoInvestigacion(grupo);
        }

        public static void EliminarGrupoInvestigacion(int idGinvestigacion)
        {
            ServicioGInvestigacion.EliminarGrupoInvestigacion(idGinvestigacion);
        }
    }
}
