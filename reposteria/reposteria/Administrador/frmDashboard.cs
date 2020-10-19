using reposteria.clases;
using reposteria.methods;
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

namespace reposteria.Administrador
{
    
    public partial class frmDashboard : Form
    {
        private DataView dv;
        DataTable dt;
        int id = -1;
        List<cCategoria> clientes = new List<cCategoria>();
        public frmDashboard()
        {
            InitializeComponent();
            //getsCategorias();
            

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {

        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuMaterialTextbox3_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            tbs.SelectTab(tbsProductos);
            getsCategorias();
            cbxProductos.DataSource = clientes;
            cbxProductos.DisplayMember = "NombreCategoria";
            cbxProductos.ValueMember = "idCategoria";
            getsProductos();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            tbs.SelectTab(tbsCategorias);
            getsCategorias();
        }

        private void btnPromociones_Click(object sender, EventArgs e)
        {
            tbs.SelectTab(tbsSucursales);
            getsSucursales();


        }

        private void bntGcategoria_Click(object sender, EventArgs e)
        {
            Categoria methods = new Categoria();
            if(txtCNombre.Text == "")
            {
                MessageBox.Show("ingresar nombre de la categoria");
            }
            else {
                if (methods.guardarCategoria(txtCNombre.Text, id))
                {
                    MessageBox.Show("Se ha guardado la categoria correctamente");
                    getsCategorias();
                    txtCNombre.Text = "";
                    id = -1;
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
            
        }
        public void getsCategorias()
        {
            cCategoria cliente = new cCategoria();
            cliente.idCategoria = -1;
            string Busca = "";
            

            HttpClient client = new HttpClient();
            var vURLservicio = "http://www.facturadp.com/kenduAPI/api/Categorias/";


            if (String.IsNullOrEmpty(Busca))
                Busca = "''";

            string urlParametro = "?IdCategoria=" + cliente.idCategoria + "&Conexion=2";
            client.BaseAddress = new Uri(vURLservicio);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParametro).Result;
            if (response.IsSuccessStatusCode)
            {
                clientes = response.Content.ReadAsAsync<List<cCategoria>>().Result;
                //clientes.ForEach(i => Console.WriteLine(i.Nombre));



                dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("Nombre");


                foreach (var clien in clientes)
                {
                    dt.Rows.Add(clien.idCategoria, clien.NombreCategoria );


                }
                //dt.Columns[0].ColumnMapping = MappingType.Hidden;


                dv = new DataView(dt);
                dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvCategorias.DataSource = dv;
                dgvCategorias.Columns["id"].Visible = false;


            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }
        public void getsSucursales()
        {
            cSucursal sucursal = new cSucursal();
            sucursal.SucursalId = -1;
            string Busca = "";
            List<cSucursal> clientes = new List<cSucursal>();

            HttpClient client = new HttpClient();
            var vURLservicio = "http://www.facturadp.com/kenduAPI/api/sucursal/";


            if (String.IsNullOrEmpty(Busca))
                Busca = "''";

            string urlParametro = "?IdSucursal=" + sucursal.SucursalId + "&Conexion=2";
            client.BaseAddress = new Uri(vURLservicio);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParametro).Result;
            if (response.IsSuccessStatusCode)
            {
                clientes = response.Content.ReadAsAsync<List<cSucursal>>().Result;
                //clientes.ForEach(i => Console.WriteLine(i.Nombre));



                dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Colonia");
                dt.Columns.Add("Calle");
                dt.Columns.Add("NumInt");
                dt.Columns.Add("NumExt");
                dt.Columns.Add("CodigoPostal");
                dt.Columns.Add("Ciudad");
                dt.Columns.Add("Estado");
                dt.Columns.Add("Tipo");
                dt.Columns.Add("Telefono");



                foreach (var clien in clientes)
                {
                    dt.Rows.Add(clien.SucursalId, clien.Nombre, clien.Colonia, clien.Calle, clien.NumInt, clien.NumExt, clien.CodigoPostal, clien.Ciudad, clien.Estado, clien.Tipo, clien.Telefono);


                }
                //dt.Columns[0].ColumnMapping = MappingType.Hidden;


                dv = new DataView(dt);
                dgvSucursales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvSucursales.DataSource = dv;
                dgvSucursales.Columns["id"].Visible = false;


            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }
        public void getsProductos()
        {
            cProducto producto = new cProducto();
            cProductoDetalle detalle1 = new cProductoDetalle();
            producto.IdProducto = -1;
            string Busca = "";
            List<cProducto> clientes = new List<cProducto>();
            List<cProductoDetalle> detalle = new List<cProductoDetalle>();


            HttpClient client = new HttpClient();
            var vURLservicio = "http://www.facturadp.com/kenduAPI/api/Producto/";


            if (String.IsNullOrEmpty(Busca))
                Busca = "''";

            string urlParametro = "?IdCategoria=" + producto.IdProducto + "&Conexion=2";
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
                dt.Columns.Add("IdCategoria");
                dt.Columns.Add("NombreCategoria");
                dt.Columns.Add("Status");
                dt.Columns.Add("IdProductoDetalle");
                dt.Columns.Add("CostoProveedor");
                dt.Columns.Add("Precio");
                dt.Columns.Add("Precio Promocion");





                foreach (var clien in clientes)
                {
                    dt.Rows.Add(clien.IdProducto, clien.Descripcion, clien.IdCategoria, clien.NombreCategoria, clien.Status, clien.Detalle.IdProductoDetalle,  clien.Detalle.CostoProveedor, clien.Detalle.Precio, clien.Detalle.PrecioPromocion);


                }
                //dt.Columns[0].ColumnMapping = MappingType.Hidden;


                dv = new DataView(dt);

                dgvProducto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvProducto.DataSource = dv;
                dgvProducto.Columns["id"].Visible = false;
                dgvProducto.Columns["IdCategoria"].Visible = false;
                dgvProducto.Columns["Status"].Visible = false;
                dgvProducto.Columns["IdProductoDetalle"].Visible = false;
               


            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(this.dgvCategorias.CurrentRow.Cells[0].Value.ToString());
            txtCNombre.Text = this.dgvCategorias.CurrentRow.Cells[1].Value.ToString();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            if(txtSnombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa el nombre de la sucursal");
            }
            else {
                Sucursales methods = new Sucursales();
                if (methods.guardarSucursal(id, txtSnombre.Text, txtScolonia.Text, txtScalle.Text, txtSnumint.Text, txtSnumext.Text, txtScodigop.Text, txtSciudad.Text, txtSestado.Text, txtStipo.Text, txtStelefono.Text))
                {
                    MessageBox.Show("Se ha agregado la sucursal correctamente");
                    id = -1;
                    getsSucursales();
                    txtSnombre.Text = "";
                    txtScolonia.Text = "";
                    txtScalle.Text = "";
                    txtSnumint.Text = "";
                    txtSnumext.Text = "";
                    txtScodigop.Text = "";
                    txtSciudad.Text = "";
                    txtSestado.Text = "";
                    txtStipo.Text = "";
                    txtStelefono.Text = "";
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
            
        }

        private void dgvSucursales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(this.dgvSucursales.CurrentRow.Cells[0].Value.ToString());
            txtSnombre.Text = this.dgvSucursales.CurrentRow.Cells[1].Value.ToString();
            txtScolonia.Text = this.dgvSucursales.CurrentRow.Cells[2].Value.ToString();
            txtScalle.Text = this.dgvSucursales.CurrentRow.Cells[3].Value.ToString();
            txtSnumint.Text = this.dgvSucursales.CurrentRow.Cells[4].Value.ToString();
            txtSnumext.Text = this.dgvSucursales.CurrentRow.Cells[5].Value.ToString();
            txtScodigop.Text = this.dgvSucursales.CurrentRow.Cells[6].Value.ToString();
            txtSciudad.Text = this.dgvSucursales.CurrentRow.Cells[7].Value.ToString();
            txtSestado.Text = this.dgvSucursales.CurrentRow.Cells[8].Value.ToString();
            txtStipo.Text = this.dgvSucursales.CurrentRow.Cells[9].Value.ToString();
            txtStelefono.Text = this.dgvSucursales.CurrentRow.Cells[10].Value.ToString();
        }

        private void txtPCosto_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto methods = new Producto();
            if (txtPDescripcion.Text.Trim() == "" || txtPprecio.Text.Trim() == "")
            {
                MessageBox.Show("Falta la descripcion o precio del producto");
            }
            else
            {
               
                 if(methods.guardarProducto(id, txtPDescripcion.Text, cbxProductos.SelectedValue.ToString(), cbxProductos.GetItemText(cbxProductos.SelectedItem), double.Parse(txtPCosto.Text), double.Parse(txtPprecio.Text), double.Parse(txtPMayoreo.Text)))
                 {
                     MessageBox.Show("Se ha agregado el producto correctamente");
                     id = -1;
                     txtPDescripcion.Text = "";
                     txtPCosto.Text = "";
                     txtPprecio.Text = "";
                     txtPMayoreo.Text = "";
                     getsProductos();
                 }
                 else
                 {
                     MessageBox.Show("Ha ocurrido un error");
                 }
                
            }
            
        }

        private void dgvProducto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(this.dgvProducto.CurrentRow.Cells[0].Value.ToString());
            txtPDescripcion.Text = this.dgvProducto.CurrentRow.Cells[1].Value.ToString();
            txtPCosto.Text = this.dgvProducto.CurrentRow.Cells[6].Value.ToString();
            txtPprecio.Text = this.dgvProducto.CurrentRow.Cells[7].Value.ToString();
            txtPMayoreo.Text = this.dgvProducto.CurrentRow.Cells[8].Value.ToString();

            cbxProductos.SelectedValue = int.Parse(this.dgvProducto.CurrentRow.Cells[2].Value.ToString());
        }

        private void txtPCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            string carac = "1234567890." + (char)8;
            if (!carac.Contains(e.KeyChar))
            {
                e.Handled = true;

            }
            else if (e.KeyChar == 46)
            {
                if (txtPCosto.Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            string carac = "1234567890." + (char)8;
            if (!carac.Contains(e.KeyChar))
            {
                e.Handled = true;

            }
            else if (e.KeyChar == 46)
            {
                if (txtPprecio.Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPMayoreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            string carac = "1234567890." + (char)8;
            if (!carac.Contains(e.KeyChar))
            {
                e.Handled = true;

            }
            else if (e.KeyChar == 46)
            {
                if (txtPMayoreo.Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
