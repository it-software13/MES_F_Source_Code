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

namespace SjeMES_QCM_Ex.F_QCM_Ex_Item_Set_UC
{
    public partial class UC_F_QCM_Ex_Item_Set_New_CL : UserControl
    {
        public string CL_QRCODE_JSON = "";
        public UC_F_QCM_Ex_Item_Set_New_CL(bool is_readonly = false)
        {
            InitializeComponent();
            if (is_readonly)
                ReadOnlyControl();
        }

        public void ReadOnlyControl()
        {
            foreach (Control item in this.Controls)
            {
                item.Enabled = false;
            }
        }

        private void UC_F_QCM_Ex_Item_Set_New_CL_Load(object sender, EventArgs e)
        {
            GetQrCodeInfo(CL_QRCODE_JSON, false);
        }

        public void GetQrCodeInfo(string jsonKey, bool suppliers_read = true)
        {
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonKey);
            DataTable dt = print_lict(dic["rcpt_date"].ToString(), dic["chk_no"].ToString(), dic["item_no"].ToString(), dic["chk_seq"].ToString(), dic["org_id"].ToString());
            if (dt.Rows.Count > 0)
            {
                DataRow fRow = dt.Rows[0];
                tb_cl_lh.Text = fRow["Material_No"].ToString();
                tb_cl_clmc.Text = fRow["Material_Name"].ToString();
                tb_artandshoe.Text = fRow["Shoe_NameAndArt_No"].ToString();
                tb_order_number.Text = fRow["ORDER_NOAndQty"].ToString();
                tb_cl_suoyongbuweimingcheng.Text = fRow["part_no"].ToString();
                if (suppliers_read)
                {
                    txt_cl_cs.Text = fRow["SUPPLIERS_NAME"].ToString();
                    lab_cl_cs_code.Text = fRow["suppliers_code"].ToString();
                }

                List<string> art_list = new List<string>();
                foreach (var item in tb_artandshoe.Text.Split(','))
                {
                    var item_info = item.Split('/');
                    if (item_info.Length == 2)
                    {
                        art_list.Add(item_info[1]);
                    }
                }

            }
        }

        public DataTable print_lict(string rcpt_date, string chk_no, string item_no, string chk_seq, string org_id)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("chk_no", chk_no);//收料单号
            p.Add("item_no", item_no);//料号
            p.Add("chk_seq", chk_seq);//料号序号
            p.Add("rcpt_date", rcpt_date);//收料日期
            p.Add("org_id", org_id);//工厂编号
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.VMaterialinventory",//类名
                                        "CheckResultMainDmp_PrintXC2",//方法名
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
            return dt;
        }

    }
}
