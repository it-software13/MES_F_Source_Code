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
    public partial class F_QCM_SatraLeatherEvaluationReport : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public DataTable _dt { get; set; }
        public F_QCM_SatraLeatherEvaluationReport(DataTable dt)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _dt = dt;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_SatraLeatherEvaluationReport_Load(object sender, EventArgs e)
        {
            #region 添加表结构
            dataGridView1.Rows.Add();
            DataGridViewRow dgvr1 = dataGridView1.Rows[0];
            dgvr1.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_1;
            dgvr1.Cells["PANIT_LEVEL1"].Value = "I";
            dgvr1.Cells["ACTUAL_AREA1"].Value = "0";
            dgvr1.Cells["XISHU"].Value = "97%";
            dgvr1.Cells["coefficient"].Value = "0.97";
            dgvr1.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr2 = dataGridView1.Rows[1];
            dgvr2.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_2;
            dgvr2.Cells["PANIT_LEVEL1"].Value = "II";
            dgvr2.Cells["ACTUAL_AREA1"].Value = "0";
            dgvr2.Cells["XISHU"].Value = "93%";
            dgvr2.Cells["coefficient"].Value = "0.93";
            dgvr2.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr3 = dataGridView1.Rows[2];
            dgvr3.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_3;
            dgvr3.Cells["PANIT_LEVEL1"].Value = "III";
            dgvr3.Cells["ACTUAL_AREA1"].Value = "0";
            dgvr3.Cells["XISHU"].Value = "88%";
            dgvr3.Cells["coefficient"].Value = "0.88";
            dgvr3.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr4 = dataGridView1.Rows[3];
            dgvr4.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_4;
            dgvr4.Cells["PANIT_LEVEL1"].Value = "IV";
            dgvr4.Cells["ACTUAL_AREA1"].Value = "0";
            dgvr4.Cells["XISHU"].Value = "83%";
            dgvr4.Cells["coefficient"].Value = "0.83";
            dgvr4.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr5 = dataGridView1.Rows[4];
            dgvr5.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_5;
            dgvr5.Cells["PANIT_LEVEL1"].Value = "V";
            dgvr5.Cells["ACTUAL_AREA1"].Value = "0";
            dgvr5.Cells["XISHU"].Value = "78%";
            dgvr5.Cells["coefficient"].Value = "0.78";
            dgvr5.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr6 = dataGridView1.Rows[5];
            dgvr6.Cells["level"].Value = string.Empty;
            dgvr6.Cells["PANIT_LEVEL1"].Value = "I-V总数";
            dgvr6.Cells["ACTUAL_AREA1"].Value = "0";
            dgvr6.Cells["XISHU"].Value = "-";
            dgvr6.Cells["coefficient"].Value = null;
            dgvr6.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr7 = dataGridView1.Rows[6];
            dgvr7.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_6;
            dgvr7.Cells["PANIT_LEVEL1"].Value = "VI";
            dgvr7.Cells["ACTUAL_AREA1"].Value = "0";
            dgvr7.Cells["XISHU"].Value = "73%";
            dgvr7.Cells["coefficient"].Value = "0.73";
            dgvr7.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr8 = dataGridView1.Rows[7];
            dgvr8.Cells["level"].Value = enum_paintedskin_level.enum_barcode_print_type_6B;
            dgvr8.Cells["PANIT_LEVEL1"].Value = "六级以下";
            dgvr8.Cells["ACTUAL_AREA1"].Value = "0";
            dgvr8.Cells["XISHU"].Value = "-";
            dgvr8.Cells["coefficient"].Value = null;
            dgvr8.Cells["multiple"].Value = "0";

            dataGridView1.Rows.Add();
            DataGridViewRow dgvr9 = dataGridView1.Rows[8];
            dgvr9.Cells["level"].Value = string.Empty;
            dgvr9.Cells["PANIT_LEVEL1"].Value = "总数";
            dgvr9.Cells["ACTUAL_AREA1"].Value = "0";
            dgvr9.Cells["XISHU"].Value = "-";
            dgvr9.Cells["coefficient"].Value = null;
            dgvr9.Cells["multiple"].Value = "0";
            #endregion
            GetDateList1();
            GetDataList2();
            GetDataList3();
            if (_dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in _dt.Rows)
                {
                    txt_item_no.Text = dr["item_no"].ToString();
                    txt_item_name.Text = dr["item_name"].ToString();
                    txt_actual_rate.Text = dr["actual_rate"].ToString();
                    txt_qty.Text = dr["qty"].ToString();
                    txt_date.Text = dr["date"].ToString();
                    txt_vend_name.Text = dr["vend"].ToString();
                    txt_item_type_name.Text = dr["itemtype"].ToString();
                    txt_createby.Text = dr["createby"].ToString();
                    txt_createby2.Text = dr["paint_no"].ToString();
                    txt_avguse.Text = dr["AVERAGE_USE_RATE"].ToString();
                    txt_mjcy.Text = dr["DIFFERENCE_COEFFICIENT"].ToString();
                    txt_assess.Text = dr["ASSESSMENT"].ToString();
                    i++;
                }
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
                                dgvr.Cells["ACTUAL_AREA1"].Value = dr["ACTUAL_AREA"].ToString();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDataList2()
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
                                            "GetSupllyAndActualSUM",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                dataGridView2.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView2.Rows.Add();
                        DataGridViewRow dgvr = dataGridView2.Rows[i];

                        dgvr.Cells["SUPPLIER_AREA"].Value = dr["SUPPLIER_AREA"].ToString();
                        dgvr.Cells["ACTUAL_AREA"].Value = dr["ACTUAL_AREA"].ToString();
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDataList3()
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
                                            "GetSupllyAndActual",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                dataGridView3.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView3.Rows.Add();
                        DataGridViewRow dgvr = dataGridView3.Rows[i];
                        if (dr["PAINT_LEVEL"].ToString() == "1")
                        {
                            dr["PAINT_LEVEL"] = "I";
                        }
                        if (dr["PAINT_LEVEL"].ToString() == "2")
                        {
                            dr["PAINT_LEVEL"] = "II";
                        }
                        if (dr["PAINT_LEVEL"].ToString() == "3")
                        {
                            dr["PAINT_LEVEL"] = "III";
                        }
                        if (dr["PAINT_LEVEL"].ToString() == "4")
                        {
                            dr["PAINT_LEVEL"] = "IV";
                        }
                        if (dr["PAINT_LEVEL"].ToString() == "5")
                        {
                            dr["PAINT_LEVEL"] = "V";
                        }
                        if (dr["PAINT_LEVEL"].ToString() == "6")
                        {
                            dr["PAINT_LEVEL"] = "VI";
                        }
                        if (dr["PAINT_LEVEL"].ToString() == "6B")
                        {
                            dr["PAINT_LEVEL"] = "六级以下";
                        }

                        dgvr.Cells["PAINT_LEVEL"].Value = dr["PAINT_LEVEL"].ToString();
                        dgvr.Cells["SUPPLIER_AREA2"].Value = dr["SUPPLIER_AREA"].ToString();
                        dgvr.Cells["ACTUAL_AREA2"].Value = dr["ACTUAL_AREA"].ToString();
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_calculate_Click(object sender, EventArgs e)
        {
            #region I-V级数量总合计算
            float onesl = Convert.ToInt32(dataGridView1.Rows[0].Cells["ACTUAL_AREA1"].Value);
            float twosl = Convert.ToInt32(dataGridView1.Rows[1].Cells["ACTUAL_AREA1"].Value);
            float threesl = Convert.ToInt32(dataGridView1.Rows[2].Cells["ACTUAL_AREA1"].Value);
            float foursl = Convert.ToInt32(dataGridView1.Rows[3].Cells["ACTUAL_AREA1"].Value);
            float fivesl = Convert.ToInt32(dataGridView1.Rows[4].Cells["ACTUAL_AREA1"].Value);
            float sumsl = onesl + twosl + threesl + foursl + fivesl;
            dataGridView1.Rows[5].Cells["ACTUAL_AREA1"].Value = sumsl;
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


            #region 数量总和
            var VI = dataGridView1.Rows[6].Cells["ACTUAL_AREA1"].Value;
            var VI2 = dataGridView1.Rows[7].Cells["ACTUAL_AREA1"].Value;
            if (VI == "")
            {
                VI = 0;
            }
            if (VI2 == "")
            {
                VI2 = 0;
            }
            float zonghe = sumsl + Convert.ToSingle(VI) + Convert.ToSingle(VI2);
            #endregion

            #region 倍数总和
            var bs = dataGridView1.Rows[6].Cells["multiple"].Value;
            var bs2 = dataGridView1.Rows[7].Cells["multiple"].Value;
            if (bs == "")
            {
                bs = 0;
            }
            if (bs2 == "")
            {
                bs2 = 0;
            }
            float bszh = sumbs + Convert.ToSingle(bs) + Convert.ToSingle(bs2);
            #endregion


            dataGridView1.Rows[8].Cells["ACTUAL_AREA1"].Value = zonghe;
            dataGridView1.Rows[8].Cells["multiple"].Value = bszh;

            float shul = Convert.ToInt32(dataGridView1.Rows[5].Cells["ACTUAL_AREA1"].Value.ToString());
            float beis = Convert.ToInt32(dataGridView1.Rows[5].Cells["multiple"].Value.ToString());
            string pqc;
            if (shul==0 || beis==0)
            {
                pqc = "0";
                txt_gjzlxs.Text = pqc;
            }
            else
            {
                pqc = (beis / shul * 100).ToString() + "%";//购进质量系数
                txt_gjzlxs.Text = pqc;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "ACTUAL_AREA1" && (dataGridView1.Rows[e.RowIndex].Index == 6 || dataGridView1.Rows[e.RowIndex].Index == 7))
                {
                    txt_shul.Text = dataGridView1.CurrentCell.Value.ToString(); //对textbox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    txt_shul.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    txt_shul.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "multiple" && (dataGridView1.Rows[e.RowIndex].Index == 6 || dataGridView1.Rows[e.RowIndex].Index == 7))
                {
                    txt_beishu.Text = dataGridView1.CurrentCell.Value.ToString(); //对textbox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    txt_beishu.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    txt_beishu.Visible = true;
                }
                else
                {
                    txt_shul.Visible = false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }



        private void btn_Update_Click(object sender, EventArgs e)
        {
            string PAINT_NO = string.Empty;
            try
            {
                if (_dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in _dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        PAINT_NO = dr["paint_no"].ToString();
                        i++;
                    }
                }

                float shul = Convert.ToInt32(dataGridView1.Rows[5].Cells["ACTUAL_AREA1"].Value.ToString());
                float beis = Convert.ToInt32(dataGridView1.Rows[5].Cells["multiple"].Value.ToString());
                string PURCHASE_COEFFICIENT;
                if (shul==0)
                {
                    PURCHASE_COEFFICIENT = "0";
                }
                else
                {
                    PURCHASE_COEFFICIENT = (beis / shul * 100).ToString() + "%";//购进质量系数
                }
                string AVERAGE_USE_RATE = txt_avguse.Text;
                string DIFFERENCE_COEFFICIENT = txt_mjcy.Text;
                string ASSESSMENT = txt_assess.Text;

                string level = dataGridView1.Rows[6].Cells["level"].Value.ToString();
                string level2 = dataGridView1.Rows[7].Cells["level"].Value.ToString();
                string VI = dataGridView1.Rows[6].Cells["ACTUAL_AREA1"].Value.ToString();
                string VI2 = dataGridView1.Rows[7].Cells["ACTUAL_AREA1"].Value.ToString();
                string M = dataGridView1.Rows[6].Cells["multiple"].Value.ToString();
                string M2 = dataGridView1.Rows[7].Cells["multiple"].Value.ToString();


                if (VI=="")
                {
                    VI = "0";
                }
                if (VI2 == "")
                {
                    VI2 = "0";
                }
                if (M=="")
                {
                    M = "0";
                }
                if (M2 == "")
                {
                    M2 = "0";
                }





                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("PAINT_NO", PAINT_NO);
                p.Add("PURCHASE_COEFFICIENT", PURCHASE_COEFFICIENT);
                p.Add("AVERAGE_USE_RATE", AVERAGE_USE_RATE);
                p.Add("DIFFERENCE_COEFFICIENT", DIFFERENCE_COEFFICIENT);
                p.Add("ASSESSMENT", ASSESSMENT);


                p.Add("level", level);
                p.Add("level2", level2);
                p.Add("VI", VI);
                p.Add("VI2", VI2);
                p.Add("M", M);
                p.Add("M2", M2);


                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                            Program.Client.APIURL,
                            "SJ_QCMAPI",//类库名
                            "SJ_QCMAPI.SatraLeatherEvaluationBase",//类名
                            "UpdateList",//方法名
                            Program.Client.UserToken,//token
                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                else
                {
                    MessageBox.Show("保存成功！");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt1_Validated(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = txt_shul.Text.ToString();
        }
        private void txt2_Validated(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = txt_beishu.Text.ToString();
        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_shul.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_shul.Text, out oldf);
                    b2 = float.TryParse(txt_shul.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void txt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_beishu.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_beishu.Text, out oldf);
                    b2 = float.TryParse(txt_beishu.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void txt_mjcy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_beishu.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_beishu.Text, out oldf);
                    b2 = float.TryParse(txt_beishu.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void txt_avguse_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_beishu.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_beishu.Text, out oldf);
                    b2 = float.TryParse(txt_beishu.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }
    }
}
