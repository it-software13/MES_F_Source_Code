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
    public partial class F_QCM_ConfirmShoes_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private string type = string.Empty;
        public F_QCM_ConfirmShoes_Main(string ID)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
    Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(dateTimeP_putin_date);
            InitDateTimePicker(dateTimeP_end_date);
            type = ID;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        public void FormLoad()
        {

            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
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
        private void F_QCM_ConfirmShoes_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";

            this.dateTimeP_end_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_end_date.CustomFormat = " ";
            pageControl1.BindPageEvent += GetDataList;
            //GetDataList();
            FormLoad();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }
        private void btn_Select_Click(object sender, EventArgs e)
        {
            FormLoad();
            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";
            this.dateTimeP_end_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_end_date.CustomFormat = " ";
        }

        /// <summary>
        /// 搜索及视图展示(确认鞋型管理表视图)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
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
                p.Add("PROD_NO", txt_PROD_NO.Text.Trim().ToString());//ART
                p.Add("SHOE_NO", txt_SHOE_NO.Text.Trim().ToString());//鞋型
                p.Add("CONFIRM_PEOPLE", txt_CONFIRM_PEOPLE.Text.Trim().ToString());

                p.Add("confirm_type",type);//类型


                p.Add("STATUS", STATUSD);//状态
                p.Add("putin_date", putin_date);
                p.Add("end_date", end_date);

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ConfirmShoesBase",//类名
                                            "ConfirmShoesBaseView",//方法名
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
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
                        dgvr.Cells["SHOE_NO"].Value = dr["SHOE_NO"].ToString();
                        dgvr.Cells["DEVELOP_SEASON"].Value = dr["DEVELOP_SEASON"].ToString();
                        dgvr.Cells["QTY"].Value = dr["QTY"].ToString();
                        dgvr.Cells["RECEIVE_DATE"].Value = dr["RECEIVE_DATE"].ToString();
                        dgvr.Cells["RECEIVE_PEOPLE"].Value = dr["RECEIVE_PEOPLE"].ToString();
                        dgvr.Cells["RECONFIRM_DATE"].Value = dr["RECONFIRM_DATE"].ToString();
                        dgvr.Cells["CONFIRM_PEOPLE"].Value = dr["CONFIRM_PEOPLE"].ToString();
                        dgvr.Cells["CONFIRM_RESULT"].Value = dr["CONFIRM_RESULT"].ToString();
                        dgvr.Cells["REDO_REASON"].Value = dr["REDO_REASON"].ToString();
                        dgvr.Cells["REMARKS"].Value = dr["REMARKS"].ToString();

                        dgvr.Cells["CONFIRM_DATE"].Value = dr["CONFIRM_DATE"].ToString();//确认日期
                        dgvr.Cells["INVALID_DATE"].Value = dr["INVALID_DATE"].ToString();//失效日期
                        if (dr["STATUS"] != null)
                        {
                            string status =dr["STATUS"].ToString();
                            switch (status)
                            {
                                case enum_confirm_status.enum_confirm_status_0:
                                    dgvr.Cells["STATUS"].Value = enum_confirm_status.enum_confirm_status_string_0;
                                    break;
                                case enum_confirm_status.enum_confirm_status_1:
                                    dgvr.Cells["STATUS"].Value = enum_confirm_status.enum_confirm_status_string_1;
                                    break;
                                case enum_confirm_status.enum_confirm_status_2:
                                    if (dr["CONFIRM_DATE"] != null)
                                    {
                                        int Num = DateTime.Compare(Convert.ToDateTime(dr["CONFIRM_DATE"].ToString()), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
                                        if (Num > 0)//大于0就说明左边时间已经超过当前时间，过期
                                        {
                                            dgvr.Cells["STATUS"].Value = enum_confirm_status.enum_confirm_status_string_2;
                                        }
                                    }
                                    break;
                                case enum_confirm_status.enum_confirm_status_3:
                                    dgvr.Cells["STATUS"].Value = enum_confirm_status.enum_confirm_status_string_3;
                                    break;
                            }
                        }
                        
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1,0);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
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
                            string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();//ID
                            F_QCM_ConfirmShoes_Add add = new F_QCM_ConfirmShoes_Add(ID,type);
                            add.ShowDialog();
                        }
                        else if (cell.CurrentItem.Equals("UPDATE"))//修改
                        {
                            string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();//ID
                            F_QCM_ConfirmShoes_Add add = new F_QCM_ConfirmShoes_Add(ID,type);
                            add.ShowDialog();
                            FormLoad();
                        }
                        else if (cell.CurrentItem.Equals("DELETE"))//删除
                        {
                            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                 
                                    string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();//ID
                                    Dictionary<string, object> p = new Dictionary<string, object>();

                                    //p.Add("OUTSOURCING_INSPECTION_NO", OUTSOURCING_INSPECTION_NO);
                                    p.Add("ID", ID);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_QCMAPI", "SJ_QCMAPI.ConfirmShoesBase", "ConfirmShoesBaseDelete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        MessageBox.Show("操作删除成功");
                                        FormLoad();
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
            F_QCM_ConfirmShoes_Add add = new F_QCM_ConfirmShoes_Add(string.Empty,type);
            add.ShowDialog();
            button2.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            FormLoad();
        }
        /// <summary>
        /// 条件类型
        /// </summary>
        private string STATUSD=string.Empty;
        //在期内
        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Silver;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            STATUSD = enum_confirm_status.enum_confirm_status_0;
            DropText();
            FormLoad();
        }
        //已忽略
        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button2.BackColor = Color.Silver;
            button3.BackColor = Color.White;
            STATUSD = enum_confirm_status.enum_confirm_status_1;
            DropText();
            FormLoad();
        }
        //报废
        private void button3_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.Silver;
            STATUSD = enum_confirm_status.enum_confirm_status_3;
            DropText();
            FormLoad();
        }
        public void DropText()
        {
            txt_PROD_NO.Text = null;
            txt_SHOE_NO.Text = null;
            txt_CONFIRM_PEOPLE.Text = null;
            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";
            this.dateTimeP_end_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_end_date.CustomFormat = " ";
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
