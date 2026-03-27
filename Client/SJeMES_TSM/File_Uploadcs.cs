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
using MaterialSkin.Controls;

namespace SJeMES_TSM
{
    public partial class File_Upload : MaterialForm
    {
        private string file_guid;
        private string file_name;

        public File_Upload()
        {
            InitializeComponent();
        }

        private void Btn_file_upload_Click(object sender, EventArgs e)
        {
            try
            {
                //创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;//支持多选
                string path = string.Empty;
                ofd.Title = "请选择文件";
                ofd.Filter = "所有文件|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < ofd.FileNames.Length; i++)
                    {
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, ofd.FileNames[i].ToString(), Program.Client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            file_guid = resultDIC["guid"].ToString();
                            file_name = ofd.SafeFileName.Substring(0, ofd.SafeFileName.LastIndexOf('.'));
                            lbl_file_name.Text = file_name;
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

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_commit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(file_name) || string.IsNullOrEmpty(file_guid))
                {
                    string tipsMsg = SJeMES_Framework.Common.UIHelper.UImsg("请上传文件！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    MessageBox.Show(tipsMsg);
                    return;
                }

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("file_name", file_name);
                p.Add("file_guid", file_guid);
                string retdata = WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",//类库名
                                            "SJ_TSMAPI.APC_Supplementary_Data",//类名
                                            "Commit_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                }
                else
                {
                    MessageBox.Show(ret.ErrMsg);
                    this.Close();
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
