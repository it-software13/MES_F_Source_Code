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
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;

namespace SJeMES_TSM
{
    public partial class APC_SupplementaryData : MaterialForm
    { 
        private readonly MaterialSkinManager materialSkinManager;
        private object pb_loading;

        public APC_SupplementaryData()
        {
            InitializeComponent();
        }

        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }



        private void Button1_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using (File_Upload frm = new File_Upload())
            {
                frm.ShowDialog();
                FormLoad();
            }
        }

        private void APC_SupplementaryData2_Load(object sender, EventArgs e)
        {
            //GenClass.AutoSizeColumnStyle(dataGridView1);
            pageControl1.BindPageEvent += GetDataList;
            //FormLoad();
            //dataGridView1.ClearSelection();
        }
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Filename", textBox1.Text);
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
                                            "SJ_TSMAPI",//类库名
                                            "SJ_TSMAPI.APC_Supplementary_Data",//类名
                                            "GetMainList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                { 
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
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
                                            "SJ_TSMAPI",//类库名
                                            "SJ_TSMAPI.APC_Supplementary_Data",//类名
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

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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
                            Supplementary_Data add = new Supplementary_Data(dt);
                            add.ShowDialog();
                            FormLoad();
                        }
                        else if (cell.CurrentItem.Equals("delete"))//删除
                        {

                            if (MessageBox.Show("confirm deletion ?", "This deletion cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    string file_name = dataGridView1.CurrentRow.Cells["file_name"].Value.ToString();
                                    Dictionary<string, object> p = new Dictionary<string, object>();
                                    p.Add("file_name", file_name);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_TSMAPI", "SJ_TSMAPI.APC_Supplementary_Data", "Main_Delete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    { 
                                        MessageBox.Show(ret.ErrMsg);
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

        private void Button3_Click(object sender, EventArgs e)
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
                        this.pb_loading1.Visible = true;
                        int currCount = 0;
                        SetProgressBarCount(0, files.Length);
                        foreach (var file in files)
                        {
                            if (file.Extension.ToLower() == ".txt" || file.Extension.ToLower() == ".xlsx" || file.Extension.ToLower() == ".pdf" || file.Extension.ToLower() == ".pptx" || file.Extension.ToLower() == ".docx")
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
                        this.pb_loading1.Visible = false;

                        FormLoad();
                    }
                }
            }
        }
        public bool CommitFile(string file_name, string file_guid)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("file_name", file_name);
                p.Add("file_guid", file_guid);
                string retdata = WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",//类库名
                                            "SJ_TSMAPI.APC_Supplementary_Data",//类名
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
            pb_loading1.Minimum = minValue;
            pb_loading1.Maximum = maxValue;
        }

        public void StartProgressBar(int value)
        {
            if (pb_loading1 == null) return;
            Application.DoEvents();
            pb_loading1.Value = value;
            pb_loading1.Refresh();
        }

    }
}
