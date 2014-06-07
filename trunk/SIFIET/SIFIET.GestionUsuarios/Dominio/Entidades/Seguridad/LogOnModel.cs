using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
 

namespace SIFIET.GestionUsuarios.Dominio.Entidades.Seguridad
{
    public class LogOnModel
    {
        [Required]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Caracteres no validos")]
        [DisplayName("Usuario")]
        public string UserName { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña")]
        public string Password { get; set; }
 
        [DisplayName("Recordarme?")]
        public bool RememberMe { get; set; }
    }

}

