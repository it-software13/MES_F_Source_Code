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
    public partial class BDM_Chemicalkanban : MaterialForm
    {
       
        private readonly MaterialSkinManager materialSkinManager;
        public BDM_Chemicalkanban()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public void LoadPage()
        {
            pageControl1.PageSize = 25;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void BDM_Chemicalkanban_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(uiDataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            pageControl1.BindPageEvent += GetMain_List;
            LoadPage();
        }
        public string GetDateListApi(int pageSize, int pageIndex)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("container_no", textBoxEx1.Text);//容器代号
            data.Add("chemical_name", textBoxEx2.Text);//化学品名称
            data.Add("pageSize", pageSize);
            data.Add("pageIndex", pageIndex);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.BDM_Chemicalkanban",//类名
                                        "Chemicalkanban_Main_getList",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            return retdata;
        }
        public void GetMain_List(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                string retdata = GetDateListApi(pageSize, pageIndex);

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                uiDataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        uiDataGridView1.Rows.Add();
                        DataGridViewRow dgvr = uiDataGridView1.Rows[i];
                        dgvr.Cells["department_code"].Value = dr["department_code"].ToString();
                        dgvr.Cells["department_name"].Value = dr["department_name"].ToString();//产线
                        dgvr.Cells["container_no"].Value = dr["container_no"].ToString();//容器编号

                        dgvr.Cells["chemical_no"].Value = dr["chemical_no"].ToString();
                        dgvr.Cells["chemical_name"].Value = dr["chemical_name"].ToString();//化学品

                        dgvr.Cells["medicament_name"].Value = dr["medicament_name"].ToString();//药剂名称

                        dgvr.Cells["reagent_proportion"].Value = dr["reagent_proportion"].ToString();//药剂比例

                        dgvr.Cells["corresponding_humidity"].Value = dr["corresponding_humidity"].ToString();//对应温度
                        dgvr.Cells["g_mixing_time"].Value = dr["g_mixing_time"].ToString();//调胶时间
                        dgvr.Cells["autime"].Value = dr["autime"].ToString();//加胶时间
                        dgvr.Cells["effective_time"].Value = dr["effective_time"].ToString();//有效时间
                        if (!string.IsNullOrWhiteSpace(dr["g_mixing_time"].ToString()))
                        {
                           DateTime dtime= Convert.ToDateTime(dr["g_mixing_time"].ToString()).AddHours(Convert.ToDouble(dr["effective_time"].ToString()));
                            string time = dtime.ToString("yyyy-MM-dd HH:mm:ss");//到期时间
                            dgvr.Cells["effective_time2"].Value = time;
                            if (!string.IsNullOrEmpty(time))
                            {
                                DateTime dd = DateTime.Now;
                                int Num = DateTime.Compare(dd, dtime);
                                int Num1 = DateTime.Compare(dd.AddMinutes(30),dtime);
                                if (Num > 0)
                                {
                                    dgvr.Cells["effective_time2"].Style.BackColor = Color.Red;
                                }
                                if (dd < dtime)
                                {
                                    if (dtime < dd.AddMinutes(30))
                                    {
                                        dgvr.Cells["effective_time2"].Style.BackColor = Color.Yellow;
                                    }
                                }
                                if (dd.AddMinutes(30) < dtime)
                                {
                                    dgvr.Cells["effective_time2"].Style.BackColor = Color.Green;
                                }

                            }
                        }
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
              
                uiDataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string retdata = GetDateListApi(10000, 1);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {

                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dts = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dts.Rows.Count > 0)
                {

                    foreach (DataRow row in dts.Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["g_mixing_time"].ToString()))
                        {
                            DateTime dtime = Convert.ToDateTime(row["g_mixing_time"].ToString()).AddHours(Convert.ToDouble(row["effective_time"].ToString()));
                            string time = dtime.ToString("yyyy-MM-dd HH:mm:ss");//到期时间
                            row["effective_time2"] = time;
                           
                        }
                    }
                    Dictionary<string, string> Execldic = new Dictionary<string, string>();
                    Execldic.Add("DEPARTMENT_CODE", "产线编号");
                    Execldic.Add("DEPARTMENT_NAME", "产线名称");
                    Execldic.Add("CONTAINER_NO", "容器编号");
                    Execldic.Add("CHEMICAL_NO", "化学品代号");
                    Execldic.Add("CHEMICAL_NAME", "化学品名称");
                    Execldic.Add("MEDICAMENT_NAME", "药剂名称");
                    Execldic.Add("REAGENT_PROPORTION", "药剂比例");
                    Execldic.Add("CORRESPONDING_HUMIDITY", "对应湿度");
                    Execldic.Add("G_MIXING_TIME", "调胶时间");
                    Execldic.Add("AUTIME", "加胶时间");
                    Execldic.Add("EFFECTIVE_TIME", "有效时间");
                    Execldic.Add("EFFECTIVE_TIME2", "到期时间");
                    ExeclHelper.ExportToTrueExcel(dts, Execldic, "化学看板列表列表");

                }
                else
                {
                    MessageBox.Show("暂无数据导出，请检查是否操作正确");
                    return;
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
