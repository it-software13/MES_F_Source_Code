//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using MaterialSkin;
//using MaterialSkin.Controls;
//using static SJeMES_Control_Library.Controls.UCModuleControl;

//namespace SJeMES_Control_Library.Controls.Module
//{
//    public partial class frmModuleBodyData : MaterialForm
//    {
//        private MaterialSkinManager materialSkinManager;

//        public Dictionary<string, UCModuleControl> HControls;


//        private DataTable dtLAN;

//        private string _Status;
//        public string Status
//        {
//            get { return _Status; }
//            set { _Status = value; }
//        }

//        private string _HeadId;
//        public string HeadId
//        {
//            get { return _HeadId; }
//            set { _HeadId = value; }
//        }

//        private string _ModuleCode;
//        public string ModuleCode
//        {
//            get { return _ModuleCode; }
//            set { _ModuleCode = value; }
//        }

//        private SJeMES_Framework.Web.JSONPanelClassB _BodyConfig;
//        public SJeMES_Framework.Web.JSONPanelClassB BodyConfig
//        {
//            get { return _BodyConfig; }
//            set { _BodyConfig = value; }
//        }

//        private DataRow _BData;
//        public DataRow BData
//        {
//            get { return _BData; }
//            set { _BData = value; }
//        }

//        private SJeMES_Framework.Class.ClientClass _Client;
//        public SJeMES_Framework.Class.ClientClass Client
//        {
//            get { return _Client; }
//            set { _Client = value; }
//        }


//        public frmModuleBodyData(MaterialSkinManager.Themes SkinThemes,
//            SJeMES_Framework.Web.JSONPanelClassB BodyConfig, DataRow BData, string HeadId, string ModuleCode,
//            SJeMES_Framework.Class.ClientClass Client)
//        {
//            InitializeComponent();

//            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
//             SkinThemes, materialSkinManager, this);

//            this.BodyConfig = BodyConfig;
//            this.BData = BData;
//            if (!string.IsNullOrEmpty(BData["id"].ToString()))
//            {
//                this.Status = "Edit";
//            }
//            else
//            {
//                this.Status = "Add";
//            }
//            this.HeadId = HeadId;
//            this.Client = Client;
//            this.ModuleCode = ModuleCode;

//            LoadControl();
//            UpdateData();

//        }

//        private void UpdateData()
//        {
//            try
//            {


//                //foreach (string key in HControls.Keys)
//                //{
//                //    UCModuleControl control = HControls[key];
//                //    if (control.DataType != UCModuleControl.ControlDataType.Bool)
//                //    {
//                //        control.Value = BData[control.Prop].ToString();
//                //    }
//                //    else
//                //    {
//                //        if (!string.IsNullOrEmpty(BData[control.Prop].ToString()))
//                //            control.Value = Convert.ToBoolean(BData[control.Prop].ToString());
//                //        else
//                //            control.Value = false;
//                //    }
//                //}
//                foreach (string key in HControls.Keys)
//                {
//                    UCModuleControl control = HControls[key];
//                    if (control.DataType != UCModuleControl.ControlDataType.Bool)
//                    {
//                        if (this.Status == "Add")
//                        {

//                            // 时间新增不用赋值
//                            if ("SJeMES_Control_Library.Controls.UCModuleDateTime" == control.ToString())
//                            {
//                                continue;
//                            }

//                            if (BData.Table.Rows.Count > 0)
//                            {
//                                control.Value = BData.Table.Rows[0][control.Prop].ToString();
//                            }

//                            else
//                            {
//                                control.Value = BData[control.Prop].ToString();
//                            }
//                        }
//                        else
//                        {
//                            control.Value = BData[control.Prop].ToString();
//                        }

//                        //
//                    }
//                    else
//                    {
//                        if (!string.IsNullOrEmpty(BData[control.Prop].ToString()))
//                            control.Value = Convert.ToBoolean(BData[control.Prop].ToString());
//                        else
//                            control.Value = false;
//                    }

//                    //if (control.DataSQL!=null)
//                    //{
//                    //    if (control.DataSQL.StartsWith("HeadData."))
//                    //    {
//                    //        control.DataSQL = control.DataSQL.Replace("HeadData." + control.DataSQL.Replace("HeadData.", ""), BData[control.DataSQL.Replace("HeadData.", "")].ToString());
//                    //    }
//                    //}


