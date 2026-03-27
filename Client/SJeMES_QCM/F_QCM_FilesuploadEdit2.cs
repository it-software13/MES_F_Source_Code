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
    public partial class F_QCM_FilesuploadEdit2 : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public List<string> file_list = new List<string>();
        private DataTable dt = new DataTable();
        public F_QCM_FilesuploadEdit2(DataTable _dt)
        {
            InitializeComponent();
            dt = _dt;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            InitDateTimePicker(dateTimePicker1);


        }
        private void F_QCM_FilesuploadEdit2_Load(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "ENUM_VALUE";
                comboBox1.ValueMember = "ENUM_CODE";


            }
        }
        private void textBox1_Click(object sender, EventArgs e)
        {

            string sql = $@"
SELECT * FROM (
SELECT
	DISTINCT
rownum rn,
	l.name_t as Shoe_type, --鞋型
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
               

            
            //FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            //frmData.ShowDialog();
            //if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            //{
            //    textBox1.Text = frmData.RetData.Rows[0]["ART"].ToString();

            //}
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
                if (string.IsNullOrWhiteSpace(comboBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox1.Text)|| 
                    string.IsNullOrWhiteSpace(dateTimePicker1.Text))
                {
                    MessageBox.Show("Required fields cannot be empty");
                    return;
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("prod_no", textBox1.Text);
                p.Add("curr_file_type", comboBox1.SelectedValue);
                p.Add("file_list", file_list);
                p.Add("curr_valid_time", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.FilesuploadBase",//类名
                                            "Commit_Mian2",//方法名
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

    }
}
