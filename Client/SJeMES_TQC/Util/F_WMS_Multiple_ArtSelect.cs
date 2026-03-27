using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using SJeMES_TQC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPT_WMS_Stoc_Matching
{
    public partial class F_WMS_Multiple_ArtSelect : MaterialForm
    {
        public delegate void DataChangeHandler(object sender, DataTableChangeEventArgs args, int row_index);
        public event DataChangeHandler DataChange;
        DataTable art_list;
        public int RowIndex;


        public F_WMS_Multiple_ArtSelect()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_TPM_RD_Item_Art_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);

            art_list = new DataTable();
            art_list.Columns.Add("productLine", typeof(string));
            art_list.Columns.Add("productLineName", typeof(string));
            art_list.Columns.Add("depart", typeof(string));
        }

        public void OnDataChange(object sender, DataTableChangeEventArgs args)
        {
            DataChange?.Invoke(this, args, RowIndex);
        }

        public class DataTableChangeEventArgs : EventArgs
        {
            public DataTable dataTable { get; set; }
            public DataTableChangeEventArgs(DataTable dt)
            {
                dataTable = dt;
            }
        }

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            StringFormat centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void btn_qyery_Click(object sender, EventArgs e)
        {
            QueryArtList();
        }

        private void text_art_no_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                QueryArtList();
        }

        private void text_art_name_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                QueryArtList();
        }

        private void QueryArtList()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Tag = 0;
            if (string.IsNullOrEmpty(text_art_no.Text.Trim()) && string.IsNullOrEmpty(text_art_name.Text.Trim()))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter at least one query condition！");
                return;
            }
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("productLine", text_art_no.Text.Trim());
            p.Add("depart", text_art_name.Text.Trim());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "GetProductLineAndDepartment", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject retData = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(ret);

            if (retData.IsSuccess)
            {
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData.RetData.ToString());
                dataGridView1.DataSource = dt;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.Rows.Count > 0)
            //{
            //    string this_status = dataGridView1.Tag == null ? "0" : dataGridView1.Tag.ToString() == "1" ? "1" : "0";
            //    string next_status = "0".Equals(this_status) ? "1" : "0";
            //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //    {
            //        dataGridView1.Rows[i].Cells["select_add"].Value = next_status;

            //        if ("1".Equals(next_status))
            //        {
            //            string art_no = dataGridView1.Rows[i].Cells["artno"].Value.ToString();
            //            DataRow[] drs = art_list.Select("ART_NO = '" + art_no + "'");
            //            if (drs.Length == 0)
            //            {
            //                DataRow dr = art_list.NewRow();
            //                dr["ART_NO"] = art_no;
            //                dr["ART_NAME"] = dataGridView1.Rows[i].Cells["artname"].Value.ToString();
            //                art_list.Rows.Add(dr);

            //                DataGridViewRow row = new DataGridViewRow();
            //                row.CreateCells(dataGridView2);
            //                row.Cells[dataGridView2.Columns["col_art"].Index].Value = art_no;
            //                dataGridView2.Rows.Add(row);
            //            }
            //        }
            //    }
            //    dataGridView1.Tag = next_status;
            //}
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == dataGridView1.Columns["select_add"].Index)
            {
                if ("1".Equals(dataGridView1.Rows[e.RowIndex].Cells["select_add"].Value))
                {
                    string product_line = dataGridView1.Rows[e.RowIndex].Cells["product_line"].Value.ToString();
                    DataRow[] drs = art_list.Select("productLine = '" + product_line + "'");
                    if (drs.Length == 0)
                    {
                        DataRow dr = art_list.NewRow();
                        dr["productLine"] = product_line;
                        dr["depart"] = dataGridView1.Rows[e.RowIndex].Cells["_depart"].Value.ToString();
                        art_list.Rows.Add(dr);

                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView2);
                        row.Cells[dataGridView2.Columns["col_art"].Index].Value = product_line;
                        dataGridView2.Rows.Add(row);
                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index > -1 && e.ColumnIndex > -1 && e.ColumnIndex == dataGridView2.Columns["col_delete"].Index)
            {
                string art_no = dataGridView2.Rows[index].Cells["col_art"].Value.ToString();
                DataRow[] drs = art_list.Select("productLine = '" + art_no + "'");
                if (drs.Length > 0)
                {
                    art_list.Rows.Remove(drs[0]);
                    dataGridView2.Rows.RemoveAt(index);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "异常了！重新选择吧！");
                    art_list.Rows.Clear();
                    dataGridView2.Rows.Clear();
                }
            }
        }

        private void btn_clear_all_Click(object sender, EventArgs e)
        {
            art_list.Rows.Clear();
            dataGridView2.Rows.Clear();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            text_art_no.Text = "";
            text_art_name.Text = "";
            dataGridView1.DataSource = null;
            dataGridView1.Tag = 0;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            OnDataChange(this, new DataTableChangeEventArgs(art_list));
            this.Close();
        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // F_WMS_Multiple_ArtSelect
        //    // 
        //    this.ClientSize = new System.Drawing.Size(405, 282);
        //    this.Name = "F_WMS_Multiple_ArtSelect";
        //    this.Load += new System.EventHandler(this.F_WMS_Multiple_ArtSelect_Load);
        //    this.ResumeLayout(false);

        //}

        private void F_WMS_Multiple_ArtSelect_Load(object sender, EventArgs e)
        {

        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // F_WMS_Multiple_ArtSelect
        //    // 
        //    this.ClientSize = new System.Drawing.Size(354, 263);
        //    this.Name = "F_WMS_Multiple_ArtSelect";
        //    this.ResumeLayout(false);

        //}
    }
}
