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

namespace SJeMES_QCM
{
    public partial class APP_Compliance_Print_Edit : MaterialForm
    {
        string guid = string.Empty;
        public APP_Compliance_Print_Edit(SJeMES_Framework.Class.ClientClass _Program)
        {
            InitializeComponent();
            Program.Client = _Program;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "Please select a folder";
            ofd.Filter = "image file(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    guid = resultDIC["guid"].ToString();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditAPP_Compliance_Maintenance();
        }

        /// <summary>
        /// 编辑-APP2合规-模板维护
        /// </summary>
        public void EditAPP_Compliance_Maintenance()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("space_str_1", textBox1.Text.Trim());
                data.Add("space_str_2", textBox2.Text.Trim());
                data.Add("space_str_3", textBox3.Text.Trim());
                data.Add("space_str_4", textBox4.Text.Trim());
                data.Add("space_str_5", textBox5.Text.Trim());
                data.Add("space_str_6", textBox6.Text.Trim());
                //if (string.IsNullOrWhiteSpace(guid))
                //{
                //    MessageBox.Show("请先上传图片!");
                //    return;
                //}
                data.Add("autograph_img_guid", guid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_APP_Compliance", "EditAPP_Compliance_Maintenance", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Tag = "成功";
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

        /// <summary>
        /// 查询-APP2合规-主页-模板维护
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public void GetAPP_Compliance_Maintenance()
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_APP_Compliance",//类名
                                            "GetAPP_Compliance_Maintenance",//方法名
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
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    textBox1.Text = dt.Rows[0]["space_str_1"].ToString();
                    textBox2.Text = dt.Rows[0]["space_str_2"].ToString();
                    textBox3.Text = dt.Rows[0]["space_str_3"].ToString();
                    textBox4.Text = dt.Rows[0]["space_str_4"].ToString();
                    textBox5.Text = dt.Rows[0]["space_str_5"].ToString();
                    textBox6.Text = dt.Rows[0]["space_str_6"].ToString();
                    guid = dt.Rows[0]["autograph_img_guid"].ToString();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void APP_Compliance_Print_Edit_Load(object sender, EventArgs e)
        {
            GetAPP_Compliance_Maintenance();
        }
    }
}
