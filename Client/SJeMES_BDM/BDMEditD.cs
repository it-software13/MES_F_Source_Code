using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
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
    public partial class BDMEditD : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public BDMEditD()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        string mid = string.Empty;//一级菜单id
        public BDMEditD(double id,string _general_testtype_no)
        {
            InitializeComponent();
            mid = id.ToString();
            general_testtype_no = _general_testtype_no;
        }
        string dno = string.Empty;//二级菜单编号
        string did = string.Empty;//二级菜单id
        string general_testtype_no = string.Empty;//通用类别编号
        public BDMEditD(string _dno,string _did,string _general_testtype_no)
        {
            InitializeComponent();
            dno = _dno;
            did = _did;
            general_testtype_no = _general_testtype_no;
            TypeUpdata();
        }

        private void BDMEditD_Load(object sender, EventArgs e)
        {
            
        }

        //修改赋值
        public void TypeUpdata()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("dno", dno);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "TypeUpdataD", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            this.txt1.Text = item["secondary_category_name"].ToString();
                            this.txt2.Text = item["secondary_category_no"].ToString();
                            this.rtxt2.Text = item["remarks"].ToString();
                            this.txt2.ReadOnly = true;
                        }
                    }
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //类型修改,新增
        private void btn2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txt1.Text.Trim() != "" && this.txt2.Text.Trim() != "")
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("did", did);
                    data.Add("general_testtype_no", general_testtype_no);
                    data.Add("dno", this.txt2.Text.Trim());
                    data.Add("dname", this.txt1.Text.Trim());
                    data.Add("remarks", this.rtxt2.Text.Trim());
                    data.Add("mid", mid);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "TypeEditD", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        int count = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(j["RetData"].ToString());
                        if (count > 0)
                        {
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("编号重复");
                        }
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());

                }
                else
                    MessageBox.Show("代号和名称不能为空!!!");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt2_DoubleClick(object sender, EventArgs e)
        {
            string sql = @"select item_type_no as 分类代号,item_type_name as 分类名称,item_type_name2 as 分类描述 from bdm_rd_itemtype where item_type_no not in (select secondary_category_no from bdm_generalquality_d)";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();

            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {

                    object item_type_no = frmData.RetData.Rows[0]["分类代号"];
                    object item_type_name = frmData.RetData.Rows[0]["分类名称"];
                    object item_type_name2 = frmData.RetData.Rows[0]["分类描述"];

                    txt2.Text = item_type_no.ToString().Trim();
                    txt1.Text = item_type_name.ToString().Trim();
            }
        }

        private void txt1_DoubleClick(object sender, EventArgs e)
        {
            //string sql = @"select item_type_no as 分类代号,item_type_name as 分类名称,item_type_name2 as 分类描述 from bdm_rd_itemtype where item_type_no not in (select secondary_category_no from bdm_generalquality_d)";

            //FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            //frmData.ShowDialog();

            //if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            //{

            //    object item_type_no = frmData.RetData.Rows[0]["分类代号"];
            //    object item_type_name = frmData.RetData.Rows[0]["分类名称"];
            //    object item_type_name2 = frmData.RetData.Rows[0]["分类描述"];

            //    txt2.Text = item_type_no.ToString().Trim();
            //    txt1.Text = item_type_name.ToString().Trim();
            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
