using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionProgramas.Datos.Modelo;

namespace SIFIET.GestionProgramas.Dominio.Servicios
{
    static class ServicioAsignaturas
    {
        public static List<ASIGNATURA> ConsultarAsignaturas(string palabraBusqueda)
        {
          //  try
          //  {

          try
          {
                var db = new GestionProgramasEntities();
                List<ASIGNATURA> lista = new List<ASIGNATURA>();
                if (String.IsNullOrEmpty(palabraBusqueda))
                {
                    lista = (from e in db.ASIGNATURAs
                             select e).ToList();
                    return lista;
                }
                else
                {
                    lista = (from e in db.ASIGNATURAs
                             where (e.NOMBREASIGNATURA.Contains(palabraBusqueda) | e.CODIGOASIGNATURA.Contains(palabraBusqueda))
                             select e).ToList();
                    return lista;

                }
           // }
           // catch (Exception)
           // {
            //    return new List<ASIGNATURA>();
           // }

           }
           catch (Exception)
           {
               return new List<ASIGNATURA>();
           }


        }
        public static ASIGNATURA VisualizarAsignatura(decimal idAsignatura)
        {
            try
            {
                var db = new GestionProgramasEntities();
                var asignatura = (from e in db.ASIGNATURAs
                                  where e.IDENTIFICADORASIGNATURA == idAsignatura
                                  select e).First();
                return asignatura;

            }
            catch (Exception)
            {
                return null;
            }

        }

        internal static bool RegistrarAsignatura(ASIGNATURA nuevAsignatura)
        {
            try
            {
                var db = new GestionProgramasEntities();

                var asg = new ASIGNATURA();
                {
                    asg.EdicionOmodificacion = "registrar";
                    asg = nuevAsignatura;
                }
                db.ASIGNATURAs.Add(asg);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        internal static bool ModificarAsignatura(ASIGNATURA asignaturaModificada)
        {
            try
            {
                var db = new GestionProgramasEntities();

                var asignatura = (from asig in db.ASIGNATURAs where asig.IDENTIFICADORASIGNATURA == asignaturaModificada.IDENTIFICADORASIGNATURA select asig).First();
                {
                    asignatura.EdicionOmodificacion = "modificacion";
                    asignatura.CODIGOASIGNATURA = asignaturaModificada.CODIGOASIGNATURA;
                    asignatura.IDENTIFICADORPLANESTUDIOS = asignaturaModificada.IDENTIFICADORPLANESTUDIOS;
                    asignatura.NOMBREASIGNATURA = asignaturaModificada.NOMBREASIGNATURA;
                    asignatura.CORREQUISITOSASIGNATURA = asignaturaModificada.CORREQUISITOSASIGNATURA;
                    asignatura.PREREQUISITOSASIGNATURA = asignaturaModificada.PREREQUISITOSASIGNATURA;
                    asignatura.SEMESTREASIGNATURA = asignaturaModificada.SEMESTREASIGNATURA;
                    asignatura.CREDITOSASIGNATURA = asignaturaModificada.CREDITOSASIGNATURA;
                    asignatura.MODALIDADASIGNATURA = asignaturaModificada.MODALIDADASIGNATURA;
                    asignatura.CLASIFICACIONASIGNATURA = asignaturaModificada.CLASIFICACIONASIGNATURA;
                    asignatura.ESTADOASIGNATURA = asignaturaModificada.ESTADOASIGNATURA;
                    asignatura.DESCRIPCIONASIGNATURA = asignaturaModificada.DESCRIPCIONASIGNATURA;
                }
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        internal static bool EliminarAsignatura(decimal idAsignatura)
        {
            try
            {
                var db = new GestionProgramasEntities();

                var asg = (from asig in db.ASIGNATURAs where asig.IDENTIFICADORASIGNATURA == idAsignatura select asig).First();
                db.ASIGNATURAs.Remove(asg);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        internal static bool CargarInformacion(string archivo)
        {
            String linea;
            StreamReader f = new StreamReader(archivo);
            try
            {
                while ((linea = f.ReadLine()) != null)
                {
                    string[] campos = linea.Split(',');
                    var db = new GestionProgramasEntities();

                    var asg = new ASIGNATURA()
                    {
                        IDENTIFICADORASIGNATURA = decimal.Parse(campos[0]),
                        IDENTIFICADORPLANESTUDIOS = decimal.Parse(campos[1]),
                        NOMBREASIGNATURA = campos[2],
                        CORREQUISITOSASIGNATURA = campos[3],
                        PREREQUISITOSASIGNATURA = campos[4],
                        SEMESTREASIGNATURA = short.Parse(campos[5]),
                        CREDITOSASIGNATURA = decimal.Parse(campos[6]),
                        MODALIDADASIGNATURA = campos[7],
                        CLASIFICACIONASIGNATURA = campos[8],
                        ESTADOASIGNATURA = campos[9],
                        DESCRIPCIONASIGNATURA = campos[10],
                        CODIGOASIGNATURA = campos[11],
                        EdicionOmodificacion = "registrar"
                    };
                    //RegistrarAsignatura(asg);
                    db.ASIGNATURAs.Add(asg);
                    db.SaveChanges();
                }
                f.Dispose();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
