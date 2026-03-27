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

namespace SJeMES_BDM
{
    public partial class F_QCM_VWarehouse_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_VWarehouse_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
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
        private void F_QCM_VWarehouse_Main_Load(object sender, EventArgs e)
        {
            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";
            this.dateTimeP_end_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_end_date.CustomFormat = " ";
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
            dataGridView1.ClearSelection();
           // dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string putin_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
                {
                    putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.dateTimeP_end_date.Value).ToString("yyyy-MM-dd");
                }
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("putin_date", putin_date);//收料日期
                p.Add("end_date", end_date);//收料日期
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                p.Add("CHK_NO", txt_CHK_NO.Text);//收料单号
                p.Add("jieguo", txt_jieguo.Text);//物性结果
                p.Add("quyang", txt_quyang.Text);//取样状况
                p.Add("VEND_NO", txt_VEND_NO.Text);//采购厂商
                p.Add("VEND_NO2", txt_VEND_NO2.Text);//生产厂商
                p.Add("STATUS", txt_STATUS.Text);//状态
                p.Add("bianma", txt_bianma.Text);//物料编码
                p.Add("STOC_NO", txt_STOC_NO.Text);//仓别
                p.Add("wgjieguo", txt_wgjieguo.Text);//外观结果
                p.Add("ORG_ID", txt_ORG_ID.Text);//工厂
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.VMaterialinventory",//类名
                                            "CheckResultMain",//方法名
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
                        dgvr.Cells["RCPT_DATE"].Value = dr["RCPT_DATE"].ToString();
                        dgvr.Cells["CHK_NO"].Value = dr["CHK_NO"].ToString();
                        i++;
                    }
                   GenClass.AutoSizeColumn(dataGridView1);
                   
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridView1.ClearSelection();
               
                //this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            FormLoad();
            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";
            this.dateTimeP_end_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_end_date.CustomFormat = " ";
        }
        public void FormLoad()
        {

            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        //已完成
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
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    switch (dataGridView1.Columns[e.ColumnIndex].Name)
                    {
                        case "operation_a":
                            dic.Add("CHK_NO", dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString());
                            using (F_QCM_VMaterialresults_Add aa = new F_QCM_VMaterialresults_Add(dic))
                            {
                                aa.ShowDialog();
                            }
                            break;
                        case "operation_b":
                            dic.Add("RCPT_DATE", dataGridView1.CurrentRow.Cells["RCPT_DATE"].Value.ToString());
                            dic.Add("CHK_NO", dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString());
                            using (F_QCM_Viewinspectionresults_view aa = new F_QCM_Viewinspectionresults_view(dic))
                            {
                                aa.ShowDialog();
                            }
                            break;
                        case "operation_c":
                            using (F_QCM_VLaboratorytestresults_Vews aa = new F_QCM_VLaboratorytestresults_Vews(dic))
                            {
                                aa.ShowDialog();
                            }
                            break;
                        case "operation_d":
                            using (F_QCM_Viewinspectionresults_view aa = new F_QCM_Viewinspectionresults_view(dic))
                            {
                                aa.ShowDialog();
                            }
                            break;
                        case "operation_e":
                            using (F_QCM_Viewinspectionresults_view aa = new F_QCM_Viewinspectionresults_view(dic))
                            {
                                aa.ShowDialog();
                            }
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
