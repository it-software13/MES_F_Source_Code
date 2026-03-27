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

namespace SJEMS_QX
{
    public partial class Frm_MenuMultilingual : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Frm_MenuMultilingual()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
               Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.WebServiceUrl, Program.Language);
        }
        public void GetData()
        {
            string sql = string.Empty;
            if (checkBox1.Checked)
            {
                sql = "select msg as '消息/Message',ui_en as 'English Name',ui_yn AS 'Tên Việt',ui_cn as '中文名称' from SYSLAN05M";
            }
            else
            {
                if(comboBox1.SelectedIndex>=0)
                {
                    if (comboBox1.SelectedValue.Equals("1"))
                    {
                        sql = @"SELECT 
                              menu_name as '菜单名称'
                              ,ui_cn as '中文名称'
                              ,ui_en as 'English Name'
                              ,ui_yn as 'Tên Việt'
                          FROM SYSMENU01M where menu_name not in ('拣货','抛单')";

                    }
                    if (comboBox1.SelectedValue.Equals("2"))
                    {
                        sql = @"SELECT 
                              menu_name as '菜单名称'
                              ,ui_en as 'English Name'
                              ,ui_yn as 'Tên Việt'
                          FROM SYSMENU02M";
                    }
                    if (comboBox1.SelectedValue.Equals("3"))
                    {
                        sql = @"SELECT 
                              menu_name as '菜单名称'
                              ,ui_en as 'English Name'
                              ,ui_yn as 'Tên Việt'
                          FROM SYSMENU03M";
                    }
                } 
            }
           
            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            dataGridView1.DataSource = dt;

            SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, Program.Client, Program.Language, Program.WebServiceUrl, dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            Dictionary<string, string> P = new Dictionary<string, string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {  
                if (checkBox1.Checked)
                {
                    string name_C = dataGridView1.Rows[i].Cells["中文名称"].Value.ToString().Replace("'", "''"); ;
                    string name_E = dataGridView1.Rows[i].Cells["English Name"].Value.ToString().Replace("'", "''"); ;
                    string name_Y = dataGridView1.Rows[i].Cells["Tên Việt"].Value.ToString().Replace("'", "''"); ;
                    sql += "update SYSLAN05M set ui_en=N'" + name_E + "',ui_yn=N'" + name_Y + "',ui_cn=N'"+ name_C + "' " +
                      "where msg=N'" + dataGridView1.Rows[i].Cells["消息/Message"].Value.ToString() + "'";
                }
                else
                {
                    string name_E = dataGridView1.Rows[i].Cells["English Name"].Value.ToString().Replace("'","''") ;
                    string name_Y = dataGridView1.Rows[i].Cells["Tên Việt"].Value.ToString().Replace("'", "''");
                    string menuName = dataGridView1.Rows[i].Cells["菜单名称"].Value.ToString();

                    if (comboBox1.SelectedValue.Equals("1"))
                    {
                        string name_C = dataGridView1.Rows[i].Cells["中文名称"].Value.ToString().Replace("'", "''");
                        sql += "update SYSMENU01M set ui_cn=N'"+ name_C + "',ui_en=N'" + name_E + "',ui_yn=N'" + name_Y + "' where menu_name='" + menuName + "'; ";
                    }
                    else if (comboBox1.SelectedValue.Equals("2"))
                    {
                        sql += "update SYSMENU02M set ui_en=N'" + name_E + "',ui_yn=N'" + name_Y + "' where menu_name='" + menuName + "'; ";
                    }
                    else if (comboBox1.SelectedValue.Equals("3"))
                    {
                        sql += "update SYSMENU03M set ui_en=N'" + name_E + "',ui_yn=N'" + name_Y + "' where menu_name='" + menuName + "'; ";
                    }
                }
            }
              
            GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);

            string msg=SJeMES_Framework.Common.UIHelper.UImsg("保存成功！",Program.Client, Program.WebServiceUrl, Program.Language);
            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);

            GetData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox1.Enabled = false;
                string sql = "select msg as '消息/Message',ui_cn as '中文名称',ui_en as 'English Name',ui_yn AS 'Tên Việt' from SYSLAN05M";
                DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["消息/Message"].DisplayIndex = 0;
                dataGridView1.Columns["中文名称"].DisplayIndex = 1;
                dataGridView1.Columns["English Name"].DisplayIndex = 2;
                dataGridView1.Columns["Tên Việt"].DisplayIndex = 3;
            }
            else
            {
                comboBox1.Enabled = true;
                GetData();
            }
        }
         
        public void SetComboBox()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("code");
            dt.Columns.Add("value");
            DataRow dr = dt.NewRow();

            switch (Program.Language.ToLower())
            {
                case "cn":
                    {
                        dr = dt.NewRow();
                        dr["code"] = "1";
                        dr["value"] = "一级菜单";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "2";
                        dr["value"] = "二级菜单";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "3";
                        dr["value"] = "三级菜单";
                        dt.Rows.Add(dr);
                    }
                    break;
                case "en":
                    {
                        dr = dt.NewRow();
                        dr["code"] = "1";
                        dr["value"] = "First Level Menu";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "2";
                        dr["value"] = "Second Level Menu";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "3";
                        dr["value"] = "Three Level Menu";
                        dt.Rows.Add(dr);
                    };
                    break;
                case "yn":
                case "hk":
                    {
                        dr = dt.NewRow();
                        dr["code"] = "1";
                        dr["value"] = "Trình đơn hạng nhất";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "2";
                        dr["value"] = "Trình đơn hai cấp";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "3";
                        dr["value"] = "Ba cấp";
                        dt.Rows.Add(dr);
                    };
                    break;
            }

            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "code";
            comboBox1.DisplayMember = "value";
            comboBox1.SelectedIndex = -1;
        }

        private void Frm菜单多语言_Load(object sender, EventArgs e)
        {
            SetComboBox();

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        { 
            if (comboBox1.DataSource != null)
                GetData();
        }
    }
}
