using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES
{
    public partial class Frm_EmployeeInformation : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        //GDSJ_Framework.DBHelper.DataBase DB = new GDSJ_Framework.DBHelper.DataBase("oracle", Program.Org.DBServer, Program.Org.DBName, Program.Org.DBUser, Program.Org.DBPassword, string.Empty);
        List<ComboBox> listBoxDay;
        public Frm_EmployeeInformation()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
                Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client,"", Program.Client.Language);
           
          
        }
        public void getcom(params ComboBox[] comboBoxes)
        {
            //string sql = "select top 5 staff_name from HR001M";
            //DataTable dt = Program.Client.GetDT(sql);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    comboBox1.Items.Add(dt.Rows[i]["staff_name"].ToString());
            //}
            //SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, new DataGridView(), comboBox1, Program.Client, Program.Client.Language);
        }
        public void getcom2()
        {
            //string sql = "select top 10 staff_name from HR001M";
            //DataTable dt = Program.Client.GetDT(sql);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    comboBox2.Items.Add(dt.Rows[i]["staff_name"].ToString());
            //}
            //SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, new DataGridView(), comboBox1, Program.Client, Program.Client.Language);
        }
        public void GetData()
        {
            string sql = @"select staff_no as 工号,user_code AS 账号,staff_name as 姓名,staff_sex as 性别,staff_id as 身份证,
                             UDF02 角色,UDF03 as 用户权限,staff_province as 省份,staff_city as 城市,staff_address as 地址,staff_phone as 联系电话
                            ,staff_mobile as 移动电话,staff_qq as QQ号码,staff_email as 电子邮箱,staff_department as 部门编号,
                            staff_post as 职位,staff_status as 状态 from HR001M";
            //string sql = @"select staff_no from HR001M";
            DataTable dt = Program.Client.GetDT(sql);
            dataGridView1.DataSource = dt;
            //SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name,Program.Client,Program.Client.Language,dataGridView1);
        }
        //查询
        private void button4_Click(object sender, EventArgs e)
        {
            //edit by yj 20200314 add column staff_role_name
            string sql = @"select staff_no as 工号,user_code AS 账号,staff_name as 姓名,staff_sex as 性别,staff_id as 身份证,
                        staff_province as 省份,staff_city as 城市,staff_address as 地址,staff_phone as 联系电话
                        ,staff_mobile as 移动电话,staff_qq as QQ号码,staff_email as 电子邮箱,staff_department as 部门编号,
                        staff_post as 职位,staff_status as 状态,UDF02 角色 from HR001M where 1=1 ";
            //end edit
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                sql += " and (staff_no like '%"+ textBox1.Text + "%' or staff_name like '%"+ textBox1.Text + "%' or staff_department like '%"+ textBox1.Text + "%') ";
            }
            DataTable dt = Program.Client.GetDT(sql);
            //DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.Org, Program.WebServiceUrl, sql, new Dictionary<string, string>());
            dataGridView1.DataSource = dt;
            //SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, dataGridView1, new ComboBox(), Program.Client, Program.Client.Language);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                int index= dataGridView1.CurrentRow.Index;
            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            if (!string.IsNullOrEmpty(id))
            {
                DialogResult dr;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("删除用户会一并删除账号信息，确定删除吗？", 
                    Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                string msg2 = SJeMES_Framework.Common.UIHelper.UImsg("提示", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                dr = MessageBox.Show(msg, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    string UserCode = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    string sql = "delete from HR001M where staff_no='" + id + "'";
                    Program.Client.ExecuteNonQuery(sql);
                    //GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.Org, Program.WebServiceUrl, sql, new Dictionary<string, string>());
                    sql = "DELETE FROM SYSUSER01M WHERE UserCode='" + UserCode + "'";
                    Program.Client.SYSExecuteNonQuery(sql);
                    //GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                    //GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.Org, Program.WebServiceUrl, sql, new Dictionary<string, string>());
                    string msg3= SJeMES_Framework.Common.UIHelper.UImsg("删除成功！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    MessageBox.Show(msg3);
                    GetData();
                }
              
            }
            else
            {
                string msg4 = SJeMES_Framework.Common.UIHelper.UImsg("请选择需要删除的数据！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                MessageBox.Show(msg4);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_AddEmployee frm = new Frm_AddEmployee("");
            //this.TopMost = true; //最上层
            frm.StartPosition = FormStartPosition.CenterParent;
            
            frm.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            if (!string.IsNullOrEmpty(id))
            {
                Frm_AddEmployee frm = new Frm_AddEmployee(id);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            else
            {
                string msg4 = SJeMES_Framework.Common.UIHelper.UImsg("请选择需要修改的数据！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                MessageBox.Show(msg4);
            }
        }

        private void Frm员工资料_Load(object sender, EventArgs e)
        {
            GetData();

            SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, Program.Client, Program.Client.Language, Program.Client.WebServiceUrl, dataGridView1);

        }
    }
}
