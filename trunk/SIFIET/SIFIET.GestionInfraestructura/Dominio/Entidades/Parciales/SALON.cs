using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SIFIET.GestionInfraestructura.Datos.Modelo
{
    [MetadataType(typeof(SALONMETADATA))]
    public partial class SALON
    {
        public string NombreFacultad
        {
            get
            {
                var db = new GestionInfraestructuraEntities();
                return (from e in db.FACULTADs where IDENTIFICADORFACULTAD == e.IDENTIFICADORFACULTAD select e.NOMBREFACULTAD).First();
            }
        }
        
    }
    public class SALONMETADATA
    {
        [Required]
        public decimal IDENTIFICADORFACULTAD { get; set; }
        [Required]
        [NombreSalonYaExiste(ErrorMessage = "Ingrese otro nombre, ya existe un Salon usando ese nombre")]
        [StringLength(120, ErrorMessage = "El {0} no pueder ser mayor de 120 caracteres")]
        public string NOMBRESALON { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El {0} no pueder ser mayor de 50 caracteres")]
        public string ESTADOSALON { get; set; }
    }

    public sealed class NombreSalonYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var asignaturaValidacion = validationContext.ObjectInstance as SALON;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionInfraestructuraEntities();
                var asignatura = (from e in db.SALONs where asignaturaValidacion.IDENTIFICADORSALON == e.IDENTIFICADORSALON select e).FirstOrDefault();
                if (asignatura != null)
                {
                    if (value.Equals(asignatura.NOMBRESALON))
                        valid = true;
                }
                if (valid != true)
                {
                    var nombreAsignatura = (from e in db.SALONs where nombre == e.NOMBRESALON select e.NOMBRESALON).FirstOrDefault
                            ();
                    if (String.IsNullOrEmpty(nombreAsignatura))
                        valid = true;
                }
            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
