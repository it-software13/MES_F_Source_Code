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

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_OutIn : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Ex_OutIn()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void txt_qs_task_no_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", txt_qs_task_no.Text.Trim());
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetTaskInfo",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                var result= Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());

                var info= Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result["info"].ToString());

                var qs_dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["qslist"].ToString());

                int qs_qty = 0;
                dgv_qs.Rows.Clear();
                foreach (DataRow dr in qs_dt.Rows)
                {
                   int i= dgv_qs.Rows.Add();
                    dgv_qs.Rows[i].Cells[1].Value = dr["SC_QTY"].ToString();
                    dgv_qs.Rows[i].Cells[1].ReadOnly = true;
                    dgv_qs.Rows[i].Cells[2].Value = dr["QS_QTY"].ToString();
                    dgv_qs.Rows[i].Cells[2].ReadOnly = true;
                    dgv_qs.Rows[i].Cells[3].Value = dr["QS_STAFF_NAME"].ToString();
                    dgv_qs.Rows[i].Cells[3].ReadOnly = true;
                    dgv_qs.Rows[i].Cells[4].Value = dr["QS_TIME"].ToString();
                    dgv_qs.Rows[i].Cells[4].ReadOnly = true;
                    dgv_qs.Rows[i].Cells[5].Value = dr["ID"].ToString();
                    int qty = 0;
                    int.TryParse(dr["QS_QTY"].ToString(), out qty);
                    qs_qty += qty;
                }

                txt_qs_total_qty.Text = info["SEND_TEST_QTY"].ToString();
                txt_qs_qs_qty.Text = qs_qty.ToString();
                txt_qs_ast_qs_qty.Text = (int.Parse(info["SEND_TEST_QTY"].ToString()) - qs_qty).ToString();
                int newindex = dgv_qs.Rows.Add();
                dgv_qs.Rows[newindex].Cells[3].ReadOnly = true;
                dgv_qs.Rows[newindex].Cells[4].ReadOnly = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool bl = false;
            DataGridViewRow newdr=null;
            foreach (DataGridViewRow item in dgv_qs.Rows)
            {
                if(item.Cells[5].Value==null)
                {
                    bl = true;
                    newdr = item;
                }
            }

            if(!bl)
            {
                MessageBox.Show("Please scan the QR code of the inspection form to add sign-in data");
                return;
            }

            //int sc_qty = 0;
            //int.TryParse(newdr.Cells[1].Value==null?"":newdr.Cells[1].Value.ToString(), out sc_qty);

            //if(sc_qty<=0)
            //{
            //    MessageBox.Show("请输入送测数量");
            //    newdr.Cells[1].Selected=true;
            //    return;
            //}

            int qs_qty = 0;
            int.TryParse(newdr.Cells[2].Value == null ? "" : newdr.Cells[2].Value.ToString(), out qs_qty);

            if (qs_qty <= 0)
            {
                MessageBox.Show("Please enter the quantity to sign for");
                newdr.Cells[2].Selected = true;
                return;
            }
            else if(qs_qty>int.Parse(txt_qs_ast_qs_qty.Text.Trim()))
            {
                MessageBox.Show("The signed quantity cannot be > the acceptable quantity");
                newdr.Cells[2].Selected = true;
                return;
            }
            if (string.IsNullOrEmpty(txt_qs_staff_no.Text.Trim()))
            {
                MessageBox.Show("Please scan and sign for the employee number");
                txt_qs_staff_code.Focus();
                return;
            }


            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("task_no", txt_qs_task_no.Text.Trim());
            data.Add("sc_qty", qs_qty);
            data.Add("qs_qty", qs_qty);
            data.Add("qs_staff_no", txt_qs_staff_no.Text.Trim());
            data.Add("qs_staff_name", txt_qs_staff_name.Text.Trim());
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "SaveQS",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
            }
            string qs_id = ret.RetData;
            newdr.Cells["qs_id"].Value = qs_id;
            newdr.Cells[3].Value = txt_qs_staff_name.Text;
            newdr.Cells[4].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            newdr.ReadOnly = true;
            txt_qs_qs_qty.Text = (int.Parse(txt_qs_qs_qty.Text.Trim()) + qs_qty).ToString();
            txt_qs_ast_qs_qty.Text= (int.Parse(txt_qs_total_qty.Text.Trim())- int.Parse(txt_qs_qs_qty.Text.Trim())).ToString();

            MessageBox.Show("Signed successfully");
        }

        private void txt_qs_staff_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetStaffInfo(txt_qs_staff_code.Text.Trim());
                if (result != null)
                {
                    txt_qs_staff_no.Text = result["STAFF_NO"].ToString();
                    txt_qs_staff_name.Text = result["STAFF_NAME"].ToString();
                    txt_qs_staff_department.Text = result["DEPARTMENT_NAME"].ToString();
                }
            }
        }

        public Dictionary<string, object> GetStaffInfo(string staff_code)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("qrcode", staff_code);
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetStaffInfo",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return null;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_qz_task_no_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", txt_qz_task_no.Text.Trim());
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetTaskInfo",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());

                var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result["info"].ToString());

                var qz_dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["qzlist"].ToString());
                var qs_dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["qslist"].ToString());

                int total_qs_qty = 0;
                foreach (DataRow dr in qs_dt.Rows)
                {
                    int qty = 0;
                    int.TryParse(dr["QS_QTY"].ToString(), out qty);
                    total_qs_qty += qty;
                }

                int qz_qty = 0;
                dgv_qz.Rows.Clear();
                foreach (DataRow dr in qz_dt.Rows)
                {
                    int i = dgv_qz.Rows.Add();
                    dgv_qz.Rows[i].Cells[1].Value = dr["SC_QTY"].ToString();
                    dgv_qz.Rows[i].Cells[1].ReadOnly = true;
                    dgv_qz.Rows[i].Cells[2].Value = dr["QZ_QTY"].ToString();
                    dgv_qz.Rows[i].Cells[2].ReadOnly = true;
                    dgv_qz.Rows[i].Cells[3].Value = dr["QZ_STAFF_NAME"].ToString();
                    dgv_qz.Rows[i].Cells[3].ReadOnly = true;
                    dgv_qz.Rows[i].Cells[4].Value = dr["QZ_TIME"].ToString();
                    dgv_qz.Rows[i].Cells[4].ReadOnly = true;
                    dgv_qz.Rows[i].Cells[5].Value = dr["ID"].ToString();
                    int qty = 0;
                    int.TryParse(dr["QZ_QTY"].ToString(), out qty);
                    qz_qty += qty;
                }

                txt_qz_total_qty.Text = total_qs_qty.ToString();
                txt_qz_qz_qty.Text = qz_qty.ToString();
                txt_qz_last_qz_qty.Text = (total_qs_qty - qz_qty).ToString();
                int newindex = dgv_qz.Rows.Add();
                dgv_qz.Rows[newindex].Cells[3].ReadOnly = true;
                dgv_qz.Rows[newindex].Cells[4].ReadOnly = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool bl = false;
            DataGridViewRow newdr = null;
            foreach (DataGridViewRow item in dgv_qz.Rows)
            {
                if (item.Cells[5].Value == null)
                {
                    bl = true;
                    newdr = item;
                }
            }

            if (!bl)
            {
                MessageBox.Show("Please scan the QR code of the inspection form to add and remove data");
                return;
            }

            //int sc_qty = 0;
            //int.TryParse(newdr.Cells[1].Value == null ? "" : newdr.Cells[1].Value.ToString(), out sc_qty);

            //if (sc_qty <= 0)
            //{
            //    MessageBox.Show("请输入送测数量");
            //    newdr.Cells[1].Selected = true;
            //    return;
            //}

            int qz_qty = 0;
            int.TryParse(newdr.Cells[2].Value == null ? "" : newdr.Cells[2].Value.ToString(), out qz_qty);

            if (qz_qty <= 0)
            {
                MessageBox.Show("Please enter the quantity to take");
                newdr.Cells[2].Selected = true;
                return;
            }
            else if (qz_qty > int.Parse(txt_qz_last_qz_qty.Text.Trim()))
            {
                MessageBox.Show("The quantity to be withdrawn cannot be > the quantity that can be withdrawn");
                newdr.Cells[2].Selected = true;
                return;
            }
            if (string.IsNullOrEmpty(txt_qz_staff_no.Text.Trim()))
            {
                MessageBox.Show("Please scan and take the employee number");
                txt_qs_staff_code.Focus();
                return;
            }


            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("task_no", txt_qz_task_no.Text.Trim());
            data.Add("sc_qty", qz_qty);
            data.Add("qz_qty", qz_qty);
            data.Add("qz_staff_no", txt_qz_staff_no.Text.Trim());
            data.Add("qz_staff_name", txt_qz_staff_name.Text.Trim());
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "SaveQZ",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
            }
            string qz_id = ret.RetData;
            newdr.Cells["qz_id"].Value = qz_id;
            newdr.Cells[3].Value = txt_qz_staff_name.Text;
            newdr.Cells[4].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            newdr.ReadOnly = true;
            txt_qz_qz_qty.Text = (int.Parse(txt_qz_qz_qty.Text.Trim()) + qz_qty).ToString();
            txt_qz_last_qz_qty.Text = (int.Parse(txt_qz_total_qty.Text.Trim()) - int.Parse(txt_qz_qz_qty.Text.Trim())).ToString();
            MessageBox.Show("Take away successfully");
        }

        private void dgv_qz_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>-1)
            {
                if(e.ColumnIndex==0)
                {
                    if(dgv_qz.Rows[e.RowIndex].Cells["qz_id"].Value==null)
                    {
                        dgv_qz.Rows.Remove(dgv_qz.Rows[e.RowIndex]);
                    }
                }
            }
        }

        private void dgv_qs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0)
                {
                    if (dgv_qs.Rows[e.RowIndex].Cells["qs_id"].Value == null)
                    {
                        dgv_qs.Rows.Remove(dgv_qs.Rows[e.RowIndex]);
                    }
                }
            }
        }

        private void txt_qz_staff_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetStaffInfo(txt_qz_staff_code.Text.Trim());
                if (result != null)
                {
                    txt_qz_staff_no.Text = result["STAFF_NO"].ToString();
                    txt_qz_staff_name.Text = result["STAFF_NAME"].ToString();
                    txt_qz_staff_department.Text = result["DEPARTMENT_NAME"].ToString();
                }
            }
        }

        private void txt_th_qrcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (string.IsNullOrEmpty(txt_th_staff_no.Text.Trim()))
                {
                    MessageBox.Show("Please scan staff");
                    return;
                }

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", txt_th_qrcode.Text.Trim());
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetTaskInfo",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }

                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("Are you sure to return the delivery order?", "Prompt", messButton);

                if (dr == DialogResult.Cancel)//如果点击“取消”按钮
                {
                    return;
                }

                Dictionary<string, object> data1 = new Dictionary<string, object>();
                data1.Add("task_no", txt_th_qrcode.Text.Trim());
                //键值对传值
                string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "DeleteTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data1));

                ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (ret.IsSuccess)
                {
                    MessageBox.Show("returned successfully");
                    txt_th_qrcode.Text = "";
                }

            }
        }

        private void txt_th_staff_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetStaffInfo(txt_th_staff_code.Text.Trim());
                if (result != null)
                {
                    txt_th_staff_no.Text = result["STAFF_NO"].ToString();
                    txt_th_staff_name.Text = result["STAFF_NAME"].ToString();
                    txt_th_staff_department.Text = result["DEPARTMENT_NAME"].ToString();
                }
            }
        }
    }
}
