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
                    if (idContenido == 2)
                    {
                        lista = (from s in db.CONTENIDOes
                                 join f in db.CATEGORIAs on s.IDENTIFICADORCATEGORIA equals f.IDENTIFICADORCATEGORIA
                                 where
                                     (f.NOMBRECATEGORIA.ToLower().Contains(nombreContenido.ToLower()))
                                 select s).ToList();
                        return lista;
                    }
                    else
                    {
                        foreach (var cont in db.CONTENIDOes)
                        {
                            var etiquetas = ConsultarEtiquetasDelContenido(cont.IDENTIFICADORCONTENIDO);
                            var tiene = false;
                            foreach (var etiq in etiquetas)
                            {
                                if (etiq.NOMBREETIQUETA.ToLower().Contains(nombreContenido.ToLower()) & (cont.ESTADOCONTENIDO.Equals(estado)))
                                    tiene = true;
                                                               
                            }
                            if (tiene)
                                lista.Add(cont);
                        }
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

        internal static bool RegistrarContenido(CONTENIDO nuevContenido, List<ATRIBUTO> atributos, List<ETIQUETA> etiquetas)
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

                if(AsociarAtributos(cont.IDENTIFICADORCONTENIDO,atributos))
                {
                    if (AsociarEtiquetas(cont.IDENTIFICADORCONTENIDO, etiquetas))
                    {
                        return true;
                    }
                    else
                    {                        
                        db.CONTENIDOes.Remove(cont);
                        db.SaveChanges();
                        return false;
                    }
                }
                else
                {
                    db.CONTENIDOes.Remove(cont);
                    db.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }

        internal static bool ModificarContenido(CONTENIDO contenidoModificado, List<ATRIBUTO> atributos, List<ETIQUETA> etiquetas)
        {
            try
            {
                var db = new GestionContenidosEntities();

                var contenido = (from cont in db.CONTENIDOes where cont.IDENTIFICADORCONTENIDO == contenidoModificado.IDENTIFICADORCONTENIDO select cont).First();
                {
                    contenido.operacion = "Modificacion";
                    contenido.PRIORIDADCONTENIDO = contenidoModificado.PRIORIDADCONTENIDO;
                    contenido.TITULOCONTENIDO = contenidoModificado.TITULOCONTENIDO;
                    contenido.ESTADOCONTENIDO = contenidoModificado.ESTADOCONTENIDO;
                    contenido.CUERPOCONTENIDO = contenidoModificado.CUERPOCONTENIDO;
                    contenido.DESCRIPCIONCONTENIDO = contenidoModificado.DESCRIPCIONCONTENIDO;

                }
                db.SaveChanges();
                if (ActualizarAtributos(contenido.IDENTIFICADORCONTENIDO, atributos))
                {
                    if (ActualizarEtiquetas(contenido.IDENTIFICADORCONTENIDO, etiquetas))
                    {
                        return true;
                    }
                    else
                    {                        
                        return false;
                    }
                }
                else
                {                    
                    return false;
                }
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
                    cont.operacion = "Modificacion";
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
        internal static List<ATRIBUTO> ConsultarAtributosDelContenido(decimal idContenido)
        {
            try
            {
                var db = new GestionContenidosEntities();
                var atributos = new List<ATRIBUTO>();
                var cont = (from asig in db.CONTENIDOes where asig.IDENTIFICADORCONTENIDO == idContenido select asig).FirstOrDefault();                
                if(cont != null)
                {                    
                    foreach (var atri in cont.ATRIBUTOes)
                    {
                        var auxAtributo = new ATRIBUTO();
                        var datoAtributo = db.Database.SqlQuery<string>("SELECT DATO FROM CONTENIDO_ATRIBUTO WHERE IDENTIFICADORCONTENIDO = '" + idContenido + 
                            "' AND IDENTIFICADORATRIBUTO = '" + atri.IDENTIFICADORATRIBUTO +"'").FirstOrDefault();
                        auxAtributo = atri;
                        auxAtributo.dato = datoAtributo;
                        atributos.Add(auxAtributo);
                    }                    
                    return atributos;

                }                                
                return new List<ATRIBUTO>();

            }
            catch (Exception)
            {
                return new List<ATRIBUTO>();
            }
        }
        internal static List<ETIQUETA> ConsultarEtiquetasDelContenido(decimal idContenido)
        {
            try
            {
                var db = new GestionContenidosEntities();
                var etiquetas = new List<ETIQUETA>();
                var cont = (from asig in db.CONTENIDOes where asig.IDENTIFICADORCONTENIDO == idContenido select asig).FirstOrDefault();
                if (cont != null)
                {
                    foreach (var etiq in cont.ETIQUETAs)
                    {
                        var auxEtiqueta = new ETIQUETA();                       
                        auxEtiqueta = etiq;                        
                        etiquetas.Add(auxEtiqueta);
                    }
                    return etiquetas;

                }
                return new List<ETIQUETA>();

            }
            catch (Exception)
            {
                return new List<ETIQUETA>();
            }
        }
        internal static bool AsociarAtributos(decimal idContenido, List<ATRIBUTO> atributos)
        {            
            try
            {
                var db = new GestionContenidosEntities();
                foreach (var atri in atributos)
                {
                    db.Database.ExecuteSqlCommand("Insert into CONTENIDO_ATRIBUTO(IDENTIFICADORCONTENIDO,IDENTIFICADORATRIBUTO,DATO) values ('" + idContenido + "','" + atri.IDENTIFICADORATRIBUTO + "','" + atri.dato+ "')");
                    //cont.ATRIBUTOes.Add(atri);     
                }                
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
            
        }
        internal static bool ActualizarAtributos(decimal idContenido, List<ATRIBUTO> atributos)
        {            
            try
            {
                var db = new GestionContenidosEntities();
                foreach (var atri in atributos)
                {
                    //db.Database.ExecuteSqlCommand("Insert into CONTENIDO_ATRIBUTO(IDENTIFICADORCONTENIDO,IDENTIFICADORATRIBUTO,DATO) values ('" + idContenido + "','" + atri.IDENTIFICADORATRIBUTO + "','" + atri.dato + "')");
                    db.Database.ExecuteSqlCommand("Update CONTENIDO_ATRIBUTO SET DATO = '" + atri.dato + "'" +
                                              " WHERE IDENTIFICADORCONTENIDO = '" + idContenido +"' AND IDENTIFICADORATRIBUTO = '" + atri.IDENTIFICADORATRIBUTO +"'");                                       
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        internal static bool AsociarEtiquetas(decimal idContenido, List<ETIQUETA> etiquetas)
        {
            try
            {
                var db = new GestionContenidosEntities();
                foreach (var eti in etiquetas)
                {
                    db.Database.ExecuteSqlCommand("Insert into CONTENIDO_TIPO_CONTENIDO(IDENTIFICADORCONTENIDO,IDENTIFICADORETIQUETA) values ('" + idContenido + "','" + eti.IDENTIFICADORETIQUETA + "')");                    
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        internal static bool ActualizarEtiquetas(decimal idContenido, List<ETIQUETA> etiquetas)
        {
            try
            {
                var db = new GestionContenidosEntities();
                db.Database.ExecuteSqlCommand("Delete from CONTENIDO_TIPO_CONTENIDO WHERE IDENTIFICADORCONTENIDO = '" + idContenido + "'");
                db.SaveChanges();
                foreach (var eti in etiquetas)
                {
                    db.Database.ExecuteSqlCommand("Insert into CONTENIDO_TIPO_CONTENIDO(IDENTIFICADORCONTENIDO,IDENTIFICADORETIQUETA) values ('" + idContenido + "','" + eti.IDENTIFICADORETIQUETA + "')");                      
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static void ObtenerIdTipoRecurso(object tipoRecurso)
        {
            throw new NotImplementedException();
        }
    }
}
