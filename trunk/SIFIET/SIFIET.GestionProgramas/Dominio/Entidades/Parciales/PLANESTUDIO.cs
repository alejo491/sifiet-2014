using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionProgramas.Datos.Modelo
{
    [MetadataType(typeof(PLANESTUDIOMETADATA))]
    public partial class PLANESTUDIO
    {
    }

    public class PLANESTUDIOMETADATA
    {
        [Display(Name = "Nombre del Plan :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string NOMBREPLANESTUDIOS { get; set; }

        [Display(Name = "Descripción :")]
        public string DESCRIPCIONPLANESTUDIOS { get; set; }

        [Display(Name = "Fecha Inicio :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string FECHAINICIOPLANESTUDIOS { get; set; }

        [Display(Name = "Fecha Fin :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string FECHAFINPLANESTUDIOS { get; set; }

        [Display(Name = "Codigo del Plan :")]
        [Required(ErrorMessage = "Este campos es requerido")]
        public string CODIGOPLANESTUDIOS { get; set; }

        [Display(Name = "Estado :")]
        public string ESTADOPLANESTUDIOS { get; set; }

        public virtual PROGRAMA PROGRAMA { get; set; }

    }
}
