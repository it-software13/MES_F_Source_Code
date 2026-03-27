using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_AQL
{
    public partial class F_AQL_ShoeMaterial_Composition_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_AQL_ShoeMaterial_Composition_List(DataTable dt_item)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);


            dgvData.Rows.Clear();
            if (dt_item != null && dt_item.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow item in dt_item.Rows)
                {
                    dgvData.Rows.Add();
                    DataGridViewRow dgvr = dgvData.Rows[i];
                    dgvr.Cells["ZJJ"].Value = item["ZJJ"].ToString();//Season 季节
                    dgvr.Cells["MAKTX"].Value = item["MAKTX"].ToString();//Model Name 鞋型
                    dgvr.Cells["MIIDS"].Value = item["MIIDS"].ToString();//Material information in descending sequence 材料信息降序排列
                    dgvr.Cells["SUM_TOTAL"].Value = item["SUM_TOTAL"].ToString();//Total % 占比
                    dgvr.Cells["SUM_TOTAL_CHECK"].Value = item["SUM_TOTAL_CHECK"].ToString();//100% checking for material info 100%检查材料信息
                    dgvr.Cells["颜色代码"].Value = "no value";
                    dgvr.Cells["鞋面颜色名称"].Value = "no value";
                    dgvr.Cells["Date_Change_date"].Value = item["Date_Change_date"].ToString();//Date Change date 变更日期
                    dgvr.Cells["ZKFFZR_NM"].Value = item["ZKFFZR_NM"].ToString();//Developer 开发员
                    dgvr.Cells["ZBM_X"].Value = item["ZBM_X"].ToString();//Department 部门
                    dgvr.Cells["ZSTATUS_NM"].Value = item["ZSTATUS_NM"].ToString();//Status 状况
                    dgvr.Cells["ZCOL1_NM"].Value = item["ZCOL1_NM"].ToString();//Footwear ankle covering 脚踝包裹
                    dgvr.Cells["ZCOL2_NM"].Value = item["ZCOL2_NM"].ToString();//FTW Outsole material 底部材料
                    dgvr.Cells["ZCOL3_NM"].Value = item["ZCOL3_NM"].ToString();//FTW lining 内里
                    dgvr.Cells["ZCOL4_NM"].Value = item["ZCOL4_NM"].ToString();//Tongue label position 鞋舌尺码标位置
                    dgvr.Cells["ZCOL5_NM"].Value = item["ZCOL5_NM"].ToString();//Vulcanized or not for FTW 是否被硫化
                    dgvr.Cells["ZCOL6_NM"].Value = item["ZCOL6_NM"].ToString();//Inlay Sole(中底布) 鞋舌尺码标位置

                    i++;
                }
            }

        }
    }
}
