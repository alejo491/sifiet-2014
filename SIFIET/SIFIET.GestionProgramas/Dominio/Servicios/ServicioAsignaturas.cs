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
                    asignatura = asignaturaModificada;
                    db.SaveChanges();
                }
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

                    var asg = new ASIGNATURA();
                    {
                        asg.IDENTIFICADORASIGNATURA = decimal.Parse(campos[0]);
                        asg.IDENTIFICADORPLANESTUDIOS = decimal.Parse(campos[1]);
                        asg.NOMBREASIGNATURA = campos[2];
                        asg.CORREQUISITOSASIGNATURA = campos[3];
                        asg.PREREQUISITOSASIGNATURA = campos[4];
                        asg.SEMESTREASIGNATURA = short.Parse(campos[5]);
                        asg.CREDITOSASIGNATURA = decimal.Parse(campos[6]);
                        asg.MODALIDADASIGNATURA = campos[7];
                        asg.CLASIFICACIONASIGNATURA = campos[8];
                        asg.ESTADOASIGNATURA = campos[9];
                    }
                    db.ASIGNATURAs.Add(asg);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
