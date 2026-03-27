using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class F_QCM_BATCH_PRODUCTION_Detail : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        DataGridViewRow _dr;
        public F_QCM_BATCH_PRODUCTION_Detail(DataGridViewRow dr)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _dr = dr;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public string ID = "";
        private void F_QCM_BATCH_PRODUCTION_Detail_Load(object sender, EventArgs e)
        {
            if (_dr != null)
            {
                ID = _dr.Cells["ID"].Value.ToString();
                txt_batch_code.Enabled = false;
                txt_batch_code.Text = _dr.Cells["量试编号"].Value.ToString();
                txt_kfjd.Text = _dr.Cells["开发季度"].Value.ToString();
                txt_type.Text = _dr.Cells["类别"].Value.ToString();
                txt_art.Text = _dr.Cells["ART"].Value.ToString();
                dtp_batch_date.Value = DateTime.Parse(_dr.Cells["量试日期"].Value.ToString());
                dtp_production_date.Value = DateTime.Parse(_dr.Cells["生产日期"].Value.ToString());
                txt_shoe_name.Text = _dr.Cells["鞋型名称"].Value.ToString();
                txt_ddmh.Text = _dr.Cells["大底模号"].Value.ToString();
                txt_size_double.Text = _dr.Cells["试作SIZE_双数"].Value.ToString();
                txt_color.Text = _dr.Cells["配色"].Value.ToString();
                txt_shoe_last.Text = _dr.Cells["楦头"].Value.ToString();
                txt_procedure.Text = _dr.Cells["工艺"].Value.ToString();
                txt_zzhq.Text = _dr.Cells["组长会签"].Value.ToString();
                txt_department.Text = _dr.Cells["执行部门"].Value.ToString();
            }

            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("BATCH_CODE", txt_batch_code.Text.Trim());
            p.Add("type", "1");

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.BatchProduction",//类名
                                        "GetProblemDetail",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                dataGridView1.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    int i = dataGridView1.Rows.Add();

                    dataGridView1.Rows[i].Cells["问题点"].Value = dr["PROBLEM"].ToString();
                    dataGridView1.Rows[i].Cells["附件1"].Value = "查看";
                    dataGridView1.Rows[i].Cells["解决方式"].Value = dr["SOLUTION"].ToString();
                    dataGridView1.Rows[i].Cells["附件2"].Value = "查看";
                    dataGridView1.Rows[i].Cells["PROBLEM_IMG"].Value = dr["PROBLEM_IMG"].ToString();
                    dataGridView1.Rows[i].Cells["SOLUTION_IMG"].Value = dr["SOLUTION_IMG"].ToString();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "附件1")
            {
                string FILE_URL = this.dataGridView1.Rows[e.RowIndex].Cells["PROBLEM_IMG"].Value.ToString().Trim();

                DataTable dt = new DataTable();
                dt.Columns.Add("img_url");
                dt.Columns.Add("img_name");
                if (!string.IsNullOrEmpty(FILE_URL))
                {
                    var list = FILE_URL.Split(',').ToList();
                    foreach (var item in list)
                    {
                        DataRow dr = dt.NewRow();
                        dr["img_url"] = Program.Client.PicUrl + item.Split('|')[1];
                        dr["img_name"] = item.Split('|')[0];
                        dt.Rows.Add(dr);
                    }
                }

                SJeMES_Control_Library.Forms.FrmImgList frm = new SJeMES_Control_Library.Forms.FrmImgList(dt);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();

            }
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "附件2")
            {
                string FILE_URL = this.dataGridView1.Rows[e.RowIndex].Cells["SOLUTION_IMG"].Value.ToString().Trim();

                DataTable dt = new DataTable();
                dt.Columns.Add("img_url");
                dt.Columns.Add("img_name");
                if (!string.IsNullOrEmpty(FILE_URL))
                {
                    var list = FILE_URL.Split(',').ToList();
                    foreach (var item in list)
                    {
                        DataRow dr = dt.NewRow();
                        dr["img_url"] = Program.Client.PicUrl + item.Split('|')[1];
                        dr["img_name"] = item.Split('|')[0];
                        dt.Rows.Add(dr);
                    }
                }
                SJeMES_Control_Library.Forms.FrmImgList frm = new SJeMES_Control_Library.Forms.FrmImgList(dt);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();


            }
        }
    }
}
