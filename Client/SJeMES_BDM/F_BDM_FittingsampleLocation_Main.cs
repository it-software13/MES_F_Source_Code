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

namespace SJeMES_BDM
{
    public partial class F_BDM_FittingsampleLocation_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_FittingsampleLocation_Main()
        {
            InitializeComponent();
            InitDateTimePicker(dateTimeP_putin_date);
            InitDateTimePicker(dateTimeP_end_date);
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
        public void FormLoad()
        {

            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            F_BDM_FittingsampleLocation_Add add = new F_BDM_FittingsampleLocation_Add(string.Empty);
            add.ShowDialog();
            FormLoad();
        }
        private void F_BDM_FittingsampleLocation_Main_Load(object sender, EventArgs e)
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
                        if (cell.CurrentItem.Equals("UPDATE"))
                        {
                            string item_no = dataGridView1.CurrentRow.Cells["item_no"].Value.ToString();
                            F_BDM_FittingsampleLocation_Add add = new F_BDM_FittingsampleLocation_Add(item_no);
                            add.ShowDialog();
                            FormLoad();
                        }
                        else if (cell.CurrentItem.Equals("DELETE"))
                        {
                            if (MessageBox.Show("confirm deletion? ", "This delete cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    string item_no = dataGridView1.CurrentRow.Cells["item_no"].Value.ToString();
                                    Dictionary<string, object> p = new Dictionary<string, object>();

                                    p.Add("ITEM_NO", item_no);
                                    p.Add("CAO", "Delete");
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_QCMAPI", "SJ_QCMAPI.FittingsamplestorageBase", "FittingsamplestorageUpdate", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Operation deleted successfully");
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
        /// <summary>
        /// 搜索及视图展示（试穿部样品管存放视图）
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
                p.Add("ITEM_NO", txt_ITEM_NO.Text.Trim());
                p.Add("NAME_S", txt_NAME_S.Text.Trim().ToString());
                p.Add("SUPPLIERS_NAME", txt_SUPPLIERS_NAME.Text.Trim().ToString());
                p.Add("PARENT_ITEM_NO", txt_PARENT_ITEM_NO.Text.Trim().ToString());

                p.Add("putin_date", putin_date);
                p.Add("end_date", end_date);
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.FittingsamplestorageBase",//类名
                                            "FittingsamplestorageGetList",//方法名
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
                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["item_no"].Value = dr["item_no"].ToString();
                        dgvr.Cells["name_s"].Value = dr["item_name"].ToString();
                        dgvr.Cells["location_name"].Value = dr["location_name"].ToString();
                        dgvr.Cells["suppliers_name"].Value = dr["vend_name"].ToString();

                        dgvr.Cells["parent_item_no"].Value = dr["prod_no"].ToString();
                        dgvr.Cells["putin_date"].Value = dr["putin_date"].ToString();
                        dgvr.Cells["end_date"].Value = dr["end_date"].ToString();
                        int Num = DateTime.Compare(Convert.ToDateTime(DateTime.Now.AddDays(7).ToString()), Convert.ToDateTime(dr["end_date"].ToString()));
                        if (Num > 0)
                        {
                            dataGridView1.Rows[i].Cells["end_date"].Style.ForeColor = Color.Red;
                        }
                        i++;
                    }
                }
                GenClass.AutoSizeColumn(dataGridView1);
                totalCount = int.Parse(dic["rowCount"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
