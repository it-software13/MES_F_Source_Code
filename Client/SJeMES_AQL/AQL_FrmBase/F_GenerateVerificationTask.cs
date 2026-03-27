using MaterialSkin.Controls;
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

namespace SJeMES_AQL.AQL_FrmBase
{
    public partial class F_GenerateVerificationTask : MaterialForm
    {
        //检验类型
        public class TestType
        {
            public string code { get; set; }
            public string value { get; set; }
        }

        //新旧鞋型
        public class NewOldshoe
        {
            public string code { get; set; }
            public string value { get; set; }
        }
        Dictionary<string, string> dics = new Dictionary<string, string>();
        F_AQL_PointBox pFrm;
        public F_GenerateVerificationTask(Dictionary<string, string> _dics, F_AQL_PointBox _pFrm)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dics = _dics;
            pFrm = _pFrm;

            #region 检验类型
            List<TestType> ttList = new List<TestType>();
            TestType t1 = new TestType();
            t1.code = "0";
            t1.value = "Finally";//最终
            ttList.Add(t1);
            TestType t2 = new TestType();
            t2.code = "1";
            t2.value = "Rummage";//翻箱
            ttList.Add(t2);
            TestType t3 = new TestType();
            t3.code = "2";
            t3.value = "Again";//再次
            ttList.Add(t3);
            TestType t4 = new TestType();
            t4.code = "3";
            t4.value = "Rummage_Again";//再次翻箱
            ttList.Add(t4);
            ttList.RemoveAt(Convert.ToInt32(dics["comboBox4"]));
            comboBox4.DataSource = ttList;
            comboBox4.DisplayMember = "value";
            comboBox4.ValueMember = "code";
            #endregion
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            pFrm.G_CLOSE = false;
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            //键值对传值
            p.Add("original_task_no", dics["original_task_no"]);
            p.Add("po", dics["po"]);
            p.Add("INSPECTION_TYPE", this.comboBox4.SelectedValue);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_PointBox",//类名
                                        "GenerateVerificationTask",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            else
            {
                pFrm.G_CLOSE = true;
                MessageBox.Show("Generated Successfully!");//生成成功
                this.Close();
            }
        }
    }
}
