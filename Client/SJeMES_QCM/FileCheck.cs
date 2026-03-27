using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Control_Library.Forms;
using SJeMES_Control_Library;

namespace SJeMES_QCM
{
    public partial class FileCheck : UserControl
    {
        public FileCheck()
        {
            InitializeComponent();
        }

        private void FileCheck_Load(object sender, EventArgs e)
        {
          
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lab_url.Text))
            {
                string FILE_URL = Program.Client.PicUrl + lab_url.Text;
                ShowFileHelper.ShowFile(FILE_URL);
            }
        }
    }
}
