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

namespace SJeMES_QA
{
    public partial class F_DQA_ShoeShape_trait_File : MaterialForm
    {
        public string shoe_no { get; set; }
        public F_DQA_ShoeShape_trait_File(string _shoe_no)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            shoe_no = _shoe_no;

            List<F_DQA_ShoeShape_trait_File_type> insplist = new List<F_DQA_ShoeShape_trait_File_type>();
            F_DQA_ShoeShape_trait_File_type t0 = new F_DQA_ShoeShape_trait_File_type();
            t0.code = "0";
            t0.value = "FD";
            insplist.Add(t0);
            F_DQA_ShoeShape_trait_File_type t1 = new F_DQA_ShoeShape_trait_File_type();
            t1.code = "1";
            t1.value = "VS";
            insplist.Add(t1);
            F_DQA_ShoeShape_trait_File_type t2 = new F_DQA_ShoeShape_trait_File_type();
            t2.code = "2";
            t2.value = "LR";
            insplist.Add(t2);
            F_DQA_ShoeShape_trait_File_type t3 = new F_DQA_ShoeShape_trait_File_type();
            t3.code = "3";
            t3.value = "Other";//其它
            insplist.Add(t3);
            cb_filetype.DataSource = insplist;
            cb_filetype.DisplayMember = "value";
            cb_filetype.ValueMember = "code";
        }

        private void UploadAll()
        {

            try
            {
                // string res = UpLoad("3", file_type);
                // 创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                //判断选择的路径
                string path = string.Empty;
                ofd.Title = "Please select a file";//请选择文件
                ofd.Filter = "All files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                    string filePath = ofd.FileName;

                    UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                    if (res.IsSuccess)
                    {
                        var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());

                        //保存文件信息 
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("shoes_code", shoe_no);//鞋型
                        data.Add("file_id", resultDIC["guid"].ToString());//文件关联id
                        data.Add("file_type", cb_filetype.SelectedValue);//文件类型
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "UploadtraitEditFile", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        if (!ret.IsSuccess)
                            throw new Exception(ret.ErrMsg);
                        else
                        {
                            //MessageBox.Show("上传文件成功！");
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Failed to upload file！");
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
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UploadAll();
        }
    }

    public class F_DQA_ShoeShape_trait_File_type
    {
        public string code { get; set; }
        public string value { get; set; }
    }
}
