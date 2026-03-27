using SJeMES_Framework.Web;
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
    public partial class Frm_FieldMultilingual : Form
    {
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

        public Frm_FieldMultilingual()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.WebServiceUrl, Program.Language);
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            //if (checkBox1.Checked) UpdateButtonQX();
            //else UpdateQX();
            //if (!checkBox1.Checked)UpdateQX();


            string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully！", Program.Client, Program.WebServiceUrl, Program.Language);
            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);

            //this.Close();

        }

        /// <summary>
        /// 保存按钮多语言设置
        /// </summary>
        private void UpdateButtonQX()
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (!string.IsNullOrEmpty(dr.Cells[3].Value.ToString().Trim()) || !string.IsNullOrEmpty(dr.Cells[4].Value.ToString().Trim()))
                {
                    string sql = @"
UPDATE SJQDMS_UILAN set ui_en=@ui_en,ui_yn=@ui_yn
WHERE ui_code=@ui_code and ui_tittle=@ui_tittle and ui_cn=@ui_cn
";
                    Dictionary<string, string> P = new Dictionary<string, string>();
                    P.Add("ui_tittle", dr.Cells[0].Value.ToString());
                    P.Add("ui_code", dr.Cells[1].Value.ToString());
                    P.Add("ui_en", dr.Cells[3].Value.ToString());
                    P.Add("ui_yn", dr.Cells[4].Value.ToString());
                    P.Add("ui_cn", dr.Cells[2].Value.ToString());

                    GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);

                }
                else
                {
                    string sql = @"
delete from SYSLANGUAGE
where AppCode=@AppCode and ColumnName=@ColumnName and TableName=@TableName
";
                    Dictionary<string, string> P = new Dictionary<string, string>();
                    P.Add("AppCode", textBox2.Text.Trim());
                    P.Add("TableName", dr.Cells[1].Value.ToString());
                    P.Add("ColumnName", dr.Cells[3].Value.ToString());

                    GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);
                }


            }


        }

        /// <summary>
        /// 保存字段多语言设置
        /// </summary>
        private void UpdateQX()
        {


            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (!string.IsNullOrEmpty(dr.Cells[4].Value.ToString().Trim())|| !string.IsNullOrEmpty(dr.Cells[5].Value.ToString().Trim()))
                {
                    string sql = @"
if not Exists(select 1 from SYSLANGUAGE where AppCode=@AppCode and ColumnName=@ColumnName and TableName=@TableName)
INSERT INTO SYSLANGUAGE
(AppCode,ColumnName,TableName,ColumnID,ColumnName_EN,ColumnName_HK)
VALUES
(@AppCode,@ColumnName,@TableName,@ColumnID,@ColumnName_EN,@ColumnName_HK)
else 
update SYSLANGUAGE set ColumnName_EN=@ColumnName_EN,ColumnName_HK=@ColumnName_HK
where AppCode=@AppCode and ColumnName=@ColumnName and TableName=@TableName
";
                    Dictionary<string, string> P = new Dictionary<string, string>();
                    P.Add("AppCode", textBox2.Text.Trim());
                    P.Add("TableName", dr.Cells[1].Value.ToString());
                    P.Add("ColumnName", dr.Cells[3].Value.ToString());
                    P.Add("ColumnID", dr.Cells[2].Value.ToString());
                    P.Add("ColumnName_EN", dr.Cells[4].Value.ToString());
                    P.Add("ColumnName_HK", dr.Cells[5].Value.ToString());

                    GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);

                }
                else
                {
                    string sql = @"
delete from SYSLANGUAGE
where AppCode=@AppCode and ColumnName=@ColumnName and TableName=@TableName
";
                    Dictionary<string, string> P = new Dictionary<string, string>();
                    P.Add("AppCode", textBox2.Text.Trim());
                    P.Add("TableName", dr.Cells[1].Value.ToString());
                    P.Add("ColumnName", dr.Cells[3].Value.ToString());

                    GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);
                }


            }


        }

        public void Getparent(string sqlwhere="",string strText="")
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //Commented by Ashok on 2025/03/28
            //            string sql = @"
            //SELECT 
            //a.APP_Code AS '模块代号',
            //a.App_Name AS '模块名称',
            //a.App_Json MApp_Json,
            //b.APP_JSON 
            //FROM SYSAPP01M a
            //LEFT JOIN SYSAPP01A1 b ON a.APP_Code=b.APP_Code
            //where a.APP_Code= '" + textBox2.Text + "'";

            string sql = @"
