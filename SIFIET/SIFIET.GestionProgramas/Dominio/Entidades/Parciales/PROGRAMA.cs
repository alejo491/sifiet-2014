using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionProgramas.Datos.Modelo
{
    [MetadataType(typeof(PROGRAMAMETADATA))]
    public partial class PROGRAMA
    {
        
    }

    public class PROGRAMAMETADATA
    {

        [Display(Name = "Codigo SNIES:")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public Nullable<decimal> CODIGOSNIESPROGRAMA { get; set; }


        [Display(Name = "Nombre :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string NOMBREPROGRAMA { get; set; }

        [Display(Name = "Descripcion :")]
        public string DESCRIPCIONPROGRAMA { get; set; }

        [Display(Name = "Facultad:")]
        //[Required(ErrorMessage = "Este campos es requerido")]
        public virtual FACULTAD FACULTAD { get; set; }

        [Display(Name = "Duración :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public Nullable<decimal> DURACIONPROGRAMA { get; set; }

        [Display(Name = "Modalidad :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string MODALIDADPROGRAMA { get; set; }

        [Display(Name = "Jornada :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string JORNADAPROGRAMA { get; set; }

        [Display(Name = "Admisión :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string ADMISIONPROGRAMA { get; set; }

        [Display(Name = "Duración Periodo :")]
        public string PERIODODURACIONPROGRAMA { get; set; }

        [Display(Name = "Estado :")]
        public string ESTADOPROGRAMA { get; set; }
      
    }
}
