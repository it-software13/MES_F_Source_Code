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

namespace SJeMES_TQC
{
    public partial class TQC_Task_Main_Opra_Confirm : MaterialForm
    {
        string task_id;
        List<bool> res;
        public TQC_Task_Main_Opra_Confirm(string _task_id,List<bool> _res)
        {
            InitializeComponent();
            task_id = _task_id;
            res = _res;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_commit_Click(object sender, EventArgs e)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("user_code", tb_user_code.Text);
            data.Add("task_id", task_id);
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TQCAPI",//类库名
                                        "SJ_TQCAPI.TQC_Task",//类名
                                        "CheckTQC_Task_Main_OP",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
            }
            else
            {
                if (ret.RetData == "true")
                {
                    res[0] = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("No permission");
                }
            }
        }
    }
}
