using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionContenidos.Datos.Modelo
{
    [MetadataType(typeof(ATRIBUTOMETADATA))]
    public partial class ATRIBUTO
    {
        public string operacion { get; set; }
        public string dato { get; set; }
    }

    public class ATRIBUTOMETADATA
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(255, ErrorMessage = "El {0} no pueder ser mayor de 255 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-zÑñáéíóúÁÉÍÓÚ_]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string NOMBREATRIBUTO { get; set; }
        [Display(Name = "Tamaño")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public Nullable<decimal> TAMANIOATRIBUTO { get; set; }
        [Display(Name = "Panel de Edicion")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public Nullable<decimal> PANELEDICIONATRIBUTO { get; set; }
        [Display(Name = "Obligarorio")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public Nullable<decimal> OBLIGATORIOATRIBUTO { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string TIPOATRIBUTO { get; set;}
    }
}
