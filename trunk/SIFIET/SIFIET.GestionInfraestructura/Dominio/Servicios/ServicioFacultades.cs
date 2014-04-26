using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Datos.Modelo;

namespace SIFIET.GestionInfraestructura.Dominio.Servicios
{
    static class ServicioFacultades
    {
        public static List<FACULTAD> ConsultarFacultades(decimal idFacultad, string nomFacultad)
        {
            try
            {
                var db = new GestionInfraestructuraEntities();
                var lista = new List<FACULTAD>();
                if (String.IsNullOrEmpty(nomFacultad) & idFacultad == 0)
                {
                    lista = (from e in db.FACULTADs
                             select e).ToList();
                    return lista;
                }
                else
                {
                    if (idFacultad != 0)
                    {
                        lista = (from e in db.FACULTADs
                                 where
                                     (e.IDENTIFICADORFACULTAD == idFacultad)
                                 select e).ToList();
                        return lista;
                    }
                    else
                    {
                        lista = (from e in db.FACULTADs
                                 where
                                     (e.NOMBREFACULTAD.ToLower().Contains(nomFacultad.ToLower()))
                                 select e).ToList();
                        return lista;
                    }

                }
            }
            catch (Exception)
            {
                return new List<FACULTAD>();
            }


        }
    }
}
