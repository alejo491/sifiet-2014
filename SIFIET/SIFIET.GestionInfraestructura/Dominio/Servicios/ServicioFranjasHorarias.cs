using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Datos.Modelo;

namespace SIFIET.GestionInfraestructura.Dominio.Servicios
{
    class ServicioFranjasHorarias
    {
        public static List<FRANJA_HORARIA> ConsultarFranjaHoraria(decimal idSalon=0, string dia = "",
            string horaInicio = "", string horaFin = "")
        {
            var db = new GestionInfraestructuraEntities();
            var lstFranjaHoraria = new List<FRANJA_HORARIA>();
            if (idSalon == 0)
            {
                lstFranjaHoraria = db.FRANJA_HORARIA.ToList();
            }
            else if (dia.Equals("") && horaInicio.Equals("") && horaFin.Equals(""))
            {
                var agrupacion =
                    (from franja in db.FRANJA_HORARIA where franja.IDENTIFICADORSALON == idSalon && franja.ESTADOFRANJA.Trim().Equals("Disponible") group franja by franja.DIAFRANJA.Trim() into f select f).ToList();

                lstFranjaHoraria = new List<FRANJA_HORARIA>();
                foreach (var grupo in agrupacion)
                {
                    var oFranja = new FRANJA_HORARIA();
                    oFranja.DIAFRANJA = grupo.Key;
                    oFranja.IDENTIFICADORFRANJA=grupo.First().IDENTIFICADORFRANJA;
                    lstFranjaHoraria.Add(oFranja);
                }
            }
            else if (!dia.Equals("") && horaInicio.Equals("") && horaFin.Equals(""))
            {
                var agrupacion =
                    (from franja in db.FRANJA_HORARIA where franja.IDENTIFICADORSALON == idSalon && franja.ESTADOFRANJA.Trim().Equals("Disponible") group franja by franja.DIAFRANJA.Trim() into f select f).ToList();

                lstFranjaHoraria = new List<FRANJA_HORARIA>();
                foreach (var grupo in agrupacion)
                {
                    var oFranja = new FRANJA_HORARIA();
                    oFranja.DIAFRANJA = grupo.Key;
                    lstFranjaHoraria.Add(oFranja);
                }
            }
            else if (!dia.Equals("") && !horaInicio.Equals("") && horaFin.Equals(""))
            {
                
            }
            else if (!dia.Equals("") && !horaInicio.Equals("") && !horaFin.Equals(""))
            {
                
            }
            return lstFranjaHoraria;
        }
    }
}
