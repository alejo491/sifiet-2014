using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionContenidos.Datos.Modelo
{
    [MetadataType(typeof(ETIQUETAMETADATA))]
    public partial class ETIQUETA
    {
        public string operacion { get; set; }
    }

    public class ETIQUETAMETADATA
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campos es requerido")]
        [NombreYaExiste(ErrorMessage = "Ingrese otro nombre, ya existe una Etiqueta usando ese nombre")]
        [StringLength(255, ErrorMessage = "El {0} no pueder ser mayor de 255 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-z]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string NOMBREETIQUETA { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Este campos es requerido")]
        [StringLength(1000, ErrorMessage = "El {0} no pueder ser mayor de 1000 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-z]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string DESCRIPCIONETIQUETA { get; set; }
    }

    public sealed class NombreYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var asignaturaValidacion = validationContext.ObjectInstance as ETIQUETA;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionContenidosEntities();
                var asignatura = (from e in db.ETIQUETAs where asignaturaValidacion.IDENTIFICADORETIQUETA == e.IDENTIFICADORETIQUETA select e).FirstOrDefault();
                if (asignatura != null)
                {
                    if (value.Equals(asignatura.NOMBREETIQUETA))
                        valid = true;
                }
                if (valid != true)
                {
                    var nombreAsignatura = (from e in db.ETIQUETAs where nombre == e.NOMBREETIQUETA select e.NOMBREETIQUETA).FirstOrDefault
                            ();
                    if (String.IsNullOrEmpty(nombreAsignatura))
                        valid = true;
                }
            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
