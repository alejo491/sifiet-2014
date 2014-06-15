using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionContenidos.Datos.Modelo
{
    [MetadataType(typeof(CATEGORIAMETADATA))]
    public partial class CATEGORIA
    {
        public string operacion { get; set; }
    }

    public class CATEGORIAMETADATA
    {
        [Display(Name = "Nombre :")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [NombreCategoriaYaExiste(ErrorMessage = "Ingrese otro nombre, ya existe una Etiqueta usando ese nombre")]
        [StringLength(255, ErrorMessage = "El {0} no pueder ser mayor de 255 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-zÑñáéíóúÁÉÍÓÚ_]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string NOMBRECATEGORIA { get; set; }

        [Display(Name = "Descripción :")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(1000, ErrorMessage = "El {0} no pueder ser mayor de 1000 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-zÑñáéíóúÁÉÍÓÚ_.,]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string DESCRIPCIONCATEGORIA { get; set; }

        [Display(Name = "Visible :")]
        [Required(ErrorMessage = "Seleccione una opción")]
        public Nullable<decimal> VISIBLEPRINCIPALCATEGORIA { get; set; }
    }

    public sealed class NombreCategoriaYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var categoriaValidacion = validationContext.ObjectInstance as CATEGORIA;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionContenidosEntities();
                var categoria = (from c in db.CATEGORIAs where categoriaValidacion.IDENTIFICADORCATEGORIA == c.IDENTIFICADORCATEGORIA select c).FirstOrDefault();
                if (categoria != null)
                {
                    if (value.Equals(categoria.NOMBRECATEGORIA))
                        valid = true;
                }
                if (valid != true)
                {
                    var nombreCategoria = (from c in db.CATEGORIAs where nombre == c.NOMBRECATEGORIA select c.NOMBRECATEGORIA).FirstOrDefault
                            ();
                    if (String.IsNullOrEmpty(nombreCategoria))
                        valid = true;
                }
            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
