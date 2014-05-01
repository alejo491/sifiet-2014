using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionUsuarios.Datos.Modelo;
using System.Data;

namespace SIFIET.GestionUsuarios.Dominio.Servicios
{
    class ServicioUsuarios
    {

        public static List<USUARIO> ConsultarUsuarios()
        {

            var db = new GestionUsuariosEntities();

            List<USUARIO> lista = (from e in db.USUARIOs
                                   select e).ToList();
            return lista;
        }


        internal static void RegistrarUsuario(USUARIO usuario, string[] roles)
        {
            var db = new GestionUsuariosEntities();



            db.USUARIOs.Add(usuario);
            db.SaveChanges();

            usuario =
                (from usu in db.USUARIOs
                    where usu.EMAILINSTITUCIONALUSUARIO.Trim().Equals(usuario.EMAILINSTITUCIONALUSUARIO)
                 && usu.IDENTIFICACIONUSUARIO.Equals(usuario.IDENTIFICACIONUSUARIO)   
                 select usu).FirstOrDefault();

            foreach (var rol in roles)
            {
                if (usuario != null) usuario.ROLs.Add(db.ROLs.Find(decimal.Parse(rol.Trim())));
            }
            db.SaveChanges();
        }

        public static USUARIO ConsultarUsuario(int idUsuario)
        {
            var db = new GestionUsuariosEntities();
            return db.USUARIOs.Find(idUsuario);
        }

        internal static void ModificarUsuario(USUARIO usuario, string[] roles)
        {
            var db = new GestionUsuariosEntities();
            
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();

            

            db.Database.ExecuteSqlCommand("Delete from USUARIO_TIENE_ROL" +
                            " WHERE IDENTIFICADORUSUARIO= " + usuario.IDENTIFICADORUSUARIO);
            db.SaveChanges();
            foreach (var rol in roles)
            {
                db.Database.ExecuteSqlCommand("Insert into USUARIO_TIENE_ROL(IDENTIFICADORROL,IDENTIFICADORUSUARIO)" +
                            "values (" + rol + "," + usuario.IDENTIFICADORUSUARIO + ")");
                db.SaveChanges();
            }
           


            
        }

        public static void EliminarUsuario(int idUsuario)
        {
            var db = new GestionUsuariosEntities();
            var usuario = db.USUARIOs.Find(idUsuario);
            usuario.ESTADOUSUARIO = "Desactivo";
            db.SaveChanges();
        }

        internal static List<USUARIO> BuscarUsuarioPorIdentificacion(string id)
        {
            var db = new GestionUsuariosEntities();
            List<USUARIO> lista = (from e in db.USUARIOs
                                   where e.IDENTIFICACIONUSUARIO.Equals(id)
                                   select e).ToList();
            return lista;
        }

        internal static List<USUARIO> BuscarUsuarioPorNombre(string nombre)
        {
            var db = new GestionUsuariosEntities();
            List<USUARIO> lista = (from e in db.USUARIOs
                                   where e.NOMBRESUSUARIO.Contains(nombre)
                                   select e).ToList();
            return lista;
        }

        internal static List<USUARIO> BuscarUsuarioPorApellido(string apellido)
        {
            var db = new GestionUsuariosEntities();
            List<USUARIO> lista = (from e in db.USUARIOs
                                   where e.APELLIDOSUSUARIO.Contains(apellido)
                                   select e).ToList();
            return lista;
        }
    }
}
