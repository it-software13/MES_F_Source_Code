using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class FileCheckStatus : UserControl
    {
        F_QCM_ART_File_Detail _pfrom;
        string _type = "";
        public FileCheckStatus(F_QCM_ART_File_Detail pfrom,string type)
        {
            InitializeComponent();
            _pfrom = pfrom;
            _type = type;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked)
            {
                _pfrom.check_result[_type]["WHD"] = true;
            }
            else
            {
                _pfrom.check_result[_type]["WHD"] = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                _pfrom.check_result[_type]["YHD"] = true;
            }
            else
            {
                _pfrom.check_result[_type]["YHD"] = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                _pfrom.check_result[_type]["QRQM"] = true;
            }
            else
            {
                _pfrom.check_result[_type]["QRQM"] = false;
            }
        }
    }
}
