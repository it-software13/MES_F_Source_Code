using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Controls
{
    public partial class UCModuleControl : UCControlBase, IContainerControl
    {
        public enum ControlDataType
        {
            String,
            Int,
            Decimal,
            DataSource,
            Bool,
            Enum,
            Date,
            Time,
            DateTime
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

        private  bool _ReadOnly;
        [Description("是否只读"), Category("自定义")]
        public virtual bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
            }
        }

        private string _Prop;
        [Description("绑定字段"), Category("自定义")]
        public virtual string Prop
        {
            get { return _Prop; }
            set
            {
                _Prop = value;
            }
        }

        private bool _IsEdit;
        [Description("允许修改"), Category("自定义")]
        public virtual bool IsEdit
        {
            get { return _IsEdit; }
            set
            {
                _IsEdit = value;
            }
        }

        private bool _IsAdd;
        [Description("允许新增"), Category("自定义")]
        public virtual bool IsAdd
        {
            get { return _IsAdd; }
            set
            {
                _IsAdd = value;
            }
        }

        private bool _IsSysField;
        public virtual bool IsSysField
        {
            get { return _IsSysField; }
            set { _IsSysField = value; }
        }

        private object _Value;
        public virtual object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private string _ErrMsg;
        public virtual string ErrMsg
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }

        private bool _ShowErrMsg;
        public virtual bool ShowErrMsg
        {
            get { return _ShowErrMsg; }
            set { _ShowErrMsg = value; }
        }

        private bool _IsNull;
        public virtual bool IsNull
        {
            get { return _IsNull; }
            set { _IsNull = value; }
        }
        private string _DataSQL;
        //public string DataSQL = string.Empty;
        public UCModuleControl()
        {
            InitializeComponent();
        }

        public virtual void InitControl(SJeMES_Framework.Web.JSONControlH _ControlConfig)
        {
          
        }

        public virtual void InitControl(SJeMES_Framework.Web.JSONControlB _ControlConfig)
        {
            //switch (_ControlConfig.type)
            //{
            //    case "text":
            //        this.DataType = ControlDataType.String;
            //        foreach (SJeMES_Framework.Web.JSONControlHRules rule in _ControlConfig.rules)
            //        {
            //            if (rule.type == "digits")
            //            {
            //                this.DataType = ControlDataType.Int;
            //            }
            //            else if (rule.type == "number")
            //            {
            //                this.DataType = ControlDataType.Decimal;
            //            }


            //        }


            //        break;
            //    case "other":
            //        this.DataType = ControlDataType.DataSource;
            //        this.DataSQL = _ControlConfig.otherData.sql;
            //        break;
            //}
        }
    }
}
