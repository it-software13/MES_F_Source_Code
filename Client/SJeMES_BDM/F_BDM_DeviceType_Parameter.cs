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
    public partial class F_BDM_DeviceType_Parameter : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _id { get; set; }
        public F_BDM_DeviceType_Parameter(string id)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            _id = id;
        }

        private void F_BDM_DeviceType_Parameter_Load(object sender, EventArgs e)
        {
            //只要加载一次委托 
            pageControl1.BindPageEvent += GetData;
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="STAFF_NO"></param>
        public void GetData(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string eq_name = string.Empty;
                string correction_frequency = string.Empty;
                string control_type = string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("id", _id);
                data.Add("EQ_TYPE", "1");//0：校正项目；1：参数项目

                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Equipment", "GetEquipment_type_d",
                     Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    dataGridView1.Rows.Clear();
                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["唯一id"].Value = dr["唯一ID"].ToString();//行号
                            dgvr.Cells["id"].Value = dr["LINE"].ToString();//行号
                            dgvr.Cells["name"].Value = dr["ITEM_NAME"].ToString();//参数项目名称

                            i++;
                        }
                    }
                    totalCount = int.Parse(dic["rowCount"].ToString());
                    GenClass.AutoSizeColumn(dataGridView1);
                    this.splitContainer1.Visible = true;
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

        private void btn_add_Click(object sender, EventArgs e)
        {
            F_BDM_DeviceType_Parameter_Item f_BDM_DeviceType_Parameter_Item = new F_BDM_DeviceType_Parameter_Item(_id);
            f_BDM_DeviceType_Parameter_Item.ShowDialog();
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                switch (dataGridView1.Columns[e.ColumnIndex].Name)
                {
                    case "Edit":
                        string did = dataGridView1.Rows[e.RowIndex].Cells["唯一id"].Value.ToString();
                        DeleteEquipment_type_d(did);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 删除参数内容
        /// </summary>
        /// <param name="did"></param>
        public void DeleteEquipment_type_d(string did)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("M_ID", _id);//主表关联
            data.Add("D_ID", did);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_BDMAPI", "SJ_BDMAPI.BDM_Equipment", "DeleteEquipment_type_d",
                 Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Successfully Deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
                pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
                pageControl1.SetPage();
            }
        }
    }
}
