using DataGrid.DataGridViewCustomColumn;
using SJeMES_Control_Library.Forms;
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

namespace SJeMES_BDM.UControl
{
    public partial class UCTable : UserControl
    {

        private DataTable data;
        public UCTable(DataTable dt)
        {
            InitializeComponent();
            data = dt;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void UCTable_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            if (data.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in data.Rows)
                {
                    dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    dgvr.Cells["Itemnumber"].Value = dr["itemnumber"].ToString();//项次
                    dgvr.Cells["choice_no"].Value = dr["choice_no"].ToString();//材料编号/工序代码
                    dgvr.Cells["choice_name"].Value = dr["choice_name"].ToString();//材料名称/工序名称

                    dgvr.Cells["qa_risk_desc"].Value = dr["qa_risk_desc"].ToString();//品质风险描述
                    dgvr.Cells["qa_risk_category_code"].Value = dr["qa_risk_category_code"].ToString();//品质风险类别
                    dgvr.Cells["art_codes"].Value = dr["art_codes"].ToString();//相关art
                    dgvr.Cells["phase_date"].Value = dr["phase_date"].ToString();//日期
                    dgvr.Cells["phase_creation_no"].Value = dr["phase_creation_no"].ToString();//阶段编号
                    dgvr.Cells["total_production"].Value = dr["total_production"].ToString();//生产总数
                    dgvr.Cells["bad_qty"].Value = dr["bad_qty"].ToString();//不良数
                    dgvr.Cells["bad_rate"].Value = dr["bad_rate"].ToString();//不良率
                    dgvr.Cells["measures"].Value = dr["measures"].ToString();//改善措施&行动方案
                    dgvr.Cells["person_in_charge"].Value = dr["person_in_charge"].ToString();//负责人
                    dgvr.Cells["image_guid"].Value = dr["image_guid"].ToString();//图片guid
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
            try
            {
                if (e.RowIndex < 0)
                    return;
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("DETAIL"))//查看
                        {
                            FrmImgList add = new FrmImgList();
                            add.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
