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
    public partial class F_BDM_DeviceType_CP_Item : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string EQ_TYPE = string.Empty;
        public F_BDM_DeviceType_CP_Item()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
        }

        public F_BDM_DeviceType_CP_Item(string _EQ_TYPE)
        {
            EQ_TYPE = _EQ_TYPE;
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
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
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("KeyCode", textBox1.Text.Trim());
                data.Add("EQ_TYPE", EQ_TYPE);//0：校正项目；1：参数项目

                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Equipment", "GetDeviceType_CP_Item",
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
                            dgvr.Cells["code"].Value = dr["code"].ToString();//编号
                            dgvr.Cells["name"].Value = dr["name"].ToString();//名称

                            i++;
                        }
                    }
                    totalCount = int.Parse(dic["rowCount"].ToString());
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell ck = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                if (i != e.RowIndex)
                {
                    ck.Value = false;
                }
                else
                {
                    ck.Value = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool istrue = false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    istrue = (bool)dataGridView1.Rows[i].Cells["xz"].Value;
                }
                catch 
                {
                    istrue = false;
                }
                if (istrue == true)
                {
                    string code = dataGridView1.Rows[i].Cells["code"].Value.ToString();
                    string name = dataGridView1.Rows[i].Cells["name"].Value.ToString();
                    this.Name = name;
                    this.Tag = code;
                    this.Close();
                }
                else
                    this.Close();
            }
        }

        private void F_BDM_DeviceType_CP_Item_Load(object sender, EventArgs e)
        {
            //只要加载一次委托 
            pageControl1.BindPageEvent += GetData;
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
    }
}
