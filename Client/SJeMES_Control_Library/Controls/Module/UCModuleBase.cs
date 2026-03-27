//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using SJeMES_Framework.Class;

//namespace SJeMES_Control_Library.Controls
//{
//    public partial class UCModuleBase : UCControlBase, IContainerControl
//    {
//        public enum ModuleStatus { Add,Edit,See }

//        public partial class OtherMenu
//        {
//            public string Title;
//            public string Action;
//            public string Url;
//            public string DllName;
//            public string ClassName;
//            public string Method;
//            public Dictionary<string, string> Parameters = new Dictionary<string, string>();
//        }

//        Dictionary<string, UCModuleControl> HControls;
//        Dictionary<string, UCModuleControl> BControls;
//        Dictionary<string, UCModuleControl> HOtherDataControls;

//        Dictionary<string, List<string>> UserPowser = new Dictionary<string, List<string>>();


//        #region 属性


//        private string _DataStatus;
//        public string DataStatus
//        {
//            get { return _DataStatus; }
//            set { 
//                _DataStatus = value;
//                switch (_DataStatus)
//                {
//                    case "2":
//                        btn_Aduit.FillColor = Color.Green;
//                        btn_DoSure.FillColor = Color.Gray;
//                        btn_Edit.Visible = btn_Del.Visible = false;
//                        break;
//                    case "1":
//                        btn_Aduit.FillColor = Color.Gray;
//                        btn_DoSure.FillColor = Color.Green;
//                        btn_Edit.Visible = btn_Del.Visible = false;
//                        break;
//                    case "7":
//                        btn_Aduit.FillColor = Color.Gray;
//                        btn_DoSure.FillColor = Color.Green;
//                        btn_Edit.Visible = btn_Del.Visible = false;
//                        break;
//                    case "8":
//                        btn_Aduit.FillColor = Color.Gray;
//                        btn_DoSure.FillColor = Color.Gray;
//                        btn_Edit.Visible = btn_Del.Visible = true;
//                        break;
//                }
//            }
//        }


//        private bool _HasStatus = false;

//        private DataTable _HData;

//        public DataTable HData
//        {
//            set { 
//                _HData = value; 
//            }
//            get { return _HData; }
//        }

//        private DataTable _dtLAN;
//        public DataTable dtLAN
//        {
//            set
//            {
//                _dtLAN = value;
//            }
//            get { return _dtLAN; }
//        }

//        private Dictionary<string, DataTable> _BData;

//        public Dictionary<string, DataTable> BData
//        {
//            set { _BData = value; }
//            get { return _BData; }
//        }

//        private Dictionary<string, OtherMenu> _OtherMenus;
//        public Dictionary<string, OtherMenu> OtherMenus
//        {
//            set { _OtherMenus = value; }
//            get { return _OtherMenus; }
//        }


//        private SJeMES_Framework.Web.JSONFormClass _ModuleConfig;

//        public SJeMES_Framework.Web.JSONFormClass ModuleConfig
//        {
//            set { _ModuleConfig = value; }
//            get { return _ModuleConfig; }
//        }
//        private string _title;

//        public string title
//        {
//            get
//            {
//                return _title;
//            }
//            set
//            {
//                _title = value;

//            }
//        }

//        private ModuleStatus _Status;
//        public ModuleStatus Status
//        {
//            set
//            {
//                _Status = value;

//                switch (value)
//                {
//                    case ModuleStatus.Add:
//                        btn_Add.Visible = false;
//                        btn_Edit.Visible = false;
//                        btn_DoSure.Visible = false;
//                        btn_Aduit.Visible = false;
//                        btn_Del.Visible = false;
//                        btn_Save.Visible = true;

//                        InitAddStatus();
//                        break;
//                    case ModuleStatus.Edit:
//                        btn_Add.Visible = false;
//                        btn_Edit.Visible = false;
//                        btn_DoSure.Visible = false;
//                        btn_Aduit.Visible = false;
//                        btn_Del.Visible = true;
//                        btn_Save.Visible = true;

//                        InitEditStatus();
//                        break;

//                    case ModuleStatus.See:
//                        btn_Add.Visible = true;
//                        btn_Edit.Visible = true;
//                        btn_DoSure.Visible = true;
//                        btn_Aduit.Visible = true;
//                        btn_Del.Visible = true;
//                        btn_Save.Visible = false;
//                        foreach (string key in HControls.Keys)
//                        {
//                            UCModuleControl control = HControls[key];
//                            if (control.IsSysField)
//                            {
//                                control.ReadOnly = true;
//                            }
//                            else
//                            {
//                                control.ReadOnly = true;
//                            }
//                        }

//                        foreach (string key in BControls.Keys)
//                        {
//                            UCModuleControl control = BControls[key];
//                            control.ReadOnly = true;
//                        }

//                        if (_HasStatus)
//                        {
//                            GetDocStatus();
//                        }

//                        break;
//                }



//            }
//            get { return _Status; }

//        }

//        private void GetDocStatus()
//        {
//           try
//            {
//                DataStatus = ModuleHelper.GetDocStatus(ModuleCode, DataId, Client);



//            }
//            catch(Exception ex)
//            {
//                MessageHelper.ShowErr(this.FindForm(),ex.Message) ;
//            }
//        }

//        private void InitEditStatus()
//        {

//            foreach (string key in HControls.Keys)
//            {
//                UCModuleControl control = HControls[key];
//                if (control.IsSysField)
//                {
//                    control.ReadOnly = true;
//                    control.Visible = false;
//                }
//                else
//                {
//                    control.ReadOnly = !control.IsEdit;
//                }
//            }

//            foreach (string key in BControls.Keys)
//            {
//                UCModuleControl control = BControls[key];
//                control.ReadOnly = false;
//            }

//        }

//        private void UpdateData()
//        {
//            try
//            {
//                #region 更新头数据
//                if (HData.Rows.Count > 0)
//                {
//                    foreach (string key in HControls.Keys)
//                    {
//                        UCModuleControl control = HControls[key];
//                        if (control.DataType != UCModuleControl.ControlDataType.Bool)
//                        {
//                            control.Value = HData.Rows[0][control.Prop].ToString();
//                        }
//                        else
//                        {
//                            control.Value = Convert.ToBoolean(HData.Rows[0][control.Prop].ToString());
//                        }
//                    }
//                }
//                else
//                {
//                    foreach (string key in HControls.Keys)
//                    {
//                        UCModuleControl control = HControls[key];
//                        if (control.DataType != UCModuleControl.ControlDataType.Bool)
//                        {
//                            control.Value = string.Empty;
//                        }
//                        else
//                        {
//                            control.Value = false;
//                        }
//                    }
//                }
//                #endregion

//                #region 更新身数据
//                foreach (string key in BControls.Keys)
//                {
//                    UCModuleDataBody control = BControls[key] as UCModuleDataBody;
//                    if (BData.ContainsKey(control.ControlConfig.Title))
//                    {
//                        control.BData = BData[control.ControlConfig.Title];
//                    }
//                    else
//                    {
//                        control.BData = new DataTable();
//                    } 
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }
//        }

//        private void InitAddStatus()
//        {
//            this.DataId = string.Empty;
//            foreach (string key in HControls.Keys)
//            {
//                UCModuleControl control = HControls[key];
//                if (control.IsSysField)
//                {
//                    control.ReadOnly = true;
//                    control.Visible = false;
//                }
//                else
//                {
//                    control.ReadOnly = !control.IsAdd;
//                }
//            }

//            foreach (string key in BControls.Keys)
//            {
//                UCModuleControl control = BControls[key];
//                control.ReadOnly = true;
//            }


//            LoadData();
//            UpdateData();
//        }

//        private string _DataId;

//        public string DataId
//        {
//            get
//            {
//                return _DataId;
//            }
//            set
//            {
//                _DataId = value;
//                if(!string.IsNullOrEmpty(value))
//                {
//                    btn_Del.Visible = true;
//                }
//                else
//                {
//                    btn_Del.Visible = false;
//                }
//            }
//        }

//        private string _ModuleCode;

//        public string ModuleCode
//        {
//            get
//            {
//                return _ModuleCode;
//            }
//            set
//            {
//                _ModuleCode = value;
//            }
//        }

//        private SJeMES_Framework.Class.ClientClass _Client;

//        public SJeMES_Framework.Class.ClientClass Client
//        {
//            get
//            {
//                return _Client;
//            }
//            set
//            {
//                _Client = value;

//                if (value.Language != "cn")
//                {
//                    string sql = @"
//SELECT 
//ui_tittle AS '功能名称',
//ui_code AS '控件ID',
//ui_cn AS '控件名称',
//ui_en AS '英语名称',
//ui_yn AS '粤语名称'
//FROM SJQDMS_UILAN where ui_tittle='all' and ui_cn='" + label2.Text + "'";
//                    DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(_Client.WebServiceUrl, sql, new Dictionary<string, string>());
//                    if(dt!=null && dt.Rows.Count>0)
//                    {
//                        if (value.Language == "en")
//                            label2.Text = !string.IsNullOrEmpty(dt.Rows[0]["英语名称"].ToString()) ? dt.Rows[0]["英语名称"].ToString() : label2.Text;
//                        else
//                            label2.Text = !string.IsNullOrEmpty(dt.Rows[0]["粤语名称"].ToString()) ? dt.Rows[0]["粤语名称"].ToString() : label2.Text;
//                    }

//                }
//            }
//        }
//        #endregion


//        //定义委托
//        public delegate void BackHandle(object sender, EventArgs e);
//        //定义事件
//        public event BackHandle Back;

//        public UCModuleBase(string ModuleCode,string DataId,SJeMES_Framework.Class.ClientClass Client,string title)
//        {
//            InitializeComponent();
//            HControls = new Dictionary<string, UCModuleControl>();
//            BControls = new Dictionary<string, UCModuleControl>();
//            this.ModuleCode = ModuleCode;
//            this.title = title;
//            this.Client = Client;
//            this.DataId = DataId;
//            LoadData();
//            //if (ucCombox1.Source.Count > 0)
//            //{
//            //    ucCombox1.SelectedIndex = 0;
//            //}

