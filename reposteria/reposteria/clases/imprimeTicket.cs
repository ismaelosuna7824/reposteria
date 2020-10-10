using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;
using LibPrintTicket;
using reposteria.Properties;

namespace reposteria.clases
{
    class imprimeTicket
    {
        public void imprimirTicket(GunaDataGridView grid, string folio, string empleado, string comentarios, double total, double cambio, double efectivo)
        {
      
            Ticket tickets = new Ticket();
            //Image img = Resources.bakery_logo_cake_confectionery_frosting_icing_cake_png_clip_art;

            //tickets.HeaderImage = img;
            tickets.AddHeaderLine("Reposteria");
            tickets.AddHeaderLine("EXPEDIDO POR:");
            tickets.AddHeaderLine(empleado);
            tickets.AddHeaderLine("RFC: MARL-730815-A83");
            tickets.AddHeaderLine("Av Ceylan #1133 Col. Industrial VallejoSDAJSKDSLKKDSHKDHSASAJDSJHADKJSDSKJADHDSJSKJDSHADHAKJD CP 02300 Azcapotzalco Mexico DF");
            tickets.AddSubHeaderLine("folio # 1");
            tickets.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            //NECESITAMOS CONFIGURARLO AQUI PARA Q SOLO IMPRIMA LA FECHA DE LA FACTURA
            //tickets.AddItem("1", "Nombre producto que vamos a vender esta largo", "Total");
            double tots = 0;
            foreach (DataGridViewRow fila in grid.Rows)
            {
                

                tickets.AddItem(fila.Cells[2].Value.ToString(), fila.Cells[1].Value.ToString(), fila.Cells[4].Value.ToString());
                //ticket.lineasIgual();
                //ticket.AgregaArticulo(fila.Cells[0].Value.ToString(), int.Parse(fila.Cells[3].Value.ToString()),double.Parse(fila) );
                //ticket.lineasIgual();
                tots = tots + double.Parse(fila.Cells[4].Value.ToString());

            }
            tickets.AddTotal("SUBTOTAL", tots.ToString());

            tickets.AddTotal("TOTAL", total.ToString());
            tickets.AddTotal("", "");
            tickets.AddTotal("RECIBIDO", efectivo.ToString());
            tickets.AddTotal("CAMBIO", cambio.ToString());
            tickets.AddTotal("", "");
            tickets.AddFooterLine(comentarios);
            tickets.AddFooterLine("");
            //tickets.AddFooterLine("No nos hacemnos responsables por los valores u onjetos olvidados, en las prendas, CONSERVE ESTA NOTA, ES NECESARIA PARA  RECOJER LA ROPA");
            tickets.AddFooterLine("");
            tickets.AddFooterLine("¡¡¡GRACIAS POR SU PREFERENCIA!!!");

            tickets.PrintTicket("HP53D614"); //Nombre de la impresora de tickets
        }

    }
}
