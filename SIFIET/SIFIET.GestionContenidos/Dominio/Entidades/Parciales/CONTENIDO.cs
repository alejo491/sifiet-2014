using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionContenidos.Datos.Modelo
{    
    [MetadataType(typeof(CONTENIDOMETADATA))]
    public partial class CONTENIDO
    {        
        public string operacion { get; set; }
        public string autorContenido 
        { 
            get
            {
                var db = new GestionContenidosEntities();
                var usuario = (from e in db.USUARIOs where IDENTIFICADORUSUARIO == e.IDENTIFICADORUSUARIO select e).FirstOrDefault();
                if (usuario != null)
                    return usuario.NOMBRESUSUARIO;
                else
                    return "";
            }
            set 
            {
                if (!String.IsNullOrEmpty(value))
                {
                    var db = new GestionContenidosEntities();
                    var usuario = (from e in db.USUARIOs where value.Equals(e.EMAILINSTITUCIONALUSUARIO) select e).FirstOrDefault();
                    if (usuario != null)                    
                        IDENTIFICADORUSUARIO = usuario.IDENTIFICADORUSUARIO;
                    else                    
                        IDENTIFICADORUSUARIO = 0;
                }
            }
        }
        public string NombreCategoria
        {
            get
            {
                var db = new GestionContenidosEntities();
                return (from e in db.CATEGORIAs where e.IDENTIFICADORCATEGORIA == IDENTIFICADORCATEGORIA select e.NOMBRECATEGORIA).FirstOrDefault();
            }
        }
       
    }

    public class CONTENIDOMETADATA
    {        
        [Required]
        [TituloContenidoYaExiste(ErrorMessage = "Ingrese otro titulo, ya existe un Contenidod usando ese titulo")]
        [StringLength(255, ErrorMessage = "El {0} no pueder ser mayor de 255 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-zÑñáéíóúÁÉÍÓÚ_]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string TITULOCONTENIDO { get; set; }

        [StringLength(1000, ErrorMessage = "El {0} no pueder ser mayor de 1000 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-zÑñáéíóúÁÉÍÓÚ_]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string DESCRIPCIONCONTENIDO { get; set; }

        [StringLength(4000, ErrorMessage = "El {0} no pueder ser mayor de 4000 caracteres")]
        [RegularExpression(@"^[A-Z0-9 a-zÑñáéíóúÁÉÍÓÚ_]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        public string CUERPOCONTENIDO { get; set; }        

        [Required]
        [StringLength(50, ErrorMessage = "El {0} no pueder ser mayor de 50 caracteres")]
        public string ESTADOCONTENIDO { get; set; }

        [RegularExpression(@"^[A-Z0-9 a-zÑñáéíóúÁÉÍÓÚ_]*$", ErrorMessage = "Caracteres Inválidos, Solo ingresa números y letras")]//Solo Numero y letras
        [StringLength(100, ErrorMessage = "El {0} no pueder ser mayor de 100 caracteres")]
        public string PRIORIDADCONTENIDO { get; set; }
    }

    public sealed class TituloContenidoYaExiste : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            var contenidoValidacion = validationContext.ObjectInstance as CONTENIDO;
            if (value != null)
            {
                var nombre = value as string;
                var db = new GestionContenidosEntities();
                var contenido = (from e in db.CONTENIDOes where contenidoValidacion.IDENTIFICADORCONTENIDO == e.IDENTIFICADORCONTENIDO select e).FirstOrDefault();
                if (contenido != null)
                {
                    if (value.Equals(contenido.TITULOCONTENIDO))
                        valid = true;
                }
                if (valid != true)
                {
                    var TITULOCONTENIDO = (from e in db.CONTENIDOes where nombre == e.TITULOCONTENIDO select e.TITULOCONTENIDO).FirstOrDefault
                            ();
                    if (String.IsNullOrEmpty(TITULOCONTENIDO))
                        valid = true;
                }
            }
            return valid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
