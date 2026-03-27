using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public partial class F_IQC_Bad_Report_Read : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_IQC_Bad_Report_Read(DataTable dt)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
         Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            GetView(dt);
        }

        public void GetView(DataTable dt) 
        {

            foreach (DataRow item in dt.Rows)
            { //枚举 0：QIP总巡检核准；1：会签业务/仓库股长；2：QIP材料助理复核；3：检验员判定
                int index=dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["Column1"].Value = item["CONFIRM_BY"].ToString();
                string dom = string.Empty;
                //switch (item["DEPARTMENT"].ToString())
                //{
                //    case "0":
                //        dom = "QIP总巡检核准";
                //        break;
                //    case "1":
                //        dom = "会签业务/仓库股长";
                //        break;
                //    case "2":
                //        dom = "QIP材料助理复核";
                //        break;
                //    case "3":
                //        dom = "检验员判定";
                //        break;
                //} 
                switch (item["DEPARTMENT"].ToString())
                {
                    case "0":
                        dom = "QIP General Inspection Approval";
                        break;
                    case "1":
                        dom = "Countersign business/warehousing chief";
                        break;
                    case "2":
                        dom = "QIP material assistant review";
                        break;
                    case "3":
                        dom = "Inspector judgment";
                        break;
                }
                dataGridView1.Rows[index].Cells["Column2"].Value = dom;

                if (item["ISDELETE"].ToString()==""|| item["ISDELETE"].ToString() == "0")
                {
                    //dataGridView1.Rows[index].Cells["Column4"].Value = "签名";
                    dataGridView1.Rows[index].Cells["Column4"].Value = "Sign";
                    dataGridView1.Rows[index].Cells["Column3"].Value = item["CREATEDATE"].ToString() + " " + item["CREATETIME"].ToString();
                }
                else 
                {
                    //dataGridView1.Rows[index].Cells["Column4"].Value = "取消签名";
                    dataGridView1.Rows[index].Cells["Column4"].Value = "Cancel signature";
                    dataGridView1.Rows[index].Cells["Column3"].Value = item["DELETEDATE"].ToString() + " " + item["DELETETIME"].ToString();
                }
            }
        }
        
    }
}
