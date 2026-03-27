using GDSJ_Framework.WinForm.CommonForm;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJEMS_QX
{
    public partial class Frm_PermissionSettings : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        private int mod = 0;
        private ComboBox ComboBox = new ComboBox();
        public Frm_PermissionSettings()
        {
            InitializeComponent();

            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
                Program.SkinThemes, materialSkinManager, this);

            GetComboBox();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.WebServiceUrl, Program.Language);

        }
        private string ee = string.Empty;
        private string ss = string.Empty;
        public static void UIShow(string Form, Control control,string Language)
        {
            string sql = @"
SELECT 
ui_tittle AS '功能名称',
ui_code AS '控件ID',
ui_cn AS '控件名称',
ui_en AS '英语名称',
ui_yn AS '粤语名称'
FROM SJQDMS_UILAN where ui_tittle='" + Form + "'";
            //DataTable dt = Program.SYSDB.GetDataTable(sql);
            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            if (control is ComboBox)
            {
                ComboBox c = control as ComboBox;

                for (int i = 0; i < c.Items.Count; i++)
                {
                    DataRow[] dr = dt.Select("控件名称='" + c.Items[i].ToString().Trim().Replace("\r\n", "") + "'");
                    if (dr.Length > 0 && Language == "en")
                        c.Items[i] = !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : c.Items[i].ToString().Trim().Replace("\r\n", "");// 
                    if (dr.Length > 0 && Language == "hk")
                        c.Items[i] = !string.IsNullOrEmpty(dr[0]["粤语名称"].ToString()) ? dr[0]["粤语名称"].ToString() : c.Items[i].ToString().Trim().Replace("\r\n", "");
                }
            }
            else
            {
                foreach (Control c in control.Controls)
                {
                    if (c.Controls.Count > 0)
                    {
                        UIShow(Form, c, Language);
                    }

                    if (c is ComboBox)
                    {
                        ComboBox cc = c as ComboBox;

                        for (int i = 0; i < cc.Items.Count; i++)
                        {
                            DataRow[] dr = dt.Select("控件名称='" + cc.Items[i].ToString().Trim().Replace("\r\n", "") + "'");
                            if (dr.Length > 0 && Language == "en")
                                cc.Items[i] = !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : cc.Items[i].ToString().Trim().Replace("\r\n", "");// && Program.Client.Language != "en"
                            if (dr.Length > 0 && Language == "hk")
                                cc.Items[i] = !string.IsNullOrEmpty(dr[0]["粤语名称"].ToString()) ? dr[0]["粤语名称"].ToString() : cc.Items[i].ToString().Trim().Replace("\r\n", "");
                        }
                    }
                    else if (!string.IsNullOrEmpty(c.Text.Trim()))
                    {

                        DataRow[] dr = dt.Select("控件名称='" + c.Text.Trim() + "'");
                        if (dr.Length > 0 && Language == "en")
                            c.Text = !string.IsNullOrEmpty(dr[0]["英语名称"].ToString()) ? dr[0]["英语名称"].ToString() : c.Text.Trim();// && Program.Client.Language != "en"
                        if (dr.Length > 0 && Language == "hk")
                            c.Text = !string.IsNullOrEmpty(dr[0]["粤语名称"].ToString()) ? dr[0]["粤语名称"].ToString() : c.Text.Trim();
                    }
                }
            }
        }
        
        public void Getparent()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView2.DataSource = null;
            dataGridView2.Columns.Clear();
            dataGridView3.DataSource = null;
            dataGridView3.Columns.Clear();

            string Language = Program.Language;//语言 

            #region 一级菜单数据
            string sql = @"select * from (
	                                    select (case '{0}' 
		                                    when 'cn' then menu_name 
		                                    when 'en' then ui_en
		                                    when 'yn' then ui_yn
                                            when 'hk' then ui_yn
		                                    else menu_name end) as 一级菜单
	                                    from SYSMENU01M(nolock)
                                    where menu_name not in ('拣货','抛单')) tab where 1=1";
            sql = string.Format(sql, Language.ToLower());

            if (!string.IsNullOrEmpty(textBox3.Text) && comboBox1.SelectedValue.Equals("1"))
                sql += " and 一级菜单='" + textBox3.Text + "'";
            if (!string.IsNullOrEmpty(textBox3.Text) && comboBox1.SelectedValue.Equals("2"))
                sql += " and 一级菜单='" + GetData(ee) + "'";
            if (!string.IsNullOrEmpty(textBox3.Text) && comboBox1.SelectedValue.Equals("3"))
                sql += " and 一级菜单='" + GetData(ee) + "'";

            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());

            DataGridViewColumn myCol = new DataGridViewCheckBoxColumn();
            myCol.Visible = true;
            myCol.Name = "选择";
            myCol.HeaderText = "选择";
            dataGridView1.Columns.Add(myCol);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["选择"].Width = 70;
            #endregion


            #region 二级菜单
            sql = @"select * from(
                    select 
	                    (case '{0}' 
	                    when 'cn' then cn_一级菜单
	                    when 'en' then en_一级菜单
	                    when 'yn' then yn_一级菜单
	                    when 'hk' then yn_一级菜单
	                    else cn_一级菜单 end) '一级菜单',
	                    (case '{0}' 
	                    when 'cn' then cn_二级菜单
	                    when 'en' then en_二级菜单
	                    when 'yn' then yn_二级菜单
	                    when 'hk' then yn_二级菜单
	                    else cn_一级菜单 end) '二级菜单'
                    from (
	                    select b.menu_name 'cn_一级菜单',b.ui_en 'en_一级菜单',b.ui_yn 'yn_一级菜单',
		                    a.menu_name 'cn_二级菜单',
		                    (case when isnull(a.ui_en,'')=''then a.menu_name else a.ui_en end)'en_二级菜单',
		                    (case when isnull(a.ui_yn,'')=''then a.menu_name else a.ui_yn end)'yn_二级菜单'
	                    from SYSMENU02M(nolock)a
	                    left join SYSMENU01M(nolock)b on a.menu_parent=b.menu_name
	                    where a.menu_parent not in ('拣货','抛单')
                    ) tab)a where 1=1 ";
            sql = string.Format(sql,Language.ToLower());
            if (!string.IsNullOrEmpty(textBox3.Text) && comboBox1.SelectedValue.Equals("1"))
                sql += " and 一级菜单='" + textBox3.Text + "'";
            else if (!string.IsNullOrEmpty(textBox3.Text) && comboBox1.SelectedValue.Equals("2"))
                sql += " and 二级菜单='" + textBox3.Text + "'";
            else if (!string.IsNullOrEmpty(textBox3.Text) && comboBox1.SelectedValue.Equals("3"))
                sql += " and 二级菜单='" + ss + "'";

            dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            string y = string.Empty;
            if (comboBox1.SelectedValue.Equals("1"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(y))
                        y = "'" + dt.Rows[i]["二级菜单"].ToString() + "'";
                    else
                        y += ",'" + dt.Rows[i]["二级菜单"].ToString() + "'";
                }
            }
            DataGridViewColumn myCol2 = new DataGridViewCheckBoxColumn();
            myCol2.Visible = true;
            myCol2.Name = "选择";
            myCol2.HeaderText = "选择";
            dataGridView2.Columns.Add(myCol2);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns["选择"].Width = 70;
            #endregion

            #region 三级菜单 
            sql = @"select * from (select 
                                (case '{0}' 
		                        when 'cn' then cn_二级菜单 
		                        when 'en' then en_二级菜单
		                        when 'yn' then yn_二级菜单
		                        when 'hk' then yn_二级菜单
		                        else cn_二级菜单 end) '二级菜单',
		                        (case '{0}'
		                        when 'cn' then cn_三级菜单
		                        when 'en' then en_三级菜单
		                        when 'yn' then yn_三级菜单
		                        when 'hk' then yn_三级菜单
		                        else cn_三级菜单 end) '三级菜单',
                                (case '{0}'
		                       	when 'cn' then cn_权限明细
		                        when 'en' then en_权限明细
		                        when 'yn' then yn_权限明细
		                        when 'hk' then yn_权限明细
		                        else cn_权限明细 end) btnRole
                        from(
	                        select b.menu_name 'cn_二级菜单',
			                        (case when isnull(b.ui_en,'')=''then b.menu_name else b.ui_en end)'en_二级菜单',
			                        (case when isnull(b.ui_yn,'')=''then b.menu_name else b.ui_yn end)'yn_二级菜单',
			                        a.menu_name 'cn_三级菜单',
			                        (case when isnull(a.ui_en,'')=''then a.menu_name else a.ui_en end)'en_三级菜单',
			                        (case when isnull(a.ui_yn,'')=''then a.menu_name else a.ui_yn end)'yn_三级菜单',
                                    a.btnRole 'cn_权限明细',
			                        (case when isnull(a.btnRole_en,'')=''then a.btnRole else a.btnRole_en end) 'en_权限明细',
			                        (case when isnull(a.btnRole_yn,'')=''then a.btnRole else a.btnRole_yn end) 'yn_权限明细'
	                        from SYSMENU03M(nolock)a
	                        left join SYSMENU02M(nolock)b on a.menu_parent = b.menu_name
                        ) tab)A where 1=1 ";
            sql = string.Format(sql,Language.ToLower());
            if (!string.IsNullOrEmpty(textBox3.Text) && comboBox1.SelectedValue.Equals("1"))
                sql += " and 二级菜单 in (" + y + ")";
            else if (!string.IsNullOrEmpty(textBox3.Text) && comboBox1.SelectedValue.Equals("2"))
                sql += " and 二级菜单='" + textBox3.Text + "'";
            else if(!string.IsNullOrEmpty(textBox3.Text) && comboBox1.SelectedValue.Equals("3"))
                sql += " and 三级菜单='" + textBox3.Text + "'";

            dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            
            dt.Columns.Add("SavebtnRole");

            DataGridViewColumn myCol3 = new DataGridViewCheckBoxColumn();
            myCol3.Visible = true;
            myCol3.Name = "选择";
            dataGridView3.Columns.Add(myCol3);
            dataGridView3.DataSource = dt;

            //DataGridViewComboBoxColumn com = new DataGridViewComboBoxColumn();
            //com.HeaderText = "权限";
            //com.Name = "权限";
            //com.Items.Add("全部");
            //com.Items.Add("部门");
            //com.Items.Add("个人");

            DataGridViewComboBoxColumn com = GetDGVComboBox();
            dataGridView3.Columns.Add(com);
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                dataGridView3.Rows[i].Cells["权限"].Value = "3";
            }

            DataGridViewTextBoxColumn dgtbc = new DataGridViewTextBoxColumn();
            dgtbc.HeaderText = "明细权限";
            dgtbc.Name = "明细权限";
            dgtbc.ReadOnly = true;
            dataGridView3.Columns.Add(dgtbc);
            dataGridView3.Columns["btnRole"].Visible = false;
            dataGridView3.Columns["SavebtnRole"].Visible = false;
            dataGridView3.Columns["选择"].Width = 70;
            #endregion

        }
        public string GetData(string value)
        {
            switch (value)
            {
                case "智能智造":
                    return "生产管理";
                case "生产管理":
                    return "智能智造";
                case "质量卫士":
                    return "质量管理";
                case "质量管理":
                    return "质量卫士";
                case "设备":
                    return "设备管理";
                case "设备管理":
                    return "设备";
                case "权限":
                    return "权限管理";
                case "权限管理":
                    return "权限";
                default:
                    return value;
            }
        }
       
        //选择全部
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == false))
                {
                    dataGridView1.Rows[i].Cells[0].Value = "True";
                }
                else
                    continue;
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[0].Value) == false))
                {
                    dataGridView2.Rows[i].Cells[0].Value = "True";
                }
                else
                    continue;
            }
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView3.Rows[i].Cells[0].Value) == false))
                {
                    dataGridView3.Rows[i].Cells[0].Value = "True";
                }
                else
                    continue;
            }
        }
        //清空全部
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true))
                {
                    dataGridView1.Rows[i].Cells[0].Value = "False";
                }
                else
                    continue;
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[0].Value) == true))
                {
                    dataGridView2.Rows[i].Cells[0].Value = "False";
                }
                else
                    continue;
            }
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView3.Rows[i].Cells[0].Value) == true))
                {
                    dataGridView3.Rows[i].Cells[0].Value = "False";
                }
                else
                    continue;
            }
        }
        //选择权限
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sql = string.Empty;
            int mostChoiceNum;
            DataGridViewCheckBoxCell dgvCheck = (DataGridViewCheckBoxCell)(this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0]);
            if (this.dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                if (Convert.ToBoolean(dgvCheck.EditedFormattedValue) == false)
                {
                    dgvCheck.Value = true;
                }
                else
                {
                    dgvCheck.Value = false;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                     DataGridViewCheckBoxCell check = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    if (check != null && ((bool)check.FormattedValue == true))
                    {
                        string name = GetData(dataGridView1.Rows[i].Cells[1].Value.ToString());
                        sql = "select menu_name as '二级菜单' FROM SYSMENU02M where menu_parent='" + name + "'";
                        DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            for (int a = 0; a < dataGridView2.Rows.Count; a++)
                            {
                                if (dataGridView2.Rows[a].Cells[2].Value.ToString() == dt.Rows[j]["二级菜单"].ToString())
                                {
                                    if ((Convert.ToBoolean(dataGridView2.Rows[a].Cells[0].Value) == false))
                                    {
                                        dataGridView2.Rows[a].Cells[0].Value = "True";
                                        sql = "SELECT menu_name as '三级菜单' FROM SYSMENU03M where menu_parent='"+ dataGridView2.Rows[a].Cells[2].Value.ToString() + "'";
                                        DataTable dt1 = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                                        for (int x = 0; x < dt1.Rows.Count; x++)
                                        {
                                            for (int xx = 0; xx < dataGridView3.Rows.Count; xx++)
                                            {
                                                if (dataGridView3.Rows[xx].Cells[2].Value.ToString() == dt1.Rows[x]["三级菜单"].ToString())
                                                {
                                                    if ((Convert.ToBoolean(dataGridView3.Rows[xx].Cells[0].Value) == false))
                                                    {
                                                        dataGridView3.Rows[xx].Cells[0].Value = "True";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string name = GetData(dataGridView1.Rows[i].Cells[1].Value.ToString());
                        sql = "select menu_name as '二级菜单' FROM SYSMENU02M where menu_parent='" + name + "'";
                        DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            for (int a = 0; a < dataGridView2.Rows.Count; a++)
                            {
                                if (dataGridView2.Rows[a].Cells[2].Value.ToString() == dt.Rows[j]["二级菜单"].ToString())
                                {
                                    if ((Convert.ToBoolean(dataGridView2.Rows[a].Cells[0].Value) == true))
                                    {
                                        dataGridView2.Rows[a].Cells[0].Value = "False";
                                        sql = "SELECT menu_name as '三级菜单' FROM SYSMENU03M where menu_parent='" + dataGridView2.Rows[a].Cells[2].Value.ToString() + "'";
                                        DataTable dt1 = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
                                        for (int x = 0; x < dt1.Rows.Count; x++)
                                        {
                                            for (int xx = 0; xx < dataGridView3.Rows.Count; xx++)
                                            {
                                                if (dataGridView3.Rows[xx].Cells[2].Value.ToString() == dt1.Rows[x]["三级菜单"].ToString())
                                                {
                                                    if ((Convert.ToBoolean(dataGridView3.Rows[xx].Cells[0].Value) == true))
                                                    {
                                                        dataGridView3.Rows[xx].Cells[0].Value = "False";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            }
        ///Save permissions
        private void button3_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            string sql1 = string.Empty;
            string msg = string.Empty;

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the role", Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }


            sql = $@"select * from SYSMENU02M(nolock)";
            DataTable dt_SYSMENU02M = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());

            sql = $@"select * from SYSMENU03M(nolock)";
            DataTable dt_SYSMENU03M = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());

            sql = @"
                    SELECT * FROM SYSROLE01M
                    WHERE Role_No='" + textBox1.Text + @"'
                    ";
            DataTable dtRole = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            if (dtRole.Rows.Count > 0)//The role already exists, determine whether the role needs to be updated
            {
                if (mod == 0)
                {
                    MessageBoxButtons mess = MessageBoxButtons.OKCancel;

                    string msg1 = "提示";
                    string msg2 = "角色重复，是否要更新？";
                    List<string> lstKeys = new List<string>();
                    lstKeys.Add(msg1);
                    lstKeys.Add(msg2);
                    Dictionary<string,object> dic = SJeMES_Framework.Common.UIHelper.UIListMsg(lstKeys, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    if (dic.Count > 0)
                    {
                        msg1 = dic[msg1].ToString();
                        msg2 = dic[msg2].ToString();
                    }

                    DialogResult dr = MessageBox.Show(msg2, msg1, mess);
                    if (dr == DialogResult.Cancel) return;
                    }
                if (dtRole.Rows[0]["Role_Name"].ToString().Trim() != textBox2.Text.Trim())
                {
                    sql = @"
                            UPDATE SYSROLE01M
                            SET [Role_Name]=@Role_Name
                            WHERE Role_No=@Role_No";
                    Dictionary<string, string> P = new Dictionary<string, string>();
                    P.Add("Role_No", textBox1.Text);
                    P.Add("Role_Name", textBox2.Text);

                    GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);
                }

                #region 更新保存 
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell check = dataGridView3.Rows[i].Cells[0] as DataGridViewCheckBoxCell;

                    string level_2 = dataGridView3.Rows[i].Cells["二级菜单"].Value.ToString();
                    string level_3 = dataGridView3.Rows[i].Cells["三级菜单"].Value.ToString();
                     
                    if (check != null && ((bool)check.FormattedValue == true))
                    {
                        //查询一,二级菜单
                        string menu_parent = string.Empty;
                        string parentsecond = string.Empty;                        
                        DataRow[] dr2s = dt_SYSMENU02M.Select($@" menu_name='{level_2}' or ui_yn='{level_2}' or ui_en='{level_2}'");
                        if (dr2s.Length > 0)
                        {
                            menu_parent = dr2s[0]["menu_parent"].ToString();
                            parentsecond = dr2s[0]["menu_name"].ToString();
                        }

                        //查询三级菜单
                        string menuname = string.Empty;
                        DataRow[] dr3s = dt_SYSMENU03M.Select($@"menu_name='{level_3}' or ui_yn='{level_3}' or ui_en='{level_3}'");
                        if (dr3s.Length > 0)
                        {
                            menuname = dr3s[0]["menu_name"].ToString();
                        }
                        sql1 += @" if not exists(select 1 from SYSROLE03M(nolock) where Role_No='{0}' and menuname='{3}')
                                      insert into SYSROLE03M(Role_No,parent,parentsecond,menuname,UDF01,btnRole)
                                      values('{0}','{1}','{2}','{3}','{4}','{5}')
                                     else
                                         update SYSROLE03M set UDF01='{4}',btnRole='{5}' where Role_No='{0}' and menuname='{3}';";
                        sql1 = string.Format(sql1, textBox1.Text, menu_parent, parentsecond, menuname, dataGridView3.Rows[i].Cells["权限"].Value, 
                            dataGridView3.Rows[i].Cells["SavebtnRole"].Value);
                    }
                    else
                    { 
                        //查询三级菜单
                        string menuname = string.Empty;
                        DataRow[] dr3s = dt_SYSMENU03M.Select($@"menu_name='{level_3}' or ui_yn='{level_3}' or ui_en='{level_3}'"); 
                        if (dr3s.Length > 0)
                        {
                            menuname = dr3s[0]["menu_name"].ToString();
                        }
                        sql1 += @"  delete SYSROLE03M where Role_No='{0}' and menuname='{1}'; ";
                        sql1 = string.Format(sql1, textBox1.Text, menuname);
                    }
                }
                #endregion

                GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql1, new Dictionary<string, string>());
            }
            else
            {
                sql = @"
                        INSERT INTO SYSROLE01M
                        (Role_No,Role_Name)
                        VALUES
                        (@Role_No,@Role_Name)";
                Dictionary<string, string> P = new Dictionary<string, string>();
                P.Add("Role_No", textBox1.Text);
                P.Add("Role_Name", textBox2.Text);

                GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, P);


                #region 更新保存 
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell check = dataGridView3.Rows[i].Cells[0] as DataGridViewCheckBoxCell;

                    string level_2 = dataGridView3.Rows[i].Cells["二级菜单"].Value.ToString();
                    string level_3 = dataGridView3.Rows[i].Cells["三级菜单"].Value.ToString();
                    
                    if (check != null && ((bool)check.FormattedValue == true))
                    {
                        //查询一,二级菜单
                        string menu_parent = string.Empty;
                        string parentsecond = string.Empty;
                        DataRow[] dr2s = dt_SYSMENU02M.Select($@" menu_name='{level_2}' or ui_yn='{level_2}' or ui_en='{level_2}'");
                        if (dr2s.Length > 0)
                        {
                            menu_parent = dr2s[0]["menu_parent"].ToString();
                            parentsecond = dr2s[0]["menu_name"].ToString();
                        }
                        //查询三级菜单
                        string menuname = string.Empty;
                        DataRow[] dr3s = dt_SYSMENU03M.Select($@"menu_name='{level_3}' or ui_yn='{level_3}' or ui_en='{level_3}'");
                        if (dr3s.Length > 0)
                        {
                            menuname = dr3s[0]["menu_name"].ToString();
                        }
                        sql1 += @" if not exists(select 1 from SYSROLE03M(nolock) where Role_No='{0}' and menuname='{3}')
                                      insert into SYSROLE03M(Role_No,parent,parentsecond,menuname,UDF01,btnRole)
                                      values('{0}','{1}','{2}','{3}','{4}','{5}')
                                     else
                                         update SYSROLE03M set UDF01='{4}',btnRole='{5}' where Role_No='{0}' and menuname='{3}';";
                        sql1 = string.Format(sql1, textBox1.Text, menu_parent, parentsecond,
                            menuname, dataGridView3.Rows[i].Cells["权限"].Value, dataGridView3.Rows[i].Cells["SavebtnRole"].Value);
                    }
                    else
                    {
                        //查询三级菜单
                        string menuname = string.Empty;
                        sql = $@"select menu_parent,menu_name from SYSMENU03M(nolock) where menu_name='{level_3}' or ui_yn='{level_3}' or ui_en='{level_3}'";
                        DataRow[] dr3s = dt_SYSMENU03M.Select($@"menu_name='{level_3}' or ui_yn='{level_3}' or ui_en='{level_3}'");
                        if (dr3s.Length > 0)
                        {
                            menuname = dr3s[0]["menu_name"].ToString();
                        }
                        sql1 += @"  delete SYSROLE03M where Role_No='{0}' and menuname='{1}';";
                        sql1 = string.Format(sql1, textBox1.Text, menuname);
                    }
                }
                #endregion


                GDSJ_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql1, new Dictionary<string, string>());
            }


            msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功！", Program.Client, Program.WebServiceUrl, Program.Language);
            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
        }


        /// <summary>
        /// 获取指定权限
        /// </summary>
        public void GetUser()
        {
            string sql = @"
                            select * from (
                            select Role_No,
	                            (case '{0}' 
	                            when 'cn' then b.menu_name 
	                            when 'en' then (case when isnull(b.ui_en,'')=''then b.menu_name else b.ui_en end)
	                            when 'hk' then (case when isnull(b.ui_yn,'')=''then b.menu_name else b.ui_yn end)
	                            when 'yn' then (case when isnull(b.ui_yn,'')=''then b.menu_name else b.ui_yn end)
	                            else a.parent end) parent,
	                            (case '{0}'
	                            when 'cn' then c.menu_name 
	                            when 'en' then (case when isnull(c.ui_en,'')=''then c.menu_name else c.ui_en end)
	                            when 'hk' then (case when isnull(c.ui_yn,'')=''then c.menu_name else c.ui_yn end)
	                            when 'yn' then (case when isnull(c.ui_yn,'')=''then c.menu_name else c.ui_yn end)
	                            else a.parentsecond end) parentsecond,
	                            (case '{0}'
	                            when 'cn' then d.menu_name 
	                            when 'en' then (case when isnull(d.ui_en,'')=''then d.menu_name else d.ui_en end)
	                            when 'hk' then (case when isnull(d.ui_yn,'')=''then d.menu_name else d.ui_yn end)
	                            when 'yn' then (case when isnull(d.ui_yn,'')=''then d.menu_name else d.ui_yn end)
	                            else a.menuname end) menuname,
	                            a.UDF01,a.btnRole
                            from SYSROLE03M(nolock)a
                            left join SYSMENU01M(nolock)b on a.parent=b.menu_name
                            left join SYSMENU02M(nolock)c on a.parentsecond = c.menu_name
                            left join SYSMENU03M(nolock)d on a.menuname = d.menu_name
                            where a.parent not in ('拣货','抛单')
                            ) tab where Role_No='{1}'";
            sql = string.Format(sql, Program.Language.ToLower(), textBox1.Text);
            DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebServiceUrl, sql, new Dictionary<string, string>());
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //一级菜单
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        if (dataGridView1.Rows[j].Cells[1].Value.ToString() == dt.Rows[i]["parent"].ToString())
                        {
                            if ((Convert.ToBoolean(dataGridView1.Rows[j].Cells[0].Value) == false))
                            {
                                dataGridView1.Rows[j].Cells[0].Value = "True";
                            }
                        }
                    }
                    //二级菜单
                    for (int j = 0; j < dataGridView2.Rows.Count; j++)
                    {
                        if (dataGridView2.Rows[j].Cells[2].Value.ToString() == dt.Rows[i]["parentsecond"].ToString())
                        {
                            if ((Convert.ToBoolean(dataGridView2.Rows[j].Cells[0].Value) == false))
                            {
                                dataGridView2.Rows[j].Cells[0].Value = "True";
                            }
                        }
                    }
                    //三级菜单
                    for (int j = 0; j < dataGridView3.Rows.Count; j++)
                    {
                        if (dataGridView3.Rows[j].Cells["三级菜单"].Value.ToString() == dt.Rows[i]["menuname"].ToString())
                        {
                            if ((Convert.ToBoolean(dataGridView3.Rows[j].Cells["选择"].Value) == false))
                            {
                                dataGridView3.Rows[j].Cells["选择"].Value = "True";
                            }
                            if (string.IsNullOrEmpty(dt.Rows[i]["UDF01"].ToString()))
                            {
                                dataGridView3.Rows[j].Cells["权限"].Value ="3";
                            }
                            else
                            {
                                dataGridView3.Rows[j].Cells["权限"].Value = dt.Rows[i]["UDF01"].ToString();
                            }

                            #region 明细权限
                            if (!string.IsNullOrEmpty(dt.Rows[i]["btnRole"].ToString())
                                && !string.IsNullOrEmpty(dataGridView3.Rows[j].Cells["btnRole"].Value.ToString()))
                            {
                                string[] strbtnRole = dataGridView3.Rows[j].Cells["btnRole"].Value.ToString().Split(',');//全部内容
                                string[] saveRole = dt.Rows[i]["btnRole"].ToString().Split(',');//保存的内容
                                if (strbtnRole.Length > 0 && saveRole.Length > 0)
                                {
                                    string valText = ""; 
                                    foreach (string role in saveRole)
                                    {
                                        foreach (var item in strbtnRole)
                                        {
                                            string[] strbtnRole2 = item.Split('|');
                                            if (strbtnRole2.Length > 1 && strbtnRole2[0].Equals(role))
                                            {
                                                valText += strbtnRole2[1] + ",";
                                            }
                                        }
                                    }
                                    dataGridView3.Rows[j].Cells["明细权限"].Value = valText.Trim().TrimEnd(',');
                                    dataGridView3.Rows[j].Cells["SavebtnRole"].Value = dt.Rows[i]["btnRole"].ToString();
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
        }

        #region 角色代号操作（双击选择，输入新增）
        
        private void textBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        //获取角色
        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name+"|"+ System.Reflection.MethodBase.GetCurrentMethod().Name;
            string sql = "SELECT Role_No AS '角色代号',Role_Name AS '角色名称' FROM SYSROLE01M";

            //多语言调用
            frmSearchData frm = new frmSearchData(FrmMenthName, Program.Client, Program.WebServiceUrl,
                sql, Program.Language, true,true);

            //原有调用
            //frmSearchData frm = new frmSearchData(Program.WebServiceUrl,
            //    "SELECT Role_No AS '角色代号',Role_Name AS '角色名称' FROM SYSROLE01M", true, true);

            frm.ShowDialog();

            if (!string.IsNullOrEmpty(frm.ReturnDataXML))
            {
                textBox1.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<角色代号>", "</角色代号>");
                textBox2.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<角色名称>", "</角色名称>");
                GetUser();
                mod = 1;
            }
        }
        #endregion

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            mod = 0;
        }
        //菜单名称
        private void textBox3_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                if (comboBox1.SelectedValue.Equals("1"))
                {
                    string sql = @"select (case '{0}' 
		                                when 'cn' then menu_name 
		                                when 'en' then ui_en
		                                when 'yn' then ui_yn
                                        when 'hk' then ui_yn
		                                else menu_name end) as 一级菜单
	                                from SYSMENU01M(nolock)
                                where menu_name not in ('拣货','抛单')";
                    sql = string.Format(sql, Program.Language.ToLower());
                     
                    frmSearchData frm = new frmSearchData(FrmMenthName+"_1", Program.Client, Program.WebServiceUrl,
                       sql, Program.Language, true, true);
                     
                    //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(
                    //Program.WebServiceUrl, sql, true, true);
                   
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.ReturnDataXML))
                    {
                        textBox3.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<一级菜单>", "</一级菜单>");
                    }
                }
                else if (comboBox1.SelectedValue.Equals("2"))
                { 
                    //string sql = @"  select * from (SELECT (case when menu_parent='智能智造' THEN '生产管理' WHEN menu_parent='质量卫士' THEN '质量管理'
                    //                 WHEN menu_parent='设备' THEN '设备管理' WHEN menu_parent='权限' THEN '权限管理' ELSE menu_parent END) AS '一级菜单',
                    //                 menu_name as '二级菜单' FROM SYSMENU02M WHERE menu_parent not in ('拣货','抛单')) tab where 1=1";
                    string sql = @"select 
	                    (case '{0}' 
	                    when 'cn' then cn_一级菜单
	                    when 'en' then en_一级菜单
	                    when 'yn' then yn_一级菜单
	                    when 'hk' then yn_一级菜单
	                    else cn_一级菜单 end) '一级菜单',
	                    (case '{0}' 
	                    when 'cn' then cn_二级菜单
	                    when 'en' then en_二级菜单
	                    when 'yn' then yn_二级菜单
	                    when 'hk' then yn_二级菜单
	                    else cn_一级菜单 end) '二级菜单'
                    from (
	                    select b.menu_name 'cn_一级菜单',b.ui_en 'en_一级菜单',b.ui_yn 'yn_一级菜单',
		                    a.menu_name 'cn_二级菜单',
		                    (case when isnull(a.ui_en,'')=''then a.menu_name else a.ui_en end)'en_二级菜单',
		                    (case when isnull(a.ui_yn,'')=''then a.menu_name else a.ui_yn end)'yn_二级菜单'
	                    from SYSMENU02M(nolock)a
	                    left join SYSMENU01M(nolock)b on a.menu_parent=b.menu_name
	                    where a.menu_parent not in ('拣货','抛单')
                    ) tab";
                    sql = string.Format(sql, Program.Language.ToLower());

                    frmSearchData frm = new frmSearchData(FrmMenthName + "_2", Program.Client, Program.WebServiceUrl,
                       sql, Program.Language, true, true);

                    //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.WebServiceUrl, sql, true, true);
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(frm.ReturnDataXML))
                    {
                        ee= GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<一级菜单>", "</一级菜单>").ToString();
                        textBox3.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<二级菜单>", "</二级菜单>");
                    }
                }
                else if (comboBox1.SelectedValue.Equals("3"))
                {
                    //string sql = @"SELECT menu_parent as '二级菜单' ,menu_name as '三级菜单' FROM SYSMENU03M where 1=1";

                    string sql = @"select (case '{0}' 
		                        when 'cn' then cn_二级菜单 
		                        when 'en' then en_二级菜单
		                        when 'yn' then yn_二级菜单
		                        when 'hk' then yn_二级菜单
		                        else cn_二级菜单 end) '二级菜单',
		                        (case '{0}'
		                        when 'cn' then cn_三级菜单
		                        when 'en' then en_三级菜单
		                        when 'yn' then yn_三级菜单
		                        when 'hk' then yn_三级菜单
		                        else cn_三级菜单 end) '三级菜单'
                        from(
	                        select b.menu_name 'cn_二级菜单',
			                        (case when isnull(b.ui_en,'')='' then b.menu_name else b.ui_en end) 'en_二级菜单',
			                        (case when isnull(b.ui_yn,'')='' then b.menu_name else b.ui_yn end) 'yn_二级菜单',
			                        a.menu_name 'cn_三级菜单',
			                        (case when isnull(a.ui_en,'')='' then a.menu_name else a.ui_en end) 'en_三级菜单',
			                        (case when isnull(a.ui_yn,'')='' then a.menu_name else a.ui_yn end) 'yn_三级菜单'
	                        from SYSMENU03M(nolock)a
	                        left join SYSMENU02M(nolock)b on a.menu_parent = b.menu_name
                        ) tab";
                    sql = string.Format(sql, Program.Language.ToLower());
                    frmSearchData frm = new frmSearchData(FrmMenthName + "_3", Program.Client, Program.WebServiceUrl,
                      sql, Program.Language, true, true);

                   //GDSJ_Framework.WinForm.CommonForm.frmSearchData frm = new GDSJ_Framework.WinForm.CommonForm.frmSearchData(Program.WebServiceUrl, sql, true, true);
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.ReturnDataXML))
                    {
                       // ee = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<一级菜单>", "</一级菜单>").ToString();
                        ss= GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<二级菜单>", "</二级菜单>").ToString();
                        textBox3.Text = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<三级菜单>", "</三级菜单>");
                        ee=GDSJ_Framework.Common.WebServiceHelper.GetString(Program.WebServiceUrl, "select menu_parent from SYSMENU02M where menu_name='" + ss+"'", new Dictionary<string, string>());
                    }
                }
                Getparent();
                GetUser();
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("请选择菜单级别!", Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg); 
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView3.Columns[this.dataGridView3.CurrentCell.ColumnIndex].HeaderText.Equals("明细权限"))
            {
                string btnRole = dataGridView3.CurrentRow.Cells["btnRole"].Value.ToString();
                string SavebtnRole = dataGridView3.CurrentRow.Cells["SavebtnRole"].Value.ToString();
                Form_AssignAuthority form3 = new Form_AssignAuthority(btnRole, SavebtnRole);
                if (form3.ShowDialog() == DialogResult.OK)
                {
                    dataGridView3.CurrentRow.Cells["SavebtnRole"].Value = form3.sbCheck.ToString();
                    dataGridView3.CurrentCell.Value = form3.strCheck.ToString();
                    form3.Close();
                }
            }
        }

        private void Frm权限设置_Load(object sender, EventArgs e)
        {

            Getparent();

            SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name,Program.Client,Program.Language,Program.WebServiceUrl,dataGridView1,dataGridView2,dataGridView3);
        }
        
        /// <summary>
        /// 加载菜单等级下拉框
        /// </summary>
        public void GetComboBox()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("code");
                dt.Columns.Add("value");
                DataRow dr = dt.NewRow();

                switch (Program.Language.ToLower())
                {
                    case "cn":
                        {
                            dr = dt.NewRow();
                            dr["code"] = "1";
                            dr["value"] = "一级菜单";
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["code"] = "2";
                            dr["value"] = "二级菜单";
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["code"] = "3";
                            dr["value"] = "三级菜单";
                            dt.Rows.Add(dr);
                        }
                        break;
                    case "en":
                        {
                            dr = dt.NewRow();
                            dr["code"] = "1";
                            dr["value"] = "First Level Menu";
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["code"] = "2";
                            dr["value"] = "Second Level Menu";
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["code"] = "3";
                            dr["value"] = "Three Level Menu";
                            dt.Rows.Add(dr);
                        };
                        break;
                    case "yn":
                    case "hk":
                        {
                            dr = dt.NewRow();
                            dr["code"] = "1";
                            dr["value"] = "Trình đơn hạng nhất";
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["code"] = "2";
                            dr["value"] = "Trình đơn hai cấp";
                            dt.Rows.Add(dr);

                            dr = dt.NewRow();
                            dr["code"] = "3";
                            dr["value"] = "Ba cấp";
                            dt.Rows.Add(dr);
                        };
                        break;
                }

                comboBox1.DataSource = dt;
                comboBox1.ValueMember = "code";
                comboBox1.DisplayMember = "value";
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.WebServiceUrl, Program.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg); 
            }
        }
         

        /// <summary>
        /// 返回dgv单元多语言格下拉框
        /// </summary>
        /// <returns></returns>
        public DataGridViewComboBoxColumn GetDGVComboBox()
        {
            DataGridViewComboBoxColumn com = new DataGridViewComboBoxColumn();
            com.HeaderText = "权限";
            com.Name = "权限";

            DataTable dt = new DataTable();
            dt.Columns.Add("code");
            dt.Columns.Add("value");
            DataRow dr = dt.NewRow();

            switch (Program.Language.ToLower())
            {
                case "cn":
                    {
                        dr = dt.NewRow();
                        dr["code"] = "1";
                        dr["value"] = "全部";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "2";
                        dr["value"] = "部门";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "3";
                        dr["value"] = "个人";
                        dt.Rows.Add(dr);
                    }
                    break;
                case "en":
                    {
                        dr = dt.NewRow();
                        dr["code"] = "1";
                        dr["value"] = "All";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "2";
                        dr["value"] = "Department";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "3";
                        dr["value"] = "Personal";
                        dt.Rows.Add(dr);
                    };
                    break;
                case "yn":
                case "hk":
                    {
                        dr = dt.NewRow();
                        dr["code"] = "1";
                        dr["value"] = "nguyên";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "2";
                        dr["value"] = "Bộ";
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        dr["code"] = "3";
                        dr["value"] = "cá nhân";
                        dt.Rows.Add(dr);
                    };
                    break;
            }
            com.DataSource = dt;
            com.ValueMember = "code";
            com.DisplayMember = "value";
            return com;
        }


    }
}
