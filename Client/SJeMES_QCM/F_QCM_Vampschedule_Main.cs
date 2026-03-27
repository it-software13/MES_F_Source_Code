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
    public partial class F_QCM_Vampschedule_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Vampschedule_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(dtp);//日期选择器
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_Vampschedule_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            #region 日期选择器初始为空
            this.dtp.Format = DateTimePickerFormat.Custom;
            this.dtp.CustomFormat = " "; 
            #endregion
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
        /// 部门产线视图展示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                #region 获取日期控件的值
                string start_date1 = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dtp.Text))
                {
                    start_date1 = Convert.ToDateTime(this.dtp.Value).ToString("yyyy-MM-dd");
                } 
                #endregion
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("PUTINTO_DATE", start_date1.ToString());
                p.Add("SHOE_NO", txt_SHOE_NO.Text.Trim().ToString());
                p.Add("SE_ID", txt_SE_ID.Text.Trim().ToString());

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.VampscheduleBase",//类名
                                            "GetVampscheduleList",//方法名
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
                        dgvr.Cells["WEEK_TIMES"].Value = dr["WEEK_TIMES"].ToString();
                        dgvr.Cells["PUTINTO_DATE"].Value = dr["PUTINTO_DATE"].ToString();
                        dgvr.Cells["WORK_HOURS"].Value = dr["WORK_HOURS"].ToString();
                        dgvr.Cells["ORDER_DELIVERY_DATE"].Value = dr["ORDER_DELIVERY_DATE"].ToString();
                        dgvr.Cells["LEAD_TIME"].Value = dr["LEAD_TIME"].ToString();
                        dgvr.Cells["LAST_NUMBER"].Value = dr["LAST_NUMBER"].ToString();
                        dgvr.Cells["TRIP_QTY"].Value = dr["TRIP_QTY"].ToString();
                        dgvr.Cells["VAMP_TYPE"].Value = dr["VAMP_TYPE"].ToString();
                        dgvr.Cells["SHOE_NO"].Value = dr["SHOE_NO"].ToString();
                        dgvr.Cells["MODULE_NO"].Value = dr["MODULE_NO"].ToString();
                        dgvr.Cells["SE_ID"].Value = dr["SE_ID"].ToString();
                        dgvr.Cells["ITEM_NO"].Value = dr["ITEM_NO"].ToString();
                        dgvr.Cells["QTY"].Value = dr["QTY"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());

                this.dataGridView1.ClearSelection();

                GenClass.AutoSizeColumn(dataGridView1);

                //this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void btn_entering_Click(object sender, EventArgs e)
        {
            F_QCM_Vampschedule_Add add = new F_QCM_Vampschedule_Add();
            add.ShowDialog();
            FormLoad();
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            FormLoad();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