//                }

//            }
//            catch (Exception ex)
//            {
//                MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }
//        }
//        private string checkName(DataTable dt, string name)
//        {
//            string columnName = "TableName", columnName1 = "ColumnName";

//            if (dt.Rows.Count > 0 && Client.Language != "cn")
//            {
//                DataRow[] dataRows_LAN = dt.Select(columnName1 + "='" + name + "' and " + columnName + "='" + BodyConfig.table + "'");
//                if (Client.Language == "en" && dataRows_LAN.Length > 0)
//                {
//                    name = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_EN"].ToString()) ? dataRows_LAN[0]["ColumnName_EN"].ToString() : name;
//                }
//                else if (Client.Language == "hk" && dataRows_LAN.Length > 0)
//                {
//                    name = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_HK"].ToString()) ? dataRows_LAN[0]["ColumnName_HK"].ToString() : name;
//                }
//            }
//            return name;
//        }




//        private void LoadControl()
//        {
//            HControls = new Dictionary<string, UCModuleControl>();
//            if (Client.Language != "cn")
//            {
//                string sql = @"
//SELECT 
//ui_tittle AS '功能名称',
//ui_code AS '控件ID',
//ui_cn AS '控件名称',
//ui_en AS '英语名称',
//ui_yn AS '粤语名称'
//FROM SJQDMS_UILAN where ui_tittle='all' and ui_cn='" + label2.Text.Trim() + "'";
//                DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(_Client.WebServiceUrl, sql, new Dictionary<string, string>());
//                if (Client.Language == "en")
//                    label2.Text = !string.IsNullOrEmpty(dt.Rows[0]["英语名称"].ToString()) ? dt.Rows[0]["英语名称"].ToString() : label2.Text;
//                else
//                    label2.Text = !string.IsNullOrEmpty(dt.Rows[0]["粤语名称"].ToString()) ? dt.Rows[0]["粤语名称"].ToString() : label2.Text;

//            }
//            try
//            {
//                string sql = @"select * from (select a.UserCode,b.AppCode,b.TableName,
//b.ColumnName,b.ColumnID from SYSROLE01A1 a
//left join SYSROLE01M c on a.Role_Name = c.Role_Name
//left join SYSPOWER_R b on c.Role_No = b.Role_No)a where UserCode='" + Client.UserCode + "' and AppCode='" + ModuleCode.Remove(0, 3) + "'";
//                DataTable dtPow = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
//                string columnName = "TableName", columnName1 = "ColumnName";

//                string sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode.Remove(0, 3) + "'";
//                //DataTable dt = Client.GetDT(sql);

//                dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
//                if (dtPow.Rows.Count == 0)
//                {
//                    sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode + "'";
//                    dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
//                }
//                string name = "";

//                if (dtPow.Rows.Count == 0)
//                {
//                    foreach (SJeMES_Framework.Web.JSONControlB b in BodyConfig.tableHead)
//                    {
//                        if (!b.systemFiled)

//                        {
//                            if (b.type == "input" || b.type == "other" || b.type == "txt")
//                            {
//                                UCModuleTextBox control = new UCModuleTextBox();
//                                //UCModuleControl mc = new UCModuleControl();
//                                //mc.InitControl(b);
//                                control.InitControl(b);
//                                control.dr = BData;
//                                control.Client = this.Client;
//                                control.SelectedData += Control_SelectedData;
//                                control.Value = BData[control.Prop].ToString();
//                                control.Title = checkName(dtLAN, control.Title);
//                                HControls.Add(control.Title, control);



//                            }
//                            else if (b.type == "select")
//                            {
//                                UCModuleComBox control = new UCModuleComBox();
//                                control.InitControl(b);
//                                control.Value = BData[control.Prop].ToString();
//                                control.Title = checkName(dtLAN, control.Title);
//                                HControls.Add(control.Title, control);
//                            }

//                            else if (b.type == "date")
//                            {
//                                UCModuleDateTime control = new UCModuleDateTime();
//                                control.InitControl(b);
//                                control.Value = BData[control.Prop].ToString();
//                                control.Title = checkName(dtLAN, control.Title);
//                                HControls.Add(control.Title, control);
//                            }

//                            else if (b.type == "switch")
//                            {
//                                UCModuleSwitch control = new UCModuleSwitch();
//                                control.InitControl(b);
//                                control.Value = BData[control.Prop].ToString();
//                                control.Title = checkName(dtLAN, control.Title);

