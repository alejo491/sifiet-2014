using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionUsuarios.Datos.Modelo;

namespace SIFIET.GestionUsuarios.Dominio.Entidades.Parciales
{
    [MetadataType(typeof(USUARIOMETADATA))]
    public partial class USUARIO
    {
    }

    public class USUARIOMETADATA{
            public decimal IDENTIFICADORUSUARIO { get; set; }

            [Required]
            [StringLength(15, ErrorMessage = "El {0} no pueder ser mayor de 15 caracteres")]
            public string EMAILINSTITUCIONALUSUARIO { get; set; }

            [Required]
            [StringLength(15,ErrorMessage = "La {0} de estar entre 6 y 15 caracteres",MinimumLength = 6)]
            public string PASSWORDUSUARIO { get; set; }

            [Required]
            [IDYaExiste(ErrorMessage = "Esta Identificacion ya esta en uso, ingrese otro")]
            [StringLength(10, ErrorMessage = "La {0} no pueder ser mayor de 10 caracteres")]
            [EsNumeric(ErrorMessage = "La {0} solo toma valores numericos ")]
            public string IDENTIFICACIONUSUARIO { get; set; }

            [Required]
            public string NOMBRESUSUARIO { get; set; }

            [Required]
            public string APELLIDOSUSUARIO { get; set; }


            public string ESTADOUSUARIO { get; set; }

            public sealed class IDYaExiste : ValidationAttribute
            {
                protected override ValidationResult IsValid(object value, ValidationContext validationContext)
                {
                    bool valid = false;
                    
                    if (value != null)
                    {
                        
                        if (valid != true)
                        {
                            var idUsuarioValor = value as string;
                            var db = new GestionUsuariosEntities();
                            var idUsuario = (from e in db.USUARIOs where idUsuarioValor.Equals(e.IDENTIFICACIONUSUARIO) select e.IDENTIFICACIONUSUARIO).FirstOrDefault();
                            if (String.IsNullOrEmpty(idUsuario))
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
                            var idUsuarioValor = value as string;
                            int x;
                            if (int.TryParse(idUsuarioValor,out x))
                                valid = true;
                        }

                    }
                    return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
                }
            }
        
    }
}
