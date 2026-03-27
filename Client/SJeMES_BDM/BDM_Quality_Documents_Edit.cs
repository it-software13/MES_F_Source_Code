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

namespace SJeMES_BDM
{
    public partial class BDM_Quality_Documents_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string file_type = string.Empty;//文件分类
        string file_guid = string.Empty;//文件关联id
        public BDM_Quality_Documents_Edit(string _file_type)
        {
            InitializeComponent();
            file_type = _file_type;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            if (_file_type=="2")
            {
                label1.Visible = true;
                comboBox1.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BDM_Quality_Documents_Edit_Load(object sender, EventArgs e)
        {
            GetRole_Edit();
        }

        /// <summary>
        /// 查询-万邦品质文件/客户品质文件-编辑页-角色
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

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                //判断选择的路径
                string path = string.Empty;
                ofd.Title = "请选择文件";
                ofd.Filter = "所有文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                    filePath = ofd.FileName;


                    UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                    if (res.IsSuccess)
                    {
                        var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                        file_guid = resultDIC["guid"].ToString();
                        label3.Text = ofd.SafeFileName;
                    }
                    else
                    {

                        MessageBox.Show("上传文件失败！");
                    }

                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(file_guid))
            {
                MessageBox.Show("请上传文件!");
                return;
            }
            EditQuality_Documents();
        }

        /// <summary>
        /// 保存-万邦品质文件/客户品质文件-编辑页
        /// </summary>
        public void EditQuality_Documents()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("remark", textBox1.Text.Trim());
                data.Add("file_type", file_type);
                data.Add("file_guid", file_guid);
                data.Add("role_no", comboBox1.SelectedValue.ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.BDM_Quality_Documents", "EditQuality_Documents", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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
    }
}
