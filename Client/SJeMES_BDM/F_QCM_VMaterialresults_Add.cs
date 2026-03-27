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

namespace SJeMES_BDM
{
    public partial class F_QCM_VMaterialresults_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Dictionary<string, object> dics;
        public F_QCM_VMaterialresults_Add(Dictionary<string,object> dic)
        {
            InitializeComponent();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
         Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
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

        private void F_QCM_VMaterialresults_Add_Load(object sender, EventArgs e)
        {
            lab_lh.Text = dics["CHK_NO"].ToString();//料号
            /* lab_sccs.Text = dics[""].ToString();//生产厂商

             lab_wlmc.Text = dics[""].ToString();//材料名称
             lab_jccs.Text = dics[""].ToString();//进仓厂商
             lab_jcsl.Text = dics[""].ToString();//进仓数量
             lab_xx.Text = dics[""].ToString();//鞋型
             lab_art.Text = dics[""].ToString();//art
             lab_bw.Text = dics[""].ToString();//部位*/
             

            //加载表身视图
            if (string.IsNullOrEmpty(lab_lh.Text))
            {
                try
                {
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("chk_no", lab_lh.Text);//收料日期

                  
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.VMaterialinventory",//类名
                                                "CheckResultLRView",//方法名
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
                            dgvr.Cells["test_item_name"].Value = dr["test_item_name"].ToString();
                            dgvr.Cells["test_standard"].Value = dr["test_standard"].ToString();
                            dgvr.Cells["determine"].Value = dr["determine"].ToString();
                            dgvr.Cells["remark"].Value = dr["remark"].ToString();
                            dgvr.Cells["image_guid"].Value = dr["image_guid"].ToString();
                            i++;
                        }
                        GenClass.AutoSizeColumn(dataGridView1);

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
            
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                string checkeds = string.Empty;
                if (radioButton_pass.Checked)
                {
                    checkeds = radioButton_pass.Text;
                }
                if (radioButton_fail.Checked)
                {
                    checkeds = radioButton_fail.Text;
                }
                if (string.IsNullOrWhiteSpace(txt_cysl.Text)||
                    string.IsNullOrWhiteSpace(txt_aqljb.Text)||
                    string.IsNullOrWhiteSpace(txt_acre.Text)||
                    string.IsNullOrWhiteSpace(txt_cysl.Text)||
                    string.IsNullOrWhiteSpace(checkeds) )
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("必填项不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
                else
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();

                    Dictionary<string, object> a = new Dictionary<string, object>();
                    a.Add("sample_qty", txt_cysl.Text);//抽样数量
                    a.Add("chk_no", dics["CHK_NO"]);//来料单号
                    //a.Add("SHOE_NO", txt_acre.Text);
                    a.Add("bad_qty", txt_bhgs.Text);//不合格数量
                    a.Add("determine", checkeds);//判断
                    int i = 0;
                    if (dataGridView1.Rows.Count > 0)
                    {
                        List<Dictionary<string, object>> diclist = new List<Dictionary<string, object>>();
                        foreach (DataGridViewRow dgr in dataGridView1.Rows)
                        {
                            if (i < dataGridView1.Rows.Count - 1)
                            {
                                Dictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("test_item_no", dgr.Cells["test_item_no"].ToString());//检验项编号
                                dic.Add("test_item_name", dgr.Cells["test_item_name"].ToString());//检验项名称
                                dic.Add("test_standard", dgr.Cells["test_standard"].ToString());//检验标准
                                dic.Add("determine", dgr.Cells["determine"].ToString());//检验结果
                                dic.Add("image_guid",dgr.Cells["image_guid"].ToString());//图片关联id
                                dic.Add("remark", dgr.Cells["remark"].ToString());//备注
                                diclist.Add(dic);
                            }
                        }
                        p.Add("p", a);
                        p.Add("diclist", diclist);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.VMaterialinventory",//类名
                                                    "CheckResultAdd",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                        else
                        {
                            MessageBox.Show("保存数据成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请填写内容再保存");
                    }
                  
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
