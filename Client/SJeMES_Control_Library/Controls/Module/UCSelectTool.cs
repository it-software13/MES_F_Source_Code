using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Controls
{
    public partial class UCSelectTool : UCControlBase, IContainerControl
    {

        private string _WhereKey;

        private SJeMES_Framework.Class.ClientClass _Client;
        public SJeMES_Framework.Class.ClientClass Client
        {
            get { return _Client; }
            set{
                _Client = value;
                if(value!=null && value.Language != "cn")
                {
                    string sql = @"
SELECT 
ui_tittle AS '功能名称',
ui_code AS '控件ID',
ui_cn AS '控件名称',
ui_en AS '英语名称',
ui_yn AS '粤语名称'
FROM SJQDMS_UILAN where ui_tittle='all' and ui_id='" + label1.Text + "'";
                    DataTable dt = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(_Client.WebServiceUrl, sql, new Dictionary<string, string>());
                    if (dt!=null && dt.Rows.Count>0)
                    {
                        if (value.Language == "en" && dt.Rows.Count > 0)
                            label1.Text = !string.IsNullOrEmpty(dt.Rows[0]["英语名称"].ToString()) ? dt.Rows[0]["英语名称"].ToString() : label1.Text;
                        else
                            label1.Text = !string.IsNullOrEmpty(dt.Rows[0]["粤语名称"].ToString()) ? dt.Rows[0]["粤语名称"].ToString() : label1.Text;
                    }
                    

                }

            }
        }


        public string WhereKey
        {
            get { return _WhereKey; }
        }

        /// <summary>
        /// Keys
        /// </summary>
        private string[] _Keys;
        /// <summary>
        /// 高级查询字段数组
        /// </summary>
        /// <value>The Keys.</value>
        [Description("高级查询字段数组"), Category("自定义")]
        public virtual string[] Keys
        {
            get { return _Keys; }
            set
            {
                _Keys = value;
                
            }
        }

        /// <summary>
        /// IsSelectMore
        /// </summary>
        private bool _IsSelectMore;
        /// <summary>
        /// 是否开启高级查询
        /// </summary>
        /// <value>The IsSelectMore.</value>
        [Description("高级查询字段数组"), Category("自定义")]
        public virtual bool IsSelectMore
        {
            get { return _IsSelectMore; }
            set
            {
                _IsSelectMore = value;
                if(value)
                {
                    this.ucBtnImg2.Show();
                }
                else
                {
                    this.ucBtnImg2.Hide();
                }
            }
        }


        //定义委托
        public delegate void SelectDataHandle(object sender, EventArgs e);
        //定义事件
        public event SelectDataHandle SelectData;


        public UCSelectTool()
        {
            InitializeComponent();
            ucTextBoxEx1.txtInput.KeyPress += TxtInput_KeyPress;
            //string configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");

            //Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configstring);
            //label1.Text = "模糊查询";
            //if (Pconfig["language"] == "hk")
            //{
            //    label1.Text = "模糊查询";
            //}
            //else if (Pconfig["language"] == "en")
            //{
            //    label1.Text = "Fuzzy query";
            //}
        }

        private void TxtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            _WhereKey = ucTextBoxEx1.InputText;
            if (e.KeyChar == 13)
            {
                if (SelectData != null)
                    SelectData(this, new EventArgs());
            }
        }

       

        private void ucBtnImg1_BtnClick(object sender, EventArgs e)
        {
            _WhereKey = ucTextBoxEx1.InputText;
            if (SelectData != null)
                SelectData(this, new EventArgs());
        }

        private void ucBtnImg2_BtnClick(object sender, EventArgs e)
        {

        }
    }
}
