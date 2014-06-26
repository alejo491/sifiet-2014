using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionContenidos.Datos.Modelo;

namespace SIFIET.GestionContenidos.Dominio.Servicios
{
    class ServicioBloques
    {
        public static List<BLOQUE> ConsultarBloques()
        {
            var db = new GestionContenidosEntities();
            var consulta = from m in db.BLOQUEs
                            select m;

            List<BLOQUE> bloques = consulta.ToList();
            List<BLOQUE> listaBloques = new List<BLOQUE>();
            int contador = 0;
            foreach (var item in bloques) { 
                contador++;
                if (contador <= 4) {
                    listaBloques.Add(item);
                }
            
            }
            return listaBloques;
        }

        public static bool AsignarCategoriasBloques(string bloque1, string bloque2, string bloque3, string bloque4)
        {

            var db = new GestionContenidosEntities();
            decimal number;

            if (bloque1 != "")
            {
                string sql = "Update BLOQUE SET IDENTIFICADORCATEGORIA = '" +bloque1 +"' WHERE IDENTIFICADORBLOQUE = " + 1;
                db.Database.ExecuteSqlCommand(sql);
                db.SaveChanges();
            }

            if (bloque2 != "") {
                string sql = "Update BLOQUE SET IDENTIFICADORCATEGORIA = '" + bloque2 + "' WHERE IDENTIFICADORBLOQUE = " + 2;
                db.Database.ExecuteSqlCommand(sql);
                db.SaveChanges();
            }

            if (bloque3 != "") {
                string sql = "Update BLOQUE SET IDENTIFICADORCATEGORIA = '" + bloque3 + "' WHERE IDENTIFICADORBLOQUE = " +3;
                db.Database.ExecuteSqlCommand(sql);
                db.SaveChanges();
            
            }

            if (bloque4 != "") {
                string sql = "Update BLOQUE SET IDENTIFICADORCATEGORIA = '" + bloque4 + "' WHERE IDENTIFICADORBLOQUE = " + 4;
                db.Database.ExecuteSqlCommand(sql);
                db.SaveChanges();
            }

           
            db.SaveChanges();
            
            return true;
        }
    }
}
