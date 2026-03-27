using SJeMES_Control_Library.Forms;
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
    public partial class F_QCM_ART_File_Detail : Form
    {
        public F_QCM_ART_File_Detail()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public Dictionary<string, Dictionary<string, bool>> check_result = new Dictionary<string, Dictionary<string, bool>>();


        DataTable dt_p = null;
        private void txt_po_Click(object sender, EventArgs e)
        {
            string sql = @"select 'PO202111210001' as PO,'GW6348' as ART, '测试鞋型' as 鞋型,'万国' as 客户,1000 as 数量 from BDM_DPSTAGE_M
                            UNION 
                            select 'PO202111210002' as PO,'GW6348' as ART, '测试鞋型' as 鞋型,'万丰' as 客户,8000 as 数量 from BDM_DPSTAGE_M";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client, "R");
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0 && frmData.RetData.Rows[0]["PO"].ToString() != txt_po.Text)
            {
                if (string.IsNullOrEmpty(frmData.RetData.Rows[0]["ART"].ToString()))
                {
                    MessageBox.Show("该PO单据没有绑定ART编号,请查阅");
                    return;
                }

                SetCheckResultDefault();

                txt_po.Text = frmData.RetData.Rows[0]["PO"].ToString();
                txt_art.Text = frmData.RetData.Rows[0]["ART"].ToString();
                txt_shoe.Text = frmData.RetData.Rows[0]["鞋型"].ToString();
                txt_cust.Text = frmData.RetData.Rows[0]["客户"].ToString();
                txt_nums.Text = frmData.RetData.Rows[0]["数量"].ToString();


                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("ART", txt_art.Text.Trim());
                p.Add("PO", txt_po.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ARTFileBind",//类名
                                            "GetInfoByART",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    dt_p = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                    DataRow[] dr_user = dt_p.Select("SURE_USER<>''");
                    if (dr_user.Length > 0)
                    {
                        txt_sure.Text = dr_user[0]["SURE_USER_NAME"].ToString();
                        user_code = dr_user[0]["SURE_USER"].ToString();
                    }
                    else
                    {
                        txt_sure.Text = "";
                        user_code = "";
                    }

                    #region 绘制

                    DataRow[] dr1 = dt_p.Select("TYPE='验货'");
                    int left1 = 0;

                    panel_1.Controls.Clear();
                    for (int i = 0; i < dr1.Length; i++)
                    {
                        if (dr1[i]["WHD"].ToString().ToUpper() == "TRUE")
                        { check_result["验货"]["WHD"] = true; }
                        if (dr1[i]["YHD"].ToString().ToUpper() == "TRUE")
                        { check_result["验货"]["YHD"] = true; }
                        if (dr1[i]["QRQM"].ToString().ToUpper() == "TRUE")
                        { check_result["验货"]["QRQM"] = true; }

                        FileCheck check = new FileCheck();

                        check.Width = 113;
                        check.Left = (i * 113) - i;
                        left1 += 113;
                        check.Top = 0;
                        check.Height = 105;
                        check.panel1.Width = check.Width;
                        check.panel1.Height = 28;
                        check.panel1.Top = 0;
                        check.panel1.Left = 0;
                        check.label1.Text = dr1[i]["ENUM_VALUE"].ToString();
                        check.linkLabel1.Text = dr1[i]["FILE_NAME"].ToString();
                        check.lab_url.Text = dr1[i]["FILE_URL"].ToString();

                        check.panel2.Width = check.Width;
                        check.panel2.Height = check.Height - check.panel1.Height;
                        check.panel2.Top = check.panel1.Height - 1;
                        check.panel2.Left = 0;
                        panel_1.Controls.Add(check);
                    }

                    FileCheckStatus checkstatus = new FileCheckStatus(this, "验货");
                    checkstatus.Width = 113;
                    checkstatus.Left = left1 - dr1.Length - 1;
                    checkstatus.Top = 0;
                    checkstatus.Height = 105;
                    checkstatus.panel1.Width = checkstatus.Width;
                    checkstatus.panel1.Height = 28;
                    checkstatus.panel1.Top = 0;
                    checkstatus.panel1.Left = 0;
                    checkstatus.panel2.Width = checkstatus.Width;
                    checkstatus.panel2.Height = checkstatus.Height - checkstatus.panel1.Height;
                    checkstatus.panel2.Top = checkstatus.panel1.Height - 1;
                    checkstatus.panel2.Left = 0;
                    checkstatus.checkBox1.Checked = check_result["验货"].ContainsKey("WHD") ? check_result["验货"]["WHD"] : false;
                    checkstatus.checkBox2.Checked = check_result["验货"].ContainsKey("YHD") ? check_result["验货"]["YHD"] : false;
                    checkstatus.checkBox3.Checked = check_result["验货"].ContainsKey("QRQM") ? check_result["验货"]["QRQM"] : false;
                    panel_1.Controls.Add(checkstatus);



                    foreach (var item in ddl_bglx.Items)
                    {
                        DataRow[] dr_cs = dt_p.Select($"TYPE='测试' and TYPE1='{item.ToString()}'");
                        if (dr_cs.Length > 0)
                        {
                            if (dr_cs[0]["WHD"].ToString().ToUpper() == "TRUE")
                            { check_result[$"测试#{item.ToString()}"]["WHD"] = true; }
                            if (dr_cs[0]["YHD"].ToString().ToUpper() == "TRUE")
                            { check_result[$"测试#{item.ToString()}"]["YHD"] = true; }
                            if (dr_cs[0]["QRQM"].ToString().ToUpper() == "TRUE")
                            { check_result[$"测试#{item.ToString()}"]["QRQM"] = true; }
                        }
                    }

                    DataRow[] dr2 = dt_p.Select($"TYPE='测试' and TYPE1='{ddl_bglx.Text}'");
                    int left2 = 0;
                   
                    panel_2.Controls.Clear();
                    for (int i = 0; i < dr2.Length; i++)
                    {
                        is_setDefaul.Add($"测试#{ddl_bglx.Text}");

                        if (dr2[i]["WHD"].ToString().ToUpper() == "TRUE")
                        { check_result[$"测试#{ddl_bglx.Text}"]["WHD"] = true; }
                        if (dr2[i]["YHD"].ToString().ToUpper() == "TRUE")
                        { check_result[$"测试#{ddl_bglx.Text}"]["YHD"] = true; }
                        if (dr2[i]["QRQM"].ToString().ToUpper() == "TRUE")
                        { check_result[$"测试#{ddl_bglx.Text}"]["QRQM"] = true; }

                        FileCheck check = new FileCheck();

                        check.Width = 113;
                        check.Left = (i * 113) - i;
                        left2 += 113;
                        check.Top = 0;
                        check.Height = 105;
                        check.panel1.Width = check.Width;
                        check.panel1.Height = 28;
                        check.panel1.Top = 0;
                        check.panel1.Left = 0;
                        check.panel2.Width = check.Width;
                        check.panel2.Height = check.Height - check.panel1.Height;
                        check.panel2.Top = check.panel1.Height - 1;
                        check.panel2.Left = 0;
                        check.label1.Text = dr2[i]["ENUM_VALUE"].ToString();
                        check.linkLabel1.Text = dr2[i]["FILE_NAME"].ToString();
                        check.lab_url.Text = dr2[i]["FILE_URL"].ToString();
                        panel_2.Controls.Add(check);
                    }

                    FileCheckStatus checkstatus2 = new FileCheckStatus(this, $"测试#{ddl_bglx.Text}");
                    checkstatus2.Width = 113;
                    checkstatus2.Left = left2 - dr2.Length - 1;
                    checkstatus2.Top = 0;
                    checkstatus2.Height = 105;
                    checkstatus2.panel1.Width = checkstatus2.Width;
                    checkstatus2.panel1.Height = 28;
                    checkstatus2.panel1.Top = 0;
                    checkstatus2.panel1.Left = 0;
                    checkstatus2.panel2.Width = checkstatus2.Width;
                    checkstatus2.panel2.Height = checkstatus2.Height - checkstatus2.panel1.Height;
                    checkstatus2.panel2.Top = checkstatus2.panel1.Height - 1;
                    checkstatus2.panel2.Left = 0;

                    checkstatus2.checkBox1.Checked = check_result[$"测试#{ddl_bglx.Text}"].ContainsKey("WHD") ? check_result[$"测试#{ddl_bglx.Text}"]["WHD"] : false;
                    checkstatus2.checkBox2.Checked = check_result[$"测试#{ddl_bglx.Text}"].ContainsKey("YHD") ? check_result[$"测试#{ddl_bglx.Text}"]["YHD"] : false;
                    checkstatus2.checkBox3.Checked = check_result[$"测试#{ddl_bglx.Text}"].ContainsKey("QRQM") ? check_result[$"测试#{ddl_bglx.Text}"]["QRQM"] : false;
                    panel_2.Controls.Add(checkstatus2);


                    DataRow[] dr3 = dt_p.Select($"TYPE='FDVS'");
                    int left3 = 0;
                    panel_3.Controls.Clear();
                    for (int i = 0; i < dr3.Length; i++)
                    {
                        if (dr3[i]["WHD"].ToString().ToUpper() == "TRUE")
                        { check_result["FDVS"]["WHD"] = true; }
                        if (dr3[i]["YHD"].ToString().ToUpper() == "TRUE")
                        { check_result["FDVS"]["YHD"] = true; }
                        if (dr3[i]["QRQM"].ToString().ToUpper() == "TRUE")
                        { check_result["FDVS"]["QRQM"] = true; }

                        FileCheck check = new FileCheck();
                        check.Width = 113;
                        check.Left = (i * 113) - i;
                        left3 += 113;
                        check.Top = 0;
                        check.Height = 105;
                        check.panel1.Width = check.Width;
                        check.panel1.Height = 28;
                        check.panel1.Top = 0;
                        check.panel1.Left = 0;
                        check.panel2.Width = check.Width;
                        check.panel2.Height = check.Height - check.panel1.Height;
                        check.panel2.Top = check.panel1.Height - 1;
                        check.panel2.Left = 0;
                        check.label1.Text = dr3[i]["ENUM_VALUE"].ToString();
                        check.linkLabel1.Text = dr3[i]["FILE_NAME"].ToString();
                        check.lab_url.Text = dr3[i]["FILE_URL"].ToString();
                        panel_3.Controls.Add(check);
                    }

                    FileCheckStatus checkstatus3 = new FileCheckStatus(this, $"FDVS");
                    checkstatus3.Width = 113;
                    checkstatus3.Left = left3 - dr3.Length - 1;
                    checkstatus3.Top = 0;
                    checkstatus3.Height = 105;
                    checkstatus3.panel1.Width = checkstatus3.Width;
                    checkstatus3.panel1.Height = 28;
                    checkstatus3.panel1.Top = 0;
                    checkstatus3.panel1.Left = 0;
                    checkstatus3.panel2.Width = checkstatus3.Width;
                    checkstatus3.panel2.Height = checkstatus3.Height - checkstatus3.panel1.Height;
                    checkstatus3.panel2.Top = checkstatus3.panel1.Height - 1;
                    checkstatus3.panel2.Left = 0;
                    checkstatus3.checkBox1.Checked = check_result["FDVS"].ContainsKey("WHD") ? check_result["FDVS"]["WHD"] : false;
                    checkstatus3.checkBox2.Checked = check_result["FDVS"].ContainsKey("YHD") ? check_result["FDVS"]["YHD"] : false;
                    checkstatus3.checkBox3.Checked = check_result["FDVS"].ContainsKey("QRQM") ? check_result["FDVS"]["QRQM"] : false;
                    panel_3.Controls.Add(checkstatus3);
                    #endregion
                }




            }
        }
        public string user_code = "";
        private void txt_sure_Click(object sender, EventArgs e)
        {
            string sql = "select STAFF_NO as 工号,STAFF_NAME as 名称,staff_department as 部门 from hr001m";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client, "R");
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_sure.Text = frmData.RetData.Rows[0]["名称"].ToString();
                user_code = frmData.RetData.Rows[0]["工号"].ToString();
            }
        }

        private void F_QCM_ART_File_Detail_Load(object sender, EventArgs e)
        {
            ddl_bglx.SelectedIndex = 0;
            SetCheckResultDefault();
        }

        List<string> is_setDefaul = new List<string>();

        private void ddl_bglx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dt_p != null)
            {
                panel_2.Controls.Clear();
                DataRow[] dr2 = dt_p.Select($"TYPE='测试' and TYPE1='{ddl_bglx.Text}'");
                int left2 = 0;
                if (!check_result.ContainsKey($"测试#{ddl_bglx.Text}"))
                {
                    check_result[$"测试#{ddl_bglx.Text}"] = new Dictionary<string, bool>();
                    check_result[$"测试#{ddl_bglx.Text}"]["WHD"] = false;
                    check_result[$"测试#{ddl_bglx.Text}"]["YHD"] = false;
                    check_result[$"测试#{ddl_bglx.Text}"]["QRQM"] = false;
                }
                for (int i = 0; i < dr2.Length; i++)
                {
                    if (!is_setDefaul.Contains($"测试#{ddl_bglx.Text}"))
                    {
                        if (dr2[i]["WHD"].ToString().ToUpper() == "SURE")
                        { check_result[$"测试#{ddl_bglx.Text}"]["WHD"] = true; }
                        if (dr2[i]["YHD"].ToString().ToUpper() == "SURE")
                        { check_result[$"测试#{ddl_bglx.Text}"]["YHD"] = true; }
                        if (dr2[i]["QRQM"].ToString().ToUpper() == "SURE")
                        { check_result[$"测试#{ddl_bglx.Text}"]["QRQM"] = true; }
                        is_setDefaul.Add($"测试#{ddl_bglx.Text}");
                    }

                    FileCheck check = new FileCheck();
                    check.Width = 113;
                    check.Left = (i * 113) - i;
                    left2 += 113;
                    check.Top = 0;
                    check.Height = 105;
                    check.panel1.Width = check.Width;
                    check.panel1.Height = 28;
                    check.panel1.Top = 0;
                    check.panel1.Left = 0;
                    check.panel2.Width = check.Width;
                    check.panel2.Height = check.Height - check.panel1.Height;
                    check.panel2.Top = check.panel1.Height - 1;
                    check.panel2.Left = 0;
                    check.label1.Text = dr2[i]["ENUM_VALUE"].ToString();
                    check.linkLabel1.Text = dr2[i]["FILE_NAME"].ToString();
                    check.lab_url.Text = dr2[i]["FILE_URL"].ToString();
                    panel_2.Controls.Add(check);
                }

                FileCheckStatus checkstatus2 = new FileCheckStatus(this, $"测试#{ddl_bglx.Text}");


                checkstatus2.Width = 113;
                checkstatus2.Left = left2 - dr2.Length - 1;
                checkstatus2.Top = 0;
                checkstatus2.Height = 105;
                checkstatus2.panel1.Width = checkstatus2.Width;
                checkstatus2.panel1.Height = 28;
                checkstatus2.panel1.Top = 0;
                checkstatus2.panel1.Left = 0;
                checkstatus2.panel2.Width = checkstatus2.Width;
                checkstatus2.panel2.Height = checkstatus2.Height - checkstatus2.panel1.Height;
                checkstatus2.panel2.Top = checkstatus2.panel1.Height - 1;
                checkstatus2.panel2.Left = 0;

                checkstatus2.checkBox1.Checked = check_result[$"测试#{ddl_bglx.Text}"].ContainsKey("WHD") ? check_result[$"测试#{ddl_bglx.Text}"]["WHD"] : false;
                checkstatus2.checkBox2.Checked = check_result[$"测试#{ddl_bglx.Text}"].ContainsKey("YHD") ? check_result[$"测试#{ddl_bglx.Text}"]["YHD"] : false;
                checkstatus2.checkBox3.Checked = check_result[$"测试#{ddl_bglx.Text}"].ContainsKey("QRQM") ? check_result[$"测试#{ddl_bglx.Text}"]["QRQM"] : false;
                panel_2.Controls.Add(checkstatus2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_po.Text))
            {
                MessageBox.Show("请选择PO单据");
                return;
            }
            if (string.IsNullOrEmpty(user_code))
            {
                MessageBox.Show("请选择确认人");
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("PO", txt_po.Text.Trim());
            p.Add("SURE_USER", user_code);
            p.Add("SURE_USER_NAME", txt_sure.Text);
            p.Add("CHECK_DATA", check_result);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ARTFileBind",//类名
                                        "SavePOFileCheck",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                MessageBox.Show("提交成功");
            }
        }


        public void SetCheckResultDefault()
        {
            is_setDefaul.Clear();
            check_result.Clear();
            if (!check_result.ContainsKey($"验货"))
            {
                check_result["验货"] = new Dictionary<string, bool>();
                check_result["验货"]["WHD"] = false;
                check_result["验货"]["YHD"] = false;
                check_result["验货"]["QRQM"] = false;
            }
            foreach (var item in ddl_bglx.Items)
            {
                if (!check_result.ContainsKey($"测试#{item.ToString()}"))
                {
                    check_result[$"测试#{item.ToString()}"] = new Dictionary<string, bool>();
                    check_result[$"测试#{item.ToString()}"]["WHD"] = false;
                    check_result[$"测试#{item.ToString()}"]["YHD"] = false;
                    check_result[$"测试#{item.ToString()}"]["QRQM"] = false;
                }
            }
            if (!check_result.ContainsKey($"FDVS"))
            {
                check_result["FDVS"] = new Dictionary<string, bool>();
                check_result["FDVS"]["WHD"] = false;
                check_result["FDVS"]["YHD"] = false;
                check_result["FDVS"]["QRQM"] = false;
            }
        }
    }
}
