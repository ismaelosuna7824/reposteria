using reposteria.clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Windows.Forms;
using reposteria.Properties;
using Guna.UI.WinForms;

namespace reposteria
{
    public partial class venta : Form
    {

       
        string descripcion;
        double precios;
        int id;
        public int idCategoria;
        public int totalEriculos;
        public static int idCliente = -1;
        public venta()
        {
            InitializeComponent();
            Console.WriteLine(publicId.IdUsuario);
            lblNombreE.Text = publicId.nombre;
            //pictureBox1.LoadAsync(@"https://www.gettyimages.es/gi-resources/images/frontdoor/editorial/Velo/GettyImages-Velo-1088643550.jpg");
            // gunaPictureBox1.LoadAsync(@"https://www.gettyimages.es/gi-resources/images/frontdoor/editorial/Velo/GettyImages-Velo-1088643550.jpg");
            gunaDataGridView1.ColumnHeadersHeight = 25;
            gunaComboBox1.SelectedIndex = gunaComboBox1.Items.IndexOf("Ventanilla");

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            login frm = new login();
            frm.Show();
            this.Hide();
        }



        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            clientes.update = 0;
            clientes frm = new clientes();
            AddOwnedForm(frm);
            frm.Show();
        }

       
       
        private void getCategorias()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.facturadp.com/kenduAPI/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List all Names.  
            HttpResponseMessage response = client.GetAsync($"categorias/?IdCategoria=-1&Conexion=2").Result;  // Blocking call!  
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                if (products.Length == 2)
                {
                    MessageBox.Show("El Usuario o contraseña son incorrectos");
                }
                else
                {

                    //Console.WriteLine("entro aqui");
                    List<cCategoria> productos = new List<cCategoria>();
                    productos = response.Content.ReadAsAsync<List<cCategoria>>().Result;
                    int a = productos.Count;
                    Categorias[] listItmes = new Categorias[a];
                    Categorias nw = new Categorias();


                    for (int i = 0; i < a; i++)
                    {
                        listItmes[i] = new Categorias();
                        listItmes[i].id = productos[i].idCategoria.ToString().Trim();
                        listItmes[i].nombre = productos[i].NombreCategoria;
                        if (productos[i].UrlImagen.Length == 0)
                        {
                            listItmes[i].image = "https://www.creativefabrica.com/wp-content/uploads/2019/02/Loading-Icon-by-Kanggraphic-7-580x386.jpg";

                        }
                        else
                        {
                            listItmes[i].image = productos[i].UrlImagen.ToString().Trim();
                        }
                       // flowLayoutPanel1.Controls.Add(listItmes[i]);
                        

                    }
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            Console.ReadLine();


           




        }

        public  void getIdCategoria(string id)
        {
            Console.WriteLine("el id es " + id);
            obtenercategorias(id);
            
        }

        

       

        private void gunaAdvenceButton1_Click_1(object sender, EventArgs e)
        {
            double total = 0;
            totalEriculos = 0;
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dgvVenta);

            row.Cells[0].Value = id;
            row.Cells[1].Value = descripcion;
            row.Cells[2].Value = txtCantidad.Text;
            row.Cells[3].Value = precios.ToString();
            row.Cells[4].Value = int.Parse(txtCantidad.Text) * precios;
            dgvVenta.Rows.Add(row);

            
                foreach (DataGridViewRow rows in dgvVenta.Rows)
                {


                    total += Convert.ToDouble(rows.Cells["subtototal"].Value);
                    totalEriculos += Convert.ToInt32(rows.Cells["Cantidad"].Value);
                    labeltotal.Text = Convert.ToString("$ " +total);
                   




                }
            

