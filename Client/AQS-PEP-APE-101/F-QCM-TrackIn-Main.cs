using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using FastReport.Design.ToolWindows;
using System.Drawing.Printing;
using System.IO;
using SJeMES_Framework.Web;

namespace AQS_PEP_APE_101
{
    public partial class F_QCM_TrackIn_Main : UserControl
    {
        public string mainname;
        public int X;
        public int Y;
        Dictionary<string, JSONMenu> menu = new Dictionary<string, JSONMenu>();
        public F_QCM_TrackIn_Main(string name, SJeMES_Framework.Class.ClientClass org, Dictionary<string, JSONMenu> menus)
        {
            //MessageBox.Show(name);
            menu = menus;
            mainname = name;
            Program.Client = org;
            InitializeComponent();
             GetMenu();
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        //获取菜单栏
        public void GetMenu()
        {
            //Dictionary<string, object> data = new Dictionary<string, object>();
            //data.Add("name", mainname);
            //string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
            //    "SJ_SYSAPI", "SJ_SYSAPI.Menu", "GetSYSMenu2", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            //var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
            //if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            //{
            //     menu= Newtonsoft.Json.JsonConvert.DeserializeObject<List<SJeMES_Framework.Web.JSONMenu>>(j["RetData"].ToString());
            //    if (menu.Count>0)
            //    {
                    CreateMenu();
                    int i = 0;
                    foreach (string key in menu.Keys)
                    {
                        if (i == 0)
                        {
                    SJeMES_Framework.Web.JSONMenu m = menu[key];
                    string name = string.Empty;
                            if (Program.Client.Language == "cn")
                            {
                                name = m.menu_name;
                            }
                            else if (Program.Client.Language == "en")
                            {
                                if (string.IsNullOrEmpty(m.ui_en))
                                {
                                    name = m.menu_name;
                                }
                                else
                                {
                                    name = m.ui_en;
                                }

                            }
                            else if (Program.Client.Language == "hk")
                            {
                                if (string.IsNullOrEmpty(m.ui_yn))
                                {
                                    name = m.menu_name;
                                }
                                else
                                {
                                    name = m.ui_yn;
                                }
                            }
                            getfrom(name);
                        }
                        i++;
                    }

            //    }
            //}
            //else
            //{
            //    throw new Exception(j["ErrMsg"].ToString());
            //}
        }

        public void CreateMenu()
        {
                //加载MenuStrip菜单
                ToolStripMenuItem topMenu = new ToolStripMenuItem();
                LoadSubMenu(ref topMenu, "0");
                
                //getfrom();


        }

        /// <summary>
        /// 递归创建MenuStrip菜单(模块列表)
        /// </summary>
        /// <param name="topMenu">父菜单项</param>
        /// <param name="FATHER_ID">父菜单的ID</param>
        private void LoadSubMenu(ref ToolStripMenuItem topMenu, String inFatherId)
        {
            ToolStripMenuItem subMenu;
            foreach (string key in menu.Keys)
            {
                SJeMES_Framework.Web.JSONMenu m = menu[key];
                string name = string.Empty;
                if (Program.Client.Language=="cn")
                {
                    name= m.menu_name;
                }
                else if (Program.Client.Language == "en")
                {
                    if (string.IsNullOrEmpty(m.ui_en))
                    {
                        name = m.menu_name;
                    }
                    else
                    {
                        name = m.ui_en;
                    }
                   
                }
                else if (Program.Client.Language == "hk")
                {
                    if (string.IsNullOrEmpty(m.ui_yn))
                    {
                        name = m.menu_name;
                    }
                    else
                    {
                        name = m.ui_yn;
                    }
                }
                //创建子菜单项
                subMenu = new ToolStripMenuItem();
                subMenu.Name = name;
                subMenu.Text = name;

                //判断是否为顶级菜单
                if (inFatherId == "0")
                {
                    subMenu.Tag = name;
                    subMenu.ForeColor = Color.White;
                    //subMenu.DropDown.AutoSize = false;
                    subMenu.DropDown.Size = new Size(50, 150);
                    subMenu.Margin=new System.Windows.Forms.Padding(0,0,30,0);
                    subMenu.Click += new EventHandler(subMenu_Click);
                    menuStrip1.Items.Add(subMenu);
                }
                else
                {
                    //subMenu.Tag = dv["MODULE_ACTION"].ToString();
                    //给菜单项加事件。
                    subMenu.Click += new EventHandler(subMenu_Click);

                    topMenu.DropDownItems.Add(subMenu);
                }

                //递归调用
               // LoadSubMenu(ref subMenu, dv["ID"].ToString());

            }

        }


        /// <summary>
        /// 菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void subMenu_Click(object sender, EventArgs e)
        {

            try
            {
                string acName = ((ToolStripMenuItem)sender).Tag.ToString();
                getfrom(acName);
                //foreach (SJeMES_Framework.Web.JSONMenu m in menu)
                //{
                //    if (m.menu_name== acName)
                //    {
                //        getfrom(m.menu_url);
                //    }
                //}
                //tag属性在这里有用到。

                //string acName = ((ToolStripMenuItem)sender).Tag.ToString();

                //if (acName != "")
                //{
                //    string[] strArray = acName.Split(new char[] { ',' });
                //    if (strArray.Length > 2)
                //    {
                //    }
                //    else
                //    {
                //        String str = "void " + acName;
                //        foreach (MethodInfo info in base.GetType().GetMethods())
                //        {
                //            if (str.Trim().ToLower().CompareTo(info.ToString().Trim().ToLower()) == 0)
                //            {
                //                info.Invoke(this, null);
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception exception)
            {

            }
        }

        //嵌套页面
        public void getfrom(string name)
        {
           

            foreach (string key in menu.Keys)
            {
                SJeMES_Framework.Web.JSONMenu m = menu[key];
                string na = string.Empty;
                if (Program.Client.Language == "cn")
                {
                    na = m.menu_name;
                }
                else if (Program.Client.Language == "en")
                {
                    if (string.IsNullOrEmpty(m.ui_en))
                    {
                        na = m.menu_name;
                    }
                    else
                    {
                        na = m.ui_en;
                    }

                }
                else if (Program.Client.Language == "hk")
                {
                    if (string.IsNullOrEmpty(m.ui_yn))
                    {
                        na = m.menu_name;
                    }
                    else
                    {
                        na = m.ui_yn;
                    }
                }
                if (na == name)
                {
                    Assembly assembly = null;


                    string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

                    if (!File.Exists(path + @"\" + m.menu_dll + ".dll"))
                    {
                        MessageBox.Show("找不到" + m.menu_dll + ".dll文件");
                        return;
                    }
                    assembly = Assembly.LoadFile(path + @"\" + m.menu_dll + ".dll");
                    //必须使用 名称空间+用户控件类名称
                    object[] args = new object[1];
                    args[0] = Program.Client;
                    //Binder binder = new assembly.Stub();
                    object oClient = (System.Windows.Forms.Control)assembly.CreateInstance(m.menu_dll + "." + m.menu_url, true,System.Reflection.BindingFlags.Default, null, args, null, null);
                    Control c = (Control)oClient;
                    c.Dock= DockStyle.Fill;
                    //TabPage tp = new TabPage();
                    //tab_Module.TabPages.Add(tp);
                    //tab_Module.SelectedTab = tp;
                    this.panel1.Controls.Add(c);
                    //this.tab_Module.Controls.Add((Control)oClient);

                    //assembly = Assembly.LoadFile(path + @"\" + m.menu_dll + ".dll");
                    //Type type = assembly.GetType("OperatingPlatform.Interface");
                    //object instance = null;
                    //instance = Activator.CreateInstance(type);

                    //MethodInfo mi = type.GetMethod("RunApp");


                    //object[] args = new object[1];

                    //args[0] = Program.Client;

                    //object obj = mi.Invoke(instance, args);
                    //OperatingPlatform.UCOperating frm2 = new OperatingPlatform.UCOperating(Program.Client); //实例化一个子窗口
                    //填充

                    //frm2.Dock = DockStyle.Fill;

                    ////清空Panel里面的控件

                    //this.panel1.Controls.Clear();

                    ////加入控件

                    //this.panel1.Controls.Add(frm2);

                    ////让窗体显示

                    //frm2.Show();
                }
            }
            
           

        }

        private void F_QCM_TrackIn_Main_Load(object sender, EventArgs e)
        {
            //this.Resize += new EventHandler(F_QCM_TrackIn_Main_Resize);//窗体调整大小时引发事件

            //X = this.Width;//获取窗体的宽度

            //Y = this.Height;//获取窗体的高度

            //setTag(this);//调用方法
        }
        private void setTag(Control cons)
        {
            //遍历窗体中的控件
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                float a = Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = Convert.ToSingle(mytag[1]) * newy;//高度
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;//左边距离
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }

        private void F_QCM_TrackIn_Main_Resize(object sender, EventArgs e)
        {
            //float newx = (this.Width) / X; //窗体宽度缩放比例
            //float newy = this.Height / Y;//窗体高度缩放比例
            //setControls(newx, newy, this);//随窗体改变控件大小
            //this.Text = this.Width.ToString() + " " + this.Height.ToString();//窗体标题栏文本
        }
    }
}
