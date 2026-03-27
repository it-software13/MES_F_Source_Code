using MaterialSkin;
using MaterialSkin.Controls;
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
using static SJeMES_IQC.F_IQC_VWarehouse_Main;

namespace SJeMES_BDM
{
    public partial class BDM_Quality_Documents_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string file_type = string.Empty;//文件分类
        public BDM_Quality_Documents_Main(string _file_type)
        {
            InitializeComponent();
            InitDateTimePicker(dateTimePicker1);
            InitDateTimePicker(dateTimePicker2);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            file_type = _file_type;
            switch (_file_type)
            {
                case "0":
                    //this.Text = "品质目标";
                    this.Text = "Quality Target";
                    break;
                case "1":
                    //this.Text = "组织架构";
                    this.Text = "Organization";
                    break;
                case "2":
                    this.Text = "WI";
                    label4.Visible = true;
                    comboBox1.Visible = true;
                    break;
                case "3":
                    //this.Text = "品质制度";
                    this.Text = "Quality System";
                    break;
                case "4":
                    //this.Text = "培训文件";
                    this.Text = "Training Documents";
                    break;
                case "5":
                    //this.Text = "品质报告";
                    this.Text = "Quality Report";
                    break;
                case "6":
                    //this.Text = "政策";
                    this.Text = "Policy";
                    break;
                case "7":
                    this.Text = "BPM";
                    break;
                default:
                    break;
            }

            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "   ";

            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "   ";
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

        private void BDM_Quality_Documents_Main_Load(object sender, EventArgs e)
        {
            GetRole_Edit();

            pageControl1.BindPageEvent += GetQuality_Documents_Main;
            LoadPage();
        }

        /// <summary>
        /// 查询-万邦品质文件/客户品质文件-主页-角色
        /// </summary>
        public void GetRole_Edit()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Quality_Documents",//类名
                                            "GetRole_Edit",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt1.Rows.Count > 0)
                {
                    comboBox1.DataSource = dt1;
                    comboBox1.DisplayMember = "value";
                    comboBox1.ValueMember = "code";
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 查询-万邦品质文件/客户品质文件-主页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetQuality_Documents_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("FILE_NAME", textBox1.Text.Trim());
                data.Add("REMARK", textBox2.Text.Trim());
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text))
                    data.Add("sUPLOAD_TIME", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text))
                    data.Add("eUPLOAD_TIME", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                data.Add("file_type", file_type);
                if (file_type == "2")
                    data.Add("role_no", comboBox1.SelectedValue.ToString());
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Quality_Documents",//类名
                                            "GetQuality_Documents_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

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
                        dgvr.Cells["序号"].Value = i + 1;
                        dgvr.Cells["mid"].Value = dr["mid"].ToString();
                        dgvr.Cells["文件名称"].Value = dr["FILE_NAME"].ToString();
                        dgvr.Cells["备注"].Value = dr["REMARK"].ToString();
                        dgvr.Cells["上传时间"].Value = Convert.ToDateTime(dr["UPLOAD_TIME"].ToString()).ToString("yyyy-MM-dd");
                        dgvr.Cells["fileurl"].Value = dr["FILE_URL"].ToString();
                        dgvr.Cells["文件状态"].Value = dr["FILE_STATE"].ToString();
                        dgvr.Cells["角色名称"].Value = dr["role_name"].ToString();
                        if (file_type == "2")
                            dataGridView1.Columns["角色名称"].Visible = true;
                        if (dr["FILE_STATE"].ToString() == "1")
                        {
                            //dgvr.Cells["冻结"].Value = "解冻";
                            dgvr.Cells["冻结"].Value = "Thaw";
                            ((DataGridViewDisableButtonCell)dgvr.Cells["预览"]).Enabled = false;
                            ((DataGridViewDisableButtonCell)dgvr.Cells["删除"]).Enabled = false;
                        }
                        else
                        {
                            //dgvr.Cells["冻结"].Value = "冻结";
                            dgvr.Cells["冻结"].Value = "Freeze";
                        }
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (BDM_Quality_Documents_Edit b = new BDM_Quality_Documents_Edit(file_type))
            {
                b.ShowDialog();
            }
            LoadPage();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "预览" && ((DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells["预览"]).Enabled)
                //if (dataGridView1.Columns[e.ColumnIndex].Name == "Preview" && ((DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells["预览"]).Enabled)
                {
                    string file_url = Program.Client.PicUrl + Convert.ToString(dataGridView1.CurrentRow.Cells["fileurl"].Value);
                    string file_name = Convert.ToString(dataGridView1.CurrentRow.Cells["文件名称"].Value);
                    //ShowFileHelper.ShowFile(file_url, file_name);
                    FrmShowFile frmShowFile = new FrmShowFile(file_url, file_name);
                    frmShowFile.ShowDialog();
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "冻结" && ((DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells["冻结"]).Enabled)
                //if (dataGridView1.Columns[e.ColumnIndex].Name == "Freeze" && ((DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells["冻结"]).Enabled)
                {
                    string mid = Convert.ToString(dataGridView1.CurrentRow.Cells["mid"].Value);
                    string file_state = Convert.ToString(dataGridView1.CurrentRow.Cells["文件状态"].Value);
                    EditQuality_Documents_State(mid, file_state);
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "删除" && ((DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells["删除"]).Enabled)
               // if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete" && ((DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells["删除"]).Enabled)
                {
                    string mid = Convert.ToString(dataGridView1.CurrentRow.Cells["mid"].Value);
                    DeleteQuality_Documents(mid);
                }
            }
        }

        /// <summary>
        /// 修改-万邦品质文件/客户品质文件-编辑页-改状态
        /// </summary>
        public void EditQuality_Documents_State(string mid, string file_state)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("mid", mid);
                data.Add("file_state", file_state);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.BDM_Quality_Documents", "EditQuality_Documents_State", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 删除-万邦品质文件/客户品质文件-编辑页
        /// </summary>
        public void DeleteQuality_Documents(string mid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("mid", mid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.BDM_Quality_Documents", "DeleteQuality_Documents", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
