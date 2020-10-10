using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reposteria.clases
{
    class cProducto
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int Status { get; set; }
        public cProductoDetalle Detalle { get; set; }
        public int Conexion { get; set; }
    }
}
