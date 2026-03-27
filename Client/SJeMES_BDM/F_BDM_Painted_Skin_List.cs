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
    public partial class F_BDM_Painted_Skin_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string task_no = string.Empty;
        public F_BDM_Painted_Skin_List()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_BDM_Painted_Skin_List(string _task_no,string _task_state)
        {
            task_no = _task_no;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            if (_task_state=="已完成")
            {
                button2.Visible = false;
                button3.Visible = true;
            }
            else
            {
                button2.Visible = true;
                button3.Visible = false;
            }
        }

        private void F_BDM_Painted_Skin_List_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetPainted_Skin_List_HZ();
            GetPainted_Skin_List_Head();
            GetPainted_Skin_List_Staff();

            pageControl1.BindPageEvent += GetPainted_Skin_List_task_d;
            LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView2.ClearSelection();
            this.dataGridView3.ClearSelection();
        }

        /// <summary>
        /// 画皮查看进度页面画皮汇总查询
        /// </summary>
        public void GetPainted_Skin_List_HZ()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Painted_Skin",//类名
                                            "GetPainted_Skin_List_HZ",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["pl_level"].Value = dr["enum_value"].ToString();
                        dgvr.Cells["qty"].Value = dr["qty"].ToString();
                        dgvr.Cells["coefficient"].Value = dr["coefficient"].ToString() == "-" ? "-" : (Convert.ToDecimal(dr["coefficient"].ToString()) * 100) + "%";
                        dgvr.Cells["multiple"].Value = dr["multiple"].ToString()==""?"0": Math.Round(decimal.Parse(dr["multiple"].ToString()), 2).ToString();
                        i++;
                    }
                }
                dataGridView1.ClearSelection();

                var dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
                dataGridView3.Rows.Clear();
                if (dt1.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        dataGridView3.Rows.Add();
                        DataGridViewRow dgvr = dataGridView3.Rows[i];
                        dgvr.Cells["pl_level3"].Value = dr["enum_value"].ToString();
                        dgvr.Cells["qty3"].Value = dr["qty"].ToString();
                        i++;
                    }
                }
                dataGridView3.ClearSelection();

                label12.Text = dic["pecft"].ToString();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 画皮查看进度页面页面头查询
        /// </summary>
        public void GetPainted_Skin_List_Head()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Painted_Skin",//类名
                                            "GetPainted_Skin_List_Head",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    label6.Text = dt.Rows[0]["vend_name"].ToString();
                    label7.Text = dt.Rows[0]["item_no"].ToString();
                    label8.Text = dt.Rows[0]["item_name"].ToString();
                    label9.Text = dt.Rows[0]["mtl_qty"].ToString();
                    label10.Text = dt.Rows[0]["wh_date"].ToString();
                    label15.Text = dt.Rows[0]["yhp_qty"].ToString();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 画皮查看进度页面画皮记录者查询
        /// </summary>
        public void GetPainted_Skin_List_Staff()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Painted_Skin",//类名
                                            "GetPainted_Skin_List_Staff",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["staff_name"] = "全部";
                    dr["createby"] = "全部0318";
                    dt.Rows.InsertAt(dr, 0);
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "staff_name";
                    comboBox1.ValueMember = "createby";
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
            this.Close();
        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 画皮查看进度页面画皮记录查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetPainted_Skin_List_task_d(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("createby", comboBox1.SelectedValue.ToString());//操作人
                data.Add("task_no", task_no);//任务编号
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Painted_Skin",//类名
                                            "GetPainted_Skin_List_task_d",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView2.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView2.Rows.Add();
                        DataGridViewRow dgvr = dataGridView2.Rows[i];
                        dgvr.Cells["pl_level2"].Value = dr["pl_level"].ToString();
                        dgvr.Cells["qty2"].Value = dr["qty"].ToString();
                        dgvr.Cells["createby"].Value = dr["createby"].ToString();
                        dgvr.Cells["staff_name"].Value = dr["staff_name"].ToString();
                        dgvr.Cells["createdatetime"].Value = dr["wh_date"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView2.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Visible)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("是否完成画皮!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                DialogResult dr = SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                if (dr == DialogResult.OK)
                    Painted_Skin_List_Complete();
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("是否取消完成画皮!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                DialogResult dr = SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                if (dr == DialogResult.OK)
                    Painted_Skin_List_CancelComplete();
            }
        }

        /// <summary>
        /// 画皮查看进度页完成画皮
        /// </summary>
        public void Painted_Skin_List_Complete()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Painted_Skin", "Painted_Skin_List_Complete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 画皮查看进度页取消完成画皮
        /// </summary>
        public void Painted_Skin_List_CancelComplete()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Painted_Skin", "Painted_Skin_List_CancelComplete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
