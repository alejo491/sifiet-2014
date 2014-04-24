using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Datos.Modelo;

namespace SIFIET.GestionProgramas.Dominio.Servicios
{
    static class ServicioPlanesEstudio
    {
        public static List<PLANESTUDIO> ConsultarPlanestudios(string palabraBusqueda)
        {
            try
            {
                var db = new GestionProgramasEntities();
                List<PLANESTUDIO> lista = new List<PLANESTUDIO>();
                if (String.IsNullOrEmpty(palabraBusqueda))
                {
                    lista = (from e in db.PLANESTUDIOS select e).ToList();
                    return lista;
                }
                else
                {
                    lista = (from e in db.PLANESTUDIOS
                             where
                                 (e.NOMBREPLANESTUDIOS.Contains(palabraBusqueda) |
                                  e.IDENTIFICADORPLANESTUDIOS == decimal.Parse(palabraBusqueda))
                             select e).ToList();
                    return lista;

                }

            }
            catch (Exception)
            {
                return new List<PLANESTUDIO>();
            }
        }
    }
}
