using FastReport;
using FastReport.Dialog;
using FastReport.Preview;
using FastReport.Web;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public partial class F_IQC_VWarehouseDmp_Print : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_IQC_VWarehouseDmp_Print()
        {
            InitializeComponent();
            InitDateTimePicker(dateTimeP_putin_date);
            InitDateTimePicker(dateTimeP_end_date);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void F_IQC_VWarehouseDmp_Print_Load(object sender, EventArgs e)
        {
            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";

            this.dateTimeP_end_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_end_date.CustomFormat = " ";

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
        /// <summary>
        /// 皮料检验报告数据源
        /// </summary>
        public DataTable Print_SelectListPL()
        {
            string putin_date = string.Empty;
            string end_date = string.Empty;


            if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
            {
                putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date.Text))
            {
                end_date = Convert.ToDateTime(this.dateTimeP_end_date.Value).ToString("yyyy-MM-dd");
            }

            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("putin_date", putin_date);//日期
            p.Add("end_date", end_date);
            p.Add("cscode", txt_cscode.Text);//厂商代码
            p.Add("stoc_no", txt_stoc_no.Text);//仓库
            p.Add("item_no", txt_item_no.Text);//材料编号"4020200267"
            p.Add("failremark", textBoxfailremark.Text);//不合格证明
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.VMaterialinventory",//类名
                                        "CheckResultMainDmp_PrintPL",//方法名
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
            count1 = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(dic["total"].ToString());
            return dt;

        }
        /// <summary>
        /// 原材料检验报告数据源
        /// </summary>
        public DataTable Print_SelectListYCL()
        {
            string putin_date = string.Empty;
            string end_date = string.Empty;
           
            if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
            {
                putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date.Text))
            {
                end_date = Convert.ToDateTime(this.dateTimeP_end_date.Value).ToString("yyyy-MM-dd");
            }
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("putin_date", putin_date);//日期
            p.Add("end_date", end_date);
            p.Add("cscode", txt_cscode.Text);//厂商代码
            p.Add("stoc_no", txt_stoc_no.Text);//仓库
            p.Add("item_no", txt_item_no.Text);//材料编号"4020200267"
            p.Add("failremark", textBoxfailremark.Text);//不合格证明
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.VMaterialinventory",//类名
                                        "CheckResultMainDmp_PrintYCL",//方法名
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
            count2 = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(dic["total"].ToString());
            return dt;

        }
        /// <summary>
        /// 打印的数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dt"></param>
        private  void prints(string url, DataTable dt)
        {
            try
            {
                List<DataTable> list_dt = new List<DataTable>();
                List<DataSet> list_ds = new List<DataSet>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dd = dt.Clone();
                        DataRow row = dt.Rows[i];
                        dd.ImportRow(row);
                        list_dt.Add(dd);
                    }
                    for (int i = 0; i < list_dt.Count; i++)
                    {
                        list_dt[i].TableName = "Table";
                        DataSet dsa = new DataSet();
                        dsa.Tables.Add(list_dt[i].Copy());
                        list_ds.Add(dsa);
                    }
                    using (FrmSelectPrint add = new FrmSelectPrint(url, list_ds))
                    {
                        add.ShowDialog();
                    }
                }
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_out_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string count1 = string.Empty;
        private void btn_pljybg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_item_no.Text) ||
             string.IsNullOrWhiteSpace(dateTimeP_putin_date.Text) ||
             string.IsNullOrWhiteSpace(dateTimeP_end_date.Text))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the necessary conditions and then execute the query, prompt: material number, date of receipt！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            else
            {
                if (dateTimeP_putin_date.Value.AddDays(62) >= dateTimeP_end_date.Value)
                {
                    string url = Application.StartupPath + "/newfrx/皮料检验不良报告.frx";//皮料检验不良报告
                    DataTable dt = Print_SelectListPL();
                    if (MessageBox.Show($"According to the condition query data, there are a total of [{count1}] items. If there are too many data, an exception will occur. Do you want to print？ ", "*", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        prints(url, dt);
                    }
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("It is forbidden to print the data within the specified range of 62 days for the date of receipt, please re-regulate the number of days", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }

            }
           
          
        }
        private string count2 = string.Empty;
        private void btn_ycl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_item_no.Text) ||
               string.IsNullOrWhiteSpace(dateTimeP_putin_date.Text) ||
               string.IsNullOrWhiteSpace(dateTimeP_end_date.Text))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the necessary conditions and then execute the query, prompt: material number, date of receipt！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            else
            {
                if (dateTimeP_putin_date.Value.AddDays(62)>= dateTimeP_end_date.Value)
                {
                    string url = Application.StartupPath + "/newfrx/原物料检验不良报告.frx";
                    DataTable dt = Print_SelectListYCL();
                    if (MessageBox.Show($"According to the condition query data, there are a total of [{count2}] items. If there are too many data, an exception will occur. Do you want to print？ ", "*", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        prints(url, dt);
                    }
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("It is forbidden to print the data within the specified range of 62 days for the receipt date selection, please re-standardize the number of days", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
               
            }
           
               
        }

        private void txt_stoc_no_Click(object sender, EventArgs e)
        {
            //            string sql = $@"
            //select*from (
            //SELECT DISTINCT
            //	'123' KN,
            //	A.org_id  编号,
            //	b.ORG_NAME 名称 
            //FROM
            //	wms_rcpt_m A
            //INNER JOIN base001m b ON A .ORG_ID = b.ORG_CODE
            //UNION all
            //SELECT DISTINCT
            //	'1234' KN,
            //	STOC_NO  编号,
            //	'仓库' ORG_NAME 
            //FROM
            //	wms_rcpt_m ) order by KN ASC
            //";

            string sql = $@"
select*from (
SELECT DISTINCT
	'123' KN,
	A.org_id,
	b.ORG_NAME
FROM
	wms_rcpt_m A
INNER JOIN base001m b ON A .ORG_ID = b.ORG_CODE
UNION all
SELECT DISTINCT
	'1234' KN,
	STOC_NO,
	'Storehouse' ORG_NAME 
FROM
	wms_rcpt_m ) order by KN ASC
";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client, "KN");
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_stoc_no.Text = frmData.RetData.Rows[0]["org_id"].ToString();

            }
        }
    }
}
