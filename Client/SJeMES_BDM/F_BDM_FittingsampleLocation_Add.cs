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
    public partial class F_BDM_FittingsampleLocation_Add : MaterialForm
    {
        /// <summary>
        /// 料品代号
        /// </summary>
        private string item_nos = string.Empty;
        /// <summary>
        /// 库位代号
        /// </summary>
        private string location_no = string.Empty;
        /// <summary>
        /// 库位名称
        /// </summary>
        private string location_name = string.Empty;
        /// <summary>
        /// 供应商代号
        /// </summary>
        private string vend_no = string.Empty;
        /// <summary>
        /// 品号
        /// </summary>
        private string txt_ITEM_NOS = string.Empty;
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_FittingsampleLocation_Add(string item_no)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            item_nos = item_no;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_BDM_FittingsampleLocation_Add_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(item_nos))
            {
                txt_ITEM_NO.ReadOnly = true;
                txt_NAME_S.ReadOnly = true;
                txt_SUPPLIERS_NAME.ReadOnly = true;
                //txt_PARENT_ITEM_NO.ReadOnly = true;
                UpdateView(item_nos);//修改前展示
            }
        }
        private void txt_ITEM_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txt_ITEM_NO.Text))
                {
                    TT();
                }
                else
                {
                    MessageBox.Show("Please enter the product number");
                }
            }
        }
        /// <summary>
        /// 品号输入带出几个输入框的值
        /// </summary>
        public void TT()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("item_no", txt_ITEM_NO.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.FittingsamplestorageBase",//类名BDM_LABORATORYSAMPLE_LOCATION
                                            "FittingsamplestorageGetView",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                DataTable dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data1"].ToString());
                if (dt.Rows.Count > 0 && dt1.Rows.Count > 0)
                {
                    txt_ITEM_NO.Text = dt.Rows[0]["ITEM_NO"].ToString();//品号
                    txt_ITEM_NOS = dt.Rows[0]["ITEM_NO"].ToString();//品号
                    txt_NAME_S.Text = dt.Rows[0]["NAME_S"].ToString();//品名
                    cbo_PARENT_ITEM_NO.Text = dt.Rows[0]["PARENT_ITEM_NO"].ToString();//ART
                    vend_no = dt1.Rows[0]["SUPPLIERS_CODE"].ToString();//供应商代号
                    txt_SUPPLIERS_NAME.Text = dt1.Rows[0]["SUPPLIERS_NAME"].ToString();//供应商名称
                    cbo_PARENT_ITEM_NO.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data2"].ToString());
                    cbo_PARENT_ITEM_NO.DisplayMember = "PARENT_ITEM_NO";
                    cbo_PARENT_ITEM_NO.ValueMember = "PARENT_ITEM_NO";
                    cbo_PARENT_ITEM_NO.SelectedIndex = -1;  
                }
                else
                {
                    MessageBox.Show("The condition of the product number is insufficient, and the next operation cannot be performed");
                    txt_NAME_S.Text = null;
                    cbo_PARENT_ITEM_NO.DataSource = null;
                    txt_SUPPLIERS_NAME.Text = null;
                    txt_ITEM_NOS = null;
                }
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void txt_fittingsamplelocation_Click(object sender, EventArgs e)
        {
            string sql = "select location_no 库位编号,location_name 库位名称,remarks 备注 from bdm_fittingsample_location";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_fittingsamplelocation.Text = frmData.RetData.Rows[0]["库位名称"].ToString();
                location_no = frmData.RetData.Rows[0]["库位编号"].ToString();
            }
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (NotNull.Trues(
                    txt_ITEM_NO.Text,
                    txt_ITEM_NOS,
                    txt_NAME_S.Text,
                    txt_SUPPLIERS_NAME.Text,
                    txt_fittingsamplelocation.Text
                    )
                )
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Required fields cannot be empty！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
                else
                {
                    //不为空就修改
                    if (!string.IsNullOrEmpty(item_nos))
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("ITEM_NO", item_nos);
                        p.Add("CAO", "Update");//修改
                        p.Add("location_no", location_no);
                        p.Add("location_name", txt_fittingsamplelocation.Text);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.FittingsamplestorageBase",//类名
                                                    "FittingsamplestorageUpdate",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                        else
                        {
                            MessageBox.Show("Data saved successfully");
                        }
                    }
                    //为空就添加
                    else
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("item_no", txt_ITEM_NOS);
                        p.Add("item_name", txt_NAME_S.Text.Trim());
                        p.Add("vend_name", txt_SUPPLIERS_NAME.Text.Trim());
                        p.Add("prod_no", cbo_PARENT_ITEM_NO.SelectedValue.ToString());
                        p.Add("location_name", txt_fittingsamplelocation.Text.Trim());
                        p.Add("vend_no", vend_no);
                        p.Add("location_no", location_no);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.FittingsamplestorageBase",//类名
                                                    "FittingsamplestorageeAdd",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                        else
                        {
                            MessageBox.Show("Data saved successfully");
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
        /// 修改前显示
        /// </summary>
        public void UpdateView(string item_nos)
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("item_no", item_nos);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.FittingsamplestorageBase",//类名
                                            "FittingsamplestorageUpdateView",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        txt_ITEM_NO.Text = item["item_no"].ToString();
                        txt_ITEM_NOS = item["item_no"].ToString();
                        TT();
                        txt_NAME_S.Text = item["item_name"].ToString();
                        txt_SUPPLIERS_NAME.Text = item["vend_name"].ToString();
                        cbo_PARENT_ITEM_NO.SelectedValue = item["prod_no"].ToString();
                        txt_fittingsamplelocation.Text = item["location_name"].ToString();
                        location_no = item["location_no"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void btn_Out_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
