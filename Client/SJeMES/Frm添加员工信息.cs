
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

namespace SJeMES
{
    public partial class Frm_AddEmployee : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string staffno = string.Empty;
        //GDSJ_Framework.DBHelper.DataBase DB = new GDSJ_Framework.DBHelper.DataBase("oracle", Program.Org.DBServer, Program.Org.DBName, Program.Org.DBUser, Program.Org.DBPassword, string.Empty);
        public Frm_AddEmployee(string staff_no)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
               Program.SkinThemes, materialSkinManager, this);
            staffno = staff_no;

            //if(!string.IsNullOrEmpty(staff_no))
            //{
            //    btn_updatePwd.Visible = true;
            //}
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
           // SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            GetData();
        }

        public Frm_AddEmployee(string staff_no, SJeMES_Framework.Class.ClientClass _Client)
        {
            Program.Client = _Client;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
               Program.SkinThemes, materialSkinManager, this);
            staffno = staff_no;
            disableContorl();

            //if(!string.IsNullOrEmpty(staff_no))
            //{
            //    btn_updatePwd.Visible = true;
            //}

            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            GetData();
        }

        private void disableContorl()
        {
            textBox14.Enabled = false;
            textBox12.Enabled = false;
            textBox13.Enabled = false;
            comboBox1.Enabled = false;
            textBox3.Enabled = false;
        }

        public void GetData()
        {
            if (!string.IsNullOrEmpty(staffno))
            {
                textBox1.Enabled = false;
                textBox14.Enabled = false;
                this.Text = "编辑员工信息";
                string sql = "select * from HR001M where staff_no='" + staffno + "'";
                DataTable dt = Program.Client.GetDT(sql);
                //DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.Org, Program.WebServiceUrl, sql, new Dictionary<string, string>());
                if (dt.Rows.Count>0)
                {
                    textBox1.Text = dt.Rows[0]["staff_no"].ToString();
                    textBox2.Text = dt.Rows[0]["staff_name"].ToString();
                    comboBox2.Text = dt.Rows[0]["staff_sex"].ToString();
                    textBox4.Text = dt.Rows[0]["staff_id"].ToString();
                    textBox5.Text = dt.Rows[0]["staff_province"].ToString();
                    textBox6.Text = dt.Rows[0]["staff_city"].ToString();
                    textBox7.Text = dt.Rows[0]["staff_address"].ToString();
                    textBox8.Text = dt.Rows[0]["staff_phone"].ToString();
                    textBox9.Text = dt.Rows[0]["staff_mobile"].ToString();
                    textBox10.Text = dt.Rows[0]["staff_qq"].ToString();
                    textBox11.Text = dt.Rows[0]["staff_email"].ToString();
                    textBox12.Text = dt.Rows[0]["staff_department"].ToString();
                    textBox13.Text = dt.Rows[0]["staff_post"].ToString();
                    textBox14.Text = dt.Rows[0]["user_code"].ToString();
                    comboBox1.Text = dt.Rows[0]["staff_status"].ToString();

                    //add by yj 20200314 add role set 
                    //table HR001M add columns 
                    textBox3.Text = dt.Rows[0]["UDF01"].ToString();
                    textBox15.Text = dt.Rows[0]["UDF02"].ToString();
                    comboBox3.Text = dt.Rows[0]["UDF03"].ToString();
                    //end add
                }


            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(staffno))
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("工号不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg); 
                    return;
                }
                if (string.IsNullOrEmpty(textBox14.Text))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("登录账号不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg); 
                    return;
                }
                string sql = "select * from HR001M where staff_no='"+ textBox1.Text + "'";
                DataTable dt = Program.Client.GetDT(sql);
                //DataTable dt= GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
               
                if (dt.Rows.Count==0)
                {
                    //edit by yj 20200314 add role set 
                    //table HR001M add columns 
                    sql = @"INSERT INTO HR001M
           (staff_no
           ,staff_name
           ,staff_sex
           ,staff_id
           ,staff_province
           ,staff_city
           ,staff_address
           ,staff_phone
           ,staff_mobile
           ,staff_qq
           ,staff_email
           ,staff_department
           ,staff_post
           ,staff_status
           ,user_code
           ,UDF01
           ,UDF02
,UDF03
          )
     VALUES
           ('" + textBox1.Text + @"'
           ,'" + textBox2.Text + @"'
           ,'" + comboBox2.Text + @"'
           ,'" + textBox4.Text + @"'
           ,'" + textBox5.Text + @"'
           ,'" + textBox6.Text + @"'
           ,'" + textBox7.Text + @"'
           ,'" + textBox8.Text + @"'
           ,'" + textBox9.Text + @"'
           ,'" + textBox10.Text + @"'
           ,'" + textBox11.Text + @"'
           ,'" + textBox12.Text + @"'
           ,'" + textBox13.Text + @"'
           ,'" + comboBox1.Text + @"'
           ,'" + textBox14.Text + @"'
 ,'" + textBox3.Text + @"'
 ,'" + textBox15.Text + @"'
 ,'" + comboBox3.Text + @"'
          )";
                    //end edit

                    Program.Client.ExecuteNonQuery(sql);
                    //GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.Org, Program.WebServiceUrl, sql, new Dictionary<string, string>());
                    string userpwd = SJeMES_Framework.Common.Security.MD5(textBox14.Text.Trim());
                    sql = @"
INSERT INTO SYSUSER01M
(UserCode,UserPwd,MaxWindow,BarCode)
VALUES
('" + textBox14.Text.Trim() + @"','" + userpwd.ToUpper() + @"','False','" + SJeMES_Framework.Common.Security.MD5(textBox14.Text.Trim()).ToUpper() + @"')
"; 
                    //edit by yj 20200314 add role set 
                    if (!string.IsNullOrEmpty(textBox3.Text))
                    {
                        sql += @"
INSERT INTO SYSROLE01A1
(UserCode,Role_Name,UserJurisdiction)
VALUES
('" + textBox14.Text.Trim() + @"','" + textBox15.Text.Trim() + @"','"+comboBox3.Text+@"')
";
                    }
                    //end add
                    //GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                    Program.Client.SYSExecuteNonQuery(sql);

                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("添加成功！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    //SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    SJeMES_Control_Library.MessageHelper.ShowOK(this,msg);
                    this.Close();
                    //GetData();
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("已存在相同工号！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg); 
                }
              
            }
            else
            {
                //edit by yj 20200314 add role set 
                string sql = @"UPDATE HR001M
   SET staff_no = '"+textBox1.Text+ @"'
      ,staff_name ='" + textBox2.Text + @"'
      ,staff_sex = '" + comboBox2.Text + @"'
      ,staff_id ='" + textBox4.Text + @"'
      ,staff_province = '" + textBox5.Text + @"'
      ,staff_city = '" + textBox6.Text + @"'
      ,staff_address = '" + textBox7.Text + @"'
      ,staff_phone = '" + textBox8.Text + @"'
      ,staff_mobile ='" + textBox9.Text + @"'
      ,staff_qq = '" + textBox10.Text + @"'
      ,staff_email = '" + textBox11.Text + @"'
      ,staff_department = '" + textBox12.Text + @"'
      ,staff_post = '" + textBox13.Text + @"'
      ,staff_status = '" + comboBox1.Text + @"'
      ,user_code = '" + textBox14.Text + @"'
,UDF01 = '" + textBox3.Text + @"'
,UDF02 = '" + textBox15.Text + @"'
,UDF03 = '" + comboBox3.Text + @"'
 WHERE staff_no='" + staffno+"' ";
                //end edit                
                Program.Client.ExecuteNonQuery(sql);

                //edit by yj 20200314 add role set 
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    sql = @"
if not Exists(select 1 from SYSROLE01A1 where UserCode='" + textBox14.Text.Trim() + @"')
INSERT INTO SYSROLE01A1
(UserCode,Role_Name)
VALUES
('" + textBox14.Text.Trim() + @"','" + textBox15.Text.Trim() + @"')
else 
update SYSROLE01A1 set Role_Name='" + textBox15.Text.Trim() + @"',UserJurisdiction='"+comboBox3.Text+"' where UserCode='" + textBox14.Text.Trim() + @"'
";
                    Program.Client.SYSExecuteNonQuery(sql);

                }
                //end add

                string msg = SJeMES_Framework.Common.UIHelper.UImsg("修改成功！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                 
                this.Close();
                // GetData();
            }
          
        }

        private void textBox12_Click(object sender, EventArgs e)
        {

            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            string sql = @"  SELECT department_code as 部门编号,department_name as 部门名称 FROM BASE005M";

            //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.Client.Org, FrmMenthName,
            //    Program.Client, Program.Client.WebServiceUrl,
            //    sql, Program.Client.Language, true, true);
            //frm.ShowDialog();
            //if (!string.IsNullOrEmpty(frm.ReturnDataXML))
            //{
            //    textBox12.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<部门编号>", "</部门编号>");
            //}

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData!=null && frmData.RetData.Rows.Count > 0)
            {
                textBox12.Text = frmData.RetData.Rows[0]["部门编号"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm添加员工信息_Load(object sender, EventArgs e)
        {

            #region 绑定下拉框内容
            //string sql = @"select enum_type,enum_code,
		          //                  (case '{0}'
		          //                  when 'cn' then ui_cn
		          //                  when 'en' then ui_en
		          //                  when 'hk' then ui_yn
		          //                  else enum_value end) enum_value
            //                from( 
	           //                 select enum_type,enum_code,enum_value,
	           //                 (case when nvl(ui_cn,'')=''then enum_value else ui_cn end) ui_cn,
	           //                 (case when nvl(ui_en,'')=''then enum_value else ui_en end) ui_en,
	           //                 (case when nvl(ui_yn,'')=''then enum_value else ui_yn end) ui_yn
	           //                 from SYS001M) tab
            //                where enum_type in ('enum_Sex','enum_EmployeeStatus')";

            string sql = @"select enum_type,enum_code,
		                            (case '{0}'
		                            when 'cn' then ui_cn
		                            when 'en' then ui_en
		                            when 'hk' then ui_yn
		                            else enum_value end) enum_value
                            from( 
	                            select enum_type,enum_code,enum_value,
	                            (case when isnull(ui_cn,'')=''then enum_value else ui_cn end) ui_cn,
	                            (case when isnull(ui_en,'')=''then enum_value else ui_en end) ui_en,
	                            (case when isnull(ui_yn,'')=''then enum_value else ui_yn end) ui_yn
	                            from SYS001M) tab
                            where enum_type in ('enum_Sex','enum_EmployeeStatus')";

            sql = string.Format(sql, Program.Client.Language.ToLower());
            DataTable dt = Program.Client.GetDT(sql);
            if(dt!=null && dt.Rows.Count>0)
            {
                DataRow[] dr1 = dt.Select("enum_type='enum_Sex'");
                DataRow[] dr2 = dt.Select("enum_type='enum_EmployeeStatus'");
                 
                if (dr1.Length>0)
                {
                    DataTable dtcombox = new DataTable();
                    dtcombox.Columns.Add("enum_code");
                    dtcombox.Columns.Add("enum_value");
                    foreach (DataRow dr in dr1)
                    {
                        DataRow drnew = dtcombox.NewRow();
                        drnew["enum_code"] = dr["enum_code"].ToString();
                        drnew["enum_value"] = dr["enum_value"].ToString();
                        dtcombox.Rows.Add(drnew);
                    } 
                    comboBox2.DataSource = dtcombox;
                    comboBox2.DisplayMember = "enum_value";
                    comboBox2.ValueMember = "enum_code";
                    comboBox2.SelectedIndex = -1;
                }
                if(dr2.Length>0)
                {
                    DataTable dtcombox = new DataTable();
                    dtcombox.Columns.Add("enum_code");
                    dtcombox.Columns.Add("enum_value");
                    dtcombox.Rows.Clear();
                    foreach (DataRow dr in dr2)
                    {
                        DataRow drnew = dtcombox.NewRow();
                        drnew["enum_code"] = dr["enum_code"].ToString();
                        drnew["enum_value"] = dr["enum_value"].ToString();
                        dtcombox.Rows.Add(drnew);
                    }
                    comboBox1.DataSource = dtcombox;
                    comboBox1.DisplayMember = "enum_value";
                    comboBox1.ValueMember = "enum_code";
                    comboBox1.SelectedIndex = -1;
                }

            }


            #endregion
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            string sql = @"  SELECT Role_No as 角色代号,Role_Name as 角色名称 FROM SYSROLE01M";

            GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(FrmMenthName,
                Program.Client, Program.Client.WebServiceUrl,
                sql, Program.Client.Language, true, true);
            frm.ShowDialog();

            //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.Client.WebServiceUrl, sql, true, true);
            //frm.ShowDialog();

            if (!string.IsNullOrEmpty(frm.ReturnDataXML))
            {
                textBox3.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<角色代号>", "</角色代号>");
                textBox15.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<角色名称>", "</角色名称>");
            }
        }

       
    }
}
