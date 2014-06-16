using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionContenidos.Datos.Modelo
{
    public partial class CONTENIDO
    {        
        public string operacion { get; set; }
        public string autorContenido 
        { 
            get
            {
                var db = new GestionContenidosEntities();
                var usuario = (from e in db.USUARIOs where IDENTIFICADORUSUARIO == e.IDENTIFICADORUSUARIO select e).FirstOrDefault();
                if (usuario != null)
                    return usuario.NOMBRESUSUARIO;
                else
                    return "";
            }
            set 
            {
                if (!String.IsNullOrEmpty(value))
                {
                    var db = new GestionContenidosEntities();
                    var usuario = (from e in db.USUARIOs where value.Equals(e.EMAILINSTITUCIONALUSUARIO) select e).FirstOrDefault();
                    if (usuario != null)                    
                        IDENTIFICADORUSUARIO = usuario.IDENTIFICADORUSUARIO;
                    else                    
                        IDENTIFICADORUSUARIO = 0;
                }
            }
        }               
    }
}
