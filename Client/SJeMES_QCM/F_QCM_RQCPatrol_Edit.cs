using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_RQCPatrol_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_RQCPatrol_Edit()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public class Info
        {
            public string Id { get; set; }
            public string Name { get; set; }

        }

        private DataTable h=new DataTable();

        public DataTable ha
        {
            get { return h; }
            set { h = value; }
        }

        private void F_QCM_RQCPatrol_Edit_Load(object sender, EventArgs e)
        {
            #region 下拉框数据绑定
            IList<Info> infoList = new List<Info>();
            Info info1 = new Info() { Id = "1", Name = "a" };
            Info info2 = new Info() { Id = "2", Name = "b" };
            Info info3 = new Info() { Id = "3", Name = "c" };
            infoList.Add(info1);
            infoList.Add(info2);
            infoList.Add(info3);
            comboBox1.DataSource = infoList;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";
            #endregion
        }

        public static string GetRandomString(int iLength)
        {
            string buffer = "0123456789";// 随机字符中也可以为汉字（任何）
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            int range = buffer.Length;
            for (int i = 0; i < iLength; i++)
            {
                sb.Append(buffer.Substring(r.Next(range), 1));
            }
            return sb.ToString();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            h.Columns.Add("vendor", typeof(object));
            h.Columns.Add("inspection_no", typeof(object));
            h.Columns.Add("inspection_type", typeof(object));
            h.Columns.Add("date", typeof(object));
            h.Columns.Add("region", typeof(object));
            h.Columns.Add("Productionline", typeof(object));
            h.Columns.Add("machine", typeof(object));
            h.Columns.Add("timequantum", typeof(object));
            h.Columns.Add("order", typeof(object));
            h.Columns.Add("Codenumber", typeof(object));
            h.Columns.Add("art", typeof(object));
            h.Columns.Add("shoes", typeof(object));
            h.Columns.Add("parts", typeof(object));
            h.Rows.Add();
            h.Rows[0]["vendor"] = textBox1.Text; 
            h.Rows[0]["inspection_no"] = GetRandomString(7);
            h.Rows[0]["inspection_type"] = comboBox1.SelectedValue;
            h.Rows[0]["date"] = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            h.Rows[0]["region"] = textBox3.Text;
            h.Rows[0]["Productionline"] = textBox4.Text;
            h.Rows[0]["machine"] = textBox5.Text;
            h.Rows[0]["timequantum"] = textBox8.Text;
            h.Rows[0]["order"] = textBox7.Text;
            h.Rows[0]["Codenumber"] = textBox6.Text;
            h.Rows[0]["art"] = textBox11.Text;
            h.Rows[0]["shoes"] = textBox10.Text;
            h.Rows[0]["parts"] = textBox9.Text;
            this.Close();
        }


        private void btnclear_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
