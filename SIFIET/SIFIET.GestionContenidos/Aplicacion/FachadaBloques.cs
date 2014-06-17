using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionContenidos.Datos.Modelo;

namespace SIFIET.GestionContenidos.Aplicacion
{
    public static class FachadaBloques
    {
        public static List<BLOQUE> ConsultarBloques(string idBloque)
        {
            return SIFIET.GestionContenidos.Dominio.Servicios.ServicioBloques.ConsultarBloques(idBloque);
        }
    }
}
