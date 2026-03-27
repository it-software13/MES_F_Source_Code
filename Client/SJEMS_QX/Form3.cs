using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJEMS_QX
{
    public partial class Form_AssignAuthority : Form
    {
        string str = string.Empty;
        string strSavebtnRole = string.Empty;
        public string sbCheck = string.Empty;
        public string strCheck = string.Empty;
        public Form_AssignAuthority(string str,string SavebtnRole)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.str = str;
            this.strSavebtnRole = SavebtnRole;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //加载CheckBox
            if (!string.IsNullOrEmpty(str))
            {
                string[] content1 = str.Split(',');
                if(content1.Length>0)
                {
                    foreach (var item in content1)
                    {
                        string[] contro = item.Split('|');
                        if(contro.Length>1)
                        {
                            CheckBox cb = new CheckBox();
                            cb.Name = contro[0];
                            cb.Text = contro[1];
                            cb.Margin = new Padding(10, 10, 10, 10);
                            cb.Font = new Font("楷体", 12);//字体
                            flowLayoutPanel1.Controls.Add(cb);
                        }
                    }
                }
            }

            //选中CheckBox
            if(!string.IsNullOrEmpty(this.strSavebtnRole))
            {
                string[] check = this.strSavebtnRole.Split(',');
                if(check.Length>0)
                {
                    foreach (CheckBox cb in flowLayoutPanel1.Controls)
                    {
                        foreach (string name in check)
                        {
                            if(cb.Name.Equals(name))
                            {
                                cb.Checked = true;
                            }
                        }
                    }   
                }
            }
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.WebServiceUrl, Program.Language);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            { 
                foreach (CheckBox item in flowLayoutPanel1.Controls)
                {
                    if (item.Checked == true)
                    {
                        sbCheck += item.Name + ",";
                        strCheck += item.Text + ",";
                    }
                }
                sbCheck = sbCheck.ToString().TrimEnd(',');
                strCheck = strCheck.ToString().TrimEnd(',');
                this.DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
