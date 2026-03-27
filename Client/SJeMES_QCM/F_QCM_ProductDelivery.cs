using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
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
    public partial class F_QCM_ProductDelivery : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_ProductDelivery()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(start_date);
            InitDateTimePicker(end_date);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        DataTable dt = new DataTable();
        //初始化
        private void F_QCM_ProductDelivery_Load(object sender, EventArgs e)
        {
            dt = GetData();

            this.dataGridView1.DataSource = dt;

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        public DataTable GetData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("厂区", typeof(string));
            dt.Columns.Add("订单号", typeof(string));
            dt.Columns.Add("制令", typeof(string));
            dt.Columns.Add("ARTNO", typeof(string));
            dt.Columns.Add("鞋型名称", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            dt.Columns.Add("满箱百分比", typeof(string));
            dt.Columns.Add("满箱日期", typeof(string));
            dt.Columns.Add("测钉", typeof(string));
            dt.Columns.Add("验货日期", typeof(string));
            dt.Columns.Add("重新验货", typeof(string));

            dt.Columns.Add("验货结果", typeof(string));

            dt.Columns.Add("出货日期", typeof(string));
            dt.Columns.Add("ETC", typeof(string));
            dt.Columns.Add("仓库位置", typeof(string));
            dt.Columns.Add("生产组别", typeof(string));
            dt.Columns.Add("备注", typeof(string));

            Random ran = new Random();
            for (int i = 1; i < 11; i++)
            {
                DataRow dr = dt.NewRow();
                dr["厂区"] = i / 2 == 0 ? "200" : "300";
                dr["订单号"] = DateTime.Now.ToString("yyyyMMdd") + (1000 + i);
                dr["制令"] = "F0A" + DateTime.Now.ToString("yMdM")+(10+i);
                dr["ARTNO"] = i < 6 ? "B3751" : "G54992";
                dr["鞋型名称"] = i < 5 ? "TMAC 3 Restomod" : i > 5 && i < 7 ? "Marquee Boost" : "Y-3 KAIWA";
                dr["数量"] =(1+i) + DateTime.Now.ToString("MM")+(i-1);
                dr["满箱百分比"] = i%2==0?"98": i%3==0?"85": i%5==0?"99":"93";
                dr["满箱日期"] = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                dr["测钉"] = "Y";
                dr["验货日期"] = i < 4 ? DateTime.Now.ToString("yyyy-MM-dd") : DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
                dr["验货结果"] = "PASS";
                dr["重新验货"] = "N";
                

                dr["出货日期"] = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");
                dr["ETC"] = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
                dr["仓库位置"] = "**420";
                dr["生产组别"] = "5L15*320";
                dr["备注"] = "";

                dt.Rows.Add(dr);
            }
            //this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            //this.dataGridView1 = new DataGridView with {.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells}
            //GenClass.AutoSizeColumn(this.dataGridView1);
            return dt;
        }

        //搜索
        private void Searchbtn_Click(object sender, EventArgs e)
        {

            DataTable newdt = new DataTable();
            string WHERE = string.Empty;
            try
            {
                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }

                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }


                if (string.IsNullOrEmpty(start_date) && string.IsNullOrEmpty(end_date))
                {
                    WHERE += "";
                }
                else
                {
                    
                    if (string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date))
                    {
                        if (string.IsNullOrEmpty(start_date))
                        {
                            start_date = "1977-01-01";
                        }
                        if (string.IsNullOrEmpty(end_date))
                        {
                            end_date = "3021-01-01";
                        }
                    }
                    WHERE += $@"and ( 验货日期 > '{start_date}'and 验货日期 < '{end_date}') ";
                }
                
                

                if (!string.IsNullOrEmpty(txt_zl.Text))
                    WHERE += $@"and 制令 LIKE '%{txt_zl.Text}%'";

                if (!string.IsNullOrEmpty(txt_order.Text))
                    WHERE += $@"and 订单号 LIKE '%{txt_order.Text}%'";

                if (!string.IsNullOrEmpty(txt_art.Text))
                    WHERE += $@"and ARTNO LIKE '%{txt_art.Text}%'";

                if (!string.IsNullOrEmpty(txt_area.Text))
                    WHERE += $@"and 厂区 LIKE '%{txt_area.Text}%'";

                if (!string.IsNullOrEmpty(comboBox1.Text))
                    WHERE += $@"and 验货结果 LIKE '%{comboBox1.Text}%'";

                

                WHERE = WHERE.Remove(WHERE.IndexOf("and"), 3);

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (txt_zl.Text.Trim() == "" && txt_order.Text.Trim() == "" && txt_art.Text.Trim() == "" && txt_area.Text.Trim() == "" && comboBox1.Text.Trim() == "" && start_date.Trim() == "" && end_date.Trim() == "")//start_date
                    {
                        this.dataGridView1.DataSource = dt;
                    }
                    else
                    {

                        newdt = dt.Clone();

                        DataRow[] dr = dt.Select(WHERE);
                        for (int i = 0; i < dr.Length; i++)
                        {
                            newdt.ImportRow((DataRow)dr[i]);
                        }
                    }
                    this.dataGridView1.DataSource = newdt;

                }
                else
                {

                }
            }
            catch
            {
                dt = GetData();

                this.dataGridView1.DataSource = dt;
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
    }
}
