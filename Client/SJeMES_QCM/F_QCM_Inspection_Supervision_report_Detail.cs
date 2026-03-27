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
    public partial class F_QCM_Inspection_Supervision_report_Detail : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _SPOTCHECK_NO { get; set; }
        public string _VEND_NO { get; set; }
        public F_QCM_Inspection_Supervision_report_Detail(string SPOTCHECK_NO,string VEND_NO)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            _SPOTCHECK_NO = SPOTCHECK_NO;
            _VEND_NO = VEND_NO;
            //InitDateTimePicker(txt_SPOTCHECK_DATE);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        #region 日期控件初始为空值处理

        /// <summary>
        /// 初始化日期时间控件
        /// </summary>
        /// <param name="dtp"></param>
        public static void InitDateTimePicker(DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = " ";  //必须设置成" "
            dtp.ValueChanged -= DateTimePicker_ValueChanged;
            dtp.ValueChanged += DateTimePicker_ValueChanged;
            dtp.KeyPress -= DateTimePicker_KeyPress;
            dtp.KeyPress += DateTimePicker_KeyPress;
        }

        public static void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "yyyy-MM-dd"; //null;
            dtp.Checked = false;// 解决BUG ：防止日期控件不能选择相同日期的 --- 要放置在设置格式之后
        }

        public static void DateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)  // backspace左删除键
            {
                DateTimePicker dtp = (DateTimePicker)sender;
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = " ";
            }
        }
        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                string TESTITEM_CODE = Convert.ToString(dataGridView1.CurrentRow.Cells["TESTITEM_CODE"].Value);//检测项编号
                string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                if (name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                        return;
                    if (cell.CurrentItem.Equals("selectpicture"))
                    {
                        //查询dt
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("SPOTCHECK_NO", _SPOTCHECK_NO);//检验单号
                        data.Add("TESTITEM_CODE", TESTITEM_CODE);//检测项编号

                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_QCMAPI", "SJ_QCMAPI.SpotCheck", "GetPhotoImgList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        if (!ret.IsSuccess)
                            throw new Exception(ret.ErrMsg);
                        else
                        {
                            var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                            FrmImgList frmImgList = new FrmImgList(dt,null,"1");
                            frmImgList.ShowDialog();
                        }
                        
                    }

                }
            }
        }

        private void F_QCM_Inspection_Supervision_report_Detail_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += SearchData;
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        //查询方法
        public void SearchData(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                #region 头
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("SPOTCHECK_NO", _SPOTCHECK_NO);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);


                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.SpotCheck", "GetSpotCheckDetail", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    dataGridView1.Rows.Clear();
                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {


                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                this.txt_VEND_NAME.Text = dr["VEND_NAME"].ToString();
                                this.txt_PART_NO.Text = dr["PART_NO"].ToString();
                                this.txt_SHOE_NOS.Text = dr["SHOE_NOS"].ToString();

                                this.txt_PROD_NO.Text = dr["PROD_NO"].ToString();
                                this.txt_PO_ORDER.Text = dr["PO_ORDER"].ToString();
                                this.txt_SPOTCHECK_DATE.Text = dr["SPOTCHECK_DATE"].ToString();
                                this.txt_PO_QTY.Text = dr["PO_QTY"].ToString();
                                this.txt_PLANSAMP_QTY.Text = dr["PLANSAMP_QTY"].ToString();
                                this.txt_PROCESS_TYPE.Text = dr["PROCESS_TYPE"].ToString();
                            }
                        }

                    }
                }
                #endregion

                #region 表格

                string retdata2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
             "SJ_QCMAPI", "SJ_QCMAPI.SpotCheck", "GetSpotCheckDetailList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata2);
                if (!ret2.IsSuccess)
                    throw new Exception(ret2.ErrMsg);
                else
                {
                    Dictionary<string, object> dic2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2.RetData);
                    dataGridView1.Rows.Clear();
                    var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic2["Data"].ToString());

                    int i = 0;
                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt2.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["TESTITEM_CODE"].Value = dr["TESTITEM_CODE"].ToString();
                            dgvr.Cells["TESTITEM_NAME"].Value = dr["TESTITEM_NAME"].ToString();
                            dgvr.Cells["TEST_STANDARD"].Value = dr["TEST_STANDARD"].ToString();
                            dgvr.Cells["TEST_QTY"].Value = dr["TEST_QTY"].ToString();

                            dgvr.Cells["AQL_LEVEL"].Value = dr["AQL_LEVEL"].ToString();
                            dgvr.Cells["DEFECT_CONTENT"].Value = dr["DEFECT_CONTENT"].ToString();
                            dgvr.Cells["BAD_QTY"].Value = dr["BAD_QTY"].ToString();
                            dgvr.Cells["NG_QTY"].Value = dr["NG_QTY"].ToString();
                            dgvr.Cells["CHECK_RESULT"].Value = dr["CHECK_RESULT"].ToString();
                            dgvr.Cells["REMARKS"].Value = dr["REMARKS"].ToString();

                            i++;
                        }
                    }
                    totalCount = int.Parse(dic2["rowCount"].ToString());
                }

                #endregion

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }

        }

        //确认编辑
        private void editbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("确认编辑？", "此操作不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("SPOTCHECK_NO", _SPOTCHECK_NO);

                    data.Add("VEND_NO", _VEND_NO);
                    data.Add("VEND_NAME", this.txt_VEND_NAME.Text);
                    data.Add("PART_NO", this.txt_PART_NO.Text);
                    data.Add("SHOE_NOS", this.txt_SHOE_NOS.Text);
                    data.Add("PROD_NO", this.txt_PROD_NO.Text);
                    data.Add("PO_ORDER", this.txt_PO_ORDER.Text);
                    data.Add("SPOTCHECK_DATE", Convert.ToDateTime(this.txt_SPOTCHECK_DATE.Value).ToString("yyyy-MM-dd"));
                    data.Add("PO_QTY", this.txt_PO_QTY.Text);
                    data.Add("PLANSAMP_QTY", this.txt_PLANSAMP_QTY.Text);
                    data.Add("PROCESS_TYPE", this.txt_PROCESS_TYPE.Text);
                    


                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_QCMAPI", "SJ_QCMAPI.SpotCheck", "UpdateSpotCheck", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                        throw new Exception(ret.ErrMsg);
                    else
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("编辑成功！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
