//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIFIET.GestionContenidos.Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class CONTENIDO
    {
        public CONTENIDO()
        {
            this.ETIQUETAs = new HashSet<ETIQUETA>();
        }
    
        public decimal IDENTIFICADORCONTENIDO { get; set; }
        public decimal IDENTIFICADORUSUARIO { get; set; }
        public decimal IDENTIFICADORCATEGORIA { get; set; }
        public string TITULOCONTENIDO { get; set; }
        public string DESCRIPCIONCONTENIDO { get; set; }
        public string CUERPOCONTENIDO { get; set; }
        public string FECHACREACIONCONTENIDO { get; set; }
        public string ESTADOCONTENIDO { get; set; }
        public string PRIORIDADCONTENIDO { get; set; }
    
        public virtual ICollection<ETIQUETA> ETIQUETAs { get; set; }
    }
}
