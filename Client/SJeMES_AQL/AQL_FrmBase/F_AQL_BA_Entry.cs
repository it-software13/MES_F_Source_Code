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

namespace SJeMES_AQL.AQL_FrmBase
{
    public partial class F_AQL_BA_Entry : Form
    {
        Dictionary<string, object> dics = new Dictionary<string, object>();
        List<string> FineItemList = new List<string>();
        public F_AQL_BA_Entry(Dictionary<string, object> _dics)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dics = _dics;
            DisabledEdit();
        }
        /// <summary>
        /// 禁用按钮【old】
        /// </summary>
        public void DisabledEdit()
        {
            if (dics["effective_status"].ToString() == "失效" || dics["BA_EDIT_STATE"].ToString() == "1")
            {
                //groupBox1.Enabled = false;
                //groupBox2.Enabled = false;
                //groupBox3.Enabled = false;
                btn_submit.Enabled = false;

            }

            
        }
        /// <summary>
        /// 禁用按钮方法【需求不允许禁用变为灰色】
        /// </summary>
        public bool EnableEdit()
        {
            if (dics["effective_status"].ToString() == "Fail" || dics["BA_EDIT_STATE"].ToString() == "1")//失效
            {
                return false;

            }
            return true;
        }
        
        private void F_AQL_BA_Entry_Load(object sender, EventArgs e)
        {
            this.splitContainer1.Panel1.Controls.Clear();
            F_AQL_Inspection_GeneralInformation uc = new F_AQL_Inspection_GeneralInformation("BA_Entry", dics);//BA录入
            //uc.TopLevel = false;

            //使用DockStyle进行填充
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            //将需要填充窗体的容器设置为窗体的父容器
            // uc.Parent = this.splitContainer1.Panel1;
            //使用内置函数ADD()进行窗体的添加
            this.splitContainer1.Panel1.Controls.Add(uc);

            this.FormBorderStyle = FormBorderStyle.None;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            GetBA_Entry();

        }

        //精美/不精美选择的数据
        public void FineItem_Btn(object sender, EventArgs e)
        {
            bool result = EnableEdit();
            if (!result)
                return;

            Button button = (Button)sender;
            if (!FineItemList.Contains(button.Text))
            {
                FineItemList.Add(button.Text);
            }
            button11.Text = "Not_Beautifully_Confirmed";//不精美确认
        }

        //只能输入整数
        public void zhengshu(object sender, KeyPressEventArgs e)
        {
            bool result = EnableEdit();
            if (!result)
                return;

            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }

