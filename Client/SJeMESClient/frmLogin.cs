using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMESClient
{
    public partial class frmLogin : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public frmLogin()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
                Program.SkinThemes, materialSkinManager, this);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                ucTextBoxEx1.InputText = Program.Client.UserCode;


                #region 语言
                List<string> LanguagesType = LoginHelper.GetLanguagesType();

                List<KeyValuePair<string, string>> lstCom = new List<KeyValuePair<string, string>>();
                foreach (string s in LanguagesType)
                {
                    string txt = s;
                    if (s.Equals("hk"))
                        txt = "yn";
                    lstCom.Add(new KeyValuePair<string, string>(txt, txt));

                }

                ucCombox1.Source = lstCom;

                foreach (KeyValuePair<string, string> kv in lstCom)
                {
                    if ((kv.Key.Equals("yn") ? "hk" : kv.Key) == Program.Client.Language) // kv.Key == Program.Client.Language ||
                    {
                        ucCombox1.TextValue = kv.Key;
                    }
                }

                if (string.IsNullOrEmpty(ucCombox1.TextValue) && lstCom.Count > 0)
                {
                    ucCombox1.SelectedIndex = 0;
                } 
                #endregion

                #region 公司
                Dictionary<string, string> Orgs = new Dictionary<string, string>();
                Orgs = LoginHelper.GetOrg();
                List<KeyValuePair<string, string>> lstCom2 = new List<KeyValuePair<string, string>>();
                foreach (string key in Orgs.Keys)
                {
                    lstCom2.Add(new KeyValuePair<string, string>(key, Orgs[key]));


                }

                ucCombox2.Source = lstCom2;

                foreach (KeyValuePair<string, string> kv in lstCom2)
                {
                    if (kv.Key == Program.Client.CompanyCode)
                    {
                        ucCombox2.SelectedValue = kv.Key;
                    }
                }

                if (string.IsNullOrEmpty(ucCombox2.TextValue) && lstCom2.Count > 0)
                {
                    ucCombox2.SelectedIndex = 0;
                }
                #endregion


                #region  
                //更新 SJeMES Launcher.exe 程序
                GetLauncherFile();
                #endregion

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void ucBtnImg2_BtnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ucBtnImg3_BtnClick(object sender, EventArgs e)
        {
            SJeMES_Control_Library.Forms.FrmInputs frm = new
                 SJeMES_Control_Library.Forms.FrmInputs("设置(Set Up)",
                 new string[] { "API地址","WS服务地址" },
                 new Dictionary<string, SJeMES_Control_Library.TextInputType>(),
                 new Dictionary<string, string>(),
                 new Dictionary<string, SJeMES_Control_Library.Controls.KeyBoardType>(),
                 new List<string>() { "API地址", "WS服务地址" },
                 new Dictionary<string, string>() { {"API地址",Program.Client.APIURL},{ "WS服务地址", Program.Client.WebServiceUrl } });
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(Program.configstring);

                Pconfig["api"] = frm.Values[0];
                Pconfig["webservice"] = frm.Values[1];
                Program.Client.WebServiceUrl = frm.Values[1];
                Program.WebService.Url = Program.Client.WebServiceUrl;
                Program.Client.APIURL = frm.Values[0];
               
                Program.configstring = Newtonsoft.Json.JsonConvert.SerializeObject(Pconfig);
                

                //System.IO.File.Delete("Config.json");
                //SJeMES_Framework.Common.TXTHelper.WriteToEnd("Config.json", Program.configstring);
                if (!string.IsNullOrEmpty(Program.configstring))
                {
                    bool rest = SJeMES_Framework.Common.TXTHelper.WriteLine("Config.json", Program.configstring);
                    if (!rest)
                    {
                        System.IO.File.Delete("Config.json");
                        SJeMES_Framework.Common.TXTHelper.WriteLine("Config.json", Program.configstring);
                    }
                }
            }
        }

        /// <summary>
        /// 登录点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBtnImg1_BtnClick(object sender, EventArgs e)
        {
            try
            {
                #region 更新上一层的驱动更新程序
                string currPath = System.Windows.Forms.Application.StartupPath.ToString();
                //1.先检查上一层是否有文件
                DirectoryInfo currPathInfo = new DirectoryInfo(currPath);
                string currParentPath = Path.Combine(currPathInfo.Parent.FullName, "SJeMES Launcher.exe.config");
                if (File.Exists(currParentPath))
                {
                    string currParentPathInfo = SJeMES_Framework.Common.TXTHelper.ReadToEnd(currParentPath);
                    //if (!currParentPathInfo.Contains("Encrypt"))
                    //{
                        string oldConfig1 = Path.Combine(currPath, "uploadLauncher", "SJeMES Launcher.exe.config");
                        string newConfig1 = Path.Combine(currPathInfo.Parent.FullName, "SJeMES Launcher.exe.config");
                        File.Copy(oldConfig1, newConfig1, true);
                        string oldConfig2 = Path.Combine(currPath, "uploadLauncher", "SJeMES Launcher.exe");
                        string newConfig2 = Path.Combine(currPathInfo.Parent.FullName, "SJeMES Launcher.exe");
                        File.Copy(oldConfig2, newConfig2, true);
                    //}
                }
                #endregion

                if (string.IsNullOrEmpty(ucTextBoxEx1.InputText))
                {
                    string msg4 = SJeMES_Framework.Common.UIHelper.UImsg("Please input Username", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    throw new Exception(msg4);
                }

                if (string.IsNullOrEmpty(ucTextBoxEx2.InputText))
                {
                    string msg4 = SJeMES_Framework.Common.UIHelper.UImsg("please enter password", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    throw new Exception(msg4);
                }

                if (LoginHelper.Login(
                    ucCombox2.SelectedValue, ucCombox2.SelectedText,
                    ucTextBoxEx1.InputText.Trim(),
                    ucTextBoxEx2.InputText.Trim()))
                { 
                    if(MenuHelper.GetMenu())
                    {
                        #region 保存Congfig
                        Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(Program.configstring);

                        Program.Client.Language = ucCombox1.TextValue.Equals("yn") ? "hk" : ucCombox1.TextValue;

                        Pconfig["org"] = Program.Client.CompanyCode;
                        Pconfig["language"] = Program.Client.Language;
                        Pconfig["usercode"] = Program.Client.UserCode;

                        string webURL = Pconfig["webservice"];
                        if (webURL != "http://10.2.171.110:8083/SJ-WebService.asmx")
                        {
                            //DialogResult Error = System.Windows.Forms.MessageBox.Show("WS服务地址有误",
                            //     "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            //throw new Exception();
                        }
                        Program.configstring = Newtonsoft.Json.JsonConvert.SerializeObject(Pconfig);

                        //System.IO.File.Delete("Config.json");
                        //SJeMES_Framework.Common.TXTHelper.WriteToEnd("Config.json", Program.configstring); 
                        //System.IO.File.Delete("LoginUser.json");
                        //SJeMES_Framework.Common.TXTHelper.WriteToEnd("LoginUser.json", Program.configstring);

                        if (!string.IsNullOrEmpty(Program.configstring))
                        {
                            bool rest = SJeMES_Framework.Common.TXTHelper.WriteLine("Config.json", Program.configstring);
                            if (!rest)
                            {
                                System.IO.File.Delete("Config.json");
                                SJeMES_Framework.Common.TXTHelper.WriteLine("Config.json", Program.configstring);
                            }
                        }
                        #endregion

                        //SJeMES_Control_Library.Forms.FrmInputs frm = SJeMES_Control_Library.Forms.FrmInputs()

                        Program.Client.Org = LoginHelper.OrgInfos[Program.Client.CompanyCode];
                        Program.Client.Org.User = new SJeMES_Framework.Class.UserClass(Program.Client.UserCode,Program.Client.UserName);

                        string cfig = Program.configstring;
                        Program.FrmMain = new frmMain();
                        Program.FrmMain.Show();
                       
                        Program.IsExit = true;
                        this.Hide();
                    }
                } 
            }
            catch (Exception ex)
            { 
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void ucCombox1_TextChangedEvent(object sender, EventArgs e)
        {
            if (ucCombox1.TextValue=="en")
            {
                ucPanelTitle1.Title = "Login Information";
                ucBtnImg3.BtnText = "Set up";
                ucBtnImg2.BtnText = "Sign out";
                ucBtnImg1.BtnText = "Sign in";
            }
            else if (ucCombox1.TextValue == "yn")
            {
                ucPanelTitle1.Title = "truy cập";
                ucBtnImg3.BtnText = "Chuẩn bị";
                ucBtnImg2.BtnText = "Ký tên vô.";
                ucBtnImg1.BtnText = "Ký vô.";
            }
            else
            {
                ucPanelTitle1.Title = "登录信息";
                ucBtnImg3.BtnText = "设置";
                ucBtnImg2.BtnText = "退出";
                ucBtnImg1.BtnText = "登录";
            }
            #region 保存Congfig
            Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(Program.configstring);

            Program.Client.Language = ucCombox1.TextValue.Equals("yn") ? "hk" : ucCombox1.TextValue;

            Pconfig["org"] = Program.Client.CompanyCode;
            Pconfig["language"] = Program.Client.Language;
            Pconfig["usercode"] = Program.Client.UserCode;
            Program.configstring = Newtonsoft.Json.JsonConvert.SerializeObject(Pconfig);

            //System.IO.File.Delete("Config.json");
            //SJeMES_Framework.Common.TXTHelper.WriteToEnd("Config.json", Program.configstring);
            if (!string.IsNullOrEmpty(Program.configstring))
            {
                bool rest = SJeMES_Framework.Common.TXTHelper.WriteLine("Config.json", Program.configstring);
                if (!rest)
                {
                    System.IO.File.Delete("Config.json");
                    SJeMES_Framework.Common.TXTHelper.WriteLine("Config.json", Program.configstring);
                }
            } 

            #endregion
            // SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", ucCombox1.TextValue);
        }

        /// <summary>
        /// 对比 SJeMES Launcher 文件版本信息
        /// </summary>
        public void GetLauncherFile()
        {
            try
            {
                #region 终止 SJeMES Launcher 进程 
                Process[] process;//创建一个PROCESS类数组
                process = Process.GetProcesses();//获取当前任务管理器所有运行中程序
                foreach (Process proces in process)//遍历
                {
                    if (proces.ProcessName == "SJeMES Launcher" || proces.ProcessName.Contains("SJeMES Launcher"))
                    {
                         proces.CloseMainWindow();
                        //proces.Kill();
                    }
                } 
                #endregion

                DirectoryInfo path_exe = new DirectoryInfo(Application.StartupPath); //exe目录
                String path = path_exe.Parent.FullName; //上级的目录
                string filename = path + @"\SJeMES Launcher.exe";

                //判断路径的文件是否存在,存在的才更新
                if (System.IO.File.Exists(filename))
                {  
                    string sql = "select FilesMD5,FilesBASE64 from SYSUploadFile where Files='SJeMES Launcher.exe'";
                    DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.WebService.Url, sql, new Dictionary<string, string>());
                    

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //比对版本信息 
                        string V_Launcher = ReadLauncherTXT();//旧版本信息

                        //更新 SJeMES Launcher.exe
                        if (string.IsNullOrEmpty(V_Launcher) || !V_Launcher.Equals(dt.Rows[0]["FilesMD5"].ToString()))
                        {
                            string FilesBASE64 = dt.Rows[0]["FilesBASE64"].ToString();
                            if (!string.IsNullOrEmpty(FilesBASE64))
                                Base64StringToFile(path + @"\SJeMES Launcher.exe", FilesBASE64);

                            //更新文件后更新文本
                            WriteLauncherTXT(dt.Rows[0]["FilesMD5"].ToString());
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("终止进程提示："+ex.ToString());
            } 
        }

        /// <summary>
        /// 读取 Launcher 版本号信息
        /// </summary>
        /// <returns></returns>
        public string ReadLauncherTXT()
        {
            string Version = string.Empty;

            DirectoryInfo path_exe = new DirectoryInfo(Application.StartupPath); //exe目录 
            string filename = System.AppDomain.CurrentDomain.BaseDirectory + @"\UpdateLauncher.txt";
            if (System.IO.File.Exists(filename))
            {
                Version = System.IO.File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"\UpdateLauncher.txt", Encoding.UTF8).Trim();
            } 
            return Version;
        }

        /// <summary>
        /// 写入版本新
        /// </summary>
        /// <param name="VersionTxt"></param>
        public void WriteLauncherTXT(string VersionTxt)
        {
            try
            {
                //System.IO.File.Delete("UpdateLauncher.txt");
                //SJeMES_Framework.Common.TXTHelper.WriteToEnd("UpdateLauncher.txt", VersionTxt);
                bool rest = SJeMES_Framework.Common.TXTHelper.WriteLine("UpdateLauncher.txt", VersionTxt);
                if (!rest)
                {
                    System.IO.File.Delete("UpdateLauncher.txt");
                    SJeMES_Framework.Common.TXTHelper.WriteLine("UpdateLauncher.txt", VersionTxt);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "写入【UpdateLauncher.txt】失败！");
            }
        }

        /// <summary>
        /// 转成文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strbase64"></param>
        public static void Base64StringToFile(string path, string strbase64)
        { 
            try
            {


                strbase64 = strbase64.Replace(' ', '+');
                System.IO.MemoryStream stream = new System.IO.MemoryStream(Convert.FromBase64String(strbase64));
                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                 
                byte[] b = stream.ToArray(); 
                fs.Write(b, 0, b.Length);
                fs.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
