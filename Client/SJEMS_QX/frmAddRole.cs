using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using SJeMES_Control_Library.Controls;
using SJeMES_Framework.Web;

namespace SJEMS_QX
{
    public partial class Frm_FieldAuthorization : Form
    {
        private string OperationType;
        private string RoleName;
        private string RoleNo;


        public partial class OtherMenu
        {
            public string Title;
            public string Action;
            public string Url;
            public string DllName;
            public string ClassName;
            public string Method;
            public Dictionary<string, string> Parameters = new Dictionary<string, string>();
        }
        private Dictionary<string, OtherMenu> _OtherMenus;
        private SJeMES_Framework.Web.JSONFormClass _ModuleConfig;

        public SJeMES_Framework.Web.JSONFormClass ModuleConfig
        {
            set { _ModuleConfig = value; }
            get { return _ModuleConfig; }
        }
        public Frm_FieldAuthorization()
        {

            InitializeComponent();
            
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            string sql = string.Empty;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.WebServiceUrl, Program.Language);
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                dr.Cells[4].Value = true;
            }
        }

        private void btn_SelectNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                dr.Cells[4].Value = false;

            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {           
            UpdateQX();

            string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功！", Program.Client, Program.WebServiceUrl, Program.Language);
            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);


        }

        /// <summary>
        /// 保存权限
        /// </summary>
        private void UpdateQX()
        {

            //GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, "DELETE FROM SYSROLE02M WHERE Role_Name='" + RoleName + @"'", new Dictionary<string, string>());

            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.Cells[4].Value.ToString().Trim() == "True")
                {
                    string sql = @"
if not Exists(select 1 from SYSPOWER_R where Role_No=@Role_No and AppCode=@AppCode and ColumnName=@ColumnName and TableName=@TableName)
INSERT INTO SYSPOWER_R
(Role_No,AppCode,ColumnName,TableName,ColumnID)
VALUES
(@Role_No,@AppCode,@ColumnName,@TableName,@ColumnID)
";
                    Dictionary<string, string> P = new Dictionary<string, string>();
                    P.Add("Role_No", textBox1.Text.Trim());
                    P.Add("AppCode", textBox2.Text.Trim());
                    P.Add("TableName", dr.Cells[1].Value.ToString());
                    P.Add("ColumnName", dr.Cells[3].Value.ToString());
                    P.Add("ColumnID", dr.Cells[2].Value.ToString());

                    GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);

                }
                else
                {
                    string sql = @"
delete from SYSPOWER_R
where Role_No=@Role_No and AppCode=@AppCode and ColumnName=@ColumnName and TableName=@TableName
";
                    Dictionary<string, string> P = new Dictionary<string, string>();
                    P.Add("Role_No", textBox1.Text.Trim());
                    P.Add("AppCode", textBox2.Text.Trim());
                    P.Add("TableName", dr.Cells[1].Value.ToString());
                    P.Add("ColumnName", dr.Cells[3].Value.ToString());

                    GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);
                }


            }


        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
//                if (checkBox1.Checked)
//                {
//                    string sql = @"select as '模块名称' from SYSROLE03M where menuname not in (select AppName from SYSAPP03M)
//AND Role_No='" + textBox1.Text+"'";

//                    GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.WebServiceUrl, sql, true, true);

//                    frm.ShowDialog();

//                    if (!string.IsNullOrEmpty(frm.ReturnDataXML))
//                    {
//                        textBox2.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<模块名称>", "</模块名称>");
//                        textBox3.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<模块名称>", "</模块名称>");
//                    }
//                }
                //else
                //{
                    string sql = @"SELECT
