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
    
    public partial class GRUPO_INVESTIGACION
    {
        public decimal IDENTIFICADORGRUPOINVES { get; set; }
        public decimal IDENTIFICADORUSUARIO { get; set; }
        public decimal IDENTIFICADORDEPARTAMENTO { get; set; }
        public string NOMBREGRUPOINVESTIGACION { get; set; }
        public string ESTADOGRUPOINVESTIGACION { get; set; }
        public string DESCRIPCIONGRUPOINVESTIGACION { get; set; }
        public string CODIGOGRUPOINVESTIGACION { get; set; }
    
        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }
        public virtual DOCENTE DOCENTE { get; set; }
    }
}
