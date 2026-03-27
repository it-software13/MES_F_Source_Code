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
using static SJeMES_IQC.F_IQC_VWarehouse_Main;

namespace SJeMES_AQL
{
    public partial class F_AQL_ShoeMaterial_Composition : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        DataTable _dt = new DataTable();
        public F_AQL_ShoeMaterial_Composition()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_AQL_ShoeMaterial_Composition_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        public void GetData()
        {
            try
            {
                Dictionary<string, object> p1 = new Dictionary<string, object>();
                p1.Add("art", txt_art.Text);
                string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_ShoeMaterial_Composition",//类名
                                        "GetMaintenanceOfShoeMaterialComposition",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p1));
                ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1);
                if (!ret1.IsSuccess)
                {
                    throw new Exception(ret1.ErrMsg);
                }

                dgvData.Rows.Clear();
                if (!string.IsNullOrEmpty(ret1.RetData))
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret1.RetData);
                    _dt = dt;
                    int i = 0;
                    foreach (DataRow item in dt.Rows)
                    {
                        dgvData.Rows.Add();
                        DataGridViewRow dgvr = dgvData.Rows[i];
                        dgvr.Cells["ART编码"].Value = item["MATNR"].ToString();
                        dgvr.Cells["描述"].Value = item["MAKTX"].ToString();
                        dgvr.Cells["鞋帮"].Value = item["ZCOL1_NM"].ToString();
                        dgvr.Cells["Outsole"].Value = item["ZCOL2_NM"].ToString();
                        dgvr.Cells["内里"].Value = item["ZCOL3"].ToString();
                        dgvr.Cells["鞋舌标"].Value = item["ZCOL4_NM"].ToString();
                        dgvr.Cells["硫化否"].Value = item["ZCOL5_NM"].ToString();
                        dgvr.Cells["Inlay_Solez"].Value = item["ZCOL6"].ToString();
                        dgvr.Cells["创建人"].Value = item["ERNAM"].ToString();
                        dgvr.Cells["创建日期"].Value = item["ERSDA"].ToString();
                        dgvr.Cells["最后修改人"].Value = item["AENAM"].ToString();
                        dgvr.Cells["最后修改日期"].Value = item["LAEDA"].ToString();
                        dgvr.Cells["最后修改时间"].Value = item["LAST_CHANGED_TIME"].ToString();
                        string ICONNAME_BE = item["ICONNAME_BE"].ToString();
                        string ICONNAME_BE_RES = "";
                        switch (ICONNAME_BE)
                        {
                            case "@08@":
                                //ICONNAME_BE_RES = "绿灯";
                                ICONNAME_BE_RES = "Green_Light";
                                break;
                            case "@09@":
                                //ICONNAME_BE_RES = "黄灯";
                                ICONNAME_BE_RES = "Yellow_Light";
                                break;
                            case "@EB@":
                                //ICONNAME_BE_RES = "熄灯";
                                ICONNAME_BE_RES = "Lights_up";
                                break;
                            default:
                                break;
                        }
                        dgvr.Cells["开发量产单维护状态"].Value = ICONNAME_BE_RES;
                        string ICONNAME_K = item["ICONNAME_K"].ToString();
                        string ICONNAME_K_RES = "";
                        switch (ICONNAME_K)
                        {
                            case "@08@":
                                //ICONNAME_K_RES = "绿灯";
                                ICONNAME_K_RES = "Green_Light";
                                break;
                            case "@09@":
                                //ICONNAME_K_RES = "黄灯";
                                ICONNAME_K_RES = "Yellow_Light";
                                break;
                            case "@EB@":
                                //ICONNAME_K_RES = "熄灯";
                                ICONNAME_K_RES = "Lights_up";
                                break;
                            default:
                                break;
                        }
                        dgvr.Cells["开发生效"].Value = ICONNAME_K_RES;
                        string ICONNAME_B = item["ICONNAME_B"].ToString();
                        string ICONNAME_B_RES = "";
                        switch (ICONNAME_B)
                        {
                            case "@08@":
                                //ICONNAME_B_RES = "绿灯";
                                ICONNAME_B_RES = "Green_Light";
                                break;
                            case "@09@":
                                //ICONNAME_B_RES = "黄灯";
                                ICONNAME_B_RES = "Yellow_Light";
                                break;
                            case "@EB@":
                                //ICONNAME_B_RES = "熄灯";
                                ICONNAME_B_RES = "Lights_up";
                                break;
                            default:
                                break;
                        }
                        dgvr.Cells["报价生效"].Value = ICONNAME_B_RES;
                        string ICONNAME_Y = item["ICONNAME_Y"].ToString();
                        string ICONNAME_Y_RES = "";
                        switch (ICONNAME_Y)
                        {
                            case "@08@":
                                //ICONNAME_Y_RES = "绿灯";
                                ICONNAME_Y_RES = "Green_Light";
                                break;
                            case "@09@":
                                //ICONNAME_Y_RES = "黄灯";
                                ICONNAME_Y_RES = "Yellow_Light";
                                break;
                            case "@EB@":
                                //ICONNAME_Y_RES = "熄灯";
                                ICONNAME_Y_RES = "Lights_up";
                                break;
                            default:
                                break;
                        }
                        dgvr.Cells["开发复核"].Value = ICONNAME_Y_RES;
                        dgvr.Cells["数据统计"].Value = "No_Value";//无取值
                        dgvr.Cells["数量"].Value = "No_Value";//无取值

                        i++;
                    }
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void txt_art_DoubleClick(object sender, EventArgs e)
        {
            F_AQL_ShoeMaterial_Composition_Art frm = new F_AQL_ShoeMaterial_Composition_Art(txt_art.Text);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            if (frm.selectlist.Count > 0)
            {
                string poorder = "";
                foreach (var item in frm.selectlist)
                {
                    poorder += item["poorder"].ToString() + ",";
                }
                txt_art.Text = poorder.Trim(',');
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dgvData.Columns[e.ColumnIndex].Name == "操作")
                {
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        DataRow curr_row = _dt.Rows[e.RowIndex];
                        DataTable curr_dt = (DataTable)curr_row["ITEM"];

                        F_AQL_ShoeMaterial_Composition_List frm = new F_AQL_ShoeMaterial_Composition_List(curr_dt);
                        frm.Show();
                    }
                }
            }
        }
    }
}
