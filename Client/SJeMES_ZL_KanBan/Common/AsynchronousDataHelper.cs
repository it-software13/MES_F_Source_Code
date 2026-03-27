using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_ZL_KanBan.Common
{
    public class AsynchronousDataHelper
    {
        public Dictionary<string, DataTable> dic_data;
        /// <summary>
        /// 数据是否获取完
        /// </summary>
        public bool Status { get; set; }
        public Panel _panel { get; set; }
        public FrmWholeLifeMain _frm { get; set; }

        #region 参数
        /// <summary>
        /// ART
        /// </summary>
        public string Art { get; set; }
        /// <summary>
        /// PO
        /// </summary>
        public string PO { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string end_date { get; set; }
        /// <summary>
        /// 页签数量
        /// </summary>
        //public int num { get; set; }

        #endregion 
        public TabControl tabControl { get; set; }

        #region 构造函数
        public AsynchronousDataHelper(Dictionary<string, object> param, TabControl tabControl1, FrmWholeLifeMain frm)
        {
            dic_data = new Dictionary<string, DataTable>();
            Art = param["Art"].ToString();
            PO = param["PO"].ToString();
            start_date = param["start_date"].ToString();
            end_date = param["end_date"].ToString();
            //num = int.Parse(param["num"].ToString());
            tabControl = tabControl1;
            Status = false;
            //_panel = frm.panel1;
            _frm = frm;
        }

        #endregion

        public void GetData()
        {
            try
            {
                foreach (TabPage item in tabControl.TabPages)
                {

                    /*
                    Task.Run(() =>
                    {
                        try
                        {
                            switch (item.Name)
                            {
                                case "QA":
                                    GetQAData(item);
                                    break;
                                case "试穿测试":

                                    break;

                                default:
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }).ContinueWith(t => {
                        if(_frm.tag == item.Tag.ToString())
                        {
                            
                            _panel.Visible = false;
                            _frm.TagDataLoad(_frm.tag);
                        }
                       

                    });//任务状态标记、可选更新UI、关闭当前loading控件（当前选中的页签读取完成时关闭）
                    */
                }
            }
            catch(Exception ex)
            {

            }
        }

        Dictionary<string, bool> dicTaskStatus;
        //callback
        

        //获取QA页签数据
        private void GetQAData(TabPage tabPage)
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值  
                data.Add("Art", Art);//名称
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetQAData",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                { 
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dic_data.Add(tabPage.Name, dt);
            }
            catch (Exception ex)
            {
                throw new Exception("000:"+ex.Message);
            }


        }


    }




}
