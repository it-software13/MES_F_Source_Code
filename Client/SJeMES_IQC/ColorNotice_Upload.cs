using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;

namespace SJeMES_IQC
{
    public partial class ColorNotice_Upload : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string file_url = string.Empty;// 文件上传 返回的guid
        public string file_name = string.Empty;// 文件上传 返回的名称
        public string file_guid = string.Empty;// 文件上传 返回的guid 

        public ColorNotice_Upload()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
           // Get_ColorNotice();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            try
            {
                //创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;//支持多选
                string path = string.Empty;
                ofd.Title = "Please select a file";
                ofd.Filter = "image file(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < ofd.FileNames.Length; i++)
                    {
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, ofd.FileNames[i].ToString(), Program.Client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            file_url = resultDIC["url"].ToString();
                            file_name = ofd.SafeFileName.Substring(0, ofd.SafeFileName.LastIndexOf('.'));
                            //lbl_file_name.Text = file_name;
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("file_name", file_name);
                            p.Add("file_url", file_url);
                            string retdata = WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                        "SJeMES_IQC",//类库名
                                                        "SJeMES_IQC.IQC_ColorNotice",//类名
                                                        "ColorNoticeUpload",//方法名
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
                                //this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            Get_ColorNotice();
        }
  
        private void btn_Search_Click(object sender, EventArgs e)
        {
            LoadPage(); //Ashok
        }

        private void Get_ColorNotice()
        {
            string Mat_Code = txt_mcode.Text;
            string start_date=string.Empty;
            string end_date= string.Empty; 
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Mat_Code", Mat_Code);
            p.Add("start_date", start_date);
            p.Add("end_date", end_date);
            string retdata = WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_ColorNotice",//类名
                                        "Get_ColorNotice",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
            }
            else
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData); 
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["name"].Value = dr["file_name"].ToString();
                        dgvr.Cells["upload_time"].Value = dr["file_upload_time"].ToString();
                        dgvr.Cells["fileurl"].Value = dr["file_url"].ToString();
                        i++;
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("upload"))
                        {
                            string material_code = dataGridView1.CurrentRow.Cells["mcode"].Value.ToString();
                            try
                            {
                                OpenFileDialog ofd = new OpenFileDialog();
                                ofd.Multiselect = false;//支持多选
                                string path = string.Empty;
                                ofd.Title = "Please select a file";
                                ofd.Filter = "All files|*.*";

                                if (ofd.ShowDialog() == DialogResult.OK)
                                {
                                    for (int i = 0; i < ofd.FileNames.Length; i++)
                                    {
                                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, ofd.FileNames[i].ToString(), Program.Client.UserToken);
                                        if (res.IsSuccess)
                                        {
                                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                            file_guid = resultDIC["guid"].ToString();
                                            file_url = resultDIC["url"].ToString();
                                            file_name = ofd.SafeFileName.Substring(0, ofd.SafeFileName.LastIndexOf('.')) + "&" + DateTime.Now.ToString("yyyyMMdd");
                                            //lbl_file_name.Text = file_name;
                                        }
                                    }
                                    Dictionary<string, object> p = new Dictionary<string, object>();
                                    p.Add("file_name", file_name);
                                    p.Add("file_guid", file_guid);
                                    p.Add("file_url", file_url);
                                    p.Add("mcode", material_code);
                                    string retdata = WebAPIHelper.Post(
                                                                Program.Client.APIURL,
                                                                "SJeMES_IQC",//类库名
                                                                "SJeMES_IQC.IQC_ColorNotice",//类名
                                                                "ColorNoticeUpload",//方法名
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
                                        //this.Close();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                            }
                        }
                        else if (cell.CurrentItem.Equals("view"))
                        {
                            string material_code = dataGridView1.CurrentRow.Cells["mcode"].Value.ToString();
                            DataTable dt = File_list(material_code);
                            ColorNotice_Upload_View add = new ColorNotice_Upload_View(dt);
                            add.ShowDialog();
                            //FormLoad();


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

        private void ColorNotice_Upload_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.dataGridView1.ClearSelection();
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            pageControl1.BindPageEvent += Get_Material;
            LoadPage();
        }
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        private void Get_Material(int pageSize, int pageIndex, out int totalCount)
        {
            string Mat_Code = txt_mcode.Text;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Mat_Code", Mat_Code);
            p.Add("pageSize", pageSize);
            p.Add("pageIndex", pageIndex);
            string retdata = WebAPIHelper.Post(
                                       Program.Client.APIURL,
                                       "SJeMES_IQC",//类库名
                                       "SJeMES_IQC.IQC_ColorNotice",//类名
                                       "Get_Material_Details",//方法名
                                       Program.Client.UserToken,//token
                                       Newtonsoft.Json.JsonConvert.SerializeObject(p));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            } 
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData); 
            var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["mcode"].Value = dr["item_no"].ToString();
                        dgvr.Cells["mname"].Value = dr["name_t"].ToString(); 
                        i++;
                    }
                }
                
          
            totalCount = int.Parse(dic["rowCount"].ToString());
        }

        public DataTable File_list(string material_code)
        {
            DataTable dt = new DataTable();
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("material_code", material_code);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_ColorNotice",//类名
                                            "Get_ColorNotice_file",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    item["FILE_URL"] = Program.Client.PicUrl + item["FILE_URL"];
                }
            }
            return dt;
        }
    }
}
