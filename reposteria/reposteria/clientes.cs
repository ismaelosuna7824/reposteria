using Guna.UI.WinForms;
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
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using reposteria.clases;

namespace reposteria
{
    public partial class clientes : Form
    {
        private DataView dv;
        DataTable dt;
        int id = 0;
        public clientes()
        {
            InitializeComponent();
            dgvClientes.ColumnHeadersHeight = 25;
            getsClientes();
        }

        private void txtNombre_Click(object sender, EventArgs e)
        {
            
        }

        private void txtNombre_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void gunaLabel3_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            cCliente cliente = new cCliente();
            if (id != 0) cliente.IdCliente = 1; else cliente.IdCliente = -1;
            cliente.Nombre = txtNombre.Text;
            cliente.ApePaterno = txtApePaterno.Text;
            cliente.ApeMaterno = txtApeMaterno.Text;
            cliente.Email = txtEmail.Text;
            cliente.Telefono = txtTelefono.Text;
            cliente.Calle = txtCalle.Text;
            cliente.NoExt = txtNomExt.Text;
            cliente.NoInt = txtNomInt.Text;
            cliente.Colonia = txtColonia.Text;
            cliente.Ciudad = txtCiudad.Text;
            cliente.FechaAlta = DateTime.Now;
            cliente.Comentario = txtComentarios.Text;
            cliente.Conexion = 2;

