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
    public partial class F_QCM_TaskList : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_TaskList()
        {
        materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
    Program.SkinThemes, materialSkinManager, this);
        InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        DataTable dt = new DataTable();
        private void F_QCM_TaskList_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            dt = GetData();
            this.dataGridView1.DataSource = dt;
        }


        public DataTable GetData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("厂区", typeof(string));
            dt.Columns.Add("PO", typeof(string));
            dt.Columns.Add("ART", typeof(string));
            dt.Columns.Add("鞋型", typeof(string));
            dt.Columns.Add("客户", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            dt.Columns.Add("满箱状态", typeof(string));
            dt.Columns.Add("状态", typeof(string));

            Random ran = new Random();
            for (int i = 1; i < 11; i++)
            {
                DataRow dr = dt.NewRow();
                dr["厂区"] = i % 2 == 0 ? "一厂" :i%3==0? "二厂":"三厂";
                dr["PO"] = DateTime.Now.ToString("yMdm") + (10 + i);
                dr["ART"] = "ART" +0+ (i+1)+(i-1)+(i);
                dr["鞋型"] = i < 5 ? "TMAC 3 Restomod" : i > 5 && i < 7 ? "Marquee Boost" : "Y-3 KAIWA";
                dr["客户"] = i < 6 ? "B3751" : "G54992";
                dr["数量"] = (1 + i) + DateTime.Now.ToString("MM") + (i - 1);
                dr["满箱状态"] = i % 2 == 0 ? "98" : i % 3 == 0 ? "85" : i % 5 == 0 ? "99" : "93";
                dr["状态"] = i%2==0?"已验货":"未验货";

                dt.Rows.Add(dr);
            }
            this.dataGridView1.ClearSelection();

            GenClass.AutoSizeColumn(dataGridView1);

            //this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable newdt = new DataTable();
            try
            {
                string WHERE = string.Empty;

                if (!string.IsNullOrEmpty(txt_org.Text))
                    WHERE += $@"and 厂区 LIKE '%{txt_org.Text}%'";

                if (!string.IsNullOrEmpty(txt_po.Text))
                    WHERE += $@"and PO LIKE '%{txt_po.Text}%'";

                if (!string.IsNullOrEmpty(txt_art.Text))
                    WHERE += $@"and ART LIKE '%{txt_art.Text}%'";

                if (!string.IsNullOrEmpty(txt_shoe.Text))
                    WHERE += $@"and 鞋型 LIKE '%{txt_shoe.Text}%'";

                WHERE = WHERE.Remove(WHERE.IndexOf("and"), 3);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (txt_org.Text.Trim() == "" && txt_po.Text.Trim() == "" && txt_art.Text.Trim() == "" && txt_shoe.Text.Trim() == "")
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
            }
            catch
            {
                dt = GetData();
                this.dataGridView1.DataSource = dt;
            }
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
