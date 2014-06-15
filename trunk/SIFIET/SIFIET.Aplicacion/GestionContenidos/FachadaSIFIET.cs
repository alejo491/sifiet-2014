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
    }
}
