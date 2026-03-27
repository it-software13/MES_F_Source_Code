using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMESSystemTools
{
    public partial class Frm_SJeMESSystemTools : MaterialForm
    {
        public string MenuP=string.Empty;
        public string MenuLevel=string.Empty;
        private readonly MaterialSkinManager materialSkinManager;

        public string title = string.Empty;//名称

        public string biodyTitle = string.Empty;//表身名称

        public string App_JsonHList = string.Empty;

        //添加表头返回参数
        public string appCode = string.Empty;
        public string key = string.Empty;
        public string appName = string.Empty;
        public string tableName = string.Empty;

        public string mod = string.Empty;//当前是否为添加
        public string isHaveHead = string.Empty;//添加表身判断当前是否有表头
        public int bodyCount = 0;//添加表身判断当前是否有表头
        public int nowBody = 0;//当前设置的表身
        public string moi = string.Empty;//设置表头保存

        public DataTable dtMore;



        public Dictionary<string,string> values = new Dictionary<string, string>();

        public Frm_SJeMESSystemTools()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
                MaterialSkinManager.Themes.LIGHT, materialSkinManager, this);
            GetOrg();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);

        }

        private void GetOrg()
        {
            try
            {
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                "SJ_SYSAPI", "SJ_SYSAPI.SYS", "GetOrg", string.Empty, string.Empty);

                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    System.Data.DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(j["RetData"].ToString());

                    DataTable data = new DataTable();
                    data.Columns.Add("org");
                    data.Columns.Add("orgname");
                    data.Columns.Add("dbtype");
                    data.Columns.Add("dbserver");
                    data.Columns.Add("dbname");
                    data.Columns.Add("dbuser");
                    data.Columns.Add("dbpassword");

                    foreach(DataRow dr in dt.Rows)
                    {
                        DataRow ddr = data.NewRow();
                        ddr["org"] = dr["org"].ToString();
                        ddr["orgname"] = dr["orgname"].ToString();
                        ddr["dbtype"] = dr["dbtype"].ToString();
                        ddr["dbserver"] = dr["dbserver"].ToString();
                        ddr["dbname"] = dr["dbname"].ToString();
                        ddr["dbuser"] = dr["dbuser"].ToString();
                        ddr["dbpassword"] = dr["dbpassword"].ToString();

                        data.Rows.Add(ddr);

                    }

                    dataGridView1.DataSource = data.DefaultView;
                    dataGridView1.Update();
                    //SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, Program.Client, Program.Client.Language, Program.Client.WebServiceUrl, dataGridView1);
                }
                else
                {
                    throw new Exception(j["ErrMsg"].ToString());
                }
            }
            catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void ucBtnImg3_BtnClick(object sender, EventArgs e)
        {
            SJeMES_Control_Library.Forms.FrmInputs frm = new
                  SJeMES_Control_Library.Forms.FrmInputs("设置(Set Up)",
                  new string[] { "API地址" },
                  new Dictionary<string, SJeMES_Control_Library.TextInputType>(),
                  new Dictionary<string, string>(),
                  new Dictionary<string, SJeMES_Control_Library.Controls.KeyBoardType>(),
                  new List<string>() { "API地址" },
                  new Dictionary<string, string>() { { "API地址", Program.Client.APIURL }
                  });
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(Program.configstring);

                Pconfig["api"] = frm.Values[0];
               
                Program.Client.APIURL = frm.Values[0];
               

                Program.configstring = Newtonsoft.Json.JsonConvert.SerializeObject(Pconfig);


                //System.IO.File.Delete("Config.json");
                //SJeMES_Framework.Common.TXTHelper.WriteToEnd("Config.json", Program.configstring);
                SJeMES_Framework.Common.TXTHelper.WriteLine("Config.json", Program.configstring);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            string sql = @"select * from (
	                            select menu_name menu,
	                            (case '{0}' 
		                            when 'cn' then menu_name 
		                            when 'en' then ui_en
		                            when 'yn' then ui_yn
                                    when 'hk' then ui_yn
		                            else menu_name end) as menu_name
	                            from SYSMENU01M(nolock)
                            where menu_name not in ('拣货','抛单')) tab";
            sql = string.Format(sql, Program.Client.Language.ToLower());
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sql", sql); 
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                try
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());
                    if(dt!=null && dt.Rows.Count>0)
                    {
                        List<KeyValuePair<string, string>> KeyValues = new List<KeyValuePair<string, string>>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            KeyValues.Add(new KeyValuePair<string, string>(dr["menu"].ToString(), dr["menu_name"].ToString()));
                        }
                         
                        ucCombox1.Source = KeyValues;
                        ucCombox1.SelectedIndex = 0;
                    }
                }
                catch { }

            }
            else
            {
                throw new Exception(j["ErrMsg"].ToString());
            }

            //List<KeyValuePair<string, string>> KeyValues = new List<KeyValuePair<string, string>>();
            //KeyValues.Add(new KeyValuePair<string, string>("控制台", "控制台"));
            //KeyValues.Add(new KeyValuePair<string, string>("基础资料", "基础资料"));
            //KeyValues.Add(new KeyValuePair<string, string>("智能仓储", "智能仓储"));
            //KeyValues.Add(new KeyValuePair<string, string>("智能智造", "生产管理"));
            //KeyValues.Add(new KeyValuePair<string, string>("质量卫士", "质量管理"));
            //KeyValues.Add(new KeyValuePair<string, string>("设备管理", "设备管理"));
            //KeyValues.Add(new KeyValuePair<string, string>("系统管理", "系统管理"));
            //KeyValues.Add(new KeyValuePair<string, string>("权限管理", "权限管理"));
            //ucCombox1.Source = KeyValues;
            //ucCombox1.SelectedIndex = 0;
        }

        private void btn_Add_BtnClick(object sender, EventArgs e)
        {
            

            SJeMES_Control_Library.Forms.FrmInputs frm = new
                SJeMES_Control_Library.Forms.FrmInputs("添加企业库(Add Enterprise Library)",
                new string[] { "org","orgname","dbtype","dbserver","dbname","dbuser","dbpassword" },
                new Dictionary<string, SJeMES_Control_Library.TextInputType>(),
                new Dictionary<string, string>(),
                new Dictionary<string, SJeMES_Control_Library.Controls.KeyBoardType>(),
                new List<string>() { "org","orgname","dbtype","dbserver","dbname","dbuser","dbpassword" },
                new Dictionary<string, string>(),new List<string>(), 600);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Dictionary<string, object> P = new Dictionary<string, object>();
                P.Add("org", frm.Values[0]);
                P.Add("orgname", frm.Values[1]);
                P.Add("dbtype", frm.Values[2]);
                P.Add("dbserver", frm.Values[3]);
                P.Add("dbname", frm.Values[4]);
                P.Add("dbuser", frm.Values[5]);
                P.Add("dbpassword", frm.Values[6]);

                string sql = SJeMES_Framework.Common.StringHelper.GetInsertSqlByDictionary("SqlServer", "SYSORG01M", P);
                string sqlp = SJeMES_Framework.Common.StringHelper.GetSqlPByDictionary(P);
                string pname = SJeMES_Framework.Common.StringHelper.GetPNameByDictionary(P);


                

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("sql", sql);
                data.Add("sqlp", sqlp);
                data.Add("pname", pname);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                {

                    GetOrg();
                }
                else
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }


            }
        }

        private void btn_Del_BtnClick(object sender, EventArgs e)
        {
            try { 
                if(dataGridView1.SelectedCells.Count>0)
                {
                    if(SJeMES_Control_Library.MessageHelper.ShowWarning(this,"是否删除选中的数据？")== DialogResult.OK)
                    {
                        string CompanyCode = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["org"].Value.ToString();

                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("org", CompanyCode);

                        string sql = @"delete from SYSORG01M where org=@org";
                        string sqlp = SJeMES_Framework.Common.StringHelper.GetSqlPByDictionary(p);
                        string pname = SJeMES_Framework.Common.StringHelper.GetPNameByDictionary(p);


                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("sql", sql);
                        data.Add("sqlp", sqlp);
                        data.Add("pname", pname);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                        var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                        if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                        {

                            GetOrg();
                        }
                        else
                        {
                            throw new Exception(ret["ErrMsg"].ToString());
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void btn_Edit_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {

                    string CompanyCode = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["org"].Value.ToString();
                    string CompanyName = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["orgname"].Value.ToString();
                    string DBType = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["dbtype"].Value.ToString();
                    string DBServer = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["dbserver"].Value.ToString();
                    string DBName = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["dbname"].Value.ToString();
                    string DBUser = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["dbuser"].Value.ToString();
                    string DBPassword = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["dbpassword"].Value.ToString();


                    SJeMES_Control_Library.Forms.FrmInputs frm = new
                SJeMES_Control_Library.Forms.FrmInputs("修改企业库(Modify Enterprise Library)",
                new string[] { "org", "orgname", "dbtype", "dbserver", "dbname", "dbuser", "dbpassword" },
                new Dictionary<string, SJeMES_Control_Library.TextInputType>(),
                new Dictionary<string, string>(),
                new Dictionary<string, SJeMES_Control_Library.Controls.KeyBoardType>(),
                new List<string>() { "org", "orgname", "dbtype", "dbserver", "dbname", "dbuser", "dbpassword" },
                new Dictionary<string, string>()
                { { "org", CompanyCode },
                { "orgname", CompanyName },
                { "dbtype", DBType },
                { "dbserver", DBServer },
                { "dbname", DBName },
                { "dbuser", DBUser },
                { "dbpassword", DBPassword }}
                ,new List<string>() { "org" }, 600);
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("org", frm.Values[0]);
                        p.Add("orgname", frm.Values[1]);
                        p.Add("dbtype", frm.Values[2]);
                        p.Add("dbserver", frm.Values[3]);
                        p.Add("dbname", frm.Values[4]);
                        p.Add("dbuser", frm.Values[5]);
                        p.Add("dbpassword", frm.Values[6]);

                        string sql = SJeMES_Framework.Common.StringHelper.GetUpdateSqlByDictionary("SYSORG01M",
                            " org=@org ", p);

                        string sqlp = SJeMES_Framework.Common.StringHelper.GetSqlPByDictionary(p);
                        string pname = SJeMES_Framework.Common.StringHelper.GetPNameByDictionary(p);


                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("sql", sql);
                        data.Add("sqlp", sqlp);
                        data.Add("pname", pname);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                        var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                        if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                        {

                            GetOrg();
                        }
                        else
                        {
                            throw new Exception(ret["ErrMsg"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                GetModuleList(string.Empty);
            }
        }

        private void ucCombox1_SelectedChangedEvent(object sender, EventArgs e)
        {
            try
            {
                MenuP = ucCombox1.SelectedValue;
                MenuLevel = "2";
                GetMenus();
                GetMenuInfo2();
                MenuUpdate();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void MenuUpdate()
        {
            string Language = Program.Client.Language.ToLower();
            if (Program.Menus !=null)
            {
                SJeMES_Framework.Web.JSONMenu menu = new SJeMES_Framework.Web.JSONMenu();
                this.treeViewEx1.Nodes.Clear();
                foreach(SJeMES_Framework.Web.JSONMenu m in Program.Menus)
                {
                    if (m.menu_name == ucCombox1.SelectedValue)
                    {
                        menu = m;
                    }
                }

                foreach (string key in menu.children.Keys)
                {
                    SJeMES_Framework.Web.JSONMenu m = menu.children[key];
                    TreeNode tnForm = new TreeNode("  " + m.menu_name);
                    tnForm.Name = m.menu_name;

                    tnForm.Text = m.menu_name;
                    if (Language.Equals("cn"))
                        tnForm.Text = m.menu_name;
                    else if(Language.Equals("en") && !string.IsNullOrEmpty(m.ui_en))
                        tnForm.Text = m.ui_en;
                    else if (Language.Equals("hk") && !string.IsNullOrEmpty(m.ui_yn))
                        tnForm.Text = m.ui_yn; 

                    foreach (string key2 in m.children.Keys)
                    {
                        SJeMES_Framework.Web.JSONMenu m2 = m.children[key2];

                        TreeNode node = new TreeNode(m2.menu_name);
                        node.Name = m2.menu_name;

                        string m2_name = m2.menu_name;
                        if (Language.Equals("cn") )
                            node.Text = m2.menu_name;
                        else if (Language.Equals("en") && !string.IsNullOrEmpty(m2.ui_yn))
                            node.Text = m2.ui_en;
                        else if (Language.Equals("hk") && !string.IsNullOrEmpty(m2.ui_yn))
                            node.Text = m2.ui_yn; 

                        tnForm.Nodes.Add(node);
                    }

                    treeViewEx1.Nodes.Add(tnForm);
                }
            }
        }

        

        private void GetMenus()
        {
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.Menu", "GetSYSMenu", Program.Client.UserToken, string.Empty);

            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {


                Program.Menus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SJeMES_Framework.Web.JSONMenu>>(j["RetData"].ToString());
            }
            else
            {
                throw new Exception(j["ErrMsg"].ToString());
            }
        }

        private void GetMenuInfo2()
        {
           
            DataTable dt = new DataTable();
            Dictionary<string, object> data = new Dictionary<string, object>();
            string value = string.Empty;
            if (!string.IsNullOrEmpty(MenuP))
            {
                value = MenuP;
            }
            else
            {
                value = ucCombox1.SelectedValue;
            }
            data.Add("sql", @"
                                select 
                                menu_parent as '上级菜单(Superior Menu)',
                                menu_name as '菜单名称(Menu Name)',
	                            ui_en as 'Menu name',
	                            ui_yn as 'Tên trình đơn',
                                menu_info as '菜单描述(Menu Descript)',
                                menu_seq as '菜单顺序(Menu Order)'
                                from SYSMENU02M
                                where menu_parent ='" + value.Trim() + @"'
                                ORDER BY menu_seq
                                ");
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                try
                {

                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());
                    if (dt.Rows.Count==0)
                    {
                        Dictionary<string, object> q = new Dictionary<string, object>();
                        q.Add("sql", @"
                                        select 
                                        menu_parent as '上级菜单(Superior Menu)',
                                        menu_name as '菜单名称(Menu Name)',
	                                    ui_en as 'Menu name',
	                                    ui_yn as 'Tên trình đơn',
                                        menu_info as '菜单描述(Menu Descript)',
                                        menu_seq as '菜单顺序(Menu Order)',
                                        menu_action as '操作(Operation)',
                                        menu_dll as 'Dll',
                                        menu_class as 'Class',
                                        menu_method as 'Method',
                                        menu_url as 'Url',
                                        menu_module as 'Module',
                                        btnRole AS '权限中文(Permission Chinese)',
                                        btnRole_en as '权限英文(Permission Englist)',
                                        btnRole_yn as '权限越文(Permission Vietnam)'
                                        from SYSMENU03M
                                        where menu_parent ='" + value.Trim()+@"'
                                        order by menu_seq
                                        ");
                       retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(q));

                       j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                        if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                        {
                            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());
                        }
                        else
                        {
                            throw new Exception(j["ErrMsg"].ToString());
                        }
                    }
                }
                catch { }
                
            }
            else
            {
                throw new Exception(j["ErrMsg"].ToString());
            }

            if (dt != null)
            {
                dataGridView2.DataSource = dt.DefaultView;
            }
            else
            {
                dataGridView2.DataSource = new DataTable().DefaultView;
            }
            dataGridView2.Update();
            //SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, Program.Client, Program.Client.Language, Program.Client.WebServiceUrl, dataGridView2);
        }

        private void GetMenuInfo3(string menuname)
        {
            DataTable dttmp = new DataTable();
            DataTable dt = new DataTable();
            Dictionary<string, object> data = new Dictionary<string, object>();

            data = new Dictionary<string, object>();
            data.Add("sql", @"
                            select 
                            menu_parent as '上级菜单(Superior Menu)',
                            menu_name as '菜单名称(Menu Name)',
	                        ui_en as 'Menu name',
	                        ui_yn as 'Tên trình đơn',
                            menu_info as '菜单描述(Menu Descript)',
                            menu_seq as '菜单顺序(Menu Order)',
                            menu_action as '操作(Operation)',
                            menu_dll as 'Dll',
                            menu_class as 'Class',
                            menu_method as 'Method',
                            menu_url as 'Url',
                            menu_module as 'Module',
                            btnRole AS '权限中文(Permission Chinese)',
                            btnRole_en as '权限英文(Permission Englist)',
                            btnRole_yn as '权限越文(Permission Vietnam)'
                            from SYSMENU03M
                            where menu_parent ='" + menuname.Trim() + @"'
                            order by menu_seq
                            ") ; 

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                try
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());
                }
                catch { }

                
            }
            else
            {
                throw new Exception(j["ErrMsg"].ToString());
            }



            if (dt != null)
            {
                dataGridView2.DataSource = dt.DefaultView;
            }
            else
            {
                dataGridView2.DataSource = new DataTable().DefaultView;
            }
            dataGridView2.Update();
        }

        private void ucBtnImg1_BtnClick(object sender, EventArgs e)
        {
            try
            {
                string menu_parent = string.Empty;
                string menu_seq = string.Empty;
                if (dataGridView2.SelectedCells.Count > 0)
                {

                     menu_parent = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["上级菜单(Superior Menu)"].Value.ToString();
                     menu_seq = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["菜单顺序(Menu Order)"].Value.ToString();
                }
                else
                {
                    if (!String.IsNullOrEmpty(MenuP))
                    {
                        menu_parent = MenuP.Trim();
                    }
                    else
                    {
                        menu_parent = ucCombox1.SelectedValue.Trim();
                    }
                    
                     menu_seq = "1";
                }

                if(MenuLevel =="2")
                {
                    AddMENU2(menu_parent, menu_seq);
                }
                else
                {
                    AddMENU3(menu_parent, menu_seq);
                }
                   
                
            }
            catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void AddMENU2(string menu_parent, string menu_seq)
        {
            SJeMES_Control_Library.Forms.FrmInputs frm = new
               SJeMES_Control_Library.Forms.FrmInputs("新增菜单(Add Menu)",
               new string[] { "上级菜单(Superior Menu)", "菜单名称(Menu Name)", "菜单描述(Menu Descript)", "菜单顺序(Menu Order)", },
               new Dictionary<string, SJeMES_Control_Library.TextInputType>(),
               new Dictionary<string, string>(),
               new Dictionary<string, SJeMES_Control_Library.Controls.KeyBoardType>(),
               new List<string>() { "上级菜单(Superior Menu)", "菜单名称(Menu Name)", "菜单顺序(Menu Order)" },
               new Dictionary<string, string>()
               {
                { "上级菜单(Superior Menu)", menu_parent },
                { "菜单顺序(Menu Order)", menu_seq }}
               , new List<string>() { "上级菜单(Superior Menu)" }, 650);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                string sql = string.Empty;

                #region 判断菜单是否已经存在
                Dictionary<string, object> data = new Dictionary<string, object>();
                DataTable dt = new DataTable();
                data.Add("sql", @"select 1 from SYSMENU02M(nolock) where menu_parent='" + frm.Values[0] + "' and menu_name='" + frm.Values[1] + "'");
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                        throw new Exception("菜单名称【" + frm.Values[1] + "】已经存在了，请检查！");
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
                #endregion



                Dictionary<string, object> p = new Dictionary<string, object>();
                //p.Add("menu_parent", frm.Values[1]);
                //p.Add("menu_name", frm.Values[2]);
                //p.Add("menu_info", frm.Values[3]);
                //p.Add("menu_seq", frm.Values[4]);

                p.Add("menu_parent", frm.Values[0]);
                p.Add("menu_name", frm.Values[1]);
                p.Add("menu_info", frm.Values[2]);
                p.Add("menu_seq", frm.Values[3]);
                p.Add("menu_enable", "True"); 

                sql = SJeMES_Framework.Common.StringHelper.GetInsertSqlByDictionary("SqlServer", "SYSMENU02M", p);

                string sqlp = SJeMES_Framework.Common.StringHelper.GetSqlPByDictionary(p);
                string pname = SJeMES_Framework.Common.StringHelper.GetPNameByDictionary(p);


                data = new Dictionary<string, object>();
                data.Add("sql", sql);
                data.Add("sqlp", sqlp);
                data.Add("pname", pname);
                retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                {
                    GetMenus();
                    GetMenuInfo2();
                    MenuUpdate();
                }
                else
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }
            }
        }

        private void AddMENU3(string menu_parent,string menu_seq)
        {
            SJeMES_Control_Library.Forms.FrmInputs frm = new
               SJeMES_Control_Library.Forms.FrmInputs("新增菜单(Add Menu)",
               new string[] { "上级菜单(Superior Menu)", 
                   "菜单名称(Menu Name)",
                   "菜单描述(Menu Descript)", 
                   "菜单顺序(Menu Order)",
                   "操作(Operation)", 
                   "Dll",
                   "Class", 
                   "Method",
                   "Url", 
                   "Module",
                   "权限中文(Permission Chinese)",
                   "权限英文(Permission Englist)",
                   "权限越文(Permission Vietnam)" },
               new Dictionary<string, SJeMES_Control_Library.TextInputType>(),
               new Dictionary<string, string>(),
               new Dictionary<string, SJeMES_Control_Library.Controls.KeyBoardType>(),
               new List<string>() { "上级菜单(Superior Menu)", "菜单名称(Menu Name)", "菜单顺序(Menu Order)" },
               new Dictionary<string, string>()
               {
                { "上级菜单(Superior Menu)", menu_parent },
                { "菜单顺序(Menu Order)", menu_seq }}
               , new List<string>() { "上级菜单(Superior Menu)" }, 750);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                #region 判断菜单是否已经存在
                Dictionary<string, object> data = new Dictionary<string, object>();
                DataTable dt = new DataTable();
                data.Add("sql", @"select 1 from SYSMENU03M(nolock) where menu_parent='" + frm.Values[0] + "' and menu_name='" + frm.Values[1] + "'");
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                        throw new Exception("菜单名称【" + frm.Values[1] + "】已经存在了，请检查！");
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
                #endregion

                #region 注掉 20200413 by hedx
                //Dictionary<string, object> p = new Dictionary<string, object>(); 
                //p.Add("menu_parent", frm.Values[0]);
                //p.Add("menu_name", frm.Values[1]);
                //p.Add("menu_info", frm.Values[2]);
                //p.Add("menu_seq", frm.Values[3]);
                //p.Add("menu_action", frm.Values[4]);
                //p.Add("menu_dll", frm.Values[5]);
                //p.Add("menu_class", frm.Values[6]);
                //p.Add("menu_method", frm.Values[7]);
                //p.Add("menu_url", frm.Values[8]);
                //p.Add("menu_module", frm.Values[9]);
                //p.Add("menu_enable", "True");
                //p.Add("btnRole", frm.Values[10]); 
                //string sql = SJeMES_Framework.Common.StringHelper.GetInsertSqlByDictionary("SqlServer", "SYSMENU03M", p);
                //string sqlp = SJeMES_Framework.Common.StringHelper.GetSqlPByDictionary(p);
                //string pname = SJeMES_Framework.Common.StringHelper.GetPNameByDictionary(p);
                //data = new Dictionary<string, object>();
                //data.Add("sql", sql);
                //data.Add("sqlp", sqlp);
                //data.Add("pname", pname);
                //retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                //     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                #endregion

                #region 新 20200413 by hedx  
                string sql = @"insert into SYSMENU03M(menu_parent,menu_name,menu_info,menu_seq,menu_action,menu_dll,
                                              menu_class,menu_method,menu_url,menu_module,menu_enable,btnRole,btnRole_en,btnRole_yn)
                                             values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',N'{11}',N'{12}',N'{13}')";
                sql = string.Format(sql, frm.Values[0], frm.Values[1], frm.Values[2], frm.Values[3], frm.Values[4], frm.Values[5], frm.Values[6],
                    frm.Values[7], frm.Values[8], frm.Values[9],"True", frm.Values[10], frm.Values[11], frm.Values[12]);

                data = new Dictionary<string, object>();
                data.Add("sql", sql);
                data.Add("sqlp", "");
                data.Add("pname", "");
                retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                #endregion

                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                {
                    //GetMenus();
                    GetMenuInfo2();
                    //MenuUpdate();
                }
                else
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }
            }
        }

        private void ucBtnImg2_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedCells.Count > 0)
                {
                    if (MenuLevel == "2")
                    {
                        if (SJeMES_Control_Library.MessageHelper.ShowWarning(this, "是否删除选中的数据？删除二级菜单，三级菜单将无法关联。") == DialogResult.OK)
                        {
                            DelMENU2();
                        }
                    }
                    else
                    {
                        if (SJeMES_Control_Library.MessageHelper.ShowWarning(this, "是否删除选中的数据？") == DialogResult.OK)
                        {
                            DelMENU3();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void DelMENU2()
        {
            try
            {
                string name = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["菜单名称(Menu Name)"].Value.ToString();

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("menu_name", name);

                string sql = @"delete from SYSMENU02M where menu_name=@menu_name";
                string sqlp = SJeMES_Framework.Common.StringHelper.GetSqlPByDictionary(p);
                string pname = SJeMES_Framework.Common.StringHelper.GetPNameByDictionary(p);


                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("sql", sql);
                data.Add("sqlp", sqlp);
                data.Add("pname", pname);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                {

                    GetMenus();
                    GetMenuInfo2();
                    MenuUpdate();
                }
                else
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void DelMENU3()        {
            try
            {
                string name = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["菜单名称(Menu Name)"].Value.ToString();

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("menu_name", name);

                string sql = @"delete from SYSMENU03M where menu_name=@menu_name";
                string sqlp = SJeMES_Framework.Common.StringHelper.GetSqlPByDictionary(p);
                string pname = SJeMES_Framework.Common.StringHelper.GetPNameByDictionary(p);


                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("sql", sql);
                data.Add("sqlp", sqlp);
                data.Add("pname", pname);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                {

                    GetMenus();
                    GetMenuInfo2();
                    MenuUpdate();
                }
                else
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this.FindForm(), ex.Message);
            }
        }

        private void ucBtnImg4_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedCells.Count > 0)
                {
                    if(MenuLevel == "3")
                    {
                        EditMENU3();
                    }
                    else
                    {
                        EditMENU2();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void EditMENU2()
        {
            try
            {
                string menu_parent = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["上级菜单(Superior Menu)"].Value.ToString();
                string name = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["菜单名称(Menu Name)"].Value.ToString();
                string menu_info = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["菜单描述(Menu Descript)"].Value.ToString();
                string menu_seq = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["菜单顺序(Menu Order)"].Value.ToString();
                


                SJeMES_Control_Library.Forms.FrmInputs frm = new
            SJeMES_Control_Library.Forms.FrmInputs("修改菜单(Update Menu)",
            new string[] { "上级菜单(Superior Menu)", "菜单名称(Menu Name)", "菜单描述(Menu Descript)", "菜单顺序(Menu Order)",  },
            new Dictionary<string, SJeMES_Control_Library.TextInputType>(),
            new Dictionary<string, string>(),
            new Dictionary<string, SJeMES_Control_Library.Controls.KeyBoardType>(),
            new List<string>() { "上级菜单(Superior Menu)", "菜单名称(Menu Name)", "菜单顺序(Menu Order)" },
            new Dictionary<string, string>()
            {
                { "上级菜单(Superior Menu)", menu_parent },
                { "菜单名称(Menu Name)", name },
                { "菜单描述(Menu Descript)", menu_info },
                { "菜单顺序(Menu Order)", menu_seq }
              }
            , new List<string>() { "上级菜单(Superior Menu)", "菜单名称(Menu Name)" }, 650);
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();

                    p.Add("menu_parent", frm.Values[0]);
                    p.Add("menu_name", frm.Values[1]);
                    p.Add("menu_info", frm.Values[2]);
                    p.Add("menu_seq", frm.Values[3]);
                   

                    string sql = SJeMES_Framework.Common.StringHelper.GetUpdateSqlByDictionary("SYSMENU02M",
                        " menu_name =@menu_name ", p);

                    string sqlp = SJeMES_Framework.Common.StringHelper.GetSqlPByDictionary(p);
                    string pname = SJeMES_Framework.Common.StringHelper.GetPNameByDictionary(p);


                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("sql", sql);
                    data.Add("sqlp", sqlp);
                    data.Add("pname", pname);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                    var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                    {
                        GetMenus();
                        GetMenuInfo2();
                        MenuUpdate();
                    }
                    else
                    {
                        throw new Exception(ret["ErrMsg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void EditMENU3()
        {
            try
            {
                string menu_parent = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["上级菜单(Superior Menu)"].Value.ToString();
                string name = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["菜单名称(Menu Name)"].Value.ToString();
                string menu_info = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["菜单描述(Menu Descript)"].Value.ToString();
                string menu_seq = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["菜单顺序(Menu Order)"].Value.ToString();
                string action = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["操作(Operation)"].Value.ToString();
                string dllname = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["Dll"].Value.ToString();
                string classname = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["Class"].Value.ToString();
                string method = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["Method"].Value.ToString();
                string url = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["Url"].Value.ToString();
                string module = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["Module"].Value.ToString();
                string btnRole = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["权限中文(Permission Chinese)"].Value.ToString();
                string btnRole_en = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["权限英文(Permission Englist)"].Value.ToString();
                string btnRole_yn = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["权限越文(Permission Vietnam)"].Value.ToString();

                SJeMES_Control_Library.Forms.FrmInputs frm = new
            SJeMES_Control_Library.Forms.FrmInputs("修改菜单(Update Menu)",
            new string[] { "上级菜单(Superior Menu)",
                "菜单名称(Menu Name)", 
                "菜单描述(Menu Descript)", 
                "菜单顺序(Menu Order)",
                "操作(Operation)", 
                "Dll", "Class", 
                "Method", 
                "Url", 
                "Module",
                "权限中文(Permission Chinese)",
                "权限英文(Permission Englist)",
                "权限越文(Permission Vietnam)" },
            new Dictionary<string, SJeMES_Control_Library.TextInputType>(),
            new Dictionary<string, string>(),
            new Dictionary<string, SJeMES_Control_Library.Controls.KeyBoardType>(),
            new List<string>() { "上级菜单(Superior Menu)", "菜单名称(Menu Name)", "菜单顺序(Menu Order)" },
            new Dictionary<string, string>()
            {
                { "上级菜单(Superior Menu)", menu_parent },
                { "菜单名称(Menu Name)", name },
                { "菜单描述(Menu Descript)", menu_info },
                { "菜单顺序(Menu Order)", menu_seq },
                { "操作(Operation)", action },
                { "Dll", dllname },
                { "Class", classname },
                { "Method", method },
                { "Url", url },
                { "Module", module },
                { "权限中文(Permission Chinese)", btnRole },
                { "权限英文(Permission Englist)", btnRole_en },
                { "权限越文(Permission Vietnam)", btnRole_yn }}
            , new List<string>() { "上级菜单(Superior Menu)", "菜单名称(Menu Name)" }, 750);
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    #region 注掉 20200413 by hedx
                    //Dictionary<string, object> p = new Dictionary<string, object>();
                    //p.Add("menu_parent", frm.Values[0]);
                    //p.Add("menu_name", frm.Values[1]);
                    //p.Add("menu_info", frm.Values[2]);
                    //p.Add("menu_seq", frm.Values[3]);
                    //p.Add("menu_action", frm.Values[4]);
                    //p.Add("menu_dll", frm.Values[5]);
                    //p.Add("menu_class", frm.Values[6]);
                    //p.Add("menu_method", frm.Values[7]);
                    //p.Add("menu_url", frm.Values[8]);
                    //p.Add("menu_module", frm.Values[9]);
                    //p.Add("btnRole", frm.Values[10]);
                    //p.Add("btnRole_en", frm.Values[11]);
                    //p.Add("btnRole_yn", frm.Values[12]); 
                    //string sql = SJeMES_Framework.Common.StringHelper.GetUpdateSqlByDictionary("SYSMENU03M",
                    //    " menu_name =@menu_name ", p);
                    //string sqlp = SJeMES_Framework.Common.StringHelper.GetSqlPByDictionary(p);
                    //string pname = SJeMES_Framework.Common.StringHelper.GetPNameByDictionary(p);
                    //Dictionary<string, object> data = new Dictionary<string, object>();
                    //data.Add("sql", sql);
                    //data.Add("sqlp", sqlp);
                    //data.Add("pname", pname);
                    //string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                    //     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    #endregion

                    #region 新处理 20200413 by hedx 
                    string sql = @"update SYSMENU03M set menu_parent='{0}',menu_name='{1}',menu_info='{2}',menu_seq='{3}',menu_action='{4}',
                                                        menu_dll='{5}',menu_class='{6}',menu_method='{7}',menu_url='{8}',menu_module='{9}',
                                                        btnRole=N'{10}',btnRole_en=N'{11}',btnRole_yn=N'{12}'
                                    where menu_name='{1}'";
                    sql = string.Format(sql, frm.Values[0], frm.Values[1], frm.Values[2], frm.Values[3], 
                        frm.Values[4], frm.Values[5], frm.Values[6], frm.Values[7], frm.Values[8],
                        frm.Values[9], frm.Values[10], frm.Values[11], frm.Values[12]);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("sql", sql);
                    data.Add("sqlp", "");
                    data.Add("pname", "");
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    #endregion

                    var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                    {
                        GetMenus();
                        GetMenuInfo2(); 
                    }
                    else
                    {
                        throw new Exception(ret["ErrMsg"].ToString());
                    }
                }
                //var isaa = "".Split(',').Contains("aa");
            }
            catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetModuleList(textBox1.Text.Trim());
        }

        private void GetModuleList(string WhereVale)
        {
            try
            {
                string sql = @"
SELECT APP_Code as '模块代号',App_Name as '模块名称'
FROM SYSAPP01M
";
                if (!string.IsNullOrEmpty(WhereVale))
                {
                    sql += " WHERE APP_Code LIKE '%" + WhereVale + @"%' OR App_Name LIKE '%" + WhereVale + @"%'";
                }

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("sql", sql);
                
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    try
                    {
                        DataTable dttmp2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());

                        dataGridView3.DataSource = dttmp2.DefaultView;
                        dataGridView3.Update();
                    }
                    catch { }
                    
                }
                else
                {
                    throw new Exception(j["ErrMsg"].ToString());
                }

                //SJeMES_Framework.Common.UIHelper.UIdataGridView(this.Name, Program.Client, Program.Client.Language, Program.Client.WebServiceUrl, dataGridView3);


            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
           

        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                mod = "";
                bodyCount = 0;
                if (dataGridView3.SelectedCells.Count>0)
                {
                    values.Clear();
                    string ModuleCode = dataGridView3.Rows[dataGridView3.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                    appCode = ModuleCode;
                    title = dataGridView3.Rows[dataGridView3.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                    ModuleSettingHelper.GetFormClass(ModuleCode);
                    if(GetClient())
                    {
                        UpdateModuleUI(ModuleCode);
                    }

                }

            }catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);

            }
        }

        private void UpdateModuleUI(string ModuleCode)
        {
            SJeMES_Control_Library.Controls.UCModuleBase uCModuleBase;
            if (moi == "insert")
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(ModuleSettingHelper.FormClass);
                json = json.Replace("'", "''");

                string AppJson = "";
                if (values.ContainsKey(AppJson))
                {
                    values.Remove(AppJson);
                }
                values.Add(AppJson, json);
                if (ModuleSettingHelper.FormClass.PanelB != null)
                {
                    if (ModuleSettingHelper.FormClass.PanelB.Count > 0)
                    {
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(ModuleSettingHelper.FormClass.PanelB[nowBody]);
                        biodyTitle = ModuleSettingHelper.FormClass.PanelB[nowBody].Title;
                        if (values.ContainsKey(biodyTitle)) values.Remove(biodyTitle);
                        values.Add(biodyTitle, json);

                    }
                }


                uCModuleBase = new
                                 SJeMES_Control_Library.Controls.UCModuleBase(ModuleSettingHelper.FormClass, string.Empty, Program.Client, title);

                biodyTitle = "";
                json = "";
            }
            else
            {
                if (!string.IsNullOrEmpty(ModuleCode))
                {
                    uCModuleBase = new
                                     SJeMES_Control_Library.Controls.UCModuleBase(ModuleCode, string.Empty, Program.Client, title);
                }
                else
                {
                    if (moi != "insert")
                    {
                        ModuleSettingHelper.FormClass.APPCode = "";
                        ModuleSettingHelper.FormClass.APPName = "";
                        ModuleSettingHelper.FormClass.PanelB = null;
                        ModuleSettingHelper.FormClass.PanelH = null;
                        ModuleSettingHelper.FormClass.PanelHList = null;
                    }
                    String json = Newtonsoft.Json.JsonConvert.SerializeObject(ModuleSettingHelper.FormClass);
                    json = json.Replace("'", "''");

                    string AppJson = "";
                    if (values.ContainsKey(AppJson))
                    {
                        values.Remove(AppJson);
                    }
                    values.Add(AppJson, json);
                    if (ModuleSettingHelper.FormClass.PanelB != null)
                    {
                        if (ModuleSettingHelper.FormClass.PanelB.Count > 0)
                        {
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(ModuleSettingHelper.FormClass.PanelB[nowBody]);
                            biodyTitle = ModuleSettingHelper.FormClass.PanelB[nowBody].Title;
                            if (values.ContainsKey(biodyTitle)) values.Remove(biodyTitle);
                            values.Add(biodyTitle, json);

                        }
                    }


                    uCModuleBase = new
                                     SJeMES_Control_Library.Controls.UCModuleBase(ModuleSettingHelper.FormClass, string.Empty, Program.Client, title);

                    biodyTitle = "";
                    json = "";
                }
            }

            uCModuleBase.Status = SJeMES_Control_Library.Controls.UCModuleBase.ModuleStatus.Add;
            uCModuleBase.Dock = DockStyle.Fill;



            panel_Module.Controls.Clear();
            panel_Module.Controls.Add(uCModuleBase);
            panel_Module.AutoScroll = true;



            contextMenuStrip1.Items.Clear();
            ToolStripMenuItem tsmi0 = new ToolStripMenuItem("保存配置");
            tsmi0.Click += Tsmi_Click;
            contextMenuStrip1.Items.Add(tsmi0);
            ToolStripMenuItem tsmi = new ToolStripMenuItem("添加表身");
            tsmi.Click += Tsmi_Click;
            contextMenuStrip1.Items.Add(tsmi);
            ToolStripMenuItem tsmi2 = new ToolStripMenuItem("设置表头");
            tsmi2.Click += Tsmi_Click;
            contextMenuStrip1.Items.Add(tsmi2);

            if (ModuleSettingHelper.FormClass.PanelB != null)
            {
                foreach (SJeMES_Framework.Web.JSONPanelClassB b in ModuleSettingHelper.FormClass.PanelB)
                {
                    ToolStripMenuItem tsmi3 = new ToolStripMenuItem("设置表身[" + b.Title + "]");
                    tsmi3.Click += Tsmi_Click;
                    contextMenuStrip1.Items.Add(tsmi3);
                }
            }
        }

        private void Tsmi_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem menu = sender as ToolStripMenuItem;
                if(menu.Text == "添加表身")
                {
                    
                    mod = "add";
                    biodyTitle = "";
                    int row = 0;
                    if (ModuleSettingHelper.FormClass.PanelH != null)
                    {
                        if (ModuleSettingHelper.FormClass.PanelB!=null && ModuleSettingHelper.FormClass.PanelB.Count>0)
                        {
                            if (ModuleSettingHelper.FormClass.PanelB[0].Title == "")
                            {
                                row = 0;
                            }
                            else
                            {
                                SJeMES_Framework.Web.JSONPanelClassB jcb = new SJeMES_Framework.Web.JSONPanelClassB();
                                ModuleSettingHelper.FormClass.PanelB.Add(jcb);
                                row = ModuleSettingHelper.FormClass.PanelB.Count - 1;
                            }
                        }
                        else
                        {
                            SJeMES_Framework.Web.JSONPanelClassB jcb = new SJeMES_Framework.Web.JSONPanelClassB();
                            ModuleSettingHelper.FormClass.PanelB.Add(jcb);
                            row = ModuleSettingHelper.FormClass.PanelB.Count - 1;
                        }
                        nowBody = row;
                        ModuleSettingHelper.FormClass.PanelB[row].seq = row + 1;
                        FormEditBody frm = new FormEditBody(ModuleSettingHelper.FormClass.PanelB[row]);

                        frm.ShowDialog();
                        if (frm.IsSave)
                        {
                            ModuleSettingHelper.FormClass.PanelB[row] = frm.B;
                            //ModuleSettingHelper.FormClass.PanelB[bodyCount].Title = "";
                            //ModuleSettingHelper.FormClass.PanelB[bodyCount].seq = 1;
                            //ModuleSettingHelper.FormClass.PanelB[bodyCount].table = "";
                            //ModuleSettingHelper.FormClass.PanelB[bodyCount].tableKeys = frm.HeadKeys;

                            //ModuleSettingHelper.FormClass.PanelB[bodyCount].HeadKeys = frm.HeadKeys;
                            UpdateModuleUI(string.Empty);
                            bodyCount++;
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先设置表头！");
                    }
                   
                }
                else if (menu.Text == "保存配置")
                {
                    if(values.Count>0)
                    {
                        foreach (string key in values.Keys)
                        {
                            //string sql = "INSERT INTO [SJEMSSYS].[dbo].[DEMO] ([JsonTest],[AppCode],[AppName]) VALUES ('" + values[key] + "','" + appCode + "','" + key + "');";
                            string sql = string.Empty;
                            if (string.IsNullOrEmpty(key))
                            {
                                if (mod == "addHead")
                                {
                                    string keys = string.Empty;

                                    if (ModuleSettingHelper.FormClass.PanelH.tableKeys.Count>0)
                                    {
                                        keys = "['" + ModuleSettingHelper.FormClass.PanelH.tableKeys[0] + "']";
                                    }
                                    //string ss = values;

                                    JObject jo = (JObject)JsonConvert.DeserializeObject(values[key]);
                                    string PanelH = jo["PanelH"].ToString();
                                    sql += @"INSERT INTO [SJEMSSYS].[dbo].[SYSAPP01M] ([APP_Code], [App_Name],[APP_XML], [App_Json],[App_TableH],[App_TableKeysH],[App_JsonH],[App_JsonHList]) 
values('" + appCode + "','" + appName + "' ,'','" + values[key] + "','" + tableName + "','" + keys.Replace("'", "\"") + "','"+ PanelH + "','"+ App_JsonHList + "'); ";
                                }
                                else
                                {
                                    string keys = string.Empty;
                                    if (ModuleSettingHelper.FormClass.PanelH.tableKeys.Count > 0)
                                    {
                                        keys = "['" + ModuleSettingHelper.FormClass.PanelH.tableKeys[0] + "']";
                                    }
                                    JObject jo = (JObject)JsonConvert.DeserializeObject(values[key]);
                                    string PanelH = jo["PanelH"].ToString();
                                    //sql += "UPDATE TOP(1) [SJEMSSYS].[dbo].[SYSAPP01M] SET [App_Json]=N'" + values[key] + "'WHERE([APP_Code] = N'" + appCode + "'); ";
                                    sql += @"if((select isnull(COUNT(1),0) from [SJEMSSYS].[dbo].[SYSAPP01M] where [APP_Code] = N'" + appCode + "')>0)" +
                                        "update TOP(1) [SJEMSSYS].[dbo].[SYSAPP01M] SET[App_Json]=N'" + values[key] + "' WHERE([APP_Code] = N'" + appCode + "')" +
                                        "else " +
                                        "INSERT INTO[SJEMSSYS].[dbo].[SYSAPP01M]([APP_Code], [App_Name],[APP_XML], [App_Json],[App_TableH],[App_TableKeysH],[App_JsonH],[App_JsonHList])" +
                                        "values('" + appCode + "','" + appName + "' ,'','" + values[key] + "','" + tableName + "','" + keys.Replace("'", "\"") + "','" + PanelH + "','"+ App_JsonHList + "')";
                                }
                                if (dtMore!=null && dtMore.Rows.Count>0)
                                {
                                    for (int i = 0; i < dtMore.Rows.Count; i++)
                                    {
                                        sql += @" if((select COUNT(*) from [SJEMSSYS].[dbo].[SYSAPP01A2] where App_Code='"+ appCode + "' and Title='"+dtMore.Rows[i]["Title"] + @"')>0)
 update[SJEMSSYS].[dbo].[SYSAPP01A2] set [Title] = '" + dtMore.Rows[i]["Title"] + @"',[Action]='" + dtMore.Rows[i]["Action"] + @"',[Url]='" + dtMore.Rows[i]["Url"] + @"',[DllName]='" + dtMore.Rows[i]["DllName"] + @"',
[ClassName]='" + dtMore.Rows[i]["ClassName"] + @"',[Method]='" + dtMore.Rows[i]["Method"] + @"',[Parameters]='" + dtMore.Rows[i]["Parameters"] + @"'
 where App_Code = '" + appCode + "' and Title = '" + dtMore.Rows[i]["Title"] + @"'
 else 
 insert into[SJEMSSYS].[dbo].[SYSAPP01A2] ([App_Code],[Title],[Action],[Url],[DllName],[ClassName],[Method],[Parameters]) values('"+appCode+"','"+ dtMore.Rows[i]["Title"] + "'," +
 "'"+ dtMore.Rows[i]["Action"] + "','"+ dtMore.Rows[i]["Url"] + "','" + dtMore.Rows[i]["DllName"] + "','" + dtMore.Rows[i]["ClassName"] + "','" + dtMore.Rows[i]["Method"] + "','" + dtMore.Rows[i]["Parameters"] + "')";
                                    }
                                }
                            }
                            else
                            {
                                if (mod == "add")
                                {
                                    for (int i = 0; i < ModuleSettingHelper.FormClass.PanelB.Count; i++)
                                    {
                                        string keysB = string.Empty;
                                        string keys = string.Empty;
                                        if (ModuleSettingHelper.FormClass.PanelB[i].Title == key)
                                        {
                                            if (ModuleSettingHelper.FormClass.PanelB[i].tableKeys.Count>0)
                                            {
                                                for (int j = 0; j < ModuleSettingHelper.FormClass.PanelB[i].tableKeys.Count; j++)
                                                {
                                                    if (string.IsNullOrEmpty(keysB))
                                                    {
                                                        keysB= string.Format("\"{0}\"", ModuleSettingHelper.FormClass.PanelB[i].tableKeys[j]);
                                                    }
                                                    else
                                                    {
                                                        keysB += ","+string.Format("\"{0}\"", ModuleSettingHelper.FormClass.PanelB[i].tableKeys[j]);
                                                    }
                                                }
                                                keysB = "[" + keysB + "]";
                                            }
                                            
                                            if (ModuleSettingHelper.FormClass.PanelB[i].HeadKeys.Count > 0)
                                            {
                                                keys = "['" + ModuleSettingHelper.FormClass.PanelB[i].HeadKeys[0] + "']";
                                            }
                                            //                                            sql += @"INSERT INTO[SJEMSSYS].[dbo].[SYSAPP01A1]
                                            //        ([APP_Code], [BodyTitle], [Seq], [APP_JSON], [TableB], [TableKeysB], [HeadKeys])
                                            //VALUES
                                            //(N'" + appCode + "', N'"+ key + "', N'"+ (i+1) + "', '" + values[key] + "', N'" + ModuleSettingHelper.FormClass.PanelB[i].table + "', N'"+ keysB + "', N'" + keys.Replace("'", "\"") + "');";
                                            sql += @"if((select isnull(COUNT(1),0) from [SJEMSSYS].[dbo].[SYSAPP01A1] where ([APP_Code] = N'" + appCode + "') and ([BodyTitle]=N'" + key + "'))>0)" +
                                                "UPDATE TOP(1) [SJEMSSYS].[dbo].[SYSAPP01A1] SET[APP_JSON]=N'" + values[key].Replace("'", "''") + "' WHERE([APP_Code] = N'" + appCode + "') and ([BodyTitle]=N'" + key + "')" +
                                                "else " +
                                                "INSERT INTO[SJEMSSYS].[dbo].[SYSAPP01A1]([APP_Code], [BodyTitle], [Seq], [APP_JSON], [TableB], [TableKeysB], [HeadKeys]) " +
                                                "VALUES(N'" + appCode + "', N'" + key + "', N'" + (i + 1) + "', '" + values[key].Replace("'", "''") + "', N'" + ModuleSettingHelper.FormClass.PanelB[i].table + "', N'" + keysB + "', N'" + keys.Replace("'", "\"") + "')";

                                        }
                                    }
                                    
                                }
                                else
                                {
                                    #region new
                                    for (int i = 0; i < ModuleSettingHelper.FormClass.PanelB.Count; i++)
                                    {
                                        string keysB = string.Empty;
                                        string keys = string.Empty;
                                        if (ModuleSettingHelper.FormClass.PanelB[i].Title == key)
                                        {
                                            if (ModuleSettingHelper.FormClass.PanelB[i].tableKeys.Count > 0)
                                            {
                                                for (int j = 0; j < ModuleSettingHelper.FormClass.PanelB[i].tableKeys.Count; j++)
                                                {
                                                    if (string.IsNullOrEmpty(keysB))
                                                    {
                                                        keysB = string.Format("\"{0}\"", ModuleSettingHelper.FormClass.PanelB[i].tableKeys[j]);
                                                    }
                                                    else
                                                    {
                                                        keysB += "," + string.Format("\"{0}\"", ModuleSettingHelper.FormClass.PanelB[i].tableKeys[j]);
                                                    }
                                                }
                                                keysB = "[" + keysB + "]";
                                            }

                                            if (ModuleSettingHelper.FormClass.PanelB[i].HeadKeys.Count > 0)
                                            {
                                                keys = "['" + ModuleSettingHelper.FormClass.PanelB[i].HeadKeys[0] + "']";
                                            }
                                            sql += @"if((select isnull(COUNT(1),0) from [SJEMSSYS].[dbo].[SYSAPP01A1] where ([APP_Code] = N'" + appCode + "') and ([BodyTitle]=N'" + key + "'))>0)" +
                                                "UPDATE TOP(1) [SJEMSSYS].[dbo].[SYSAPP01A1] SET[APP_JSON]=N'" + values[key].Replace("'", "''") + "',[TableKeysB]='"+ keysB + "',[HeadKeys]='"+ keys.Replace("'", "\"") + "' WHERE([APP_Code] = N'" + appCode + "') and ([BodyTitle]=N'" + key + "')" +
                                                "else " +
                                                "INSERT INTO[SJEMSSYS].[dbo].[SYSAPP01A1]([APP_Code], [BodyTitle], [Seq], [APP_JSON], [TableB], [TableKeysB], [HeadKeys]) " +
                                                "VALUES(N'" + appCode + "', N'" + key + "', N'" + (i + 1) + "', '" + values[key].Replace("'", "''") + "', N'" + ModuleSettingHelper.FormClass.PanelB[i].table + "', N'" + keysB + "', N'" + keys.Replace("'", "\"") + "')";

                                        }
                                    }
                                    #endregion

                                    // sql += "UPDATE TOP(1) [SJEMSSYS].[dbo].[SYSAPP01A1] SET [APP_JSON]=N'" + values[key].Replace("'","''") + "'WHERE([APP_Code] = N'" + appCode + "') and ([BodyTitle]=N'" + key + "'); ";
                                    //sql += @"if((select isnull(COUNT(1),0) from [SJEMSSYS].[dbo].[SYSAPP01A1] where ([APP_Code] = N'" + appCode + "') and ([BodyTitle]=N'" + key + "'))>0)" +
                                    //    "UPDATE TOP(1) [SJEMSSYS].[dbo].[SYSAPP01A1] SET[APP_JSON]=N'" + values[key].Replace("'","''") + "' WHERE([APP_Code] = N'" + appCode + "') and ([BodyTitle]=N'" + key + "')" +
                                    //    "else " +
                                    //    "INSERT INTO[SJEMSSYS].[dbo].[SYSAPP01A1]([APP_Code], [BodyTitle], [Seq], [APP_JSON], [TableB], [TableKeysB], [HeadKeys]) " +
                                    //    "VALUES(N'" + appCode + "', N'"+ key + "', N'"+ (i+1) + "', '" + values[key] + "', N'" + ModuleSettingHelper.FormClass.PanelB[i].table + "', N'"+ keysB + "', N'" + keys.Replace("'", "\"") + "')";
                                }
                            }
                            //Dictionary<string, object> p = new Dictionary<string, object>();
                            //p.Add("sql", sql);

                            //string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                            //         "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            //var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                            //if (!Convert.ToBoolean(j["IsSuccess"].ToString()))
                            //{
                            //    throw new Exception(j["ErrMsg"].ToString());
                            //}
                            Program.Client.SYSExecuteNonQuery(sql);
                        }
                    }
                    MessageBox.Show("保存成功");
                    GetModuleList(textBox1.Text.Trim());
                }
                else if(menu.Text == "设置表头")
                {
                    FormEditHead frm = new FormEditHead(ModuleSettingHelper.FormClass.PanelH);
                    frm.ShowDialog();
                    moi = "insert";
                    if (frm.IsSave)
                    {
                        dtMore = frm.dtMore;
                        ModuleSettingHelper.FormClass.PanelH = frm.H;
                        if (mod == "addHead")
                        {
                            App_JsonHList = frm.App_JsonHList;
                            ModuleSettingHelper.FormClass.APPCode = frm.AppCpde;
                            ModuleSettingHelper.FormClass.APPName = frm.AppName;
                            ModuleSettingHelper.FormClass.PanelB = new List<SJeMES_Framework.Web.JSONPanelClassB>();
                            //ModuleSettingHelper.FormClass.PanelHList = new SJeMES_Framework.Web.JSONPanelClassHList();
                            appCode = frm.AppCpde;
                            key = frm.Key;
                            appName = frm.AppName;
                            tableName = frm.H.table;
                        }
                        if (!string.IsNullOrEmpty(frm.AppCpde))
                        {
                            //moi = "insert";
                            UpdateModuleUI(frm.AppCpde);
                        }
                        else
                        {
                            UpdateModuleUI(string.Empty);
                        }
                        

                    }
                }
                else if(menu.Text.StartsWith("设置表身"))
                {
                    moi = "insert";
                    string Title = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(menu.Text, "设置表身[", "]");
                    biodyTitle = Title;
                    for (int i=0;i< ModuleSettingHelper.FormClass.PanelB.Count;i++)
                    {
                        if(ModuleSettingHelper.FormClass.PanelB[i].Title == Title)
                        {
                            nowBody = i;
                            String json = Newtonsoft.Json.JsonConvert.SerializeObject(ModuleSettingHelper.FormClass);
                            String json1 = Newtonsoft.Json.JsonConvert.SerializeObject(ModuleSettingHelper.FormClass.PanelB[i]);

                            FormEditBody frm = new FormEditBody(ModuleSettingHelper.FormClass.PanelB[i]);
                            frm.ShowDialog();
                            if (frm.IsSave)
                            {
                                ModuleSettingHelper.FormClass.PanelB[i] = frm.B;
                                UpdateModuleUI(string.Empty);

                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this,ex.Message);
            }
        }

        private bool GetClient()
        {
            try { 

                if(string.IsNullOrEmpty(Program.Client.UserToken))
                {
                    frmLogin frm = new frmLogin();
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(Program.Client.UserToken))

                        return true;
                    else
                        return false;
                }
                else
                {
                    return true;
                }

            } catch (Exception ex) { SJeMES_Control_Library.MessageHelper.ShowErr(this,ex.Message); return false; }
        }

        private void treeViewEx1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MenuLevel = "3";
            MenuP = treeViewEx1.SelectedNode.Name;
            GetMenuInfo3(treeViewEx1.SelectedNode.Name);

        }
        //添加
        private void ucBtnImg5_BtnClick(object sender, EventArgs e)
        {
            UpdateModuleUI(string.Empty);
            mod = "addHead";
            moi = string.Empty;
            GetOrg();
        }

        private void ucBtnImg6_BtnClick(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count>0)
            {
                int index = dataGridView3.CurrentRow.Index;    //取得选中行的索引  

                string  id= dataGridView3.Rows[index].Cells["模块代号"].Value.ToString();   //获取单元格列名为‘Id’的值
                string name = dataGridView3.Rows[index].Cells["模块名称"].Value.ToString();   //获取单元格列名为‘name’的值
                if (string.IsNullOrEmpty(name))
                {
                    name = id;
                }
                if (!string.IsNullOrEmpty(id))
                {
                    if (MessageBox.Show("确认删除"+name+"吗？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string sql = "delete from SYSAPP01M where APP_Code='" + id + "'";
                        sql += "delete from SYSAPP01A1 where APP_Code='" + id + "'";
                        Program.Client.SYSExecuteNonQuery(sql);
                        MessageBox.Show("删除成功！");
                        //GetOrg();
                        GetModuleList(textBox1.Text.Trim());
                    }
                }
                else
                {
                    MessageBox.Show("请选中删除行！");
                }
            }
        }
        private void treeViewEx1_MouseClick(object sender, MouseEventArgs e)
        {
            MenuLevel = "3";
            MenuP = treeViewEx1.TopNode.FullPath;
            GetMenuInfo3(treeViewEx1.TopNode.FullPath);
        }
    }
}