//        }

//        public UCModuleBase(SJeMES_Framework.Web.JSONFormClass ModuleConfig, string DataId, SJeMES_Framework.Class.ClientClass Client,string title)
//        {
//            InitializeComponent();
//            HControls = new Dictionary<string, UCModuleControl>();
//            BControls = new Dictionary<string, UCModuleControl>();
//            if (ModuleConfig.APPCode.Contains("PC_"))
//            {
//                this.ModuleCode= ModuleConfig.APPCode;
//            }
//            else
//            {
//                this.ModuleCode = "PC_" + ModuleConfig.APPCode;
//            }
//            //if (string.IsNullOrEmpty(ModuleConfig.APPCode))
//            //{
//            //    MessageHelper.ShowErr(this.FindForm(), "未找到对应的模块编码，请确认是否保存配置！");
//            //    return;
//            //}
//            this.title = title;
//            this.ModuleConfig = ModuleConfig;
//            this.Client = Client;
//            this.DataId = DataId;
//            LoadData();


//        }

//        private void btn_Back_BtnClick(object sender, EventArgs e)
//        {
//            if (Back != null)
//                Back(this, new EventArgs());
//        }



//        private void LoadContorls()
//        {

//            try
//            {
//                //获取表头表身权限
//                //UserPowser = new Dictionary<string, List<string>>();



//                LoadHControls();
//                LoadBControls();
//            }
//            catch(Exception ex)
//            {
//                MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }
//        }

//        private void LoadBControls()
//        {
//            tab_Body.TabPages.Clear();
//            BControls = new Dictionary<string, UCModuleControl>();
//            if(ModuleConfig.PanelB!=null)
//            {
//                List<string> lstKeys = new List<string>();
//                foreach (SJeMES_Framework.Web.JSONPanelClassB b in ModuleConfig.PanelB)
//                {
//                    lstKeys.Add(b.Title);
//                }
//                if(lstKeys.Count>0)
//                {
//                    Dictionary<string, object> dic = SJeMES_Framework.Common.UIHelper.UIListMsg(lstKeys, Client, Client.WebServiceUrl, Client.Language);
//                    if(dic.Count>0)
//                    {
//                        foreach (SJeMES_Framework.Web.JSONPanelClassB b in ModuleConfig.PanelB)
//                        {
//                            string strText = b.Title;
//                            if (dic.ContainsKey(b.Title))
//                                strText = dic[b.Title].ToString();

//                            TabPage tp = new TabPage(strText);
//                            UCModuleDataBody control = new UCModuleDataBody(b, this.DataId, this.ModuleCode, this.Client);

//                            BControls.Add(strText, control);
//                            control.Dock = DockStyle.Fill;
//                            control.ReadLoad += BControl_ReadLoad;
//                            if (BData.ContainsKey(strText))
//                            {
//                                control.BData = BData[strText];
//                            }
//                            tp.Controls.Add(control);

//                            tab_Body.TabPages.Add(tp);
//                        }
//                    }
//                } 
//            }
//        }

//        private void BControl_ReadLoad(object sender, EventArgs e)
//        {
//            try
//            {
//                UCModuleDataBody control = sender as UCModuleDataBody;

//                Dictionary<string, object> p = ModuleHelper.GetBData(ModuleCode, control.ControlConfig.seq, control.ControlConfig.table, DataId, Client);
//                DataTable dt = p["Data"] as DataTable;
//               // MessageBox.Show(dt.Rows.Count.ToString());
//                if (dt == null)
//                {
//                    dt = new DataTable();
//                    List<string> Heads = p["Heads"] as List<string>;
//                    foreach (string s in Heads)
//                    {
//                        dt.Columns.Add(s);
//                    }
//                }

//                BData[control.ControlConfig.Title] = dt;
//                UpdateData();

//            }
//            catch(Exception ex)
//            {
//                MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }

//        }

//        private void ResetLayout()
//        {
//            panel_Head.Controls.Clear();

//            int i = 0;
//            int k = 0;
//            int RowMax = 4;
//            if (this.FindForm().Width > 1900)
//            {
//                RowMax = 6;
//            }
//            int Height = 0;
//            foreach (string key in HControls.Keys)
//            {
//                UCModuleControl control = HControls[key];
//                control.Top = 55 * (i / RowMax);
//                control.Left = 250 * k + (10 * k);

//                this.panel_Head.Controls.Add(control);

//                i++;
//                k++;
//                if (k == RowMax)
//                {
//                    k = 0;
//                }
//                Height = control.Top + 60;
//            }
//            if (Height < 200) Height = 200;
//            panel1.Height = Height;
//        }

//        private string checkName(DataTable dt, string name)
//        {
//            string columnName = "TableName", columnName1 = "ColumnName";

//            if (dt.Rows.Count > 0 && Client.Language != "cn")
//            {
//                DataRow[] dataRows_LAN = dt.Select(columnName1 + "='" + name + "' and " + columnName + "='" + ModuleConfig.PanelH.table + "'");
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

//        private void LoadHControls()
//        {
//            _HasStatus = false;
//            HControls = new Dictionary<string, UCModuleControl>();

//            string sql = @"select * from (select a.UserCode,b.AppCode,b.TableName,
//b.ColumnName,b.ColumnID from SYSROLE01A1 a
//left join SYSROLE01M c on a.Role_Name = c.Role_Name
//left join SYSPOWER_R b on c.Role_No = b.Role_No)a where UserCode='" + Client.UserCode + "' and AppCode='" + ModuleCode.Remove(0, 3) + "'";
//            DataTable dtPow = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
//            string columnName = "TableName", columnName1 = "ColumnID";

//            string sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode.Remove(0, 3) + "'";
//            //string sql1 = "select * from SYSLANGUAGE where TableName='" + ModuleCode.Remove(0, 3) + "'";
//            //DataTable dt = Client.GetDT(sql);

//            DataTable dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
//            if (dtPow.Rows.Count == 0)
//            {
//                sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode + "'";
//                dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
//            }

//            if (ModuleConfig.PanelH != null)
//            {
//                if (dtPow.Rows.Count == 0)
//                {
//                    foreach (SJeMES_Framework.Web.JSONControlH c in ModuleConfig.PanelH.formData)
//                    {
//                        c.headId = this.DataId;
//                        if (c.name.ToLower() == "status")
//                        {
//                            _HasStatus = true;
//                        }

//                        if (c.type == "text" || c.type == "other")
//                        {
//                            UCModuleTextBox control = new UCModuleTextBox();
//                            control.InitControl(c);
//                            control.Client = this.Client;
//                            control.SelectedData += Control_SelectedData;
//                            if (HData.Rows.Count > 0)
//                                control.Value = HData.Rows[0][control.Prop].ToString();
//                            control.Title = checkName(dtLAN, control.Title);
//                            HControls.Add(control.Title, control);
//                        }
//                        else if (c.type == "select")
//                        {
//                            UCModuleComBox control = new UCModuleComBox();
//                            control.InitControl(c);
//                            if (HData.Rows.Count > 0)
//                                control.Value = HData.Rows[0][control.Prop].ToString();
//                            control.Title = checkName(dtLAN, control.Title);
//                            HControls.Add(control.Title, control);
//                        }

//                        else if (c.type == "datePicker")
//                        {
//                            UCModuleDateTime control = new UCModuleDateTime();
//                            control.InitControl(c);
//                            if (HData.Rows.Count > 0)
//                                control.Value = HData.Rows[0][control.Prop].ToString();
//                            control.Title = checkName(dtLAN, control.Title);
//                            HControls.Add(control.Title, control);
//                        }

//                        else if (c.type == "switch")
//                        {
//                            UCModuleSwitch control = new UCModuleSwitch();
//                            control.InitControl(c);
//                            if (HData.Rows.Count > 0)
//                                control.Value = HData.Rows[0][control.Prop].ToString();
//                            control.Title = checkName(dtLAN, control.Title);
//                            HControls.Add(control.Title, control);
//                        }
//                    }
//                }
//                else
//                {
//                    foreach (SJeMES_Framework.Web.JSONControlH c in ModuleConfig.PanelH.formData)
//                    {
//                        c.headId = this.DataId;
//                        DataRow[] dataRows1 = dtPow.Select(columnName1 + "='" + c.name + "' and " + columnName + "='" + ModuleConfig.PanelH.table + "'");

//                        if (dataRows1.Length > 0)
//                        {
//                            if (c.name.ToLower() == "status")
//                            {
//                                _HasStatus = true;
//                            }

//                            if (c.type == "text" || c.type == "other")
//                            {
//                                UCModuleTextBox control = new UCModuleTextBox();
//                                control.InitControl(c);
//                                control.Client = this.Client;
//                                control.SelectedData += Control_SelectedData;

//                                if (HData.Rows.Count > 0)
//                                    control.Value = HData.Rows[0][control.Prop].ToString();

//                                control.Title = checkName(dtLAN, control.Title);

//                                HControls.Add(control.Title, control);
//                            }
//                            else if (c.type == "select")
//                            {
//                                UCModuleComBox control = new UCModuleComBox();
//                                control.InitControl(c);
//                                if (HData.Rows.Count > 0)
//                                    control.Value = HData.Rows[0][control.Prop].ToString();
//                                control.Title = checkName(dtLAN, control.Title);

//                                HControls.Add(control.Title, control);
//                            }

//                            else if (c.type == "datePicker")
//                            {
//                                UCModuleDateTime control = new UCModuleDateTime();
//                                control.InitControl(c);
//                                if (HData.Rows.Count > 0)
//                                    control.Value = HData.Rows[0][control.Prop].ToString();
//                                control.Title = checkName(dtLAN, control.Title);

//                                HControls.Add(control.Title, control);
//                            }

//                            else if (c.type == "switch")
//                            {
//                                UCModuleSwitch control = new UCModuleSwitch();
//                                control.InitControl(c);
//                                if (HData.Rows.Count > 0)
//                                    control.Value = HData.Rows[0][control.Prop].ToString();
//                                control.Title = checkName(dtLAN, control.Title);

