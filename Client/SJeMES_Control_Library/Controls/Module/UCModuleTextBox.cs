using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SJeMES_Control_Library.Controls
{
    public partial class UCModuleTextBox : UCModuleControl, IContainerControl
    {
        private string _DataSourceSQL;
        public  string DataSourceSQL
        {
            get { return _DataSourceSQL; }
            set { _DataSourceSQL = value;  }
        }

        private SJeMES_Framework.Class.ClientClass _Client;
        public SJeMES_Framework.Class.ClientClass Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        private string _ErrMsg;
        public override string ErrMsg
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; lab_Err.Text = _ErrMsg; }
        }

        private bool _ShowErrMsg;
        public override bool ShowErrMsg
        {
            get { return _ShowErrMsg; }
            set { _ShowErrMsg = value; lab_Err.Visible = value; }
        }


        private bool _IsEdit;
        [Description("允许修改"), Category("自定义")]
        public override bool IsEdit
        {
            get { return _IsEdit; }
            set
            {
                _IsEdit = value;
            }
        }

        private bool _IsAdd;
        [Description("允许新增"), Category("自定义")]
        public override bool IsAdd
        {
            get { return _IsAdd; }
            set
            {
                _IsAdd = value;
            }
        }

        private bool _IsSysField;
        public override bool IsSysField
        {
            get { return _IsSysField; }
            set { _IsSysField = value; }
        }


        private bool _ReadOnly=false;
        public override bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
                if(this.DataType != ControlDataType.DataSource)
                    ucTextBoxEx1.txtInput.ReadOnly = value;
                if (value)
                {
                    ucTextBoxEx1.IsShowClearBtn = false;
                    lab_Title.ForeColor = Color.Black;
                    
                }
                else
                {
                    ucTextBoxEx1.IsShowClearBtn = true;
                    if(this.IsNull)
                    {
                        lab_Title.ForeColor = Color.Black;
                    }
                    else
                    {
                        lab_Title.ForeColor = Color.FromArgb(255, 128, 128);
                    }
                }
            }
        }

        private string _Title;
        [Description("标题"), Category("自定义")]
        public virtual string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                lab_Title.Text = value;
            }
        }
        
        private string _Prop;
        [Description("绑定字段"), Category("自定义")]
        public override string Prop
        {
            get { return _Prop; }
            set
            {
                _Prop = value;
            }
        }
        private string _Text = string.Empty;

        [Description("显示的内容"), Category("自定义")]
        public virtual string Text
        {
            get { return _Text; }
            
        }
        private string _Value =string.Empty;

        [Description("实际数据"), Category("自定义")]
        public override object Value
        { 
            get { return _Value; }
            set
            {
                _Value = value.ToString();
                if(ucTextBoxEx1.InputText != value.ToString())
                    ucTextBoxEx1.InputText = value.ToString() ;
            }
        }

        private bool _IsNull=true;
        [Description("是否可以为空"), Category("自定义")]
        public override bool IsNull
        {
            get { return _IsNull; }
            set
            {
                _IsNull = value;
                if(value)
                {
                    lab_Title.ForeColor =Color.Black;
                }
                else
                {
                    if (!ReadOnly)
                    {
                        lab_Title.ForeColor = Color.FromArgb(255, 128, 128);
                    }
                }
            }
        }
        private ControlDataType _DataType;
        [Description("数据类型"), Category("自定义")]
        public override ControlDataType DataType
        {
            get { return _DataType; }
            set
            {
                _DataType = value;
                if(value == ControlDataType.DataSource)
                {
                    ucTextBoxEx1.IsShowSearchBtn = true;
                    ucTextBoxEx1.txtInput.ReadOnly = true;
                }
                else
                {
                    ucTextBoxEx1.IsShowSearchBtn = false;
                    ucTextBoxEx1.txtInput.ReadOnly = false;
                }
            }
        }

        public DataRow dr = null;

        public string HeadId = "";

        //定义委托
        public delegate void SelectedDataHandle(DataTable dtSelected);
        //定义事件
        public event SelectedDataHandle SelectedData;

        public UCModuleTextBox()
        {
            InitializeComponent();
            lab_Err.Visible = false;
        }

        private void ucTextBoxEx1_SearchClick(object sender, EventArgs e)
        {
            if(this.DataType == ControlDataType.DataSource &&
                !string.IsNullOrEmpty(this.DataSourceSQL))
            {
                if (DataSourceSQL.Contains("HeadData."))
                {
                    string[] s = Regex.Matches(DataSourceSQL, @"'(.*?)'").Cast<Match>().Select(x => x.Groups[1].Value).ToArray();
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (s[i].StartsWith("HeadData."))
                        {
                            string name = s[i].Replace("HeadData.", "");
                            DataSourceSQL = DataSourceSQL.Replace(s[i], dr.Table.Rows[0][name].ToString());
                        }
                    }  
                }
                if ((string.IsNullOrEmpty(this.HeadId) && this.IsAdd) || (!string.IsNullOrEmpty(this.HeadId) && this.IsEdit))
                {
                    Forms.FrmSelectData frm = new Forms.FrmSelectData(this.DataSourceSQL, true, this.Client);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dt = frm.RetData;
                        if (SelectedData != null)
                            SelectedData(dt);
                    }
                }
            }
        }

        private void ucTextBoxEx1_TextChanged(object sender, EventArgs e)
        {
            if (this.Value.ToString() != ucTextBoxEx1.InputText)
                this.Value = ucTextBoxEx1.InputText;
        }

        public override void InitControl(SJeMES_Framework.Web.JSONControlH _ControlConfig)
        {

            this.Title = _ControlConfig.Item.label;
            this.Prop = _ControlConfig.name;
            this.IsSysField = _ControlConfig.systemFiled;

            this.IsEdit = _ControlConfig.control.IsEdit;
            this.IsAdd = _ControlConfig.control.IsAdd;

            this.HeadId = _ControlConfig.headId;

            foreach (SJeMES_Framework.Web.JSONControlHRules rule in _ControlConfig.rules)
            {
                if (rule.type == "required")
                {
                    this.IsNull = false;
                }
            }

            switch (_ControlConfig.type)
            {
                case "text":
                    this.DataType = ControlDataType.String;
                    foreach (SJeMES_Framework.Web.JSONControlHRules rule in _ControlConfig.rules)
                    {
                        if (rule.type == "digits")
                        {
                            this.DataType = ControlDataType.Int;
                        }
                        else if (rule.type == "number")
                        {
                            this.DataType = ControlDataType.Decimal;
                        }


                    }


                    break;
                case "other":
                    this.DataType = ControlDataType.DataSource;
                    this.DataSourceSQL = _ControlConfig.otherData.sql;
                    break;
            }



        }

        public override void InitControl(SJeMES_Framework.Web.JSONControlB _ControlConfig)
        {

            this.Title = _ControlConfig.label;
            this.Prop = _ControlConfig.prop;
            this.IsSysField = _ControlConfig.systemFiled;

            this.IsEdit = _ControlConfig.IsEdit;
            this.IsAdd = _ControlConfig.IsAdd;

            foreach (SJeMES_Framework.Web.JSONControlHRules rule in _ControlConfig.rules)
            {
                if (rule.type == "required")
                {
                    this.IsNull = false;
                }
            }

            switch (_ControlConfig.type)
            {
                case "text":
                    this.DataType = ControlDataType.String;
                    foreach (SJeMES_Framework.Web.JSONControlHRules rule in _ControlConfig.rules)
                    {
                        if (rule.type == "digits")
                        {
                            this.DataType = ControlDataType.Int;
                        }
                        else if (rule.type == "number")
                        {
                            this.DataType = ControlDataType.Decimal;
                        }


                    }


                    break;
                case "other":
                    this.DataType = ControlDataType.DataSource;
                    this.DataSourceSQL = _ControlConfig.otherData.sql;
                    break;
            }



        }
    }
}
