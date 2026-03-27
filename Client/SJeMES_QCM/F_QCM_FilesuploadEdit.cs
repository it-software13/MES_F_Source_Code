using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
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
    public partial class F_QCM_FilesuploadEdit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public List<string> file_list = new List<string>();
        private DataTable dt = new DataTable();
        public F_QCM_FilesuploadEdit(DataTable _dt)
        {
            InitializeComponent();
            dt = _dt;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            InitDateTimePicker(dateTimeP_putin_date);
        }
        private void F_QCM_FilesuploadEdit_Load(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dt.Rows[i]["FILE_TYPE"]);
                }
                comboBox1.SelectedIndex = -1;
            }
        }
        #region 日期控件初始为空值处理

        /// <summary>
        /// 初始化日期时间控件
        /// </summary>
        /// <param name="dtp"></param>
        public static void InitDateTimePicker(DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = " ";  //必须设置成" "
            dtp.ValueChanged -= DateTimePicker_ValueChanged;
            dtp.ValueChanged += DateTimePicker_ValueChanged;
            dtp.KeyPress -= DateTimePicker_KeyPress;
            dtp.KeyPress += DateTimePicker_KeyPress;
        }

        public static void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "yyyy-MM-dd"; //null;
            dtp.Checked = false;// 解决BUG ：防止日期控件不能选择相同日期的 --- 要放置在设置格式之后
        }

        public static void DateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)  // backspace左删除键
            {
                DateTimePicker dtp = (DateTimePicker)sender;
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = " ";
            }
        }
        #endregion

        private void textBox1_Click(object sender, EventArgs e)
        {

            string sql = $@"
SELECT * FROM (
SELECT
	DISTINCT
rownum rn,
	l.name_t as 鞋型, --鞋型
	r.prod_no as ART--art
FROM
 bdm_rd_prod r 
LEFT JOIN BDM_RD_STYLE l ON r.SHOE_NO = l.SHOE_NO
)tab
where 1=1 
";
            using (F_QCM_FilesuploadSelectArt frmData = new F_QCM_FilesuploadSelectArt(textBox1.Text, sql))
            {
                frmData.ShowDialog();

                if (frmData.selectlist.Count > 0)
                {
                    string art = string.Empty;
                    foreach (var item in frmData.selectlist)
                    {
                        art += item["ART"].ToString() + ",";
                    }
                    textBox1.Text = art.TrimEnd(',');
                }
            };

            //            string sql = $@"SELECT
            //	DISTINCT
            //	l.name_t as 鞋型, --鞋型
            //	r.prod_no as ART--art
            //FROM
            // bdm_rd_prod r 
            //LEFT JOIN BDM_RD_STYLE l ON r.SHOE_NO = l.SHOE_NO
            //";
            //            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            //            frmData.ShowDialog();
            //            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            //            {
            //                textBox1.Text = frmData.RetData.Rows[0]["ART"].ToString();

            //            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;//支持多张图片
                                       //判断选择的路径
                string path = string.Empty;
                ofd.Title = "请选择文件";
                ofd.Filter = "所有文件|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    file_list = new List<string>();
                    int count = 0;
                    for (int i = 0; i < ofd.FileNames.Length; i++)
                    {
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, ofd.FileNames[i].ToString(), Program.Client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            file_list.Add(resultDIC["guid"].ToString());
                        }
                        count++;

                    }
                    label5.Text = count.ToString();


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
            try
            {
                string putin_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
                {
                    putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
                }
                if (string.IsNullOrWhiteSpace(comboBox1.Text) || 
                    string.IsNullOrWhiteSpace(putin_date) || 
                    string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("必填项不能为空");
                    return;
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("prod_no", textBox1.Text);
                p.Add("putin_date", putin_date);
                p.Add("curr_file_type", comboBox1.Text);
                p.Add("file_list", file_list);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.FilesuploadBase",//类名
                                            "Commit_Mian",//方法名
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
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
