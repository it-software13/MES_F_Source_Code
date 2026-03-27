using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMESSystemTools
{
    public partial class FrmMoreButton : Form
    {
        public DataTable ChildrensTable;

        public DataTable dt;
        public FrmMoreButton()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            LoadXMLData();
        }

        private void LoadXMLData()
        {
            try
            {
                ChildrensTable = new DataTable();
                ChildrensTable.Columns.Add("Title");
                ChildrensTable.Columns.Add("Action");
                ChildrensTable.Columns.Add("Url");
                ChildrensTable.Columns.Add("DllName");
                ChildrensTable.Columns.Add("ClassName");
                ChildrensTable.Columns.Add("Method");
                ChildrensTable.Columns.Add("Parameters");
                dataGridView1.DataSource = ChildrensTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //保存
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                dt = new DataTable("Datas");
                DataColumn dc1 = new DataColumn("Title", typeof(string));
                DataColumn dc2 = new DataColumn("Action", typeof(string));
                DataColumn dc3 = new DataColumn("Url", typeof(string));
                DataColumn dc4 = new DataColumn("DllName", typeof(string));
                DataColumn dc5 = new DataColumn("ClassName", typeof(string));
                DataColumn dc6 = new DataColumn("Method", typeof(string));
                DataColumn dc7= new DataColumn("Parameters", typeof(string));
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                dt.Columns.Add(dc5);
                dt.Columns.Add(dc6);
                dt.Columns.Add(dc7);
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    string[] keys = dataGridView1.Rows[i].Cells["Parameters"].Value.ToString().Split(';');
                    foreach (var item in keys)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            dic.Add(item.Split(':')[0], item.Split(':')[1]);
                        }
                    }
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(dic);
                    DataRow newRow;
                    newRow = dt.NewRow();
                    newRow["Title"] = dataGridView1.Rows[i].Cells["Title"].Value.ToString();
                    newRow["Action"] = dataGridView1.Rows[i].Cells["Action"].Value.ToString();
                    newRow["Url"] = dataGridView1.Rows[i].Cells["Url"].Value.ToString();
                    newRow["DllName"] = dataGridView1.Rows[i].Cells["DllName"].Value.ToString();
                    newRow["ClassName"] = dataGridView1.Rows[i].Cells["ClassName"].Value.ToString();
                    newRow["Method"] = dataGridView1.Rows[i].Cells["Method"].Value.ToString();
                    newRow["Parameters"] = json;
                    dt.Rows.Add(newRow);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入配置参数！");
            }
        }
    }
}