            if(txtNombre.Text == "" || txtTelefono.Text == "")
            {
                MessageBox.Show("El nombre y Teléfono son obligatorios");
            }
            else {

                var json = JsonConvert.SerializeObject(cliente);
                // Console.WriteLine(json);

                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://www.facturadp.com/kenduAPI/";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var response = client.PutAsJsonAsync("api/cliente", cliente).Result;
                    Console.WriteLine(response.StatusCode);
                    //MessageBox.Show("Se ha Agregado el cliente correctamente");
                    if (response.IsSuccessStatusCode)
                    {
                        mensaje(id);
                        ClearTextBoxes();
                        getsClientes();
                        
                    }
                }

            }

            




        }

        private void gunaTileButton1_Click(object sender, EventArgs e)
        {
            venta frm = new venta();
            frm.Show();
            this.Close();
        }

        public void getsClientes()
        {
            cCliente cliente = new cCliente();
            cliente.IdCliente = -1;
            string Busca = "";
            List<cCliente> clientes = new List<cCliente>();

            HttpClient client = new HttpClient();
            var vURLservicio = "http://www.facturadp.com/kenduAPI/api/Cliente/";


            if (String.IsNullOrEmpty(Busca))
                Busca = "''";

            string urlParametro = "?idCliente=" + cliente.IdCliente + "&Buscar=" + Busca + "&Conexion=2";
            client.BaseAddress = new Uri(vURLservicio);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParametro).Result;
            if (response.IsSuccessStatusCode)
            {
                clientes = response.Content.ReadAsAsync<List<cCliente>>().Result;
                //clientes.ForEach(i => Console.WriteLine(i.Nombre));



                dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("ApePaterno");
                dt.Columns.Add("ApeMaterno");
                dt.Columns.Add("Email");
                dt.Columns.Add("Telefono");
                dt.Columns.Add("Calle");
                dt.Columns.Add("NoExt");
                dt.Columns.Add("NoInt");
                dt.Columns.Add("Colonia");
                dt.Columns.Add("Ciudad");
                dt.Columns.Add("FechaAlta");
                dt.Columns.Add("Comentario");


                foreach (var clien in clientes)
                {
                    /*
                    
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(gunaDataGridView1);

                    row.Cells[0].Value = clien.IdCliente;
                    row.Cells[1].Value = clien.Nombre;
                    row.Cells[2].Value = clien.ApePaterno;
                    row.Cells[3].Value = clien.ApeMaterno;
                    row.Cells[4].Value = clien.Telefono;
                    row.Cells[5].Value = clien.Email;
                    gunaDataGridView1.Rows.Add(row); */
                    dt.Rows.Add(clien.IdCliente, clien.Nombre, clien.ApePaterno, clien.ApeMaterno, clien.Email, clien.Telefono, clien.Calle, clien.NoExt, clien.NoInt, clien.Colonia, clien.Ciudad, clien.FechaAlta, clien.Comentario);


                }
                //dt.Columns[0].ColumnMapping = MappingType.Hidden;


                dv = new DataView(dt);
                dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvClientes.DataSource = dv;
                dgvClientes.Columns["id"].Visible = false;
                dgvClientes.Columns["NoInt"].Visible = false;
                dgvClientes.Columns["FechaAlta"].Visible = false;
                dgvClientes.Columns["Ciudad"].Visible = false;
                dgvClientes.Columns["Ciudad"].Visible = false;


            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            dv.RowFilter = string.Format("Nombre Like '%{0}%'", txtNombre.Text.Trim());
            dgvClientes.DataSource = dv;
        }

        private void clientes_Load(object sender, EventArgs e)
        {

        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void bunifuMaterialTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void gunaLineTextBox1_TextChanged(object sender, EventArgs e)
        {
            dv = dt.DefaultView;
            dv.RowFilter = "Nombre LIKE '" + txtSearch.Text + "%'";
            dgvClientes.DataSource = dv;
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(this.dgvClientes.CurrentRow.Cells[0].Value.ToString()); 
            txtNombre.Text = this.dgvClientes.CurrentRow.Cells[1].Value.ToString();
            txtApePaterno.Text = this.dgvClientes.CurrentRow.Cells[2].Value.ToString();
            txtApeMaterno.Text = this.dgvClientes.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = this.dgvClientes.CurrentRow.Cells[4].Value.ToString();
            txtTelefono.Text = this.dgvClientes.CurrentRow.Cells[5].Value.ToString();
            txtCalle.Text = this.dgvClientes.CurrentRow.Cells[6].Value.ToString();
            txtNomExt.Text = this.dgvClientes.CurrentRow.Cells[7].Value.ToString();
            txtNomInt.Text = this.dgvClientes.CurrentRow.Cells[8].Value.ToString();
            txtColonia.Text = this.dgvClientes.CurrentRow.Cells[9].Value.ToString();
            txtCiudad.Text = this.dgvClientes.CurrentRow.Cells[10].Value.ToString();
            txtComentarios.Text = this.dgvClientes.CurrentRow.Cells[12].Value.ToString();
        }


        public void mensaje(int i)
        {
            if(i != 0)
            {
                MessageBox.Show("El Cliete ha sido Actualizado");
            }
            else
            {
                MessageBox.Show("El Cliete ha sido Guardado");

            }
        }

        public void ClearTextBoxes()

        {
            id = 0;
            txtNombre.Text = "";
            txtApePaterno.Text = "";
            txtApeMaterno.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtCalle.Text = "";
            txtNomExt.Text = "";
            txtNomInt.Text = "";
            txtColonia.Text = "";
            txtCiudad.Text = "";
            txtComentarios.Text = "";




        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            ClearTextBoxes();
            
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            string carac = "1234567890" + (char)8;
            if (!carac.Contains(e.KeyChar))
            {
                e.Handled = true;

            }
            else if (e.KeyChar == 46)
            {
                if (txtTelefono.Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtNomExt_KeyPress(object sender, KeyPressEventArgs e)
        {
            string carac = "1234567890" + (char)8;
            if (!carac.Contains(e.KeyChar))
            {
                e.Handled = true;

            }
            else if (e.KeyChar == 46)
            {
                if (txtTelefono.Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtNomInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            string carac = "1234567890" + (char)8;
            if (!carac.Contains(e.KeyChar))
            {
                e.Handled = true;

            }
            else if (e.KeyChar == 46)
            {
                if (txtTelefono.Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
