using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace reposteria
{
    public partial class Categorias : UserControl
    {
        public Categorias()
        {
            InitializeComponent();
        }
        #region Properties
        private string _id;
        private string _nombre;
        private string _image;

        [Category("Custom Props")]
        public string id
        {
            get { return _id; }
            set { _id = value;}
        }
        [Category("Custom Props")]
        public string nombre { 
            get { return _nombre; }
            set { _nombre = value; lblNombre.Text =  value; }
        }
        [Category("Custom Props")]
        public string image
        {
            get { return _image; }
            set { _image = value; gunaPictureBox1.LoadAsync($@"{_image}"); }
        }





        #endregion

        private void Categorias_Click(object sender, EventArgs e)
        {
            venta frm = new venta();
            //frm.getIdCategoria(id);
        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            venta vd = new venta();
            vd.getIdCategoria(id);
            
        }

        public event EventHandler My_Click;
    }
}
