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
    public partial class UC_F_QCM_Ex_Item_Set_New_CPX : UserControl
    {
        public UC_F_QCM_Ex_Item_Set_New_CPX(bool is_readonly = false)
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
                if (item.Name == "txt_cpx_ddpo")
                {
                    txt_cpx_ddpo.ReadOnly = true;
                    item.Enabled = true;
                }
            }
        }

        private void txt_cpx_ddpo_DoubleClick(object sender, EventArgs e)
        {
            if (!txt_cpx_ddpo.ReadOnly)
            {
                F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(txt_cpx_art.Text, txt_cpx_ddpo.Text);
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
                    txt_cpx_ddpo.Text = poorder.Trim(',');
                    txt_cpx_posl.Text = total_qty.ToString();
                }
            }
        }

        private void txt_cpx_ddpo_MouseHover(object sender, EventArgs e)
        {
            Control currC = this.txt_cpx_ddpo;
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
