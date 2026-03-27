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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_CUSTOMER_COMPLAINT_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_CUSTOMER_COMPLAINT_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(SPOTCHECK_DATE_START);
            InitDateTimePicker(SPOTCHECK_DATE_END);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }


        public void GetData(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string SPOTCHECK_DATE_START = string.Empty;
                string SPOTCHECK_DATE_END = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.SPOTCHECK_DATE_START.Text))
                {
                    SPOTCHECK_DATE_START = Convert.ToDateTime(this.SPOTCHECK_DATE_START.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.SPOTCHECK_DATE_END.Text))
                {
                    SPOTCHECK_DATE_END = Convert.ToDateTime(this.SPOTCHECK_DATE_END.Value).ToString("yyyy-MM-dd");
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("PROD_NO", this.txt_ART.Text);
                data.Add("SHOE_NO", this.txt_shoes.Text);
                data.Add("SPOTCHECK_DATE_START", SPOTCHECK_DATE_START);
                data.Add("SPOTCHECK_DATE_END", SPOTCHECK_DATE_END);

                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.CustomerComplaint", "GetCustomerComplaintList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    dataGridView1.Rows.Clear();
                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["COMPLAINT_NO"].Value = dr["COMPLAINT_NO"].ToString();
                            dgvr.Cells["COMPLAINT_DATE"].Value = dr["COMPLAINT_DATE"].ToString();
                            dgvr.Cells["COUNTRY_REGION"].Value = dr["COUNTRY_REGION"].ToString();

                            dgvr.Cells["PO_ORDER"].Value = dr["PO_ORDER"].ToString();
                            dgvr.Cells["DEVELOP_SEASON"].Value = dr["DEVELOP_SEASON"].ToString();
                            dgvr.Cells["CATEGORY"].Value = dr["CATEGORY"].ToString();
                            dgvr.Cells["DEVELOPMENT_COURSE"].Value = dr["DEVELOPMENT_COURSE"].ToString();

                            dgvr.Cells["PRODUCT_MONTH"].Value = dr["PRODUCT_MONTH"].ToString();
                            dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
                            dgvr.Cells["SHOE_NO"].Value = dr["SHOE_NO"].ToString();

                            dgvr.Cells["MATERIAL_WAY"].Value = dr["MATERIAL_WAY"].ToString();
                            dgvr.Cells["PRODUCTIONLINE_NO"].Value = dr["PRODUCTIONLINE_NO"].ToString();
                            dgvr.Cells["PRODUCTIONLINE_NAME"].Value = dr["PRODUCTIONLINE_NAME"].ToString();

                            dgvr.Cells["NG_QTY"].Value = dr["NG_QTY"].ToString();
                            dgvr.Cells["COMPLAINT_MONEY"].Value = dr["COMPLAINT_MONEY"].ToString();
                            dgvr.Cells["DEFECT_CONTENT"].Value = dr["DEFECT_CONTENT"].ToString();
                            i++;
                        }
                    }
                    totalCount = int.Parse(dic["rowCount"].ToString());
                    dataGridView1.ClearSelection(); 
                    this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;

                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
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

        private void F_QCM_CUSTOMER_COMPLAINT_Main_Click(object sender, EventArgs e)
        {

        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        public void F_QCM_CUSTOMER_COMPLAINT_Main_Load(object sender, EventArgs e)
        {
            this.SPOTCHECK_DATE_START.Format = DateTimePickerFormat.Custom;
            this.SPOTCHECK_DATE_START.CustomFormat = " ";

            this.SPOTCHECK_DATE_END.Format = DateTimePickerFormat.Custom;
            this.SPOTCHECK_DATE_END.CustomFormat = " ";

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //只要加载一次委托 
            pageControl1.BindPageEvent += GetData;
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            
            F_QCM_CUSTOMER_COMPLAINT_Add add = new F_QCM_CUSTOMER_COMPLAINT_Add(this);
            add.ShowDialog();

        }

        public string SafeFileName { get; set; }
        public string filePath { get; set; }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    string COMPLAINT_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["COMPLAINT_NO"].Value);//单号
                    string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                        if (cell.CurrentItem == null)
                            return;
                        if (cell.CurrentItem.Equals("selectImg")) // 图片查看
                        {
                            //查询dt
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("COMPLAINT_NO", COMPLAINT_NO);//检验单号
                                                                   //data.Add("TESTITEM_CODE", TESTITEM_CODE);//检测项编号

                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                 "SJ_QCMAPI", "SJ_QCMAPI.CustomerComplaint", "GetPhotoImgList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                            if (!ret.IsSuccess)
                                throw new Exception(ret.ErrMsg);
                            else
                            {
                                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                                foreach (DataRow item in dt.Rows)
                                {
                                    item["IMG_URL"] = Program.Client.PicUrl + item["IMG_URL"].ToString();
                                }

                                FrmImgList frmImgList = new FrmImgList(dt, null, "1");
                                frmImgList.ShowDialog();
                            }

                        }
                        else if (cell.CurrentItem.Equals("selectfile"))//FrmFileList 文件查看
                        {

                            FrmFileList add = new FrmFileList(FileView(COMPLAINT_NO), Program.Client.UploadUrl, Program.Client.UserToken);
                            add.ShowDialog();

                        }
                        else if (cell.CurrentItem.Equals("selectinfo"))//查看详情
                        {

                            F_QCM_CUSTOMER_COMPLAINT_Detail frm = new F_QCM_CUSTOMER_COMPLAINT_Detail(COMPLAINT_NO);
                            frm.ShowDialog();

                        }
                        else if (cell.CurrentItem.Equals("uploadimg"))
                        {
                            string guid = Guid.NewGuid().ToString("N");
                            //创建文件弹出选择窗口（包括文件名）对象
                            OpenFileDialog ofd = new OpenFileDialog();
                            //判断选择的路径
                            string path = string.Empty;
                            ofd.Title = "请选择文件夹";
                            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
                            if (ofd.ShowDialog() == DialogResult.OK)
                            {
                                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                                filePath = ofd.FileName;


                                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoad(Program.Client.APIURL, filePath, 16, Program.Client.UserToken);
                                if (res.IsSuccess)
                                {
                                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());


                                    //保存图片信息QCM_CUSTOMER_COMPLAINT_FILE 
                                    Dictionary<string, object> data = new Dictionary<string, object>();
                                    data.Add("COMPLAINT_NO", COMPLAINT_NO);//检验单号
                                    data.Add("IMG_NAME", resultDIC["filename"].ToString());//图片名称
                                    data.Add("IMG_URL", resultDIC["url"].ToString());//图片路径
                                    data.Add("GUID", guid);//guid
                                    data.Add("TYPE", "1");//图片


                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_QCMAPI", "SJ_QCMAPI.CustomerComplaint", "SavePhotoImgList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                                    if (!ret.IsSuccess)
                                        throw new Exception(ret.ErrMsg);
                                    else
                                    {
                                        MessageBox.Show("上传图片成功！");
                                    }

                                }
                                else
                                {

                                    MessageBox.Show("上传图片失败！");
                                }

                            }
                        }
                        else if (cell.CurrentItem.Equals("uploadfile"))//查看文件
                        {
                            //UploadAll(COMPLAINT_NO);

                            try
                            {
                                string guid = Guid.NewGuid().ToString("N");
                                // 创建文件弹出选择窗口（包括文件名）对象
                                OpenFileDialog ofd = new OpenFileDialog();
                                //判断选择的路径
                                string path = string.Empty;
                                ofd.Title = "请选择文件";
                                ofd.Filter = "所有文件|*.*";
                                if (ofd.ShowDialog() == DialogResult.OK)
                                {
                                    SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                                    filePath = ofd.FileName;


                                    UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoad(Program.Client.APIURL, filePath, 17, Program.Client.UserToken);
                                    if (res.IsSuccess)
                                    {
                                        var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());


                                        //保存图片信息QCM_CUSTOMER_COMPLAINT_FILE 
                                        Dictionary<string, object> data = new Dictionary<string, object>();
                                        data.Add("COMPLAINT_NO", COMPLAINT_NO);//检验单号
                                        data.Add("IMG_NAME", resultDIC["filename"].ToString());//图片名称
                                        data.Add("IMG_URL", resultDIC["url"].ToString());//文件路径
                                        data.Add("GUID", guid);//guid
                                        data.Add("TYPE", "2");//文件


                                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                             "SJ_QCMAPI", "SJ_QCMAPI.CustomerComplaint", "SavePhotoImgList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                                        if (!ret.IsSuccess)
                                            throw new Exception(ret.ErrMsg);
                                        else
                                        {
                                            MessageBox.Show("上传文件成功！");
                                        }

                                    }
                                    else
                                    {

                                        MessageBox.Show("上传文件失败！");
                                    }

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
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

       
        /// <summary>
        /// 文件的dt视图;
        /// </summary>
        public static DataTable FileView(string COMPLAINT_NO)
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("tablename", "QCM_CUSTOMER_COMPLAINT_FILE");

            Dictionary<string, object> fileddic = new Dictionary<string, object>();
            fileddic.Add("IMG_NAME", "file_name");
            fileddic.Add("IMG_URL", "file_url");
            p.Add("fileds", fileddic);

            Dictionary<string, object> parmsdic = new Dictionary<string, object>();
            parmsdic.Add("COMPLAINT_NO", COMPLAINT_NO);
            parmsdic.Add("TYPE", "2");
            p.Add("parms", parmsdic);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.BASE",//类名
                                        "GetFileView",//方法名
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

        

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //下载导入模板
        private void Modelbtn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("投诉编号");
                dt.Columns.Add("投诉日期");
                dt.Columns.Add("国家区域");
                dt.Columns.Add("PO");
                dt.Columns.Add("开发季度");
                dt.Columns.Add("CATEGORY");
                dt.Columns.Add("开发课");
                dt.Columns.Add("量产月份");
                dt.Columns.Add("ART");
                dt.Columns.Add("鞋型");
                dt.Columns.Add("MATERIAL_WAY");
                dt.Columns.Add("产线代号");
                dt.Columns.Add("产线名称");
                dt.Columns.Add("不良数量");
                dt.Columns.Add("投诉金额");
                dt.Columns.Add("问题点");
                FolderBrowserDialog ofd = new FolderBrowserDialog();
                ofd.ShowDialog();
                string path = ofd.SelectedPath;
                SJeMES_Framework.Common.NPOIHelper.TableToExcel(dt, path + @"\" + $"客户投诉导入模板{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx");
                MessageBox.Show("下载成功");
            }
            catch
            {
                MessageBox.Show("下载失败");
            }
        }

        //导入文件
        private void Importbtn_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件";
            ofd.Filter = "EXECL|*.xlsx;*.xls";
            string SafeFileName = "";
            string filePath = "";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = Path.GetExtension(ofd.FileName);
                filePath = ofd.FileName;
            }
            if (!string.IsNullOrEmpty(filePath))
            {
                if (SafeFileName != ".xlsx" && SafeFileName != ".xls")
                {
                    MessageBox.Show("文件类型错误,请选择(.xlsx,.xls)类型文件");
                    return;
                }
                DataTable dt = SJeMES_Framework.Common.NPOIHelper.ExcelToTable(filePath);
                if (dt.Columns.Count != 16 || dt.Columns[0].ColumnName != "投诉编号")
                {
                    MessageBox.Show("导入模板错误,请查阅");
                    return;
                }
                if (dt != null)
                {
                    SJeMES_Control_Library.Forms.FrmImport frm = new SJeMES_Control_Library.Forms.FrmImport(dt);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                    bool is_sure = frm.is_sure;
                    if (is_sure)
                    {
                        //请求api的数据展示
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("SOURCE", dt);
                        p.Add("import_type", 2);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.BASE",//类名
                                                    "ImportData",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (ret.IsSuccess)
                        {
                            MessageBox.Show("导入成功");
                            this.F_QCM_CUSTOMER_COMPLAINT_Main_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                    }
                }
            }
        }
    }

}

