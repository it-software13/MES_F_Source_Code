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
    public partial class F_QCM_Viewinspectionresults_view : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Dictionary<string, object> dics;
        public F_QCM_Viewinspectionresults_view(Dictionary<string,object> dic)
        {
            InitializeComponent();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_QCM_Viewinspectionresults_view_Load(object sender, EventArgs e)
        {
            if (dics != null)
            {
                lab1.Text = dics["CHK_NO"].ToString();
                try
                {
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("CHK_NO", lab1.Text);//收料单号

                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.VMaterialinventory",//类名
                                                "CheckResultJYView",//方法名
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
                    dataGridView1.Rows.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["test_item_name"].Value = dr["test_item_name"].ToString();//检验项名称
                            dgvr.Cells["test_standard"].Value = dr["test_standard"].ToString();//检验标准
                            dgvr.Cells["determine"].Value = dr["determine"].ToString();//检验结果
                            if (dr["determine"].ToString() == "0")
                            {

                                dgvr.Cells["determine"].Style.BackColor=Color.Green;
                            }
                            else
                            {
                                dgvr.Cells["determine"].Style.BackColor = Color.Red;
                            }
                            i++;
                        }
                        GenClass.AutoSizeColumn(dataGridView1);

                    }
                    this.dataGridView1.ClearSelection();
                }
                catch (Exception ex)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }

            }
        }
    }
}