//                                HControls.Add(control.Title, control);
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    foreach (SJeMES_Framework.Web.JSONControlB b in BodyConfig.tableHead)
//                    {
//                        DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
//                        DataRow[] dataRows1 = dtPow.Select(columnName1 + "='" + b.label + "' and " + columnName + "='" + BodyConfig.table + "'");

//                        if (dataRows1.Length > 0)
//                        {
//                            if (!b.systemFiled)

//                            {
//                                if (b.type == "input" || b.type == "other" || b.type == "txt")
//                                {
//                                    UCModuleTextBox control = new UCModuleTextBox();
//                                    control.InitControl(b);
//                                    control.Client = this.Client;
//                                    control.SelectedData += Control_SelectedData;
//                                    control.Value = BData[control.Prop].ToString();
//                                    control.Title = checkName(dtLAN, control.Title);
//                                    HControls.Add(control.Title, control);
//                                }
//                                else if (b.type == "select")
//                                {
//                                    UCModuleComBox control = new UCModuleComBox();
//                                    control.InitControl(b);
//                                    control.Value = BData[control.Prop].ToString();
//                                    control.Title = checkName(dtLAN, control.Title);
//                                    HControls.Add(control.Title, control);
//                                }

//                                else if (b.type == "date")
//                                {
//                                    UCModuleDateTime control = new UCModuleDateTime();
//                                    control.InitControl(b);
//                                    control.Value = BData[control.Prop].ToString();
//                                    control.Title = checkName(dtLAN, control.Title);
//                                    HControls.Add(control.Title, control);
//                                }

//                                else if (b.type == "switch")
//                                {
//                                    UCModuleSwitch control = new UCModuleSwitch();
//                                    control.InitControl(b);
//                                    control.Value = BData[control.Prop].ToString();
//                                    control.Title = checkName(dtLAN, control.Title);
//                                    HControls.Add(control.Title, control);
//                                }
//                            }
//                        }
//                    }


//                }
//                int i = 0;
//                int k = 0;
//                int RowMax = 4;
//                if (this.FindForm().Width > 1900)
//                {
//                    RowMax = 6;
//                }
//                int Height = 0;
//                foreach (string key in HControls.Keys)
//                {
//                    UCModuleControl control = HControls[key];

//                    if (this.Status == "Edit" && !control.IsEdit)
//                    {
//                        control.ReadOnly = true;
//                    }
//                    else if (this.Status == "Add" && !control.IsAdd)
//                    {
//                        control.ReadOnly = true;
//                    }

//                    control.Top = 55 * (i / RowMax);
//                    control.Left = 250 * k + (10 * k);

//                    this.panel_Controls.Controls.Add(control);

//                    i++;
//                    k++;
//                    if (k == RowMax)
//                    {
//                        k = 0;
//                    }
//                    Height = control.Top + 60;
//                }

//                this.Height = Height + 200;
//            }
//            catch (Exception ex)
//            {
//                MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }
//        }

//        private void Control_SelectedData(DataTable dtSelected)
//        {
//            try
//            {
//                foreach (DataColumn dc in dtSelected.Columns)
//                {
//                    string keyId = "R";
//                    if (dc.ColumnName != "R")
//                    {
//                        keyId = checkName(dtLAN, dc.ColumnName);

//                    }
//                    foreach (string key in HControls.Keys)
//                    {

//                        if (key == keyId)
//                        {
//                            HControls[key].Value = dtSelected.Rows[0][dc.ColumnName].ToString();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }
//        }

//        private void btn_Back_BtnClick(object sender, EventArgs e)
//        {
//            this.DialogResult = DialogResult.Cancel;
//            this.Close();
//        }

//        private void btn_Save_BtnClick(object sender, EventArgs e)
//        {
//            try
//            {
//                if (CheckData())
//                {

//                    string TableName = BodyConfig.table;
//                    string AppCode = ModuleCode;

//                    Dictionary<string, string> TableData = new Dictionary<string, string>();
//                    List<Dictionary<string, object>> RowData = new List<Dictionary<string, object>>();
//                    Dictionary<string, object> Row = new Dictionary<string, object>();
//                    if (!string.IsNullOrEmpty(BData["id"].ToString()))
//                    {
//                        Row.Add("id", BData["id"].ToString());
//                    }

