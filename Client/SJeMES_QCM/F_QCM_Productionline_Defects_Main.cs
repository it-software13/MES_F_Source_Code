using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SJeMES_QCM
{
    public partial class F_QCM_Productionline_Defects_Main : MaterialForm
    {

        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Productionline_Defects_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_Productionline_Defects_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
        }

        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 部门产线视图展示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("department_no", txt_Department_no.Text.Trim());
                p.Add("department_name", txt_Department_name.Text.Trim().ToString());
                p.Add("productionline_no", txt_productionline_no.Text.Trim().ToString());
                p.Add("productionline_name", txt_productionline_name.Text.Trim().ToString());

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.Quality_DepartmentBase",//类名
                                            "GetQuality_DepartmentList",//方法名
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
                        dgvr.Cells["department_no"].Value = dr["department_no"].ToString();
                        dgvr.Cells["department_name"].Value = dr["department_name"].ToString();
                        dgvr.Cells["productionline_no"].Value = dr["productionline_no"].ToString();
                        dgvr.Cells["productionline_name"].Value = dr["productionline_name"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                GenClass.AutoSizeColumn(dataGridView1);
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Columns["Operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    if (name == "Operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["Operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("UPDATE"))
                        {


                            string department_no = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                            string department_name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                            string productionline_no = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                            string productionline_name = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                            DataTable dt = new DataTable();
                            dt.Columns.Add("department_no", typeof(string));
                            dt.Columns.Add("department_name", typeof(string));
                            dt.Columns.Add("productionline_no", typeof(string));
                            dt.Columns.Add("productionline_name", typeof(string));

                            DataRow dr = dt.NewRow();


                            dr["department_no"] = department_no;
                            dr["department_name"] = department_name;
                            dr["productionline_no"] = productionline_no;
                            dr["productionline_name"] = productionline_name;

                            dt.Rows.Add(dr);
                            F_QCM_Productionline_Defects_Detail add = new F_QCM_Productionline_Defects_Detail(dt);
                            add.ShowDialog();
                            FormLoad();
                        }
                        else if (cell.CurrentItem.Equals("DELETE"))
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            FormLoad();
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
