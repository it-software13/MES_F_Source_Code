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

namespace SJeMES_IQC
{
    public partial class F_IQC_Customer_Complaint_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string po_num = "0";//po数量
        string imglist = string.Empty;//图片集
        string mid = string.Empty;
        public F_IQC_Customer_Complaint_Edit()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_IQC_Customer_Complaint_Edit(string _mid)
        {
            InitializeComponent();
            mid = _mid;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            textBox1.Enabled = false;
        }

        public void empty()
        {
            label14.Text = "";
            label15.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";
        }

        private void F_IQC_Customer_Complaint_Edit_Load(object sender, EventArgs e)
        {
            empty();
            GetCustomer_Complaint_Edit();
        }

        /// <summary>
        /// 客户投诉根据po查询数据
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetCustomer_Complaint_Edit_PO()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("PO",textBox3.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Customer_Complaint",//类名
                                            "GetCustomer_Complaint_Edit_PO",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count>0)
                {
                    label14.Text = dt.Rows[0]["DEVELOP_SEASON"].ToString();
                    label15.Text = dt.Rows[0]["PRODUCT_MONTH"].ToString();
                    label17.Text = dt.Rows[0]["Material_Way"].ToString();
                    label18.Text = dt.Rows[0]["prod_no"].ToString();
                    label19.Text = dt.Rows[0]["shoe_name"].ToString();
                    label20.Text = dt.Rows[0]["cx"].ToString();
                    po_num = dt.Rows[0]["SE_QTY"].ToString();
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("no data!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    textBox3.Text = "";
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 客户投诉编辑修改时查询
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetCustomer_Complaint_Edit()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("mid", mid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Customer_Complaint",//类名
                                            "GetCustomer_Complaint_Edit",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    textBox1.Text = dt.Rows[0]["COMPLAINT_NO"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["COMPLAINT_DATE"].ToString());
                    dateTimePicker1.Text = dt.Rows[0]["COMPLAINT_DATE"].ToString();
                    textBox3.Text = dt.Rows[0]["PO_ORDER"].ToString();
                    textBox4.Text = dt.Rows[0]["COUNTRY_REGION"].ToString();
                    textBox5.Text = dt.Rows[0]["NG_QTY"].ToString();
                    textBox6.Text = dt.Rows[0]["COMPLAINT_MONEY"].ToString();
                    richTextBox1.Text = dt.Rows[0]["DEFECT_CONTENT"].ToString();
                    imglist= dt.Rows[0]["imglist"].ToString();

                    label14.Text = dt.Rows[0]["DEVELOP_SEASON"].ToString();
                    label15.Text = dt.Rows[0]["PRODUCT_MONTH"].ToString();
                    label17.Text = dt.Rows[0]["Material_Way"].ToString();
                    label18.Text = dt.Rows[0]["prod_no"].ToString();
                    label19.Text = dt.Rows[0]["shoe_name"].ToString();
                    label20.Text = dt.Rows[0]["cx"].ToString();
                    tb_fob.Text = dt.Rows[0]["FOB"].ToString();
                    po_num = dt.Rows[0]["SE_QTY"].ToString();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetCustomer_Complaint_Edit_PO();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(po_num);
        }

        /// <summary>
        /// 客户投诉编辑页面查询图片
        /// </summary>
        /// <returns></returns>
        public DataTable Getimage_guid(string image_guid)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("image_guid", image_guid);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_Customer_Complaint",//类名
                                        "Getimage_guid",//方法名
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
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("net_file_url", typeof(string));
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["file_url"].ToString()))
                    {
                        try
                        {
                            dr["net_file_url"] = Program.Client.PicUrl + dr["file_url"].ToString();
                        }
                        catch
                        {
                        }
                    }
                    i++;
                }
            }
            return dt;
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
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in ofd.FileNames)
                {
                    SafeFileName = System.IO.Path.GetFileName(item);
                    filePath = item;
                    UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                    if (res.IsSuccess)
                    {
                        var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                        if (imglist != null && !string.IsNullOrEmpty(imglist))
                        {
                            imglist = imglist + "," + resultDIC["guid"].ToString();
                        }
                        else
                        {
                            imglist = resultDIC["guid"].ToString();
                        }
                        //string union_id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        //TQC_Task_Edit_Upload(union_id, resultDIC["guid"].ToString());
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("uploaded successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var currRowFileDt = Getimage_guid(imglist);
            FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.APIURL, Program.Client.UserToken, "", false);
            add.ShowDialog();
            int i = 0;
            string image_guids = string.Empty;
            foreach (DataRow item in currRowFileDt.Rows)
            {
                image_guids += item["guid"];
                if (i < currRowFileDt.Rows.Count - 1)
                {
                    image_guids += ",";
                }
                i++;
            }
            imglist = image_guids;

            //SJeMES_Control_Library.Forms.FrmImgList fil = new SJeMES_Control_Library.Forms.FrmImgList(Getimage_guid(dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString()), null, "");
            //fil.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 客户投诉编辑
        /// </summary>
        public void EditCustomer_Complaint_Edit()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("mid", mid);//条件 主表id
                data.Add("COMPLAINT_NO", textBox1.Text.Trim());//条件 投诉编号
                data.Add("COMPLAINT_DATE", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));//条件 投诉日期
                data.Add("COUNTRY_REGION", textBox4.Text.Trim());//条件 国家区域
                data.Add("PO_ORDER", textBox3.Text.Trim());//条件 投诉PO单号
                data.Add("NG_QTY", textBox5.Text.Trim());//条件 不良数量
                data.Add("COMPLAINT_MONEY", textBox6.Text.Trim());//条件 投诉金额
                data.Add("DEFECT_CONTENT", richTextBox1.Text.Trim());//条件 问题点
                data.Add("FOB", tb_fob.Text.Trim());//FOB
                data.Add("imglist", imglist);//条件 图片集
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_Customer_Complaint", "EditCustomer_Complaint_Edit", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                if (ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
                }
                else
                    throw new Exception(ret.ErrMsg.ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Complaint ID cannot be empty!");
                return;
            }
            //if (string.IsNullOrWhiteSpace(textBox3.Text))
            //{
            //    MessageBox.Show("PO不能为空!");
            //    return;
            //}
            if (!string.IsNullOrWhiteSpace(tb_fob.Text.Trim()))
            {
                decimal isNum = 0;
                if(!decimal.TryParse(tb_fob.Text.Trim(),out isNum))
                {
                    MessageBox.Show("FOB must be a number!");
                    return;
                }
            }
            EditCustomer_Complaint_Edit();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;  //非以上键则禁止输入
            }
            if (e.KeyChar == '0' && textBox5.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入0
            if (e.KeyChar == '.' && textBox5.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入小数点
            if (e.KeyChar == '.' && textBox5.Text.Contains(".")) e.Handled = true; //禁止输入多个小数点
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;  //非以上键则禁止输入
            }
            if (e.KeyChar == '0' && textBox6.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入0
            if (e.KeyChar == '.' && textBox6.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入小数点
            if (e.KeyChar == '.' && textBox6.Text.Contains(".")) e.Handled = true; //禁止输入多个小数点
        }

        private void tb_fob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;  //非以上键则禁止输入
            }
            if (e.KeyChar == '0' && textBox6.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入0
            if (e.KeyChar == '.' && textBox6.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入小数点
            if (e.KeyChar == '.' && textBox6.Text.Contains(".")) e.Handled = true; //禁止输入多个小数点
        }
    }
}
