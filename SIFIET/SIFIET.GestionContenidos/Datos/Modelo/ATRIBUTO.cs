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
    
    public partial class ATRIBUTO
    {
        public ATRIBUTO()
        {
            this.CONTENIDOes = new HashSet<CONTENIDO>();
        }
    
        public decimal IDENTIFICADORATRIBUTO { get; set; }
        public decimal IDENTIFICADORCATEGORIA { get; set; }
        public string NOMBREATRIBUTO { get; set; }
        public Nullable<decimal> TAMANIOATRIBUTO { get; set; }
        public Nullable<decimal> PANELEDICIONATRIBUTO { get; set; }
        public Nullable<decimal> OBLIGATORIOATRIBUTO { get; set; }
        public string TIPOATRIBUTO { get; set; }
    
        public virtual CATEGORIA CATEGORIA { get; set; }
        public virtual ICollection<CONTENIDO> CONTENIDOes { get; set; }
    }
}
