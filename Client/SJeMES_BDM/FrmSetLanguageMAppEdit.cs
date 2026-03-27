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
    public partial class FrmSetLanguageMAppEdit : MaterialForm
    {
        private string moudle_code;
        private readonly MaterialSkinManager materialSkinManager;
        public FrmSetLanguageMAppEdit(DataGridViewRow _currentRow)
        {
            InitializeComponent();

            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            moudle_code = _currentRow.Cells["moudle_code"].Value.ToString();

            txt_filed_code.Text = _currentRow.Cells["filed_code"].Value.ToString();
            txt_cn.Text = _currentRow.Cells["cn"].Value.ToString();
            txt_en.Text = _currentRow.Cells["en"].Value.ToString();
            txt_yn.Text = _currentRow.Cells["yn"].Value.ToString();
        }

        private void FrmSetLanguageMAppEdit_Load(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                #region 非空校验
                if (string.IsNullOrEmpty(txt_cn.Text))
                    throw new Exception("cn 不能为空");
                if (string.IsNullOrEmpty(txt_en.Text))
                    throw new Exception("en 不能为空");
                if (string.IsNullOrEmpty(txt_yn.Text))
                    throw new Exception("yn 不能为空");
                #endregion

                Dictionary<string, object> paramDic = new Dictionary<string, object>();
                paramDic.Add("moudle_code", moudle_code);
                paramDic.Add("field_code", txt_filed_code.Text);
                paramDic.Add("cn", txt_cn.Text);
                paramDic.Add("en", txt_en.Text);
                paramDic.Add("yn", txt_yn.Text);

                string retdata = WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.UILAN_APP",//类名
                                            "EditAppLanguageByCS",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(paramDic));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    string msg = UIHelper.UImsg("保存成功", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
                }
                else
                {
                    throw new Exception(ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                string msg = UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
