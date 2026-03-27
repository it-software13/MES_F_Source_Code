using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Framework.Web;

namespace SJeMES_Control_Library.Controls
{
    public partial class UCModuleSwitch : UCModuleControl, IContainerControl
    {

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
                ucSwitch1.Enabled = !value;

                if (value)
                {

                    lab_Title.ForeColor = Color.Black;
                    
                }
                else
                {
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
        private bool _Text=false;

        [Description("显示的内容"), Category("自定义")]
        public virtual bool Text
        {
            get { return _Text; }
            
        }
        private bool _Value=false;

        [Description("实际数据"), Category("自定义")]
        public override object Value
        { 
            get { return _Value; }
            set
            {
                bool v = false;
                try
                {
                    v = Convert.ToBoolean(value);
                }
                catch { }
                _Text = v;
                _Value = v;
                if (ucSwitch1.Checked != v)
                {
                    ucSwitch1.Checked = v;
                }
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
        public virtual ControlDataType DataType
        {
            get { return _DataType; }
            set
            {
                _DataType = value;
            }
        }


        public UCModuleSwitch()
        {
            InitializeComponent();
            lab_Err.Visible = false;
        }

        private void ucSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if(Convert.ToBoolean(this.Value) != ucSwitch1.Checked)
            this.Value = ucSwitch1.Checked;
        }

        public override void InitControl(JSONControlH _ControlConfig)
        {


            this.Title = _ControlConfig.Item.label;
            this.Prop = _ControlConfig.name;
            this.IsSysField = _ControlConfig.systemFiled;
            this.DataType = ControlDataType.Bool;

            this.IsAdd = _ControlConfig.control.IsAdd;
            this.IsEdit = _ControlConfig.control.IsEdit;

        }

        public override void InitControl(JSONControlB _ControlConfig)
        {


            this.Title = _ControlConfig.label;
            this.Prop = _ControlConfig.prop;
            this.IsSysField = _ControlConfig.systemFiled;
            this.DataType = ControlDataType.Bool;

            this.IsAdd = _ControlConfig.IsAdd;
            this.IsEdit = _ControlConfig.IsEdit;

        }
    }
}
