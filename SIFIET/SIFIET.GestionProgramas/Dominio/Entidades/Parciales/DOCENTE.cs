using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFIET.GestionProgramas.Datos.Modelo
{
    public partial class DOCENTE
    {
        public string NombreCompletoDocente
        {
            get
            {
                return NOMBRESUSUARIO + " " + APELLIDOSUSUARIO;
            }
        }
    }
}
