using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reposteria.clases
{
    class cVentaDetalle
    {
        public int IdVentaDetalle { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public double Cantidad { get; set; }
        public double PrecioVenta { get; set; }
        public double Importe { get; set; }
        public double Costo { get; set; }
    }
}
