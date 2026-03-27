using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SJeMES_AQL
{
    public partial class F_AQL_O_ORDER_Upload : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public F_AQL_O_ORDER_Upload()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            InitDateTimePicker(dateTimePicker1);
            InitDateTimePicker(dateTimePicker2);
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

        private void F_QCM_Filesupload_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
            dataGridView1.ClearSelection();
        }

        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("customer_no", tb_customer_no.Text);
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    p.Add("update_s", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    p.Add("update_e", dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd"));
                }
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_OOrderFlie",//类名
                                            "GetMainList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["file_name"].Value = dr["FILE_NAME"].ToString();
                        dgvr.Cells["curr_upload_time"].Value = dr["CURR_UPLOAD_TIME"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
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
                    string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("select"))//查看
                        {
                            string file_name = dataGridView1.CurrentRow.Cells["file_name"].Value.ToString();
                            DataTable dt = File_list(file_name);
                            F_AQL_OOrderFlie_Upload_View add = new F_AQL_OOrderFlie_Upload_View(dt);
                            add.ShowDialog();
                            FormLoad();
                        }
                        else if (cell.CurrentItem.Equals("delete"))//删除
                        {

                            if (MessageBox.Show("confirm deletion？", "This deletion cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    string file_name = dataGridView1.CurrentRow.Cells["file_name"].Value.ToString();
                                    Dictionary<string, object> p = new Dictionary<string, object>();
                                    p.Add("file_name", file_name);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_OOrderFlie", "Main_Delete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Operation deleted successfully");
                                        FormLoad();
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
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public DataTable File_list(string file_name)
        {
            DataTable dt = new DataTable();
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("file_name", file_name);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_OOrderFlie",//类名
                                            "Main_ListFile",//方法名
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

        private void btn_uploadfile_Click(object sender, EventArgs e)
        {
            using (F_AQL_OOrderFlie_Upload_Add frm = new F_AQL_OOrderFlie_Upload_Add())
            {
                frm.ShowDialog();
                FormLoad();
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void btn_uploadfile_multi_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog.SelectedPath.Trim() != "")
                {
                    string selectDicPath = folderBrowserDialog.SelectedPath;
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(selectDicPath);
                    System.IO.FileInfo[] files = directoryInfo.GetFiles();
                    if (files.Length > 0)
                    {
                        this.Enabled = false;
                        this.pb_loading.Visible = true;
                        int currCount = 0;
                        SetProgressBarCount(0, files.Length);
                        foreach (var file in files)
                        {
                            if (file.Extension.ToLower() == ".pdf")
                            {
                                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, file.FullName, Program.Client.UserToken);
                                if (res.IsSuccess)
                                {
                                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                    string file_guid = resultDIC["guid"].ToString();
                                    string file_name = file.Name.Replace(".pdf", "");
                                    CommitFile(file_name, file_guid);
                                }
                            }
                            currCount++;
                            System.Threading.Thread.Sleep(100);
                            StartProgressBar(currCount);
                        }
                        this.Enabled = true;
                        this.pb_loading.Visible = false;

                        FormLoad();
                    }
                }
            }
        }

        public bool CommitFile(string file_name,string file_guid)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("file_name", file_name);
                p.Add("file_guid", file_guid);
                string retdata = WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_OOrderFlie",//类名
                                            "Commit_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                return ret.IsSuccess;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        private void SetProgressBarCount(int minValue, int maxValue)
        {
            pb_loading.Minimum = minValue;
            pb_loading.Maximum = maxValue;
        }

        public void StartProgressBar(int value)
        {
            if (pb_loading == null) return;
            Application.DoEvents();
            pb_loading.Value = value;
            pb_loading.Refresh();
        }

    }
}
