using MaterialSkin;
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
using MaterialSkin.Controls;
using SJeMES_Framework.Common;

namespace SJeMES_TQC
{
    public partial class TQC_DR_View : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private TQC_Task_Edit tqc;
        string art = string.Empty;
        public TQC_DR_View(TQC_Task_Edit _tqc, string _art)
        {
            InitializeComponent();
            tqc = _tqc;
            art = _art;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            //TQC_DR_Data_View();
        }

        
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        public void TQC_DR_Data_View(int PageSize, int PageIndex, out int totalCount)
        {
            totalCount = 0; 
            try
            { 
                Dictionary<string, object> data = new Dictionary<string, object>(); 
                data.Add("art", art);
                data.Add("pageSize", PageSize);
                data.Add("pageIndex", PageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TQCAPI",//类库名
                                          "SJ_TQCAPI.TQC_Task",//类名
                                          "GetReturnDatalist",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));


                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridViewEx1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridViewEx1.Rows.Add();
                        DataGridViewRow dgvr = dataGridViewEx1.Rows[i];
                        // dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        dgvr.Cells["REGION"].Value = dr["REGION"].ToString();
                        dgvr.Cells["FACTORY_NO"].Value = dr["FACTORY_NO"].ToString();
                        dgvr.Cells["FACTORY_NAME"].Value = dr["FACTORY_NAME"].ToString();

                        dgvr.Cells["SALESORGAN_NO"].Value = dr["SALESORGAN_NO"].ToString();
                        dgvr.Cells["SALESORGAN_NAME"].Value = dr["SALESORGAN_NAME"].ToString();

                        dgvr.Cells["ARTICLE"].Value = dr["ARTICLE"].ToString();
                        dgvr.Cells["SHOES_NAME"].Value = dr["SHOES_NAME"].ToString();

                        dgvr.Cells["PRODUCTION_DATE"].Value = dr["PRODUCTION_DATE"].ToString();

                        dgvr.Cells["MASTERCODE"].Value = dr["MASTERCODE"].ToString();
                        dgvr.Cells["MASTERNAME"].Value = dr["MASTERNAME"].ToString();

                        dgvr.Cells["SECONDCODE"].Value = dr["SECONDCODE"].ToString();
                        dgvr.Cells["SECONDNAME"].Value = dr["SECONDNAME"].ToString();

                        dgvr.Cells["FOB"].Value = dr["FOB"].ToString();
                        dgvr.Cells["QTY"].Value = dr["QTY"].ToString();
                        dgvr.Cells["MONEY"].Value = dr["MONEY"].ToString();
                        dgvr.Cells["PRICE"].Value = dr["PRICE"].ToString();
                        dgvr.Cells["RETURN_MONTH"].Value = dr["RETURN_MONTH"].ToString();


                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                //dataGridViewEx1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
             
        }
        

        private void TQC_DR_View_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

           // this.dataGridView1.ClearSelection();
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            pageControl1.BindPageEvent += TQC_DR_Data_View;
            LoadPage();
        }
    }
}