            txtCantidad.Text = "";
        }

        private void venta_Load(object sender, EventArgs e)
        {
            
        }

        private void gunaAdvenceButton1_Click_2(object sender, EventArgs e)
        {
            obtenercategorias("2");
        }

        public void obtenercategorias(string ids)
        {
            DataView dv;
            DataTable dt;

            string Busca = "";
            List<cProducto> clientes = new List<cProducto>();

            HttpClient client = new HttpClient();
            var vURLservicio = "http://www.facturadp.com/kenduAPI/api/producto/";


            if (String.IsNullOrEmpty(Busca))
                Busca = "''";

            string urlParametro = "?IdCategoria=" + ids + "&Conexion=2";
            client.BaseAddress = new Uri(vURLservicio);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParametro).Result;
            if (response.IsSuccessStatusCode)
            {

                clientes = response.Content.ReadAsAsync<List<cProducto>>().Result;
                //clientes.ForEach(i => Console.WriteLine(i.Nombre));



                dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("Precio");


                foreach (var clien in clientes)
                {

                    dt.Rows.Add(clien.IdProducto, clien.Descripcion, clien.Detalle.Precio);


                }
                //dt.Columns[0].ColumnMapping = MappingType.Hidden;


                dv = new DataView(dt);
                gunaDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gunaDataGridView1.DataSource = dv;
                gunaDataGridView1.Columns["id"].Visible = false;






            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            obtenercategorias("1");
        }

        private void btnHe_Click(object sender, EventArgs e)
        {
            obtenercategorias("2");
        }

        private void btnEs_Click(object sender, EventArgs e)
        {
            obtenercategorias("3");

        }

        private void btnEx_Click(object sender, EventArgs e)
        {
            obtenercategorias("4");
        }

        private void gunaDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(this.gunaDataGridView1.CurrentRow.Cells[0].Value.ToString());
            descripcion = this.gunaDataGridView1.CurrentRow.Cells[1].Value.ToString();
            precios = double.Parse(this.gunaDataGridView1.CurrentRow.Cells[2].Value.ToString());
           
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(this.gunaDataGridView1.CurrentRow.Cells[0].Value.ToString());
            descripcion = this.gunaDataGridView1.CurrentRow.Cells[1].Value.ToString();
            precios = double.Parse(this.gunaDataGridView1.CurrentRow.Cells[2].Value.ToString());
        }

        private void dgvVenta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            double total = 0;
            totalEriculos = 0;
            dgvVenta.Rows.Remove(dgvVenta.CurrentRow);
            foreach (DataGridViewRow rows in dgvVenta.Rows)
            {


                total += Convert.ToDouble(rows.Cells["subtototal"].Value);
                totalEriculos += Convert.ToInt32(rows.Cells["Cantidad"].Value);
                labeltotal.Text = Convert.ToString("$ " + total);




            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            imprimeTicket ticket = new imprimeTicket();
            if (labeltotal.Text == "")
            {
                MessageBox.Show("No hay nada para vender");
            }
            else if (txtEfectivo.Text == "")
            {
                MessageBox.Show("No se ha ingresado una cantidad en efectivo");
            }
            else if (Double.Parse(txtEfectivo.Text) < Double.Parse(labeltotal.Text.Substring(1)))
            {
                MessageBox.Show("La cantidad de efectivo es menor al total");
            }
            else {

                double cambio;
                cVenta venta = new cVenta();
                cVentaDetalle vd = new cVentaDetalle();
                venta.IdVenta = -1;
                venta.IdCliente = idCliente;
                venta.IdSucursal = 1;
                venta.IdEmpleado = publicId.IdUsuario;
                venta.Empleado = publicId.nombre;
                venta.FechaVenta = DateTime.Now.ToString("yyy-MM-dd");
                venta.HoraVenta = DateTime.Now.TimeOfDay.ToString();
                venta.Subtotal = double.Parse(labeltotal.Text.Substring(1));
                venta.Total = double.Parse(labeltotal.Text.Substring(1));
                venta.Comentario = txtComentario.Text;
                venta.Status = 1;
                venta.SuPago = double.Parse(txtEfectivo.Text);
                cambio = Double.Parse(txtEfectivo.Text) - Double.Parse(labeltotal.Text.Substring(1));
                venta.SuCambio = cambio;
                venta.TotalArticulos = totalEriculos;
                venta.Conexion = 2;
                venta.TipoVenta = int.Parse(gunaComboBox1.SelectedIndex.ToString());
                venta.Detalle = new List<cVentaDetalle>();
                foreach (DataGridViewRow rows in dgvVenta.Rows)
                {
                    vd = new cVentaDetalle();
                    vd.IdVentaDetalle = -1;
                    vd.IdVenta = -1;
                    vd.IdProducto = int.Parse(rows.Cells["dataid"].Value.ToString());
                    vd.DescripcionProducto = rows.Cells["articulo"].Value.ToString();
                    vd.Cantidad = int.Parse(rows.Cells["Cantidad"].Value.ToString());
                    vd.PrecioVenta = double.Parse(rows.Cells["precio"].Value.ToString());
                    vd.Importe = double.Parse(rows.Cells["subtototal"].Value.ToString());
                    vd.Costo = double.Parse(rows.Cells["precio"].Value.ToString());
                    venta.Detalle.Add(vd);

                }

                var json = JsonConvert.SerializeObject(venta);
                // Console.WriteLine(json);
                // Console.WriteLine(json);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://www.facturadp.com/kenduAPI/";
                //Console.WriteLine(idCliente);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var response = client.PutAsJsonAsync("api/Venta", venta).Result;
                    Console.WriteLine(response.StatusCode);
                    //MessageBox.Show("Se ha Agregado el cliente correctamente");
                    if (response.IsSuccessStatusCode)
                    {
                        ticket.imprimirTicket(dgvVenta, "12334", publicId.nombre, txtComentario.Text, double.Parse(labeltotal.Text.Substring(1)), cambio, double.Parse(txtEfectivo.Text));
                        MessageBox.Show("su cambio es " + cambio);
                        idCliente = -1;
                        dgvVenta.Rows.Clear();
                        labeltotal.Text = "";
                        txtClientes.Text = "";
                        txtComentario.Text = "";
                        txtEfectivo.Text = "";
                        MessageBox.Show("La venta se ha realizado correctamente");
                        Console.WriteLine("hola");

                        
                    }
                }
            }

            //MessageBox.Show(gunaComboBox1.SelectedIndex.ToString());
        }

        private void txtClientes_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtClientes_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clientes.update = 1;
            clientes frm = new clientes();
            AddOwnedForm(frm);
            frm.Show();
        }
    }
}
