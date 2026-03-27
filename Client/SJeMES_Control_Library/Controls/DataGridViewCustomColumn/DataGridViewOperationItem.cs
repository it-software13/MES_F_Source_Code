using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DataGrid.DataGridViewCustomColumn
{
    /// <summary>
    /// 表示 DataGridViewOperationItem
    /// </summary>
    public class DataGridViewOperationItem
    {
        /// <summary>
        /// 初始化 <see cref="DataGridViewOperationItem"/> 类的新实例
        /// </summary>
        public DataGridViewOperationItem()
        {
        }

        internal DataGridViewOperationCell DataGridViewOperationCell { get; set; }

        private string m_Name;

        /// <summary>
        /// 名称
        /// </summary>
        [Browsable(true), DefaultValue(""), Category("Appearance"), Description("名称")]
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
                if (this.DataGridViewOperationCell != null)
                {
                    this.DataGridViewOperationCell.OnDataGridViewColumnCommonChange();
                }
            }
        }

        private string m_Text;

        /// <summary>
        /// 文本
        /// </summary>
        [Browsable(true), DefaultValue(""), Category("Appearance"), Description("文本")]
        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
                if (this.DataGridViewOperationCell != null)
                {
                    this.DataGridViewOperationCell.OnDataGridViewColumnCommonChange();
                }
            }
        }

        private Image m_Image;

        /// <summary>
        /// 单元格显示的图像
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("单元格显示的图像。")]
        public Image Image
        {
            get
            {
                return m_Image;
            }
            set
            {
                m_Image = value;
                if (this.DataGridViewOperationCell != null)
                {
                    this.DataGridViewOperationCell.OnDataGridViewColumnCommonChange();
                }
            }
        }

        private bool m_Enabled = true;

        /// <summary>
        /// 是否可用
        /// </summary>
        [Browsable(true), DefaultValue(true), Category("Appearance"), Description("是否可用。")]
        public bool Enabled
        {
            get
            {
                return m_Enabled;
            }
            set
            {
                m_Enabled = value;
                if (this.DataGridViewOperationCell != null)
                {
                    this.DataGridViewOperationCell.OnDataGridViewColumnCommonChange();
                }
            }
        }

        private bool m_Visible = true;

        /// <summary>
        /// 是否可见
        /// </summary>
        [Browsable(true), DefaultValue(true), Category("Appearance"), Description("是否可见。")]
        public bool Visible
        {
            get
            {
                return m_Visible;
            }
            set
            {
                m_Visible = value;
                if (this.DataGridViewOperationCell != null)
                {
                    this.DataGridViewOperationCell.OnDataGridViewColumnCommonChange();
                }
            }
        }

        /// <summary>
        /// 当前选择项
        /// </summary>
        public static string ActiveItem { get; set; }
    }

    /// <summary>
    /// 表示 DataGridViewOperationItem 集合
    /// </summary>
    public class DataGridViewOperationItems : Collection<DataGridViewOperationItem>
    {
        /// <summary>
        /// 初始化类 <see cref="DataGridViewOperationItems"/> 的新实例
        /// </summary>
        /// <param name="cell">DataGridViewOperationCell 对象</param>
        public DataGridViewOperationItems(DataGridViewOperationCell cell)
        {
            this.DataGridViewOperationCell = cell;
        }

        protected DataGridViewOperationCell DataGridViewOperationCell { get; set; }

        /// <summary>
        /// 将元素插入集合的指定索引处。
        /// </summary>
        /// <param name="index">从零开始的索引，应在该位置插入 item。</param>
        /// <param name="item">要插入的 <see cref="DataGridViewOperationItem"/> 对象。</param>
        protected override void InsertItem(int index, DataGridViewOperationItem item)
        {
            item.DataGridViewOperationCell = this.DataGridViewOperationCell;
            base.InsertItem(index, item);
            if (this.DataGridViewOperationCell != null)
            {
                this.DataGridViewOperationCell.OnDataGridViewColumnCommonChange();
            }
        }

        /// <summary>
        /// 替换指定索引处的元素。
        /// </summary>
        /// <param name="index">待替换元素的从零开始的索引。</param>
        /// <param name="item">位于指定索引处的 <see cref="DataGridViewOperationItem"/> 的新值。</param>
        protected override void SetItem(int index, DataGridViewOperationItem item)
        {
            item.DataGridViewOperationCell = this.DataGridViewOperationCell;
            base.SetItem(index, item);
            if (this.DataGridViewOperationCell != null)
            {
                this.DataGridViewOperationCell.OnDataGridViewColumnCommonChange();
            }
        }

        /// <summary>
        /// 移除集合的指定索引处的元素。
        /// </summary>
        /// <param name="index">要移除的元素的从零开始的索引。</param>
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            if (this.DataGridViewOperationCell != null)
            {
                this.DataGridViewOperationCell.OnDataGridViewColumnCommonChange();
            }
        }

        /// <summary>
        /// 从集合中移除所有元素。
        /// </summary>
        protected override void ClearItems()
        {
            base.ClearItems();
            if (this.DataGridViewOperationCell != null)
            {
                this.DataGridViewOperationCell.OnDataGridViewColumnCommonChange();
            }
        }

        /// <summary>
        /// 获取集合中实际包含的可见元素数。
        /// </summary>
        /// <returns></returns>
        public virtual int VisibleCount
        {
            get
            {
                int count = 0;
                foreach (DataGridViewOperationItem item in this)
                {
                    if (item.Visible) count++;
                }
                return count;
            }
        }
    }
}
