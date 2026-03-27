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
    public partial class F_QCM_BdmoutqualitylistmSelect : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_BdmoutqualitylistmSelect()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
    Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(dateTimeP_CREATEDATE);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_BdmoutqualitylistmSelect_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.dateTimeP_CREATEDATE.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_CREATEDATE.CustomFormat = " ";
            GetDataList();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }
        public void GetDataList()
        {
            try
            {
                string CREATEDATE = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_CREATEDATE.Text))
                {
                    CREATEDATE = Convert.ToDateTime(this.dateTimeP_CREATEDATE.Value).ToString("yyyy-MM-dd");
                }
            
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("SUPPLIERS_NAME", txt_SUPPLIERS_NAME.Text.Trim().ToString());
                //dateTimeP_CREATEDATE.Text.Trim().ToString()
                p.Add("CREATEDATE", CREATEDATE);
                
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.OutQuantityStandard",//类名
                                            "GetAllProjectListLog",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,object>>(retdata);
                if (string.IsNullOrEmpty(ret["IsSuccess"].ToString()))
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }
               
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret["RetData1"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        //dgvr.Cells["SUPPLIERS_CODE"].Value = dr["SUPPLIERS_CODE"].ToString();
                        dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();
                        dgvr.Cells["REAL_SCORE"].Value = dr["REAL_SCORE"].ToString();
                        dgvr.Cells["GUID"].Value = dr["GUID"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
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
        private void btn_Select_Click(object sender, EventArgs e)
        {
            GetDataList();
            this.dateTimeP_CREATEDATE.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_CREATEDATE.CustomFormat = " ";
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

                        if (cell.CurrentItem.Equals("DETAIL"))//查看
                        {
                            string GUID = dataGridView1.CurrentRow.Cells["GUID"].Value.ToString();
                            F_QCM_BdmoutqualitylistmSelect_List add = new F_QCM_BdmoutqualitylistmSelect_List(GUID);
                            add.ShowDialog();

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
    }
}
