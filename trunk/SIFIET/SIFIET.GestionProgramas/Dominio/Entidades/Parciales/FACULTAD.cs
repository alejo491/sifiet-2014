using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionProgramas.Datos.Modelo
{
    [MetadataType(typeof(FACULTADMETADATA))]
    public partial class FACULTAD
    {
    }

    public class FACULTADMETADATA
    {
        [Display(Name = "ID Facultad :")]
        public decimal IDENTIFICADORFACULTAD { get; set; }

        [Display(Name = "Facultad :")]
        public string NOMBREFACULTAD { get; set; }

        [Display(Name = "Estado :")]
        public string ESTADOFACULTAD { get; set; }

        [Display(Name = "Codigo :")]
        public string CODIGOFACULTAD { get; set; }

    }
}