using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            List<FRANJA_HORARIA> lstFranjaHoraria;
            if (idSalon == 0)
            {
                return  db.FRANJA_HORARIA.ToList();
            }
            else
            {
                lstFranjaHoraria =
                    (from franja in db.FRANJA_HORARIA where franja.IDENTIFICADORSALON == idSalon select franja).ToList();
                return lstFranjaHoraria;
            }
        }

        public static List<string> ConsultarFranjaHorariaDisponible(decimal idSalon, string dia = "",
            string horaInicio = "", string horaFin = "")
        {
            var db = new GestionInfraestructuraEntities();
            var resultado = new List<string>();
            resultado.Add("Seleccionar");
            if (dia.Equals("") && horaInicio.Equals("") && horaFin.Equals(""))
            {

                /*var agrupacion =
                    (from franja in db.FRANJA_HORARIA where franja.IDENTIFICADORSALON == idSalon && franja.ESTADOFRANJA.Trim().Equals("Activo") group franja by franja.DIAFRANJA.Trim() into f select f).ToList();

                lstFranjaHoraria = new List<FRANJA_HORARIA>();
                foreach (var grupo in agrupacion)
                {
                    var oFranja = new FRANJA_HORARIA();
                    oFranja.DIAFRANJA = grupo.Key;
                    oFranja.IDENTIFICADORFRANJA=grupo.First().IDENTIFICADORFRANJA;
                    lstFranjaHoraria.Add(oFranja);
                }*/
                var lstDias = CrearListaDias();
                //var lstHoras = CrearListaHoras();
                foreach (var diaSemana in lstDias)
                {
                    var auxFranjas = (from auxFranja in db.FRANJA_HORARIA
                        where auxFranja.DIAFRANJA.ToLower().Trim().Equals(
                            diaSemana.ToLower().Trim()) && auxFranja.IDENTIFICADORSALON == idSalon
                        select auxFranja
                        ).ToList();
                    if (auxFranjas.Any())
                    {
                        for (var hora=6; hora<=22; hora++)
                        {
                            var auxFranjasHora = (from auxFranja in auxFranjas
                                where auxFranja.HORAINICIOFRANJA.ToLower().Trim().Equals(
                                    hora.ToString().ToLower().Trim())
                                select auxFranja).FirstOrDefault();
                            if (auxFranjasHora==null)
                            {
                                resultado.Add(diaSemana);
                                break;
                            }
                            hora=int.Parse(auxFranjasHora.HORAFINFRANJA)-1;
                        }
                    }
                    else
                    {
                        resultado.Add(diaSemana);
                    }
                }
            }
            else if (!dia.Equals("") && horaInicio.Equals("") && horaFin.Equals(""))
            {
                //var lstHoras = CrearListaHoras();
                for (var horaDia=6;horaDia<=22;horaDia++)
                {
                    string horaDiaStr = horaDia.ToString();
                    var auxFranjasHora = (from franja in db.FRANJA_HORARIA
                        where franja.IDENTIFICADORSALON == idSalon
                              && franja.DIAFRANJA.Trim().ToLower().Equals(dia.Trim().ToLower())
                              && franja.HORAINICIOFRANJA.Trim().ToLower().Equals(horaDiaStr.Trim().ToLower())
                        select franja).FirstOrDefault();
                    if (auxFranjasHora==null)
                    {
                        resultado.Add(horaDia.ToString());
                    }
                    else
                    {
                        horaDia = int.Parse(auxFranjasHora.HORAFINFRANJA)-1;
                    }
                }
            }
            else if (!dia.Equals("") && !horaInicio.Equals("") && !horaInicio.Equals("Seleccionar") && horaFin.Equals(""))
            {
                int desde=int.Parse(horaInicio.Trim());
                for (var i = desde + 1; i <= 23; i++)
                {
                    resultado.Add(i.ToString());
                    string horaDiaStr = i.ToString();
                    var auxFranjasHora = (from franja in db.FRANJA_HORARIA
                                          where franja.IDENTIFICADORSALON == idSalon
                                                && franja.DIAFRANJA.Trim().ToLower().Equals(dia.Trim().ToLower())
                                                && franja.HORAINICIOFRANJA.Trim().Equals(horaDiaStr.Trim())
                                          select franja).FirstOrDefault();
                    if (auxFranjasHora != null)
                    {
                        break;
                    }
                }
            }
            return resultado;
        }
        private List<FRANJA_HORARIA> crearFranjaHorarias()
        {
            var lstDias = CrearListaDias();
            var lstHoras = CrearListaHoras();
            var lstFranjas = new List<FRANJA_HORARIA>();
            foreach (var dia in lstDias)
            {
                foreach (var hora in lstHoras)
                {
                    var oFraja = new FRANJA_HORARIA()
                    {
                        DIAFRANJA = dia,
                        HORAINICIOFRANJA = hora.ToString(),
                        HORAFINFRANJA = (hora+1).ToString(),
                    };
                    lstFranjas.Add(oFraja);
                }
            }
            return lstFranjas;
        }

        private static List<string> CrearListaDias()
        {
            var lstDias = new List<string> {"lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo"};
            return lstDias;
        }

        private static List<int> CrearListaHoras()
        {
            var lstHoras = new List<int>();
            for (var i = 6; i < 22; i++)
            {
                lstHoras.Add(i);
            }
            return lstHoras;
        } 
    }
}