//                                HControls.Add(control.Title, control);
//                            }
//                        }
//                    }
//                } 
//            }

//            ResetLayout();
//        }

//        /// <summary>
//        /// 添加了中文列名转换成对应语言列名的判断
//        /// david
//        /// 2023.2.13
//        /// </summary>
//        /// <param name="dtSelected"></param>
//        private void Control_SelectedData(DataTable dtSelected)
//        {
//            try
//            {
//                foreach(DataColumn dc in dtSelected.Columns)
//                {
//                    string keyId = "R";
//                    if (dc.ColumnName != "R")
//                    {
//                        keyId = checkName(dtLAN, dc.ColumnName);
//                    }
//                    foreach (string key in HControls.Keys)
//                    {
//                        if(key == keyId)
//                        {
//                            HControls[key].Value = dtSelected.Rows[0][dc.ColumnName].ToString();
//                        }
//                    }
//                }
//            }
//            catch(Exception ex)
//            {
//                MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }
//        }

//        private void LoadData()
//        {
//            try
//            {
//                string sql = @"
//SELECT
//a.AppCode AS '模块代号',
//a.AppName AS '模块名称',
//'False' AS '全部权限',
//ISNULL([Select],'False') AS '查看数据',
//ISNULL([Add],'False') AS '添加数据',
//ISNULL([Edit],'False') AS '修改数据',
//ISNULL([Delete],'False') AS '删除数据',
//ISNULL(DoSure ,'False') AS '确认操作',
//ISNULL(Audit ,'False') AS '审核操作',
//ISNULL(DoWork ,'False') AS '其他操作',
//ISNULL([Print] ,'False') AS '打印',
//ISNULL(Fun ,'False') AS '更多功能'
//FROM SYSAPP03M a
//LEFT JOIN (select a.UserCode,b.AppCode,
//[Select],[Add],Edit,[Delete],
//DoSure,Audit,DoWork,[Print],Fun from SYSROLE01A1 a
//left join SYSROLE02M b on a.Role_Name=b.Role_Name) b ON a.AppCode = b.AppCode
//where a.AppName in(select menuname from SYSPOWER 
//where  UserCode='" + Client.UserCode + "') and UserCode= '" + Client.UserCode + "' and a.AppCode='" + ModuleCode.Remove(0, 3) + "'";
//                //DataTable dt = Client.GetDT(sql);

//                DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
//                if (dt!=null && dt.Rows.Count > 0)
//                {
//                    if (dt.Rows[0]["添加数据"].ToString().Trim() == "False")
//                    {
//                        btn_Add.Enabled = false;
//                    }
//                    if (dt.Rows[0]["修改数据"].ToString().Trim() == "False")
//                    {
//                        //ucBtnImg5.Visible = false;
//                        btn_Edit.Enabled = false;

//                    }
//                    if (dt.Rows[0]["删除数据"].ToString().Trim() == "False")
//                    {
//                        //ucBtnImg3.Visible = false;
//                        btn_Del.Enabled = false;

//                    }
//                    if (dt.Rows[0]["确认操作"].ToString().Trim() == "False")
//                    {
//                        btn_DoSure.Enabled = false;
//                    }
//                    if (dt.Rows[0]["审核操作"].ToString().Trim() == "False")
//                    {
//                        btn_Aduit.Enabled = false;
//                    }
//                    if (dt.Rows[0]["更多功能"].ToString().Trim() == "False")
//                    {
//                        ucCombox1.Enabled = false;
//                    }
//                }


//                if (ModuleConfig == null)
//                {
//                    Dictionary<string, object> Config = ModuleHelper.GetModuleConfig(ModuleCode, Client);

//                    ModuleConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<SJeMES_Framework.Web.JSONFormClass>(Config["App_Json"].ToString());

//                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Config["OtherMenu"].ToString());


//                    //ModuleConfig.PanelH.formData;//表头字段

//                    //foreach(SJeMES_Framework.Web.JSONPanelClassB panelb in ModuleConfig.PanelB)
//                    //{
//                    //    panelb.Title;
//                    //    panelb.HeadKeys;//表身字段
//                    //}

//                    OtherMenus = new Dictionary<string, OtherMenu>();
//                    if (dt != null)
//                    {
//                        foreach (DataRow dr in dt.Rows)
//                        {
//                            OtherMenu m = new OtherMenu();
//                            m.Title = dr["Title"].ToString();
//                            m.Action = dr["Action"].ToString();
//                            m.Url = dr["Url"].ToString();
//                            m.DllName = dr["DllName"].ToString();
//                            m.ClassName = dr["ClassName"].ToString();
//                            m.Method = dr["Method"].ToString();
//                            m.Parameters = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,string>>(dr["Parameters"].ToString());
//                            OtherMenus.Add(m.Title, m);
//                        }
//                        LoadOtherMenu();
//                    }
//                }

//                HData = new DataTable();
//                BData = new Dictionary<string, DataTable>();
//                if (!string.IsNullOrEmpty(DataId))
//                {
//                    LoadHeadData();
//                    LoadBodyData();

//                }
//            }
//            catch (Exception ex)
//            {
//                MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }
//        }

//        private void LoadOtherMenu()
//        {
//            ucCombox1.Visible = true;
//            List<KeyValuePair<string, string>> lisCom = new List<KeyValuePair<string, string>>();
//            lisCom.Add(new KeyValuePair<string, string>("More", "More"));

//            List<string> lstKeys = new List<string>();
//            foreach (string key in OtherMenus.Keys)
//            {
//                lstKeys.Add(key);
//            }
//            if(lstKeys.Count>0)
//            {
//                Dictionary<string, object> dic = SJeMES_Framework.Common.UIHelper.UIListMsg(lstKeys, Client, Client.WebServiceUrl, Client.Language);
//                if (dic.Count > 0)
//                {
//                    foreach (string key in OtherMenus.Keys)
//                    {
//                        if (dic.ContainsKey(key))
//                            lisCom.Add(new KeyValuePair<string, string>(key, dic[key].ToString()));
//                        else
//                            lisCom.Add(new KeyValuePair<string, string>(key, key));
//                    }
//                }
//            }
//            ucCombox1.Source = lisCom;

//        }

//        private void LoadBodyData()
//        {
//            string sql = @"select * from (select a.UserCode,b.AppCode,b.TableName,
//b.ColumnName,b.ColumnID from SYSROLE01A1 a
//left join SYSROLE01M c on a.Role_Name=c.Role_Name
//left join SYSPOWER_R b on c.Role_No=b.Role_No)a where UserCode='" + Client.UserCode + "' and AppCode='" + ModuleCode.Remove(0, 3) + "'";
//            DataTable dtPow = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
//            string columnName = "TableName", columnName1 = "ColumnName";
//            BData = new Dictionary<string, DataTable>();

//            if (dtPow.Rows.Count == 0)
//            {
//                foreach (SJeMES_Framework.Web.JSONPanelClassB b in ModuleConfig.PanelB)
//                {
//                    Dictionary<string, object> p = ModuleHelper.GetBData(ModuleCode, b.seq, b.table, DataId, Client);
//                    DataTable dt = p["Data"] as DataTable;
//                    if (dt.Rows.Count == 0)
//                    {
//                        dt = new DataTable();
//                        List<string> Heads = p["Heads"] as List<string>;
//                        foreach (string s in Heads)
//                        {
//                            dt.Columns.Add(s);
//                        }
//                    }

//                    BData.Add(b.Title, dt);

//                }
//            }
//            else {
//                foreach (SJeMES_Framework.Web.JSONPanelClassB b in ModuleConfig.PanelB)
//                {
//                    DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
//                    DataRow[] dataRows = dtPow.Select(columnName + "='" + b.table + "'");

//                    if (dataRows.Length > 0)
//                    {
//                        Dictionary<string, object> p = ModuleHelper.GetBData(ModuleCode, b.seq, b.table, DataId, Client);
//                        DataTable dt = p["Data"] as DataTable;
//                        if (dt.Rows.Count == 0)
//                        {
//                            dt = new DataTable();
//                            List<string> Heads = p["Heads"] as List<string>;
//                            foreach (string s in Heads)
//                            {
//                                DataRow[] dataRows1 = dtPow.Select(columnName1 + "='" + s + "'");
//                                if (dataRows1.Length > 0) dt.Columns.Add(s);
//                            }
//                        }

//                        BData.Add(b.Title, dt);
//                    }
//                }
//            }


//        }

//        private void LoadHeadData()
//        {
//            HData = ModuleHelper.GetHData(ModuleCode, ModuleConfig.PanelH.table, DataId,title,Client);
//            HData = ModuleHelper.GetHData(ModuleCode, ModuleConfig.PanelH.table, DataId,title,Client);
//        }

//        private void btn_Add_BtnClick(object sender, EventArgs e)
//        {
//            this.Status = ModuleStatus.Add;
//        }

//        private void btn_Del_BtnClick(object sender, EventArgs e)
//        {
//            try
//            {

//                if (SJeMES_Control_Library.MessageHelper.ShowWarning(this.FindForm(), "是否确认删除数据？") == DialogResult.OK)
//                {



//                    DataTable dt = new DataTable();
//                    dt.Columns.Add("TableName");
//                    dt.Columns.Add("Id");

//                    DataRow dr = dt.NewRow();
//                    dr["TableName"] = ModuleConfig.PanelH.table;
//                    dr["Id"] = DataId;                                             
//                    dt.Rows.Add(dr);

//                    if (ModuleHelper.DelData(dt, Client))
//                    {
//                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this.FindForm(), "删除数据成功");
//                        btn_Back_BtnClick(this, new EventArgs());
//                    }
//                }

//            }
//            catch (Exception ex)
//            {
//                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }
//        }

//        private void btn_Edit_BtnClick(object sender, EventArgs e)
//        {
//            this.Status = ModuleStatus.Edit;
//        }

