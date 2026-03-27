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
    public partial class F_QCM_Productionline_Defects_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public DataTable _dt { get; set; }
        public F_QCM_Productionline_Defects_Add(DataTable dt)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _dt = dt;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_Productionline_Defects_Add_Load(object sender, EventArgs e)
        {

        }

        private void btnNewAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_defect_no.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_defect_name.Text.Trim()))
                {
                    throw new Exception("必填项不能为空，请检查！");
                }
                if (_dt.Rows.Count > 0)
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    foreach (DataRow item in _dt.Rows)
                    {
                        p.Add("department_no", item["department_no"].ToString());
                        p.Add("department_name",item["department_name"].ToString());
                        p.Add("productionline_no",item["productionline_no"].ToString());
                        p.Add("productionline_name",item["productionline_name"].ToString());
                    }
                    p.Add("defect_no", txt_defect_no.Text);
                    p.Add("defect_name", txt_defect_name.Text);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.Quality_DepartmentBase",//类名
                                                "ProductionlineDefectsM_NewAdd",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                    else
                    {
                        MessageBox.Show("添加成功");
                        this.Close();
                    }
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
