using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SJeMES_QCM
{
    public partial class F_QCM_InwarehouseAnomalyStat : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_InwarehouseAnomalyStat()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_InwarehouseAnomalyStat_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
        }


        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }


        /// <summary>
        /// 进仓异常材料统计展示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("SUPPLIERS_NAME", txt_SUPPLIERS_NAME.Text.Trim());
                p.Add("ITEM_TYPE_NAME", txt_ITEM_TYPE_NAME.Text.Trim().ToString());
                p.Add("Reject_ratioD", txt_min_thl.Text.Trim().ToString());
                p.Add("Reject_ratioS", txt_max_thl.Text.Trim().ToString());
                p.Add("acceptableD", txt5.Text.Trim());
                p.Add("acceptableS", txt6.Text.Trim().ToString());
                p.Add("Special_miningBLD", txt_min_tcbl.Text.Trim().ToString());
                p.Add("Special_miningBLS", txt_max_tcbl.Text.Trim().ToString());
                p.Add("Emergency_releaseBLD", txt_min_jjfxl.Text.Trim().ToString());
                p.Add("Emergency_releaseBLS", txt1_max_jjfxl.Text.Trim().ToString());

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.InwarehouseAnomalyStatBase",//类名
                                            "GetInwarehouseAnomalyStatList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                datagridview1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        datagridview1.Rows.Add();
                        DataGridViewRow dgvr = datagridview1.Rows[i];
                        dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();
                        dgvr.Cells["ITEM_TYPE_NAME"].Value = dr["ITEM_TYPE_NAME"].ToString();
                        dgvr.Cells["COUNT"].Value = dr["COUNT"].ToString();
                        dgvr.Cells["Bad_batch"].Value = dr["Bad_batch"].ToString();
                        dgvr.Cells["Reject_ratio"].Value = (dr["Reject_ratio"]+"%").ToString();
                        dgvr.Cells["PhysicalProperties"].Value = dr["PhysicalProperties"].ToString();
                        dgvr.Cells["ys"].Value = dr["ys"].ToString();
                        dgvr.Cells["gg"].Value = dr["gg"].ToString();
                        dgvr.Cells["czbl"].Value = dr["czbl"].ToString();
                        dgvr.Cells["qt"].Value = dr["qt"].ToString();
                        dgvr.Cells["SBad_batch"].Value = dr["SBad_batch"].ToString();
                        dgvr.Cells["TH"].Value = (dr["TH"]+"%").ToString();
                        dgvr.Cells["acceptable"].Value = (dr["acceptable"]+"%").ToString();
                        dgvr.Cells["ranking"].Value = dr["ranking"].ToString();
                        dgvr.Cells["Special_mining"].Value = dr["Special_mining"].ToString();
                        dgvr.Cells["Special_miningBL"].Value = dr["Special_miningBL"].ToString();
                        dgvr.Cells["Emergency_release"].Value = dr["Emergency_release"].ToString();
                        dgvr.Cells["Emergency_releaseBL"].Value = dr["Emergency_releaseBL"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(datagridview1);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                datagridview1.ClearSelection();

                GenClass.AutoSizeColumn(datagridview1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void datagridview1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
    }
}
