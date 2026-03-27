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
    public partial class F_QCM_BATCH_PRODUCTION_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        F_QCM_BATCH_PRODUCTION _main;
        DataGridViewRow _dr;
        public F_QCM_BATCH_PRODUCTION_Edit(F_QCM_BATCH_PRODUCTION main, DataGridViewRow dr)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _main = main;
            _dr = dr;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string ID = "";
        private void F_QCM_BATCH_PRODUCTION_Edit_Load(object sender, EventArgs e)
        {
            if (_dr != null)
            {
                ID = _dr.Cells["ID"].Value.ToString();
                txt_batch_code.Enabled = false;
                txt_batch_code.Text = _dr.Cells["量试编号"].Value.ToString();
                txt_kfjd.Text = _dr.Cells["开发季度"].Value.ToString();
                txt_type.Text = _dr.Cells["类别"].Value.ToString();
                txt_art.Text = _dr.Cells["ART"].Value.ToString();
                dtp_batch_date.Value = DateTime.Parse(_dr.Cells["量试日期"].Value.ToString());
                dtp_production_date.Value = DateTime.Parse(_dr.Cells["生产日期"].Value.ToString());
                txt_shoe_name.Text = _dr.Cells["鞋型名称"].Value.ToString();
                txt_ddmh.Text = _dr.Cells["大底模号"].Value.ToString();
                txt_size_double.Text = _dr.Cells["试作SIZE_双数"].Value.ToString();
                txt_color.Text = _dr.Cells["配色"].Value.ToString();
                txt_shoe_last.Text = _dr.Cells["楦头"].Value.ToString();
                txt_procedure.Text = _dr.Cells["工艺"].Value.ToString();
                txt_zzhq.Text = _dr.Cells["组长会签"].Value.ToString();
                txt_department.Text = _dr.Cells["执行部门"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_batch_code.Text.Trim()))
            {
                MessageBox.Show("量试编号不能为空");
                return;
            }

            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ID", ID);
            p.Add("BATCH_CODE", txt_batch_code.Text.Trim());
            p.Add("DEVELOP_QUARTER", txt_kfjd.Text.Trim());
            p.Add("TYPE", txt_type.Text.Trim());
            p.Add("ART", txt_art.Text.Trim());
            p.Add("BATCH_DATE", dtp_batch_date.Value.ToString("yyyy-MM-dd"));
            p.Add("PRODUCTION_DATE", dtp_production_date.Value.ToString("yyyy-MM-dd"));
            p.Add("SHOE_NAME", txt_shoe_name.Text.Trim());
            p.Add("BIG_MOLD_NO", txt_ddmh.Text.Trim());
            p.Add("SIZE_DOUBLE", txt_size_double.Text.Trim());
            p.Add("COLOR", txt_color.Text.Trim());
            p.Add("DEPARTMENT", txt_department.Text.Trim());
            p.Add("PROCEDURE", txt_procedure.Text.Trim());
            p.Add("LEADER_AUTOGRAPH", txt_zzhq.Text.Trim());
            p.Add("SHOE_LAST", txt_shoe_last.Text.Trim());
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.BatchProduction",//类名
                                        "Edit",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (ret.IsSuccess)
            {

                _main.FormLoad();
                MessageBox.Show("保存成功");
                this.Close();
            }
            else
            {
                MessageBox.Show(ret.ErrMsg);
            }
            return;
        }
    }
}
