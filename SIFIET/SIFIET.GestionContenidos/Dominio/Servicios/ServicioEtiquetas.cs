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
    class ServicioEtiquetas
    {

        public static ETIQUETA ConsultarEtiqueta(decimal idEtiqueta)
        {
            var db = new GestionContenidosEntities();
            return db.ETIQUETAs.Find(idEtiqueta);
        }

        public static List<ETIQUETA> ConsultarEtiquetas(string busqueda)
        {
            var db = new GestionContenidosEntities();
            var etiquetas = from m in db.ETIQUETAs
                                 select m;

            if (!String.IsNullOrEmpty(busqueda))
            {
                etiquetas = (from m in db.ETIQUETAs
                    where (m.NOMBREETIQUETA.ToUpper().Contains(busqueda.ToUpper()))
                    select m);
            }
            return etiquetas.ToList();
        }

        public static bool RegistrarEtiqueta(ETIQUETA objEtiqueta)
        {
            var db = new GestionContenidosEntities();
            try
            {
                db.ETIQUETAs.Add(objEtiqueta);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EditarEtiqueta(ETIQUETA objEtiqueta)
        {
            var db = new GestionContenidosEntities();
            try
            {
                db.Entry(objEtiqueta).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EliminarEtiqueta(decimal idEtiqueta)
        {
            var db = new GestionContenidosEntities();
            try
            {
                ETIQUETA objEtiqueta = db.ETIQUETAs.Find(idEtiqueta);
                objEtiqueta.operacion = "eliminar";
                db.Entry(objEtiqueta).State = EntityState.Deleted;
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
