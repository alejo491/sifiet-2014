using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionProgramas.Datos.Modelo
{
    [MetadataType(typeof(PLANESTUDIOMETADATA))]
    public partial class PLANESTUDIO
    {
        public string operacion { get; set; }
    }

    public class PLANESTUDIOMETADATA
    {
        [Display(Name = "Nombre del Plan :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        [StringLength(120, ErrorMessage = "El {0} no pueder ser mayor de 120 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-z]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        [PlanEstudiosNombreExiste(ErrorMessage = "Ya existe un plan de estudios con el nombre que desea registrar, por favor cambie el campo si desea crear otro plan de estudio, o realice una búsqueda para editar los campos del plan existente.")]
        public string NOMBREPLANESTUDIOS { get; set; }

        [Display(Name = "Descripción :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        [StringLength(120, ErrorMessage = "El {0} no pueder ser mayor de 120 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-z]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string DESCRIPCIONPLANESTUDIOS { get; set; }

        [Display(Name = "Fecha Inicio :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string FECHAINICIOPLANESTUDIOS { get; set; }

        [Display(Name = "Fecha Fin :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string FECHAFINPLANESTUDIOS { get; set; }

        [Display(Name = "Codigo del Plan :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        [StringLength(15, ErrorMessage = "El {0} no pueder ser mayor de 15 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-z]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string CODIGOPLANESTUDIOS { get; set; }

        [Display(Name = "Estado :")]
        public string ESTADOPLANESTUDIOS { get; set; }


        public virtual PROGRAMA PROGRAMA { get; set; }

    }

    public sealed class PlanEstudiosNombreExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var planEstudioValidacion = validationContext.ObjectInstance as PLANESTUDIO;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionProgramasEntities();
                if (planEstudioValidacion.operacion == "editar" || planEstudioValidacion.operacion == "eliminar")
                {
                    var planEstudioNombre = (from e in db.PLANESTUDIOS where planEstudioValidacion.IDENTIFICADORPLANESTUDIOS == e.IDENTIFICADORPLANESTUDIOS select e.NOMBREPLANESTUDIOS).FirstOrDefault();
                    var nombrePlanEstudio = (from e in db.PLANESTUDIOS where nombre.ToUpper() == e.NOMBREPLANESTUDIOS.ToUpper() && e.NOMBREPLANESTUDIOS.ToUpper() != planEstudioNombre.ToUpper() select e.NOMBREPLANESTUDIOS).FirstOrDefault();
                    if (String.IsNullOrEmpty(nombrePlanEstudio))
                    {
                        valid = true;
                    }
                }
                else
                {
                    var planEstudio = (from e in db.PLANESTUDIOS where planEstudioValidacion.NOMBREPLANESTUDIOS == e.NOMBREPLANESTUDIOS select e).FirstOrDefault();
                    var nombrePlanEstudio = (from e in db.PLANESTUDIOS where nombre.ToUpper() == e.NOMBREPLANESTUDIOS.ToUpper() select e.NOMBREPLANESTUDIOS).FirstOrDefault();
                    if (String.IsNullOrEmpty(nombrePlanEstudio))
                    {
                        valid = true;
                    }
                }


            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}