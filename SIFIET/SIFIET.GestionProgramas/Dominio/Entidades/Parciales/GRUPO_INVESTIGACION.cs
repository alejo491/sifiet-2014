using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionProgramas.Datos.Modelo
{
    [MetadataType(typeof(GRUPO_INVESTIGACIONMETADATA))]
    public partial class GRUPO_INVESTIGACION
    {
        public string EdicionOmodificacion { get; set; }
    }

    public class GRUPO_INVESTIGACIONMETADATA
    {

        public decimal IDENTIFICADORUSUARIO { get; set; }

        [Required]
        public decimal IDENTIFICADORDEPARTAMENTO { get; set; }


        [Display(Name = "Nombre")]
        [Required]
        [StringLength(250, ErrorMessage = "El Nombre no pueder ser mayor de 250 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-záéíóúñÑ -]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        [GrupoNombreYaExiste(ErrorMessage = "Este Nombre ya está en uso, ingrese otro")]
        public string NOMBREGRUPOINVESTIGACION { get; set; }

        [Required]
        public string ESTADOGRUPOINVESTIGACION { get; set; }

        [Display(Name = "Descripción")]
        [Required]
        [StringLength(250, ErrorMessage = "La Descripción no pueder ser mayor de 250 caracteres")]
        public string DESCRIPCIONGRUPOINVESTIGACION { get; set; }

        [Display(Name = "Código")]
        [Required]
        [RegularExpression(@"^[A-Z0-9 a-záéíóúñÑ -]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]
        [StringLength(15, ErrorMessage = "El Código no pueder ser mayor de 15 caracteres")]
        [CodigoGInvestigacionYaExiste(ErrorMessage = "Este Código ya está en uso, ingrese otro")]
        public string CODIGOGRUPOINVESTIGACION { get; set; }

    }

    public sealed class CodigoGInvestigacionYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var grupoInvestigacionValidacion = (validationContext.ObjectInstance as GRUPO_INVESTIGACION).EdicionOmodificacion;
            if (value != null)
            {
                if (grupoInvestigacionValidacion.Equals("modificacion"))
                {
                    valid = true;
                }
                if (valid != true)
                {
                    var idGInvestigacionValor = value as string;
                    var db = new GestionProgramasEntities();
                    var idGInvestigacion = (from g in db.GRUPO_INVESTIGACION where idGInvestigacionValor.Equals(g.CODIGOGRUPOINVESTIGACION) && !g.ESTADOGRUPOINVESTIGACION.Equals("Eliminado")  select g.CODIGOGRUPOINVESTIGACION).FirstOrDefault();
                    if (String.IsNullOrEmpty(idGInvestigacion))
                        valid = true;
                }

            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }

    public sealed class GrupoNombreYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var grupoInvestigacionValidacion = (validationContext.ObjectInstance as GRUPO_INVESTIGACION).EdicionOmodificacion;
            if (value != null)
            {
                if (grupoInvestigacionValidacion.Equals("modificacion"))
                {
                    valid = true;
                }
                if (valid != true)
                {
                    var nombreGInvestigacionValor = value as string;
                    var db = new GestionProgramasEntities();
                    var nombreGInvestigacion = (from g in db.GRUPO_INVESTIGACION where nombreGInvestigacionValor.Equals(g.NOMBREGRUPOINVESTIGACION) select g.NOMBREGRUPOINVESTIGACION).FirstOrDefault();
                    if (String.IsNullOrEmpty(nombreGInvestigacion))
                        valid = true;
                }
            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }

    public sealed class EsNumeric : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;

            if (value != null)
            {

                if (valid != true)
                {
                    var codigoGInvestigacion = value as string;
                    int x;
                    if (int.TryParse(codigoGInvestigacion, out x))
                        valid = true;
                }

            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}