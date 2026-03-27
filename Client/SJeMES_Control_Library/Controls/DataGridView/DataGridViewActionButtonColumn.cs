using SJeMES_Control_Library.Controls.Btn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Controls.DataGridView
{
   public  class DataGridViewActionButtonColumn: DataGridViewColumn
    {
        public List<ActionButton> buttonList;

        public DataGridViewActionButtonColumn(List<ActionButton> lst)
        {
            DataGridViewActionButtonCell dataGridViewActionButtonCell = new DataGridViewActionButtonCell();
             
            //List<ActionButton> list = new List<ActionButton>();
            //list.Add(ActionButtonDefaultConfig.GetUpdateBtnConfig());
            //list.Add(ActionButtonDefaultConfig.GetDeleteBtnConfig());
            //list.Add(ActionButtonDefaultConfig.GetDetailBtnConfig());
            //list.Add(ActionButtonDefaultConfig.GetPrintBtnConfig());
            //list.Add(ActionButtonDefaultConfig.GetUploadIMGBtnConfig());
            //list.Add(ActionButtonDefaultConfig.GetUploadFileBtnConfig());

            buttonList = lst;

            DataGridViewActionButtonCell.ButtonList = buttonList;

            this.CellTemplate = dataGridViewActionButtonCell;
            this.HeaderText = "操作";
        }

        public List<ActionButton> ButtonList { get => buttonList; set => buttonList = value; }
    }
}
