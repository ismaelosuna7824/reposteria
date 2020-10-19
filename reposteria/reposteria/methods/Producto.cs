using reposteria.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
namespace reposteria.methods
{
    class Producto
    {
        
        public bool guardarProducto(int id, string Descripcion, string IdCategoria, string NombreCategoria, double CostoProveedor, double Precio, double PrecioPromocion )
        {
            cProducto producto = new cProducto();
            
            producto.IdProducto = id == -1 ? producto.IdProducto = -1 : producto.IdProducto = id;
            producto.Descripcion = Descripcion;
            producto.IdCategoria = int.Parse(IdCategoria);
            producto.NombreCategoria = NombreCategoria;
            producto.Status = 1;
            producto.Conexion = 2;
            cProductoDetalle detalle = new cProductoDetalle();

            detalle.IdProductoDetalle = -1;
            detalle.IdProducto = int.Parse(IdCategoria);
            detalle.CostoProveedor = CostoProveedor;
            detalle.Precio = Precio;
            detalle.PrecioPromocion = PrecioPromocion;

            producto.Detalle = detalle;


            var json = JsonConvert.SerializeObject(producto);
            //Console.WriteLine(json);
           
                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                        var url = "http://www.facturadp.com/kenduAPI/";

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(url);
                            var response = client.PutAsJsonAsync("api/Producto", producto).Result;
                            Console.WriteLine(response.StatusCode);
                            //MessageBox.Show("Se ha Agregado el cliente correctamente");
                            if (response.IsSuccessStatusCode)
                            {
                                return true;

                            }
                        }
                        return false;
            
           
        }

    }
}
