using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionContenidos.Datos.Modelo;

namespace SIFIET.GestionContenidos.Dominio.Servicios
{
    class ServicioContenidos
    {
        public static List<CONTENIDO> ConsultarContenido(decimal idContenido, string nombreContenido, string estado)
        {
            try
            {
                var db = new GestionContenidosEntities();                
                var lista = new List<CONTENIDO>();
                if (String.IsNullOrEmpty(nombreContenido))
                {
                    if (String.IsNullOrEmpty(estado))
                        lista = (from e in db.CONTENIDOes where !e.ESTADOCONTENIDO.Equals("Eliminado") select e).ToList();
                    else
                        lista = (from e in db.CONTENIDOes where e.ESTADOCONTENIDO.Equals(estado) select e).ToList();
                    return lista;
                }
                else
                {
                    if (idContenido == 1)
                    {
                        lista = (from e in db.CONTENIDOes
                                 where (e.TITULOCONTENIDO.ToLower().Contains(nombreContenido.ToLower())) & (e.ESTADOCONTENIDO.Equals(estado))
                                 select e).ToList();
                        return lista;
                    }
                    else
                    {
                        lista = (from e in db.CONTENIDOes
                                 where (e.TITULOCONTENIDO.ToLower().Contains(nombreContenido.ToLower())) & (e.ESTADOCONTENIDO.Equals(estado))
                                 select e).ToList();
                        return lista;
                    }

                }
            }
            catch (Exception)
            {
                return new List<CONTENIDO>();
            }


        }
        public static CONTENIDO VisualizarContenido(decimal idContenido)
        {
            try
            {
                var db = new GestionContenidosEntities();
                var contenido = (from e in db.CONTENIDOes
                                  where e.IDENTIFICADORCONTENIDO == idContenido
                                  select e).First();
                return contenido;

            }
            catch (Exception)
            {
                return null;
            }

        }

        internal static bool RegistrarContenido(CONTENIDO nuevContenido, List<ATRIBUTO> atributos)
        {
            try
            {
                var db = new GestionContenidosEntities();

                var cont = new CONTENIDO();
                {
                    cont.operacion = "registrar";
                    cont = nuevContenido;
                }
                db.CONTENIDOes.Add(cont);
                db.SaveChanges();
                cont = (from c in db.CONTENIDOes where nuevContenido.TITULOCONTENIDO.Equals(c.TITULOCONTENIDO) select c).FirstOrDefault();
                for (int i = 0; i < atributos.Count; i++)
                {
                    var atributo = new ATRIBUTO();
                    atributo = atributos.ElementAt(i);
                    atributos.RemoveAt(i);
                    atributo.operacion = "registrar";
                    //atributo.dato = "blanco";
                    atributos.Add(atributo);                    
                }
                foreach(var atri in atributos)
                {
                    //db.Database.ExecuteSqlCommand("Insert into CONTENIDO_ATRIBUTO(IDENTIFICADORCONTENIDO,IDENTIFICADORATRIBUTO,DATO) values (5,1,'hola');");
                    cont.ATRIBUTOes.Add(atri);                    
                }

                db.SaveChanges();                    
                
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        internal static bool ModificarContenido(CONTENIDO contenidoModificada)
        {
            try
            {
                var db = new GestionContenidosEntities();

                var contenido = (from asig in db.CONTENIDOes where asig.IDENTIFICADORCONTENIDO == contenidoModificada.IDENTIFICADORCONTENIDO select asig).First();
                {
                    contenido.operacion = "modificacion";                   
                }
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        internal static bool EliminarContenido(decimal idContenido)
        {
            try
            {
                var db = new GestionContenidosEntities();

                var cont = (from asig in db.CONTENIDOes where asig.IDENTIFICADORCONTENIDO == idContenido select asig).First();
                {
                    cont.operacion = "modificacion";
                    cont.ESTADOCONTENIDO = "Eliminado";
                }
                //db.CONTENIDOes.Remove(cont);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        
    }
}