        //精美/不精美确认
        private void button11_Click(object sender, EventArgs e)
        {
            bool result = EnableEdit();
            if (!result)
                return;

            if (button11.Text == "Beautiful confirm")//精美确认
                textBox2.Text = (Convert.ToInt32(textBox2.Text) + 1).ToString();
            else
            {
                textBox1.Text = (Convert.ToInt32(textBox1.Text) + 1).ToString();
                for (int i = 0; i < FineItemList.Count; i++)
                {
                    switch (FineItemList[i])
                    {
                        case "C1":
                            textBox3.Text = (Convert.ToInt32(textBox3.Text) + 1).ToString();
                            break;
                        case "C2":
                            textBox4.Text = (Convert.ToInt32(textBox4.Text) + 1).ToString();
                            break;
                        case "C3":
                            textBox5.Text = (Convert.ToInt32(textBox5.Text) + 1).ToString();
                            break;
                        case "C4":
                            textBox6.Text = (Convert.ToInt32(textBox6.Text) + 1).ToString();
                            break;
                        case "C5":
                            textBox7.Text = (Convert.ToInt32(textBox7.Text) + 1).ToString();
                            break;
                        case "C6":
                            textBox8.Text = (Convert.ToInt32(textBox8.Text) + 1).ToString();
                            break;
                        case "C7":
                            textBox9.Text = (Convert.ToInt32(textBox9.Text) + 1).ToString();
                            break;
                        case "C8":
                            textBox10.Text = (Convert.ToInt32(textBox10.Text) + 1).ToString();
                            break;
                        case "C9":
                            textBox11.Text = (Convert.ToInt32(textBox11.Text) + 1).ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            decimal bjms = Convert.ToDecimal(textBox1.Text);
            decimal jms = Convert.ToDecimal(textBox2.Text);
            textBox12.Text = Math.Round((((bjms + jms) - bjms) / (bjms + jms) * 5), 2).ToString();
            label6.Text = (bjms + jms).ToString();

            EditFineItem();
        }

        /// <summary>
        /// 编辑-BA录入-精美/不精美确认
        /// </summary>
        public void EditFineItem()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString());
                data.Add("star_level", textBox12.Text.Trim());
                data.Add("exquisite_qty", textBox2.Text.Trim());
                data.Add("not_exquisite_qty", textBox1.Text.Trim());
                data.Add("c1_qty", textBox3.Text.Trim());
                data.Add("c2_qty", textBox4.Text.Trim());
                data.Add("c3_qty", textBox5.Text.Trim());
                data.Add("c4_qty", textBox6.Text.Trim());
                data.Add("c5_qty", textBox7.Text.Trim());
                data.Add("c6_qty", textBox8.Text.Trim());
                data.Add("c7_qty", textBox9.Text.Trim());
                data.Add("c8_qty", textBox10.Text.Trim());
                data.Add("c9_qty", textBox11.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_BA_Entry", "EditFineItem", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    MessageBox.Show("Confirmed Success!");
                    button11.Text = "Beautiful confirm";//精美确认
                    FineItemList.Clear();
                    GetBA_Entry();
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
        /// 查询-BA录入
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetBA_Entry()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("task_no", dics["task_no"]);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_BA_Entry",//类名
                                            "GetBA_Entry",//方法名
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

                if (dt.Rows.Count > 0)
                {
                    textBox12.Text = dt.Rows[0]["star_level"].ToString();
                    textBox2.Text = dt.Rows[0]["exquisite_qty"].ToString();
                    textBox1.Text = dt.Rows[0]["not_exquisite_qty"].ToString();
                    textBox3.Text = dt.Rows[0]["c1_qty"].ToString();
                    textBox4.Text = dt.Rows[0]["c2_qty"].ToString();
                    textBox5.Text = dt.Rows[0]["c3_qty"].ToString();
                    textBox6.Text = dt.Rows[0]["c4_qty"].ToString();
                    textBox7.Text = dt.Rows[0]["c5_qty"].ToString();
                    textBox8.Text = dt.Rows[0]["c6_qty"].ToString();
                    textBox9.Text = dt.Rows[0]["c7_qty"].ToString();
                    textBox10.Text = dt.Rows[0]["c8_qty"].ToString();
                    textBox11.Text = dt.Rows[0]["c9_qty"].ToString();

                    label6.Text = (Convert.ToInt32(dt.Rows[0]["exquisite_qty"].ToString()) + Convert.ToInt32(dt.Rows[0]["not_exquisite_qty"].ToString())).ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //BA星级只能输入小数
        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool result =  EnableEdit();
            if (!result)
                return;
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46) //小数点                          
            {
                if (textBox12.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(textBox12.Text, out oldf);
                    b2 = float.TryParse(textBox12.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        /// <summary>
        /// 编辑-BA录入-BA星级/不精美数/精美数
        /// </summary>
        public void Edit_BABjmJm(string state, string value, ref string ErrMsg)
        {
            try
            {
                decimal bjm = Convert.ToDecimal(textBox1.Text == "" ? "0" : textBox1.Text);//不精美数
                decimal jm = Convert.ToDecimal(textBox2.Text == "" ? "0" : textBox2.Text);//精美数
                decimal jys = jm + bjm;//检验数
                decimal baxj = 0;//BA星级
                if (jys != 0)
                    baxj = Math.Round(((jys - bjm) / jys) * 5, 2);//BA星级
                textBox12.Text = baxj.ToString();

                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString());
                data.Add("star_level", textBox12.Text);
                data.Add("state", state);
                data.Add("value", value);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_BA_Entry", "Edit_BABjmJm", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    //MessageBox.Show("确认成功!");
                    //GetBA_Entry();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                //SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                ErrMsg += msg;
            }
        }

        //BA星级/不精美数/精美数 回车
        private void BABjmJm_KeyDown(object sender, KeyEventArgs e)
        {
            bool result = EnableEdit();
            if (!result)
                return;

            TextBox tb = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    MessageBox.Show("Value cannot be empty!");//值不能为空
                    return;
                }

                if (MessageBox.Show("Please confirm whether to save？", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)//请确认是否保存//确认
                {
                    string ErrMsg = string.Empty;
                    Edit_BABjmJm(textBox1.Tag.ToString(), textBox1.Text,ref ErrMsg);
                    Edit_BABjmJm(textBox12.Tag.ToString(), textBox12.Text, ref ErrMsg);
                    Edit_BABjmJm(textBox2.Tag.ToString(), textBox2.Text, ref ErrMsg);
                    Edit_qty(textBox3.Tag.ToString(), textBox3.Text, ref ErrMsg);
                    Edit_qty(textBox4.Tag.ToString(), textBox4.Text, ref ErrMsg);
                    Edit_qty(textBox5.Tag.ToString(), textBox5.Text, ref ErrMsg);
                    Edit_qty(textBox6.Tag.ToString(), textBox6.Text, ref ErrMsg);
                    Edit_qty(textBox7.Tag.ToString(), textBox7.Text, ref ErrMsg);
                    Edit_qty(textBox8.Tag.ToString(), textBox8.Text, ref ErrMsg);
                    Edit_qty(textBox9.Tag.ToString(), textBox9.Text, ref ErrMsg);
                    Edit_qty(textBox10.Tag.ToString(), textBox10.Text, ref ErrMsg);
                    Edit_qty(textBox11.Tag.ToString(), textBox11.Text, ref ErrMsg);

                    if (!string.IsNullOrEmpty(ErrMsg))
                    {
                        MessageBox.Show(ErrMsg);
                    }
                    else
                    {
                        GetBA_Entry();
                        MessageBox.Show("Saved successfully!");//保存成功
                    }
                }
            }
        }

        /// <summary>
        /// 编辑-BA录入-c数
        /// </summary>
        public void Edit_qty(string state, string value, ref string ErrMsg)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString());
                data.Add("state", state);
                data.Add("value", value);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_BA_Entry", "Edit_qty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    //MessageBox.Show("确认成功!");
                    //GetBA_Entry();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                //SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                ErrMsg += msg;
            }
        }

        //c数 回车
        private void qty_KeyDown(object sender, KeyEventArgs e)
        {
            bool result = EnableEdit();
            if (!result)
                return;

            TextBox tb = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    MessageBox.Show("Value cannot be empty!");//值不能为空
                    return;
                }
                if (MessageBox.Show("Please confirm whether to save？", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)//请确认是否保存//确认
                {
                    string ErrMsg = string.Empty;
                    Edit_BABjmJm(textBox1.Tag.ToString(), textBox1.Text, ref ErrMsg);
                    Edit_BABjmJm(textBox12.Tag.ToString(), textBox12.Text, ref ErrMsg);
                    Edit_BABjmJm(textBox2.Tag.ToString(), textBox2.Text, ref ErrMsg);
                    Edit_qty(textBox3.Tag.ToString(), textBox3.Text, ref ErrMsg);
                    Edit_qty(textBox4.Tag.ToString(), textBox4.Text, ref ErrMsg);
                    Edit_qty(textBox5.Tag.ToString(), textBox5.Text, ref ErrMsg);
                    Edit_qty(textBox6.Tag.ToString(), textBox6.Text, ref ErrMsg);
                    Edit_qty(textBox7.Tag.ToString(), textBox7.Text, ref ErrMsg);
                    Edit_qty(textBox8.Tag.ToString(), textBox8.Text, ref ErrMsg);
                    Edit_qty(textBox9.Tag.ToString(), textBox9.Text, ref ErrMsg);
                    Edit_qty(textBox10.Tag.ToString(), textBox10.Text, ref ErrMsg);
                    Edit_qty(textBox11.Tag.ToString(), textBox11.Text, ref ErrMsg);

                    if (!string.IsNullOrEmpty(ErrMsg))
                    {
                        //SJeMES_Control_Library.MessageHelper.ShowErr(this, ErrMsg);
                        MessageBox.Show(ErrMsg);
                    }
                    else
                    {
                        GetBA_Entry();
                        MessageBox.Show("Saved successfully！");//保存成功
                    }
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            decimal bjm = Convert.ToDecimal(textBox1.Text == "" ? "0" : textBox1.Text);//不精美数
            decimal jm = Convert.ToDecimal(textBox2.Text == "" ? "0" : textBox2.Text);//精美数
            decimal jys = jm + bjm;//检验数
            decimal baxj = 0;//BA星级
            if (jys != 0)
                baxj = Math.Round(((jys - bjm) / jys) * 5, 2);//BA星级
            textBox12.Text = baxj.ToString();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            bool result = EnableEdit();
            if (!result)
                return;

            decimal bjm = Convert.ToDecimal(textBox1.Text == "" ? "0" : textBox1.Text);//不精美数
            decimal jm = Convert.ToDecimal(textBox2.Text == "" ? "0" : textBox2.Text);//精美数
            decimal jys = jm + bjm;//检验数
            decimal baxj = 0;//BA星级
            if (jys != 0)
                baxj = Math.Round(((jys - bjm) / jys) * 5, 2);//BA星级
            textBox12.Text = baxj.ToString();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to submit?!", "Submit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);//确认提交吗//提交

            if (dr == DialogResult.OK)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_BA_Entry", "EditBaState", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (ret.IsSuccess)
                {
                    MessageBox.Show("Confirmed_Success!");//确认成功
                    dics["BA_EDIT_STATE"] = "1";
                    DisabledEdit();
                    bool result = EnableEdit();
                    if (!result)
                        return;

                }
                else
                {
                    throw new Exception(ret.ErrMsg);
                }
            }
        }
    }
}
