using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
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
    public partial class F_QCM_VampQuality_query : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_VampQuality_query()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(start_time);
            InitDateTimePicker(end_time);
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

        DataTable dt = new DataTable();
        private void F_QCM_VampQuality_query_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            dt = GetData();
            this.dataGridView1.DataSource = dt;
            GenClass.AutoSizeColumn(dataGridView1);
             
            dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        //搜索
        private void searchbtn_Click(object sender, EventArgs e)
        {
            DataTable newdt = new DataTable();
            string WHERE = string.Empty;

            string start_date = string.Empty;
            string end_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.start_time.Text))
            {
                start_date = Convert.ToDateTime(this.start_time.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.end_time.Text))
            {
                end_date = Convert.ToDateTime(this.end_time.Value).ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(txt_art.Text))
                WHERE += $@"and ART LIKE '%{txt_art.Text}%'";

            if (!string.IsNullOrEmpty(txt_shoes.Text))
                WHERE += $@"and 鞋型 LIKE '%{txt_shoes.Text}%'";
            //string txt = comboBox1.Text;
            if (!string.IsNullOrEmpty(comboBox1.Text))
                WHERE += $@"and `A-01状态` = '{comboBox1.Text}'";

            if (!string.IsNullOrEmpty(start_date) || !string.IsNullOrEmpty(end_date))
            {
                if (string.IsNullOrEmpty(start_date))
                    start_date = "1977-01-01";
                if (string.IsNullOrEmpty(end_date))
                    start_date = "3099-01-01";
                WHERE += $@"and ( `A-01到期日期` BETWEEN '{start_date}' AND '{end_date}') ";
            }
            if (!string.IsNullOrEmpty(WHERE))
                WHERE = WHERE.Remove(WHERE.IndexOf("and"), 3);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (txt_art.Text.Trim() == "" && txt_shoes.Text.Trim() == "" && comboBox1.Text.Trim() == "")
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
                    this.dataGridView1.DataSource = newdt;
                }
                

            }
        }
        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ART", typeof(string));
            dt.Columns.Add("鞋型", typeof(string));
            dt.Columns.Add("A-01状态", typeof(string));
            dt.Columns.Add("A-01到期日期", typeof(string));

            for (int i = 0; i < 12; i++)
            {
                DataRow dr = dt.NewRow();

                dr["ART"] = i<6?"YIH854":"IUK652";
                dr["鞋型"] = i < 6?"篮球鞋":"足球鞋";
                dr["A-01状态"] = i < 7 ? "正常" : "临期";
                dr["A-01到期日期"] = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");

                dt.Rows.Add(dr);
            }
            GenClass.AutoSizeColumn(dataGridView1);
            return dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;
                string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                if (name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }

                    if (cell.CurrentItem.Equals("selectbtn"))//查看照片
                    {
                        FrmShowFile ff = new FrmShowFile(Program.Client.PicUrl + "/File/A-01报告GY2819-20211013.PDF", "A-01报告");
                        ff.ShowDialog();
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }
    }
}
