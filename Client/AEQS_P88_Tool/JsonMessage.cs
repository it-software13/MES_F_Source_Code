using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace AEQS_P88_Tool
{
    public partial class JsonMessage : MaterialForm
    {
        string msg = string.Empty;
        public JsonMessage(string Json)
        {
            InitializeComponent();
            msg = Json;
        } 
       private void JsonMessage_Load(object sender, EventArgs e)
        {
            richTextBox1.SelectionLength = 0;//设置不选中
            richTextBox1.Text = msg;
            
        }
    }
}
