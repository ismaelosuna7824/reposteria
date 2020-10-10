using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using reposteria.clases;

namespace reposteria
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            //pictureBox1.Image.RotateFlip((RotateFlipType.Rotate90FlipX));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.facturadp.com/kenduAPI/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.  
            HttpResponseMessage response = client.GetAsync($"usuario/?Usuario={txtUser.Text}&Password={txtUser.Text}&Conexion=2").Result;  // Blocking call!  
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                //clientes = response.Content.ReadAsAsync<List<publicId>>().Result;
                var res = response.Content.ReadAsAsync<List<dynamic>>().Result;
                if (products.Length == 2) {
                    MessageBox.Show("El Usuario o contraseña son incorrectos");
                }
                else
                {
                   // var outs = products.TrimStart('[').TrimEnd(']').Split(',');

                    publicId.IdUsuario = res[0].IdUsuario;
                    //int a = outs[1].Length;
                    publicId.nombre = res[0].Nombre;
                    venta frm = new venta();
                    frm.Show();
                    this.Hide();
                }

                //if(products.ToString().Length = 2) { }
                /*
                
                */
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            Console.ReadLine();
            /*
            venta frm = new venta();
            frm.Show();
            this.Hide();*/

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
