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

namespace SJeMES_QCM
{
    public partial class F_QCM_BdmoutqualitylistmSelect_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 关联键
        /// </summary>
        private string GUIDS = string.Empty;
        public F_QCM_BdmoutqualitylistmSelect_List(string GUID)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
   Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            GUIDS = GUID;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_QCM_BdmoutqualitylistmSelect_List_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetDataList();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }
        public void GetDataList()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("GUID", GUIDS);
             

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.OutQuantityStandard",//类名
                                            "GetAllProjectListLogDetailsList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (string.IsNullOrEmpty(ret["IsSuccess"].ToString()))
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }

                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret["RetData1"].ToString());

                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["PROJECT"].Value = dr["PROJECT"].ToString();
                        dgvr.Cells["SCORE"].Value = dr["SCORE"].ToString();
                        dgvr.Cells["REAL_SCORE"].Value = dr["REAL_SCORE"].ToString();
                        dgvr.Cells["PROBLEM_POINT"].Value = dr["PROBLEM_POINT"].ToString();
                        dgvr.Cells["REMARK"].Value = dr["REMARK"].ToString();
                        dgvr.Cells["GUID"].Value = dr["GUID"].ToString();
                        dgvr.Cells["GUID_IMG"].Value = dr["GUID_IMG"].ToString();
                        dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();
                        dgvr.Cells["CREATEDATE"].Value = dr["CREATEDATE"].ToString();

                        label_SUPPLIERS_NAME.Text = dr["SUPPLIERS_NAME"].ToString()!=null ? dr["SUPPLIERS_NAME"].ToString():"无";
                        label_SUPPLIERS_Day.Text = dr["CREATEDATE"].ToString()!=null? dr["CREATEDATE"].ToString():"无";
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
                }
               ;
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
                {
                    return;
                }
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
                            string GUID_IMG = dataGridView1.CurrentRow.Cells["GUID_IMG"].Value.ToString();
                            FrmImgList add = new FrmImgList(FileView(GUID_IMG), null,"4");
                            add.ShowDialog();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        /// <summary>
        /// 发外厂商品质体系项目日志详情图片
        /// </summary>
        public DataTable FileView(string GUID_IMG)
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("GUID_IMG", GUID_IMG);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.OutQuantityStandard",//类名
                                            "GetAllProjectListIMG",//方法名
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
                        dr["IMG_URL"] = Program.Client.PicUrl + dr["IMG_URL"].ToString();
                        dr["IMG_NAME"] = dr["IMG_NAME"].ToString();
                        i++;
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
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
    }
}
