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

namespace SJeMES_BDM
{
    public partial class F_BDM_QualityStandard_Item_List : Form
    {
        int b;
        public F_BDM_QualityStandard_Item_List(int a)
        {
            InitializeComponent();
            b = a;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_BDM_QualityStandard_Item_List_Load(object sender, EventArgs e)
        {
            if (b == 1)
                //b=1 测试项
                BDM_TESTITEM_M();
            if (b == 2)
                // b=2外观检测项
                BDM_APTESTITEM_M();
            if (b == 3)
                //b=3 试穿检测项
                BDM_TNTESTITEM_M();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //搜索
        private void button3_Click(object sender, EventArgs e)
        {
            if (b == 1)
                //b=1 测试项
                BDM_TESTITEM_M();
            if (b == 2)
                // b=2外观检测项
                BDM_APTESTITEM_M();
            if (b == 3)
                //b=3 试穿检测项
                BDM_TNTESTITEM_M();
        }

        /// <summary>
        /// 查询测试项数据
        /// </summary>
        public void BDM_TESTITEM_M()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("txt_type_code", this.txt_type_code.Text.Trim());
                p.Add("txt_code", this.txt_code.Text.Trim());
                p.Add("txt_name", this.txt_name.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.Generalquality",//类名
                                            "GetBDM_TESTITEM_M",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                //视图数据显示
                this.dgvDetection.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 查询外观测试项数据
        /// </summary>
        public void BDM_APTESTITEM_M()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("txt_type_code", this.txt_type_code.Text.Trim());
                p.Add("txt_code", this.txt_code.Text.Trim());
                p.Add("txt_name", this.txt_name.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.Generalquality",//类名
                                            "GetBDM_APTESTITEM_M",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                //视图数据显示
                this.dgvDetection.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 查询试穿检验项数据
        /// </summary>
        public void BDM_TNTESTITEM_M()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("txt_type_code", this.txt_type_code.Text.Trim());
                p.Add("txt_code", this.txt_code.Text.Trim());
                p.Add("txt_name", this.txt_name.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.Generalquality",//类名
                                            "GetBDM_TNTESTITEM_M",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                //视图数据显示
                this.dgvDetection.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
