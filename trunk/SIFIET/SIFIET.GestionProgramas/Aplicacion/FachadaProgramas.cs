using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Datos.Modelo;
using System.Data;
using SIFIET.GestionProgramas.Dominio.Servicios;


namespace SIFIET.GestionProgramas.Aplicacion
{
    public static class FachadaProgramas
    {
        public static PROGRAMA ConsultarProgramaAcademico(decimal idPrograma) {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioProgramas.ConsultarProgramaAcademico(idPrograma);
        }

        public static List<PROGRAMA> ConsultarProgramasAcademicos(string estado, string campo, string busqueda)
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioProgramas.ConsultarProgramasAcademicos(estado, campo, busqueda);
        }

        public static List<PROGRAMA> ConsultarProgramasAcademicos()
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioProgramas.ConsultarProgramasAcademicos();
        }

        public static bool RegistrarProgramaAcademico(PROGRAMA objPrograma)
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioProgramas.RegistrarProgramaAcademico(objPrograma);
        }

        public static bool EditarProgramaAcademico(PROGRAMA objPrograma) 
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioProgramas.EditarProgramaAcademico(objPrograma);
        }

        public static bool EliminarProgramaAcademico(decimal idPrograma)
        {
            return SIFIET.GestionProgramas.Dominio.Servicios.ServicioProgramas.EliminarProgramaAcademico(idPrograma);
        }

        public static bool CargarInformacion(DataSet datosExcel)
        {
           return SIFIET.GestionProgramas.Dominio.Servicios.ServicioProgramas.CargarInformacion(datosExcel);
        }

        public static bool VerificarExistenciaPrograma(string nombrePrograma)
        {
            return ServicioProgramas.VerificarExistenciaPrograma(nombrePrograma);
        }

        public static string ObtenerIdPrograma(string nombrePrograma)
        {
            return ServicioProgramas.ObtenerIdPrograma(nombrePrograma);
        }

        public static bool VerificarExistenciaFacultad(string nombreFacultad)
        {
            return ServicioProgramas.VerificarExistenciaFacultad(nombreFacultad);
        }
    }
}
