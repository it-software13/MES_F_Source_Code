using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SJeMES_Control_Library;

/*
 * @Author: 江心逐浪
 * 
 * @E-Mail: jiangshan_111@126.com   
 * 
 * @Content:
 *      主要是对DataGridView进行扩展。
 */
namespace SJeMES_Control_Library
{
    /// <summary>
    /// 扩展的DataGridView
    /// </summary>
    public class DataGridViewEx : DataGridView
    {

        public DataGridViewEx()
            : base()
        {

        }

        /// <summary>
        /// checkbox的单元格改变事件
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        internal void OnCheckBoxCellCheckedChange(int columnIndex, int rowIndex, bool value)
        {
            bool existsChecked = false, existsNoChecked = false;
            DataGridViewCheckBoxCellEx cellEx;
            foreach (DataGridViewRow row in this.Rows)
            {
                cellEx = row.Cells[columnIndex] as DataGridViewCheckBoxCellEx;
                if (cellEx == null) return;
                existsChecked |= cellEx.Checked;
                existsNoChecked |= !cellEx.Checked;
            }

            DataGridViewCheckBoxColumnHeaderCellEx headerCellEx =
                this.Columns[columnIndex].HeaderCell as DataGridViewCheckBoxColumnHeaderCellEx;

            if (headerCellEx == null) return;

            CheckState oldState = headerCellEx.CheckedAllState;

            if (existsChecked)
                headerCellEx.CheckedAllState = existsNoChecked ? CheckState.Indeterminate : CheckState.Checked;
            else
                headerCellEx.CheckedAllState = CheckState.Unchecked;

            if (oldState != headerCellEx.CheckedAllState)
                this.InvalidateColumn(columnIndex);
        }

        /// <summary>
        /// 全选中/取消全选中
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="isCheckedAll"></param>
        internal void OnCheckAllCheckedChange(int columnIndex,bool isCheckedAll)
        {
            DataGridViewCheckBoxCellEx cellEx;
            foreach (DataGridViewRow row in this.Rows)
            {
                cellEx = row.Cells[columnIndex] as DataGridViewCheckBoxCellEx;
                if (cellEx == null) continue;
                cellEx.Checked = isCheckedAll;
            }
        }

    }
}
