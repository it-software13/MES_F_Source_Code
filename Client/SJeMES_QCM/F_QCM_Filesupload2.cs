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

namespace SJeMES_QCM
{
    public partial class F_QCM_Filesupload2 : MaterialForm
    {

        private readonly MaterialSkinManager materialSkinManager;
        private DataTable _dt = new DataTable();
        public F_QCM_Filesupload2()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            start_date.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            end_date.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            InitDateTimePicker(start_date);
            InitDateTimePicker(end_date);
            InitDateTimePicker(dateTimePicker1);
            InitDateTimePicker(dateTimePicker2);

            this.start_date.Format = DateTimePickerFormat.Custom;
            this.start_date.CustomFormat = "   ";

            this.end_date.Format = DateTimePickerFormat.Custom;
            this.end_date.CustomFormat = "   ";

            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "   ";

            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "   ";

        }
        private void F_QCM_Filesupload2_Load(object sender, EventArgs e)
        {

            GenClass.AutoSizeColumnStyle(dataGridView1);
           /* this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;*/
            pageControl1.BindPageEvent += GetDataList;
            
            dataGridView1.ClearSelection();
            //加载下拉框
            coblist();

            FormLoad();
        }
        public void FormLoad()
        {

            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
            
        }
        public void coblist()
        {
            try
            {
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.FilesuploadBase",//类名
                                        "CobList2",//方法名
                                        Program.Client.UserToken, "");
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                _dt = dt;
               
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.InsertAt(dr, 0);
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "ENUM_VALUE";
                    comboBox1.ValueMember = "ENUM_CODE";
                   
                }
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormLoad();
        }
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //if (DateTime.Parse(start_date.Value.ToString("yyyy-MM-dd")) == DateTime.Parse(end_date.Value.ToString("yyyy-MM-dd")))
                //    end_date.Value = DateTime.Parse(end_date.Value.ToString("yyyy-MM-dd")).AddDays(1);
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("file_type", comboBox1.SelectedValue);
                p.Add("txt_Art", txt_Art.Text);

                if (!string.IsNullOrWhiteSpace(start_date.Text.ToString()))
                {
                    p.Add("start_date", start_date.Value.ToString("yyyy-MM-dd 00:00:00"));
                }
                if (!string.IsNullOrWhiteSpace(end_date.Text.ToString()))
                {
                    p.Add("end_date", end_date.Value.ToString("yyyy-MM-dd 23:59:59"));
                }

                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    p.Add("start_dateyxq", dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    p.Add("end_dateyxq", dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59"));
                }

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.FilesuploadBase",//类名
                                            "GetMianList2",//方法名
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
                        dgvr.Cells["filetype_no"].Value = dr["filetype_no"].ToString();
                        dgvr.Cells["file_type"].Value = dr["CURR_FILE_TYPE"].ToString();
                        dgvr.Cells["prod_no"].Value = dr["PROD_NO"].ToString();
                        dgvr.Cells["curr_upload_time"].Value = dr["CURR_UPLOAD_TIME"].ToString();
                        dgvr.Cells["CURR_VALID_TIME"].Value = dr["CURR_VALID_TIME"].ToString();
                        i++;
                    }
                    // GenClass.AutoSizeColumn(dataGridView1);

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

        private void button1_Click(object sender, EventArgs e)
        {
            using (F_QCM_FilesuploadEdit2 frm = new F_QCM_FilesuploadEdit2(_dt))
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
                            string prod_no = dataGridView1.CurrentRow.Cells["prod_no"].Value.ToString();
                            DataTable dt = File_list(prod_no, dataGridView1.CurrentRow.Cells["filetype_no"].Value.ToString());
                            F_QCM_FilesuploadView add = new F_QCM_FilesuploadView(dt,"2");
                            add.ShowDialog();
                            FormLoad();
                        }
                        else if (cell.CurrentItem.Equals("delete"))//删除
                        {

                            if (MessageBox.Show("confirm deletion？", "This deletion is irreversible", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {

                                try
                                {

                                    string prod_no = dataGridView1.CurrentRow.Cells["prod_no"].Value.ToString();//ID
                                    Dictionary<string, object> p = new Dictionary<string, object>();

                                    //p.Add("OUTSOURCING_INSPECTION_NO", OUTSOURCING_INSPECTION_NO);
                                    p.Add("prod_no", prod_no);
                                    p.Add("filetype_no", dataGridView1.CurrentRow.Cells["filetype_no"].Value.ToString());
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_QCMAPI", "SJ_QCMAPI.FilesuploadBase", "Main_Delete2", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Deletion operation successful");
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
        public DataTable File_list(string prod_no,string file_type)
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("prod_no", prod_no);
                data.Add("file_type", file_type);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.FilesuploadBase",//类名
                                            "Main_ListFile2",//方法名
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

        private void btn_Select_Click(object sender, EventArgs e)
        {
            FormLoad();
        }
    }
}
