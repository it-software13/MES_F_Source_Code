using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls;
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
    public partial class F_QCM_AQLBAEntering_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_AQLBAEntering_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_AQLBAEntering_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }
        public DataTable table()
        {
            DataTable ddd = new DataTable();
            ddd.Columns.Add("箱号", typeof(string));
            ddd.Columns.Add("码数", typeof(string));
            ddd.Columns.Add("双数", typeof(string));
            ddd.Columns.Add("订单数", typeof(string));
            DataRow dr1 = ddd.NewRow();
            dr1["箱号"] = 2021111600001;
            dr1["码数"] = "5";
            dr1["双数"] = 0;
            dr1["订单数"] = 3;
            ddd.Rows.Add(dr1);

            DataRow dr2 = ddd.NewRow();
            dr2["箱号"] = 2021111600002;
            dr2["码数"] = "5.5";
            dr2["双数"] = 1;
            dr2["订单数"] = 5;
            ddd.Rows.Add(dr2);

            DataRow dr3 = ddd.NewRow();
            dr3["箱号"] = 2021111600003;
            dr3["码数"] = "6.0";
            dr3["双数"] = 1;
            dr3["订单数"] = 4;
            ddd.Rows.Add(dr3);

            DataRow dr4 = ddd.NewRow();
            dr4["箱号"] = 2021111600005;
            dr4["码数"] = "5";
            dr4["双数"] = 0;
            dr4["订单数"] = 2;
            ddd.Rows.Add(dr4);
            return ddd;
        }
        DataTable dd = new DataTable();

        public DataTable table2()
        {

            dd.Columns.Add("不精美项目", typeof(string));
            dd.Columns.Add("不精美数量", typeof(string));
            DataRow dr1 = dd.NewRow();
            dr1["不精美项目"] = "C9";
            dr1["不精美数量"] = 2;
            dd.Rows.Add(dr1);

            DataRow dr2 = dd.NewRow();
            dr2["不精美项目"] = "C8";
            dr2["不精美数量"] = 2;
            dd.Rows.Add(dr2);

            DataRow dr3 = dd.NewRow();
            dr3["不精美项目"] = "C7";
            dr3["不精美数量"] = 1;
            dd.Rows.Add(dr3);

            DataRow dr4 = dd.NewRow();
            dr4["不精美项目"] = "C6";
            dr4["不精美数量"] = 1;
            dd.Rows.Add(dr4);

            DataRow dr5 = dd.NewRow();
            dr5["不精美项目"] = "C5";
            dr5["不精美数量"] = 3;
            dd.Rows.Add(dr5);

            DataRow dr6 = dd.NewRow();
            dr6["不精美项目"] = "C4";
            dr6["不精美数量"] = 2;
            dd.Rows.Add(dr6);

            DataRow dr7 = dd.NewRow();
            dr7["不精美项目"] = "C3";
            dr7["不精美数量"] = 2;
            dd.Rows.Add(dr7);

            DataRow dr8 = dd.NewRow();
            dr8["不精美项目"] = "C2";
            dr8["不精美数量"] = 6;
            dd.Rows.Add(dr8);

            DataRow dr9 = dd.NewRow();
            dr9["不精美项目"] = "C1";
            dr9["不精美数量"] = 1;
            dd.Rows.Add(dr9);
            return dd;
        }
        public DataTable table3()
        {
            DataTable dddd = new DataTable();
            dddd.Columns.Add("不良项目", typeof(string));
            dddd.Columns.Add("不良数量", typeof(string));

            DataRow dr1 = dddd.NewRow();
            dr1["不良项目"] = "[340.05]修补不良鞋带";
            dr1["不良数量"] = 2;
            dddd.Rows.Add(dr1);

            DataRow dr2 = dddd.NewRow();
            dr2["不良项目"] = "[340.08]修补不良鞋底";
            dr2["不良数量"] = 5;
            dddd.Rows.Add(dr2);

            DataRow dr3 = dddd.NewRow();
            dr3["不良项目"] = "[340.06]修补不良鞋ART";
            dr3["不良数量"] = 1;
            dddd.Rows.Add(dr3);

            DataRow dr4 = dddd.NewRow();
            dr4["不良项目"] = "[400.01]污染(脏）(2点<2MM)";
            dr4["不良数量"] = 9;
            dddd.Rows.Add(dr4);

            DataRow dr5 = dddd.NewRow();
            dr5["不良项目"] = "[400.05]修补不良，补漆不良，画...";
            dr5["不良数量"] = 9;
            dddd.Rows.Add(dr5);

            DataRow dr6 = dddd.NewRow();
            dr6["不良项目"] = "[400.03]高胶或处理剂外溢( <4M...";
            dr6["不良数量"] = 10;
            dddd.Rows.Add(dr6);
            return dddd; ;
        }

        private void btn_c1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_1.Text.Trim()))
            {
                string tt = ((Button)sender).Text;
                switch (tt)
                {
                    case "C1":
                        dataGridView2.Rows[8].Cells["Column2"].Value = (Convert.ToInt32(dataGridView2.Rows[8].Cells["Column2"].Value) + 1).ToString();
                        break;
                    case "C2":
                        dataGridView2.Rows[7].Cells["Column2"].Value = (Convert.ToInt32(dataGridView2.Rows[7].Cells["Column2"].Value) + 1).ToString();
                        break;
                    case "C3":
                        dataGridView2.Rows[6].Cells["Column2"].Value = (Convert.ToInt32(dataGridView2.Rows[6].Cells["Column2"].Value) + 1).ToString();
                        break;
                    case "C4":
                        dataGridView2.Rows[5].Cells["Column2"].Value = (Convert.ToInt32(dataGridView2.Rows[5].Cells["Column2"].Value) + 1).ToString();
                        break;
                    case "C5":
                        dataGridView2.Rows[4].Cells["Column2"].Value = (Convert.ToInt32(dataGridView2.Rows[4].Cells["Column2"].Value) + 1).ToString();
                        break;
                    case "C6":
                        dataGridView2.Rows[3].Cells["Column2"].Value = (Convert.ToInt32(dataGridView2.Rows[3].Cells["Column2"].Value) + 1).ToString();
                        break;
                    case "C7":
                        dataGridView2.Rows[2].Cells["Column2"].Value = (Convert.ToInt32(dataGridView2.Rows[2].Cells["Column2"].Value) + 1).ToString();
                        break;
                    case "C8":
                        dataGridView2.Rows[1].Cells["Column2"].Value = (Convert.ToInt32(dataGridView2.Rows[1].Cells["Column2"].Value) + 1).ToString();
                        break;
                    case "C9":
                        dataGridView2.Rows[0].Cells["Column2"].Value = (Convert.ToInt32(dataGridView2.Rows[0].Cells["Column2"].Value) + 1).ToString();
                        break;
                }
            }
            

        }

        private void btn_add3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_1.Text))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("PO单号不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
                else
                {
                    MessageBox.Show("确认成功");
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
           
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeViewEx1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            dataGridView3.DataSource=null;
            string [] arr = { 
                "[100.01]包装与标签", "[100.02]不良项都是现有的", "[100.03]导入进来就行",
                "[200.03]导入进来就行", "[200.02]不良项都是现有的", 
                "[310.01]{1.5}皮料不良（纹路松散，脱胶）","[310.02]{1.5}材料破损、材质不良（织布）", "[310.03]{1.5}左右脚颜色、皮料为纹路不对称","[310.04]{0})尖锐边(童鞋)或任何可能            ",
                "[320.01]导入进来就行","[320.02]导入进来就行", "[320.03]导入进来就行","[320.04]导入进来就行",
                "[330.01]导入进来就行","[330.02]导入进来就行",
                "[340.05]修补不良鞋规格码数","[340.06]修补不良鞋ART","[340.02]导入进来就行","[340.05]修补不良鞋带","[340.02]导入进来就行",
                "[350.01]导入进来就行","[350.02]导入进来就行","[350.03]导入进来就行","[350.04]导入进来就行","[350.05]导入进来就行",

            };
            string name = e.Node.Text.ToString();
            treeViewEx2.Nodes.Clear();
            for (int i = 0; i < arr.Length-1; i++)
            {
                if (name.Contains(arr[i].Substring(0, 4)))
                {
                    TreeNode root = new TreeNode(arr[i]);
                    treeViewEx2.Nodes.Add(root);
                }
            }
           
        }

        private void treeViewEx2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            dataGridView3.DataSource = table3(); ;
            string name = e.Node.Text.ToString();
          
            if (!string.IsNullOrEmpty(name))
            {
                DataRow[] dr = table3().Select($@"不良项目='{name}'");
                DataTable dt = table3().Clone();
                for (int i = 0; i < dr.Length; i++)
                {
                    dt.ImportRow(dr[i]);
                }
                dataGridView3.DataSource = dt;
            }
            else
            {
                dataGridView3.DataSource = table3();
            }
        }

        private void btn_add2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_1.Text))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("PO单号不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
                else
                {
                    MessageBox.Show("点箱成功");
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        int i = 0;
        private void btn_aad_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrEmpty(txt_1.Text))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("PO单号不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                   
                }
               
                else
                {
                    i += 1;
                    if (i==1)
                    {
                        dataGridView1.DataSource = table();

                        DataTable dt = table2();
                        dataGridView2.Rows.Clear();
                        if (dt.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                dataGridView2.Rows.Add();
                                DataGridViewRow dgvr = dataGridView2.Rows[i];
                                dgvr.Cells["Column1"].Value = dr["不精美项目"].ToString();
                                dgvr.Cells["Column2"].Value = dr["不精美数量"].ToString();
                                i++;
                            }
                        }
                        treeViewEx1.Visible = true;
                        txt_2.Text = "德国";
                        cob_3.Text = "Statement";
                        txt_4.Text = "GX3971";
                        txt_5.Text = "422";
                        cbo_6.Text = "3-翻箱";
                        txt_7.Text = "PO-检验A8员";
                        txt_81.Text = "FORUMHI GTX";
                        txt_82.Text = "422";
                        cbo_83.Text = "新鞋型";
                        cbo_84.Text = "2PG5L11";
                        textBox24.Text = "3.5";
                        textBox25.Text = "422";
                        textBox26.Text = "3";

                        txt_8.Text = "17%";
                        txt_9.Text = "24";
                        txt_10.Text = "3";
                        txt_11.Text = "4";
                        txt_12.Text = "3";
                        txt_13.Text = "3";
                        txt_14.Text = "Ⅱ";
                        txt_15.Text = "80";
                        txt_16.Text = "2";
                        txt_17.Text = "3";
                        txt_18.Text = "0";
                        txt_19.Text = "4";
                        txt_20.Text = "0";
                        txt_21.Text = "1";
                        txt_22.Text = "0";
                        txt_23.Text = "3";
                        txt_24.Text = "Accepted";
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
            if (!string.IsNullOrEmpty(txt_1.Text.Trim()))
            {
                MessageBox.Show("点箱成功");
            }
           

        }
    }
}
