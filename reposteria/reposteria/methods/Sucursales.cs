using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using reposteria.clases;

namespace reposteria.methods
{
    class Sucursales
    {
        cSucursal sucursal = new cSucursal();
        public bool guardarSucursal(int id, string Nombre, string Colonia, string Calle, string NumInt ,  string NumExt , string CodigoPostal, string Ciudad, string Estado, string Tipo, string Telefono)
        {
            sucursal.SucursalId = id == -1 ? sucursal.SucursalId = -1 : sucursal.SucursalId = id;
            sucursal.Nombre = Nombre;
            sucursal.Colonia = Colonia;
            sucursal.Calle = Calle;
            sucursal.NumInt = NumInt;
            sucursal.NumExt = NumExt;
            sucursal.CodigoPostal = CodigoPostal;
            sucursal.Ciudad = Ciudad;
            sucursal.Estado = Estado;
            sucursal.Tipo = int.Parse(Tipo);
            sucursal.Telefono = Telefono;
            sucursal.Conexion = 2;

            
            var json = JsonConvert.SerializeObject(sucursal);
            //Console.WriteLine(json);

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://www.facturadp.com/kenduAPI/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.PostAsJsonAsync("api/sucursal", sucursal).Result;
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
