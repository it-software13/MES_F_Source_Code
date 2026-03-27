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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_Reinspectionreport_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 检验单号
        /// </summary>
        private string OUTSOURCING_INSPECTION_NOS = string.Empty;
       
       /// <summary>
       /// 厂商类型代号带出供应商表内容
       /// </summary>
        private string SUPPLIERS_TYPE = string.Empty;
        private string IDS=string.Empty;
        private string GUIDS = string.Empty;
        private string Type = string.Empty;
        public F_QCM_Reinspectionreport_Add(string ID,string GUID, string type,string OUTSOURCING_INSPECTION_NO)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            //OUTSOURCING_INSPECTION_NOS = OUTSOURCING_INSPECTION_NO;
            Type = type;
            IDS = ID;
            GUIDS = GUID;
            OUTSOURCING_INSPECTION_NOS = OUTSOURCING_INSPECTION_NO;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_QCM_Reinspectionreport_Add_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //加载一级下拉框
            ONEXLK();
            GetDataList();
            //首页点击查看重检报告明细显示
            if (!string.IsNullOrEmpty(OUTSOURCING_INSPECTION_NOS) && Type == "DETAIL")
            {
                btn_Out.Visible = false;
                btn_Add.Visible = false;
                stringtypeList();
            }
            if (!string.IsNullOrEmpty(OUTSOURCING_INSPECTION_NOS) && Type == "UPDATE")
            {
                stringtypeList();
            }
        }
        /// <summary>
        /// 修改还有视图表头展示
        /// </summary>
        public void stringtypeList()
        {
            try
            {
                DataTable dt = DataList();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //txt_txt_OUTSOURCING_INSPECTION_NO.Text = item["OUTSOURCING_INSPECTION_NO"].ToString();
                        label_OUTSOURCING_INSPECTION_NO.Text = item["OUTSOURCING_INSPECTION_NO"].ToString();
                        label_OUTSOURCING_INSPECTION_NO.Visible = true;

                        cbo_SUPPLIERS_TYPE.SelectedValue = item["SUPPLIERS_TYPE"].ToString();
                        // cbo_SUPPLIERS_NAME.Text= item["SUPPLIERS_NAME"].ToString();
                        TWOXLK();

                        txt_PO_ORDER.Text = item["PO_ORDER"].ToString();
                        txt_PROD_NO.Text = item["PROD_NO"].ToString();
                        txt_WH_QTY.Text = item["WH_QTY"].ToString();

                        txt_SPOT_CHECK_QTY.Text = item["SPOT_CHECK_QTY"].ToString();
                        txt_SHOE_NO.Text = item["SHOE_NO"].ToString();
                        string DD = item["SUPPLIERS_CODE"].ToString();
                        cbo_SUPPLIERS_NAME.SelectedValue = DD;
                    }
                }
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (NotNull.Trues(
                    cbo_SUPPLIERS_TYPE.Text,
                    txt_WH_QTY.Text,
                    cbo_SUPPLIERS_NAME.Text,
                    txt_SPOT_CHECK_QTY.Text,
                    txt_PO_ORDER.Text,
                    txt_PROD_NO.Text
                    )
                )
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("必填项不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
                else
                {
                    //点击添加
                    if (Type == "Add")
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("SUPPLIERS_TYPE", cbo_SUPPLIERS_TYPE.SelectedValue);
                        p.Add("SUPPLIERS_CODE", cbo_SUPPLIERS_NAME.SelectedValue);
                        p.Add("SUPPLIERS_NAME", cbo_SUPPLIERS_NAME.Text.Trim());
                        p.Add("PO_ORDER", txt_PO_ORDER.Text.Trim());
                        p.Add("PROD_NO", txt_PROD_NO.Text.Trim());
                        p.Add("WH_QTY", txt_WH_QTY.Text.Trim());
                        p.Add("SPOT_CHECK_QTY", txt_SPOT_CHECK_QTY.Text.Trim());
                        p.Add("SHOE_NO", txt_SHOE_NO.Text.Trim());

                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.ReinspectionreportBase",//类名
                                                    "ReinspectionreportAdd",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                        else
                        {
                            MessageBox.Show("保存数据成功");
                            Thread.Sleep(700);//当前线程睡一下
                            this.Close();
                        }
                    }
                    //点击修改
                    else if (!string.IsNullOrEmpty(OUTSOURCING_INSPECTION_NOS) && Type == "UPDATE")
                    {
                        if (DataList().Rows.Count > 0)
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("GUID", GUIDS);
                            p.Add("ID", IDS);
                            p.Add("OUTSOURCING_INSPECTION_NO", OUTSOURCING_INSPECTION_NOS);
                            p.Add("SUPPLIERS_TYPE", cbo_SUPPLIERS_TYPE.SelectedValue);
                            p.Add("SUPPLIERS_CODE", cbo_SUPPLIERS_NAME.SelectedValue);
                            p.Add("SUPPLIERS_NAME", cbo_SUPPLIERS_NAME.Text.Trim());
                            p.Add("PO_ORDER", txt_PO_ORDER.Text.Trim());
                            p.Add("PROD_NO", txt_PROD_NO.Text.Trim());
                            p.Add("WH_QTY", txt_WH_QTY.Text.Trim());
                            p.Add("SPOT_CHECK_QTY", txt_SPOT_CHECK_QTY.Text.Trim());
                            p.Add("SHOE_NO", txt_SHOE_NO.Text.Trim());
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                        "SJ_QCMAPI.ReinspectionreportBase",//类名
                                                        "ReinspectionreportUpdate",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (!ret.IsSuccess)
                            {
                                MessageBox.Show(ret.ErrMsg);
                            }
                            else
                            {
                                MessageBox.Show("修改数据成功");
                                Thread.Sleep(700);//当前线程睡一下
                                this.Close();
                            }
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
        /// 检验明细数据展示
        /// </summary>
        public void GetDataList()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //p.Add("OUTSOURCING_INSPECTION_NO", OUTSOURCING_INSPECTION_NOS);//外包检验编号
                p.Add("GUID", GUIDS);///ID
                p.Add("ID", IDS);//关联键
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ReinspectionreportBase",//类名
                                            "ReinspectionreportXView",//方法名
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
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["TESTITEM_CATEGORY"].Value = dr["TESTITEM_CATEGORY"].ToString();
                        dgvr.Cells["TESTITEM_CODE"].Value = dr["TESTITEM_CODE"].ToString();
                        dgvr.Cells["TESTITEM_NAME"].Value = dr["TESTITEM_NAME"].ToString();
                        dgvr.Cells["TESTTYPE_NO"].Value = dr["TESTTYPE_NO"].ToString();
                        dgvr.Cells["TESTTYPE_NAME"].Value = dr["TESTTYPE_NAME"].ToString();
                        dgvr.Cells["SAMPLE_NUM"].Value = dr["SAMPLE_NUM"].ToString();
                        dgvr.Cells["AQL_LEVEL"].Value = dr["AQL_LEVEL"].ToString();
                        dgvr.Cells["PROBLEM_POINT"].Value = dr["PROBLEM_POINT"].ToString();
                        if (dr["INS_RES"].ToString() == "0")
                        {
                            dgvr.Cells["INS_RES"].Value ="flase";
                        }
                        else if(dr["INS_RES"].ToString() == "1")
                        {
                            dgvr.Cells["INS_RES"].Value = "true";
                        }
                        dgvr.Cells["GUID_IMG"].Value = dr["GUID_IMG"].ToString(); 
                        dgvr.Cells["REMARK"].Value = dr["REMARK"].ToString(); 
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
            dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }
        /// <summary>
        /// 抽检明细查看或者修改数据展示
        /// </summary>
        public DataTable DataList()
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            //p.Add("OUTSOURCING_INSPECTION_NO", OUTSOURCING_INSPECTION_NOS);//外包检验编号
            p.Add("GUID", GUIDS);//ID
            p.Add("ID", IDS);//关联键
            p.Add("OUTSOURCING_INSPECTION_NO", OUTSOURCING_INSPECTION_NOS);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ReinspectionreportBase",//类名
                                        "ReinspectionreportViewByid",//方法名
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
            return dt;
        }
        /// <summary>
        /// 加载二级下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_SUPPLIERS_TYPE_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TWOXLK();
        }

        public void TWOXLK()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("VENDOR_TYPE_NO", cbo_SUPPLIERS_TYPE.SelectedValue);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ReinspectionreportBase",//类名
                                            "ReinspectionreportTWOXLK",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    cbo_SUPPLIERS_NAME.DataSource = dt;
                    cbo_SUPPLIERS_NAME.ValueMember = "SUPPLIERS_CODE";
                    cbo_SUPPLIERS_NAME.DisplayMember = "SUPPLIERS_NAME";
                    //SUPPLIERS_TYPE = dt.Rows[0]["vendor_type_no"].ToString();//厂商类别代号
                    cbo_SUPPLIERS_NAME.SelectedIndex = -1;
                }
                else
                {
                    //cbo_SUPPLIERS_TYPE.DataSource = null;
                }
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 一级下拉内容
        /// </summary>
        public void ONEXLK()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ReinspectionreportBase",//类名
                                            "ReinspectionreportONEXLK",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    cbo_SUPPLIERS_TYPE.DataSource = dt;
                    cbo_SUPPLIERS_TYPE.ValueMember = "vendor_type_no";
                    cbo_SUPPLIERS_TYPE.DisplayMember = "vendor_type_name";
                    //SUPPLIERS_TYPE = dt.Rows[0]["vendor_type_no"].ToString();//厂商类别代号
                    cbo_SUPPLIERS_TYPE.SelectedIndex = -1;
                }
                else
                {
                    cbo_SUPPLIERS_TYPE.DataSource = null;
                }
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_QCM_Reinspectionreport_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void cbo_SUPPLIERS_TYPE_SelectedValueChanged(object sender, EventArgs e)
        { 
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
                            FrmImgList add = new FrmImgList(FileView(GUID_IMG), null, "5");
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
        /// 重检报告图片资源表
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
                                            "SJ_QCMAPI.ReinspectionreportBase",//类名
                                            "ReinspectionreporttListIMG",//方法名
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
            catch (Exception)
            {

                throw;
            }
        }
        private void btn_Out_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_PO_ORDER_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string sql = "select a.MER_PO PO单号,b.PROD_NO ART编号,b.SHOE_NO 鞋型 from BDM_SE_ORDER_MASTER a left join  BDM_SE_ORDER_ITEM b on a.SE_ID=b.SE_ID";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_PO_ORDER.Text = frmData.RetData.Rows[0]["PO单号"].ToString();
                txt_PROD_NO.Text = frmData.RetData.Rows[0]["ART编号"].ToString();
                txt_SHOE_NO.Text = frmData.RetData.Rows[0]["鞋型"].ToString();
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
