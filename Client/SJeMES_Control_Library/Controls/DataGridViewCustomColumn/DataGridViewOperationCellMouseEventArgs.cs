using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGrid.DataGridViewCustomColumn
{
    /// <summary>
    /// 为与 <see cref="DataGridViewOperationCell"/> 操作有关的 <see cref="DataGridView"/> 事件提供数据。
    /// </summary>
    public class DataGridViewOperationCellEventArgs: DataGridViewCellEventArgs
    {
        /// <summary>
        /// 初始化<see cref="DataGridViewOperationCellMouseEventArgs"/>类的新实例
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="columnIndex">单元格的从零开始的列索引。</param>
        /// <param name="rowIndex">单元格的从零开始的行索引。</param>
        public DataGridViewOperationCellEventArgs(string name, int columnIndex, int rowIndex) : base(columnIndex, rowIndex)
        {
            this.Name = name;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 表示将处理 <see cref="DataGridViewOperationCell"/> 操作相关的 <see cref="DataGridView"/> 事件的方法。
    /// </summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e"><see cref="DataGridViewOperationCellEventArgs"/> 事件数据</param>
    public delegate void DataGridViewOperationCellHandler(object sender, DataGridViewOperationCellEventArgs e);
}