//        private void btn_DoSure_BtnClick(object sender, EventArgs e)
//        {
//            if(DataStatus == "8")
//            {
//                if(MessageHelper.ShowWarning(this.FindForm(),"是否确认单据？") == DialogResult.OK)
//                {
//                    DataStatus = ModuleHelper.DocDoSure(ModuleCode, DataId, true, Client);
//                    LoadData();
//                    UpdateData();
//                    MessageHelper.ShowSuccess(this.FindForm(), "确认单据成功");
//                }
//            }
//            else if (DataStatus == "7" || DataStatus == "1")
//            {
//                if (MessageHelper.ShowWarning(this.FindForm(), "是否取消确认单据？") == DialogResult.OK)
//                {
//                    DataStatus = ModuleHelper.DocDoSure(ModuleCode, DataId, false, Client);
//                    LoadData();
//                    UpdateData();
//                    MessageHelper.ShowSuccess(this.FindForm(), "取消确认单据成功");
//                }
//            }
//        }

//        private void btn_Aduit_BtnClick(object sender, EventArgs e)
//        {
//            if (DataStatus == "1" || DataStatus == "7")
//            {
//                if (MessageHelper.ShowWarning(this.FindForm(), "是否审核单据？") == DialogResult.OK)
//                {
//                    DataStatus = ModuleHelper.DocAudit(ModuleCode, DataId, true, Client);
//                    LoadData();
//                    UpdateData();
//                    MessageHelper.ShowSuccess(this.FindForm(), "审核单据成功");
//                }
//            }
//            else if (DataStatus == "2")
//            {
//                if (MessageHelper.ShowWarning(this.FindForm(), "是否取消审核单据？") == DialogResult.OK)
//                {
//                    DataStatus = ModuleHelper.DocAudit(ModuleCode, DataId, false, Client);
//                    LoadData();
//                    UpdateData();
//                    MessageHelper.ShowSuccess(this.FindForm(), "取消审核单据成功");
//                }
//            }
//        }

//        private void btn_Save_BtnClick(object sender, EventArgs e)
//        {
//            try
//            {
//                if (CheckData())
//                {

//                    string TableName = ModuleConfig.PanelH.table;
//                    string AppCode = ModuleCode;

//                    Dictionary<string, string> TableData = new Dictionary<string, string>();
//                    List<Dictionary<string, object>> RowData = new List<Dictionary<string, object>>();
//                    Dictionary<string, object> Row = new Dictionary<string, object>();
//                    if (!string.IsNullOrEmpty(DataId))
//                    {
//                        Row.Add("id", DataId);
//                    }
//                    foreach (string key in HControls.Keys)
//                    {
//                        UCModuleControl control = HControls[key];
//                        if (!control.IsSysField)
//                        {
//                            if (!Row.ContainsKey(control.Prop))
//                                //MessageBox.Show(control.Prop+":"+ control.Value);
//                            Row.Add(control.Prop, control.Value);
//                        }
//                    }

//                    RowData.Add(Row);
//                    TableData.Add(TableName, Newtonsoft.Json.JsonConvert.SerializeObject(RowData));

//                    if (!string.IsNullOrEmpty(DataId))
//                    {
//                        if (ModuleHelper.EditHData(AppCode, TableName, TableData, Client))
//                        {
//                            LoadData();
//                            UpdateData();
//                            MessageHelper.ShowSuccess(this.FindForm(), "修改数据成功");
//                        }
//                    }
//                    else
//                    {
//                        DataId = ModuleHelper.AddHData(AppCode, TableName, TableData, Client);

//                        foreach(string key in BControls.Keys)
//                        {
//                            ((UCModuleDataBody)BControls[key]).HeadId = DataId;
//                        }
//                        LoadData();
//                        UpdateData();
//                        Status = ModuleStatus.Edit;
//                        MessageHelper.ShowSuccess(this.FindForm(), "添加数据成功");

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

//                if (!control.IsNull && string.IsNullOrEmpty(control.Value.ToString().Trim()))
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

//        private void UCModuleBase_Load(object sender, EventArgs e)
//        {
//            LoadContorls();
//            this.Status = Status;
//            if (ucCombox1.Source!=null)
//            {
//                if (ucCombox1.Source.Count > 0)
//                {
//                    ucCombox1.SelectedIndex = 0;
//                }
//            }

//        }

//        private void UCModuleBase_SizeChanged(object sender, EventArgs e)
//        {
//            ResetLayout();
//        }

//        private void ucCombox1_SelectedChangedEvent(object sender, EventArgs e)
//        {
//            if(ucCombox1.SelectedIndex!=0)
//            {
//                DoOtherWork();
//                ucCombox1.SelectedIndex = 0;
//            }
//        }

//        private void DoOtherWork()
//        {
//            try
//            {
//                Dictionary<string, object> P = new Dictionary<string, object>();
//                Dictionary<string, string> P2 = new Dictionary<string, string>();
//                OtherMenu fbc = new OtherMenu();
//                string tmp = string.Empty;
//                string tmp2 = string.Empty;
//                foreach (string key in  OtherMenus.Keys)
//                {
//                    if(key == ucCombox1.SelectedValue)
//                    {
//                        fbc = OtherMenus[key];
//                        break;
//                    }
//                }
//                switch (fbc.Action)
//                {
//                    case "AccessWeb":
//                        System.Diagnostics.Process.Start(fbc.Url);
//                        break;
//                    case "RunApp":
//                        foreach (string s in fbc.Parameters.Keys)
//                        {
//                            if (fbc.Parameters[s].StartsWith("HeadData."))
//                            {
//                                try
//                                {
//                                    P.Add(s, HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString());
//                                }
//                                catch (Exception ex)
//                                {

//                                    MessageBox.Show("没有数据，不能进行该操作！");
//                                    return;
//                                }

//                            }
//                            else
//                            {
//                                P.Add(s, fbc.Parameters[s]);
//                            }
//                        }

//                        P.Add("Org", Client.Org.Org);
//                        P.Add("OrgName", Client.Org.OrgName);
//                        P.Add("DBServer", Client.Org.DBServer);
//                        P.Add("DBType", Client.Org.DBType);
//                        P.Add("DBName", Client.Org.DBName);
//                        P.Add("DBUser", Client.Org.DBUser);
//                        P.Add("DBPassword", Client.Org.DBPassword);
//                        P.Add("IsMaxWindow", false);
//                        P.Add("WebServiceUrl", Client.WebServiceUrl);
//                        P.Add("User", Client.UserCode);


//                        SJeMES_Framework.Common.OtherPrograms.RunApp(fbc.DllName, fbc.ClassName, fbc.Method, P);
//                        return;
//                    case "RunFastReport":
//                        Dictionary<string, string> dic = new Dictionary<string, string>();
//                        foreach (string s in fbc.Parameters.Keys)
//                        {
//                            if (fbc.Parameters[s].StartsWith("HeadData."))
//                            {
//                                try
//                                {
//                                    dic.Add(s, HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString() + "*" + fbc.Parameters[s].Replace("HeadData.", ""));
//                                }
//                                catch (Exception ex)
//                                {
//                                    MessageBox.Show("没有数据，不能进行该操作！");
//                                    return;
//                                }

//                            }
//                            else
//                            {
//                                dic.Add(s, fbc.Parameters[s]);
//                            }


//                        }
//                        SJeMES_Control_Library.Forms.frmFastReport frmFR = new SJeMES_Control_Library.Forms.frmFastReport(Client.Org, Client.WebServiceUrl, dic);
//                        frmFR.Show();
//                        return;
//                    case "PrintFastReport":
//                        string docNo = string.Empty;
//                        string moduleNo = string.Empty;
//                        string headDataKey = string.Empty;
//                        foreach (string s in fbc.Parameters.Keys)
//                        {
//                            if (fbc.Parameters[s].StartsWith("HeadData."))
//                            {
//                                try
//                                {
//                                    docNo = HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString();
//                                    moduleNo = ModuleConfig.PanelH.table;
//                                    headDataKey = fbc.Parameters[s].Replace("HeadData.", "");
//                                }
//                                catch (Exception ex)
//                                {
//                                    MessageBox.Show("没有数据，不能进行该操作！");
//                                    return;
//                                }

//                            }
//                        }
//                        Forms.FrmReportMain frMain = new Forms.FrmReportMain(docNo, moduleNo, headDataKey,this.Client);
//                        frMain.ShowDialog();
//                        return;
//                    case "RunService":
//                        if (HData.Rows[0] == null)
//                            return;
//                        foreach (string s in fbc.Parameters.Keys)
//                        {
//                            if (fbc.Parameters[s].StartsWith("HeadData."))
//                            {
//                                P2.Add(s, HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString());
//                            }
//                            else
//                            {
//                                P2.Add(s, fbc.Parameters[s]);
//                            }
//                        }

//                        P2.Add("UserCode", Client.Org.User.UserCode);


//                        string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Client.Org, Client.WebServiceUrl, fbc.DllName, fbc.ClassName, fbc.Method, P2);
//                        if (Convert.ToBoolean(SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
//                        {

//                            MessageBox.Show("操作成功");
//                            LoadData();
//                            UpdateData();

//                        }

//                        else
//                        {
//                            MessageBox.Show(SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
//                        }
//                        return;
//                    case "PrintBarCode":

//                        foreach (string s in fbc.Parameters.Keys)
//                        {
//                            if (fbc.Parameters[s].StartsWith("HeadData."))
//                            {
//                                tmp = HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString();
//                                tmp2 = "@" + fbc.Parameters[s].Replace("HeadData.", "");
//                            }

//                        }

//                        string sql = fbc.Parameters["SQL"];

//                        sql = sql.Replace(tmp2, tmp);
//                        //判断数据库是否有存储过程
//                        if (SJeMES_Framework.Common.WebServiceHelper.GetDataTable(Client.Org, Client.WebServiceUrl,
//                           "select * from dbo.sysobjects t where t.name='sp_PowerWarehouse'", new Dictionary<string, string>()).Rows.Count > 0)
//                        {
//                            sql = "exec sp_PowerWarehouse '" + sql.Replace("'", "''") + "','" + Client.Org.User.UserCode + "','material_no',''";
//                        }
//                        System.Data.DataTable dt = SJeMES_Framework.Common.WebServiceHelper.GetDataTable(Client.Org, Client.WebServiceUrl, sql, new Dictionary<string, string>());

