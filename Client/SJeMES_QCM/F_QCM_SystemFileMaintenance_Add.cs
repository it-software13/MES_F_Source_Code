using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_SystemFileMaintenance_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        
        public DataTable _dt { get; set; }
        public F_QCM_SystemFileMaintenance_Add(DataTable dt)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            _dt = dt;
        }
        public string txt_Type = "";
        public string file_url = "";
        public string file_name = "";
        private void textBox1_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件";
            ofd.Filter = "所有文件|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string SafeFileName = Path.GetFileName(ofd.FileName);
                string filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoad(Program.Client.APIURL, filePath, 11, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    file_url = resultDIC["url"].ToString();
                    file_name = resultDIC["filename"].ToString();
                    this.fileName.Enabled = false;
                    this.link_file_url.Text = Program.Client.PicUrl + file_url;
                    this.panel1.Visible = true;
                    MessageBox.Show("上传文件成功！");
                }
                else
                {

                    MessageBox.Show("上传文件失败！");
                }
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this.comboBox1.Text) || string.IsNullOrEmpty(this.link_file_url.Text))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("所有字段为必填项，请检查！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }

            DataRow dr = _dt.NewRow();
            dr["上传时间"] = DateTime.Now.ToString("yyyy-MM-dd");
            dr["有效文件"] = file_name;
            dr["文件类型"] = this.comboBox1.Text;
            _dt.Rows.Add(dr);
            this.Close();

        }

        private void link_delete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.link_file_url.Text = "";
            this.panel1.Visible = false;
            this.fileName.Enabled = true;
        }
    }
}
