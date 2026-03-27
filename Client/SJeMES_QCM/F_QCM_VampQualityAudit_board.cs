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
    public partial class F_QCM_VampQualityAudit_board : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_VampQualityAudit_board()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        private string tj;
       

        private void F_QCM_VampQualityAudit_board_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            dataGridView1.DataSource = table();
        }
        private void btn_Select_Click(object sender, EventArgs e)
        {
            tj= cb_SE_ID.Text;
            if (!string.IsNullOrEmpty(tj))
            {
                DataRow[] dr = table().Select($@"厂区='{tj}'");
                DataTable dt = table().Clone();
                for (int i = 0; i < dr.Length; i++)
                {
                    dt.ImportRow(dr[i]);
                }
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource=table();
            }
        }
        public DataTable table()
        {
            DataTable dd = new DataTable();
            dd.Columns.Add("序号", typeof(string));
            dd.Columns.Add("厂区", typeof(string));
            dd.Columns.Add("进仓数量", typeof(string));
            dd.Columns.Add("不良数", typeof(string));
            dd.Columns.Add("不良率", typeof(string));
            dd.Columns.Add("合格数", typeof(string));
            dd.Columns.Add("合格率", typeof(string));
            dd.Columns.Add("PO检验数", typeof(string));
            dd.Columns.Add("PO合格数", typeof(string));
            dd.Columns.Add("PO合格率", typeof(string));
            dd.Columns.Add("PO不合格数", typeof(string));
            dd.Columns.Add("PO不合格率", typeof(string));
            dd.Columns.Add("汇总月份", typeof(string));
            DataRow dr1 = dd.NewRow();
            dr1["序号"] = 1;
            dr1["厂区"] = "鞋厂1号";
            dr1["进仓数量"] = 20;
            dr1["不良数"] = 2;
            dr1["不良率"] = "10%";
            dr1["合格数"] = 18;
            dr1["合格率"] = "90%";
            dr1["PO检验数"] = 25;
            dr1["PO合格数"] = 20;
            dr1["PO合格率"] = "80%";
            dr1["PO不合格数"] = 5;
            dr1["PO不合格率"] = "20%";
            dr1["汇总月份"] = 1;
            dd.Rows.Add(dr1);

            DataRow dr2 = dd.NewRow();
            dr2["序号"] = 2;
            dr2["厂区"] = "鞋厂2号";
            dr2["进仓数量"] = 50;
            dr2["不良数"] =3 ;
            dr2["不良率"] = "6%";
            dr2["合格数"] = 47;
            dr2["合格率"] = "94%";
            dr2["PO检验数"] = 25;
            dr2["PO合格数"] = 24;
            dr2["PO合格率"] = "96%";
            dr2["PO不合格数"] = 1;
            dr2["PO不合格率"] = "4%";
            dr2["汇总月份"] = 3;
            dd.Rows.Add(dr2);

            DataRow dr3 = dd.NewRow();
            dr3["序号"] = 3;
            dr3["厂区"] = "鞋厂2号";
            dr3["进仓数量"] = 21;
            dr3["不良数"] = 1;
            dr3["不良率"] = "1.1%";
            dr3["合格数"] = 20;
            dr3["合格率"] = "98.1%";
            dr3["PO检验数"] = 40;
            dr3["PO合格数"] = 45;
            dr3["PO合格率"] = "89.9%";
            dr3["PO不合格数"] = 5;
            dr3["PO不合格率"] = "10.1%";
            dr3["汇总月份"] = 3;
            dd.Rows.Add(dr3);

            DataRow dr4 = dd.NewRow();
            dr4["序号"] = 4;
            dr4["厂区"] = "鞋厂1号";
            dr4["进仓数量"] = 30;
            dr4["不良数"] = 1;
            dr4["不良率"] = "3%";
            dr4["合格数"] = 29;
            dr4["合格率"] = "97%";
            dr4["PO检验数"] = 25;
            dr4["PO合格数"] = 20;
            dr4["PO合格率"] = "80%";
            dr4["PO不合格数"] = 5;
            dr4["PO不合格率"] = "20%";
            dr4["汇总月份"] = 1;
            dd.Rows.Add(dr4);

            DataRow dr5 = dd.NewRow();
            dr5["序号"] = 5;
            dr5["厂区"] = "鞋厂1号";
            dr5["进仓数量"] = 20;
            dr5["不良数"] = 2;
            dr5["不良率"] = "10%";
            dr5["合格数"] = 18;
            dr5["合格率"] = "90%";
            dr5["PO检验数"] = 60;
            dr5["PO合格数"] = 57;
            dr5["PO合格率"] = "96.66%";
            dr5["PO不合格数"] = 3;
            dr5["PO不合格率"] = "33.33%";
            dr5["汇总月份"] = 1;
            dd.Rows.Add(dr5);

            DataRow dr6 = dd.NewRow();
            dr6["序号"] = 6;
            dr6["厂区"] = "鞋厂4号";
            dr6["进仓数量"] = 33;
            dr6["不良数"] = 1;
            dr6["不良率"] = "3.33%";
            dr6["合格数"] = 32;
            dr6["合格率"] = "96.66%";
            dr6["PO检验数"] = 20;
            dr6["PO合格数"] = 20;
            dr6["PO合格率"] = "100%";
            dr6["PO不合格数"] = 0;
            dr6["PO不合格率"] = "0%";
            dr6["汇总月份"] = 1;
            dd.Rows.Add(dr6);

            DataRow dr7 = dd.NewRow();
            dr7["序号"] = 7;
            dr7["厂区"] = "鞋厂3号";
            dr7["进仓数量"] = 20;
            dr7["不良数"] = 2;
            dr7["不良率"] = "10%";
            dr7["合格数"] = 18;
            dr7["合格率"] = "90%";
            dr7["PO检验数"] = 25;
            dr7["PO合格数"] = 20;
            dr7["PO合格率"] = "80%";
            dr7["PO不合格数"] = 5;
            dr7["PO不合格率"] = "20%";
            dr7["汇总月份"] = 4;
            dd.Rows.Add(dr7);

            DataRow dr8 = dd.NewRow();
            dr8["序号"] = 8;
            dr8["厂区"] = "鞋厂3号";
            dr8["进仓数量"] = 20;
            dr8["不良数"] = 1;
            dr8["不良率"] = "5%";
            dr8["合格数"] = 19;
            dr8["合格率"] = "95%";
            dr8["PO检验数"] = 40;
            dr8["PO合格数"] = 40;
            dr8["PO合格率"] = "100%";
            dr8["PO不合格数"] = 0;
            dr8["PO不合格率"] = "0%";
            dr8["汇总月份"] = 7;
            dd.Rows.Add(dr8);

            DataRow dr9 = dd.NewRow();
            dr9["序号"] = 9;
            dr9["厂区"] = "鞋厂4号";
            dr9["进仓数量"] = 11;
            dr9["不良数"] = 2;
            dr9["不良率"] = "19%";
            dr9["合格数"] = 9;
            dr9["合格率"] = "81%";
            dr9["PO检验数"] = 20;
            dr9["PO合格数"] = 20;
            dr9["PO合格率"] = "100%";
            dr9["PO不合格数"] = 0;
            dr9["PO不合格率"] = "0%";
            dr9["汇总月份"] = 12;
            dd.Rows.Add(dr9);
            return dd;

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
