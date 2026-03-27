using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_TQC.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_TQC
{
    public partial class TQC_Task_Check : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        DataTable data1 = new DataTable();
        string task_no = string.Empty;
        string ck = string.Empty;
        public TQC_Task_Check()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public TQC_Task_Check(DataTable _dt,string _task_no,string _ck)
        {
            InitializeComponent();
            data1 = _dt;
            task_no = _task_no;
            ck = _ck;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void TQC_Task_Check_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (data1.Rows.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("choice_name");
                dt.Columns.Add("inspection_name");
                dt.Columns.Add("enum_value");
                dt.Columns.Add("standard_value");
                dt.Columns.Add("unit");
                dt.Columns.Add("remark");
                dt.Columns.Add("other_measures");
                dt.Columns.Add("source");
                dt.Columns.Add("file_url");
                dt.Columns.Add("task_no");
                foreach (DataRow item1 in data1.Rows)
                {
                    dt.Rows.Clear();
                    DataRow drr = dt.NewRow();
                    drr["id"] = item1["id"];
                    drr["choice_name"] = item1["choice_name"];
                    drr["inspection_name"] = item1["inspection_name"];
                    drr["enum_value"] = item1["enum_value"];
                    drr["standard_value"] = item1["standard_value"];
                    drr["unit"] = item1["unit"];
                    drr["remark"] = item1["remark"];
                    drr["other_measures"] = item1["other_measures"];
                    drr["source"] = item1["source"];
                    drr["file_url"] = item1["file_url"];
                    drr["task_no"] = task_no;
                    dt.Rows.Add(drr);
                    if (dt.Rows.Count > 0)
                    {
                        UC_DQLMQL_Check uc = new UC_DQLMQL_Check(dt,this,ck);
                        this.flowLayoutPanelTable.Controls.Add(uc);
                    }
                }

            }
        }
    }
}
