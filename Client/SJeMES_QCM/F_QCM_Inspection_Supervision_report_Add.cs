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
    public partial class F_QCM_Inspection_Supervision_report_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Inspection_Supervision_report _f_QCM_Inspection_Supervision_Report { get; set; }
        public F_QCM_Inspection_Supervision_report_Add(F_QCM_Inspection_Supervision_report f_QCM_Inspection_Supervision_Report)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _f_QCM_Inspection_Supervision_Report = f_QCM_Inspection_Supervision_Report;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void actbtn_Click(object sender, EventArgs e)
        {

            try
            {
                #region 验证

                if(string.IsNullOrEmpty(this.txt_vend_name.Text) || 
                    string.IsNullOrEmpty(this.txt_way.Text) || 
                    string.IsNullOrEmpty(this.txt_part_no.Text) || 
                    string.IsNullOrEmpty(this.txt_shoe_nos.Text) || 
                    string.IsNullOrEmpty(this.txt_ART.Text) || 
                    string.IsNullOrEmpty(this.txt_PO.Text) || 
                    string.IsNullOrEmpty(this.txt_number.Text) || 
                    string.IsNullOrEmpty(this.txt_scqty.Text) || 
                    string.IsNullOrEmpty(this.txt_cjqty.Text) || 
                    string.IsNullOrEmpty(this.txt_ProcessTypes.Text))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("所有字段为必填项，请检查！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }

                #endregion

                string start_date = string.Empty;
                string end_date = string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("VEND_NAME", this.txt_vend_name.Text);
                data.Add("INSPECT_METHOD", this.txt_way.Text);
                data.Add("PART_NO", this.txt_part_no.Text);
                data.Add("SHOE_NOS", this.txt_shoe_nos.Text);
                data.Add("PROD_NO", this.txt_ART.Text);
                data.Add("PO_ORDER", this.txt_PO.Text);
                data.Add("CODE_NUMBER", this.txt_number.Text);

                data.Add("PO_QTY", this.txt_scqty.Text);
                data.Add("PLANSAMP_QTY", this.txt_cjqty.Text);
                data.Add("PROCESS_TYPE", this.txt_ProcessTypes.Text);



                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.SpotCheck", "AddSpotCheck", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    MessageBox.Show(ret.ErrMsg);
                    this.Close();
                    _f_QCM_Inspection_Supervision_Report.F_QCM_Inspection_Supervision_report_Load(null, null);
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }


        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
