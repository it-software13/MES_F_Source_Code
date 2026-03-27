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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public partial class F_BDM_KetCap_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string id = string.Empty;//检测项id
        public F_BDM_KetCap_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        string workshop_section_no = string.Empty;
        public F_BDM_KetCap_Main(string _workshop_section_no, SJeMES_Framework.Class.ClientClass client)
        {
            Program.Client = client;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            workshop_section_no = _workshop_section_no;

            this.button2.Enabled = false;
        }

        /// <summary>
        /// 查询检测项目类型
        /// </summary>
        public void Getenum_inspection_type()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("workshop_section_no", workshop_section_no);
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_KetCap",//类名
                                            "Getenum_inspection_type",//方法名
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
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "enum_value";
                comboBox1.ValueMember = "enum_value2";
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public List<DataGridView> dgv_list = new List<DataGridView>();
        public string stag_key = "wu";
        public string stag_key2 = "wu";

        /// <summary>
        /// 动态生成dgv控件
        /// </summary>
        public void InitialDgvData(DataTable dt)
        {
            this.splitContainer3.Panel1.Controls.Clear();
            this.splitContainer3.Panel2.Controls.Clear();

            dgv_list = new List<DataGridView>();
            //1.调用接口

            //2.根据接口返回结果条数，做循环，假设返回15条。
            //一个dgv最多10条，那就需要两个dgv
            int resCount = dt.Rows.Count;
            int dgvRowCount = 10;//dgv最大行数
            int dgvCount = (resCount + dgvRowCount - 1) / dgvRowCount;//计算dgv个数

            for (int i = 0; i < dgvCount; i++)
            {
                #region 建dgv
                DataGridView dataGridView = new DataGridView();
                dataGridView.Name = $@"dgv_{i}";
                dataGridView.Dock = DockStyle.Fill;
                var col1 = new DataGridViewColumn();
                var col2 = new DataGridViewColumn();
                var col3 = new DataGridViewColumn();
                var col4 = new DataGridViewColumn();
                //要插入列的类型
                col1.CellTemplate = new DataGridViewTextBoxCell();
                col1.Name = "id";
                col1.HeaderText = "id";
                col1.Visible = false;
                col1.ReadOnly = true;
                dataGridView.Columns.Insert(0, col1);
                col2.CellTemplate = new DataGridViewTextBoxCell();
                col2.Name = "inspection_code";
                col2.HeaderText = "Code name";
                col1.ReadOnly = true;
                dataGridView.Columns.Insert(1, col2);
                col3.CellTemplate = new DataGridViewTextBoxCell();
                col3.Name = "inspection_name";
                col3.HeaderText = "Bad items";//不良项
                col3.ReadOnly = true;
                dataGridView.Columns.Insert(2, col3);
                col4.CellTemplate = new DataGridViewTextBoxCell();
                col4.Name = "shortcut_key";
                col4.HeaderText = "Corresponding button";//对应按钮
                col4.ReadOnly = true;
                dataGridView.Columns.Insert(3, col4);
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView.ColumnHeadersHeight = 50;
                dataGridView.RowTemplate.Height = 30; //改变行的高度;
                int min = Math.Min(dgvRowCount, dt.Rows.Count);
                #endregion
                switch (i)
                {
                    case 0:
                        //分页读取接口返回数据
                        for (int a = 0; a < min; a++)
                        {
                            dataGridView.Rows.Add();
                            DataGridViewRow dgvr = dataGridView.Rows[a];
                            dgvr.Cells["id"].Value = dt.Rows[a]["id"].ToString();
                            dgvr.Cells["inspection_code"].Value = dt.Rows[a]["inspection_code"].ToString();
                            dgvr.Cells["inspection_name"].Value = dt.Rows[a]["inspection_name"].ToString();
                            dgvr.Cells["shortcut_key"].Value = dt.Rows[a]["shortcut_key"].ToString();
                        }
                        this.splitContainer3.Panel1.Controls.Add(dataGridView);
                        break;
                    case 1:
                        //分页读取接口返回数据
                        int b = 0;
                        for (int a = dgvRowCount; a < dt.Rows.Count; a++)
                        {
                            dataGridView.Rows.Add();
                            DataGridViewRow dgvr = dataGridView.Rows[b];
                            dgvr.Cells["id"].Value = dt.Rows[a]["id"].ToString();
                            dgvr.Cells["inspection_code"].Value = dt.Rows[a]["inspection_code"].ToString();
                            dgvr.Cells["inspection_name"].Value = dt.Rows[a]["inspection_name"].ToString();
                            dgvr.Cells["shortcut_key"].Value = dt.Rows[a]["shortcut_key"].ToString();
                            b++;
                        }
                        this.splitContainer3.Panel2.Controls.Add(dataGridView);
                        break;
                    default:
                        break;
                }
                dataGridView.CellClick += new DataGridViewCellEventHandler(dataGridView_CellClick);
                dataGridView.KeyPress += new KeyPressEventHandler(DGV_KeyPress);
                dataGridView.KeyDown += new KeyEventHandler(DGV_KeyDown);
                //dataGridView.KeyUp += new KeyEventHandler(DGV_KeyDown);
                dgv_list.Add(dataGridView);
            }
        }

        private void DGV_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex reg = new Regex(@"[!！@#$%\\^&*()./、{}【】~`·\\-_=\\+——><《》\\?？]+");

            const string pattern1 = @"^[0-9]*$"; const string pattern2 = @"^[A-Za-z]+$";
            if (!reg.IsMatch(e.KeyChar.ToString()))
            {
                if (!Regex.IsMatch(pattern1, e.KeyChar.ToString()) || !Regex.IsMatch(pattern2, e.KeyChar.ToString()))//如果不是字符 也不是数字
                {
                    e.Handled = true; //当前输入处理置为已处理。即文本框不再显示当前按键信息
                }
            }

        }

        /// <summary>
        /// 根据检测项查询数据
        /// </summary>
        /// <param name="tablename"></param>
        public void GetTestItem(string tablename)
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("tablename", tablename);//编号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_KetCap",//类名
                                            "GetTestItem",//方法名
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
                InitialDgvData(dt);
                var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                dataGridView1.Rows.Clear();
                if (dt2.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt2.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["序号"].Value = i + 1;
                        dgvr.Cells["操作"].Value = dr["tqc_key_name"].ToString();
                        dgvr.Cells["操作代号"].Value = dr["tqc_key"].ToString();
                        dgvr.Cells["对应按钮"].Value = dr["shortcut_key"].ToString();
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 动态添加dgv点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                foreach (var dgv in dgv_list)
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value.ToString() == "请输入按键")
                            {
                                if (stag_key != "wu")
                                {
                                    cell.Value = stag_key;
                                    stag_key = "wu";
                                    break;
                                }
                            }
                        }
                    }
                }

                DataGridView dataGridView1 = (DataGridView)sender;
                if (dataGridView1.Columns[e.ColumnIndex].Name == "shortcut_key") // textbox显示条件 
                {
                    stag_key = dataGridView1.Rows[e.RowIndex].Cells["shortcut_key"].Value.ToString();
                    dataGridView1.Rows[e.RowIndex].Cells["shortcut_key"].Value = "请输入按键";
                    id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                    string sorting = dataGridView1.CurrentRow.Cells["shortcut_key"].Value.ToString();
                }
            }
        }

        private void F_BDM_KetCap_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            Getenum_inspection_type();
            GetTestItem(comboBox1.SelectedValue.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTestItem(comboBox1.SelectedValue.ToString());
        }

        private void DGV_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (var dgv in dgv_list)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value.ToString() == e.KeyData.ToString())
                        {
                            cell.Value = string.Empty;
                        }
                    }
                }
            }

            foreach (var dgv in dgv_list)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value.ToString() == "请输入按键"
                            && ((Convert.ToInt32(e.KeyValue) >= 48 && Convert.ToInt32(e.KeyValue) <= 57)
                            || (Convert.ToInt32(e.KeyValue) >= 96 && Convert.ToInt32(e.KeyValue) <= 200)
                            || (Convert.ToInt32(e.KeyValue) >= 65 && Convert.ToInt32(e.KeyValue) <= 90)))
                        {

                            cell.Value = e.KeyCode.ToString();
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditTestItem();
        }

        /// <summary>
        /// 检测项编辑
        /// </summary>
        public void EditTestItem()
        {
            try
            {
                if (dgv_list.Count > 0)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    DataTable TestItem = new DataTable();
                    TestItem.Columns.Add(new DataColumn() { ColumnName = "id" });
                    TestItem.Columns.Add(new DataColumn() { ColumnName = "inspection_code" });
                    TestItem.Columns.Add(new DataColumn() { ColumnName = "inspection_name" });
                    TestItem.Columns.Add(new DataColumn() { ColumnName = "shortcut_key", DataType = typeof(String) });
                    for (int i = 0; i < dgv_list.Count; i++)
                    {
                        TestItem.Merge(GetDgvToTable(dgv_list[i]));
                    }
                    foreach (DataRow item in TestItem.Rows)
                    {
                        if (item["shortcut_key"].ToString() == "请输入按键")
                        {
                            item["shortcut_key"] = stag_key;
                        }
                    }
                    data.Add("tablename", comboBox1.SelectedValue.ToString());
                    data.Add("TestItem", TestItem);
                    DataTable TqcItem = GetDgvToTable(dataGridView1);
                    foreach (DataRow item in TqcItem.Rows)
                    {
                        if (item["对应按钮"].ToString() == "请输入按键")
                        {
                            item["对应按钮"] = stag_key2;
                        }
                    }
                    data.Add("TqcItem", TqcItem);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.BDM_KetCap", "EditTestItem", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                        GetTestItem(comboBox1.SelectedValue.ToString());
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
                }
                else
                {
                    MessageBox.Show("No data!!!");
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        /// <summary>
        /// dgv控件转datatable
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value.ToString() == "请输入按键")
                        {
                            if (stag_key2 != "wu")
                            {
                                cell.Value = stag_key2;
                                stag_key2 = "wu";
                                break;
                            }
                        }
                    }
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "对应按钮") // textbox显示条件 
                {
                    stag_key2 = dataGridView1.Rows[e.RowIndex].Cells["对应按钮"].Value.ToString();
                    dataGridView1.Rows[e.RowIndex].Cells["对应按钮"].Value = "请输入按键";
                }
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex reg = new Regex(@"[!！@#$%\\^&*()./、{}【】~`·\\-_=\\+——><《》\\?？]+");

            const string pattern1 = @"^[0-9]*$"; const string pattern2 = @"^[A-Za-z]+$";
            if (!reg.IsMatch(e.KeyChar.ToString()))
            {
                if (!Regex.IsMatch(pattern1, e.KeyChar.ToString()) || !Regex.IsMatch(pattern2, e.KeyChar.ToString()))//如果不是字符 也不是数字
                {
                    e.Handled = true; //当前输入处理置为已处理。即文本框不再显示当前按键信息
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (var dgv in dgv_list)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value.ToString() == e.KeyData.ToString())
                        {
                            cell.Value = string.Empty;
                        }
                    }
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString() == e.KeyData.ToString())
                    {
                        cell.Value = string.Empty;
                    }
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString() == "请输入按键"
                        && ((Convert.ToInt32(e.KeyValue) >= 48 && Convert.ToInt32(e.KeyValue) <= 57)
                        || (Convert.ToInt32(e.KeyValue) >= 96 && Convert.ToInt32(e.KeyValue) <= 200)
                        || (Convert.ToInt32(e.KeyValue) >= 65 && Convert.ToInt32(e.KeyValue) <= 90)))
                    {

                        cell.Value = e.KeyCode.ToString();
                    }
                }
            }
        }
    }

}
