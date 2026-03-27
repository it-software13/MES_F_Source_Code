using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SJeMES_QCM
{
    public partial class F_QCM_SystemFileMaintenance_Main : MaterialForm
    {
        
        private readonly MaterialSkinManager materialSkinManager;

        DataTable dt = new DataTable();
        public F_QCM_SystemFileMaintenance_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            dt.Columns.Add("文件类型", typeof(string));
            dt.Columns.Add("有效文件", typeof(string));
            dt.Columns.Add("上传时间", typeof(string));
            dt.Columns.Add("文件路径", typeof(string));
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }




        private void F_QCM_SystemFileMaintenance_Main_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //#region 初始化
            //DataRow dr = dt.NewRow();
            //dr["文件类型"] = "adidas文件";
            //dr["有效文件"] = "File/常规鞋带政策 A0356 V4.pdf";
            //dr["上传时间"] = "2021-11-18";
            //dt.Rows.Add(dr);
            ////DataRow dr1 = dt.NewRow();
            ////dr1["文件类型"] = "adidas文件";
            ////dr1["有效文件"] = "File/鞋业B、C品_A0216_FTW_B_C_Grade.pdf";
            ////dr1["上传时间"] = "2021-11-19";
            ////dt.Rows.Add(dr1);

            //DataRow dr2 = dt.NewRow();
            //dr2["文件类型"] = "WI";
            //dr2["有效文件"] = "File/01-电绣检验工作说明 A.0.pdf";
            //dr2["上传时间"] = "2021-11-18";
            //dt.Rows.Add(dr2);

            //DataRow dr3= dt.NewRow();
            //dr3["文件类型"] = "WI";
            //dr3["有效文件"] = "File/03-高周波检验工作说明 C.3.pdf";
            //dr3["上传时间"] = "2021-11-20";
            //dt.Rows.Add(dr3);

            ////DataRow dr4 = dt.NewRow();
            ////dr4["文件类型"] = "培训文件";
            ////dr4["有效文件"] = "File/BA 培训讲义 -2021.03.31.pptx";
            ////dr4["上传时间"] = "2021-11-20";
            ////dt.Rows.Add(dr4);

            //DataRow dr5 = dt.NewRow();
            //dr5["文件类型"] = "培训文件";
            //dr5["有效文件"] = "File/BA证书模板（结业证书）.pdf";
            //dr5["上传时间"] = "2021-11-18";
            //dt.Rows.Add(dr5);

            ////DataRow dr6 = dt.NewRow();
            ////dr6["文件类型"] = "品质报告";
            ////dr6["有效文件"] = "File/2021.10.25 APE品质月会.pptx";
            ////dr6["上传时间"] = "2021-11-17";
            ////dt.Rows.Add(dr6);

            //DataRow dr7 = dt.NewRow();
            //dr7["文件类型"] = "品质报告";
            //dr7["有效文件"] = "File/aJ Footwear Quality Feedback Report_2021 Q1 翻译.pdf";
            //dr7["上传时间"] = "2021-11-19";
            //dt.Rows.Add(dr7);

            //DataRow dr8 = dt.NewRow();
            //dr8["文件类型"] = "品质流程";
            //dr8["有效文件"] = "File/72 电子称校正SOP-A4.pdf";
            //dr8["上传时间"] = "2021-11-18";
            //dt.Rows.Add(dr8);

            //DataRow dr9 = dt.NewRow();
            //dr9["文件类型"] = "品质流程";
            //dr9["有效文件"] = "File/XTM袜套测试.pdf";
            //dr9["上传时间"] = "2021-11-17";
            //dt.Rows.Add(dr9);

            //DataRow dr10 = dt.NewRow();
            //dr10["文件类型"] = "品质目标";
            //dr10["有效文件"] = "File/FACT 2021 H1 KPI targets - For T1 Rollout.pdf";
            //dr10["上传时间"] = "2021-11-18";
            //dt.Rows.Add(dr10);

            //DataRow dr11 = dt.NewRow();
            //dr11["文件类型"] = "品质目标";
            //dr11["有效文件"] = "File/FTW Q-KPI Handbook Version 02 _ March 31st 2021.pdf";
            //dr11["上传时间"] = "2021-11-18";
            //dt.Rows.Add(dr11);

            //DataRow dr12= dt.NewRow();
            //dr12["文件类型"] = "品质制度";
            //dr12["有效文件"] = "File/028AO特采材料管控制度2018.doc";
            //dr12["上传时间"] = "2021-11-18";
            //dt.Rows.Add(dr12);

            //DataRow dr13 = dt.NewRow();
            //dr13["文件类型"] = "品质制度";
            //dr13["有效文件"] = "File/QDM系统图片拍摄与管控制度.pdf";
            //dr13["上传时间"] = "2021-11-18";
            //dt.Rows.Add(dr13);

            //DataRow dr14 = dt.NewRow();
            //dr14["文件类型"] = "组织架构";
            //dr14["有效文件"] = "File/万邦QIP组织架构图.xlsx";
            //dr14["上传时间"] = "2021-11-18";
            //dt.Rows.Add(dr14);

            //#endregion 

            #region 初始化
            DataRow dr = dt.NewRow();
            dr["文件类型"] = "adidas文件";
            dr["有效文件"] = "常规鞋带政策 A0356 V4.pdf";
            dr["文件路径"] = "File/常规鞋带政策 A0356 V4.pdf";
            dr["上传时间"] = "2021-11-18";
            dt.Rows.Add(dr);
            //DataRow dr1 = dt.NewRow();
            //dr1["文件类型"] = "adidas文件";
            //dr1["有效文件"] = "File/鞋业B、C品_A0216_FTW_B_C_Grade.pdf";
            //dr1["上传时间"] = "2021-11-19";
            //dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["文件类型"] = "WI";
            dr2["有效文件"] = "01-电绣检验工作说明 A.0.pdf";
            dr2["文件路径"] = "File/01-电绣检验工作说明 A.0.pdf";
            dr2["上传时间"] = "2021-11-18";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["文件类型"] = "WI";
            dr3["有效文件"] = "03-高周波检验工作说明 C.3.pdf";
            dr3["文件路径"] = "File/03-高周波检验工作说明 C.3.pdf";
            dr3["上传时间"] = "2021-11-20";
            dt.Rows.Add(dr3);

            //DataRow dr4 = dt.NewRow();
            //dr4["文件类型"] = "培训文件";
            //dr4["有效文件"] = "File/BA 培训讲义 -2021.03.31.pptx";
            //dr4["上传时间"] = "2021-11-20";
            //dt.Rows.Add(dr4);

            DataRow dr5 = dt.NewRow();
            dr5["文件类型"] = "培训文件";
            dr5["有效文件"] = "BA证书模板（结业证书）.pdf";
            dr5["文件路径"] = "File/BA证书模板（结业证书）.pdf";
            dr5["上传时间"] = "2021-11-18";
            dt.Rows.Add(dr5);

            //DataRow dr6 = dt.NewRow();
            //dr6["文件类型"] = "品质报告";
            //dr6["有效文件"] = "File/2021.10.25 APE品质月会.pptx";
            //dr6["上传时间"] = "2021-11-17";
            //dt.Rows.Add(dr6);

            DataRow dr7 = dt.NewRow();
            dr7["文件类型"] = "品质报告";
            dr7["有效文件"] = "aJ Footwear Quality Feedback Report_2021 Q1 翻译.pdf";
            dr7["文件路径"] = "File/aJ Footwear Quality Feedback Report_2021 Q1 翻译.pdf";
            dr7["上传时间"] = "2021-11-19";
            dt.Rows.Add(dr7);

            DataRow dr8 = dt.NewRow();
            dr8["文件类型"] = "品质流程";
            dr8["有效文件"] = "72 电子称校正SOP-A4.pdf";
            dr8["文件路径"] = "File/72 电子称校正SOP-A4.pdf";
            dr8["上传时间"] = "2021-11-18";
            dt.Rows.Add(dr8);

            DataRow dr9 = dt.NewRow();
            dr9["文件类型"] = "品质流程";
            dr9["有效文件"] = "XTM袜套测试.pdf";
            dr9["文件路径"] = "File/XTM袜套测试.pdf";
            dr9["上传时间"] = "2021-11-17";
            dt.Rows.Add(dr9);

            DataRow dr10 = dt.NewRow();
            dr10["文件类型"] = "品质目标";
            dr10["有效文件"] = "FACT 2021 H1 KPI targets - For T1 Rollout.pdf";
            dr10["文件路径"] = "File/FACT 2021 H1 KPI targets - For T1 Rollout.pdf";
            dr10["上传时间"] = "2021-11-18";
            dt.Rows.Add(dr10);

            DataRow dr11 = dt.NewRow();
            dr11["文件类型"] = "品质目标";
            dr11["有效文件"] = "FTW Q-KPI Handbook Version 02 _ March 31st 2021.pdf";
            dr11["文件路径"] = "File/FTW Q-KPI Handbook Version 02 _ March 31st 2021.pdf";
            dr11["上传时间"] = "2021-11-18";
            dt.Rows.Add(dr11);

            DataRow dr12 = dt.NewRow();
            dr12["文件类型"] = "品质制度";
            dr12["有效文件"] = "028AO特采材料管控制度2018.doc";
            dr12["文件路径"] = "File/028AO特采材料管控制度2018.doc";
            dr12["上传时间"] = "2021-11-18";
            dt.Rows.Add(dr12);

            DataRow dr13 = dt.NewRow();
            dr13["文件类型"] = "品质制度";
            dr13["有效文件"] = "QDM系统图片拍摄与管控制度.pdf";
            dr13["文件路径"] = "File/QDM系统图片拍摄与管控制度.pdf";
            dr13["上传时间"] = "2021-11-18";
            dt.Rows.Add(dr13);

            DataRow dr14 = dt.NewRow();
            dr14["文件类型"] = "组织架构";
            dr14["有效文件"] = "万邦QIP组织架构图.xlsx";
            dr14["文件路径"] = "File/万邦QIP组织架构图.xlsx";
            dr14["上传时间"] = "2021-11-18";
            dt.Rows.Add(dr14);

            #endregion

            //var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow cc in dt.Rows)
                {
                    dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    dgvr.Cells["文件类型"].Value = cc["文件类型"].ToString();
                    dgvr.Cells["有效文件"].Value = cc["有效文件"].ToString();
                    dgvr.Cells["文件路径"].Value = cc["文件路径"].ToString();
                    dgvr.Cells["上传时间"].Value = cc["上传时间"].ToString();

                    i++;
                }
            }
            //this.dataGridView1.DataSource = dt;
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            GenClass.AutoSizeColumn(dataGridView1);
        }

        private void addbtn_Click(object sender, EventArgs e)
        {

            
            F_QCM_SystemFileMaintenance_Add FrmAdd = new F_QCM_SystemFileMaintenance_Add(dt);
            FrmAdd.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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
                        return;
                    if (cell.CurrentItem.Equals("delete"))
                    {
                        if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                        }
                            
                    }
                    if(cell.CurrentItem == "selectfile")
                    {
                        string url = Convert.ToString(dataGridView1.CurrentRow.Cells["文件路径"].Value);
                         
                        //FrmShowFile add2 = new FrmShowFile(@"http://192.168.1.123:8066/" + url);
                        FrmShowFile add2 = new FrmShowFile(Program.Client.PicUrl +"/"+ url);
                        //FrmFileList add = new FrmFileList(newdt, Program.Client.APIURL, Program.Client.UserToken);
                        add2.ShowDialog();
                    }

                }
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            DataTable newdt = new DataTable();
            string WHERE = string.Empty;

            if (!string.IsNullOrEmpty(comboBox1.Text))
                WHERE += $@"and 文件类型 LIKE '%{comboBox1.Text}%'";

            if (!string.IsNullOrEmpty(txt_file_name.Text))
                WHERE += $@"and 有效文件 LIKE '%{txt_file_name.Text}%'";


            if (!string.IsNullOrEmpty(WHERE))
                WHERE = WHERE.Remove(WHERE.IndexOf("and"), 3);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (comboBox1.Text.Trim() == "" && txt_file_name.Text.Trim() == "" )
                {
                    dt.Rows.Clear();
                    dataGridView1.Rows.Clear();
                    #region 初始化
                    DataRow dr = dt.NewRow();
                    dr["文件类型"] = "adidas文件";
                    dr["有效文件"] = "常规鞋带政策 A0356 V4.pdf";
                    dr["文件路径"] = "File/常规鞋带政策 A0356 V4.pdf";
                    dr["上传时间"] = "2021-11-18";
                    dt.Rows.Add(dr);
                    //DataRow dr1 = dt.NewRow();
                    //dr1["文件类型"] = "adidas文件";
                    //dr1["有效文件"] = "File/鞋业B、C品_A0216_FTW_B_C_Grade.pdf";
                    //dr1["上传时间"] = "2021-11-19";
                    //dt.Rows.Add(dr1);

                    DataRow dr2 = dt.NewRow();
                    dr2["文件类型"] = "WI";
                    dr2["有效文件"] = "01-电绣检验工作说明 A.0.pdf";
                    dr2["文件路径"] = "File/01-电绣检验工作说明 A.0.pdf";
                    dr2["上传时间"] = "2021-11-18";
                    dt.Rows.Add(dr2);

                    DataRow dr3 = dt.NewRow();
                    dr3["文件类型"] = "WI";
                    dr3["有效文件"] = "03-高周波检验工作说明 C.3.pdf";
                    dr3["文件路径"] = "File/03-高周波检验工作说明 C.3.pdf";
                    dr3["上传时间"] = "2021-11-20";
                    dt.Rows.Add(dr3);

                    //DataRow dr4 = dt.NewRow();
                    //dr4["文件类型"] = "培训文件";
                    //dr4["有效文件"] = "File/BA 培训讲义 -2021.03.31.pptx";
                    //dr4["上传时间"] = "2021-11-20";
                    //dt.Rows.Add(dr4);

                    DataRow dr5 = dt.NewRow();
                    dr5["文件类型"] = "培训文件";
                    dr5["有效文件"] = "BA证书模板（结业证书）.pdf";
                    dr5["文件路径"] = "File/BA证书模板（结业证书）.pdf";
                    dr5["上传时间"] = "2021-11-18";
                    dt.Rows.Add(dr5);

                    //DataRow dr6 = dt.NewRow();
                    //dr6["文件类型"] = "品质报告";
                    //dr6["有效文件"] = "File/2021.10.25 APE品质月会.pptx";
                    //dr6["上传时间"] = "2021-11-17";
                    //dt.Rows.Add(dr6);

                    DataRow dr7 = dt.NewRow();
                    dr7["文件类型"] = "品质报告";
                    dr7["有效文件"] = "aJ Footwear Quality Feedback Report_2021 Q1 翻译.pdf";
                    dr7["文件路径"] = "File/aJ Footwear Quality Feedback Report_2021 Q1 翻译.pdf";
                    dr7["上传时间"] = "2021-11-19";
                    dt.Rows.Add(dr7);

                    DataRow dr8 = dt.NewRow();
                    dr8["文件类型"] = "品质流程";
                    dr8["有效文件"] = "72 电子称校正SOP-A4.pdf";
                    dr8["文件路径"] = "File/72 电子称校正SOP-A4.pdf";
                    dr8["上传时间"] = "2021-11-18";
                    dt.Rows.Add(dr8);

                    DataRow dr9 = dt.NewRow();
                    dr9["文件类型"] = "品质流程";
                    dr9["有效文件"] = "XTM袜套测试.pdf";
                    dr9["文件路径"] = "File/XTM袜套测试.pdf";
                    dr9["上传时间"] = "2021-11-17";
                    dt.Rows.Add(dr9);

                    DataRow dr10 = dt.NewRow();
                    dr10["文件类型"] = "品质目标";
                    dr10["有效文件"] = "FACT 2021 H1 KPI targets - For T1 Rollout.pdf";
                    dr10["文件路径"] = "File/FACT 2021 H1 KPI targets - For T1 Rollout.pdf";
                    dr10["上传时间"] = "2021-11-18";
                    dt.Rows.Add(dr10);

                    DataRow dr11 = dt.NewRow();
                    dr11["文件类型"] = "品质目标";
                    dr11["有效文件"] = "FTW Q-KPI Handbook Version 02 _ March 31st 2021.pdf";
                    dr11["文件路径"] = "File/FTW Q-KPI Handbook Version 02 _ March 31st 2021.pdf";
                    dr11["上传时间"] = "2021-11-18";
                    dt.Rows.Add(dr11);

                    DataRow dr12 = dt.NewRow();
                    dr12["文件类型"] = "品质制度";
                    dr12["有效文件"] = "028AO特采材料管控制度2018.doc";
                    dr12["文件路径"] = "File/028AO特采材料管控制度2018.doc";
                    dr12["上传时间"] = "2021-11-18";
                    dt.Rows.Add(dr12);

                    DataRow dr13 = dt.NewRow();
                    dr13["文件类型"] = "品质制度";
                    dr13["有效文件"] = "QDM系统图片拍摄与管控制度.pdf";
                    dr13["文件路径"] = "File/QDM系统图片拍摄与管控制度.pdf";
                    dr13["上传时间"] = "2021-11-18";
                    dt.Rows.Add(dr13);

                    DataRow dr14 = dt.NewRow();
                    dr14["文件类型"] = "组织架构";
                    dr14["有效文件"] = "万邦QIP组织架构图.xlsx";
                    dr14["文件路径"] = "File/万邦QIP组织架构图.xlsx";
                    dr14["上传时间"] = "2021-11-18";
                    dt.Rows.Add(dr14);

                    #endregion
                    //this.dataGridView1.DataSource = dt;
                    int i = 0;
                    foreach (DataRow cc in dt.Rows)
                    {
                        
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["文件类型"].Value = cc["文件类型"].ToString();
                        dgvr.Cells["有效文件"].Value = cc["有效文件"].ToString();
                        dgvr.Cells["上传时间"].Value = cc["上传时间"].ToString();

                        i++;
                    }
                }
                else
                {
                    newdt = dt.Clone();

                    DataRow[] dr = dt.Select(WHERE);
                    for (int z = 0; z < dr.Length; z++)
                    {
                        newdt.ImportRow((DataRow)dr[z]);
                    }
                    this.dataGridView1.Rows.Clear();
                    int i = 0;
                    foreach (DataRow cc in newdt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["文件类型"].Value = cc["文件类型"].ToString();
                        dgvr.Cells["有效文件"].Value = cc["有效文件"].ToString();
                        dgvr.Cells["上传时间"].Value = cc["上传时间"].ToString();

                        i++;
                    }
                    //this.dataGridView1.DataSource = newdt;
                }


            }
        }
    }
}
