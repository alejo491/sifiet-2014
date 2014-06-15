using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionContenidos.Datos.Modelo;

namespace SIFIET.GestionContenidos.Aplicacion
{
    public static class FachadaCategorias
    {       
        public static List<CATEGORIA> ConsultarCategorias(string busqueda)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioCategorias.ConsultarCategorias(busqueda);
        }

        public static decimal RegistrarCategoria(CATEGORIA categoriaIn)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioCategorias.RegistrarCategoria(categoriaIn);
        }

        public static CATEGORIA ConsultarCategoria(decimal idCategoria)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioCategorias.ConsultarCategoria(idCategoria);
        }

        public static decimal RegistrarAtributo(ATRIBUTO oAtributo)
        {
            return Dominio.Servicios.ServicioCategorias.RegistrarAtributo(oAtributo);
        }

        public static bool existeAtributoCategoria(ATRIBUTO oAtributo)
        {
            return Dominio.Servicios.ServicioCategorias.existeAtributoCategoria(oAtributo);
        }

        public static bool EliminarAtributoContenido(decimal idCatecoria, decimal idAtributo)
        {
            return Dominio.Servicios.ServicioCategorias.EliminarAtributoContenido(idCatecoria, idAtributo);
        }
    }
}