//                    foreach (string key in HControls.Keys)
//                    {
//                        UCModuleControl control = HControls[key];
//                        if (!control.IsSysField)
//                        {
//                            if (key.Contains("日期"))
//                            {
//                                if (string.IsNullOrEmpty(control.Value.ToString()))
//                                {
//                                    control.Value = DateTime.Now.ToString("yyyy-MM-dd");
//                                }
//                            }
//                            Row.Add(control.Prop, control.Value);
//                        }
//                    }

//                    RowData.Add(Row);
//                    TableData.Add(TableName, Newtonsoft.Json.JsonConvert.SerializeObject(RowData));

//                    if (!string.IsNullOrEmpty(BData["id"].ToString()))
//                    {
//                        //MessageBox.Show(BData["id"].ToString());
//                        if (ModuleHelper.EditHData(AppCode, TableName, TableData, Client))
//                        {
//                            //MessageBox.Show(BData["id"].ToString());
//                            MessageHelper.ShowSuccess(this.FindForm(), "修改数据成功");
//                            this.DialogResult = DialogResult.OK;
//                            this.Close();
//                        }
//                    }
//                    else
//                    {
//                        //MessageBox.Show("添加数据成功");
//                        if (ModuleHelper.AddBData(AppCode, TableName, HeadId, TableData, Client))
//                        {
//                            MessageHelper.ShowSuccess(this.FindForm(), "添加数据成功");
//                            this.DialogResult = DialogResult.OK;
//                            this.Close();
//                        }

//                    }
//                }
//                else
//                {
//                    MessageHelper.ShowErr(this.FindForm(), "请确认输入的内容正确");
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }
//        }

//        private bool CheckData()
//        {
//            bool ret = true;
//            foreach (string key in HControls.Keys)
//            {
//                UCModuleControl control = HControls[key];

//                if (!control.IsNull && string.IsNullOrEmpty(control.Value.ToString()))
//                {
//                    control.ErrMsg = "该内容不能为空"; control.ShowErrMsg = true; ret = false;
//                }
//                else
//                {
//                    control.ShowErrMsg = false;
//                }

//                if (!string.IsNullOrEmpty(control.Value.ToString()))
//                {
//                    switch (control.DataType)
//                    {
//                        case UCModuleControl.ControlDataType.Int:
//                            try
//                            {
//                                Convert.ToInt32(control.Value);
//                                control.ShowErrMsg = false;
//                            }
//                            catch { control.ErrMsg = "输入内容必须为整数"; control.ShowErrMsg = true; ret = false; }
//                            break;
//                        case UCModuleControl.ControlDataType.Decimal:
//                            try
//                            {
//                                Convert.ToDecimal(control.Value);
//                                control.ShowErrMsg = false;
//                            }
//                            catch { control.ErrMsg = "输入内容必须为数字"; control.ShowErrMsg = true; ret = false; }
//                            break;

//                    }
//                }


//            }

//            return ret;
//        }

//    }
//}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using static SJeMES_Control_Library.Controls.UCModuleControl;

namespace SJeMES_Control_Library.Controls.Module
{
    public partial class frmModuleBodyData : MaterialForm
    {
        private MaterialSkinManager materialSkinManager;

        public Dictionary<string, UCModuleControl> HControls;


        private DataTable dtLAN;

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
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

        private SJeMES_Framework.Web.JSONPanelClassB _BodyConfig;
        public SJeMES_Framework.Web.JSONPanelClassB BodyConfig
        {
            get { return _BodyConfig; }
            set { _BodyConfig = value; }
        }

        private DataRow _BData;
        public DataRow BData
        {
            get { return _BData; }
            set { _BData = value; }
        }

        private SJeMES_Framework.Class.ClientClass _Client;
        public SJeMES_Framework.Class.ClientClass Client
        {
            get { return _Client; }
            set { _Client = value; }
        }


        public frmModuleBodyData(MaterialSkinManager.Themes SkinThemes,
            SJeMES_Framework.Web.JSONPanelClassB BodyConfig, DataRow BData, string HeadId, string ModuleCode,
            SJeMES_Framework.Class.ClientClass Client)
        {
            InitializeComponent();

            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             SkinThemes, materialSkinManager, this);

