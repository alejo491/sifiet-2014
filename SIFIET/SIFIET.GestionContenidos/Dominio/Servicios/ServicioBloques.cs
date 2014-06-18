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

            if (bloque1 != "") {
                var a = (from bloque in db.BLOQUEs where bloque.IDENTIFICADORBLOQUE == 1 select bloque).First();
                {
                    Decimal.TryParse(bloque1, out number);
                    a.IDENTIFICADORCATEGORIA = number;

                }
            }

            if (bloque2 != "") {
                var b = (from bloque in db.BLOQUEs where bloque.IDENTIFICADORBLOQUE == 2 select bloque).First();
                {
                    Decimal.TryParse(bloque2, out number);
                    b.IDENTIFICADORCATEGORIA = number;

                }
            }

            if (bloque3 != "") {
                var c = (from bloque in db.BLOQUEs where bloque.IDENTIFICADORBLOQUE == 3 select bloque).First();
                {
                    Decimal.TryParse(bloque3, out number);
                    c.IDENTIFICADORCATEGORIA = number;

                }
            
            }

            if (bloque4 != "") {
                var d = (from bloque in db.BLOQUEs where bloque.IDENTIFICADORBLOQUE == 4 select bloque).First();
                {
                    Decimal.TryParse(bloque4, out number);
                    d.IDENTIFICADORCATEGORIA = number;

                }
            }

           
            db.SaveChanges();
            
            return true;
        }
    }
}