//                        //List<string> Data = new List<string>();
//                        //foreach (System.Data.DataRow dr in dt.Rows)
//                        //{
//                        //    Data.Add(dr[0].ToString());
//                        //}

//                        SJeMES_Control_Library.Forms.frmBarCodePrinter frm = new SJeMES_Control_Library.Forms.frmBarCodePrinter(Client, Client.WebServiceUrl, dt, fbc.Title);
//                        frm.ShowDialog();
//                        return;
//                    case "PrintBarCode2":

//                        foreach (string s in fbc.Parameters.Keys)
//                        {
//                            if (fbc.Parameters[s].StartsWith("HeadData."))
//                            {
//                                tmp = HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString();
//                                tmp2 = "@" + fbc.Parameters[s].Replace("HeadData.", "");
//                            }

//                        }

//                        sql = fbc.Parameters["SQL"];

//                        sql = sql.Replace(tmp2, tmp);
//                        string sql1 = sql;
//                        //判断数据库是否有存储过程
//                        if (SJeMES_Framework.Common.WebServiceHelper.GetDataTable(Client.Org, Client.WebServiceUrl,
//                           "select * from dbo.sysobjects t where t.name='sp_PowerWarehouse'", new Dictionary<string, string>()).Rows.Count > 0)
//                        {
//                            sql = "exec sp_PowerWarehouse '" + sql.Replace("'", "''") + "','" + Client.Org.User.UserCode + "','material_no',''";
//                        }
//                        dt = SJeMES_Framework.Common.WebServiceHelper.GetDataTable(Client.Org, Client.WebServiceUrl, sql, new Dictionary<string, string>());

//                        List<string> Data = new List<string>();
//                        foreach (System.Data.DataRow dr in dt.Rows)
//                        {
//                            Data.Add(dr[0].ToString());
//                        }

//                        frm = new Forms.frmBarCodePrinter(Client, Client.WebServiceUrl, sql1, fbc.Title, dt);
//                        frm.ShowDialog();
//                        return;

