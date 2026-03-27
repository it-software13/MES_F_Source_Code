using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls.Btn;
using SJeMES_Control_Library.Controls.DataGridView;
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

namespace SJeMES_QA
{
    public partial class F_QA_ShoeShape_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QA_ShoeShape_Main()
        {
           materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        /// <summary>
        /// QA鞋型管理视图
        /// </summary>
        /// <param name="testtype_name"></param>
        /// <param name="testitem_code"></param>
        /// <param name="testitem_name"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("develop_season", txt_develop_season.Text.Trim());
                p.Add("shoe_no", txt_shoe_no.Text.Trim().ToString());
                p.Add("prod_no", txt_prod_no.Text.Trim().ToString()); 
                p.Add("develop_type", txt_develop_type.Text.Trim().ToString()); 

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.QAShoeShapeTable",//类名
                                            "GET_ShoeShapeTable_List",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Height =60;
                        dgvr.Cells["DEVELOP_SEASON"].Value = dr["DEVELOP_SEASON"].ToString();
                        dgvr.Cells["SHOE_NO"].Value = dr["SHOE_NO"].ToString(); 
                        dgvr.Cells["DEVELOP_TYPE"].Value = dr["DEVELOP_TYPE"].ToString(); 
                        dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
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
                        dgvr.Cells["Limitedrelease"].Value = dr["Limitedrelease"].ToString() == enum_qa_file_type.enum_qa_file_type_0 ? "有" : "";
                        dgvr.Cells["Disclimer"].Value = dr["Disclimer"].ToString() == enum_qa_file_type.enum_qa_file_type_1 ? "有" : "";
                        dgvr.Cells["Visualstandard"].Value = dr["Visualstandard"].ToString() == enum_qa_file_type.enum_qa_file_type_2? "有" : "";
                        dgvr.Cells["Other"].Value = dr["Other"].ToString() == enum_qa_file_type.enum_qa_file_type_3 ? "有" : "";

                        i++;
                    }
                    //GenClass.AutoSizeColumn(dataGridView1);
                   
                }
               
                totalCount = int.Parse(dic["rowCount"].ToString());
               
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void F_QA_ShoeShape_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;

            pageControl1.BindPageEvent += GetDataList;
            //GetDataList();
            FormLoad();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }
        private void btn_Select_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        /// <summary>
        /// 单元格点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        if (cell.CurrentItem.Equals("detail"))
                        {
                            //季度
                            string DEVELOP_SEASON = Convert.ToString(dataGridView1.CurrentRow.Cells["DEVELOP_SEASON"].Value);
                            //鞋型
                            string SHOE_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["SHOE_NO"].Value);
                            if (!string.IsNullOrEmpty(DEVELOP_SEASON) ||
                                !string.IsNullOrEmpty(SHOE_NO))
                            {
                                F_QA_ShoeShape_List aa = new F_QA_ShoeShape_List(DEVELOP_SEASON, SHOE_NO);
                                aa.ShowDialog();
                                FormLoad();
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
