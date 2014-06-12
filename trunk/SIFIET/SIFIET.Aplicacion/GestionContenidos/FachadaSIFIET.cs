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
    }
}
