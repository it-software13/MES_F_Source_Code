using DataGrid.DataGridViewCustomColumn;
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

namespace SJeMES_QA.UControl
{
    public partial class UCTable : UserControl
    {
        public F_DQA_ShoeShape_trait_Main _p_main_frm;
        public F_QA_ShoeShape_List _p_list_frm;
        private DataTable data;
        public string _type = "";
        public UCTable(DataTable dt, F_DQA_ShoeShape_trait_Main p_main_frm, F_QA_ShoeShape_List p_list_frm,string type="")
        {
            InitializeComponent();
            data = dt;
            _type = type;
            _p_main_frm = p_main_frm;
            _p_list_frm = p_list_frm;
            if (_p_main_frm != null)
                button1.Visible = true;
            else
                button1.Visible = false;
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
                    dgvr.Cells["did"].Value = dr["did"].ToString();//did
                    dgvr.Cells["shoe_code"].Value = dr["shoe_code"].ToString();//鞋型
                    dgvr.Cells["choice_no"].Value = dr["choice_no"].ToString();//材料编号/工序代码
                    dgvr.Cells["choice_name"].Value = dr["choice_name"].ToString();//材料名称/工序名称

                    dgvr.Cells["qa_risk_desc"].Value = dr["qa_risk_desc"].ToString();//品质风险描述
                    dgvr.Cells["qa_risk_category_code"].Value = dr["qa_risk_category_name"].ToString();//品质风险类别
                    dgvr.Cells["art_codes"].Value = dr["art_codes"].ToString();//相关art
                    dgvr.Cells["phase_date"].Value = dr["phase_date"].ToString();//日期
                    dgvr.Cells["phase_creation_no"].Value = dr["phase_creation_no"].ToString();//阶段编号
                    dgvr.Cells["phase_creation_name"].Value = dr["phase_creation_name"].ToString();//阶段名称
                    dgvr.Cells["total_production"].Value = dr["total_production"].ToString();//生产总数
                    dgvr.Cells["bad_qty"].Value = dr["bad_qty"].ToString();//不良数
                    dgvr.Cells["bad_rate"].Value = dr["bad_rate"].ToString();//不良率
                    dgvr.Cells["measures"].Value = dr["measures"].ToString();//改善措施&行动方案
                    dgvr.Cells["measures_res"].Value = dr["measures_res"].ToString();//改善措施&行动方案
                    dgvr.Cells["remark"].Value = dr["remark"].ToString();//备注
                    dgvr.Cells["image_guid"].Value = dr["image_guid"].ToString();//图片guid
                    dgvr.Cells["is_dqa_mqa_band"].Value = dr["is_dqa_mqa_band"].ToString() == "1" ? "是" : "否";//
                    dgvr.Cells["img_name"].Value = dr["img_name"].ToString();//图片NAME
                    dgvr.Cells["img_url"].Value = dr["img_url"].ToString();//图片URL
                    dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();//图片URL
                    dgvr.Cells["qa_risk_details_desc"].Value = dr["qa_risk_details_desc"].ToString();//品质风险细项
                    i++;
                }
                int j = (this.dataGridView1.GetCellDisplayRectangle(this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex, true).Height) * (data.Rows.Count + 1);
                //this.dataGridView1.Height = j + 60;
                //this.Height = j + 60;
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
                            if(string.IsNullOrEmpty(_type))
                            {
                                DataTable imgdata = new DataTable();
                                imgdata.Columns.Add("img_name", typeof(string));
                                imgdata.Columns.Add("img_url", typeof(string));
                                DataRow dr = imgdata.NewRow();
                                dr["img_name"] = dataGridView1.Rows[e.RowIndex].Cells["img_name"].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells["img_name"].Value.ToString();
                                dr["img_url"] = Program.Client.PicUrl + (dataGridView1.Rows[e.RowIndex].Cells["img_url"].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells["img_url"].Value.ToString());
                                imgdata.Rows.Add(dr);
                                if (string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["img_url"].Value.ToString()))
                                {
                                    imgdata.Rows.Clear();
                                }

                                FrmImgList add = new FrmImgList(imgdata);
                                add.ShowDialog();
                            }
                            else
                            {
                                SJeMES_Control_Library.Forms.FrmImgList fil = new SJeMES_Control_Library.Forms.FrmImgList(Getimage_guid(dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString()), null, "");
                                fil.ShowDialog();
                            }
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string did = dataGridView1.Rows[0].Cells["did"].Value.ToString();
            string shoe_code = dataGridView1.Rows[0].Cells["shoe_code"].Value.ToString();
            using (F_DQA_ShoeShape_trait_Insert f =new F_DQA_ShoeShape_trait_Insert(did, shoe_code))
            {
                f.ShowDialog();
                _p_main_frm.GET_ShoeShapecenterView();
            }
        }


        /// <summary>
        /// 各阶段样品记录添加页面查询图片
        /// </summary>
        /// <returns></returns>
        public DataTable Getimage_guid(string image_guid)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("image_guid", image_guid);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.DQA_ShoeShape",//类名
                                        "Getimage_guidCopy",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }

            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示

            //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["img_url"] = Program.Client.PicUrl + dr["img_url"];
                }
            }
            return dt;
        }
    }
}
