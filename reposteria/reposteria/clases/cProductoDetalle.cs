using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reposteria.clases
{
    class cProductoDetalle
    {
        public int IdProductoDetalle { get; set; }
        public int IdProducto { get; set; }
        public double CostoProveedor { get; set; }
        public double Precio { get; set; }
        public double PrecioPromocion { get; set; }
    }
}
