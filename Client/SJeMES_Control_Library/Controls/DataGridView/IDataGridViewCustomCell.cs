using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Control_Library.Controls
{
    public interface IDataGridViewCustomCell
    {
        /// <summary>
        /// 绑定行关联的数据
        /// </summary>
        /// <param name="obj">The object.</param>
        void SetBindSource(object obj);
    }
}
