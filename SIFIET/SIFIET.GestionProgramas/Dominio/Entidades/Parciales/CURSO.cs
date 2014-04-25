using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SIFIET.GestionProgramas.Datos.Modelo
{
    [MetadataType(typeof(CURSOMETADATA))]
    public partial class CURSO
    {
        public string NombreAsignatura
        {
            get
            {
                var db = new GestionProgramasEntities();
                return (from e in db.ASIGNATURAs where IDENTIFICADORASIGNATURA == e.IDENTIFICADORASIGNATURA select e.NOMBREASIGNATURA).First();
            }
        }
        public string NombreDocente
        {
            get
            {
                var db = new GestionProgramasEntities();
                var nombreDocente = (from e in db.DOCENTEs where IDENTIFICADORUSUARIO == e.IDENTIFICADORUSUARIO select e.NOMBRESUSUARIO).FirstOrDefault();
                if (String.IsNullOrEmpty(nombreDocente))
                    return "";
                else
                    return nombreDocente;
            }
        }

    }
    public class CURSOMETADATA
    {
        [Required]
        public decimal IDENTIFICADORASIGNATURA { get; set; }
        [Required]
        public decimal IDENTIFICADORUSUARIO { get; set; }
        [Required]
        [StringLength(120, ErrorMessage = "El {0} no pueder ser mayor de 120 caracteres")]
        [NombreCursoYaExiste(ErrorMessage = "Ingrese otro nombre, ya existe un Curso usando ese nombre")]
        public string NOMBRECURSO { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "El {0} no pueder ser mayor de 30 caracteres")]
        public string ESTADOCURSO { get; set; }
    }

    public sealed class NombreCursoYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var cursoValidacion = validationContext.ObjectInstance as CURSO;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionProgramasEntities();
                var curso = (from e in db.CURSOes where cursoValidacion.IDENTIFICADORCURSO == e.IDENTIFICADORCURSO select e).FirstOrDefault();
                if (curso != null)
                {
                    if (value.Equals(curso.NOMBRECURSO))
                        valid = true;
                }
                if (valid != true)
                {
                    var nombreCurso = (from e in db.CURSOes where nombre == e.NOMBRECURSO select e.NOMBRECURSO).FirstOrDefault
                            ();
                    if (String.IsNullOrEmpty(nombreCurso))
                        valid = true;
                }
            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }

}