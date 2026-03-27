using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library;
using SJeMES_Control_Library.Forms;
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

namespace SJeMES_QCM
{
    public partial class F_QCM_ATR_File1 : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public F_QCM_ATR_File1()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
         Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {


        }

        public void BindData(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {


                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                p.Add("TYPE", "验货");
                p.Add("ART", txt_art.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ARTFileBind",//类名
                                            "GetFileList",//方法名
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
                        dgvr.Cells["文件类型"].Value = dr["FILE_TYPE_TEXT"].ToString();
                        dgvr.Cells["有效文件"].Value = dr["FILE_NAME"].ToString();
                        dgvr.Cells["FILE_URL"].Value = dr["FILE_URL"].ToString();
                        dgvr.Cells["FILE_TYPE"].Value = dr["FILE_TYPE"].ToString();
                        dgvr.Cells["ART"].Value = dr["ART"].ToString();
                        TimeSpan sp = DateTime.Parse(dr["EFFECTIVE_DATE"].ToString()).Subtract(DateTime.Now);
                        dgvr.Cells["有效时长"].Value = sp.Days+"天";
                        dgvr.Cells["有效日期"].Value = dr["EFFECTIVE_DATE"].ToString();
                        dgvr.Cells["绑定日期"].Value = dr["BIND_DATE"].ToString();
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        i++;
                    }
                }
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation1"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                totalCount = int.Parse(dic["rowCount"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            F_QCM_ATR_File1_Edit frm = new F_QCM_ATR_File1_Edit(this, null);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void F_QCM_ATR_File1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += BindData;
            FormLoad();
        }

        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "operation1")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation1"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("edit"))//编辑
                    {
                        string FILE_URL = this.dataGridView1.Rows[e.RowIndex].Cells["FILE_URL"].Value.ToString().Trim();
                        var dr = dataGridView1.Rows[e.RowIndex];
                        F_QCM_ATR_File1_Edit frm = new F_QCM_ATR_File1_Edit(this, dr);
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog();

                    }
                    else if (cell.CurrentItem.Equals("delete"))//删除
                    {
                        string ID = this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString().Trim();
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("ID", ID);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.ARTFileBind",//类名
                                                    "DeleteFile",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if(ret.IsSuccess)
                        {
                            MessageBox.Show("删除成功");
                            FormLoad();
                        }
                    }
                }
               
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "有效文件")
            {
                string FILE_URL = Program.Client.PicUrl + this.dataGridView1.Rows[e.RowIndex].Cells["FILE_URL"].Value.ToString().Trim();
                ShowFileHelper.ShowFile(FILE_URL);
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