            this.BodyConfig = BodyConfig;
            this.BData = BData;
            if (!string.IsNullOrEmpty(BData["id"].ToString()))
            {
                this.Status = "Edit";
            }
            else
            {
                this.Status = "Add";
            }
            this.HeadId = HeadId;
            this.Client = Client;
            this.ModuleCode = ModuleCode;

            LoadControl();
            UpdateData(); 
        }

        private void UpdateData()
        {
            try
            {


                //foreach (string key in HControls.Keys)
                //{
                //    UCModuleControl control = HControls[key];
                //    if (control.DataType != UCModuleControl.ControlDataType.Bool)
                //    {
                //        control.Value = BData[control.Prop].ToString();
                //    }
                //    else
                //    {
                //        if (!string.IsNullOrEmpty(BData[control.Prop].ToString()))
                //            control.Value = Convert.ToBoolean(BData[control.Prop].ToString());
                //        else
                //            control.Value = false;
                //    }
                //}
                foreach (string key in HControls.Keys)
                {
                    UCModuleControl control = HControls[key];
                    if (control.DataType != UCModuleControl.ControlDataType.Bool)
                    {
                        if (this.Status == "Add")
                        {

                            // 时间新增不用赋值
                            if ("SJeMES_Control_Library.Controls.UCModuleDateTime" == control.ToString())
                            {
                                continue;
                            }

                            if (BData.Table.Rows.Count > 0)
                            {
                                control.Value = BData.Table.Rows[0][control.Prop].ToString();
                            }

                            else
                            {
                                control.Value = BData[control.Prop].ToString();
                            }
                        }
                        else
                        {
                            control.Value = BData[control.Prop].ToString();
                        }

                        //
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(BData[control.Prop].ToString()))
                            control.Value = Convert.ToBoolean(BData[control.Prop].ToString());
                        else
                            control.Value = false;
                    }

                    //if (control.DataSQL!=null)
                    //{
                    //    if (control.DataSQL.StartsWith("HeadData."))
                    //    {
                    //        control.DataSQL = control.DataSQL.Replace("HeadData." + control.DataSQL.Replace("HeadData.", ""), BData[control.DataSQL.Replace("HeadData.", "")].ToString());
                    //    }
                    //}


                }

            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }
        private string checkName(DataTable dt, string name)
        {
            string columnName = "TableName", columnName1 = "ColumnName";

            if (dt.Rows.Count > 0 && Client.Language != "cn")
            {
                DataRow[] dataRows_LAN = dt.Select(columnName1 + "='" + name + "' and " + columnName + "='" + BodyConfig.table + "'");
                if (Client.Language == "en" && dataRows_LAN.Length > 0)
                {
                    name = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_EN"].ToString()) ? dataRows_LAN[0]["ColumnName_EN"].ToString() : name;
                }
                else if (Client.Language == "hk" && dataRows_LAN.Length > 0)
                {
                    name = !string.IsNullOrEmpty(dataRows_LAN[0]["ColumnName_HK"].ToString()) ? dataRows_LAN[0]["ColumnName_HK"].ToString() : name;
                }
            }
            return name;
        }




