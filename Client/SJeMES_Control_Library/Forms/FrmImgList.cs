using DataGrid.DataGridViewCustomColumn;
using SJeMES_Control_Library.Controls.Btn;
using SJeMES_Control_Library.Controls.DataGridView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Forms
{
    public partial class FrmImgList : Form
    {
        DataTable dt = new DataTable();
        string stypes = string.Empty;
        public FrmImgList()
        {
            InitializeComponent();
        }

        public FrmImgList(DataTable img,List<string> lst =null,string stype="")
        {
            
            InitializeComponent();
            this.dt = img;
            stypes = stype;
        }

        private void FrmImgList_Load(object sender, EventArgs e)
        {
            switch (stypes)
            {
                case "1":
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["img_name"].Value = dr["img_name"].ToString();
                            dgvr.Cells["img_url"].Value = dr["img_url"].ToString();
                            i++;
                        }
                    }
                    break;
                case "2":
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["img_name"].Value = dr["img_name"].ToString();
                            dgvr.Cells["img_url"].Value = dr["img_url"].ToString();
                            i++;
                        }
                    }

                    break;
                case "3"://金属检验
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["img_name"].Value = dr["img_name"].ToString();
                            dgvr.Cells["img_url"].Value = dr["img_url"].ToString();
                            i++;
                        }
                    }

                    break;
                case "4"://品质审核历史图片展示
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["img_name"].Value = dr["IMG_NAME"].ToString();
                            dgvr.Cells["img_url"].Value = dr["IMG_URL"].ToString();
                            i++;
                        }
                    }

                    break;
                case "5"://重检报告图片展示
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["img_name"].Value = dr["IMG_NAME"].ToString();
                            dgvr.Cells["img_url"].Value = dr["IMG_URL"].ToString();
                            i++;
                        }
                    }

                    break;
                case "6"://不良退货
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dataGridView1.Rows[i].Cells["img_url"].Value = dr["IMG_URL"].ToString();
                            dataGridView1.Rows[i].Cells["img_name"].Value = dr["IMG_NAME"].ToString();
                            i++;
                        }
                    }

                    break;
                default:
                    foreach (DataRow dr in dt.Rows)
                    {
                        int i = dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells["img_name"].Value = dr["img_name"].ToString();
                        dataGridView1.Rows[i].Cells["img_url"].Value = dr["img_url"].ToString();
                    }
                    break;


            }
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;


        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                string img_url = dataGridView1.Rows[e.RowIndex].Cells["img_url"].Value.ToString();
                string img_name = dataGridView1.Rows[e.RowIndex].Cells["img_name"].Value.ToString();
                string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                if (name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell == null || cell.CurrentItem== null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("selectImg"))
                    {
                        ShowFileHelper.ShowFile(img_url, img_name);
                    } 
                }
            }
        }
    }
}
