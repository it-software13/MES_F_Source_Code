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

namespace SJeMES_IQC
{
    public partial class F_IQC_VWarehouse_MJ : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_IQC_VWarehouse_MJ()
        {
            InitializeComponent();
            InitDateTimePicker(dateTimeP_putin_date);
            InitDateTimePicker(dateTimeP_end_date);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
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
        private void F_IQC_VWarehouse_MJ_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
           /* this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;*/
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
            dataGridView1.ClearSelection();
        }
        private  string ORG_IDS = "";
        private string WAREHOUSE_CODE = "";
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(txt_gc.Text))
                {
                    ORG_IDS = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(txt_ck.Text))
                {
                    WAREHOUSE_CODE = string.Empty;
                }
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                p.Add("SUPPLIERS_CODE", txt_SUPPLIERS_CODE.Text);
                p.Add("SUPPLIERS_NAME", txt_SUPPLIERS_NAME.Text);
                p.Add("ORG_ID", ORG_IDS);//工厂代号
                p.Add("STOC_NO", WAREHOUSE_CODE);//仓库代号

                string putin_date = "";
                string end_date = "";
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
                {
                    putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.dateTimeP_end_date.Value).ToString("yyyy-MM-dd");
                }
                p.Add("putin_date", putin_date);
                p.Add("end_date", end_date);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.VMaterialinventory",//类名
                                            "CheckResultMJView",//方法名
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
                        dgvr.Cells["SUPPLIERS_CODE"].Value = dr["SUPPLIERS_CODE"].ToString();
                        dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();
                        dgvr.Cells["DJ"].Value = dr["DJ"].ToString();
                        dgvr.Cells["JC"].Value = dr["JC"].ToString();
                        dgvr.Cells["STOC_NO"].Value = dr["STOC_NO"].ToString();//仓库代号
                        dgvr.Cells["WAREHOUSE_NAME"].Value = dr["WAREHOUSE_NAME"].ToString();//仓库
                        dgvr.Cells["ORG_ID"].Value = dr["ORG_ID"].ToString();//工厂编号
                        dgvr.Cells["ORG_NAME"].Value = dr["ORG_NAME"].ToString();//工厂

                        i++;
                    }
                    //GenClass.AutoSizeColumn(dataGridView1);

                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                if (!string.IsNullOrWhiteSpace(dic["org_name"].ToString()))
                {
                    txt_gc.Text = dic["org_name"].ToString();
                }
                this.dataGridView1.ClearSelection();

                //this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            FormLoad();
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
                    string name = dataGridView1.Columns[e.ColumnIndex].Name;
                    if (name=="btn")
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("SUPPLIERS_CODE", dataGridView1.CurrentRow.Cells["SUPPLIERS_CODE"].Value.ToString());
                        dic.Add("ORG_ID", dataGridView1.CurrentRow.Cells["ORG_ID"].Value.ToString());//工厂代号
                        dic.Add("ORG_NAME", dataGridView1.CurrentRow.Cells["ORG_NAME"].Value.ToString());//工厂名称
                        dic.Add("STOC_NO", dataGridView1.CurrentRow.Cells["STOC_NO"].Value.ToString());//仓库代号
                        dic.Add("WAREHOUSE_NAME", dataGridView1.CurrentRow.Cells["WAREHOUSE_NAME"].Value.ToString());//仓库名称
                        dic.Add("SUPPLIERS_NAME", dataGridView1.CurrentRow.Cells["SUPPLIERS_NAME"].Value.ToString());

                        string putin_date = "";
                        string end_date = "";
                        if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
                        {
                            putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
                        }
                        if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date.Text))
                        {
                            end_date = Convert.ToDateTime(this.dateTimeP_end_date.Value).ToString("yyyy-MM-dd");
                        }
                        dic.Add("putin_date", putin_date);
                        dic.Add("end_date", end_date);

                        if (!string.IsNullOrWhiteSpace(dic["SUPPLIERS_CODE"].ToString()))
                        {
                            using (F_IQC_VWarehouse_MJQC aa = new F_IQC_VWarehouse_MJQC(dic))
                            {
                                aa.ShowDialog();
                                FormLoad();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Missing prerequisites, please check");
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    string SUPPLIERS_CODE_LIST = string.Empty;
                    List<Dictionary<string, object>> SUPPLIERS_CODE_List = new List<Dictionary<string, object>>();
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        //检测项目
                        string cc = dgr.Cells["Column1"].EditedFormattedValue.ToString();
                        if (cc == "True")
                        {
                            dic.Add("SUPPLIERS_CODE","");
                            dic.Add("ORG_ID", "");
                            dic.Add("STOC_NO", "");
                            dic.Add("putin_date", "");
                            dic.Add("end_date", "");
                            if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
                            {
                                dic["putin_date"] = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
                            }
                            if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date.Text))
                            {
                                dic["end_date"] = Convert.ToDateTime(this.dateTimeP_end_date.Value).ToString("yyyy-MM-dd");
                            }
                            if (dgr.Cells["SUPPLIERS_CODE"].Value != null)
                            {
                                dic["SUPPLIERS_CODE"]= dgr.Cells["SUPPLIERS_CODE"].Value.ToString();
                            }
                            if(dgr.Cells["ORG_ID"].Value != null)
                            {
                                dic["ORG_ID"] = dgr.Cells["ORG_ID"].Value.ToString();
                            }
                            if(dgr.Cells["STOC_NO"].Value != null)
                            {
                                dic["STOC_NO"] = dgr.Cells["STOC_NO"].Value.ToString();
                            }
                            SUPPLIERS_CODE_List.Add(dic);
                        }
                       
                    }
                  
                  
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                               "SJeMES_IQC",//类库名
                                                "SJeMES_IQC.VMaterialinventory",//类名
                                                "CheckResultMJUpdate",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(SUPPLIERS_CODE_List));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        MessageBox.Show(ret.ErrMsg);
                    }
                    else
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("The operation is exempted from inspection successfully！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        FormLoad();
                    }
                }
                else
                {

                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please select first, and then do the inspection-free operation！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    FormLoad();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_gx_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                {
                    dgr.Cells["Column1"].Value = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                {
                    dgr.Cells["Column1"].Value = false;
                }
            }
        }

        private void txt_gc_DoubleClick(object sender, EventArgs e)
        {
            string sql = $@"SELECT
    DISTINCT
	ORG_CODE,
	ORG_NAME
FROM
	BASE001M";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_gc.Text = frmData.RetData.Rows[0]["ORG_NAME"].ToString();
                ORG_IDS = frmData.RetData.Rows[0]["ORG_CODE"].ToString();
              
            }
        }

        private void txt_ck_DoubleClick(object sender, EventArgs e)
        {
            string sql = string.Empty;
            if (string.IsNullOrWhiteSpace(ORG_IDS))
            {
                sql = $@"SELECT
    DISTINCT
	WAREHOUSE_CODE,
	WAREHOUSE_NAME
FROM
	MMS_WAREHOUSE_MANAGE
";
                FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
                frmData.ShowDialog();
                if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                {
                    txt_ck.Text = frmData.RetData.Rows[0]["WAREHOUSE_NAME"].ToString();
                    WAREHOUSE_CODE = frmData.RetData.Rows[0]["WAREHOUSE_CODE"].ToString();
                }
            }
            else
            {
                sql = $@"SELECT
	DISTINCT
	WAREHOUSE_CODE,
	WAREHOUSE_NAME
FROM
	MMS_WAREHOUSE_MANAGE
    WHERE  ORG_ID like '%{ORG_ID}%'
";
                FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
                frmData.ShowDialog();
                if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                {
                    txt_ck.Text = frmData.RetData.Rows[0]["WAREHOUSE_NAME"].ToString();
                    WAREHOUSE_CODE = frmData.RetData.Rows[0]["WAREHOUSE_CODE"].ToString();
                }
            }
        }
    }
}
