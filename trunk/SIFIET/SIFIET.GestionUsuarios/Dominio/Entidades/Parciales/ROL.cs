using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionUsuarios.Aplicacion;

namespace SIFIET.GestionUsuarios.Datos.Modelo
{
    [MetadataType(typeof(ROLMETADATA))]
    public partial class ROL
    {
    }

    public class ROLMETADATA
    {
        [Display(Name = "Nombre")]
        [Required]
        [StringLength(50,ErrorMessage = "El {0} no Puede ser Mayor de 50 Caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-z]*$",
         ErrorMessage = "Caracteres no Validos.")]
        public string NOMBREROL { get; set; }
        [Display(Name = "Descripcion")]
        [Required]
        [StringLength(250, ErrorMessage = "El {0} no Puede ser Mayor de 250 Caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-z]*$",
        ErrorMessage = "Caracteres no Validos.")]
        public string DESCRIPCIONROL { get; set; }
        [Required]
        public string ESTADOROL { get; set; }
    }

    
}
