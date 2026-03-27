using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class F_AQL_CmaTask_TaskList_Insert : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_AQL_CmaTask_TaskList_Insert()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        string _mer_po = string.Empty;
        string param_value = string.Empty;//制令数
        public F_AQL_CmaTask_TaskList_Insert(string mer_po)
        {
            InitializeComponent();
            _mer_po = mer_po;
            comboBox1.Enabled = false;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查询-新增AQL验货任务-PO
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetCmaTask_TaskList_InsertPo()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_CmaTask_TaskList",//类名
                                            "GetCmaTask_TaskList_InsertPo",//方法名
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
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "MER_PO";
                comboBox1.ValueMember = "MER_PO";

                bool b = false;
                if (!string.IsNullOrWhiteSpace(_mer_po))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        if (item["MER_PO"].ToString() == _mer_po)
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    {
                        GetCmaTask_TaskList_Insert_ponum(_mer_po);
                        comboBox1.SelectedValue = _mer_po;
                    }
                    else
                    {
                        MessageBox.Show("No such PO!");
                        this.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_AQL_CmaTask_TaskList_Insert_Load(object sender, EventArgs e)
        {
            GetCmaTask_TaskList_ParamValue();
            GetCmaTask_TaskList_InsertPo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count<=0)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Batch quantity cannot be empty!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            decimal valnum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                decimal currQty = 0;
                bool isDecimal = decimal.TryParse(dataGridView1.Rows[i].Cells["分批数量"].Value.ToString(), out currQty);
                if (!isDecimal)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Batch quantity must be a number!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);//分批数量必须为数字
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                if (currQty == 0)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Batch quantity cannot be 0!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);//分批数量不能为0
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                valnum += currQty;
            }
            if (valnum != Convert.ToDecimal(label4.Text))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Batch quantity sum is not equal to PO quantity!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);//分批数量和不等于PO数量
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }

            InsertCmaTask_TaskList_Insert();
        }

        /// <summary>
        /// 新增-新增AQL验货任务
        /// </summary>
        public void InsertCmaTask_TaskList_Insert()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("po", comboBox1.Text.ToString());
                List<string> lot_nums = new List<string>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    lot_nums.Add(dataGridView1.Rows[i].Cells["分批数量"].Value.ToString());
                }
                data.Add("lot_nums", lot_nums);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_CmaTask_TaskList", "InsertCmaTask_TaskList_Insert", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.Rows.Add();
            dataGridView1.Rows[i].Cells["分批数量"].Value = "0";
        }

        /// <summary>
        /// 查询-制令分界设置-分界数量
        /// </summary>
        public void GetCmaTask_TaskList_ParamValue()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_CmaTask_TaskList",//类名
                                            "GetCmaTask_TaskList_ParamValue",//方法名
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
                //var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                param_value = dic["param_value"].ToString();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 查询-选择AQL验货任务-PO数量
        /// </summary>
        public void GetCmaTask_TaskList_Insert_ponum(string mer_po)
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("po", mer_po);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_CmaTask_TaskList",//类名
                                            "GetCmaTask_TaskList_Insert_ponum",//方法名
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
                //var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                label4.Text = dic["PO_NUM"].ToString();
                if (string.IsNullOrWhiteSpace(dic["PO_NUM"].ToString()))
                    return;
                decimal dec_param_value = Convert.ToDecimal(param_value);
                decimal po_num = Convert.ToDecimal(dic["PO_NUM"].ToString());
                int count = 1;
                if (dec_param_value > 0 && po_num>0)
                    count = Convert.ToInt32(Math.Ceiling(po_num / dec_param_value));
                dataGridView1.Rows.Clear();
                for (int i = 0; i < count; i++)
                {
                    int a = dataGridView1.Rows.Add();
                    if (po_num > dec_param_value)
                    {
                        dataGridView1.Rows[a].Cells["分批数量"].Value = dec_param_value;
                        po_num -= dec_param_value;
                    }
                    else
                    {
                        dataGridView1.Rows[a].Cells["分批数量"].Value = po_num;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            GetCmaTask_TaskList_Insert_ponum(comboBox1.SelectedValue.ToString());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "减")
                {
                    if (dataGridView1.Rows.Count > 1)
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                    textBox2.Visible = false;
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "分批数量")
                {
                    string aa = dataGridView1.CurrentRow.Cells["分批数量"].Value is null ? "" : dataGridView1.CurrentRow.Cells["分批数量"].Value.ToString();
                    string 分批数量 = aa == "" ? "" : aa;
                    textBox2.Text = 分批数量; //分批数量

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox2.Visible = true;
                }
                else
                {
                    textBox2.Visible = false;
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox2.Text.ToString();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            GetCmaTask_TaskList_Insert_ponum(comboBox1.Text.ToString());
        }
    }
}
