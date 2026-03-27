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

namespace SJeMES_BDM
{
    public partial class F_BDM_Formula_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_Formula_List()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        /// <summary>
        /// 查询数据 
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
               
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("txt_code", txt_code.Text);
                p.Add("txt_remarks", txt_remarks.Text);
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.Formula",//类名
                                            "GetFormulaList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["formula_code_1"].Value = dr["formula_code_1"].ToString();
                        dgvr.Cells["formula_name_1"].Value = dr["formula_name_1"].ToString();
                        dgvr.Cells["formula_type_1"].Value = dr["formula_type_1"].ToString();
                        dgvr.Cells["formula_content_1"].Value = dr["formula_content_1"].ToString();
                        dgvr.Cells["remarks_1"].Value = dr["remarks_1"].ToString();
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
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }


        private void Frm_bdm_formulaList_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //只要加载一次委托 
            pageControl1.BindPageEvent += GetList;

            LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            //GetList();
        }

        /// <summary>
        /// 新建公式按钮事件
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            string formula_code = "";
            F_DBM_Formula_Edit add = new F_DBM_Formula_Edit(formula_code);
            add.ShowDialog();
            //GetList();
            LoadPage();
        }

        /// <summary>
        /// 搜索按钮事件
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            //GetList();
            LoadPage();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string formula_code = "";
            F_DBM_Formula_Edit add = new F_DBM_Formula_Edit(formula_code);
            add.ShowDialog();
            //GetList();
            LoadPage();
        }

        /// <summary>
        /// 初始化分页
        /// </summary>
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
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
                            string formula_code = dataGridView1.CurrentRow.Cells["formula_code_1"].Value.ToString();
                            string content = dataGridView1.CurrentRow.Cells["formula_type_1"].Value.ToString();
                            if (content == Formula_Type_enum.Type_enum_0)
                            {
                                MessageBox.Show("General formula cannot be modified！");
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(formula_code))
                                {
                                    F_DBM_Formula_Edit dd = new F_DBM_Formula_Edit(formula_code);
                                    dd.ShowDialog();
                                    LoadPage();
                                }
                                else
                                {
                                    MessageBox.Show("Please select the data to be modified！");
                                }
                            }
                        }
                        else if (cell.CurrentItem.Equals("DELETE"))//删除
                        {
                            string formula_code = dataGridView1.Rows[e.RowIndex].Cells["formula_code_1"].Value.ToString();
                            if (MessageBox.Show("confirm deletion? ", "This delete cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    //请求api的数据展示
                                    Dictionary<string, object> p = new Dictionary<string, object>();
                                    //键值对传值
                                    p.Add("formula_code", formula_code);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                                Program.Client.APIURL,
                                                                "SJ_QCMAPI",//类库名
                                                                "SJ_QCMAPI.Formula",//类名
                                                                "DelFormulaList",//方法名
                                                                Program.Client.UserToken,//token
                                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));

                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    MessageBox.Show("successfully deleted！");
                                    LoadPage();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
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
    }
}
