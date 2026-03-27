using MaterialSkin.Controls;
using NewExportExcels;
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

namespace SJeMES_TSM
{
    public partial class Process_List : MaterialForm
    {
        public Process_List()
        {
            InitializeComponent();
        }

        private void Process_List_Load(object sender, EventArgs e)
        {
            LoadProcessType();
        }

        public void LoadProcessType()
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "SJ_TSMAPI",
                    "SJ_TSMAPI.Registration",
                    "LoadProcessType",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                    cb_process_type.Items.Clear();

                    if (dtJson1.Rows.Count > 0)
                    {
                        cb_process_type.Items.Add("");
                        foreach (DataRow dr in dtJson1.Rows)
                        {
                            cb_process_type.Items.Add(dr["PROCESS_TYPE"].ToString());

                        }
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void cb_process_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("TYPE", cb_process_type.Text);
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "SJ_TSMAPI",
                    "SJ_TSMAPI.Registration",
                    "GetProcessList",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                    if (dtJson1.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dtJson1;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = cb_process_type.Text+"_ProcessList.xls";
                ExportExcels.Export(a, dataGridView1);
            }
        }
    }
}
