using System;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Framework.WebAPI;
using SJeMES_Framework.Common;

namespace SJeMES_IQC
{
    public partial class F_IQC_Previousinspectionresults_view : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics;
        public F_IQC_Previousinspectionresults_view(Dictionary<string, object> dic)
        {
            InitializeComponent();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_IQC_Previousinspectionresults_view_Load(object sender, EventArgs e)
        {
            if (dics != null && dics.Count > 0)
            {
                 
                lab_sccs.Text = dics["SUPPLIERS_NAME"].ToString();//生产厂商
                lab_clmc.Text = dics["ITEM_NAME"].ToString();//材料名称
                LTooltip(lab_clmc, 40, lab_clmc.Text); 
                lab_lh.Text = dics["ITEM_NO"].ToString();//料号
                //lab_jcqty.Text = dics["RCPT_QTY"].ToString();//进仓数量 
                try
                {
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>(); 
                    p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//物料代号 
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJeMES_IQC",//类库名
                                                "SJeMES_IQC.VMaterialinventory",//类名
                                                "CheckPreviousResultJYView",//方法名
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
                    DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject < DataTable > (dic["Data2"].ToString());
                    //lab_jcqty.Text = dt2.[0].ColumnName
                    lab_jcqty.Text= dt2.Rows[0]["total_qty"].ToString();
                    lblpassqty.Text = dt2.Rows[0]["pass_qty"].ToString();
                    lblfailqty.Text = dt2.Rows[0]["fail_qty"].ToString();
                    dataGridView1.Rows.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["insp_date"].Value = dr["createdate"].ToString();//检验项名称
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
