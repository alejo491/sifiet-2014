using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Aplicacion;
using SIFIET.GestionContenidos.Datos.Modelo;

namespace SIFIET.Aplicacion
{
    public partial class FachadaSIFIET
    {

        #region Metodos Gestion de Etiquetas

        public static ETIQUETA ConsultarEtiqueta(decimal idPEtiqueta)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaEtiquetas.ConsultarEtiqueta(idPEtiqueta);
        }

        public static List<ETIQUETA> ConsultarEtiquetas(string busqueda)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaEtiquetas.ConsultarEtiquetas(busqueda);
        }

        public static bool RegistrarEtiqueta(ETIQUETA objEtiqueta)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaEtiquetas.RegistrarEtiqueta(objEtiqueta);
        }

        public static bool EditarEtiqueta(ETIQUETA objEtiqueta)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaEtiquetas.EditarEtiqueta(objEtiqueta);
        }

        public static bool EliminarEtiqueta(decimal idEtiqueta)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaEtiquetas.EliminarEtiqueta(idEtiqueta);
        }

        #endregion

        #region Metodos Gestion de Categorias

        public static List<CATEGORIA> ConsultarCategorias(string busqueda)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaCategorias.ConsultarCategorias(busqueda);
        }

        public static decimal RegistrarCategoria(CATEGORIA categoriaIn)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaCategorias.RegistrarCategoria(categoriaIn);
        }

        public static CATEGORIA ConsultarCategoria(decimal idCategoria)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaCategorias.ConsultarCategoria(idCategoria);
        }

        public static decimal RegistrarAtributo(ATRIBUTO oAtributo)
        {
            return GestionContenidos.Aplicacion.FachadaCategorias.RegistrarAtributo(oAtributo);
        }

        public static bool exiteAtributoCategoria(ATRIBUTO oAtributo)
        {
            return GestionContenidos.Aplicacion.FachadaCategorias.existeAtributoCategoria(oAtributo);
        }

        public static bool EliminarAtributoContenido(decimal idCategoria, decimal idAtributo)
        {
            return GestionContenidos.Aplicacion.FachadaCategorias.EliminarAtributoContenido(idCategoria, idAtributo);
        }
        #endregion

        #region Metodos Gestion Contenido
        public static List<CONTENIDO> ConsultarContenidos(decimal idContenido, string nombreContenido, string estado)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaContenidos.ConsultarContenidos(idContenido, nombreContenido, estado);
        }
        public static CONTENIDO VisualizarContenido(decimal idContenido)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaContenidos.VisualizarContenido(idContenido);
        }
        public static bool RegistrarContenido(CONTENIDO objContenido, List<ATRIBUTO> atributos)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaContenidos.RegistrarContenido(objContenido, atributos);
        }

        public static bool ModificarContenido(CONTENIDO objContenido)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaContenidos.ModificarContenido(objContenido);
        }

        public static bool EliminarContenido(decimal idContenido)
        {
            return SIFIET.GestionContenidos.Aplicacion.FachadaContenidos.EliminarContenido(idContenido);
        }
        #endregion
    }
}
