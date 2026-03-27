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
    public partial class UC_F_QCM_Ex_Item_Set_New_BJ : UserControl
    {
        public UC_F_QCM_Ex_Item_Set_New_BJ(bool is_readonly = false)
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
                if (item.Name == "txt_bj_po_order")
                {
                    txt_bj_po_order.ReadOnly = true;
                    item.Enabled = true;
                }
            }
        }

        private void txt_bj_cs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_bj_cs.Text.Trim() != "")
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("code", txt_bj_cs.Text.Trim());

                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetCSDataByCode",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    txt_bj_cs.Text = "";
                    txt_bj_cs.Focus();
                }
                else
                {
                    var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                    txt_bj_cs.Text = dic["SUPPLIERS_NAME"].ToString();
                    lab_bj_cs_code.Text = dic["SUPPLIERS_CODE"].ToString();
                    lab_bj_cs_jc.Text = dic["JC"].ToString();
                }
            }
        }

        private void txt_bj_cs_DoubleClick(object sender, EventArgs e)
        {

            F_QCM_SelectCS frm = new F_QCM_SelectCS();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frm.selectdic.Count > 0)
            {
                txt_bj_cs.Text = frm.selectdic["SUPPLIERS_NAME"].ToString();
                lab_bj_cs_code.Text = frm.selectdic["SUPPLIERS_CODE"].ToString();
                lab_bj_cs_jc.Text = frm.selectdic["JC"].ToString();
            }
        }

        private void txt_bj_po_order_DoubleClick(object sender, EventArgs e)
        {
            if (!txt_bj_po_order.ReadOnly)
            {
                F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(txt_bj_art.Text, txt_bj_po_order.Text);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
                if (frm.selectlist.Count > 0)
                {
                    string poorder = "";
                    int total_qty = 0;
                    foreach (var item in frm.selectlist)
                    {
                        poorder += item["poorder"].ToString() + ",";
                        int qty = 0;
                        int.TryParse(item["qty"].ToString(), out qty);
                        total_qty += qty;
                    }
                    txt_bj_po_order.Text = poorder.Trim(',');
                    txt_bj_po_qty.Text = total_qty.ToString();
                }
            }
        }

        private void txt_bj_po_order_MouseHover(object sender, EventArgs e)
        {
            Control currC = this.txt_bj_po_order;
            if (!string.IsNullOrEmpty(currC.Text))
            {
                // 创建the ToolTip 
                ToolTip toolTip1 = new ToolTip();

                // 设置显示样式
                toolTip1.AutoPopDelay = 25000;
                toolTip1.InitialDelay = 500;//事件触发多久后出现提示
                toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
                toolTip1.ShowAlways = true;//是否显示提示框

                //  设置伴随的对象.
                toolTip1.SetToolTip(currC, currC.Text);//设置提示按钮和提示内容
            }
        }
    }
}
