using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
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

namespace SJeMES_BDM
{
    public partial class F_BDM_NWiththemanagement : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics = new Dictionary<string, object>();
        public F_BDM_NWiththemanagement(Dictionary<string,object> dic)
        {
            InitializeComponent();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public List<imginfo> image_list = new List<imginfo>();
        public class imginfo
        {
            public string guid { get; set; }
            public string image_url { get; set; }
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
        public void FormLoad()
        {

            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        private void F_BDM_NWiththemanagement_Load(object sender, EventArgs e)
        {
            lab_needle_category_name.Text = dics["needle_category_name"].ToString();
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
        }
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {

            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("m_id", dics["id"].ToString());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Needlemanagement",//类名
                                            "BDM_Needlemanagement_View_dz",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["staff_name"].Value = dr["STAFF_NAME"].ToString();
                        dgvr.Cells["collar_qty"].Value = dr["COLLAR_QTY"].ToString();
                        dgvr.Cells["collar_date"].Value = dr["COLLAR_DATE"].ToString(); 
                        dgvr.Cells["remarks"].Value = dr["REMARKS"].ToString(); 
                        dgvr.Cells["id"].Value = dr["ID"].ToString();
                        i++;
                    }
                }
                //GenClass.AutoSizeColumn(dataGridView1);
                totalCount = int.Parse(dic["rowCount"].ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                string putin_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_date.Text))
                {
                    putin_date = Convert.ToDateTime(this.dateTimeP_date.Value).ToString("yyyy-MM-dd");
                }
                int qty = 0;
                int.TryParse(txt_qty.Text, out qty);
                if (qty < 1)
                {
                    MessageBox.Show("Please enter the quantity >=1");

                    return;
                }
                if (image_list.Count > 0)
                {
                    foreach (imginfo item in image_list)
                    {
                        if (!string.IsNullOrWhiteSpace(item.image_url))
                        {
                            UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, item.image_url, Program.Client.UserToken);
                            if (res.IsSuccess)
                            {
                                var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                item.guid = resultDIC["guid"].ToString();
                            }
                        }

                    }
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("id", dics["id"].ToString());
                p.Add("collar_qty", txt_qty.Text);
                p.Add("collar_date", putin_date);
                p.Add("opa_type", "2");//断针
                p.Add("guid_list", image_list);//照片guid集
                p.Add("remarks", textBoxEx1.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                           "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Needlemanagement",//类名
                                            "BDM_Needlemanagement_PDAadd",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    FormLoad();
                }
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            new Dictionary<string, object>();
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
                    string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("delete"))
                        {
                            if (MessageBox.Show("confirm deletion? ", "This delete cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Dictionary<string, object> p = new Dictionary<string, object>();
                                p.Add("id", dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                                p.Add("opa_type", "2");//0领用，1发针，2断针

                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                            Program.Client.APIURL,
                                                           "SJ_BDMAPI",//类库名
                                                            "SJ_BDMAPI.BDM_Needlemanagement",//类名
                                                            "BDM_Needlemanagement_PDAdelete",//方法名
                                                            Program.Client.UserToken,//token
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                if (!ret.IsSuccess)
                                {
                                    MessageBox.Show(ret.ErrMsg);
                                }
                                else
                                {
                                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                                    FormLoad();
                                }
                            }

                        }
                        else if (cell.CurrentItem.Equals("selectimg"))
                        {
                            string id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                            DataTable dt= Getimg(id);
                            using (FrmFileList aa = new FrmFileList(dt, Program.Client.UploadUrl, Program.Client.UserToken))
                            {
                                aa.ShowDialog();
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
        }
        private DataTable Getimg(string id)
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("ID", id);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Needlemanagement",//类名
                                            "BDM_Needlemanagement_getimg",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("net_file_url", typeof(string));
                    dt.Columns.Add("tablename", typeof(string));
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr["FILE_URL"].ToString()))
                        {
                            try
                            {

                                dr["net_file_url"] = Program.Client.PicUrl + dr["FILE_URL"].ToString();
                                dr["tablename"] = "qcm_car_needle_f";
                            }
                            catch
                            {

                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void btn_addimg_Click(object sender, EventArgs e)
        {
            try
            {
                //创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;//支持多张图片
                //判断选择的路径
                string path = string.Empty;
                ofd.Title = "Please select a folder";
                ofd.Filter = "image file(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < ofd.FileNames.Length; i++)
                    {
                        SafeFileName = System.IO.Path.GetFileName(ofd.FileNames[i].ToString());
                        filePath = ofd.FileNames[i].ToString();
                        image_list.Add(new imginfo
                        {
                            image_url = filePath,
                        });
                    }
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Pre-saved photos successfully！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                }
              

            }
            catch (Exception)
            {


            }
        }
    }
}
