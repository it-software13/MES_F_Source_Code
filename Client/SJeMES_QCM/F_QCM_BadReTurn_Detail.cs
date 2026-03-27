using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
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
    public partial class F_QCM_BadReTurn_Detail : MaterialForm
    {
        public DataTable _dt { get; set; }
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_BadReTurn_Detail(DataTable dt)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _dt = dt;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_QCM_BadReTurn_Detail_Load(object sender, EventArgs e)
        {
            GetDataList();

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    txt_RETURN_NO.Text = dr["RETURN_NO"].ToString();
                    txt_RETURN_DATE.Text = dr["RETURN_DATE"].ToString();
                    txt_PLANT_AREA.Text = dr["PLANT_AREA"].ToString();
                    txt_ORDER_QTY.Text = dr["ORDER_QTY"].ToString();
                    txt_TURNOVER_QTY.Text = dr["TURNOVER_QTY"].ToString();
                    txt_B_QTY.Text = dr["B_QTY"].ToString();
                    txt_RETURN_FREQUENCY.Text = dr["RETURN_FREQUENCY"].ToString();
                    txt_SHOE_NO.Text = dr["SHOE_NO"].ToString();
                }
            }
        }

        public void GetDataList()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dt.Rows)
                    {
                        p.Add("RETURN_NO", dr["RETURN_NO"].ToString());
                    }
                }

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.BadReturnBase",//类名
                                            "GetBadReturnDetailList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["RETURN_NO"].Value = dr["RETURN_NO"].ToString();
                        dgvr.Cells["BAD_REASON"].Value = dr["BAD_REASON"].ToString();
                        dgvr.Cells["TREATMENT_METHOD"].Value = dr["TREATMENT_METHOD"].ToString();
                        dgvr.Cells["F_GUID"].Value = dr["F_GUID"].ToString();
                        i++;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                        if (cell.CurrentItem.Equals("DETAIL1"))//查看照片
                        {
                            //类型
                            string TYPE = "1";
                            //退货单号
                            string RETURN_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["RETURN_NO"].Value);
                            string F_GUID = Convert.ToString(dataGridView1.CurrentRow.Cells["F_GUID"].Value);
                            FrmImgList add = new FrmImgList(FileView(RETURN_NO, F_GUID, TYPE), null, "6");
                            add.ShowDialog();
                        }
                    }
                    if (name == "operation1")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation1"] as DataGridViewOperationCell;
                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("DETAIL2"))//查看照片
                        {
                            //类型
                            string TYPE = "2";
                            //季度
                            string RETURN_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["RETURN_NO"].Value);
                            string F_GUID = Convert.ToString(dataGridView1.CurrentRow.Cells["F_GUID"].Value);
                            FrmImgList add = new FrmImgList(FileView(RETURN_NO, F_GUID, TYPE), null, "6");
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

        public DataTable FileView(string RETURN_NO, string F_GUID, string TYPE)
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("RETURN_NO", RETURN_NO);
            p.Add("F_GUID", F_GUID);
            p.Add("TYPE", TYPE);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.BadReturnBase",//类名
                                        "GetFileViewS",//方法名
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
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["IMG_URL"].ToString()))
                    {
                        try
                        {
                            dr["IMG_URL"] = Program.Client.PicUrl + dr["IMG_URL"].ToString();
                            dr["IMG_NAME"] = Program.Client.PicUrl + dr["IMG_NAME"].ToString();
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
