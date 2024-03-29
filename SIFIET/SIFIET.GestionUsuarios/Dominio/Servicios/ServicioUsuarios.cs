﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
                                   where e.ESTADOUSUARIO.Trim().Equals("Activo")
                                   select e).ToList();
            return lista;
        }


        internal static void RegistrarUsuario(USUARIO usuario, string[] roles)
        {
            var db = new GestionUsuariosEntities();

            db.Database.ExecuteSqlCommand("INSERT INTO USUARIO(EMAILINSTITUCIONALUSUARIO, PASSWORDUSUARIO, IDENTIFICACIONUSUARIO, NOMBRESUSUARIO, APELLIDOSUSUARIO, ESTADOUSUARIO) VALUES (" +
                                              "'" + usuario.EMAILINSTITUCIONALUSUARIO + "'," +
                                              "'" + usuario.PASSWORDUSUARIO + "'," +
                                              "'" + usuario.IDENTIFICACIONUSUARIO + "'," +
                                              "'" + usuario.NOMBRESUSUARIO + "'," +
                                              "'" + usuario.APELLIDOSUSUARIO + "'," +
                                              "'" + usuario.ESTADOUSUARIO + "')");
            db.SaveChanges();


            //db.USUARIOs.Add(usuario);

        //db.SaveChanges();

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
            System.Security.Cryptography.SHA1CryptoServiceProvider sh1 =
                new System.Security.Cryptography.SHA1CryptoServiceProvider();

            string sql="Update USUARIO SET NOMBRESUSUARIO= '" + usuario.NOMBRESUSUARIO + "' ," +
                                                            " APELLIDOSUSUARIO= '" + usuario.APELLIDOSUSUARIO + "' ," +
                                                            " EMAILINSTITUCIONALUSUARIO= '" + usuario.EMAILINSTITUCIONALUSUARIO + "' ," +
                                                            " IDENTIFICACIONUSUARIO= '" + usuario.IDENTIFICACIONUSUARIO + "' ," +
                                                            " PASSWORDUSUARIO= '" + usuario.PASSWORDUSUARIO + "' ," +
                                                            " ESTADOUSUARIO= '" + usuario.ESTADOUSUARIO + "'" +
                                                            " WHERE IDENTIFICADORUSUARIO= " + usuario.IDENTIFICADORUSUARIO;
            db.Database.ExecuteSqlCommand(sql);
   
       
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
            string sql = "Update USUARIO SET ESTADOUSUARIO= 'Inactivo'" +
                                                           
                                                            " WHERE IDENTIFICADORUSUARIO= " + idUsuario;
            db.Database.ExecuteSqlCommand(sql);


            db.SaveChanges();
            
            
        }

        internal static List<USUARIO> BuscarUsuarioPorIdentificacion(string id,string estado)
        {
            var db = new GestionUsuariosEntities();
            List<USUARIO> lista = (from e in db.USUARIOs
                                   where e.IDENTIFICACIONUSUARIO.Equals(id.Trim()) && e.ESTADOUSUARIO.Equals(estado.Trim())
                                   select e).ToList();
            return lista;
        }

        internal static List<USUARIO> BuscarUsuarioPorNombre(string nombre, string estado)
        {
            var db = new GestionUsuariosEntities();
            List<USUARIO> lista = (from e in db.USUARIOs
                                   where (e.NOMBRESUSUARIO.ToLower().Contains(nombre.ToLower().Trim()) || e.NOMBRESUSUARIO.ToLower().StartsWith(nombre.ToLower().Trim()) || e.NOMBRESUSUARIO.ToLower().EndsWith(nombre.ToLower().Trim())) && e.ESTADOUSUARIO.Trim().Equals(estado.Trim())
                                   select e).ToList();
            return lista;
        }

        internal static List<USUARIO> BuscarUsuarioPorApellido(string apellido, string estado)
        {
            var db = new GestionUsuariosEntities();
            List<USUARIO> lista = (from e in db.USUARIOs
                                   where (e.APELLIDOSUSUARIO.ToLower().Contains(apellido.ToLower().Trim()) || e.APELLIDOSUSUARIO.ToLower().StartsWith(apellido.ToLower().Trim()) || e.APELLIDOSUSUARIO.ToLower().EndsWith(apellido.ToLower().Trim())) && e.ESTADOUSUARIO.Trim().Equals(estado.Trim())
                                   select e).ToList();
            return lista;
        }

        public static bool IdentificacionUsuarioExiste(string id)
        {
            try
            {
                var db = new GestionUsuariosEntities();
                var lista = (from e in db.USUARIOs
                             where e.IDENTIFICACIONUSUARIO.Trim() == id.Trim()
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
