using reposteria.clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reposteria
{
    public partial class venta : Form
    {
        public venta()
        {
            InitializeComponent();
            Console.WriteLine(publicId.IdUsuario);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            login frm = new login();
            frm.Show();
            this.Hide();
        }

        private void venta_Load(object sender, EventArgs e)
        {

        }
    }
}
