using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls.Btn;
using SJeMES_Control_Library.Controls.DataGridView;
using SJeMES_Control_Library.Forms;
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

namespace SJeMES_BDM
{
    public partial class F_QCM_Testltem_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Testltem_List()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_QCM_Testltem_List_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //只要加载一次委托 
            pageControl1.BindPageEvent += GetDataList;
            //GetDataList();
            FormLoad();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;

        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            //GetDataList();
            FormLoad();
        }
        /// <summary>
        /// 查询
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
                p.Add("testtype_no", txt_testtype_name.Text.Trim());
                p.Add("testitem_code", txt_testitem_code.Text.Trim().ToString());
                p.Add("testitem_name", txt_testitem_name.Text.Trim().ToString());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.TableView",//类名
                                            "GetBDM_TESTITEMList",//方法名
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
                if (dt.Rows.Count>0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["testtype_no"].Value = dr["testtype_no"].ToString();
                        dgvr.Cells["testtype_name"].Value = dr["testtype_name"].ToString();
                        dgvr.Cells["testitem_code"].Value = dr["testitem_code"].ToString();
                        dgvr.Cells["testitem_name"].Value = dr["testitem_name"].ToString();
                        dgvr.Cells["sample_num"].Value = dr["sample_num"].ToString();
                        dgvr.Cells["formula_name_1"].Value = dr["formula_name_1"].ToString();
                        dgvr.Cells["formula_name_2"].Value = dr["formula_name_2"].ToString();
                        dgvr.Cells["enum_value_1"].Value = dr["enum_value_1"].ToString();
                        dgvr.Cells["remarks"].Value = dr["remarks"].ToString(); 
                        dgvr.Cells["AQL_LEVEL"].Value = dr["AQL_LEVEL"].ToString(); //AQL级别

                         i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
               
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, EventArgs e)
        { 
            //添加
            F_QCM_Testltem_Edit add = new F_QCM_Testltem_Edit(string.Empty);
            add.ShowDialog();
            //GetDataList();
            FormLoad();
        }
        private void txt_testtype_name_DoubleClick(object sender, EventArgs e)
        {
            string sql = "select TESTTYPE_NO as 检测项类型编号,TESTTYPE_NAME as 检测项类型名称 from BDM_TESTTYPE_M";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_testtype_name.Text = frmData.RetData.Rows[0]["检测项类型编号"].ToString(); 
            }
        }
        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
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
                        if (cell.CurrentItem.Equals("UPDATE"))//修改
                        {
                            DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                            string id = Convert.ToString(dataGridView1.CurrentRow.Cells["testitem_code"].Value);
                            using (F_QCM_Testltem_Edit add = new F_QCM_Testltem_Edit(id))
                            {
                                add.ShowDialog();
                                //GetDataList();
                                FormLoad();
                            }
                        }
                        else if (cell.CurrentItem.Equals("DELETE"))//删除
                        {
                            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                                string id = Convert.ToString(dataGridView1.CurrentRow.Cells["testitem_code"].Value);

                                // 新增测试项数据
                                try
                                {
                                    //请求api的数据展示
                                    Dictionary<string, object> p = new Dictionary<string, object>();
                                    p.Add("data", id);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                                Program.Client.APIURL,
                                                                "SJ_QCMAPI",//类库名
                                                                "SJ_QCMAPI.BDMBASE",//类名
                                                                "GetBDM_TESTITEMDelect",//方法名
                                                                Program.Client.UserToken,//token
                                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        MessageBox.Show("删除数据成功");
                                        //GetDataList();
                                        FormLoad();
                                    }
                                }
                                catch (Exception ex)
                                {

                                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                }
                            }
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
