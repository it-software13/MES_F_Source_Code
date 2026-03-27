using SJeMES_Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QA.UControl
{
    public partial class UCTableCheck : UserControl
    {
        private DataTable data;
        private F_DQA_ShoeShape_trait_Edit ste;
        public UCTableCheck(DataTable dt, F_DQA_ShoeShape_trait_Edit _ste)
        {
            InitializeComponent();
            data = dt;
            ste = _ste;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void UCTableCheck_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            if (data.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in data.Rows)
                {
                    dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    dgvr.Cells["itemid"].Value = dr["itemid"].ToString();//鞋型品质记录——品质状况——详情id
                    dgvr.Cells["Itemnumber"].Value = dr["itemnumber"].ToString();//项次
                    dgvr.Cells["choice_no"].Value = dr["choice_no"].ToString();//材料编号/工序代码
                    dgvr.Cells["choice_name"].Value = dr["choice_name"].ToString();//材料名称/工序名称
                    dgvr.Cells["shoes_code"].Value = dr["shoes_code"].ToString();//鞋型
                    dgvr.Cells["qa_risk_desc"].Value = dr["qa_risk_desc"].ToString();//品质风险描述
                    dgvr.Cells["qa_risk_category_code"].Value = dr["qa_risk_category_code"].ToString();//品质风险类别
                    dgvr.Cells["qa_risk_category_name"].Value = dr["qa_risk_category_name"].ToString();//品质风险类别
                    dgvr.Cells["art_codes"].Value = dr["art_codes"].ToString();//相关art
                    dgvr.Cells["phase_date"].Value = dr["phase_date"].ToString();//日期
                    dgvr.Cells["phase_creation_no"].Value = dr["phase_creation_no"].ToString();//阶段编号
                    dgvr.Cells["phase_creation_name"].Value = dr["phase_creation_name"].ToString();//阶段名称
                    dgvr.Cells["total_production"].Value = dr["total_production"].ToString();//生产总数
                    dgvr.Cells["bad_qty"].Value = dr["bad_qty"].ToString();//不良数
                    dgvr.Cells["bad_rate"].Value = dr["bad_rate"].ToString();//不良率
                    dgvr.Cells["measures"].Value = dr["measures"].ToString();//改善措施&行动方案
                    dgvr.Cells["measures_res"].Value = dr["measures_res"].ToString();//改善措施结果
                    dgvr.Cells["remark"].Value = dr["remark"].ToString();//负责人
                    dgvr.Cells["image_guid"].Value = dr["image_guid"].ToString();//图片guid
                    dgvr.Cells["is_dqa_mqa_band"].Value = dr["is_dqa_mqa_band"].ToString()=="1"? "Yes" : "No";//

                    dgvr.Cells["workshop_section_no"].Value = dr["workshop_section_no"].ToString();//
                    dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();//
                    dgvr.Cells["qa_risk_details_desc"].Value = dr["qa_risk_details_desc"].ToString();

                    i++;
                }
                int j = (this.dataGridView1.GetCellDisplayRectangle(this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex, true).Height) * (data.Rows.Count + 1);
                this.dataGridView1.Height = j + 60;
                this.Height = j + 60;
                dataGridView1.ClearSelection();

            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "xz")
            {
                if (this.dataGridView1[e.ColumnIndex, e.RowIndex].Value == null)
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells["xz"].Value = false;
                }
                if (this.dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper() == "TRUE")
                {
                    this.dataGridView1[e.ColumnIndex, e.RowIndex].Value = false;
                    dataGridView1.SelectAll();
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string _selectValue = dataGridView1.Rows[i].Cells["xz"].EditedFormattedValue.ToString();
                        if (_selectValue == "False")
                        {
                            ste._itemid.Remove(dataGridView1.Rows[i].Cells["itemid"].Value.ToString());
                        }
                        //如果CheckBox已选中，则在此处继续编写代码
                    }
                    ste.Getimage_guidItem(ste._itemid);
                }
                else
                {
                    ste._itemid.Clear();
                    this.dataGridView1[e.ColumnIndex, e.RowIndex].Value = true;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string _selectValue = dataGridView1.Rows[i].Cells["xz"].EditedFormattedValue.ToString();
                        if (_selectValue == "True")
                        {
                            ste._itemid.Add(dataGridView1.Rows[i].Cells["itemid"].Value.ToString());
                        }
                        //如果CheckBox已选中，则在此处继续编写代码
                    }
                    ste.Getimage_guidItem(ste._itemid);
                }

            }
            getTotal();
        }

        /// <summary>
        /// 统计
        /// </summary>
        private int getTotal()
        {
            int mCount = 0;
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                if ((dataGridView1.Rows[i].Cells["xz"].Value + "").ToUpper() == "TRUE")
                {
                    mCount++;
                }
            }
            return mCount;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int count = 0; count < this.dataGridView1.Rows.Count; count++)
                {
                    this.dataGridView1.Rows[count].Cells["xz"].Value = true;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string _selectValue = dataGridView1.Rows[i].Cells["xz"].EditedFormattedValue.ToString();
                    if (_selectValue == "True")
                    {
                        ste._itemid.Add(dataGridView1.Rows[i].Cells["itemid"].Value.ToString());
                    }
                    //如果CheckBox已选中，则在此处继续编写代码
                }
                ste.Getimage_guidItem(ste._itemid);
            }
            else
            {
                for (int count = 0; count < this.dataGridView1.Rows.Count; count++)
                {
                    this.dataGridView1.Rows[count].Cells["xz"].Value = false;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string _selectValue = dataGridView1.Rows[i].Cells["xz"].EditedFormattedValue.ToString();
                    if (_selectValue == "False")
                    {
                        ste._itemid.Remove(dataGridView1.Rows[i].Cells["itemid"].Value.ToString());
                    }
                    //如果CheckBox已选中，则在此处继续编写代码
                }
                ste.Getimage_guidItem(ste._itemid);
            }
            getTotal();
        }
    }
}
