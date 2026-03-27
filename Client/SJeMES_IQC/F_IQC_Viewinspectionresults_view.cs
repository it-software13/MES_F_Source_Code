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

namespace SJeMES_IQC
{
    public partial class F_IQC_Viewinspectionresults_view : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics;
        public F_IQC_Viewinspectionresults_view(Dictionary<string,object> dic)
        {
            InitializeComponent();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_IQC_Viewinspectionresults_view(string ITEM_NO)
        {
            InitializeComponent();
             
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public F_IQC_Viewinspectionresults_view(Dictionary<string, object> dic, SJeMES_Framework.Class.ClientClass client)
        {
            InitializeComponent();
            dics = dic;
            Program.Client = client;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void F_QCM_Viewinspectionresults_view_Load(object sender, EventArgs e)
        {
            if (dics != null && dics.Count>0)
            {
                lab_sldh.Text = dics["CHK_NO"].ToString();//收料单号
                lab_sccs.Text = dics["SUPPLIERS_NAME"].ToString();//生产厂商
                lab_clmc.Text = dics["ITEM_NAME"].ToString();//材料名称
                LTooltip(lab_clmc, 40, lab_clmc.Text);
                lab_jcrq.Text = dics["RCPT_DATE"].ToString();//进仓日期
                lab_xx.Text = dics["SHOE_NO"].ToString();//鞋型
                lab_bw.Text =dics["PART"].ToString();//部位
                lab_sfpl.Text= dics["ORDER_NO"].ToString();//采购单号
                lab_lh.Text = dics["ITEM_NO"].ToString();//料号
                lab_jcqty.Text = dics["RCPT_QTY"].ToString();//进仓数量
                lab_art.Text = dics["PROD_NO"].ToString();//ART
                try
                {
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("CHK_NO", lab_sldh.Text);//收料单号
                    p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//物料代号
                    p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//物料序号
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJeMES_IQC",//类库名
                                                "SJeMES_IQC.VMaterialinventory",//类名
                                                "CheckResultJYView",//方法名
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
                            dgvr.Cells["test_item_name"].Value = dr["test_item_name"].ToString();//检验项名称
                            dgvr.Cells["test_standard"].Value = dr["test_standard"].ToString();//检验标准

                            if (dr["determine"].ToString() == "0")
                            {
                                dgvr.Cells["determine"].Value = "PASS";
                            }
                            else
                            {
                                dgvr.Cells["determine"].Value = "FAIL";
                                dgvr.Cells["determine"].Style.ForeColor = Color.Red;
                            }
                            dgvr.Cells["sample_qty"].Value = dr["sample_qty"].ToString();//抽样数量
                            i++;
                        }
                        GenClass.AutoSizeColumn(dataGridView1);

                    }
                    this.dataGridView1.ClearSelection();
                }
                catch (Exception ex)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }

            }
        }
        private static void LTooltip(System.Windows.Forms.Label label, int length, string value)
        {
            label.Text = value;
            if (value.Length > length)
            {
                label.Text = label.Text.Substring(0, length) + "...";
            }
            var tip = new ToolTip();
            tip.IsBalloon = false;
            tip.ShowAlways = true;
            tip.SetToolTip(label, value);
        }
    }
}
