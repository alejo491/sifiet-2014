using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionContenidos.Datos.Modelo;

namespace SIFIET.GestionContenidos.Aplicacion
{
    public static class FachadaEtiquetas
    {
        public static ETIQUETA ConsultarEtiqueta(decimal idEtiqueta)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioEtiquetas.ConsultarEtiqueta(idEtiqueta);
        }

        public static List<ETIQUETA> ConsultarEtiquetas(string busqueda)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioEtiquetas.ConsultarEtiquetas(busqueda);
        }

        public static bool RegistrarEtiqueta(ETIQUETA objEtiqueta)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioEtiquetas.RegistrarEtiqueta(objEtiqueta);
        }

        public static bool EditarEtiqueta(ETIQUETA objEtiqueta)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioEtiquetas.EditarEtiqueta(objEtiqueta);
        }

        public static bool EliminarEtiqueta(decimal idEtiqueta)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioEtiquetas.EliminarEtiqueta(idEtiqueta);
        }
    }
}
