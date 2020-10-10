using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reposteria
{
    public partial class Productos : UserControl
    {
        public Productos()
        {
            InitializeComponent();
        }

        private void gunaLabel2_Click(object sender, EventArgs e)
        {

        }

        #region Properties
        private string _id;
        private string _nombre;
        private string _precio;

        [Category("Custom Props")]
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        [Category("Custom Props")]
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; gunaLabel1.Text = value; }
        }
        [Category("Custom Props")]
        public string precio
        {
            get { return _precio; }
            set { _precio = value; gunaLabel2.Text = value; }
        }
        #endregion

    }
}
