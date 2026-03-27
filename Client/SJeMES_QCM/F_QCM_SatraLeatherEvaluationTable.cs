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
    public partial class F_QCM_SatraLeatherEvaluationTable : MaterialForm
    {

        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_SatraLeatherEvaluationTable()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(dtp);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_SatraLeatherEvaluationTable_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.dtp.Format = DateTimePickerFormat.Custom;
            this.dtp.CustomFormat = " ";
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
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string start_date1 = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dtp.Text))
                {
                    start_date1 = Convert.ToDateTime(this.dtp.Value).ToString("yyyy-MM-dd");
                }
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("ITEM_NO", txt_item_no.Text.Trim().ToString());
                p.Add("ITEM_NAME", txt_item_name.Text.Trim().ToString());
                p.Add("PAINT_DATE", start_date1.ToString());
                p.Add("ITEM_TYPE_NAME", txt_item_type_name.Text.Trim().ToString());
                p.Add("vend_name", txt_vend_name.Text.Trim().ToString());
                //p.Add("CREATEBY", txt_createby1.Text.Trim().ToString());//制表人???
                p.Add("CREATEBY", txt_createby2.Text.Trim().ToString());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.SatraLeatherEvaluationBase",//类名
                                            "GetSatraList",//方法名
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
                        //dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        dgvr.Cells["paint_no"].Value = dr["PAINT_NO"].ToString();
                        dgvr.Cells["ITEM_NO"].Value = dr["ITEM_NO"].ToString();
                        dgvr.Cells["ITEM_NAME"].Value = dr["ITEM_NAME"].ToString();
                        dgvr.Cells["ACTUAL_AREA"].Value = dr["ACTUAL_AREA"].ToString();
                        dgvr.Cells["QTY"].Value = dr["QTY"].ToString();
                        dgvr.Cells["PAINT_DATE"].Value = dr["PAINT_DATE"].ToString();
                        dgvr.Cells["vend_name"].Value = dr["vend_name"].ToString();
                        dgvr.Cells["ITEM_TYPE_NAME"].Value = dr["ITEM_TYPE_NAME"].ToString();
                        if (dr["usage_rate"].ToString() == "")
                        {
                            dgvr.Cells["usage_rate"].Value = dr["usage_rate"].ToString();
                        }
                        else
                        {
                            dgvr.Cells["usage_rate"].Value = (dr["usage_rate"] + "%").ToString();
                        }

                        dgvr.Cells["createby"].Value = dr["createby"].ToString();

                        dgvr.Cells["AVERAGE_USE_RATE"].Value = dr["AVERAGE_USE_RATE"].ToString();

                        dgvr.Cells["DIFFERENCE_COEFFICIENT"].Value = dr["DIFFERENCE_COEFFICIENT"].ToString();

                        dgvr.Cells["ASSESSMENT"].Value = dr["ASSESSMENT"].ToString();


                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridView1.ClearSelection();

                GenClass.AutoSizeColumn(dataGridView1);

                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            FormLoad();
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
                        if (cell.CurrentItem.Equals("DETAIL"))
                        {
                            string paint_no = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                            string item_no = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                            string item_name = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                            string actual_rate = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                            string qty = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                            string date = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                            string vend = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                            string itemtype = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                            string AVERAGE_USE_RATE = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                            string createby = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                            string DIFFERENCE_COEFFICIENT = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                            string ASSESSMENT = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();


                            DataTable dt = new DataTable();
                            dt.Columns.Add("paint_no", typeof(string));
                            dt.Columns.Add("item_no", typeof(string));
                            dt.Columns.Add("item_name", typeof(string));
                            dt.Columns.Add("actual_rate", typeof(string));
                            dt.Columns.Add("qty", typeof(string));
                            dt.Columns.Add("date", typeof(string));
                            dt.Columns.Add("vend", typeof(string));
                            dt.Columns.Add("itemtype", typeof(string));
                            dt.Columns.Add("AVERAGE_USE_RATE", typeof(string));
                            dt.Columns.Add("createby", typeof(string));
                            dt.Columns.Add("DIFFERENCE_COEFFICIENT", typeof(string));
                            dt.Columns.Add("ASSESSMENT", typeof(string));


                            DataRow dr = dt.NewRow();
                            dr["paint_no"] = paint_no;
                            dr["item_no"] = item_no;
                            dr["item_name"] = item_name;
                            dr["actual_rate"] = actual_rate;
                            dr["qty"] = qty;
                            dr["date"] = date;
                            dr["vend"] = vend;
                            dr["itemtype"] = itemtype;
                            dr["AVERAGE_USE_RATE"] = AVERAGE_USE_RATE;
                            dr["createby"] = createby;
                            dr["DIFFERENCE_COEFFICIENT"] = DIFFERENCE_COEFFICIENT;
                            dr["ASSESSMENT"] = ASSESSMENT;

                            dt.Rows.Add(dr);
                            F_QCM_SatraLeatherEvaluationReport add = new F_QCM_SatraLeatherEvaluationReport(dt);
                            add.ShowDialog();
                            FormLoad();
                        }
                    }
                }
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
