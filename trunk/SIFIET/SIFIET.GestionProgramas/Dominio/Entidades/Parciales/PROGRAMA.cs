﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionProgramas.Datos.Modelo
{
    [MetadataType(typeof(PROGRAMAMETADATA))]
    public partial class PROGRAMA
    {
        public string operacion { get; set; }
    }

    public class PROGRAMAMETADATA
    {


        [Display(Name = "Codigo SNIES:")]
        [Required(ErrorMessage = "Este campos es requerido")]
        [DisplayFormat(DataFormatString = "{0:#}", ApplyFormatInEditMode = true)]
        //[CodigoSNIESExiste(ErrorMessage = "Ya existe un Programa con el codigo que desea registrar, por favor cambie el campo si desea crear otro Programa, o realice una búsqueda para editar los campos del Programa existente.")]
        public Nullable<decimal> CODIGOSNIESPROGRAMA { get; set; }


        [Display(Name = "Nombre del programa:")]
        [Required(ErrorMessage = "Este campos es requerido")]
        [StringLength(120, ErrorMessage = "El {0} no pueder ser mayor de 120 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-z]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        [ProgramaNombreExiste(ErrorMessage = "Ya existe un Programa con el nombre que desea registrar, por favor cambie el campo si desea crear otro Programa, o realice una búsqueda para editar los campos del Programa existente.")]
        public string NOMBREPROGRAMA { get; set; }

        [Display(Name = "Descripción :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        [StringLength(250, ErrorMessage = "El {0} no pueder ser mayor de 250 caracteres")]
        //[RegularExpression(@"^[A-Z0-9 a-z]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string DESCRIPCIONPROGRAMA { get; set; }

        [Display(Name = "Facultad:")]
        //[Required(ErrorMessage = "Este campos es requerido")]
        public virtual FACULTAD FACULTAD { get; set; }

        [Display(Name = "Periodo :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string PERIODODURACIONPROGRAMA { get; set; }

        [Display(Name = "Duración :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> DURACIONPROGRAMA { get; set; }

        [Display(Name = "Modalidad :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string MODALIDADPROGRAMA { get; set; }

        [Display(Name = "Jornada :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string JORNADAPROGRAMA { get; set; }

        [Display(Name = "Admisión :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string ADMISIONPROGRAMA { get; set; }

        [Display(Name = "Estado :")]
        public string ESTADOPROGRAMA { get; set; }

    }

    public sealed class ProgramaNombreExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var programaValidacion = validationContext.ObjectInstance as PROGRAMA;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionProgramasEntities();
                if (programaValidacion.operacion == "editar" || programaValidacion.operacion == "eliminar")
                {
                    var programaNombre = (from e in db.PROGRAMAs where programaValidacion.IDENTIFICADORPROGRAMA == e.IDENTIFICADORPROGRAMA select e.NOMBREPROGRAMA).FirstOrDefault();
                    var nombrePrograma = (from e in db.PROGRAMAs where nombre.ToUpper() == e.NOMBREPROGRAMA.ToUpper() && e.NOMBREPROGRAMA.ToUpper() != programaNombre.ToUpper() select e.NOMBREPROGRAMA).FirstOrDefault();
                    if (String.IsNullOrEmpty(nombrePrograma))
                    {
                        valid = true;
                    }
                }
                else
                {
                    var nombrePrograma = (from e in db.PROGRAMAs where nombre.ToUpper() == e.NOMBREPROGRAMA.ToUpper() select e.NOMBREPROGRAMA).FirstOrDefault();
                    if (String.IsNullOrEmpty(nombrePrograma))
                    {
                        valid = true;
                    }
                }

            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }

    
}