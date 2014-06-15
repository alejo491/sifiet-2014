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
            var categorias = from c in db.CATEGORIAs
                            select c;

            if (!String.IsNullOrEmpty(busqueda))
            {
                categorias = (from c in db.CATEGORIAs
                             where (c.NOMBRECATEGORIA.ToUpper().Contains(busqueda.ToUpper()))
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
    }
}
