using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SJeMES;
using SJeMES_Framework.Web;

namespace SJeMESClient
{
    public partial class frmMain : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public frmMain()
        {
            InitializeComponent();
            //加载主页面顶部的菜单
            GetMenuData();

            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
               Program.SkinThemes, materialSkinManager, this);

            Program.DicModuleLists = new Dictionary<string, SJeMES_Control_Library.Controls.UCModuleBaseList>();
            Program.DicModules = new Dictionary<string, SJeMES_Control_Library.Controls.UCModuleBase>();
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);

            ControlHelper.FreezeControl(this, true);

            
            //ucNavigationMenu1.Refresh();
            //ucNavigationMenu1.Items[0].ShowTip = true;
            //ucNavigationMenu1.Items[6].Text = Program.Client.UserCode;
            ////ucNavigationMenu1.Update();
            ////ucNavigationMenu1.Refresh();
            ////UpdateMenu(ucNavigationMenu1.Items[0].Name);
            ////ucNavigationMenu1.Refresh();


            //for (int i = 0; i < ucNavigationMenu1.Items.Length; i++)
            //{
            //    foreach (SJeMES_Framework.Web.JSONMenu m in Program.Menus)
            //    { 
            //        if (m.menu_name == ucNavigationMenu1.Items[i].Name)
            //        {
            //            if (Program.Client.Language == "en" && !string.IsNullOrEmpty(m.ui_en))
            //            {
            //                ucNavigationMenu1.Items[i].Text = m.ui_en;
            //            }
            //            else if (Program.Client.Language == "en" && !string.IsNullOrEmpty(m.ui_yn))
            //            {
            //                ucNavigationMenu1.Items[i].Text = m.ui_yn;
            //            }
            //        }
            //    }
            //} 
            //ucNavigationMenu1.Visible = true;
            //ucNavigationMenu1.Dock = DockStyle.Top;
            //ucNavigationMenu1.Location = new Point(0, 0);
            //ucNavigationMenu1.Refresh();
            //ucNavigationMenu1.Update();

        }

        private void OpenModule(string Title, string ModuleCode)
        {
            List<string> removeDicModuleList = new List<string>();
            foreach (var item in Program.DicModuleLists.Keys)
            {
                if(!tab_Module.TabPages.ContainsKey("tp_" + item))
                {
                    removeDicModuleList.Add(item);
                }
            }
            foreach (var item in removeDicModuleList)
            {
                Program.DicModuleLists.Remove(item);
            }
            if (!Program.DicModuleLists.ContainsKey(ModuleCode))
            {
                SJeMES_Control_Library.Controls.UCModuleBaseList module = new SJeMES_Control_Library.Controls.UCModuleBaseList(ModuleCode, Program.Client,Title);
                //module.ModuleCode = "";
                Program.DicModuleLists.Add(module.ModuleCode, module);
                module.Dock = DockStyle.Fill;
                module.SeeData += Module_SeeData;
                module.EditData += Module_EditData;
                module.AddData += Module_AddData;

                TabPage tp = new TabPage(Title);
                tp.Name = "tp_" + module.ModuleCode; 
                tab_Module.TabPages.Add(tp);
                tab_Module.SelectedTab = tp;
                //if (module.Data == null) return;

                tp.Controls.Add(module);

                
            }
            else
            {
                SJeMES_Control_Library.Controls.UCModuleBaseList module = Program.DicModuleLists[ModuleCode];
                module.Dock = DockStyle.Fill;

                bool IsClose = true;
                foreach(TabPage tp in tab_Module.TabPages)
                {
                    if(tp.Name == "tp_" + module.ModuleCode)
                    {

                        tp.Controls.Clear();
                        tp.Controls.Add(module);
                        tab_Module.SelectedTab = tp;
                        IsClose = false;
                    }
                }

                if (IsClose)
                {
                    TabPage tp = new TabPage(Title);
                    tp.Name = "tp_" + module.ModuleCode;
                    tab_Module.TabPages.Add(tp);
                    tab_Module.SelectedTab = tp;
                    tp.Controls.Add(module);

                    
                    
                }
            }
        }

        private void Module_AddData(object sender, EventArgs e)
        {
            SJeMES_Control_Library.Controls.UCModuleBaseList modulelist = sender as SJeMES_Control_Library.Controls.UCModuleBaseList;
            SJeMES_Control_Library.Controls.UCModuleBase module;
            if (!Program.DicModules.ContainsKey(modulelist.ModuleCode))
            {
                module = new SJeMES_Control_Library.Controls.UCModuleBase(modulelist.ModuleCode, string.Empty, Program.Client, modulelist.title);
                module.Back += Module_Back;


            }
            else
            {
                module = Program.DicModules[modulelist.ModuleCode];

            }
            module.Status = SJeMES_Control_Library.Controls.UCModuleBase.ModuleStatus.Add;
            module.Dock = DockStyle.Fill;

            TabPage tp = modulelist.Parent as TabPage;

            tp.Controls.Clear();
            tp.Controls.Add(module);
        }

        private void Module_EditData(object sender, EventArgs e)
        {
            SJeMES_Control_Library.Controls.UCModuleBaseList modulelist = sender as SJeMES_Control_Library.Controls.UCModuleBaseList;
            SJeMES_Control_Library.Controls.UCModuleBase module;
            if (!Program.DicModules.ContainsKey(modulelist.ModuleCode))
            {
                module = new SJeMES_Control_Library.Controls.UCModuleBase(modulelist.ModuleCode, modulelist.SelectedId, Program.Client, modulelist.title);
                module.Back += Module_Back;
              

            }
            else
            {
                module = Program.DicModules[modulelist.ModuleCode];
                module.DataId = modulelist.SelectedId;


            }
            module.Status = SJeMES_Control_Library.Controls.UCModuleBase.ModuleStatus.Edit;
            module.Dock = DockStyle.Fill;

            TabPage tp = modulelist.Parent as TabPage;

            tp.Controls.Clear();
            tp.Controls.Add(module);
        }

        private void Module_SeeData(object sender, EventArgs e)
        {
            SJeMES_Control_Library.Controls.UCModuleBaseList modulelist = sender as SJeMES_Control_Library.Controls.UCModuleBaseList;
            SJeMES_Control_Library.Controls.UCModuleBase module;
            if (!Program.DicModules.ContainsKey(modulelist.ModuleCode))
            {
                 module = new SJeMES_Control_Library.Controls.UCModuleBase(modulelist.ModuleCode, modulelist.SelectedId, Program.Client, modulelist.title);
                module.Back += Module_Back;
                

            }
            else
            {
                module = Program.DicModules[modulelist.ModuleCode];

            }
            module.Status = SJeMES_Control_Library.Controls.UCModuleBase.ModuleStatus.See;
            module.Dock = DockStyle.Fill;

            TabPage tp = modulelist.Parent as TabPage;

            tp.Controls.Clear();
            tp.Controls.Add(module);
        }

        private void Module_Back(object sender, EventArgs e)
        {
            SJeMES_Control_Library.Controls.UCModuleBase module = sender as SJeMES_Control_Library.Controls.UCModuleBase;
            SJeMES_Control_Library.Controls.UCModuleBaseList modulelist;

            modulelist = Program.DicModuleLists[module.ModuleCode];
            modulelist.GetData();

            modulelist.Dock = DockStyle.Fill;
            TabPage tp = module.Parent as TabPage;

            tp.Controls.Clear();
            tp.Controls.Add(modulelist);
        }

        /// <summary>
        /// 根据父级菜单  加载 子级的菜单
        /// </summary>
        /// <param name="MenuName"></param>
        private void UpdateMenu(string MenuName)
        {
            try
            {
                Program.MenusInfo = new Dictionary<string, SJeMES_Framework.Web.JSONMenu>();
                SJeMES_Framework.Web.JSONMenu menu = new SJeMES_Framework.Web.JSONMenu();
                this.treeViewEx1.Nodes.Clear();
                foreach (SJeMES_Framework.Web.JSONMenu m in Program.Menus)
                {

                    if (m.menu_name == MenuName)
                    {
                        menu = m;
                        break;
                    }
                }

                //遍历 二级菜单
                foreach (string key in menu.children.Keys)
                {
                    SJeMES_Framework.Web.JSONMenu m = menu.children[key];
                    Program.MenusInfo.Add(m.menu_name, m);

                    TreeNode tnForm = new TreeNode("  " + m.menu_name);
                    tnForm.Name = m.menu_name;
                    if (Program.Client.Language=="en" && !string.IsNullOrEmpty(m.ui_en))
                    {
                        //tnForm.Name = m.ui_en;
                        tnForm.Text= m.ui_en;
                    }
                    else if (Program.Client.Language == "hk" && !string.IsNullOrEmpty(m.ui_yn))
                    {
                        //tnForm.Name = m.ui_yn;
                        tnForm.Text = m.ui_yn;
                    }
                    else
                    {
                        tnForm.Name = m.menu_name;
                    }

                    //遍历 三级菜单
                    foreach (string key2 in m.children.Keys)
                    {
                        SJeMES_Framework.Web.JSONMenu m2 = m.children[key2];
                        Program.MenusInfo.Add(m2.menu_name, m2);
                        TreeNode node = new TreeNode(m2.menu_name);
                        node.Name = m2.menu_name;
                        if (Program.Client.Language == "en" && !string.IsNullOrEmpty(m2.ui_en))
                        {
                            //node.Name = m2.ui_en;
                            node.Text = m2.ui_en;
                        }
                        else if (Program.Client.Language == "hk" && !string.IsNullOrEmpty(m2.ui_yn))
                        {
                            //node.Name = m2.ui_yn;
                            node.Text = m2.ui_yn;
                        }
                        else
                        {
                            node.Name = m2.menu_name;
                        }

                        tnForm.Nodes.Add(node);
                    }

                    //string sql = string.Empty;
                    //sql = @"SELECT menu_name FROM SYSMENU01M where (ui_cn='"+m.menu_name+ "' or ui_en='" + m.menu_name + "' or ui_yn='" + m.menu_name + "')";
                    //DataTable dt_SYSMENU01M = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebService.Url, sql, new Dictionary<string, string>());
                    //if (dt_SYSMENU01M!=null)
                    //{
                    //    if (dt_SYSMENU01M.Rows.Count>0)
                    //    {
                    //        if (dt_SYSMENU01M.Rows[0][0].ToString() != "质量管理")
                    //        {
                    //            foreach (string key2 in m.children.Keys)
                    //            {
                    //                SJeMES_Framework.Web.JSONMenu m2 = m.children[key2];
                    //                Program.MenusInfo.Add(m2.menu_name, m2);
                    //                TreeNode node = new TreeNode(m2.menu_name);
                    //                node.Name = m2.menu_name;
                    //                if (Program.Client.Language == "en" && !string.IsNullOrEmpty(m2.ui_en))
                    //                {
                    //                    //node.Name = m2.ui_en;
                    //                    node.Text = m2.ui_en;
                    //                }
                    //                else if (Program.Client.Language == "hk" && !string.IsNullOrEmpty(m2.ui_yn))
                    //                {
                    //                    //node.Name = m2.ui_yn;
                    //                    node.Text = m2.ui_yn;
                    //                }
                    //                else
                    //                {
                    //                    node.Name = m2.menu_name;
                    //                }

                    //                tnForm.Nodes.Add(node);
                    //            }
                    //        }
                    //    }
                    //}
                   
                   

                    treeViewEx1.Nodes.Add(tnForm);
                }

            }
            catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void ucNavigationMenuExt1_ClickItemed(object sender, EventArgs e)
        {
            foreach(SJeMES_Control_Library.Controls.NavigationMenuItem i in ucNavigationMenu1.Items)
            {
                i.ShowTip = false;
            }

            if(ucNavigationMenu1.SelectItem.ParentItem==null)
            {
                ucNavigationMenu1.SelectItem.ShowTip = true;

                ucNavigationMenu1.Refresh();

                UpdateMenu(ucNavigationMenu1.SelectItem.Name);
            }

            if (ucNavigationMenu1.SelectItem.Name == "修改密码")
            {
                frmUserSetting frmUser = new frmUserSetting(Program.Client.UserCode);
                frmUser.ShowDialog();
            }

            if (ucNavigationMenu1.SelectItem.Name == "个人信息修改")
            {
                Frm_AddEmployee frm = new Frm_AddEmployee(Program.Client.UserCode, Program.Client);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }

            if (ucNavigationMenu1.SelectItem.Name == "退出系统")
            {
                Program.FrmLogin.Show();
                Program.IsExit = false;
                Program.FrmMain.Close();
            }

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Program.IsExit)
            {
                Program.FrmLogin.Close();
                Application.Exit();
            }
          
          
        }

        private void treeViewEx1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {

                if (Program.MenusInfo.ContainsKey(treeViewEx1.SelectedNode.Name))
                {

                    SJeMES_Framework.Web.JSONMenu menu = Program.MenusInfo[treeViewEx1.SelectedNode.Name];
                    switch (menu.menu_action.ToLower())
                    {

                        case "runmodule":
                            if (!string.IsNullOrEmpty(menu.menu_module))
                                if (menu.menu_module.StartsWith("PC_"))
                                {
                                    //OpenModule(menu.menu_name, menu.menu_module);
                                    OpenModule(treeViewEx1.SelectedNode.Text, menu.menu_module);
                                }
                                else
                                {
                                    //OpenModule(menu.menu_name,"PC_"+ menu.menu_module);
                                    OpenModule(treeViewEx1.SelectedNode.Text, "PC_" + menu.menu_module);
                                }                            
                            break;
                        case "accessweb":
                            if (!string.IsNullOrEmpty(menu.menu_url))
                                System.Diagnostics.Process.Start(menu.menu_url);
                            break;
                        case "runclientapp":
                            if(!string.IsNullOrEmpty(menu.menu_dll) 
                                && !string.IsNullOrEmpty(menu.menu_class)
                                && !string.IsNullOrEmpty(menu.menu_method))
                            {
                                RunClientApp(menu.menu_dll, menu.menu_class, menu.menu_method,menu.menu_name);
                            }
                            break;
                        case "runapp":
                            if (!string.IsNullOrEmpty(menu.menu_dll)
                                && !string.IsNullOrEmpty(menu.menu_class)
                                && !string.IsNullOrEmpty(menu.menu_method))
                            {
                                RunApp(menu);
                            }
                            break;
                    }
                    #region  质量管理
                    //string sql = string.Empty;
                    //sql = @"SELECT menu_name FROM SYSMENU01M where (ui_cn='" + menu.menu_parent + "' or ui_en='" + menu.menu_parent + "' or ui_yn='" + menu.menu_parent + "')";
                    //DataTable dt_SYSMENU01M = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebService.Url, sql, new Dictionary<string, string>());
                    //if (dt_SYSMENU01M != null)
                    //{
                    //    if (dt_SYSMENU01M.Rows.Count > 0)
                    //    {
                    //        if (dt_SYSMENU01M.Rows[0][0].ToString()=="质量管理")
                    //        {
                    //            //RunFrom(menu.menu_name, Program.Client, menu.children);
                    //        }
                    //    }
                    //}

                    #endregion

                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
           
        }

        public void RunFrom(string name, SJeMES_Framework.Class.ClientClass org, Dictionary<string, JSONMenu> menus)
        {
            if (!Program.DicModuleLists.ContainsKey(name))
            {
                //AQS_PEP_APE_101.F_QCM_TrackIn_Main frm = new AQS_PEP_APE_101.F_QCM_TrackIn_Main(name, org, menus);
                ////Program.DicModuleLists.Add(name, frm);
                //TabPage tp = new TabPage();
                ////tp.Name = name;
                ////tp.Text = name;
                //frm.Dock = DockStyle.Fill;
                //tab_Module.TabPages.Add(tp);
                //tab_Module.SizeMode = TabSizeMode.Fixed;
                //tab_Module.ItemSize =new Size(1,1);
                //frm.Show();
                //tab_Module.SelectedTab = tp;
                //tp.Controls.Add(frm);
            }
            else
            {
                //bool IsOpen = true;
                //AQS_PEP_APE_101.F_QCM_TrackIn_Main frm = new AQS_PEP_APE_101.F_QCM_TrackIn_Main(name, org, menus);
                //foreach (TabPage tp2 in tab_Module.TabPages)
                //{
                //    if (tp2.Text == name)
                //    {
                //        tp2.Controls.Clear();
                //        tp2.Controls.Add(frm);
                //        frm.Dock = DockStyle.Fill;
                //        tab_Module.SelectedTab = tp2;
                //        tab_Module.SizeMode = TabSizeMode.Fixed;
                //        tab_Module.ItemSize = new Size(1, 1);
                //        IsOpen = false;
                //    }
                //}
                //if (IsOpen)
                //{
                //    TabPage tp = new TabPage();
                //    //tp.Name = name;
                //    //tp.Text = name;
                //    tab_Module.TabPages.Add(tp);
                //    tab_Module.SelectedTab = tp;
                //    tab_Module.SizeMode = TabSizeMode.Fixed;
                //    tab_Module.ItemSize = new Size(1, 1);
                //    tp.Controls.Add(frm);
                //}
            }
        }

        /// <summary>
        /// 打开运行 配置界面
        /// </summary>
        /// <param name="menu"></param>
        private void RunApp(SJeMES_Framework.Web.JSONMenu menu)
        {
            try
            {
                string menu_dll = menu.menu_dll;
                string menu_class = menu.menu_class;
                string menu_method = menu.menu_method;
                Dictionary<string, object> OBJ = new Dictionary<string, object>();
                OBJ.Add("WebServiceUrl", Program.Client.WebServiceUrl);
                OBJ.Add("Org", Program.Client.Org.Org);
                OBJ.Add("OrgName", Program.Client.Org.OrgName);
                OBJ.Add("DBPassword", Program.Client.Org.DBPassword);
                OBJ.Add("DBType", Program.Client.Org.DBType);
                OBJ.Add("DBServer", Program.Client.Org.DBServer);
                OBJ.Add("DBName", Program.Client.Org.DBName);
                OBJ.Add("DBUser", Program.Client.Org.DBUser);
                OBJ.Add("DBPwd", Program.Client.Org.DBPassword);
                OBJ.Add("User", Program.Client.UserCode);
                OBJ.Add("IsMaxWindow", false);

                OBJ.Add("WorkCenter", string.Empty);
                OBJ.Add("Sites", string.Empty);
                OBJ.Add("Language", Program.Client.Language);

                if (menu_dll == "SJEMS_KANBAN")
                {
                    OBJ.Add("KanBanName", menu.menu_name);
                }
                else if(menu_dll =="SJEMS_RPT")
                {
                    OBJ.Add("ReportName", menu.menu_name);
                }
                else
                {
                    OBJ.Add("FormName", menu.menu_name);
                }


                SJeMES_Framework.Common.OtherPrograms.RunApp(menu_dll, menu_class, menu_method, OBJ);
            }
            catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        /// <summary>
        /// 打开运行 客制界面
        /// </summary>
        /// <param name="DllName"></param>
        /// <param name="ClassName"></param>
        /// <param name="Method"></param>
        /// <param name="menu_name"></param>
        public static void RunClientApp(string DllName, string ClassName, string Method, string menu_name)
        {
            try
            {
                Assembly assembly = null;
                Program.Client.FormName = menu_name;


                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

                if (!File.Exists(path + @"\" + DllName + ".dll"))
                {
                    MessageBox.Show("找不到" + DllName + ".dll文件");
                    return;
                }
                assembly = Assembly.LoadFrom(path + @"\" + DllName + ".dll");




                Type type = assembly.GetType(ClassName);

                object instance = null;


                instance = Activator.CreateInstance(type);

                MethodInfo mi = type.GetMethod(Method);


                object[] args = new object[1];

                args[0] = Program.Client;

                object obj = mi.Invoke(instance, args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void ucNavigationMenu2_ClickItemed(object sender, EventArgs e)
        {
            foreach (SJeMES_Control_Library.Controls.NavigationMenuItem i in ucNavigationMenu2.Items)
            {
                i.ShowTip = false;
            }

            if (ucNavigationMenu2.SelectItem.ParentItem == null)
            {
                ucNavigationMenu2.SelectItem.ShowTip = true;

                ucNavigationMenu2.Refresh();

                UpdateMenu(ucNavigationMenu2.SelectItem.Name);
            }
            if (ucNavigationMenu2.SelectItem.Name == "修改密码")
            {
                frmUserSetting frmUser = new frmUserSetting(Program.Client.UserCode);
                frmUser.ShowDialog();
            }
            if (ucNavigationMenu2.SelectItem.Name == "个人信息修改")
            {
                Frm_AddEmployee frm = new Frm_AddEmployee(Program.Client.UserCode, Program.Client);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            if (ucNavigationMenu2.SelectItem.Text == "Exit system")
            {
                Program.FrmLogin.Show();
                Program.IsExit = false;
                Program.FrmMain.Close();
            }
        }

        private void ucNavigationMenu3_ClickItemed(object sender, EventArgs e)
        {
            foreach (SJeMES_Control_Library.Controls.NavigationMenuItem i in ucNavigationMenu3.Items)
            {
                i.ShowTip = false;
            }

            if (ucNavigationMenu3.SelectItem.ParentItem == null)
            {
                ucNavigationMenu3.SelectItem.ShowTip = true;

                ucNavigationMenu3.Refresh();

                UpdateMenu(ucNavigationMenu3.SelectItem.Name);
            }

            if (ucNavigationMenu3.SelectItem.Name == "修改密码")
            {
                frmUserSetting frmUser = new frmUserSetting(Program.Client.UserCode);
                frmUser.ShowDialog();
            }

            if (ucNavigationMenu3.SelectItem.Name == "个人信息修改")
            {
                Frm_AddEmployee frm = new Frm_AddEmployee(Program.Client.UserCode, Program.Client);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }

            if (ucNavigationMenu3.SelectItem.Text == "Thoát hệ thống")
            {
                Program.FrmLogin.Show();
                Program.IsExit = false;
                Program.FrmMain.Close();
            }
        }

        /// <summary>
        /// 加载主页面顶部的菜单
        /// </summary>
        private void GetMenuData()
        {
            try
            {
                ucNavigationMenu1.Refresh();
                 
                //初始化菜单的显示值
                string navigationMenuItem1_Text = "控制台";
                string navigationMenuItem2_Text = "基础资料";
                string navigationMenuItem3_Text = "智能仓储";
                string navigationMenuItem4_Text = "生产管理";
                string navigationMenuItem5_Text = "质量管理";
                string navigationMenuItem6_Text = "设备管理";
                string navigationMenuItem7_Text = "权限管理";
                string navigationMenuItem8_Text = "用户";
                string navigationMenuItem9_Text = "退出系统";
                string navigationMenuItem9_1_Text = "修改密码";
                string navigationMenuItem9_2_Text = "个人信息修改";
                 
                string sql = string.Empty;
                sql = @"SELECT menu_name,ui_cn,ui_en,ui_yn
                          FROM SYSMENU01M 
                        where menu_name not in ('拣货','抛单') and menu_enable='True'";
                DataTable dt_SYSMENU01M = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebService.Url, sql, new Dictionary<string, string>());
                if (dt_SYSMENU01M != null && dt_SYSMENU01M.Rows.Count > 0)
                { 
                    foreach (DataRow dr in dt_SYSMENU01M.Rows)
                    {
                        if (Program.Client.Language.Equals("en"))
                        { 
                            if (dr["menu_name"].ToString().Equals("基础资料"))
                                navigationMenuItem2_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["menu_name"].ToString() : dr["ui_en"].ToString();//基础资料
                            else if (dr["menu_name"].ToString().Equals("智能仓储"))
                                navigationMenuItem3_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["menu_name"].ToString() : dr["ui_en"].ToString();//智能仓储
                            else if (dr["menu_name"].ToString().Equals("生产管理"))
                                navigationMenuItem4_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["menu_name"].ToString() : dr["ui_en"].ToString();//生产管理
                            else if (dr["menu_name"].ToString().Equals("质量管理"))
                                navigationMenuItem5_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["menu_name"].ToString() : dr["ui_en"].ToString();//质量管理
                            else if (dr["menu_name"].ToString().Equals("设备管理"))
                                navigationMenuItem6_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["menu_name"].ToString() : dr["ui_en"].ToString();//设备管理
                            else if (dr["menu_name"].ToString().Equals("权限管理"))
                                navigationMenuItem7_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["menu_name"].ToString() : dr["ui_en"].ToString();//权限管理 
                        }
                        else if(Program.Client.Language.Equals("hk"))
                        {
                            navigationMenuItem1_Text = "Đóng";//控制台

                            if(dr["menu_name"].ToString().Equals("基础资料"))
                                navigationMenuItem2_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_yn"].ToString();//基础资料
                            else if(dr["menu_name"].ToString().Equals("智能仓储"))
                                navigationMenuItem3_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_yn"].ToString();//智能仓储
                            else if(dr["menu_name"].ToString().Equals("生产管理"))
                                navigationMenuItem4_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_yn"].ToString();//生产管理
                            else if(dr["menu_name"].ToString().Equals("质量管理"))
                                navigationMenuItem5_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_yn"].ToString();//质量管理
                            else if(dr["menu_name"].ToString().Equals("设备管理"))
                                navigationMenuItem6_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_yn"].ToString();//设备管理
                            else if(dr["menu_name"].ToString().Equals("权限管理"))
                                navigationMenuItem7_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_yn"].ToString();//权限管理

                        }
                        else
                        {
                            if (dr["menu_name"].ToString().Equals("基础资料"))
                                navigationMenuItem2_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_cn"].ToString();//基础资料
                            else if (dr["menu_name"].ToString().Equals("智能仓储"))
                                navigationMenuItem3_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_cn"].ToString();//智能仓储
                            else if (dr["menu_name"].ToString().Equals("生产管理"))
                                navigationMenuItem4_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_cn"].ToString();//生产管理
                            else if (dr["menu_name"].ToString().Equals("质量管理"))
                                navigationMenuItem5_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_cn"].ToString();//质量管理
                            else if (dr["menu_name"].ToString().Equals("设备管理"))
                                navigationMenuItem6_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_cn"].ToString();//设备管理
                            else if (dr["menu_name"].ToString().Equals("权限管理"))
                                navigationMenuItem7_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["menu_name"].ToString() : dr["ui_cn"].ToString();//权限管理
                        }
                    }
                }


                sql = @"SELECT ui_code,ui_id,ui_cn,ui_en,ui_yn from SJQDMS_UILAN where ui_code = 'frmMain'";
                DataTable dt_SJQDMS_UILAN = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebService.Url, sql, new Dictionary<string, string>());
                if (dt_SJQDMS_UILAN != null && dt_SJQDMS_UILAN.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_SJQDMS_UILAN.Rows)
                    {
                        if (Program.Client.Language.Equals("en"))
                        {
                            if(dr["ui_id"].ToString().Equals("控制台"))
                                navigationMenuItem1_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["ui_id"].ToString() : dr["ui_en"].ToString();//控制台
                            else if(dr["ui_id"].ToString().Equals("用户"))
                                navigationMenuItem8_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["ui_id"].ToString() : dr["ui_en"].ToString();//用户
                            else if(dr["ui_id"].ToString().Equals("退出系统"))
                                navigationMenuItem9_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["ui_id"].ToString() : dr["ui_en"].ToString();//退出系统
                            else if(dr["ui_id"].ToString().Equals("修改密码"))
                                navigationMenuItem9_1_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["ui_id"].ToString() : dr["ui_en"].ToString();//修改密码
                            else if (dr["ui_id"].ToString().Equals("个人信息修改"))
                                navigationMenuItem9_2_Text = string.IsNullOrEmpty(dr["ui_en"].ToString()) ? dr["ui_id"].ToString() : dr["ui_en"].ToString();//个人信息修改
                        }
                        else if(Program.Client.Language.Equals("hk"))
                        {
                            if (dr["ui_id"].ToString().Equals("控制台"))
                                navigationMenuItem1_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["ui_id"].ToString() : dr["ui_yn"].ToString();//控制台
                            else if (dr["ui_id"].ToString().Equals("用户"))
                                navigationMenuItem8_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["ui_id"].ToString() : dr["ui_yn"].ToString();//用户
                            else if (dr["ui_id"].ToString().Equals("退出系统"))
                                navigationMenuItem9_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["ui_id"].ToString() : dr["ui_yn"].ToString();//退出系统
                            else if (dr["ui_id"].ToString().Equals("修改密码"))
                                navigationMenuItem9_1_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["ui_id"].ToString() : dr["ui_yn"].ToString();//修改密码
                            else if (dr["ui_id"].ToString().Equals("个人信息修改"))
                                navigationMenuItem9_2_Text = string.IsNullOrEmpty(dr["ui_yn"].ToString()) ? dr["ui_id"].ToString() : dr["ui_yn"].ToString();//个人信息修改
                        }
                        else
                        {
                            if (dr["ui_id"].ToString().Equals("控制台"))
                                navigationMenuItem1_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["ui_id"].ToString() : dr["ui_cn"].ToString();//控制台
                            else if (dr["ui_id"].ToString().Equals("用户"))
                                navigationMenuItem8_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["ui_id"].ToString() : dr["ui_cn"].ToString();//用户
                            else if (dr["ui_id"].ToString().Equals("退出系统"))
                                navigationMenuItem9_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["ui_id"].ToString() : dr["ui_cn"].ToString();//退出系统
                            else if (dr["ui_id"].ToString().Equals("修改密码"))
                                navigationMenuItem9_1_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["ui_id"].ToString() : dr["ui_cn"].ToString();//修改密码
                            else if (dr["ui_id"].ToString().Equals("个人信息修改"))
                                navigationMenuItem9_2_Text = string.IsNullOrEmpty(dr["ui_cn"].ToString()) ? dr["ui_id"].ToString() : dr["ui_cn"].ToString();//个人信息修改
                        }
                    }
                }

                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem1 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem2 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem3 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem4 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem5 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem6 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem7 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem8 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem9 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem9_1 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                SJeMES_Control_Library.Controls.NavigationMenuItem navigationMenuItem9_2 = new SJeMES_Control_Library.Controls.NavigationMenuItem();
                this.ucNavigationMenu1 = new SJeMES_Control_Library.Controls.UCNavigationMenu(); 

                this.ucNavigationMenu1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(61)))), ((int)(((byte)(73)))));
                this.ucNavigationMenu1.Font = new System.Drawing.Font("微软雅黑", 11F);
                this.ucNavigationMenu1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                navigationMenuItem1.AnchorRight = false;
                navigationMenuItem1.DataSource = null;
                navigationMenuItem1.HasSplitLintAtTop = false;
                navigationMenuItem1.Icon = global::SJeMESClient.Properties.Resources.menu_Console;
                navigationMenuItem1.Items = new SJeMES_Control_Library.Controls.NavigationMenuItem[0];
                navigationMenuItem1.ItemWidth = 130;
                navigationMenuItem1.Name = "控制台";
                navigationMenuItem1.ShowTip = false;
                navigationMenuItem1.Text = navigationMenuItem1_Text;
                navigationMenuItem1.TipText = null;
                navigationMenuItem2.AnchorRight = false;
                navigationMenuItem2.DataSource = null;
                navigationMenuItem2.HasSplitLintAtTop = false;
                navigationMenuItem2.Icon = global::SJeMESClient.Properties.Resources.menu_Base;
                navigationMenuItem2.Items = null;
                navigationMenuItem2.ItemWidth = 150;
                navigationMenuItem2.Name = "基础资料";
                navigationMenuItem2.ShowTip = false;
                navigationMenuItem2.Text = navigationMenuItem2_Text;
                navigationMenuItem2.TipText = null;
                navigationMenuItem3.AnchorRight = false;
                navigationMenuItem3.DataSource = null;
                navigationMenuItem3.HasSplitLintAtTop = false;
                navigationMenuItem3.Icon = global::SJeMESClient.Properties.Resources.menu_WMS;
                navigationMenuItem3.Items = null;
                navigationMenuItem3.ItemWidth = 150;
                navigationMenuItem3.Name = "智能仓储";
                navigationMenuItem3.ShowTip = false;
                navigationMenuItem3.Text = navigationMenuItem3_Text;
                navigationMenuItem3.TipText = null;
                navigationMenuItem4.AnchorRight = false;
                navigationMenuItem4.DataSource = null;
                navigationMenuItem4.HasSplitLintAtTop = false;
                navigationMenuItem4.Icon = global::SJeMESClient.Properties.Resources.menu_MES;
                navigationMenuItem4.Items = null;
                navigationMenuItem4.ItemWidth = 150;
                navigationMenuItem4.Name = "生产管理";
                navigationMenuItem4.ShowTip = false;
                navigationMenuItem4.Text = navigationMenuItem4_Text;
                navigationMenuItem4.TipText = null;
                navigationMenuItem5.AnchorRight = false;
                navigationMenuItem5.DataSource = null;
                navigationMenuItem5.HasSplitLintAtTop = false;
                navigationMenuItem5.Icon = global::SJeMESClient.Properties.Resources.menu_QMS;
                navigationMenuItem5.Items = null;
                navigationMenuItem5.ItemWidth = 150;
                navigationMenuItem5.Name = "质量管理";
                navigationMenuItem5.ShowTip = false;
                navigationMenuItem5.Text = navigationMenuItem5_Text;
                navigationMenuItem5.TipText = null;
                navigationMenuItem6.AnchorRight = false;
                navigationMenuItem6.DataSource = null;
                navigationMenuItem6.HasSplitLintAtTop = false;
                navigationMenuItem6.Icon = ((System.Drawing.Image)(resources.GetObject("navigationMenuItem6.Icon")));
                navigationMenuItem6.Items = null;
                navigationMenuItem6.ItemWidth = 150;
                navigationMenuItem6.Name = "设备管理";
                navigationMenuItem6.ShowTip = false;
                navigationMenuItem6.Text = navigationMenuItem6_Text;
                navigationMenuItem6.TipText = null;
                navigationMenuItem7.AnchorRight = false;
                navigationMenuItem7.DataSource = null;
                navigationMenuItem7.HasSplitLintAtTop = false;
                navigationMenuItem7.Icon = ((System.Drawing.Image)(resources.GetObject("navigationMenuItem7.Icon")));
                navigationMenuItem7.Items = null;
                navigationMenuItem7.ItemWidth = 150;
                navigationMenuItem7.Name = "权限管理";
                navigationMenuItem7.ShowTip = false;
                navigationMenuItem7.Text = navigationMenuItem7_Text;
                navigationMenuItem7.TipText = null;
                navigationMenuItem8.AnchorRight = true;
                navigationMenuItem8.DataSource = null;
                navigationMenuItem8.HasSplitLintAtTop = false;
                navigationMenuItem8.Icon = global::SJeMESClient.Properties.Resources.menu_User;
                navigationMenuItem9.AnchorRight = false;
                navigationMenuItem9.DataSource = null;
                navigationMenuItem9.HasSplitLintAtTop = false;
                navigationMenuItem9.Icon = null;
                navigationMenuItem9.Items = null;
                navigationMenuItem9.ItemWidth = 100;
                navigationMenuItem9.Name = "退出系统";
                navigationMenuItem9.ShowTip = false;
                navigationMenuItem9.Text = navigationMenuItem9_Text;
                navigationMenuItem9.TipText = null;

                navigationMenuItem9_1.AnchorRight = false;
                navigationMenuItem9_1.DataSource = null;
                navigationMenuItem9_1.HasSplitLintAtTop = false;
                navigationMenuItem9_1.Icon = null;
                navigationMenuItem9_1.Items = null;
                navigationMenuItem9_1.ItemWidth = 100;
                navigationMenuItem9_1.Name = "修改密码";
                navigationMenuItem9_1.ShowTip = false;
                navigationMenuItem9_1.Text = navigationMenuItem9_1_Text;
                navigationMenuItem9_1.TipText = null;

                navigationMenuItem9_2.AnchorRight = false;
                navigationMenuItem9_2.DataSource = null;
                navigationMenuItem9_2.HasSplitLintAtTop = false;
                navigationMenuItem9_2.Icon = null;
                navigationMenuItem9_2.Items = null;
                navigationMenuItem9_2.ItemWidth = 100;
                navigationMenuItem9_2.Name = "个人信息修改";
                navigationMenuItem9_2.ShowTip = false;
                navigationMenuItem9_2.Text = navigationMenuItem9_2_Text;
                navigationMenuItem9_2.TipText = null;

                navigationMenuItem8.Items = new SJeMES_Control_Library.Controls.NavigationMenuItem[] {
                navigationMenuItem9_2,
                navigationMenuItem9_1,
                navigationMenuItem9};
                navigationMenuItem8.ItemWidth = 180;
                navigationMenuItem8.Name = "M_User";
                navigationMenuItem8.ShowTip = false;
                navigationMenuItem8.Text = navigationMenuItem8_Text;
                navigationMenuItem8.TipText = null;
                this.ucNavigationMenu1.Items = new SJeMES_Control_Library.Controls.NavigationMenuItem[] {
                                                                                    navigationMenuItem1,
                                                                                    navigationMenuItem2,
                                                                                    navigationMenuItem3,
                                                                                    navigationMenuItem4,
                                                                                    navigationMenuItem5,
                                                                                    navigationMenuItem6,
                                                                                    navigationMenuItem7,
                                                                                    navigationMenuItem8};
                this.ucNavigationMenu1.Location = new System.Drawing.Point(0, 0);
                this.ucNavigationMenu1.Name = "ucNavigationMenu1";
                this.ucNavigationMenu1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
                this.ucNavigationMenu1.Size = new System.Drawing.Size(1357, 60);
                this.ucNavigationMenu1.TabIndex = 0;
                this.ucNavigationMenu1.TipColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
                this.ucNavigationMenu1.ClickItemed += new System.EventHandler(this.ucNavigationMenuExt1_ClickItemed);
                 
                ucNavigationMenu1.Visible = true;
                ucNavigationMenu2.Visible = false;
                ucNavigationMenu3.Visible = false;
                ucNavigationMenu2.Dock = DockStyle.None;
                ucNavigationMenu3.Dock = DockStyle.None;
                ucNavigationMenu1.Dock = DockStyle.Top;
                ucNavigationMenu1.Location = new Point(0, 0);

                this.panel_BackGroup.Controls.Add(this.ucNavigationMenu1);

                #region MyRegion
                //if (Program.Client.Language == "en")
                //{
                //    ucNavigationMenu1.Visible = false;
                //    ucNavigationMenu1.Dock = DockStyle.None;
                //    ucNavigationMenu2.Visible = true;
                //    ucNavigationMenu2.Dock = DockStyle.Top;
                //    ucNavigationMenu2.Location = new Point(0, 0);
                //    ucNavigationMenu3.Visible = false;
                //    ucNavigationMenu3.Dock = DockStyle.None;
                //    ucNavigationMenu2.Refresh();
                //}
                //else if (Program.Client.Language == "hk")
                //{
                //    ucNavigationMenu1.Visible = false;
                //    ucNavigationMenu1.Dock = DockStyle.None;
                //    ucNavigationMenu2.Visible = false;
                //    ucNavigationMenu2.Dock = DockStyle.None;
                //    ucNavigationMenu3.Visible = true;
                //    ucNavigationMenu3.Dock = DockStyle.Top;
                //    ucNavigationMenu3.Location = new Point(0, 0);
                //    ucNavigationMenu3.Refresh();
                //}
                //else
                //{
                //    ucNavigationMenu1.Visible = true;
                //    ucNavigationMenu2.Visible = false;
                //    ucNavigationMenu3.Visible = false;
                //    ucNavigationMenu2.Dock = DockStyle.None;
                //    ucNavigationMenu3.Dock = DockStyle.None;
                //    ucNavigationMenu1.Dock = DockStyle.Top;
                //    ucNavigationMenu1.Location = new Point(0, 0);
                //    ucNavigationMenu1.Refresh();
                //}
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void treeViewEx1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                treeViewEx1.SelectedNode = null;
                tab_Module.SelectedTab = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeViewEx1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                treeViewEx1.SelectedNode = null;
                tab_Module.SelectedTab = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
