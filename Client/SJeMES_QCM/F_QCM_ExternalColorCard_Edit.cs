using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SJeMES_QCM
{
    public partial class F_QCM_ExternalColorCard_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _VEND_NO { get; set; }
        public string _SHOE_NO { get; set; }
        public string _PROD_NO { get; set; }
        public string _CARD_DATE { get; set; }
        public F_QCM_ExternalColorCard_Edit(string VEND_NO, string SHOE_NO,string PROD_NO,string CARD_DATE)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            _VEND_NO = VEND_NO;
            _SHOE_NO = SHOE_NO;
            _PROD_NO = PROD_NO;
            _CARD_DATE = CARD_DATE;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_ExternalColorCard_Add_Load(object sender, EventArgs e)
        {
            try
            {
                #region 表头
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("VEND_NO", _VEND_NO);
                data.Add("SHOE_NO", _SHOE_NO);
                data.Add("PROD_NO", _PROD_NO);
                data.Add("CARD_DATE", _CARD_DATE);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ExternalColorCard", "GetColorHead", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            this.txt_date.Text = dr["CARD_DATE"].ToString();
                            this.txt_vend_name.Text = dr["VEND_NAME"].ToString();
                            this.txt_firstarticle_type.Text = dr["FIRSTARTICLE_TYPE"].ToString();

                            this.txt_shoe_no.Text = dr["SHOE_NO"].ToString();
                            this.txt_prod_no.Text = dr["PROD_NO"].ToString();
                            //this.txt_is_qcconfirm.Text = dr["IS_QCCONFIRM"].ToString();
                            this.txt_test_result.Text = dr["TEST_RESULT"].ToString();
                        }
                    }

                }
                #endregion

                #region 表身

                Dictionary<string, object> data2 = new Dictionary<string, object>();
                data2.Add("VEND_NO", _VEND_NO);
                data2.Add("SHOE_NO", _SHOE_NO);
                data2.Add("PROD_NO", _PROD_NO);
                data2.Add("CARD_DATE", _CARD_DATE);

                string retdata2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ExternalColorCard", "GetColorCardBody", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data2));
                ResultObject ret2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata2);
                if (!ret2.IsSuccess)
                    throw new Exception(ret2.ErrMsg);
                else
                {
                    Dictionary<string, object> dic2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2.RetData);

                    var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic2["Data"].ToString());
                    if (dt2.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["CARD_DATE"].Value = dr2["CARD_DATE"].ToString();
                            dgvr.Cells["VEND_NO"].Value = dr2["VEND_NO"].ToString();
                            dgvr.Cells["SHOE_NO"].Value = dr2["SHOE_NO"].ToString();
                            dgvr.Cells["PROD_NO"].Value = dr2["PROD_NO"].ToString();
                            dgvr.Cells["APTESTITEM_NAME"].Value = dr2["APTESTITEM_NAME"].ToString();//检测项名称

                            dgvr.Cells["TEST_STANDARD"].Value = dr2["TEST_STANDARD"].ToString();//检测标准
                            dgvr.Cells["SAMP_QTY"].Value = dr2["SAMP_QTY"].ToString();//抽样数量
                            dgvr.Cells["AQL_LEVEL"].Value = dr2["AQL_LEVEL"].ToString();//AQL级别
                            dgvr.Cells["AC"].Value = dr2["AC"].ToString();
                            dgvr.Cells["RE"].Value = dr2["RE"].ToString();
                            dgvr.Cells["CHECK_RESULT"].Value = dr2["CHECK_RESULT"].ToString();
                            dgvr.Cells["REMARKS"].Value = dr2["REMARKS"].ToString();
                            i++;
                        }
                    }

                }

                #endregion
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("CARD_DATE", this.txt_date.Text);
                data.Add("VEND_NAME", this.txt_vend_name.Text);
                data.Add("FIRSTARTICLE_TYPE", this.txt_firstarticle_type.Text);
                data.Add("SHOE_NO", this.txt_shoe_no.Text);
                data.Add("PROD_NO", this.txt_prod_no.Text);
                //data.Add("IS_QCCONFIRM", this.txt_is_qcconfirm.Text);
                data.Add("TEST_RESULT", this.txt_test_result.Text);
                data.Add("VEND_NO", _VEND_NO);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ExternalColorCard", "UpdateColor", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            this.txt_date.Text = dr["CARD_DATE"].ToString();
                            this.txt_vend_name.Text = dr["VEND_NAME"].ToString();
                            this.txt_firstarticle_type.Text = dr["FIRSTARTICLE_TYPE"].ToString();

                            this.txt_shoe_no.Text = dr["SHOE_NO"].ToString();
                            this.txt_prod_no.Text = dr["PROD_NO"].ToString();
                            //this.txt_is_qcconfirm.Text = dr["IS_QCCONFIRM"].ToString();
                            this.txt_test_result.Text = dr["TEST_RESULT"].ToString();
                        }
                    }
                    MessageBox.Show(ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
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
    }
}