SELECT 
a.APP_Code AS 'Module_Code',
a.App_Name AS 'Module_Name',
a.App_Json MApp_Json,
b.APP_JSON 
FROM SYSAPP01M a
LEFT JOIN SYSAPP01A1 b ON a.APP_Code=b.APP_Code
where a.APP_Code= '" + textBox2.Text + "'";

            //DataTable dt = Program.SYSDB.GetDataTable(sql);
            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            DataTable dt1 = new DataTable();
            DataColumn dc = null;
            //Commented by Ashok on 2025/03/28
            //dc = dt1.Columns.Add("表头表体", Type.GetType("System.String"));
            //dc = dt1.Columns.Add("所属表", Type.GetType("System.String"));

            //dc = dt1.Columns.Add("字段代号", Type.GetType("System.String"));
            //dc = dt1.Columns.Add("字段名称", Type.GetType("System.String"));

            //dc = dt1.Columns.Add("英语名称", Type.GetType("System.String"));
            //dc = dt1.Columns.Add("越语名称", Type.GetType("System.String"));


            dc = dt1.Columns.Add("Header And Body", Type.GetType("System.String"));
            dc = dt1.Columns.Add("Affiliation Table", Type.GetType("System.String"));

            dc = dt1.Columns.Add("Field_Code", Type.GetType("System.String"));
            dc = dt1.Columns.Add("Field_Name", Type.GetType("System.String"));

            dc = dt1.Columns.Add("English_Name", Type.GetType("System.String"));
            dc = dt1.Columns.Add("Vietnamese_Name", Type.GetType("System.String"));


            //dc = dt1.Columns.Add("查看权限", Type.GetType("System.String"));

            string App_Json = "";
            if(dt.Rows.Count>0)
                App_Json = dt.Rows[0]["MApp_Json"].ToString();
            if (!string.IsNullOrEmpty(App_Json))
            {

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("App_Json", App_Json);
                p.Add("OtherMenu", dt);
                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Newtonsoft.Json.JsonConvert.SerializeObject(p));

                sql = "select * from SYSLANGUAGE where 1=1 ";
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    sql += " and AppCode='" + textBox2.Text + "'";
                }
                if (!string.IsNullOrEmpty(sqlwhere))
                {
                    sql += sqlwhere;
                } 
                DataTable dtPow = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());

                Dictionary<string, object> Config = ret;

                ModuleConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<SJeMES_Framework.Web.JSONFormClass>(Config["App_Json"].ToString());

                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Config["OtherMenu"].ToString());
                //MModuleConfig.PanelH.formData;
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
                    DataRow[] dataRows1 = dtPow.Select(columnName + "='" + HeadTable + "' and " + columnName1 + "='" + list[i].label + "'");

                    if (dataRows.Length > 0 && dataRows1.Length > 0)
                    {
                        newRow["英语名称"] = dataRows1[0]["ColumnName_EN"].ToString();
                        newRow["越语名称"] = dataRows1[0]["ColumnName_HK"].ToString();
                    }
                    else
                    {
                        newRow["英语名称"] = "";
                        newRow["越语名称"] = "";
                    }
                    dt1.Rows.Add(newRow);
                }
                foreach (SJeMES_Framework.Web.JSONPanelClassB panelb in ModuleConfig.PanelB)
                {

                    List<JSONControlB> ls = panelb.tableHead;
                    //if (1 == 1)
                    //{
                    //    DataRow newRow;
                    //    newRow = dt1.NewRow();
                    //    newRow["表头表体"] = "表体-" + panelb.Title;
                    //    newRow["所属表"] = panelb.table;
                    //    newRow["字段代号"] = panelb.Title;
                    //    newRow["字段名称"] = panelb.Title;
                    //    DataRow[] dataRows = dtPow.Select(columnName + "='" + panelb.table + "'");
                    //    DataRow[] dataRows1 = dtPow.Select(columnName1 + "='" + panelb.Title + "'");
                    //    if (dataRows.Length > 0 && dataRows1.Length > 0)
                    //    {
                    //        newRow["英语名称"] = dataRows1[0]["ColumnName_EN"].ToString();
                    //        newRow["越语名称"] = dataRows1[0]["ColumnName_HK"].ToString();
                    //    }
                    //    else
                    //    {
                    //        newRow["英语名称"] = " ";
                    //        newRow["越语名称"] = " ";
                    //    }
                    //    dt1.Rows.Add(newRow);
                    //}
                    for (int i = 0; i < ls.Count; i++)
                    {
                        DataRow newRow;
                        newRow = dt1.NewRow();
                        newRow["表头表体"] = "表体-" + panelb.Title;
                        newRow["所属表"] = panelb.table;
                        newRow["字段代号"] = ls[i].prop;
                        newRow["字段名称"] = ls[i].label;
                        DataRow[] dataRows = dtPow.Select(columnName + "='" + panelb.table + "'");
                        DataRow[] dataRows1 = dtPow.Select(columnName + "='" + panelb.table + "' and " +columnName1 + "='" + ls[i].label + "'");
                        if (dataRows.Length > 0 && dataRows1.Length > 0)
                        {
                            newRow["英语名称"] = dataRows1[0]["ColumnName_EN"].ToString();
                            newRow["越语名称"] = dataRows1[0]["ColumnName_HK"].ToString();
                        }
                        else
                        {
                            newRow["英语名称"] = "";
                            newRow["越语名称"] = "";
                        }
                        dt1.Rows.Add(newRow);
                    }
                }

                DataTable dtNew = dt1.Clone();
                if (!string.IsNullOrEmpty(strText))
                {
                    DataRow[] drs = dt1.Select("表头表体 like '%" + strText + "%' or 所属表 like '%" + strText + "%' or 字段代号 like '%" + strText + "%'" +
                        " or 字段名称 like '%" + strText + "%' or 英语名称 like '%" + strText + "%' or 越语名称 like '%" + strText + "%'");
                    for (int i = 0; i < drs.Length; i++)
                    {
                        dtNew.ImportRow(drs[i]);
                    }
                }
                if (dtNew != null && dtNew.Rows.Count > 0)
                    dataGridView1.DataSource = dtNew;
                else
                    dataGridView1.DataSource = dt1;
            }

            SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, Program.Client, Program.Language, Program.WebServiceUrl, dataGridView1);

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            if (checkBox1.Checked)
            {
                FrmMenthName = FrmMenthName + "1";
                //commented by Ashok on 2025/03/28
                //                string sql = @"SELECT distinct
                //a.ui_tittle AS '菜单名称'
                //FROM SJQDMS_UILAN a";// and UserCode= '" + textBox1.Text + "'
                string sql = @"SELECT distinct
a.ui_tittle AS 'Menu_Name'
FROM SJQDMS_UILAN a";// and UserCode= '" + textBox1.Text + "'

                GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(FrmMenthName, Program.Client, Program.WebServiceUrl,
                     sql, Program.Language, true, true);

                //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.WebServiceUrl, sql, true, true);

                frm.ShowDialog();

                if (!string.IsNullOrEmpty(frm.ReturnDataXML))
                {
                    textBox2.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<菜单名称>", "</菜单名称>");
                }

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //commented by Ashok on 2025/03/28
                //               sql = @"
                //SELECT 
                //ui_tittle AS '功能名称',
                //ui_code AS '控件ID',
                //ui_id AS '控件名称',
                //ui_cn AS '中文名称',
                //ui_en AS '英语名称',
                //ui_yn AS '越语名称'
                //FROM SJQDMS_UILAN
                //where ui_tittle='" + textBox2.Text + "'" +
                //" order by ui_tittle,ui_code,ui_id ";

                sql = @"
SELECT 
ui_tittle AS 'Function_Name',
ui_code AS 'Control_ID',
ui_id AS 'Control_Name',
ui_cn AS 'Chinese_Name',
ui_en AS 'English_Name',
ui_yn AS 'Vietnamese_Name'
FROM SJQDMS_UILAN
where ui_tittle='" + textBox2.Text + "'" +
" order by ui_tittle,ui_code,ui_id ";
                //DataTable dt = Program.SYSDB.GetDataTable(sql);
                DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());

                dataGridView1.DataSource = dt;
                SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, Program.Client, Program.Language, Program.WebServiceUrl, dataGridView1);
            }
            else
            {
                FrmMenthName = FrmMenthName + "2";
                //commented by Ashok on 2025/03/28
                //                string sql = @"
                //SELECT distinct
                //a.APP_Code AS '模块代号',
                //a.App_Name AS '模块名称'
                //FROM SYSAPP01M a
                //where 1=1";// and UserCode= '" + textBox1.Text + "'

                string sql = @"
SELECT distinct
a.APP_Code AS 'Module_Code',
a.App_Name AS 'Module_Name'
FROM SYSAPP01M a
where 1=1";// and UserCode= '" + textBox1.Text + "'

                GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(FrmMenthName, Program.Client, Program.WebServiceUrl,
                 sql, Program.Language, true, true);

                //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.WebServiceUrl, sql, true, true);

                frm.ShowDialog();

                if (!string.IsNullOrEmpty(frm.ReturnDataXML))
                {
                    //textBox2.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<模块代号>", "</模块代号>");
                    //textBox3.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<模块名称>", "</模块名称>");
                    textBox2.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<Module_Code>", "</Module_Code>");
                    textBox3.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<Module_Name>", "</Module_Name>");
                }

                Getparent();

            }

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex>-1 && e.ColumnIndex>-1)
            {
                if (checkBox1.Checked)
                {
                    if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex==5)
                    {
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                    }
                }
                else
                {
                    if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                    {
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                    }
                }
            }
      
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //label2.Text = "菜单名称";
                textBox2.Text = "";
                textBox3.Text = "";
                label3.Visible = false;
                textBox3.Visible = false;
                //textBox2.Enabled = false;
                //textBox3.Text = "";

                //dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                 
                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //commented by Ashok on 2025/03/28
                //                string sql = @"
                //SELECT 
                //ui_tittle AS '功能名称',
                //ui_code AS '控件ID',
                //ui_id as '控件名称',
                //ui_cn AS '中文名称',
                //ui_en AS '英语名称',
                //ui_yn AS '越语名称'
                //FROM SJQDMS_UILAN
                // order by ui_tittle,ui_code,ui_id"; 

                string sql = @"
