using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionContenidos.Datos.Modelo;

namespace SIFIET.GestionContenidos.Aplicacion
{
    public static class FachadaContenidos
    {
        public static List<CONTENIDO> ConsultarContenidos(decimal idContenido, string nombreContenido, string estado)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioContenidos.ConsultarContenido(idContenido, nombreContenido, estado);
        }
        public static CONTENIDO VisualizarContenido(decimal idContenido)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioContenidos.VisualizarContenido(idContenido);
        }
        
        public static bool RegistrarContenido(CONTENIDO objContenido, List<ATRIBUTO> atributos, List<ETIQUETA> etiquetas)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioContenidos.RegistrarContenido(objContenido, atributos, etiquetas);
        }

        public static bool ModificarContenido(CONTENIDO objContenido, List<ATRIBUTO> atributos, List<ETIQUETA> etiquetas)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioContenidos.ModificarContenido(objContenido, atributos, etiquetas);
        }

        public static bool EliminarContenido(decimal idContenido)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioContenidos.EliminarContenido(idContenido);
        }
        public static List<ATRIBUTO> ConsultarAtributosDelContenido(decimal idContenido)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioContenidos.ConsultarAtributosDelContenido(idContenido);
        }
        public static List<ETIQUETA> ConsultarEtiquetasDelContenido(decimal idContenido)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioContenidos.ConsultarEtiquetasDelContenido(idContenido);
        }
    }
}
