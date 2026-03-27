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
    public partial class F_IQC_VLaboratorytestresults_Vews : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics;
        public F_IQC_VLaboratorytestresults_Vews(Dictionary<string,object> dic)
        {
            InitializeComponent();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void F_IQC_VLaboratorytestresults_Vews_Load(object sender, EventArgs e)
        {
            //不确定对不对暂时
            string ID =dics["ID"].ToString();
            string inspection_name= dics["inspection_name"].ToString();
            string TASK_NO= dics["TASK_NO"].ToString();
            string INSPECTION_CODE = dics["INSPECTION_CODE"].ToString();

            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("PROD_NO", ID);
                p.Add("TASK_NO", TASK_NO);
                p.Add("INSPECTION_CODE", INSPECTION_CODE);
                
                
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.VMaterialinventory",//类名
                                            "CheckResultCSView",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData1.ToString());
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        txt_art.Text = dr["art_no"].ToString();
                        txt_xx.Text= dr["shoe_name"].ToString();
                        txt_ca.Text = dr["category_name"].ToString();
                        txt_jb.Text = dr["product_level_code"].ToString();
                        txt_po.Text = dr["order_po"].ToString();
                        txt_jd.Text= dr["season"].ToString();
                        txt_sjqty.Text = dr["send_test_qty"].ToString();
                        txt_ys.Text = dr["colors"].ToString();
                        txt_poqty.Text = dr["order_po_qty"].ToString();
                        txt_wl.Text = dr["material_name"].ToString();
                        txt_clid.Text = dr["makings_id"].ToString();
                        txt_bw.Text = dr["position_name"].ToString();
                        txt_cs.Text = dr["workmanship"].ToString();
                        txt_jd.Text = dr["phase_creation_name"].ToString();
                        txt_zl.Text = dr["makings_type_name"].ToString();
                        txt_mid.Text = dr["makings_id"].ToString();
                        txt_no.Text = dr["makings_type_code"].ToString();
                        txt_sjname.Text = dr["staff_name"].ToString();
                        txt_branch.Text = dr["staff_department"].ToString();
                        txt_jyzl.Text = dr["test_type"].ToString();
                        txt_jyxm.Text = inspection_name;
                        label25.Text = dr["task_no"].ToString();
                        label27.Text = dr["test_result"].ToString();
                    }
                 

                }
                if (dt2.Rows.Count>0&&dt2!=null)
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        int index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells["Column1"].Value = item["inspection_name"].ToString();
                        dataGridView1.Rows[index].Cells["Column2"].Value = item["inspection_code"].ToString();
                        dataGridView1.Rows[index].Cells["Column3"].Value = item["judge_mode"].ToString();
                        dataGridView1.Rows[index].Cells["Column4"].Value = item["item_test_result"].ToString();
                        dataGridView1.Rows[index].Cells["Column5"].Value = item["item_test_val"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }

        }

        private void btn_plot_Click(object sender, EventArgs e)
        {

        }
    }
}
