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
            venta frm = new venta();
            frm.Show();
            this.Hide();
        }
    }
}
