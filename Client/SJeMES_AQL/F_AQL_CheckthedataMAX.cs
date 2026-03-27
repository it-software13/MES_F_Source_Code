using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_AQL.AQL_FrmBase;
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
    public partial class F_AQL_CheckthedataMAX : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Dictionary<string, object> dic = new Dictionary<string, object>();
        public List<string> AQL_EDIT_STATE = new List<string>();
        public int SourcePage = 0;//0:AQL;1:验货室
        /// <summary>
        /// status(0:核对资料 1:点箱 2:AQL录入 3:照片 4:湿度录入 5:BA录入)
        /// </summary>
        /// <param name="status">类别</param>
        /// <param name="_dic">参数</param>
        public F_AQL_CheckthedataMAX(int status, Dictionary<string, object> _dic, int _source_page)
        {
            InitializeComponent();
            dic = _dic;
            SourcePage = _source_page;
            AQL_EDIT_STATE.Add(dic["AQL_EDIT_STATE"].ToString());
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            tabControl1.SelectedIndex = status;//默认哪一个开始
            if (status == 0)
            {
                this.Text = "Check information";//核对资料
                showFrm(new F_AQL_CheckthedataMain1(dic));
            }

        }
        private void F_AQL_CheckthedataMAX_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }
        private void showFrm(Form myForm) 
        {
            myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.TopLevel = false;

            //使用DockStyle进行填充
            myForm.Dock = System.Windows.Forms.DockStyle.Fill;
            //将需要填充窗体的容器设置为窗体的父容器
            myForm.Parent = this.tabControl1.SelectedTab;
            //使用内置函数ADD()进行窗体的添加
            this.tabControl1.SelectedTab.Controls.Add(myForm);
            myForm.Show();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            switch (tabControl1.SelectedTab.Name)
            {
                case "tabPage1":
                    this.Text = "Check_Information";//核对资料
                    showFrm(new F_AQL_CheckthedataMain1(dic));
                    break;
                //点箱
                case "tabPage2":
                    this.Text = "Point_Box";//点箱
                    var dxFrm = new F_AQL_PointBox(dic, this);
                    showFrm(dxFrm);
                    break;
                //AQL录入
                case "tabPage6":
                    this.Text = "AQL_Entry";//AQL录入
                    showFrm(new F_AQL_Entry(dic, this));
                    break;
                //照片
                case "tabPage3":
                    this.Text = "Photo";//照片
                    var frm = new F_AQL_CmaTask_Photo(dic);
                    showFrm(frm);
                    break;
                //湿度录入
                case "tabPage4":
                    this.Text = "Humidity_Entry";//湿度录入
                    showFrm(new F_AQL_AHumidityentry(dic));
                    break;
                //BA录入
                case "tabPage5":
                    this.Text = "BA_Entry";
                    showFrm(new F_AQL_BA_Entry(dic));
                    break;

            }
        }
    
    }
}
