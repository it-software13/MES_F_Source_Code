using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library;
using SJeMES_Control_Library.Forms;
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
    public partial class F_QCM_ATR_File1_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        F_QCM_ATR_File1 _pfrom;
        DataGridViewRow _dr;
        public F_QCM_ATR_File1_Edit(F_QCM_ATR_File1 pfrom,DataGridViewRow dr)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            _pfrom = pfrom;
            _dr = dr;
            BindData();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }


        public void BindData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("enum_type", "enum_file_type");
            data.Add("where", " and type='验货'");
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_QCMAPI", "SJ_QCMAPI.BASE", "GetSYS001MDataList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                var sourcedate = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                ddl_emun_file_type.DataSource = sourcedate;
                ddl_emun_file_type.DisplayMember = "enum_value";
                ddl_emun_file_type.ValueMember = "enum_code";
                ddl_emun_file_type.SelectedIndex = -1;
            }


            if(_dr!=null)
            {
                ddl_emun_file_type.Enabled = false;
                txt_art.Enabled = false;
                ddl_emun_file_type.SelectedValue= _dr.Cells["FILE_TYPE"].Value.ToString();
                ddl_emun_file_type.Text = _dr.Cells["文件类型"].Value.ToString();
                txt_art.Text = _dr.Cells["ART"].Value.ToString();
                dtp_time.Value = DateTime.Parse(_dr.Cells["有效日期"].Value.ToString());
                link_file_url.Text=Program.Client.PicUrl+ _dr.Cells["FILE_URL"].Value.ToString();
                file_url= _dr.Cells["FILE_URL"].Value.ToString();
                file_name= _dr.Cells["有效文件"].Value.ToString();
                this.btn_upload.Enabled = false;
                this.panel1.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string file_url = "";
        public string file_name = "";
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddl_emun_file_type.Text))
            {
                MessageBox.Show("请选择文件类型");
                return;
            }
            if (string.IsNullOrEmpty(txt_art.Text.Trim()))
            {
                MessageBox.Show("请选择ART");
                return;
            }
            if (DateTime.Parse(dtp_time.Value.ToString("yyyy-MM-dd")) <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                MessageBox.Show("有效时间必须大于当前日期");
                return;
            }
            if (string.IsNullOrEmpty(file_url))
            {
                MessageBox.Show("请上传文件");
                return;
            }

            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ART", txt_art.Text.Trim());
            p.Add("FILE_TYPE", ddl_emun_file_type.SelectedValue);
            p.Add("FILE_TYPE_TEXT", ddl_emun_file_type.Text);
            p.Add("TYPE", "验货");
            p.Add("TYPE1", "");
            p.Add("FILE_NAME", file_name);
            p.Add("FILE_URL", file_url);
            p.Add("EFFECTIVE_DATE", dtp_time.Value.ToString("yyyy-MM-dd"));
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ARTFileBind",//类名
                                        "AddFile",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (ret.IsSuccess)
            {

                _pfrom.FormLoad();
                MessageBox.Show("保存成功");
                this.Close();
            }

        }

        private void btn_upload_Click(object sender, EventArgs e)
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
                    this.btn_upload.Enabled = false;
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

        private void link_delete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (SJeMES_Framework.Common.HttpHelper.DeleteFile(Program.Client.APIURL, link_file_url.Text, 11, Program.Client.UserToken))
            {
                this.btn_upload.Enabled = true;
                file_url = "";
                file_name = "";
                this.link_file_url.Text = "";
                this.panel1.Visible = false;
            }
            else
            {
                MessageBox.Show("文件移除失败！");
            }

        }

        private void link_file_url_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string file_url = Convert.ToString(link_file_url.Text);
            ShowFileHelper.ShowFile(file_url);
        }

        private void txt_art_Click(object sender, EventArgs e)
        {
            string sql = "select PROD_NO AS ART from bdm_rd_prod";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client, "R");
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_art.Text = frmData.RetData.Rows[0]["ART"].ToString();
            }
        }
    }



}
