using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGrid.DataGridViewCustomColumn
{
    /// <summary>
    /// 在可自定义的网格中显示数据。
    /// </summary>
    public class SCDataGridView : DataGridView
    {
        /// <summary>
        /// 初始化 <see cref="SCDataGridView"/> 类的新实例。
        /// </summary>
        public SCDataGridView() : base()
        {
            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// 操作列溢出菜单
        /// </summary>
        internal ContextMenuStrip OperationOverflowContextMenuStrip { get; } = new ContextMenuStrip();

        #region -> 事件

        /// <summary>
        /// 操作列溢出菜单项点击时发生。
        /// </summary>
        [Category("Mouse"), Description("操作列溢出菜单项点击时发生。")]
        public event DataGridViewOperationCellHandler OperationCellOverflowItemClick;

        /// <summary>
        /// 引发 <see cref="OperationCellOverflowItemClick"/> 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="DataGridViewOperationCellEventArgs"/>。</param>
        protected virtual void OnOperationCellOverflowItemClick(DataGridViewOperationCellEventArgs e)
        {
            if (this.OperationCellOverflowItemClick != null)
            {
                this.OperationCellOverflowItemClick(this, e);
            }
        }

        /// <summary>
        /// 引发 <see cref="OperationCellOverflowItemClick"/> 事件。
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="columnIndex">列索引</param>
        /// <param name="rowIndex">行索引</param>
        internal void OnOperationOverflowItemClickInternal(string name, int columnIndex, int rowIndex)
        {
            this.OnOperationCellOverflowItemClick(new DataGridViewOperationCellEventArgs(name, columnIndex, rowIndex));
        }

        #endregion
    }
}
