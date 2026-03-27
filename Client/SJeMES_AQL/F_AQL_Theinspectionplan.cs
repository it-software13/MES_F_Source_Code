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

namespace SJeMES_AQL
{
    public partial class F_AQL_Theinspectionplan : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_AQL_Theinspectionplan()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void F_AQL_Theinspectionplan_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
           /* this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;*/
            pageControl1.BindPageEvent += GetMain_List;
            LoadPage();
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
        public void LoadPage()
        {
            pageControl1.PageSize = 10;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }
        public void GetMain_List(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                Dictionary<string, object> data = new Dictionary<string, object>();
                string putin_date = string.Empty;
                if (ucCheckBox1.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(this.dateTimePicker1.Text))
                    {
                        putin_date = Convert.ToDateTime(this.dateTimePicker1.Value).ToString("yyyy-MM-dd");
                    }
                }
                //键值对传值
                data.Add("plan_date", putin_date);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Theinspectionplan",//类名
                                            "Get_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["plan_datetext"].Value = dr["plan_date"].ToString()+ "Inspection plan";
                        dgvr.Cells["plan_date"].Value = dr["plan_date"].ToString();
                        dgvr.Cells["level_type"].Value = dr["level_type"].ToString();
                        dgvr.Cells["id"].Value = dr["id"].ToString(); 
                        dgvr.Cells["xh"].Value = i+1;
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (F_AQL_TheinspectionplanEdit frm=new F_AQL_TheinspectionplanEdit())
            {
                frm.ShowDialog();
                LoadPage();
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
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("update"))//删除
                        {
                            string id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                            string plan_date = dataGridView1.CurrentRow.Cells["plan_date"].Value.ToString();
                            string level_type = dataGridView1.CurrentRow.Cells["level_type"].Value.ToString();
                            F_AQL_TheinspectionplanEdit frm = new F_AQL_TheinspectionplanEdit(id, plan_date, level_type);
                            frm.ShowDialog();

                        }else if (cell.CurrentItem.Equals("select"))//删除
                        {
                            string id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                            string plan_date = dataGridView1.CurrentRow.Cells["plan_date"].Value.ToString();
                            string level_type = dataGridView1.CurrentRow.Cells["level_type"].Value.ToString();
                            F_AQL_TheinspectionplanEditSearch frm = new F_AQL_TheinspectionplanEditSearch(id, plan_date, level_type);
                            frm.ShowDialog();

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
