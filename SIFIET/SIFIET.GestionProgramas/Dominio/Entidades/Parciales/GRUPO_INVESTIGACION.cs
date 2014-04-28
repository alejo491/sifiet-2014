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

    }

    public class GRUPO_INVESTIGACIONMETADATA
    {
        
        public decimal IDENTIFICADORUSUARIO { get; set; }

        [Required]
        public decimal IDENTIFICADORDEPARTAMENTO { get; set; }

        [Required]
        [GrupoNombreYaExiste(ErrorMessage = "Este Nombre ya esta en uso, ingrese otro")]
        public string NOMBREGRUPOINVESTIGACION { get; set; }
       
        public string ESTADOGRUPOINVESTIGACION { get; set; }

        [Required]
        public string DESCRIPCIONGRUPOINVESTIGACION { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "El {0} no pueder ser mayor de 15 caracteres")]
        public string CODIGOGRUPOINVESTIGACION { get; set; }

    }

    public sealed class GrupoNombreYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var asignaturaValidacion = validationContext.ObjectInstance as GRUPO_INVESTIGACION;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionProgramasEntities();
                var asignatura = (from e in db.GRUPO_INVESTIGACION where asignaturaValidacion.NOMBREGRUPOINVESTIGACION == e.NOMBREGRUPOINVESTIGACION select e).FirstOrDefault();
                if (asignatura != null)
                {
                    if (value.Equals(asignatura.NOMBREGRUPOINVESTIGACION))
                        valid = true;
                }
                if (valid != true)
                {
                    var nombreAsignatura = (from e in db.GRUPO_INVESTIGACION where nombre == e.NOMBREGRUPOINVESTIGACION select e.NOMBREGRUPOINVESTIGACION).FirstOrDefault
                            ();
                    if (String.IsNullOrEmpty(nombreAsignatura))
                        valid = true;
                }
            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
