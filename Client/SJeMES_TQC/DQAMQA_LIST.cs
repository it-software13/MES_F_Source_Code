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

namespace SJeMES_TQC
{
    public partial class DQAMQA_LIST : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string id = string.Empty;
        string source_type = string.Empty;
        string task_no = string.Empty;
        string cck = string.Empty;//查看
        public DQAMQA_LIST()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public DQAMQA_LIST(string _id, string _source_type, string _task_no, string _ck)
        {
            InitializeComponent();
            id = _id;
            source_type = _source_type;
            task_no = _task_no;
            cck = _ck;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// DQA管理页面添加页签查询工段
        /// </summary>
        /// <param name="OBJ"></param>
        /// <returns></returns>
        public void GetDQAMQA_history(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("keycode", textBox1.Text.Trim());
                data.Add("id", id);
                data.Add("task_no", task_no);
                data.Add("source_type", source_type);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",//类库名
                                            "SJ_TQCAPI.TQC_Task",//类名
                                            "GetDQAMQA_history",//方法名
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
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["qty"].Value = dr["qty"].ToString();
                        dgvr.Cells["q_qty"].Value = dr["q_qty"].ToString();
                        dgvr.Cells["bad_desc"].Value = dr["bad_desc"].ToString();
                        dgvr.Cells["check_res"].Value = dr["check_res"].ToString();
                        dgvr.Cells["imglist"].Value = dr["imglist"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void DQAMQA_LIST_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetDQAMQA_history;
            LoadPage();
            this.dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "ck")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["ck"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("select"))
                    {
                        var currRowFileDt = GetDQAMQA_history_img(dataGridView1.Rows[e.RowIndex].Cells["imglist"].Value.ToString());
                        if (cck == "true")
                        {
                            FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.APIURL, Program.Client.UserToken, "", true, false);
                            add.ShowDialog();
                            int i = 0;
                            string image_guids = string.Empty;
                            foreach (DataRow item in currRowFileDt.Rows)
                            {
                                image_guids += item["guid"];
                                if (i < currRowFileDt.Rows.Count - 1)
                                {
                                    image_guids += ",";
                                }
                                i++;
                            }
                            dataGridView1.Rows[e.RowIndex].Cells["imglist"].Value = image_guids;
                        }
                        else
                        {
                            FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.APIURL, Program.Client.UserToken, "");
                            add.ShowDialog();
                            int i = 0;
                            string image_guids = string.Empty;
                            foreach (DataRow item in currRowFileDt.Rows)
                            {
                                image_guids += item["guid"];
                                if (i < currRowFileDt.Rows.Count - 1)
                                {
                                    image_guids += ",";
                                }
                                i++;
                            }
                            dataGridView1.Rows[e.RowIndex].Cells["imglist"].Value = image_guids;
                        }

                        //SJeMES_Control_Library.Forms.FrmImgList fil = new SJeMES_Control_Library.Forms.FrmImgList(Getimage_guid(dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString()), null, "");
                        //fil.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// dqa&mqa核对查看历史查询图片
        /// </summary>
        /// <returns></returns>
        public DataTable GetDQAMQA_history_img(string image_guid)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("image_guid", image_guid);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TQCAPI",//类库名
                                        "SJ_TQCAPI.TQC_Task",//类名
                                        "GetDQAMQA_history_img",//方法名
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
                dt.Columns.Add("net_file_url", typeof(string));
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["file_url"].ToString()))
                    {
                        try
                        {
                            dr["net_file_url"] = Program.Client.PicUrl + dr["file_url"].ToString();
                        }
                        catch
                        {
                        }
                    }
                    i++;
                }
            }
            return dt;
        }
    }
}
