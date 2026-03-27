using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_app_t_fileUpload : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Ex_app_t_fileUpload()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
       Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            InitDateTimePicker(start_date);
            InitDateTimePicker(endtime);
        }
        public void FormLoad()
        { 
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();

            //start_date.Format = DateTimePickerFormat.Custom;
            //start_date.CustomFormat = " ";
        }
        private void F_QCM_Ex_app_t_fileUpload_Load(object sender, EventArgs e)
        {
            #region 新增多选框列
            DataGridViewCheckBoxColumn checkbox = new DataGridViewCheckBoxColumn();
            //checkbox.HeaderText = "选择";
            checkbox.HeaderText = "Choose";
            checkbox.Name = "IsChecked";
            checkbox.TrueValue = true;
            checkbox.FalseValue = false;
            checkbox.DataPropertyName = "IsChecked";
            //列宽
            checkbox.Width = 100;
            //列大小不改变
            checkbox.Resizable = DataGridViewTriState.False;

            dataGridView1.Columns.Insert(0, checkbox);
            #endregion

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
                p.Add("FILE_NAME", txt_FILE_NAME.Text);
                if (!string.IsNullOrWhiteSpace(start_date.Text))
                    p.Add("start_date", start_date.Value.ToString("yyyy/MM/dd"));

                if (!string.IsNullOrWhiteSpace(endtime.Text))
                    p.Add("end_time", endtime.Value.ToString("yyyy/MM/dd"));

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
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
                        var currOpa = (DataGrid.DataGridViewCustomColumn.DataGridViewOperationItems)dgvr.Cells["operation"].Value;
                        if (dr["EFFECT"].ToString() != "1")
                        {
                            currOpa.RemoveAt(2);
                            dataGridView1.Rows[i].Cells["IsChecked"].Style.BackColor = Color.DarkGray;
                        }
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
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

        private void btn_uploadfile_Click(object sender, EventArgs e)
        {
            using (F_QCM_Ex_app_t_fileUpload_add frm = new F_QCM_Ex_app_t_fileUpload_add())
            {
                frm.ShowDialog();
                FormLoad();
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
                            F_QCM_Ex_app_t_fileUpload_view add = new F_QCM_Ex_app_t_fileUpload_view(dt);
                            add.ShowDialog();
                            FormLoad();
                        }
                        else if (cell.CurrentItem.Equals("delete"))//删除
                        {

                            if (MessageBox.Show("confirm deletion? ", "This delete cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    string file_name = dataGridView1.CurrentRow.Cells["file_name"].Value.ToString();
                                    Dictionary<string, object> p = new Dictionary<string, object>();
                                    p.Add("file_name", file_name);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_QCMAPI", "SJ_QCMAPI.ExShose", "Main_Delete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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
                        else if (cell.CurrentItem.Equals("update"))//生效
                        {

                            if (MessageBox.Show("Confirmed to take effect? ", "This effect cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    string currId = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                                    Dictionary<string, object> p = new Dictionary<string, object>();
                                    p.Add("id", currId);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_QCMAPI", "SJ_QCMAPI.ExShose", "Main_Update_EFFECT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        MessageBox.Show("The operation took effect successfully");
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
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
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

        private void btn_search_Click(object sender, EventArgs e)
        {
            FormLoad();
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
            dtp.CustomFormat = "yyyy/MM/dd"; //null;
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

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                var currOpa = (DataGrid.DataGridViewCustomColumn.DataGridViewOperationItems)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value;
                if (currOpa.Count == 2)
                    e.Cancel = true;
            }
        }

        private void btn_plsx_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirmed to take effect? ", "This effect cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<string> ids_list = new List<string>();
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    var curr_check = item.Cells["IsChecked"].Value;
                    if (Convert.ToBoolean(curr_check))
                    {
                        ids_list.Add(item.Cells["ID"].Value.ToString());
                    }
                }
                if (ids_list.Count > 0)
                {
                    try
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("ids", string.Join(",", ids_list));
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_QCMAPI", "SJ_QCMAPI.ExShose", "Main_Update_EFFECT_Batch", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            throw new Exception(ret.ErrMsg);
                        }
                        else
                        {
                            MessageBox.Show("The operation took effect successfully");
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

        private void cb_checkall_CheckedChanged(object sender, EventArgs e)
        {
            if(dataGridView1!=null && dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    var currOpa = (DataGrid.DataGridViewCustomColumn.DataGridViewOperationItems)item.Cells["operation"].Value;
                    if (currOpa.Count == 3)
                    {
                        item.Cells["IsChecked"].Value = cb_checkall.Checked;
                    }
                        
                }
            }
        }
    }
}
