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
    public partial class F_QCM_Measuringthenailcorrection_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Measuringthenailcorrection_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(dateTimeP_putin_date);
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
        private void F_QCM_Measuringthenailcorrection_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";

            DataTable dt = table();
            dataGridView1.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    dgvr.Cells["Column1"].Value = dr["序号"].ToString();//条件
                    dgvr.Cells["Column2"].Value = dr["设备编号"].ToString();//条件
                    dgvr.Cells["Column3"].Value = dr["维修人员"].ToString();//条件
                    dgvr.Cells["Column4"].Value = dr["计划矫正时间"].ToString();//条件
                    dgvr.Cells["Column5"].Value = dr["矫正时间"].ToString();//条件
                    i++;
                }
            }
        }
        private void btn_Select_Click(object sender, EventArgs e)
        {
            string putin_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
            {
                putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
            }

            if (string.IsNullOrEmpty(txt_1.Text.Trim()) && string.IsNullOrEmpty(txt_2.Text.Trim()) && string.IsNullOrEmpty(dateTimeP_putin_date.Text.Trim()))
            {
                DataTable dt = table();
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow drr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["Column1"].Value = drr["序号"].ToString();//条件
                        dgvr.Cells["Column2"].Value = drr["设备编号"].ToString();//条件
                        dgvr.Cells["Column3"].Value = drr["维修人员"].ToString();//条件
                        dgvr.Cells["Column4"].Value = drr["计划矫正时间"].ToString();//条件
                        dgvr.Cells["Column5"].Value = drr["矫正时间"].ToString();//条件
                        i++;
                    }
                }
            }
            else
            {

                string wheres = string.Empty;
                if (!string.IsNullOrEmpty(txt_1.Text.Trim()))
                {
                    wheres += $"and 设备编号 LIKE '%{txt_1.Text.Trim()}%'";
                }
                if (!string.IsNullOrEmpty(putin_date))
                {
                    wheres += $"and 矫正时间 LIKE '%{putin_date}%'";
                }
                if (!string.IsNullOrEmpty(txt_2.Text))
                {
                    wheres += $"and 维修人员 LIKE '%{txt_2.Text.Trim()}%'";
                }
                DataRow[] dr = table().Select($"1=1 {wheres}");
                DataTable dt = table().Clone();
                for (int i = 0; i < dr.Length; i++)
                {
                    dt.ImportRow(dr[i]);
                }
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow drr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["Column1"].Value = drr["序号"].ToString();//条件
                        dgvr.Cells["Column2"].Value = drr["设备编号"].ToString();//条件
                        dgvr.Cells["Column3"].Value = drr["维修人员"].ToString();//条件
                        dgvr.Cells["Column4"].Value = drr["计划矫正时间"].ToString();//条件
                        dgvr.Cells["Column5"].Value = drr["矫正时间"].ToString();//条件
                        i++;
                    }
                }
            }
          
            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";
        }
        public DataTable table()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("序号", typeof(string));
            dt.Columns.Add("设备编号", typeof(string));
            dt.Columns.Add("维修人员", typeof(string));
            dt.Columns.Add("计划矫正时间", typeof(string));
            dt.Columns.Add("矫正时间", typeof(string));

            DataRow dr1 = dt.NewRow();
            dr1["序号"] = 1;
            dr1["设备编号"] = " 测钉机001";
            dr1["维修人员"] = "孙小果";
            dr1["计划矫正时间"] = "2021-09-08 18:23:12";
            dr1["矫正时间"] = "2021-09-08 18:20:12";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["序号"] = 2;
            dr2["设备编号"] = " 测钉机002";
            dr2["维修人员"] = "鲁鲁";
            dr2["计划矫正时间"] = "2021-09-12 15:23:42";
            dr2["矫正时间"] = "2021-09-12 15:18:22";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["序号"] = 3;
            dr3["设备编号"] = " 测钉机003";
            dr3["维修人员"] = "康佳会";
            dr3["计划矫正时间"] = "2021-09-09 17:23:00";
            dr3["矫正时间"] = "2021-09-09 15:23:56";
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["序号"] = 4;
            dr4["设备编号"] = " 测钉机004";
            dr4["维修人员"] = "李士林";
            dr4["计划矫正时间"] = "2021-09-13 13:24:12";
            dr4["矫正时间"] = "2021-09-09 13:23:00";
            dt.Rows.Add(dr4);

            DataRow dr5 = dt.NewRow();
            dr5["序号"] = 5;
            dr5["设备编号"] = " 测钉机005";
            dr5["维修人员"] = "张什锦";
            dr5["计划矫正时间"] = "2021-09-14 14:26:45";
            dr5["矫正时间"] = "2021-09-14 14:46:11";
            dt.Rows.Add(dr5);

            DataRow dr6 = dt.NewRow();
            dr6["序号"] = 6;
            dr6["设备编号"] = " 测钉机006";
            dr6["维修人员"] = "孙小果";
            dr6["计划矫正时间"] = "2021-09-14 17:55:12";
            dr6["矫正时间"] = "2021-09-14 15:26:12";
            dt.Rows.Add(dr6);

            DataRow dr7 = dt.NewRow();
            dr7["序号"] = 7;
            dr7["设备编号"] = " 测钉机007";
            dr7["维修人员"] = "鲁鲁";
            dr7["计划矫正时间"] = "2021-09-15 14:26:24";
            dr7["矫正时间"] = "2021-09-15 14:27:35";
            dt.Rows.Add(dr7);

            DataRow dr8 = dt.NewRow();
            dr8["序号"] = 8;
            dr8["设备编号"] = " 测钉机008";
            dr8["维修人员"] = "康佳会";
            dr8["计划矫正时间"] = "2021-09-15 16:46:44";
            dr8["矫正时间"] = "2021-09-14 14:26:12";
            dt.Rows.Add(dr8);

            DataRow dr9 = dt.NewRow();
            dr9["序号"] = 9;
            dr9["设备编号"] = " 测钉机009";
            dr9["维修人员"] = "李士林";
            dr9["计划矫正时间"] = "2021-09-17 14:26:12";
            dr9["矫正时间"] = "2021-09-17 14:26:12";
            dt.Rows.Add(dr9);

            DataRow dr10 = dt.NewRow();
            dr10["序号"] = 10;
            dr10["设备编号"] = " 测钉机010";
            dr10["维修人员"] = "张什锦";
            dr10["计划矫正时间"] = "2021-09-14 18:33:03";
            dr10["矫正时间"] = "2021-09-14 18:22:46";
            dt.Rows.Add(dr10);

            return dt;
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
