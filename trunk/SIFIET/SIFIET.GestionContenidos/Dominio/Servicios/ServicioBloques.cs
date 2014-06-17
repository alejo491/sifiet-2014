using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data;
using SIFIET.GestionContenidos.Datos.Modelo;
using System.Diagnostics;

namespace SIFIET.GestionContenidos.Dominio.Servicios
{
    class ServicioBloques
    {
        public static List<BLOQUE> ConsultarBloques(string busqueda)
        {
            var db = new GestionContenidosEntities();
            var bloques = from c in db.BLOQUEs
                          select c;

            if (!String.IsNullOrEmpty(busqueda))
            {
                Decimal id = Decimal.Parse(busqueda); //Pendiente contralar excepcion
                bloques = (from c in db.BLOQUEs
                              where (c.IDENTIFICADORBLOQUE == id)
                              select c);
            }
            return bloques.ToList();
        }
    }
}
