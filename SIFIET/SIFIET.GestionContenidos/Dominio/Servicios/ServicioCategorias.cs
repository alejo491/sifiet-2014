using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using SIFIET.GestionContenidos.Datos.Modelo;
using System.Diagnostics;

namespace SIFIET.GestionContenidos.Dominio.Servicios
{
    class ServicioCategorias
    {
        
        public static List<CATEGORIA> ConsultarCategorias(string busqueda)
        {
            var db = new GestionContenidosEntities();
            var categorias = from c in db.CATEGORIAs where !c.ESTADOCATEGORIA.Trim().Equals("Eliminado")
                            select c;

            if (!String.IsNullOrEmpty(busqueda))
            {
                categorias = (from c in db.CATEGORIAs
                             where (c.NOMBRECATEGORIA.ToUpper().Contains(busqueda.ToUpper())) &&
                             !c.ESTADOCATEGORIA.Trim().Equals("Eliminado")
                             select c);
            }
            return categorias.ToList();
        }

        public static decimal RegistrarCategoria(CATEGORIA categoriaIn)
        {
            var db = new GestionContenidosEntities();
            try
            {
                db.CATEGORIAs.Add(categoriaIn);
                db.SaveChanges();
                var oCategoria = (from c in db.CATEGORIAs where c.NOMBRECATEGORIA == categoriaIn.NOMBRECATEGORIA select c).FirstOrDefault();
                if (oCategoria != null)
                {
                    return oCategoria.IDENTIFICADORCATEGORIA;
                }                    
                return -1;                
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static CATEGORIA ConsultarCategoria(decimal idCategoria)
        {
            var db = new GestionContenidosEntities();
            return db.CATEGORIAs.Find(idCategoria);
        }

        public static decimal RegistrarAtributo(ATRIBUTO oAtributo)
        {
            var db = new GestionContenidosEntities();
            try
            {
            db.Database.ExecuteSqlCommand("Insert into ATRIBUTO (IDENTIFICADORCATEGORIA, NOMBREATRIBUTO,OBLIGATORIOATRIBUTO,PANELEDICIONATRIBUTO,TIPOATRIBUTO,TAMANIOATRIBUTO) values (" +
                                              "'" + oAtributo.IDENTIFICADORCATEGORIA + "'," +
                                              "'" + oAtributo.NOMBREATRIBUTO + "'," +
                                              "'" + oAtributo.OBLIGATORIOATRIBUTO + "'," +
                                              "'" + oAtributo.PANELEDICIONATRIBUTO + "'," +
                                              "'" + oAtributo.TIPOATRIBUTO + "'," +
                                              "" + oAtributo.TAMANIOATRIBUTO + ")");
                db.SaveChanges();
                var _Atributo = (from a in db.ATRIBUTOes where a.NOMBREATRIBUTO.ToLower().Trim().Equals(oAtributo.NOMBREATRIBUTO.ToLower().Trim()) select a).FirstOrDefault();
                if (_Atributo != null)
                {
                    return _Atributo.IDENTIFICADORATRIBUTO;
                }
                return -1;
            }
             catch (Exception)
             {
                 return -1;
             }
        }

        public static bool existeAtributoCategoria(ATRIBUTO oAtributo)
        {
            var db = new GestionContenidosEntities();
            var atributo = (from a in db.ATRIBUTOes
                where
                    oAtributo.IDENTIFICADORCATEGORIA == a.IDENTIFICADORCATEGORIA &&
                    oAtributo.NOMBREATRIBUTO == a.NOMBREATRIBUTO
                select a).ToList();
            return atributo.Count > 0;
        }

        public static bool EliminarAtributoContenido(decimal idCategoria, decimal idAtributo)
        {
            var db = new GestionContenidosEntities();
            try
            {
                var atributo = (from a in db.ATRIBUTOes
                    where idCategoria == a.IDENTIFICADORCATEGORIA && idAtributo == a.IDENTIFICADORATRIBUTO select a).FirstOrDefault();
                db.ATRIBUTOes.Remove(atributo);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool EliminarCategoria(int idCategoria)
        {
            var db = new GestionContenidosEntities();
            try
            {
                var categoria = db.CATEGORIAs.Find(idCategoria);
                categoria.ESTADOCATEGORIA = "Eliminado";
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool ModificarCategoria(CATEGORIA categoriaIn)
        {
            var db = new GestionContenidosEntities();
            try
            {
                string sql = "Update CATEGORIA SET NOMBRECATEGORIA= '" + categoriaIn.NOMBRECATEGORIA + "' ," +
                                                             " DESCRIPCIONCATEGORIA= '" + categoriaIn.DESCRIPCIONCATEGORIA + "' ," +
                                                             " ESTADOCATEGORIA= '" + categoriaIn.ESTADOCATEGORIA + "' ," +
                                                             " VISIBLEPRINCIPALCATEGORIA= " + categoriaIn.VISIBLEPRINCIPALCATEGORIA + " " +

                                                             " WHERE IDENTIFICADORCATEGORIA= " + categoriaIn.IDENTIFICADORCATEGORIA;
                db.Database.ExecuteSqlCommand(sql);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool NombreCategoriaExiste(string p)
        {
            try
            {
                var db = new GestionContenidosEntities();
                var lista = (from e in db.CATEGORIAs
                             where e.NOMBRECATEGORIA.Trim() ==p.Trim()
                             select e).ToList();
                return lista.Count > 0;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
