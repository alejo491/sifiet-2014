using SIFIET.GestionContenidos.Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionContenidos.Aplicacion
{
    public static class FachadaBloques
    {
        public static List<BLOQUE> ConsultarBloques()
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioBloques.ConsultarBloques();
        }

        public static bool AsignarCategoriasBloques(string bloque1, string bloque2, string bloque3, string bloque4) {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioBloques.AsignarCategoriasBloques(bloque1, bloque2, bloque3, bloque4);
        }
    }
}
