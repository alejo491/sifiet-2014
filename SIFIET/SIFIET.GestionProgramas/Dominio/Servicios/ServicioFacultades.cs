using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Datos.Modelo;


namespace SIFIET.GestionProgramas.Dominio.Servicios
{
    class ServicioFacultades
    {

        public static List<FACULTAD> obtenerNombreFacultades()
        {
            var db = new GestionProgramasEntities();
            var facultades = from m in db.FACULTADs
                             select m;
            return facultades.ToList();
        }
    }
}