a.AppCode AS '模块代号',
a.AppName AS '模块名称'
FROM SYSAPP03M a
where a.AppName in(select menuname from SYSROLE03M 
where  Role_No='" + textBox1.Text + "')";

                GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(FrmMenthName, Program.Client, Program.WebServiceUrl,
                                     sql, Program.Language, true, true);

                //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.WebServiceUrl, sql, true, true);

                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(frm.ReturnDataXML))
                    {
                        textBox2.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<模块代号>", "</模块代号>");
                        textBox3.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<模块名称>", "</模块名称>");
                    }

                    Getparent();
                //}
             
            }
            else {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg("请先选择角色！", Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }


        }
        public void GetData()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            string sql = "";
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            string sql = "SELECT Role_No AS '角色代号',Role_Name AS '角色名称' FROM SYSROLE01M";
            GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(FrmMenthName, Program.Client, Program.WebServiceUrl,
                                 sql, Program.Language, true, true);


            //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.WebServiceUrl, "SELECT Role_No AS '角色代号',Role_Name AS '角色名称' FROM SYSROLE01M", true, true);
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(frm.ReturnDataXML))
            {
                textBox1.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<角色代号>", "</角色代号>");
                textBox4.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<角色名称>", "</角色名称>");
            }
        }

        public void Getparent()
        {
            try
            {

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string sql = @"
                            SELECT 
                            a.APP_Code AS '模块代号',
                            a.App_Name AS '模块名称',
                            a.App_Json MApp_Json,
                            b.APP_JSON 
                            FROM SYSAPP01M a
                            LEFT JOIN SYSAPP01A1 b ON a.APP_Code=b.APP_Code
                            where a.APP_Code= 'PC_" + textBox2.Text + "'";
                 
            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            DataTable dt1 = new DataTable();
            DataColumn dc = null;
            dc = dt1.Columns.Add("表头表体", Type.GetType("System.String"));
            dc = dt1.Columns.Add("所属表", Type.GetType("System.String"));

            dc = dt1.Columns.Add("字段代号", Type.GetType("System.String"));
            dc = dt1.Columns.Add("字段名称", Type.GetType("System.String"));
            
            dc = dt1.Columns.Add("查看权限", Type.GetType("System.String"));

            string App_Json = dt.Rows[0]["MApp_Json"].ToString();
            if (!string.IsNullOrEmpty(App_Json))
            {

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("App_Json", App_Json);
                p.Add("OtherMenu", dt);
                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Newtonsoft.Json.JsonConvert.SerializeObject(p));

                sql = "select * from SYSPOWER_R where Role_No='" + textBox1.Text + "' and AppCode='" + textBox2.Text + "'";
                DataTable dtPow = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                     
                Dictionary<string, object> Config = ret;

                ModuleConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<SJeMES_Framework.Web.JSONFormClass>(Config["App_Json"].ToString());

                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Config["OtherMenu"].ToString());
                List<JSONPanelClassHListItem> list = ModuleConfig.PanelHList.tableHead;
                string HeadTable = ModuleConfig.PanelHList.tablename;
                string columnName = "TableName", columnName1 = "ColumnName";
                for (int i = 0; i < list.Count; i++)
                {
                    DataRow newRow;
                    newRow = dt1.NewRow();
                    newRow["表头表体"] = "表头";       
                    newRow["所属表"] = HeadTable;
                    newRow["字段代号"] = list[i].prop;
                    newRow["字段名称"] = list[i].label;
                    DataRow[] dataRows = dtPow.Select(columnName + "='" + HeadTable + "'");
                    DataRow[] dataRows1 = dtPow.Select(columnName1 + "='" + list[i].label + "'");

                    if (dataRows.Length > 0 && dataRows1.Length > 0)
                        newRow["查看权限"] = "True";
                    else
                        newRow["查看权限"] = "false";


                    dt1.Rows.Add(newRow);
                }
                foreach (SJeMES_Framework.Web.JSONPanelClassB panelb in ModuleConfig.PanelB)
                { 
                    List<JSONControlB> ls = panelb.tableHead;
                    for (int i = 0; i < ls.Count; i++)
                    { 
                        DataRow newRow;
                        newRow = dt1.NewRow();
                        newRow["表头表体"] = "表体-" + panelb.Title;
                        newRow["所属表"] = panelb.table;
                        newRow["字段代号"] = ls[i].prop;
                        newRow["字段名称"] = ls[i].label;
                        DataRow[] dataRows = dtPow.Select(columnName + "='" + panelb.table + "'");
                        DataRow[] dataRows1 = dtPow.Select(columnName1 + "='" + ls[i].label + "'");
                        if (dataRows.Length>0 && dataRows1.Length > 0)
                            newRow["查看权限"] = "True";
                        else
                            newRow["查看权限"] = "false";
                        //newRow["查看权限"] = "false";
                        dt1.Rows.Add(newRow);
                    }
                }
                dataGridView1.DataSource = dt1;
                Dictionary<string, OtherMenu> OtherMenus = new Dictionary<string, OtherMenu>();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }

}
