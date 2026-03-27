using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;

namespace SJeMES_Control_Library.Controls
{
    public partial class UCModuleDataBody : UCModuleControl, IContainerControl
    {

        private DataTable _BData;
        public DataTable BData
        {
            get { return _BData; }
            set {
                _BData = value;

                dataGridView1.DataSource = _BData.DefaultView;
                dataGridView1.Update();
            }
        }

        private bool _ReadOnly=false;
        public override bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
                if (value)
                {
                    panel1.Visible = false;

                }
                else
                {
                    panel1.Visible = true;
                }
            }
        }

        private SJeMES_Framework.Web.JSONPanelClassB _ControlConfig;
        public SJeMES_Framework.Web.JSONPanelClassB ControlConfig
        {
            get { return _ControlConfig; }
            set { _ControlConfig = value; }
        }
        private string _HeadId;
        public string HeadId
        {
            get { return _HeadId; }
            set { _HeadId = value; }
        }

        private string _ModuleCode;
        public string ModuleCode
        {
            get { return _ModuleCode; }
            set { _ModuleCode = value; }
        }


        private SJeMES_Framework.Class.ClientClass _Client;
        public SJeMES_Framework.Class.ClientClass Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        //定义委托
        public delegate void ReadLoadHandle(object sender, EventArgs e);
        //定义事件
        public event ReadLoadHandle ReadLoad;

        public UCModuleDataBody(SJeMES_Framework.Web.JSONPanelClassB ControlConfig,string HeadId,string ModuleCode, SJeMES_Framework.Class.ClientClass Client)
        {
            InitializeComponent();
            this.ControlConfig = ControlConfig;
            this.HeadId = HeadId;
            this.Client = Client;
            this.ModuleCode = ModuleCode;
            string sql = "";
            if (Client.Language != "cn")
            {
                sql = @"
SELECT 
ui_tittle AS '功能名称',
ui_code AS '控件ID',
ui_cn AS '控件名称',
ui_en AS '英语名称',
ui_yn AS '粤语名称'
FROM SJQDMS_UILAN where ui_tittle='all'";
                DataTable dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(_Client.WebServiceUrl, sql, new Dictionary<string, string>());
                string name = "";
                if (Client.Language == "hk")
                {
                    name = dtLAN.Select("控件名称='" + label2.Text + "'")[0]["粤语名称"].ToString();
                    label2.Text = !string.IsNullOrEmpty(name) ? name : label2.Text;                   

                }
                else if (Client.Language == "en")
                {
                    name = dtLAN.Select("控件名称='" + label2.Text + "'")[0]["英语名称"].ToString();
                    label2.Text = !string.IsNullOrEmpty(name) ? name : label2.Text;

                }
            }
            sql = @"
SELECT
a.AppCode AS '模块代号',
a.AppName AS '模块名称',
'False' AS '全部权限',
ISNULL([Select],'False') AS '查看数据',
ISNULL([Add],'False') AS '添加数据',
ISNULL([Edit],'False') AS '修改数据',
ISNULL([Delete],'False') AS '删除数据',
ISNULL(DoSure ,'False') AS '确认操作',
ISNULL(Audit ,'False') AS '审核操作',
ISNULL(DoWork ,'False') AS '其他操作',
ISNULL([Print] ,'False') AS '打印',
ISNULL(Fun ,'False') AS '更多功能'
FROM SYSAPP03M a
LEFT JOIN SYSUSER02M b ON a.AppCode = b.AppCode
where a.AppName in(select menuname from SYSPOWER 
where  UserCode='" + Client.UserCode + "') and UserCode= '" + Client.UserCode + "' and a.AppCode='" + ModuleCode.Remove(0, 3) + "'";
            //DataTable dt = Client.GetDT(sql);

            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["添加数据"].ToString().Trim() == "False")
                {
                    ucBtnImg4.Enabled = false;
                }
                if (dt.Rows[0]["修改数据"].ToString().Trim() == "False")
                {
                    //ucBtnImg5.Visible = false;
                    ucBtnImg3.Enabled = false;

                }
                if (dt.Rows[0]["删除数据"].ToString().Trim() == "False")
                {
                    //ucBtnImg3.Visible = false;
                    ucBtnImg5.Enabled = false;

                }
            }

            if (!ControlConfig.table.ToLower().Contains("select"))
            {
                UpdateDataColumn();
            
                this.dataGridView1.AutoGenerateColumns = false;
            }
            else
            {
                this.dataGridView1.AutoGenerateColumns = true;
            }
        }

        private void UpdateDataColumn()
        {
            if (this.ControlConfig != null)
            {
                dataGridView1.Columns.Clear();
                string sql = @"select * from (select a.UserCode,b.AppCode,b.TableName,
b.ColumnName,b.ColumnID from SYSROLE01A1 a
left join SYSROLE01M c on a.Role_Name = c.Role_Name
left join SYSPOWER_R b on c.Role_No = b.Role_No)a where UserCode='" + Client.UserCode + "' and AppCode='" + ModuleCode.Remove(0, 3) + "'";
                DataTable dtPow = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
                string columnName = "TableName", columnName1 = "ColumnName";

                string sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode.Remove(0, 3) + "'";
                //DataTable dt = Client.GetDT(sql);

                DataTable dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
                if (dtPow.Rows.Count == 0)
                {
                    sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode + "'";
                    dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
                }
                if (dtPow.Rows.Count == 0)
                {
                    foreach (SJeMES_Framework.Web.JSONControlB b in this.ControlConfig.tableHead)
                    {
                        ///

                        DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                        dgvc.Name = "dc_" + b.prop;
                        dgvc.HeaderText = b.label;

                        if (dtLAN.Rows.Count > 0 && Client.Language != "cn")
                        {
                            DataRow[] dataRows_LAN = dtLAN.Select(columnName1 + "='" + b.label + "' and " + columnName + "='" + ControlConfig.table + "'");
                            if (Client.Language == "en" && dataRows_LAN.Length > 0)
                            {
                                dgvc.HeaderText = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_EN"].ToString()) ? dataRows_LAN[0]["ColumnName_EN"].ToString() : b.label;
                            }
                            else if (Client.Language == "hk" && dataRows_LAN.Length > 0)
                            {
                                dgvc.HeaderText = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_HK"].ToString()) ? dataRows_LAN[0]["ColumnName_HK"].ToString() : b.label;
                            }
                        }

                        dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dgvc.DataPropertyName = b.prop;

                        dataGridView1.Columns.Add(dgvc);
                    }
                }
                else
                {
                    foreach (SJeMES_Framework.Web.JSONControlB b in this.ControlConfig.tableHead)
                    {
                        DataRow[] dataRows1 = dtPow.Select(columnName1 + "='" + b.label + "' and "+ columnName + "='" + ControlConfig.table + "'");
                        if (dataRows1.Length > 0)
                        {
                            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                            dgvc.Name = "dc_" + b.prop;
                            dgvc.HeaderText = b.label;

                            if (dtLAN.Rows.Count > 0 && Client.Language != "cn")
                            {
                                DataRow[] dataRows_LAN = dtLAN.Select(columnName1 + "='" + b.label + "' and " + columnName + "='" + ControlConfig.table + "'");
                                if (Client.Language == "en" && dataRows_LAN.Length > 0)
                                {
                                    dgvc.HeaderText = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_EN"].ToString()) ? dataRows_LAN[0]["ColumnName_EN"].ToString() : b.label;
                                }
                                else if (Client.Language == "hk" && dataRows_LAN.Length > 0)
                                {
                                    dgvc.HeaderText = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_HK"].ToString()) ? dataRows_LAN[0]["ColumnName_HK"].ToString() : b.label;
                                }
                            }

                            dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgvc.DataPropertyName = b.prop;

                            dataGridView1.Columns.Add(dgvc);
                        }
                    }
                }
            }
        }

        private void ucBtnImg3_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    if (SJeMES_Control_Library.MessageHelper.ShowWarning(this.FindForm(), "Are you sure to delete the selected data？") == DialogResult.OK)
                    {
                        List<string> Ids = new List<string>();
                        for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
                        {
                            string id =
                            (dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem as DataRowView).Row["id"].ToString();
                            if (!Ids.Contains(id))
                            {
                                Ids.Add(id);
                            }
                        }


                        DataTable dt = new DataTable();
                        dt.Columns.Add("TableName");
                        dt.Columns.Add("Id");
                        foreach (string id in Ids)
                        {
                            DataRow dr = dt.NewRow();
                            dr["TableName"] = ControlConfig.table;
                            dr["Id"] = id;
                            dt.Rows.Add(dr);
                        }
                        if (ModuleHelper.DelData(dt, Client))
                        {
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this.FindForm(), "Data deleted successfully");
                            if (ReadLoad != null)
                                ReadLoad(this, new EventArgs());
                        }
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), "Please select the row to delete first");
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void ucBtnImg4_BtnClick(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("id");
            //foreach (SJeMES_Framework.Web.JSONControlB b in ControlConfig.tableHead)
            //{
            //    dt.Columns.Add(b.prop);
            //}

            //Module.frmModuleBodyData frm = new
            //     Module.frmModuleBodyData(MaterialSkinManager.Themes.LIGHT,
            //     ControlConfig,dt.NewRow() ,HeadId, ModuleCode,Client);

            //if(frm.ShowDialog() == DialogResult.OK)
            //{
            //    if (ReadLoad != null)
            //        ReadLoad(this, new EventArgs());
            //}

            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            foreach (SJeMES_Framework.Web.JSONControlB b in ControlConfig.tableHead)
            {
                if (!dt.Columns.Contains(b.prop))
                {
                    dt.Columns.Add(b.prop);
                }
            }
            //string key = ModuleHelper.GetHD(ModuleCode, ControlConfig.table, HeadId, Client);
            //DataRow dr = dt.NewRow();
            //dr[1] = key;
            //dr[0] = HeadId;
            //dt.Rows.Add(dr);


            DataTable dt2 = ModuleHelper.GetTableHeadsValue(ModuleCode, ControlConfig.table, HeadId, Client);
            if (dt2 != null)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = HeadId;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colname = dt.Columns[j].ColumnName;
                        if (dt2.Columns.Contains(colname))
                        {
                            dr[colname] = dt2.Rows[i][colname].ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }

            Module.frmModuleBodyData frm = new
                 Module.frmModuleBodyData(MaterialSkinManager.Themes.LIGHT,
                 ControlConfig, dt.NewRow(), HeadId, ModuleCode, Client);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (ReadLoad != null)
                    ReadLoad(this, new EventArgs());
            }
        }

        private void ucBtnImg5_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {

                    string id =
                    (dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem as DataRowView).Row["id"].ToString();

                    DataRow[] dr = BData.Select(" id = " + id);

                    Module.frmModuleBodyData frm = new
                 Module.frmModuleBodyData(MaterialSkinManager.Themes.LIGHT,
                 ControlConfig, dr[0], HeadId, ModuleCode, Client);

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (ReadLoad != null)
                            ReadLoad(this, new EventArgs());
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), "Please select the row to modify first");
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }
    }
}
