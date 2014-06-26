using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionInfraestructura.Datos.Modelo;

namespace SIFIET.GestionInfraestructura.Datos.Modelo
{
    [MetadataType(typeof (Datos.Modelo.RECURSOMETADATA))]
    public partial class RECURSO
    {
        public string NombreFacultad
        {
            get
            {
                var db = new GestionInfraestructuraEntities();
                return
                    (from e in db.FACULTADs
                        where IDENTIFICADORFACULTAD == e.IDENTIFICADORFACULTAD
                        select e.NOMBREFACULTAD).First();
            }
        }

        public string NOMBRETIPORECURSO
        {
            get
            {
                var db = new GestionInfraestructuraEntities();
                return
                    (from e in db.TIPORECURSOes
                     where IDTIPORECURSO == e.IDTIPORECURSO
                     select e.NOMBRETIPORECURSO).First();
            }
        }
    }

    public class RECURSOMETADATA
    {
        [Display(Name = "Tipo de Recurso")]
        [Required]
        public decimal IDTIPORECURSO { get; set; }

        [Display(Name = "Facultad")]
        [Required]
        public decimal IDENTIFICADORFACULTAD { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [Datos.Modelo.NombreRecursoYaExiste(ErrorMessage = "Ingrese otro nombre, ya existe un Recurso usando ese nombre")]
        [StringLength(120, ErrorMessage = "El {0} no pueder ser mayor de 120 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-z]*$", ErrorMessage = "Caracteres Inválidos")]//Solo Numero y letras
        public string NOMBRERECURSO { get; set; }

        [Display(Name = "Estado")]
        [Required]
        [StringLength(50, ErrorMessage = "El {0} no pueder ser mayor de 50 caracteres")]
        public string ESTADORECURSO { get; set; }
    }

    public sealed class NombreRecursoYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var asignaturaValidacion = validationContext.ObjectInstance as Datos.Modelo.RECURSO;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionInfraestructuraEntities();
                var asignatura = (from e in db.RECURSOes where asignaturaValidacion.IDENTIFICADORRECURSO == e.IDENTIFICADORRECURSO select e).FirstOrDefault();
                if (asignatura != null)
                {
                    if (value.Equals(asignatura.NOMBRERECURSO))
                        valid = true;
                }
                if (valid != true)
                {
                    var nombreAsignatura = (from e in db.RECURSOes where nombre == e.NOMBRERECURSO select e.NOMBRERECURSO).FirstOrDefault
                            ();
                    if (String.IsNullOrEmpty(nombreAsignatura))
                        valid = true;
                }
            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
