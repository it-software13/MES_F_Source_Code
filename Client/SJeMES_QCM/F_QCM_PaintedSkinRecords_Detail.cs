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
    public partial class F_QCM_PaintedSkinRecords_Detail : MaterialForm
    {
        public DataTable _dt { get; set; }
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_PaintedSkinRecords_Detail(DataTable dt)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _dt = dt;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_PaintedSkinRecords_Detail_Load(object sender, EventArgs e)
        {
            GetDataList();
            GetDataList1();
        }

        public void GetDataList()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dt.Rows)
                    {
                        p.Add("PAINT_NO", dr["paint_no"].ToString());
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
                DataTable data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                dataGridView1.Rows.Clear();
                if (data.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in data.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        switch (dr["PAINT_LEVEL"])
                        {
                            case "1":
                                dr["PAINT_LEVEL"] = "I";
                                break;
                            case "2":
                                dr["PAINT_LEVEL"] = "II";
                                break;
                            case "3":
                                dr["PAINT_LEVEL"] = "III";
                                break;
                            case "4":
                                dr["PAINT_LEVEL"] = "IV";
                                break;
                            case "5":
                                dr["PAINT_LEVEL"] = "V";
                                break;
                            case "6":
                                dr["PAINT_LEVEL"] = "VI";
                                break;
                            case "6B":
                                dr["PAINT_LEVEL"] = "六级以下";
                                break;

                            default:
                                break;
                        }

                        dgvr.Cells["PAINT_LEVEL"].Value = dr["PAINT_LEVEL"].ToString();
                        dgvr.Cells["SUPPLIER_AREA"].Value = dr["SUPPLIER_AREA"].ToString();
                        dgvr.Cells["ACTUAL_AREA"].Value = dr["ACTUAL_AREA"].ToString();
                        i++;
                    }
                    this.dataGridView1.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            F_QCM_PaintedSkinRecords_Detail2 form = new F_QCM_PaintedSkinRecords_Detail2(_dt);
            form.ShowDialog();
            GetDataList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList1()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                if (_dt.Rows.Count>0)
                {
                    foreach (DataRow dr in _dt.Rows)
                    {
                        p.Add("PAINT_NO", dr["paint_no"].ToString());
                    }
                }
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.SatraLeatherEvaluationBase",//类名
                                            "GetSatraListById",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));


                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        txt_vend_name.Text = dr["vend_name"].ToString();
                        txt_item_no.Text = dr["ITEM_NO"].ToString();
                        txt_item_name.Text = dr["ITEM_NAME"].ToString();
                        txt_paint_date.Text = dr["PAINT_DATE"].ToString();
                        txt_qty.Text = dr["QTY"].ToString();
                        txt_shoe_no.Text = dr["SHOE_NOS"].ToString();
                        txt_part_no.Text = dr["part_nos"].ToString();
                        txt_chk_no.Text = dr["chk_no"].ToString();
                        txt_prod_no.Text = dr["PROD_NOS"].ToString();
                        
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
