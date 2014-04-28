using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIFIET.GestionUsuarios.Aplicacion;

namespace SIFIET.GestionUsuarios.Datos.Modelo
{
    [MetadataType(typeof(ROLMETADATA))]
    public partial class ROL
    {
    }

    public class ROLMETADATA
    {
        [Display(Name = "Nombre")]
        [Required]
        [StringLength(50,ErrorMessage = "El {0} no Puede ser Mayor de 50 Caracteres")]
        public string NOMBREROL { get; set; }
        [Display(Name = "Descripcion")]
        [Required]
        [StringLength(250, ErrorMessage = "El {0} no Puede ser Mayor de 250 Caracteres")]
        public string DESCRIPCIONROL { get; set; }
    }

    public sealed class NombreExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var rolValidacion = validationContext.ObjectInstance as ROL;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionUsuariosEntities();
                var rol = (from e in db.ROLs where rolValidacion.IDENTIFICADORROL == e.IDENTIFICADORROL select e).FirstOrDefault();
                if (rol != null)
                {
                    if (value.Equals(rol.NOMBREROL.Trim()))
                        valid = true;
                }
                if (valid != true)
                {
                    var nombreAsignatura = (from e in db.ROLs where nombre.Trim().Equals(e.NOMBREROL.Trim()) select e.NOMBREROL).FirstOrDefault
                            ();
                    if (String.IsNullOrEmpty(nombreAsignatura))
                        valid = true;
                }
            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
