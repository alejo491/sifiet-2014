using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SIFIET.GestionProgramas.Datos.Modelo
{
    [MetadataType(typeof(ASIGNATURAMETADATA))]
    public partial class ASIGNATURA
    {
        public ICollection<string> ListaCorrequisitos
        {
            get
            {
                ICollection<string> lst = new List<string>();
                if (!String.IsNullOrEmpty(CORREQUISITOSASIGNATURA))
                {
                    var corre = CORREQUISITOSASIGNATURA.Split(',');
                    foreach (string idAsignatura in corre)
                    {
                        lst.Add(idAsignatura);
                    }
                }
                return lst;
            }
            set
            {
                foreach (string idAsignatura in value)
                {
                    if (String.IsNullOrEmpty(CORREQUISITOSASIGNATURA))
                    {
                        CORREQUISITOSASIGNATURA = idAsignatura;
                    }
                    else
                        CORREQUISITOSASIGNATURA = CORREQUISITOSASIGNATURA + "," + idAsignatura;
                }
            }
        }
        public ICollection<string> ListaPrerequisitos
        {
            get
            {
                ICollection<string> lst = new List<string>();
                if (!String.IsNullOrEmpty(PREREQUISITOSASIGNATURA))
                {
                    var corre = PREREQUISITOSASIGNATURA.Split(',');
                    foreach (string idAsignatura in corre)
                    {
                        lst.Add(idAsignatura);
                    }
                }
                return lst;
            }
            set
            {
                foreach (string idAsignatura in value)
                {
                    if (String.IsNullOrEmpty(PREREQUISITOSASIGNATURA))
                    {
                        PREREQUISITOSASIGNATURA = idAsignatura;
                    }
                    else
                        PREREQUISITOSASIGNATURA = PREREQUISITOSASIGNATURA + "," + idAsignatura;
                }
            }
        }
        public string EdicionOmodificacion { get; set; }

    }
    public class ASIGNATURAMETADATA
    {
        [Display(Name = "Codigo")]
        [Required]
        [IDYaExiste(ErrorMessage = "Este Codigo ya esta en uso, ingrese otro")]
        [StringLength(15, ErrorMessage = "El {0} no pueder ser mayor de 15 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-záéíóúñÑ -]*$", ErrorMessage = "Caracteres Inválidos")]//Solo Numero y letras
        public string CODIGOASIGNATURA { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [NombreYaExiste(ErrorMessage = "Ingrese otro nombre, ya existe una Asignatura usando ese nombre")]
        [StringLength(120, ErrorMessage = "El {0} no pueder ser mayor de 120 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-záéíóúñÑ]*$", ErrorMessage = "Caracteres Inválidos")]//Solo Numero y letras
        public string NOMBREASIGNATURA { get; set; }

        [Display(Name = "Correquisitos")]
        [StringLength(250, ErrorMessage = "El {0} no pueder ser mayor de 250 caracteres")]
        public string CORREQUISITOSASIGNATURA { get; set; }

        [Display(Name = "Prerrequisitos")]
        [StringLength(250, ErrorMessage = "El {0} no pueder ser mayor de 250 caracteres")]
        public string PREREQUISITOSASIGNATURA { get; set; }

        [Display(Name = "Creditos")]
        [Required]
        public Nullable<decimal> CREDITOSASIGNATURA { get; set; }

        [Display(Name = "Modalidad")]
        [Required]
        [StringLength(15, ErrorMessage = "El {0} no pueder ser mayor de 15 caracteres")]
        public string MODALIDADASIGNATURA { get; set; }

        [Display(Name = "Clasificación")]
        [Required]
        [StringLength(50, ErrorMessage = "El {0} no pueder ser mayor de 50 caracteres")]
        public string CLASIFICACIONASIGNATURA { get; set; }

        [Display(Name = "Estado")]
        [Required]
        [StringLength(50, ErrorMessage = "El {0} no pueder ser mayor de 50 caracteres")]
        public string ESTADOASIGNATURA { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(250, ErrorMessage = "El {0} no pueder ser mayor de 250 caracteres")]
        public string DESCRIPCIONASIGNATURA { get; set; }

    }

    public sealed class NombreYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var asignaturaValidacion = validationContext.ObjectInstance as ASIGNATURA;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionProgramasEntities();
                var asignatura = (from e in db.ASIGNATURAs where asignaturaValidacion.IDENTIFICADORASIGNATURA == e.IDENTIFICADORASIGNATURA && !e.ESTADOASIGNATURA.Equals("Eliminado") select e).FirstOrDefault();
                if (asignatura != null)
                {
                    if (value.Equals(asignatura.NOMBREASIGNATURA))
                        valid = true;
                }
                if (valid != true)
                {
                    var nombreAsignatura = (from e in db.ASIGNATURAs where nombre == e.NOMBREASIGNATURA && !e.ESTADOASIGNATURA.Equals("Eliminado") select e.NOMBREASIGNATURA).FirstOrDefault
                            ();
                    if (String.IsNullOrEmpty(nombreAsignatura))
                        valid = true;
                }
            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
    public sealed class IDYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var asignaturaValidacion = (validationContext.ObjectInstance as ASIGNATURA).EdicionOmodificacion;
            if (value != null)
            {
                if (asignaturaValidacion.Equals("modificacion"))
                {
                    valid = true;
                }
                if (valid != true)
                {
                    var idAsignaturaValor = value as string;
                    var db = new GestionProgramasEntities();
                    var idAsignatura = (from e in db.ASIGNATURAs where idAsignaturaValor.Equals(e.CODIGOASIGNATURA) && !e.ESTADOASIGNATURA.Equals("Eliminado") select e.CODIGOASIGNATURA).FirstOrDefault();
                    if (String.IsNullOrEmpty(idAsignatura))
                        valid = true;
                }

            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }


}
