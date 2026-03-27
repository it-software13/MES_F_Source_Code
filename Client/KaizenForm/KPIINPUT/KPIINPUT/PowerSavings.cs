using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPIINPUT
{
    public partial class PowerSavings : Form
    {
        public PowerSavings()
        {
            InitializeComponent();
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            long value11 = long.TryParse(C.Text, out var result11) ? result11 : 0;
            long value12 = long.TryParse(E.Text, out var result12) ? result11 : 0;
            if (!string.IsNullOrEmpty(C.Text) && !string.IsNullOrEmpty(E.Text))
            {
                F.Text = (value11 / value12).ToString();
            }
            if (E.Text == "")
            {
                F.Text = "";
               
            }

        }

        private void D_TextChanged(object sender, EventArgs e)
        {
            long value11 = long.TryParse(B.Text, out var result11) ? result11 : 0;
            long value12 = long.TryParse(D.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(B.Text) && !string.IsNullOrEmpty(D.Text))
            {

                E.Text = ((3600 / value11) * value12).ToString();
            }
            if (D.Text == "")
            {
                E.Text = "";
            }
        }

        private void F_TextChanged(object sender, EventArgs e)
        {
            long value11 = long.TryParse(F.Text, out var result11) ? result11 : 0;
            long value12 = long.TryParse(A.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(F.Text) && !string.IsNullOrEmpty(A.Text))
            {

                G.Text = (value11 * value12).ToString();
            }
            if (F.Text == "")
            {
                G.Text = "";
            }
        }

        private void H_TextChanged(object sender, EventArgs e)
        {
            long value11 = long.TryParse(G.Text, out var result11) ? result11 : 0;
            long value12 = long.TryParse(H.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(G.Text) && !string.IsNullOrEmpty(H.Text))
            {
              Total_Cost.Text = (value11 * value12).ToString();
            }
            if (H.Text == "")
            {
                Total_Cost.Text = "";
            }

        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }

        private void C_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void B_TextChanged(object sender, EventArgs e)
        {
            long value11 = long.TryParse(B.Text, out var result11) ? result11 : 0;
            long value12 = long.TryParse(D.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(B.Text) && !string.IsNullOrEmpty(D.Text))
            {

                E.Text = ((3600 / value11) * value12).ToString();
            }
            if (B.Text == "")
            {
                E.Text = "";
            }
        }

        private void G_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