//                }
//            }
//            catch(Exception ex)
//            {
//                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
//            }
//        }
//    }
//}



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace SJeMES_Control_Library.Controls
{
    public partial class UCModuleBase : UCControlBase, IContainerControl
    {
        public enum ModuleStatus { Add, Edit, See }

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

        Dictionary<string, UCModuleControl> HControls;
        Dictionary<string, UCModuleControl> BControls;
        Dictionary<string, UCModuleControl> HOtherDataControls;

        Dictionary<string, List<string>> UserPowser = new Dictionary<string, List<string>>();


        #region 属性


        private string _DataStatus;
        public string DataStatus
        {
            get { return _DataStatus; }
            set
            {
                _DataStatus = value;
                switch (_DataStatus)
                {
                    case "2":
                        btn_Aduit.FillColor = Color.Green;
                        btn_DoSure.FillColor = Color.Gray;
                        btn_Edit.Visible = btn_Del.Visible = false;
                        break;
                    case "1":
                        btn_Aduit.FillColor = Color.Gray;
                        btn_DoSure.FillColor = Color.Green;
                        btn_Edit.Visible = btn_Del.Visible = false;
                        break;
                    case "7":
                        btn_Aduit.FillColor = Color.Gray;
                        btn_DoSure.FillColor = Color.Green;
                        btn_Edit.Visible = btn_Del.Visible = false;
                        break;
                    case "8":
                        btn_Aduit.FillColor = Color.Gray;
                        btn_DoSure.FillColor = Color.Gray;
                        btn_Edit.Visible = btn_Del.Visible = true;
                        break;
                }
            }
        }


        private bool _HasStatus = false;

        private DataTable _HData;

        public DataTable HData
        {
            set
            {
                _HData = value;
            }
            get { return _HData; }
        }

        private DataTable _dtLAN;
        public DataTable dtLAN
        {
            set
            {
                _dtLAN = value;
            }
            get { return _dtLAN; }
        }

        private Dictionary<string, DataTable> _BData;

        public Dictionary<string, DataTable> BData
        {
            set { _BData = value; }
            get { return _BData; }
        }

        private Dictionary<string, OtherMenu> _OtherMenus;
        public Dictionary<string, OtherMenu> OtherMenus
        {
            set { _OtherMenus = value; }
            get { return _OtherMenus; }
        }


        private SJeMES_Framework.Web.JSONFormClass _ModuleConfig;

        public SJeMES_Framework.Web.JSONFormClass ModuleConfig
        {
            set { _ModuleConfig = value; }
            get { return _ModuleConfig; }
        }
        private string _title;

        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;

            }
        }

        private ModuleStatus _Status;
        public ModuleStatus Status
        {
            set
            {
                _Status = value;

                switch (value)
                {
                    case ModuleStatus.Add:
                        btn_Add.Visible = false;
                        btn_Edit.Visible = false;
                        btn_DoSure.Visible = false;
                        btn_Aduit.Visible = false;
                        btn_Del.Visible = false;
                        btn_Save.Visible = true;

                        InitAddStatus();
                        break;
                    case ModuleStatus.Edit:
                        btn_Add.Visible = false;
                        btn_Edit.Visible = false;
                        btn_DoSure.Visible = false;
                        btn_Aduit.Visible = false;
                        btn_Del.Visible = true;
                        btn_Save.Visible = true;

                        InitEditStatus();
                        break;

                    case ModuleStatus.See:
                        btn_Add.Visible = true;
                        btn_Edit.Visible = true;
                        btn_DoSure.Visible = true;
                        btn_Aduit.Visible = true;
                        btn_Del.Visible = true;
                        btn_Save.Visible = false;
                        foreach (string key in HControls.Keys)
                        {
                            UCModuleControl control = HControls[key];
                            if (control.IsSysField)
                            {
                                control.ReadOnly = true;
                            }
                            else
                            {
                                control.ReadOnly = true;
                            }
                        }

                        foreach (string key in BControls.Keys)
                        {
                            UCModuleControl control = BControls[key];
                            control.ReadOnly = true;
                        }

                        if (_HasStatus)
                        {
                            GetDocStatus();
                        }

                        break;
                }



            }
            get { return _Status; }

        }

        private void GetDocStatus()
        {
            try
            {
                DataStatus = ModuleHelper.GetDocStatus(ModuleCode, DataId, Client);



            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void InitEditStatus()
        {

            foreach (string key in HControls.Keys)
            {
                UCModuleControl control = HControls[key];
                if (control.IsSysField)
                {
                    control.ReadOnly = true;
                    control.Visible = false;
                }
                else
                {
                    control.ReadOnly = !control.IsEdit;
                }
            }

            foreach (string key in BControls.Keys)
            {
                UCModuleControl control = BControls[key];
                control.ReadOnly = false;
            }

        }

        private void UpdateData()
        {
            try
            {
                #region 更新头数据
                if (HData.Rows.Count > 0)
                {
                    foreach (string key in HControls.Keys)
                    {
                        UCModuleControl control = HControls[key];
                        if (control.DataType != UCModuleControl.ControlDataType.Bool)
                        {
                            control.Value = HData.Rows[0][control.Prop].ToString();
                        }
                        else
                        {
                            control.Value = Convert.ToBoolean(HData.Rows[0][control.Prop].ToString());
                        }
                    }
                }
                else
                {
                    foreach (string key in HControls.Keys)
                    {
                        UCModuleControl control = HControls[key];
                        if (control.DataType != UCModuleControl.ControlDataType.Bool)
                        {
                            control.Value = string.Empty;
                        }
                        else
                        {
                            control.Value = false;
                        }
                    }
                }
                #endregion

                #region 更新身数据
                foreach (string key in BControls.Keys)
                {
                    UCModuleDataBody control = BControls[key] as UCModuleDataBody;
                    if (BData.ContainsKey(control.ControlConfig.Title))
                    {
                        control.BData = BData[control.ControlConfig.Title];
                    }
                    else
                    {
                        control.BData = new DataTable();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void InitAddStatus()
        {
            this.DataId = string.Empty;
            foreach (string key in HControls.Keys)
            {
                UCModuleControl control = HControls[key];
                if (control.IsSysField)
                {
                    control.ReadOnly = true;
                    control.Visible = false;
                }
                else
                {
                    control.ReadOnly = !control.IsAdd;
                }
            }

            foreach (string key in BControls.Keys)
            {
                UCModuleControl control = BControls[key];
                control.ReadOnly = true;
            }


            LoadData();
            UpdateData();
        }

        private string _DataId;

        public string DataId
        {
            get
            {
                return _DataId;
            }
            set
            {
                _DataId = value;
                if (!string.IsNullOrEmpty(value))
                {
                    btn_Del.Visible = true;
                }
                else
                {
                    btn_Del.Visible = false;
                }
            }
        }

        private string _ModuleCode;

        public string ModuleCode
        {
            get
            {
                return _ModuleCode;
            }
            set
            {
                _ModuleCode = value;
            }
        }

        private SJeMES_Framework.Class.ClientClass _Client;

        public SJeMES_Framework.Class.ClientClass Client
        {
            get
            {
                return _Client;
            }
            set
            {
                _Client = value;

                if (value.Language != "cn")
                {
                    string sql = @"
SELECT 
ui_tittle AS '功能名称',
ui_code AS '控件ID',
ui_cn AS '控件名称',
ui_en AS '英语名称',
ui_yn AS '粤语名称'
FROM SJQDMS_UILAN where ui_tittle='all' and ui_cn='" + label2.Text + "'";
                    DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(_Client.WebServiceUrl, sql, new Dictionary<string, string>());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (value.Language == "en")
                            label2.Text = !string.IsNullOrEmpty(dt.Rows[0]["英语名称"].ToString()) ? dt.Rows[0]["英语名称"].ToString() : label2.Text;
                        else
                            label2.Text = !string.IsNullOrEmpty(dt.Rows[0]["粤语名称"].ToString()) ? dt.Rows[0]["粤语名称"].ToString() : label2.Text;
                    }

                }
            }
        }
        #endregion


        //定义委托
        public delegate void BackHandle(object sender, EventArgs e);
        //定义事件
        public event BackHandle Back;

        public UCModuleBase(string ModuleCode, string DataId, SJeMES_Framework.Class.ClientClass Client, string title)
        {
            InitializeComponent();
            HControls = new Dictionary<string, UCModuleControl>();
            BControls = new Dictionary<string, UCModuleControl>();
            this.ModuleCode = ModuleCode;
            this.title = title;
            this.Client = Client;
            this.DataId = DataId;
            LoadData();
            //if (ucCombox1.Source.Count > 0)
            //{
            //    ucCombox1.SelectedIndex = 0;
            //}

        }

        public UCModuleBase(SJeMES_Framework.Web.JSONFormClass ModuleConfig, string DataId, SJeMES_Framework.Class.ClientClass Client, string title)
        {
            InitializeComponent();
            HControls = new Dictionary<string, UCModuleControl>();
            BControls = new Dictionary<string, UCModuleControl>();
            if (ModuleConfig.APPCode.Contains("PC_"))
            {
                this.ModuleCode = ModuleConfig.APPCode;
            }
            else
            {
                this.ModuleCode = "PC_" + ModuleConfig.APPCode;
            }
            //if (string.IsNullOrEmpty(ModuleConfig.APPCode))
            //{
            //    MessageHelper.ShowErr(this.FindForm(), "未找到对应的模块编码，请确认是否保存配置！");
            //    return;
            //}
            this.title = title;
            this.ModuleConfig = ModuleConfig;
            this.Client = Client;
            this.DataId = DataId;
            LoadData();


        }

        private void btn_Back_BtnClick(object sender, EventArgs e)
        {
            if (Back != null)
                Back(this, new EventArgs());
        }



        private void LoadContorls()
        {

            try
            {
                //获取表头表身权限
                //UserPowser = new Dictionary<string, List<string>>();



                LoadHControls();
                LoadBControls();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void LoadBControls()
        {
            tab_Body.TabPages.Clear();
            BControls = new Dictionary<string, UCModuleControl>();
            if (ModuleConfig.PanelB != null)
            {
                List<string> lstKeys = new List<string>();
                foreach (SJeMES_Framework.Web.JSONPanelClassB b in ModuleConfig.PanelB)
                {
                    lstKeys.Add(b.Title);
                }
                if (lstKeys.Count > 0)
                {
                    Dictionary<string, object> dic = SJeMES_Framework.Common.UIHelper.UIListMsg(lstKeys, Client, Client.WebServiceUrl, Client.Language);
                    if (dic.Count > 0)
                    {
                        foreach (SJeMES_Framework.Web.JSONPanelClassB b in ModuleConfig.PanelB)
                        {
                            string strText = b.Title;
                            if (dic.ContainsKey(b.Title))
                                strText = dic[b.Title].ToString();

                            TabPage tp = new TabPage(strText);
                            UCModuleDataBody control = new UCModuleDataBody(b, this.DataId, this.ModuleCode, this.Client);

                            BControls.Add(strText, control);
                            control.Dock = DockStyle.Fill;
                            control.ReadLoad += BControl_ReadLoad;
                            if (BData.ContainsKey(strText))
                            {
                                control.BData = BData[strText];
                            }
                            tp.Controls.Add(control);

                            tab_Body.TabPages.Add(tp);
                        }
                    }
                }
            }
        }

        private void BControl_ReadLoad(object sender, EventArgs e)
        {
            try
            {
                UCModuleDataBody control = sender as UCModuleDataBody;

                Dictionary<string, object> p = ModuleHelper.GetBData(ModuleCode, control.ControlConfig.seq, control.ControlConfig.table, DataId, Client);
                DataTable dt = p["Data"] as DataTable;
                // MessageBox.Show(dt.Rows.Count.ToString());
                if (dt == null)
                {
                    dt = new DataTable();
                    List<string> Heads = p["Heads"] as List<string>;
                    foreach (string s in Heads)
                    {
                        dt.Columns.Add(s);
                    }
                }

                BData[control.ControlConfig.Title] = dt;
                UpdateData();

            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }

        }

        private void ResetLayout()
        {
            panel_Head.Controls.Clear();

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
                control.Top = 55 * (i / RowMax);
                control.Left = 250 * k + (10 * k);

                this.panel_Head.Controls.Add(control);

                i++;
                k++;
                if (k == RowMax)
                {
                    k = 0;
                }
                Height = control.Top + 60;
            }
            if (Height < 200) Height = 200;
            panel1.Height = Height;
        }

        private string checkName(DataTable dt, string name)
        {
            string columnName = "TableName", columnName1 = "ColumnName";

            if (dt.Rows.Count > 0 && Client.Language != "cn")
            {
                DataRow[] dataRows_LAN = dt.Select(columnName1 + "='" + name + "' and " + columnName + "='" + ModuleConfig.PanelH.table + "'");
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

        private void LoadHControls()
        {
            _HasStatus = false;
            HControls = new Dictionary<string, UCModuleControl>();

            string sql = @"select * from (select a.UserCode,b.AppCode,b.TableName,
b.ColumnName,b.ColumnID from SYSROLE01A1 a
left join SYSROLE01M c on a.Role_Name = c.Role_Name
left join SYSPOWER_R b on c.Role_No = b.Role_No)a where UserCode='" + Client.UserCode + "' and AppCode='" + ModuleCode.Remove(0, 3) + "'";
            DataTable dtPow = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
            string columnName = "TableName", columnName1 = "ColumnID";

            string sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode.Remove(0, 3) + "'";
            //DataTable dt = Client.GetDT(sql);

            dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
            if (dtPow.Rows.Count == 0)
            {
                sql1 = "select * from SYSLANGUAGE where AppCode='" + ModuleCode + "'";
                dtLAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql1, new Dictionary<string, string>());
            }

            if (ModuleConfig.PanelH != null)
            {
                if (dtPow.Rows.Count == 0)
                {
                    foreach (SJeMES_Framework.Web.JSONControlH c in ModuleConfig.PanelH.formData)
                    {
                        c.headId = this.DataId;
                        if (c.name.ToLower() == "status")
                        {
                            _HasStatus = true;
                        }

                        if (c.type == "text" || c.type == "other")
                        {
                            UCModuleTextBox control = new UCModuleTextBox();
                            control.InitControl(c);
                            control.Client = this.Client;
                            control.SelectedData += Control_SelectedData;
                            if (HData.Rows.Count > 0)
                                control.Value = HData.Rows[0][control.Prop].ToString();
                            control.Title = checkName(dtLAN, control.Title);
                            HControls.Add(control.Title, control);
                        }
                        else if (c.type == "select")
                        {
                            UCModuleComBox control = new UCModuleComBox();
                            control.InitControl(c);
                            if (HData.Rows.Count > 0)
                                control.Value = HData.Rows[0][control.Prop].ToString();
                            control.Title = checkName(dtLAN, control.Title);
                            HControls.Add(control.Title, control);
                        }

                        else if (c.type == "datePicker")
                        {
                            UCModuleDateTime control = new UCModuleDateTime();
                            control.InitControl(c);
                            if (HData.Rows.Count > 0)
                                control.Value = HData.Rows[0][control.Prop].ToString();
                            control.Title = checkName(dtLAN, control.Title);
                            HControls.Add(control.Title, control);
                        }

                        else if (c.type == "switch")
                        {
                            UCModuleSwitch control = new UCModuleSwitch();
                            control.InitControl(c);
                            if (HData.Rows.Count > 0)
                                control.Value = HData.Rows[0][control.Prop].ToString();
                            control.Title = checkName(dtLAN, control.Title);
                            HControls.Add(control.Title, control);
                        }
                    }
                }
                else
                {
                    foreach (SJeMES_Framework.Web.JSONControlH c in ModuleConfig.PanelH.formData)
                    {
                        c.headId = this.DataId;
                        DataRow[] dataRows1 = dtPow.Select(columnName1 + "='" + c.name + "' and " + columnName + "='" + ModuleConfig.PanelH.table + "'");

                        if (dataRows1.Length > 0)
                        {
                            if (c.name.ToLower() == "status")
                            {
                                _HasStatus = true;
                            }

                            if (c.type == "text" || c.type == "other")
                            {
                                UCModuleTextBox control = new UCModuleTextBox();
                                control.InitControl(c);
                                control.Client = this.Client;
                                control.SelectedData += Control_SelectedData;

                                if (HData.Rows.Count > 0)
                                    control.Value = HData.Rows[0][control.Prop].ToString();

                                control.Title = checkName(dtLAN, control.Title);

                                HControls.Add(control.Title, control);
                            }
                            else if (c.type == "select")
                            {
                                UCModuleComBox control = new UCModuleComBox();
                                control.InitControl(c);
                                if (HData.Rows.Count > 0)
                                    control.Value = HData.Rows[0][control.Prop].ToString();
                                control.Title = checkName(dtLAN, control.Title);

                                HControls.Add(control.Title, control);
                            }

                            else if (c.type == "datePicker")
                            {
                                UCModuleDateTime control = new UCModuleDateTime();
                                control.InitControl(c);
                                if (HData.Rows.Count > 0)
                                    control.Value = HData.Rows[0][control.Prop].ToString();
                                control.Title = checkName(dtLAN, control.Title);

                                HControls.Add(control.Title, control);
                            }

                            else if (c.type == "switch")
                            {
                                UCModuleSwitch control = new UCModuleSwitch();
                                control.InitControl(c);
                                if (HData.Rows.Count > 0)
                                    control.Value = HData.Rows[0][control.Prop].ToString();
                                control.Title = checkName(dtLAN, control.Title);

                                HControls.Add(control.Title, control);
                            }
                        }
                    }
                }
            }

            ResetLayout();
        }

        /// <summary>
        /// 添加了中文列名转换成对应语言列名的判断
        /// david
        /// 2023.2.13
        /// </summary>
        /// <param name="dtSelected"></param>
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

        private void LoadData()
        {
            try
            {
                string sql = @"
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
LEFT JOIN (select a.UserCode,b.AppCode,
[Select],[Add],Edit,[Delete],
DoSure,Audit,DoWork,[Print],Fun from SYSROLE01A1 a
left join SYSROLE02M b on a.Role_Name=b.Role_Name) b ON a.AppCode = b.AppCode
where a.AppName in(select menuname from SYSPOWER 
where  UserCode='" + Client.UserCode + "') and UserCode= '" + Client.UserCode + "' and a.AppCode='" + ModuleCode.Remove(0, 3) + "'";
                //DataTable dt = Client.GetDT(sql);

                DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["添加数据"].ToString().Trim() == "False")
                    {
                        btn_Add.Enabled = false;
                    }
                    if (dt.Rows[0]["修改数据"].ToString().Trim() == "False")
                    {
                        //ucBtnImg5.Visible = false;
                        btn_Edit.Enabled = false;

                    }
                    if (dt.Rows[0]["删除数据"].ToString().Trim() == "False")
                    {
                        //ucBtnImg3.Visible = false;
                        btn_Del.Enabled = false;

                    }
                    if (dt.Rows[0]["确认操作"].ToString().Trim() == "False")
                    {
                        btn_DoSure.Enabled = false;
                    }
                    if (dt.Rows[0]["审核操作"].ToString().Trim() == "False")
                    {
                        btn_Aduit.Enabled = false;
                    }
                    if (dt.Rows[0]["更多功能"].ToString().Trim() == "False")
                    {
                        ucCombox1.Enabled = false;
                    }
                }


                if (ModuleConfig == null)
                {
                    Dictionary<string, object> Config = ModuleHelper.GetModuleConfig(ModuleCode, Client);

                    ModuleConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<SJeMES_Framework.Web.JSONFormClass>(Config["App_Json"].ToString());

                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Config["OtherMenu"].ToString());


                    //ModuleConfig.PanelH.formData;//表头字段

                    //foreach(SJeMES_Framework.Web.JSONPanelClassB panelb in ModuleConfig.PanelB)
                    //{
                    //    panelb.Title;
                    //    panelb.HeadKeys;//表身字段
                    //}

                    OtherMenus = new Dictionary<string, OtherMenu>();
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            OtherMenu m = new OtherMenu();
                            m.Title = dr["Title"].ToString();
                            m.Action = dr["Action"].ToString();
                            m.Url = dr["Url"].ToString();
                            m.DllName = dr["DllName"].ToString();
                            m.ClassName = dr["ClassName"].ToString();
                            m.Method = dr["Method"].ToString();
                            m.Parameters = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(dr["Parameters"].ToString());
                            OtherMenus.Add(m.Title, m);
                        }
                        LoadOtherMenu();
                    }
                }

                HData = new DataTable();
                BData = new Dictionary<string, DataTable>();
                if (!string.IsNullOrEmpty(DataId))
                {
                    LoadHeadData();
                    LoadBodyData();

                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void LoadOtherMenu()
        {
            ucCombox1.Visible = true;
            List<KeyValuePair<string, string>> lisCom = new List<KeyValuePair<string, string>>();
            lisCom.Add(new KeyValuePair<string, string>("More", "More"));

            List<string> lstKeys = new List<string>();
            foreach (string key in OtherMenus.Keys)
            {
                lstKeys.Add(key);
            }
            if (lstKeys.Count > 0)
            {
                Dictionary<string, object> dic = SJeMES_Framework.Common.UIHelper.UIListMsg(lstKeys, Client, Client.WebServiceUrl, Client.Language);
                if (dic.Count > 0)
                {
                    foreach (string key in OtherMenus.Keys)
                    {
                        if (dic.ContainsKey(key))
                            lisCom.Add(new KeyValuePair<string, string>(key, dic[key].ToString()));
                        else
                            lisCom.Add(new KeyValuePair<string, string>(key, key));
                    }
                }
            }
            ucCombox1.Source = lisCom;

        }

        private void LoadBodyData()
        {
            string sql = @"select * from (select a.UserCode,b.AppCode,b.TableName,
b.ColumnName,b.ColumnID from SYSROLE01A1 a
left join SYSROLE01M c on a.Role_Name=c.Role_Name
left join SYSPOWER_R b on c.Role_No=b.Role_No)a where UserCode='" + Client.UserCode + "' and AppCode='" + ModuleCode.Remove(0, 3) + "'";
            DataTable dtPow = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Client.WebServiceUrl, sql, new Dictionary<string, string>());
            string columnName = "TableName", columnName1 = "ColumnName";
            BData = new Dictionary<string, DataTable>();

            if (dtPow.Rows.Count == 0)
            {
                foreach (SJeMES_Framework.Web.JSONPanelClassB b in ModuleConfig.PanelB)
                {
                    Dictionary<string, object> p = ModuleHelper.GetBData(ModuleCode, b.seq, b.table, DataId, Client);
                    DataTable dt = p["Data"] as DataTable;
                    if (dt.Rows.Count == 0)
                    {
                        dt = new DataTable();
                        List<string> Heads = p["Heads"] as List<string>;
                        foreach (string s in Heads)
                        {
                            dt.Columns.Add(s);
                        }
                    }

                    BData.Add(b.Title, dt);

                }
            }
            else
            {
                foreach (SJeMES_Framework.Web.JSONPanelClassB b in ModuleConfig.PanelB)
                {
                    DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                    DataRow[] dataRows = dtPow.Select(columnName + "='" + b.table + "'");

                    if (dataRows.Length > 0)
                    {
                        Dictionary<string, object> p = ModuleHelper.GetBData(ModuleCode, b.seq, b.table, DataId, Client);
                        DataTable dt = p["Data"] as DataTable;
                        if (dt.Rows.Count == 0)
                        {
                            dt = new DataTable();
                            List<string> Heads = p["Heads"] as List<string>;
                            foreach (string s in Heads)
                            {
                                DataRow[] dataRows1 = dtPow.Select(columnName1 + "='" + s + "'");
                                if (dataRows1.Length > 0) dt.Columns.Add(s);
                            }
                        }

                        BData.Add(b.Title, dt);
                    }
                }
            }


        }

        private void LoadHeadData()
        {
            HData = ModuleHelper.GetHData(ModuleCode, ModuleConfig.PanelH.table, DataId, title, Client);
            HData = ModuleHelper.GetHData(ModuleCode, ModuleConfig.PanelH.table, DataId, title, Client);
        }

        private void btn_Add_BtnClick(object sender, EventArgs e)
        {
            this.Status = ModuleStatus.Add;
        }

        private void btn_Del_BtnClick(object sender, EventArgs e)
        {
            try
            {

              //  if (SJeMES_Control_Library.MessageHelper.ShowWarning(this.FindForm(), "是否确认删除数据？") == DialogResult.OK)
                if (SJeMES_Control_Library.MessageHelper.ShowWarning(this.FindForm(), "Are you sure to delete the data？") == DialogResult.OK)
                {



                    DataTable dt = new DataTable();
                    dt.Columns.Add("TableName");
                    dt.Columns.Add("Id");

                    DataRow dr = dt.NewRow();
                    dr["TableName"] = ModuleConfig.PanelH.table;
                    dr["Id"] = DataId;
                    dt.Rows.Add(dr);

                    if (ModuleHelper.DelData(dt, Client))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this.FindForm(), "Data deleted successfully");
                        btn_Back_BtnClick(this, new EventArgs());
                    }
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void btn_Edit_BtnClick(object sender, EventArgs e)
        {
            this.Status = ModuleStatus.Edit;
        }

        private void btn_DoSure_BtnClick(object sender, EventArgs e)
        {
            if (DataStatus == "8")
            {
               // if (MessageHelper.ShowWarning(this.FindForm(), "是否确认单据？") == DialogResult.OK)
                if (MessageHelper.ShowWarning(this.FindForm(), "Whether to confirm the documents？") == DialogResult.OK)
                {
                    DataStatus = ModuleHelper.DocDoSure(ModuleCode, DataId, true, Client);
                    LoadData();
                    UpdateData();
                    MessageHelper.ShowSuccess(this.FindForm(), "Confirm the document successfully");
                }
            }
            else if (DataStatus == "7" || DataStatus == "1")
            {
                //if (MessageHelper.ShowWarning(this.FindForm(), "是否取消确认单据？") == DialogResult.OK)
                if (MessageHelper.ShowWarning(this.FindForm(), "Whether to cancel the confirmation document？") == DialogResult.OK)
                {
                    DataStatus = ModuleHelper.DocDoSure(ModuleCode, DataId, false, Client);
                    LoadData();
                    UpdateData();
                    MessageHelper.ShowSuccess(this.FindForm(), "Confirmation document canceled successfully");
                }
            }
        }

        private void btn_Aduit_BtnClick(object sender, EventArgs e)
        {
            if (DataStatus == "1" || DataStatus == "7")
            {
                //if (MessageHelper.ShowWarning(this.FindForm(), "是否审核单据？") == DialogResult.OK)
                if (MessageHelper.ShowWarning(this.FindForm(), "Whether to review documents？") == DialogResult.OK)
                {
                    DataStatus = ModuleHelper.DocAudit(ModuleCode, DataId, true, Client);
                    LoadData();
                    UpdateData();
                    MessageHelper.ShowSuccess(this.FindForm(), "Document verification successful");
                }
            }
            else if (DataStatus == "2")
            {
               // if (MessageHelper.ShowWarning(this.FindForm(), "是否取消审核单据？") == DialogResult.OK)
                if (MessageHelper.ShowWarning(this.FindForm(), "Whether to cancel the audit document？") == DialogResult.OK)
                {
                    DataStatus = ModuleHelper.DocAudit(ModuleCode, DataId, false, Client);
                    LoadData();
                    UpdateData();
                    MessageHelper.ShowSuccess(this.FindForm(), "Successfully cancel the audit document");
                }
            }
        }

        private void btn_Save_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if (CheckData())
                {

                    string TableName = ModuleConfig.PanelH.table;
                    string AppCode = ModuleCode;

                    Dictionary<string, string> TableData = new Dictionary<string, string>();
                    List<Dictionary<string, object>> RowData = new List<Dictionary<string, object>>();
                    Dictionary<string, object> Row = new Dictionary<string, object>();
                    if (!string.IsNullOrEmpty(DataId))
                    {
                        Row.Add("id", DataId);
                    }
                    foreach (string key in HControls.Keys)
                    {
                        UCModuleControl control = HControls[key];
                        if (!control.IsSysField)
                        {
                            if (!Row.ContainsKey(control.Prop))
                                //MessageBox.Show(control.Prop+":"+ control.Value);
                                Row.Add(control.Prop, control.Value);
                        }
                    }

                    RowData.Add(Row);
                    TableData.Add(TableName, Newtonsoft.Json.JsonConvert.SerializeObject(RowData));

                    if (!string.IsNullOrEmpty(DataId))
                    {
                        if (ModuleHelper.EditHData(AppCode, TableName, TableData, Client))
                        {
                            LoadData();
                            UpdateData();
                            MessageHelper.ShowSuccess(this.FindForm(), "Modify data successfully");
                        }
                    }
                    else
                    {
                        DataId = ModuleHelper.AddHData(AppCode, TableName, TableData, Client);

                        foreach (string key in BControls.Keys)
                        {
                            ((UCModuleDataBody)BControls[key]).HeadId = DataId;
                        }
                        LoadData();
                        UpdateData();
                        Status = ModuleStatus.Edit;
                        MessageHelper.ShowSuccess(this.FindForm(), "Add data successfully");

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

                if (!control.IsNull && string.IsNullOrEmpty(control.Value.ToString().Trim()))
                {
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
                            catch { control.ErrMsg = "The input must be an integer"; control.ShowErrMsg = true; ret = false; }
                            break;
                        case UCModuleControl.ControlDataType.Decimal:
                            try
                            {
                                Convert.ToDecimal(control.Value);
                                control.ShowErrMsg = false;
                            }
                            catch { control.ErrMsg = "The input must be a number"; control.ShowErrMsg = true; ret = false; }
                            break;

                    }
                }


            }

            return ret;
        }

        private void UCModuleBase_Load(object sender, EventArgs e)
        {
            LoadContorls();
            this.Status = Status;
            if (ucCombox1.Source != null)
            {
                if (ucCombox1.Source.Count > 0)
                {
                    ucCombox1.SelectedIndex = 0;
                }
            }

        }

        private void UCModuleBase_SizeChanged(object sender, EventArgs e)
        {
            ResetLayout();
        }

        private void ucCombox1_SelectedChangedEvent(object sender, EventArgs e)
        {
            if (ucCombox1.SelectedIndex != 0)
            {
                DoOtherWork();
                ucCombox1.SelectedIndex = 0;
            }
        }

        private void DoOtherWork()
        {
            try
            {
                Dictionary<string, object> P = new Dictionary<string, object>();
                Dictionary<string, string> P2 = new Dictionary<string, string>();
                OtherMenu fbc = new OtherMenu();
                string tmp = string.Empty;
                string tmp2 = string.Empty;
                foreach (string key in OtherMenus.Keys)
                {
                    if (key == ucCombox1.SelectedValue)
                    {
                        fbc = OtherMenus[key];
                        break;
                    }
                }
                switch (fbc.Action)
                {
                    case "AccessWeb":
                        System.Diagnostics.Process.Start(fbc.Url);
                        break;
                    case "RunApp":
                        foreach (string s in fbc.Parameters.Keys)
                        {
                            if (fbc.Parameters[s].StartsWith("HeadData."))
                            {
                                try
                                {
                                    P.Add(s, HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString());
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("There is no data, the operation cannot be performed！");
                                    return;
                                }

                            }
                            else
                            {
                                P.Add(s, fbc.Parameters[s]);
                            }
                        }

                        P.Add("Org", Client.Org.Org);
                        P.Add("OrgName", Client.Org.OrgName);
                        P.Add("DBServer", Client.Org.DBServer);
                        P.Add("DBType", Client.Org.DBType);
                        P.Add("DBName", Client.Org.DBName);
                        P.Add("DBUser", Client.Org.DBUser);
                        P.Add("DBPassword", Client.Org.DBPassword);
                        P.Add("IsMaxWindow", false);
                        P.Add("WebServiceUrl", Client.WebServiceUrl);
                        P.Add("User", Client.UserCode);


                        SJeMES_Framework.Common.OtherPrograms.RunApp(fbc.DllName, fbc.ClassName, fbc.Method, P);
                        return;
                    case "RunFastReport":
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        foreach (string s in fbc.Parameters.Keys)
                        {
                            if (fbc.Parameters[s].StartsWith("HeadData."))
                            {
                                try
                                {
                                    dic.Add(s, HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString() + "*" + fbc.Parameters[s].Replace("HeadData.", ""));
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("There is no data, the operation cannot be performed！");
                                    return;
                                }

                            }
                            else
                            {
                                dic.Add(s, fbc.Parameters[s]);
                            }


                        }
                        SJeMES_Control_Library.Forms.frmFastReport frmFR = new SJeMES_Control_Library.Forms.frmFastReport(Client.Org, Client.WebServiceUrl, dic);
                        frmFR.Show();
                        return;
                    case "PrintFastReport":
                        string docNo = string.Empty;
                        string moduleNo = string.Empty;
                        string headDataKey = string.Empty;
                        foreach (string s in fbc.Parameters.Keys)
                        {
                            if (fbc.Parameters[s].StartsWith("HeadData."))
                            {
                                try
                                {
                                    docNo = HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString();
                                    moduleNo = ModuleConfig.PanelH.table;
                                    headDataKey = fbc.Parameters[s].Replace("HeadData.", "");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("There is no data, the operation cannot be performed！");
                                    return;
                                }

                            }
                        }
                        Forms.FrmReportMain frMain = new Forms.FrmReportMain(docNo, moduleNo, headDataKey, this.Client);
                        frMain.ShowDialog();
                        return;
                    case "RunService":
                        if (HData.Rows[0] == null)
                            return;
                        foreach (string s in fbc.Parameters.Keys)
                        {
                            if (fbc.Parameters[s].StartsWith("HeadData."))
                            {
                                P2.Add(s, HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString());
                            }
                            else
                            {
                                P2.Add(s, fbc.Parameters[s]);
                            }
                        }

                        P2.Add("UserCode", Client.Org.User.UserCode);


                        string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Client.Org, Client.WebServiceUrl, fbc.DllName, fbc.ClassName, fbc.Method, P2);
                        if (Convert.ToBoolean(SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                        {

                            MessageBox.Show("Successful operation");
                            LoadData();
                            UpdateData();

                        }

                        else
                        {
                            MessageBox.Show(SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                        }
                        return;
                    case "PrintBarCode":

                        foreach (string s in fbc.Parameters.Keys)
                        {
                            if (fbc.Parameters[s].StartsWith("HeadData."))
                            {
                                tmp = HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString();
                                tmp2 = "@" + fbc.Parameters[s].Replace("HeadData.", "");
                            }

                        }

                        string sql = fbc.Parameters["SQL"];

                        sql = sql.Replace(tmp2, tmp);
                        //判断数据库是否有存储过程
                        if (SJeMES_Framework.Common.WebServiceHelper.GetDataTable(Client.Org, Client.WebServiceUrl,
                           "select * from dbo.sysobjects t where t.name='sp_PowerWarehouse'", new Dictionary<string, string>()).Rows.Count > 0)
                        {
                            sql = "exec sp_PowerWarehouse '" + sql.Replace("'", "''") + "','" + Client.Org.User.UserCode + "','material_no',''";
                        }
                        System.Data.DataTable dt = SJeMES_Framework.Common.WebServiceHelper.GetDataTable(Client.Org, Client.WebServiceUrl, sql, new Dictionary<string, string>());

                        //List<string> Data = new List<string>();
                        //foreach (System.Data.DataRow dr in dt.Rows)
                        //{
                        //    Data.Add(dr[0].ToString());
                        //}

                        SJeMES_Control_Library.Forms.frmBarCodePrinter frm = new SJeMES_Control_Library.Forms.frmBarCodePrinter(Client, Client.WebServiceUrl, dt, fbc.Title);
                        frm.ShowDialog();
                        return;
                    case "PrintBarCode2":

                        foreach (string s in fbc.Parameters.Keys)
                        {
                            if (fbc.Parameters[s].StartsWith("HeadData."))
                            {
                                tmp = HData.Rows[0][fbc.Parameters[s].Replace("HeadData.", "")].ToString();
                                tmp2 = "@" + fbc.Parameters[s].Replace("HeadData.", "");
                            }

                        }

                        sql = fbc.Parameters["SQL"];

                        sql = sql.Replace(tmp2, tmp);
                        string sql1 = sql;
                        //判断数据库是否有存储过程
                        if (SJeMES_Framework.Common.WebServiceHelper.GetDataTable(Client.Org, Client.WebServiceUrl,
                           "select * from dbo.sysobjects t where t.name='sp_PowerWarehouse'", new Dictionary<string, string>()).Rows.Count > 0)
                        {
                            sql = "exec sp_PowerWarehouse '" + sql.Replace("'", "''") + "','" + Client.Org.User.UserCode + "','material_no',''";
                        }
                        dt = SJeMES_Framework.Common.WebServiceHelper.GetDataTable(Client.Org, Client.WebServiceUrl, sql, new Dictionary<string, string>());

                        List<string> Data = new List<string>();
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            Data.Add(dr[0].ToString());
                        }

                        frm = new Forms.frmBarCodePrinter(Client, Client.WebServiceUrl, sql1, fbc.Title, dt);
                        frm.ShowDialog();
                        return;

                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }
    }
}

