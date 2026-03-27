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

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_file_Edit : MaterialForm
    {
        public string _id = "";
        public string _stockcode = "";
        public string _taskcode = "";
        private readonly MaterialSkinManager materialSkinManager;
        public bool bl = false;
        public F_QCM_Ex_file_Edit(string id = "", string stockcode = "", string taskcode = "")
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _id = id;
            _stockcode = stockcode;
            _taskcode = taskcode;
            if (!string.IsNullOrWhiteSpace(_id))
            {
                textBox2.Enabled = false;
            }
        }

        public F_QCM_Ex_file_Edit(string _sc_taskcode)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _taskcode = _sc_taskcode;
        }

        private void F_QCM_Ex_file_Edit_Load(object sender, EventArgs e)
        {
            textBox1.Text = _stockcode;
            textBox2.Text = _taskcode;
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Trim() != _stockcode)
                {
                    if (!string.IsNullOrEmpty(textBox1.Text.Trim()) && !string.IsNullOrEmpty(textBox2.Text.Trim()))
                    {
                        try
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("id", _id);
                            p.Add("Taskcode", textBox2.Text.Trim());//实验室编号
                            p.Add("Stockcode", textBox1.Text.Trim());//存放位置
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                      Program.Client.APIURL,
                                                      "SJ_QCMAPI",//类库名
                                                      "SJ_QCMAPI.ExShose",//类名
                                                      "EditARC",//方法名
                                                      Program.Client.UserToken,//token
                                                      Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                            if (ret.IsSuccess)
                            {
                                MessageBox.Show("Saved successfully");
                                this.Close();
                                bl = true;
                            }
                            else
                            {
                                MessageBox.Show(ret.ErrMsg);
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The location name cannot be the same as the original location name！");
                }
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Trim() != _stockcode)
                {
                    if (!string.IsNullOrEmpty(textBox1.Text.Trim()) && !string.IsNullOrEmpty(textBox2.Text.Trim()))
                    {
                        try
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("Taskcode", textBox2.Text.Trim());//实验室编号
                            p.Add("Stockcode", textBox1.Text.Trim());//存放位置
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                      Program.Client.APIURL,
                                                      "SJ_QCMAPI",//类库名
                                                      "SJ_QCMAPI.ExShose",//类名
                                                      "EditARC",//方法名
                                                      Program.Client.UserToken,//token
                                                      Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                            if (ret.IsSuccess)
                            {
                                MessageBox.Show("Saved successfully");
                                this.Close();
                                bl = true;
                            }
                            else
                            {
                                MessageBox.Show(ret.ErrMsg);
                            }
                        }
                        catch (Exception)
                        {
                            bl = true;
                            throw;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The location name cannot be the same as the original location name！");
                }
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
