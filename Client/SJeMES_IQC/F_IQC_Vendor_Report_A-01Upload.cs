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

namespace SJeMES_IQC
{
    public partial class F_IQC_Vendor_Report_A_01Upload : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string vend_no = string.Empty;
        string order_no = string.Empty;
        string item_no = string.Empty;
        public F_IQC_Vendor_Report_A_01Upload()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_IQC_Vendor_Report_A_01Upload(string _vend_no,string _order_no,string _item_no)
        {
            item_no = _item_no;
            vend_no = _vend_no;
            order_no = _order_no;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
           
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        string file_id = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            // string res = UpLoad("3", file_type);
            string guid = Guid.NewGuid().ToString("N");
            // 创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "Please select a file";
            ofd.Filter = "All files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;


                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    file_id = resultDIC["guid"].ToString();
                }
                else
                {

                    MessageBox.Show("Failed to upload file！");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()))
            {
                MessageBox.Show("Report ID cannot be empty!");
                return;
            }
            Vendor_Report_Main_EditA01();
        }

        /// <summary>
        /// T2 Vendor Upload Home Page Upload Operation A01 Report
        /// </summary>
        public void Vendor_Report_Main_EditA01()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                if (string.IsNullOrEmpty(file_id))
                {
                    MessageBox.Show("Please upload the file first!");
                    return;
                }
                data.Add("vend_no", vend_no);
                data.Add("order_no", order_no);
                data.Add("item_no", item_no);
                data.Add("report_type", "1");
                data.Add("start_date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                data.Add("file_id", file_id);
                data.Add("report_no", textBox1.Text.Trim());//报告编号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_Vendor_Report", "Vendor_Report_Main_EditA01", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
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
    }
}
