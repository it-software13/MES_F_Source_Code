using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SJeMES_QA
{
    public partial class F_DQA_ShoeShape_trait_Insert_ART : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _shoe_no { get; set; }
        public string[] _art_code { get; set; }
        public bool _checkstate = true;//是否全选，True:全选,false:不全选
        public F_DQA_ShoeShape_trait_Insert_ART(string shoe_no, string art_code, bool checkstate)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _shoe_no = shoe_no;
            _art_code = art_code.Split(',');
            _checkstate = checkstate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string prod_no = string.Empty;
            foreach (System.Data.DataRowView item in this.checkedListBox1.CheckedItems)
            {
                prod_no += item.Row["prod_no"].ToString() + ',';
            }
            this.Tag = prod_no.TrimEnd(',');
            this.Close();
        }

        /// <summary>
        /// 各阶段样品记录添加页面查询ART
        /// </summary>
        /// <returns></returns>
        public void GetART()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("SHOE_NO", _shoe_no);
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.DQA_ShoeShape",//类名
                                            "GetART",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    checkedListBox1.DataSource = dt;
                    checkedListBox1.DisplayMember = "prod_no";
                    checkedListBox1.ValueMember = "prod_no";
                }
                if (_checkstate)
                {
                    CheckSelectAll.Checked = true;
                }
                if (_art_code.Length==0)
                {
                    CheckSelectAll.Checked = true;
                }
                else
                {
                    CheckSelectAll.Checked=false;
                    _checkstate = false;
                }
                //循环
                List<int> checkIndexList = new List<int>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!_checkstate)
                    {
                        //string currItem = checkedListBox1.Items[i].ToString();
                        string currItem = dt.Rows[i]["prod_no"].ToString();
                        if (_art_code.Contains(currItem))
                        {
                            checkIndexList.Add(i);
                        }
                    }
                    else
                    {
                        checkIndexList.Add(i);
                    }
                    
                }
                if (dt.Rows.Count==checkIndexList.Count)
                {
                    CheckSelectAll.Checked = true;
                }

                foreach (var item in checkIndexList)
                {
                    checkedListBox1.SetItemChecked(item, true);
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_DQA_ShoeShape_trait_Insert_ART_Load(object sender, EventArgs e)
        {
            GetART();
        }

        private void CheckSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckSelectAll.Checked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                }
            }
            else
            {

                if (checkedListBox1.Items.Count == checkedListBox1.CheckedItems.Count)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }
            }
        }
        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            int num = checkedListBox1.Items.Count;
            int cicount = checkedListBox1.CheckedItems.Count;
            if (num == cicount)
            {
                CheckSelectAll.Checked = true;
            }
            else
            {
                CheckSelectAll.Checked = false;
            }
        }
    }
}
