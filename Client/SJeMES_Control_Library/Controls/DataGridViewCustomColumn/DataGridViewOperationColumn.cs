using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DataGrid.DataGridViewCustomColumn
{
    /// <summary>
    /// 表示 DataGridView 操作列
    /// </summary>
    public class DataGridViewOperationColumn: DataGridViewColumn
    {
        /// <summary>
        /// 初始化 <see cref="DataGridViewOperationColumn"/> 类的新实例。
        /// </summary>
        public DataGridViewOperationColumn() : base(new DataGridViewOperationCell())
        {
        }

        private DataGridViewOperationCell OperationCellTemplate
        {
            get
            {
                return (DataGridViewOperationCell)this.CellTemplate;
            }
        }

        /// <summary>
        /// 列的单元格模板
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if ((value != null) && !(value is DataGridViewOperationCell))
                {
                    throw new Exception(string.Format("错误的单元格模板类型：{0}。", this.GetType().FullName));
                }
                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// 获取或设置数据源属性的名称或与 <see cref="DataGridViewColumn"/> 绑定的数据库列的名称。
        /// </summary>
        [Browsable(false)]
        new public string DataPropertyName
        {
            get
            {
                return base.DataPropertyName;
            }
            set
            {
                base.DataPropertyName = value;
            }
        }

        ///// <summary>
        ///// 列的默认值单元格样式
        ///// </summary>
        //[Browsable(false), Category("Appearance"), Description("列的默认值单元格样式"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override DataGridViewCellStyle DefaultCellStyle
        //{
        //    get
        //    {
        //        return base.DefaultCellStyle;
        //    }
        //    set
        //    {
        //        base.DefaultCellStyle = value;
        //    }
        //}

        /// <summary>
        /// 与图像关联的用户定义文本
        /// </summary>
        [Browsable(true), DefaultValue(""), Category("Appearance"), Description("与图像关联的用户定义文本。")]
        public string Description
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new Exception("CellTemplate 不能为空！");
                }
                return this.OperationCellTemplate.Description;
            }
            set
            {
                if (this.CellTemplate == null)
                {
                    throw new Exception("CellTemplate 不能为空！");
                }
                this.OperationCellTemplate.Description = value;
                if (base.DataGridView != null)
                {
                    DataGridViewRowCollection rows = base.DataGridView.Rows;
                    int count = rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataGridViewOperationCell cell = rows.SharedRow(i).Cells[base.Index] as DataGridViewOperationCell;
                        if (cell != null)
                        {
                            cell.Description = value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// DataGridViewOperationItem 集合
        /// </summary>
        [Category("Appearance"), Description("DataGridViewOperationItem 集合。"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), MergableProperty(false)]
        public DataGridViewOperationItems Items
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new Exception("CellTemplate 不能为空！");
                }
                return this.OperationCellTemplate.Items;
            }
        }

        private Size m_ItemSize = new Size(16, 16);

        /// <summary>
        /// 获取或者设置图片项大小
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Size), "16, 16"), Category("Appearance"), Description("获取或者设置图片项大小。")]
        public Size ItemSize
        {
            get
            {
                return m_ItemSize;
            }
            set
            {
                m_ItemSize = value;
                this.OnDataGridViewColumnCommonChange();
            }
        }

        private Image m_OverflowImage = global::SJeMES_Control_Library.Properties.Resources.DataGridViewOperationOverflowImage;

        /// <summary>
        /// 获取或者设置溢出图片
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Image), "System.Drawing.Bitmap"), Category("Appearance"), Description("获取或者设置溢出图片。")]
        public Image OverflowImage
        {
            get
            {
                return m_OverflowImage; 
            }
            set
            {
                m_OverflowImage = value;
                this.OnDataGridViewColumnCommonChange();
            }
        }

        /// <summary>
        /// 克隆一个 <see cref="DataGridViewOperationColumn"/>。
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            DataGridViewOperationColumn column = base.Clone() as DataGridViewOperationColumn;
            if (column != null)
            {
                column.ItemSize = this.ItemSize;
            }

            return column;
        }

        /// <summary>
        /// 获取表示当前实例的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("DataGridViewOperationColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        internal void OnDataGridViewColumnCommonChange()
        {
            if (this.DataGridView != null)
            {
                Type type = this.DataGridView.GetType();
                MethodInfo method = type.GetMethod("OnColumnCommonChange", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null) method.Invoke(this.DataGridView, new object[] { this.Index });
            }
        }
    }
}
