//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIFIET.GestionProgramas.Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class FACULTAD
    {
        public FACULTAD()
        {
            this.PROGRAMAs = new HashSet<PROGRAMA>();
        }
    
        public decimal IDENTIFICADORFACULTAD { get; set; }
        public string NOMBREFACULTAD { get; set; }
        public string ESTADOFACULTAD { get; set; }
        public string CODIGOFACULTAD { get; set; }
    
        public virtual ICollection<PROGRAMA> PROGRAMAs { get; set; }
    }
}
