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

namespace SJeMES_AQL
{
    public partial class F_AQL_CMAThetestshoesArtEdit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public List<string> list = new List<string>();
        public F_AQL_CMAThetestshoes _frm;
        public F_AQL_CMAThetestshoesArtEdit(F_AQL_CMAThetestshoes frm)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _frm = frm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string str_art = textBox1.Text.Replace("\r\n","*");
            list = str_art.Split('*').ToList();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("list", list.Where(x=>!string.IsNullOrEmpty(x)).ToList());//art_list
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_AQLAPI", "SJ_AQLAPI.AQL_CMAThetestshoes", "Add_data", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (Convert.ToBoolean(ret.IsSuccess.ToString()))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                _frm.F_AQL_CMAThetestshoes_Load(null,null);
                this.Close();
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("save failed! " + ret.ErrMsg.ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }

           
        }
    }
}
