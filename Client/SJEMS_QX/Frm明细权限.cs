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
    public partial class Frm_DetailedJurisdiction : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Frm_DetailedJurisdiction()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
              Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.WebServiceUrl, Program.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData();
        }
       
        public void GetData()
        {
            //if (!string.IsNullOrEmpty(textBox1.Text))
            //{

            //}
            //else
            //{
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
                string sql = "select Role_No as '角色',Title as '模块名称',btnRole as '权限' from SYSROLE04M where 1=1 ";
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                sql += " and Title='"+textBox1.Text+"'";
            }
            //if (!string.IsNullOrEmpty(textBox2.Text))
            //{
            //    sql += " and Role_No='" + textBox2.Text + "'";
            //}
            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            if (dt.Rows.Count>0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    DataTable dt1 = new DataTable();
                    DataColumn dc = null;
                    dc = dt1.Columns.Add("角色", Type.GetType("System.String"));
                    dc = dt1.Columns.Add("模块名称", Type.GetType("System.String"));

                    dc = dt1.Columns.Add("权限", Type.GetType("System.String"));
                    DataRow newRow;
                    newRow = dt1.NewRow();
                    //newRow["角色"] = textBox2.Text;
                    newRow["模块名称"] = textBox1.Text;
                    newRow["权限"] = "";
                    dt1.Rows.Add(newRow);
                    dataGridView1.DataSource = dt1;
                }  
            }
           
            //}
        }
      
        //模块
        private void textBox1_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            string sql = "SELECT menu_parent as '二级菜单' ,menu_name as '三级菜单' FROM SYSMENU03M";
            GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(FrmMenthName, Program.Client, Program.WebServiceUrl,
               sql, Program.Language, true, true);


            //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm =
            //new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.WebServiceUrl, 
            //"SELECT menu_parent as '二级菜单' ,menu_name as '三级菜单' FROM SYSMENU03M", true, true);
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(frm.ReturnDataXML))
            {
                textBox1.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<三级菜单>", "</三级菜单>");
                GetData();
            }
        }
        //保存
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Empty;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    sql += @"
                if not Exists(select 1 from SYSROLE04M where Title='" + textBox1.Text + @"')
                INSERT  INTO SYSROLE04M
                (Title,btnRole)
                VALUES
                ('" + textBox1.Text + @"','" + dataGridView1.Rows[i].Cells[2].Value + @"')
                ELSE
                UPDATE SYSROLE04M
                SET btnRole='" + dataGridView1.Rows[i].Cells[2].Value + @"'
                WHERE Title='" + textBox1.Text + @"'
                ";
                }

                GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, new Dictionary<string, string>());

                string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功！", Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg); 
                
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
