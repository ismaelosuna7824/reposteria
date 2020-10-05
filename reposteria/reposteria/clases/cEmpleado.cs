using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reposteria.clases
{
    class cEmpleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaAlta { get; set; }
        public int Status { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int Tipo { get; set; }
        public int Conexion { get; set; }
    }
}
