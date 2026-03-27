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

namespace SJeMES_QCM
{
    public partial class F_QCM_PaintedSkinRecords_Detail2 : MaterialForm
    {
        public DataTable _dt { get; set; }
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_PaintedSkinRecords_Detail2(DataTable dt)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _dt = dt;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_PaintedSkinRecords_Detail2_Load(object sender, EventArgs e)
        {
            #region 添加表结构
            dataGridView1.Rows.Add();
            DataGridViewRow dgvr1 = dataGridView1.Rows[0];
            dgvr1.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_1;
            dgvr1.Cells["PANIT_LEVEL"].Value = "I";
            dgvr1.Cells["ACTUAL_AREA"].Value = "0";
            dgvr1.Cells["XISHU"].Value = "97%";
            dgvr1.Cells["coefficient"].Value = "0.97";
            dgvr1.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr2 = dataGridView1.Rows[1];
            dgvr2.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_2;
            dgvr2.Cells["PANIT_LEVEL"].Value = "II";
            dgvr2.Cells["ACTUAL_AREA"].Value = "0";
            dgvr2.Cells["XISHU"].Value = "93%";
            dgvr2.Cells["coefficient"].Value = "0.93";
            dgvr2.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr3 = dataGridView1.Rows[2];
            dgvr3.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_3;
            dgvr3.Cells["PANIT_LEVEL"].Value = "III";
            dgvr3.Cells["ACTUAL_AREA"].Value = "0";
            dgvr3.Cells["XISHU"].Value = "88%";
            dgvr3.Cells["coefficient"].Value = "0.88";
            dgvr3.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr4 = dataGridView1.Rows[3];
            dgvr4.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_4;
            dgvr4.Cells["PANIT_LEVEL"].Value = "IV";
            dgvr4.Cells["ACTUAL_AREA"].Value = "0";
            dgvr4.Cells["XISHU"].Value = "83%";
            dgvr4.Cells["coefficient"].Value = "0.83";
            dgvr4.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr5 = dataGridView1.Rows[4];
            dgvr5.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_5;
            dgvr5.Cells["PANIT_LEVEL"].Value = "V";
            dgvr5.Cells["ACTUAL_AREA"].Value = "0";
            dgvr5.Cells["XISHU"].Value = "78%";
            dgvr5.Cells["coefficient"].Value = "0.78";
            dgvr5.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr6 = dataGridView1.Rows[5];
            dgvr6.Cells["level"].Value = string.Empty;
            dgvr6.Cells["PANIT_LEVEL"].Value = "I-V总数";
            dgvr6.Cells["ACTUAL_AREA"].Value = "0";
            dgvr6.Cells["XISHU"].Value = "-";
            dgvr6.Cells["coefficient"].Value = null;
            dgvr6.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr7 = dataGridView1.Rows[6];
            dgvr7.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_6;
            dgvr7.Cells["PANIT_LEVEL"].Value = "VI";
            dgvr7.Cells["ACTUAL_AREA"].Value = "0";
            dgvr7.Cells["XISHU"].Value = "73%";
            dgvr7.Cells["coefficient"].Value = "0.73";
            dgvr7.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr8 = dataGridView1.Rows[7];
            dgvr8.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_6B;
            dgvr8.Cells["PANIT_LEVEL"].Value = "六级以下";
            dgvr8.Cells["ACTUAL_AREA"].Value = "0";
            dgvr8.Cells["XISHU"].Value = "-";
            dgvr8.Cells["coefficient"].Value = null;
            dgvr8.Cells["multiple"].Value = "0";


            #endregion

            GetDateList1();


            #region I-V级数量总合计算
            float onesl = Convert.ToInt32(dataGridView1.Rows[0].Cells["ACTUAL_AREA"].Value);
            float twosl = Convert.ToInt32(dataGridView1.Rows[1].Cells["ACTUAL_AREA"].Value);
            float threesl = Convert.ToInt32(dataGridView1.Rows[2].Cells["ACTUAL_AREA"].Value);
            float foursl = Convert.ToInt32(dataGridView1.Rows[3].Cells["ACTUAL_AREA"].Value);
            float fivesl = Convert.ToInt32(dataGridView1.Rows[4].Cells["ACTUAL_AREA"].Value);
            float sumsl = onesl + twosl + threesl + foursl + fivesl;
            dataGridView1.Rows[5].Cells["ACTUAL_AREA"].Value = sumsl;
            #endregion


            #region I-V级倍数总合计算
            float onebs = Convert.ToInt32(dataGridView1.Rows[0].Cells["multiple"].Value);
            float twobs = Convert.ToInt32(dataGridView1.Rows[1].Cells["multiple"].Value);
            float threebs = Convert.ToInt32(dataGridView1.Rows[2].Cells["multiple"].Value);
            float fourbs = Convert.ToInt32(dataGridView1.Rows[3].Cells["multiple"].Value);
            float fivebs = Convert.ToInt32(dataGridView1.Rows[4].Cells["multiple"].Value);
            float sumbs = onebs + twobs + threebs + fourbs + fivebs;
            dataGridView1.Rows[5].Cells["multiple"].Value = sumbs;
            #endregion

            decimal shul = Convert.ToInt32(dataGridView1.Rows[5].Cells["ACTUAL_AREA"].Value.ToString());
            decimal beis = Convert.ToInt32(dataGridView1.Rows[5].Cells["multiple"].Value.ToString());
            string pqc;
            if (shul == 0 || beis == 0)
            {
                pqc = "0";
                lab_gjzlxs2.Text = pqc;
            }
            else
            {
                pqc = Math.Round(beis / shul * 100,2).ToString() + "%";//购进质量系数
                lab_gjzlxs2.Text = pqc;
            }
        }

        public void GetDateList1()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in _dt.Rows)
                    {
                        p.Add("PAINT_NO", dataRow["PAINT_NO"].ToString());
                    }
                }

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.SatraLeatherEvaluationBase",//类名
                                            "GetSupllyAndActual2",//方法名
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
                DataTable dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
                DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            if (dgvr.Cells["level"].Value != null
                                && dgvr.Cells["level"].Value.ToString().Equals(dr["PAINT_LEVEL"].ToString()))
                            {
                                dgvr.Cells["ACTUAL_AREA"].Value = dr["ACTUAL_AREA"].ToString();
                                if (dgvr.Cells["coefficient"].Value != null && dr["ACTUAL_AREA"].ToString() != null)
                                {
                                    dgvr.Cells["multiple"].Value = Convert.ToDecimal(dr["ACTUAL_AREA"].ToString())
                                        * Convert.ToDecimal(dgvr.Cells["coefficient"].Value);
                                }
                            }
                        }
                        i++;
                    }
                }
                if (dt1.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        if (dataGridView1.Rows[6].Cells["level"].Value != null
                            && dataGridView1.Rows[6].Cells["level"].Value.ToString().Equals(dr["PAINT_LEVEL"].ToString()))
                        {
                            dataGridView1.Rows[6].Cells["multiple"].Value = dr["MULTIPLE"].ToString();
                        }
                        i++;
                    }
                }
                if (dt2.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt2.Rows)
                    {
                        if (dataGridView1.Rows[7].Cells["level"].Value != null
                            && dataGridView1.Rows[7].Cells["level"].Value.ToString().Equals(dr["PAINT_LEVEL"].ToString()))
                        {
                            dataGridView1.Rows[7].Cells["multiple"].Value = dr["MULTIPLE"].ToString();
                        }
                        i++;
                    }
                }
                this.dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
