using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls.DataGridView;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public partial class F_BDM_ProdCustomQuality_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_ProdCustomQuality_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(dateTimePicker1);
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
        private int op = 0;
        //详情条数
        private int  sun;
        //加载位置及索引
        int count = 7;
        //string flag1 = "0"; // 0-初始化  1-搜索
        private void F_BDM_ProdCustomQuality_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = " ";
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GetList;
            LoadPage();
           
            
        }
        //委托加载数据
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        /// <summary>
        /// ART品质生命周期信息展示
        /// </summary>
        public void GetList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                List<string> lst_enum_type = new List<string>();
                //请求api的数据展示
                string date_lc =string.Empty;
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text))
                {
                    date_lc = this.dateTimePicker1.Value.ToString("yyyy-MM-dd");
                }

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("develop_season", txt_develop_season.Text.Trim());
                p.Add("shoe_no", txt_shoe_no.Text.Trim());
                p.Add("prod_no", txt_prod_no.Text.Trim());
                p.Add("PRODUCT_MONTH", date_lc);
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ProdTableView",//类名
                                            "GET_PROD_List",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                //视图数据显示
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data1"].ToString());
                DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data2"].ToString());

                dataGridView1.Rows.Clear();
                if (dt1.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt1.Rows)
                    {

                        dataGridView1.Rows.Add();

                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Height = 45; 
                        dgvr.Cells["develop_season"].Value = dr["develop_season"].ToString();
                        dgvr.Cells["shoe_no"].Value = dr["shoe_no"].ToString();
                        dgvr.Cells["prod_no"].Value = dr["prod_no"].ToString();
                        dgvr.Cells["PRODUCT_MONTH"].Value = dr["PRODUCT_MONTH"].ToString();
                        dgvr.Cells["img_url2"].Value = dr["img_url"].ToString();
                        if (!string.IsNullOrEmpty(dr["img_url"].ToString()))
                        {
                            try
                            {
                                var webC = new System.Net.WebClient();
                                string url = Program.Client.PicUrl + Convert.ToString(dr["img_url"].ToString());
                                Image image = new Bitmap(webC.OpenRead(url));
                                dgvr.Cells["img_url"].Value = image;
                            }
                            catch
                            { 
                            }
                        }
                        else
                        {
                            dgvr.Cells["img_url"].Value = null;
                        }
                        i++;
                    }
                    op += 1;


                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                //this.dataGridView1.Columns[8]
                if (dt2.Rows.Count > 0 && op==1)
                {

                    int j = count;
                    int i = count;
                    foreach (DataRow dr in dt2.Rows)
                    {
                        DataGridViewColumn ageColumn = new DataGridViewColumn()
                        {
                            Name = $"DEPARTMENT_NO{i}",
                            HeaderText = dr["DEPARTMENT_NO"].ToString(),
                            CellTemplate = new DataGridViewTextBoxCell()
                        };
                        DataGridViewColumn ageColumn2 = new DataGridViewColumn()
                        {
                            Name = $"DEPARTMENT_NAME{i}",
                            HeaderText = dr["DEPARTMENT_NAME"].ToString(),
                            CellTemplate = new DataGridViewTextBoxCell()
                        };
                        DataGridViewColumn ageColumn3 = new DataGridViewColumn()
                        {
                            Name = $"REMARKS{i}",
                            HeaderText = dr["REMARKS"].ToString(),
                            CellTemplate = new DataGridViewTextBoxCell()
                        };
                        ageColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        ageColumn2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        ageColumn3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        //设置该列背景颜色
                        dataGridView1.Columns.Insert(8,ageColumn);
                        dataGridView1.Columns.Insert(8+1,ageColumn2);
                        dataGridView1.Columns.Insert(8+2,ageColumn3);
                        j++;
                        i++;
                    }
                    sun = i;
                }
               
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                this.dataGridView1.Columns["operation2"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
           
        }
        private void btn_Select_Click(object sender, EventArgs e)
         {
            LoadPage();
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = " ";

        }
        //详情伸展收缩
        public bool flag = true;

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
                            string prod_no = Convert.ToString(dataGridView1.CurrentRow.Cells["prod_no"].Value);
                            string develop_season = Convert.ToString(dataGridView1.CurrentRow.Cells["develop_season"].Value);
                            string series = Convert.ToString(dataGridView1.CurrentRow.Cells["series"].Value);
                            string shoe_no = Convert.ToString(dataGridView1.CurrentRow.Cells["shoe_no"].Value);
                            string PRODUCT_MONTH = Convert.ToString(dataGridView1.CurrentRow.Cells["PRODUCT_MONTH"].Value);
                            string img_url = Convert.ToString(dataGridView1.CurrentRow.Cells["img_url2"].Value);
                            //MessageBox.Show("详情界面样式暂未确定！");
                            F_BDM_ProdCustomQuality_Detail detail = new F_BDM_ProdCustomQuality_Detail(prod_no, develop_season, series, shoe_no, PRODUCT_MONTH, img_url);
                            detail.ShowDialog();                        }
                        //else if (cell.CurrentItem.Equals("UPDATE"))
                        //{
                        //    string prod_no = Convert.ToString(dataGridView1.CurrentRow.Cells["prod_no"].Value);
                        //    using (F_BDM_ProdCustomQuality_List add = new F_BDM_ProdCustomQuality_List(prod_no))
                        //    {
                        //        add.ShowDialog();
                        //    }
                        //}
                    }

                    if (name == "operation2")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation2"] as DataGridViewOperationCell;
                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("UPDATE"))
                        {
                            string prod_no = Convert.ToString(dataGridView1.CurrentRow.Cells["prod_no"].Value);
                            using (F_BDM_ProdCustomQuality_List add = new F_BDM_ProdCustomQuality_List(prod_no))
                            {
                                add.ShowDialog();
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
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
