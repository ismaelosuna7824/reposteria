using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reposteria.clases
{
    class cVenta
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public int IdSucursal { get; set; }
        public int IdEmpleado { get; set; }
        public string Empleado { get; set; }
        public string FechaVenta { get; set; }
        public string HoraVenta { get; set; }
        public List<cVentaDetalle> Detalle { get; set; }
        public double Subtotal { get; set; }
        public double Total { get; set; }
        public string Comentario { get; set; }
        public int Status { get; set; }
        public double SuPago { get; set; }
        public double SuCambio { get; set; }
        public int TotalArticulos { get; set; }
        public int Conexion { get; set; }
        public int TipoVenta { get; set; }
    }
}