        private void LoadControl()
        {
            HControls = new Dictionary<string, UCModuleControl>();
            if (Client.Language != "cn")
            {
                string sql = @"
SELECT 
ui_tittle AS '功能名称',
ui_code AS '控件ID',
ui_cn AS '控件名称',
ui_en AS '英语名称',
ui_yn AS '粤语名称'
FROM SJQDMS_UILAN where ui_tittle='all' and ui_cn='" + label2.Text.Trim() + "'";
                DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(_Client.WebServiceUrl, sql, new Dictionary<string, string>());
                if (Client.Language == "en")
                    label2.Text = !string.IsNullOrEmpty(dt.Rows[0]["英语名称"].ToString()) ? dt.Rows[0]["英语名称"].ToString() : label2.Text;
                else
                    label2.Text = !string.IsNullOrEmpty(dt.Rows[0]["粤语名称"].ToString()) ? dt.Rows[0]["粤语名称"].ToString() : label2.Text;

            }
            try
            {
                string sql = @"select * from (select a.UserCode,b.AppCode,b.TableName,
b.ColumnName,b.ColumnID from SYSROLE01A1 a
left join SYSROLE01M c on a.Role_Name = c.Role_Name
left join SYSPOWER_R b on c.Role_No = b.Role_No)a where UserCode='" + Client.UserCode + "' and AppCode='" + ModuleCode.Remove(0, 3) + "'";
                DataTable dtPow = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
                string columnName = "TableName", columnName1 = "ColumnName";

                string sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode.Remove(0, 3) + "'";
                //DataTable dt = Client.GetDT(sql);

                dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
                if (dtPow.Rows.Count == 0)
                {
                    sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode + "'";
                    dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
                }
                string name = "";

                if (dtPow.Rows.Count == 0)
                {
                    foreach (SJeMES_Framework.Web.JSONControlB b in BodyConfig.tableHead)
                    {
                        if (!b.systemFiled)

                        {
                            if (b.type == "input" || b.type == "other" || b.type == "txt")
                            {
                                UCModuleTextBox control = new UCModuleTextBox();
                                //UCModuleControl mc = new UCModuleControl();
                                //mc.InitControl(b);
                                control.InitControl(b);
                                control.dr = BData;
                                control.Client = this.Client;
                                control.SelectedData += Control_SelectedData;
                                control.Value = BData[control.Prop].ToString();
                                control.Title = checkName(dtLAN, control.Title);
                                HControls.Add(control.Title, control);



                            }
                            else if (b.type == "select")
                            {
                                UCModuleComBox control = new UCModuleComBox();
                                control.InitControl(b);
                                control.Value = BData[control.Prop].ToString();
                                control.Title = checkName(dtLAN, control.Title);
                                HControls.Add(control.Title, control);
                            }

                            else if (b.type == "date")
                            {
                                UCModuleDateTime control = new UCModuleDateTime();
                                control.InitControl(b);
                                control.Value = BData[control.Prop].ToString();
                                control.Title = checkName(dtLAN, control.Title);
                                HControls.Add(control.Title, control);
                            }

                            else if (b.type == "switch")
                            {
                                UCModuleSwitch control = new UCModuleSwitch();
                                control.InitControl(b);
                                control.Value = BData[control.Prop].ToString();
                                control.Title = checkName(dtLAN, control.Title);

                                HControls.Add(control.Title, control);
                            }
                        }
                    }
                }
                else
                {
                    foreach (SJeMES_Framework.Web.JSONControlB b in BodyConfig.tableHead)
                    {
                        DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                        DataRow[] dataRows1 = dtPow.Select(columnName1 + "='" + b.label + "' and " + columnName + "='" + BodyConfig.table + "'");

                        if (dataRows1.Length > 0)
                        {
                            if (!b.systemFiled)

                            {
                                if (b.type == "input" || b.type == "other" || b.type == "txt")
                                {
                                    UCModuleTextBox control = new UCModuleTextBox();
                                    control.InitControl(b);
                                    control.Client = this.Client;
                                    control.SelectedData += Control_SelectedData;
                                    control.Value = BData[control.Prop].ToString();
                                    control.Title = checkName(dtLAN, control.Title);
                                    HControls.Add(control.Title, control);
                                }
                                else if (b.type == "select")
                                {
                                    UCModuleComBox control = new UCModuleComBox();
                                    control.InitControl(b);
                                    control.Value = BData[control.Prop].ToString();
                                    control.Title = checkName(dtLAN, control.Title);
                                    HControls.Add(control.Title, control);
                                }

                                else if (b.type == "date")
                                {
                                    UCModuleDateTime control = new UCModuleDateTime();
                                    control.InitControl(b);
                                    control.Value = BData[control.Prop].ToString();
                                    control.Title = checkName(dtLAN, control.Title);
                                    HControls.Add(control.Title, control);
                                }

                                else if (b.type == "switch")
                                {
                                    UCModuleSwitch control = new UCModuleSwitch();
                                    control.InitControl(b);
                                    control.Value = BData[control.Prop].ToString();
                                    control.Title = checkName(dtLAN, control.Title);
                                    HControls.Add(control.Title, control);
                                }
                            }
                        }
                    }


                }
                int i = 0;
                int k = 0;
                int RowMax = 4;
                if (this.FindForm().Width > 1900)
                {
                    RowMax = 6;
                }
                int Height = 0;
                foreach (string key in HControls.Keys)
                {
                    UCModuleControl control = HControls[key];

                    if (this.Status == "Edit" && !control.IsEdit)
                    {
                        control.ReadOnly = true;
                    }
                    else if (this.Status == "Add" && !control.IsAdd)
                    {
                        control.ReadOnly = true;
                    }

                    control.Top = 55 * (i / RowMax);
                    control.Left = 250 * k + (10 * k);

                    this.panel_Controls.Controls.Add(control);

                    i++;
                    k++;
                    if (k == RowMax)
                    {
                        k = 0;
                    }
                    Height = control.Top + 60;
                }

                this.Height = Height + 200;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void Control_SelectedData(DataTable dtSelected)
        {
            try
            {
                foreach (DataColumn dc in dtSelected.Columns)
                {
                    string keyId = "R";
                    if (dc.ColumnName != "R")
                    {
                        keyId = checkName(dtLAN, dc.ColumnName);

                    }
                    foreach (string key in HControls.Keys)
                    {

                        if (key == keyId)
                        {
                            HControls[key].Value = dtSelected.Rows[0][dc.ColumnName].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void btn_Back_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Save_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if (CheckData())
                {

                    string TableName = BodyConfig.table;
                    string AppCode = ModuleCode;

                    Dictionary<string, string> TableData = new Dictionary<string, string>();
                    List<Dictionary<string, object>> RowData = new List<Dictionary<string, object>>();
                    Dictionary<string, object> Row = new Dictionary<string, object>();
                    if (!string.IsNullOrEmpty(BData["id"].ToString()))
                    {
                        Row.Add("id", BData["id"].ToString());
                    }

                    foreach (string key in HControls.Keys)
                    {
                        UCModuleControl control = HControls[key];
                        if (!control.IsSysField)
                        {
                            if (key.Contains("日期"))
                            {
                                if (string.IsNullOrEmpty(control.Value.ToString()))
                                {
                                    control.Value = DateTime.Now.ToString("yyyy-MM-dd");
                                }
                            }
                            Row.Add(control.Prop, control.Value);
                        }
                    }

                    RowData.Add(Row);
                    TableData.Add(TableName, Newtonsoft.Json.JsonConvert.SerializeObject(RowData));

                    if (!string.IsNullOrEmpty(BData["id"].ToString()))
                    {
                        //MessageBox.Show(BData["id"].ToString());
                        if (ModuleHelper.EditHData(AppCode, TableName, TableData, Client))
                        {
                            //MessageBox.Show(BData["id"].ToString());
                            MessageHelper.ShowSuccess(this.FindForm(), "Modify data successfully");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        //MessageBox.Show("添加数据成功");
                        if (ModuleHelper.AddBData(AppCode, TableName, HeadId, TableData, Client))
                        {
                            MessageHelper.ShowSuccess(this.FindForm(), "Add data successfully");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }

                    }
                }
                else
                {
                    MessageHelper.ShowErr(this.FindForm(), "Please confirm the input content is correct");
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private bool CheckData()
        {
            bool ret = true;
            foreach (string key in HControls.Keys)
            {
                UCModuleControl control = HControls[key];

                if (!control.IsNull && string.IsNullOrEmpty(control.Value.ToString()))
                {
                    //control.ErrMsg = "该内容不能为空"; control.ShowErrMsg = true; ret = false;
                    control.ErrMsg = "The content cannot be empty"; control.ShowErrMsg = true; ret = false;
                }
                else
                {
                    control.ShowErrMsg = false;
                }

                if (!string.IsNullOrEmpty(control.Value.ToString()))
                {
                    switch (control.DataType)
                    {
                        case UCModuleControl.ControlDataType.Int:
                            try
                            {
                                Convert.ToInt32(control.Value);
                                control.ShowErrMsg = false;
                            }
                            //catch { control.ErrMsg = "输入内容必须为整数"; control.ShowErrMsg = true; ret = false; }
                            catch { control.ErrMsg = "The input must be an integer"; control.ShowErrMsg = true; ret = false; }
                            break;
                        case UCModuleControl.ControlDataType.Decimal:
                            try
                            {
                                Convert.ToDecimal(control.Value);
                                control.ShowErrMsg = false;
                            }
                            //catch { control.ErrMsg = "输入内容必须为数字"; control.ShowErrMsg = true; ret = false; }
                            catch { control.ErrMsg = "The input must be a number"; control.ShowErrMsg = true; ret = false; }
                            break;

                    }
                }


            }

            return ret;
        }

    }
}

