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
    public partial class F_QCM_Productionline_Defects_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _defect_no { get; set; }
        public string _defect_name { get; set; }
        public F_QCM_Productionline_Defects_Edit(string defect_no,string defect_name)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _defect_no = defect_no;
            _defect_name = defect_name;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_Productionline_Defects_Edit2_Load(object sender, EventArgs e)
        {
            txt_defect_no.Text = _defect_no;
            txt_defect_name.Text = _defect_name;
        }

        private void btnNewAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();


                p.Add("defect_no", txt_defect_no.Text);
                p.Add("defect_name", txt_defect_name.Text);
                p.Add("Operation", "Modify");
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Quality_DepartmentBase", "ProductionlineDefectsM_Operation", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                else
                {
                    MessageBox.Show("操作修改成功");
                    this.Close();
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
