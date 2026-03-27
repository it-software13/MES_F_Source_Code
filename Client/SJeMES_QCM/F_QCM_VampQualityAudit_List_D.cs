using DataGrid.DataGridViewCustomColumn;
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
    public partial class F_QCM_VampQualityAudit_List_D : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string guid_m = string.Empty;
        public F_QCM_VampQualityAudit_List_D(string _guid_m,string _SUPPLIERS_NAME,string _QUALITY_DATE)
        {
            InitializeComponent();
            guid_m = _guid_m;
            labcs.Text = _SUPPLIERS_NAME;
            labdate.Text = _QUALITY_DATE;
            GitM_VAMP_QUALITY_M();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation1"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        //鞋面品质审核列表明细查询
        public void GitM_VAMP_QUALITY_M()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("guid_m",guid_m);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.VampQualityAudit", "GitQCM_VAMP_QUALITY_D", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dataGridView1.Rows.Count >= 0)
                {
                    dataGridView1.Rows.Clear();
                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["QUALITY_ITEM_CODE"].Value = dr["QUALITY_ITEM_CODE"].ToString();
                        dgvr.Cells["QUALITY_ITEM_NAME"].Value = dr["QUALITY_ITEM_NAME"].ToString();
                        dgvr.Cells["BASE_SOCRE"].Value = dr["BASE_SOCRE"].ToString();
                        dgvr.Cells["SOCRE"].Value = dr["SOCRE"].ToString();
                        dgvr.Cells["QUALITY_TYPE_CODE"].Value = dr["QUALITY_TYPE_CODE"].ToString();
                        dgvr.Cells["QUALITY_TYPE_NAME"].Value = dr["QUALITY_TYPE_NAME"].ToString();
                        dgvr.Cells["GUID"].Value = dr["GUID"].ToString();
                        dgvr.Cells["TYPE"].Value = dr["TYPE"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_QCM_VampQualityAudit_List_D_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 查看上传QA鞋型品质问题点图片
        /// </summary>
        public DataTable GET_Qcm_qa_shoeshape_image(string D_GUID)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("D_GUID", D_GUID);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.VampQualityAudit", "GitQCM_VAMP_QUALITY_IMAGEURL", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["img_url"] = Program.Client.PicUrl + dr["img_url"];
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "operation1")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation1"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("SELECT"))//查看
                    {
                        string D_GUID = dataGridView1.Rows[e.RowIndex].Cells["GUID"].Value.ToString().Trim();
                        SJeMES_Control_Library.Forms.FrmImgList fil = new SJeMES_Control_Library.Forms.FrmImgList(GET_Qcm_qa_shoeshape_image(D_GUID));
                        fil.ShowDialog();
                    }
                }
            }
        }
    }
}
