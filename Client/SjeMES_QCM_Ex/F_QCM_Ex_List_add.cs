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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_List_add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string file_guid = string.Empty;// 文件上传 返回的guid
        public string file_name = string.Empty;// 文件上传 返回的名称
        public string _task_no = string.Empty;//
        public string _sclx = string.Empty;//
        public string _art = string.Empty;//

        public Dictionary<string, object> filedic = new Dictionary<string, object>();
        public List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();


        public F_QCM_Ex_List_add(string task_no,string sclx,string art)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _task_no = task_no;
            _sclx = sclx;
            _art = art;
        }

        private void btn_file_upload_Click(object sender, EventArgs e)
        {
            try
            {
                //Create a file popup selection window (including file name) object
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;//支持多选
                string path = string.Empty;
                ofd.Title = "Please select file";
                ofd.Filter = "All files|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string text = string.Empty;
                    filediclist = new List<Dictionary<string, object>>();

                    
                    for (int i = 0; i < ofd.FileNames.Length; i++)
                    {
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, ofd.FileNames[i].ToString(), Program.Client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            filedic = new Dictionary<string, object>();
                            //file_guid = resultDIC["guid"].ToString();
                            string name = System.IO.Path.GetFileNameWithoutExtension(ofd.FileNames[i].ToString());
                            filedic.Add("file_guid", resultDIC["guid"].ToString());
                            filedic.Add("file_name", name);
                            filediclist.Add(filedic);

                            text += name + ",";

                            
                        }
                    }
                    lbl_file_name.Text = text.TrimEnd(',');
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //提交【支持多个提交】
        private void btn_commit_Click(object sender, EventArgs e)
        {
            try
            {
                if (filediclist.Count == 0)
                {
                    string tipsMsg = SJeMES_Framework.Common.UIHelper.UImsg("Please upload the file！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    MessageBox.Show(tipsMsg);
                    return;
                }
                int i = 0;

                Dictionary<string, object> p = new Dictionary<string, object>();
                //p.Add("file", );
                p.Add("file_name", filedic["file_name"]);
                p.Add("file_guid", filedic["file_guid"]);
                p.Add("task_no", _task_no);
                p.Add("sclx", _sclx);
                p.Add("art", _art);

                string retdata = WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "Commit_MainSc",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                else
                {
                    MessageBox.Show("Submitted successfully！");
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
