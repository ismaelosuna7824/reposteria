using Bunifu.Framework.UI;
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
   
    public class Categoria
    {
        cCategoria categoria = new cCategoria();
        public  bool guardarCategoria(string nombre, int id)
        {
            categoria.idCategoria = id == -1 ? categoria.idCategoria = -1: categoria.idCategoria  = id;
            categoria.NombreCategoria = nombre;
            categoria.Conexion = 2;
            categoria.UrlImagen = "";
            var json = JsonConvert.SerializeObject(categoria);
             Console.WriteLine(json);

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://www.facturadp.com/kenduAPI/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.PutAsJsonAsync("api/Categorias", categoria).Result;
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