SELECT 
ui_tittle AS 'Function_Name',
ui_code AS 'Control_ID',
ui_id as 'Control_Name',
ui_cn AS 'Chinese_Name',
ui_en AS 'English_Name',
ui_yn AS 'Vietnamese_Name'
FROM SJQDMS_UILAN
 order by ui_tittle,ui_code,ui_id";

                //DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());

                //dataGridView1.DataSource = dt;

            }
            else
            {
                textBox2.Enabled = true;
                //label2.Text = "模块代码";
                textBox2.Text = "";
                label3.Visible = true;
                textBox3.Visible = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = null; 

            }
            SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, Program.Client, Program.Language, Program.WebServiceUrl, dataGridView1);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {
                    if (!string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim()))
                    {
                        Dictionary<string, string> P = new Dictionary<string, string>();
                        //P.Add("ui_tittle", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        //P.Add("ui_code", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                        //P.Add("ui_en", dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                        //P.Add("ui_yn", dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                        //P.Add("ui_cn", dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                        //P.Add("ui_id", dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());

                        string ui_tittle = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        string ui_code = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string ui_en = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                        string ui_yn = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                        string ui_cn = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        string ui_id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                        string sql = @"
                                    UPDATE SJQDMS_UILAN set ui_en=N'{3}',ui_yn=N'{4}',ui_cn=N'{5}'
                                    WHERE ui_code='{0}' and ui_tittle='{1}' AND ui_id='{2}'
                                    ";
                        sql = string.Format(sql, ui_code, ui_tittle, ui_id, ui_en, ui_yn, ui_cn);
                        GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);

                    } 
                }
                
                }
            else
            {
                if (e.ColumnIndex == 5 || e.ColumnIndex == 4)
                {
                    if (!string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Trim())
                        || !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString().Trim()))
                    {
                        Dictionary<string, string> P = new Dictionary<string, string>();
                        //P.Add("AppCode", textBox2.Text.Trim());
                        //P.Add("TableName", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                        //P.Add("ColumnName", dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                        //P.Add("ColumnID", dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                        //P.Add("ColumnName_EN", dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                        //P.Add("ColumnName_HK", dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                        string AppCode = textBox2.Text.Trim();
                        string TableName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string ColumnName = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        string ColumnID = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        string ColumnName_EN = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                        string ColumnName_HK = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                        string sql = @"
                                        if not Exists(select 1 from SYSLANGUAGE where AppCode='{0}' and ColumnName='{2}' and TableName='{1}')
                                        INSERT INTO SYSLANGUAGE
                                        (AppCode,ColumnName,TableName,ColumnID,ColumnName_EN,ColumnName_HK)
                                        VALUES('{0}','{2}','{1}','{3}',N'{4}',N'{5}')
                                        else 
                                        update SYSLANGUAGE set ColumnName_EN=N'{4}',ColumnName_HK=N'{5}'
                                        where AppCode='{0}' and ColumnName='{2}' and TableName='{1}'
                                        ";
                        sql = string.Format(sql, AppCode, TableName, ColumnName, ColumnID, ColumnName_EN, ColumnName_HK);

                        GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);

                    }
                    else
                    {
                        string sql = @"
                                delete from SYSLANGUAGE
                                where AppCode=@AppCode and ColumnName=@ColumnName and TableName=@TableName
                                ";
                        Dictionary<string, string> P = new Dictionary<string, string>();
                        P.Add("AppCode", textBox2.Text.Trim());
                        P.Add("TableName", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                        P.Add("ColumnName", dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());

                        GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);
                    }
                }
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Empty;
                string sqlwhere = string.Empty;
                string strT = textBox1.Text.Trim();

                if (checkBox1.Checked)
                {   //commented by Ashok on 2025/03/28
                    //sql = @"
                    //    SELECT 
                    //    ui_tittle AS '功能名称',
                    //    ui_code AS '控件ID',
                    //    ui_id AS '控件名称',
                    //    ui_cn AS '中文名称',
                    //    ui_en AS '英语名称',
                    //    ui_yn AS '越语名称'
                    //    FROM SJQDMS_UILAN(nolock)
                    //    where 1=1 ";

                    sql = @"
                        SELECT 
                        ui_tittle AS 'Function_Name',
                        ui_code AS 'Control_ID',
                        ui_id AS 'Control_Name',
                        ui_cn AS 'Chinese_Namev',
                        ui_en AS 'English_Name',
                        ui_yn AS 'Vietnamese_Name'
                        FROM SJQDMS_UILAN(nolock)
                        where 1=1 ";
                    if (!string.IsNullOrEmpty(textBox2.Text))
                        sqlwhere += " and ui_tittle='" + textBox2.Text + "'";
                    if (!string.IsNullOrEmpty(strT))
                    {
                        sqlwhere += " and (ui_tittle like '%{0}%' or ui_code like '%{0}%' or ui_id like '%{0}%' or ui_cn like '%{0}%' " +
                            " or ui_en like '%{0}%' or ui_yn like '%{0}%')";
                        sqlwhere = string.Format(sqlwhere, strT);
                    }
                    sql = sql + sqlwhere + "  order by ui_tittle,ui_code,ui_id ";
                    DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                    dataGridView1.DataSource = dt;
                    SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, Program.Client, Program.Language, Program.WebServiceUrl, dataGridView1);
                }
                else
                {
                    //按钮多语言搜索
                    if (!string.IsNullOrEmpty(strT))
                    {
                        sqlwhere = " and (AppCode like '%{0}%' or TableName like '%{0}%' or  ColumnID like '%{0}%' or  ColumnName like '%{0}%' " +
                            "or  ColumnName_en like '%{0}%' or  ColumnName_HK like '%{0}%' or )";
                        sqlwhere = string.Format(sqlwhere, strT);
                    }
                    Getparent(sqlwhere, strT);
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                 
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows != null)
                {
                    if (checkBox1.Checked)
                    {
                        string ui_id = dataGridView1.CurrentRow.Cells["控件名称"].Value.ToString();
                        string ui_code = dataGridView1.CurrentRow.Cells["控件ID"].Value.ToString();

                        string msg1 = "hint";
                        string msg2 = "Are you sure you want to delete?";
                        List<string> lstKeys = new List<string>();
                        lstKeys.Add(msg1);
                        lstKeys.Add(msg2);
                        Dictionary<string, object> dic = SJeMES_Framework.Common.UIHelper.UIListMsg(lstKeys, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        if (dic.Count > 0)
                        {
                            msg1 = dic[msg1].ToString();
                            msg2 = dic[msg2].ToString();
                        }

                        DialogResult dr = MessageBox.Show(msg2, msg1, MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            string sql = @" delete SJQDMS_UILAN where ui_code=@ui_code and ui_id=@ui_id ";
                            Dictionary<string, string> P = new Dictionary<string, string>();
                            P.Add("ui_code", ui_code);
                            P.Add("ui_id", ui_id);
                            Dictionary<string, object> ret = GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);
                            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                            {
                                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            }
                            else
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Deletion failed！", Program.Client, Program.WebServiceUrl, Program.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg); 
                            }
                        }
                    }
                    else
                    {
                        string TableName = dataGridView1.CurrentRow.Cells["所属表"].Value.ToString();
                        string ColumnID = dataGridView1.CurrentRow.Cells["字段代号"].Value.ToString();

                        string msg1 = "hint";
                        string msg2 = "Are you sure you want to delete?";
                        List<string> lstKeys = new List<string>();
                        lstKeys.Add(msg1);
                        lstKeys.Add(msg2);
                        Dictionary<string, object> dic = SJeMES_Framework.Common.UIHelper.UIListMsg(lstKeys, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        if (dic.Count > 0)
                        {
                            msg1 = dic[msg1].ToString();
                            msg2 = dic[msg2].ToString();
                        }

                        DialogResult dr = MessageBox.Show(msg2, msg1, MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            string sql = @" delete SYSLANGUAGE where TableName=@TableName and ColumnID=@ColumnID ";
                            Dictionary<string, string> P = new Dictionary<string, string>();
                            P.Add("TableName", TableName);
                            P.Add("ColumnID", ColumnID);
                            Dictionary<string, object> ret = GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);
                            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                            {
                                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            }
                            else
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Deletion failed！", Program.Client, Program.WebServiceUrl, Program.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                            }
                        }
                    }
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please select the data to delete！", Program.Client, Program.WebServiceUrl, Program.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg); 
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
